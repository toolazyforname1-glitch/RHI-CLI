// rhi-live.addon32 - a command channel into a running ReShade, so the RHI CLI can read and change
// effects while a game renders. Commands arrive on a named pipe; the work itself runs on the render
// thread (addon_event::reshade_present), because the ReShade effect API is not free-threaded.
#include <windows.h>
#include <chrono>
#include <condition_variable>
#include <mutex>
#include <string>
#include <vector>
#include "reshade.hpp"

namespace {

constexpr const wchar_t *kPipeName = L"\\\\.\\pipe\\RHI_ReShadeAddon";

std::mutex g_mutex;
std::condition_variable g_cv;
std::string g_request;
std::string g_response;
bool g_has_work = false;
bool g_ready = false;
volatile bool g_stop = false;

std::string Escape(const std::string &in) {
	std::string out;
	out.reserve(in.size() + 8);
	for (const char c : in) {
		switch (c) {
		case '"': out += "\\\""; break;
		case '\\': out += "\\\\"; break;
		case '\n': out += "\\n"; break;
		case '\r': out += "\\r"; break;
		case '\t': out += "\\t"; break;
		default: out += c; break;
		}
	}
	return out;
}

bool LogEnabled() {
	static int cached = -1;
	if (cached >= 0) return cached == 1;

	char path[MAX_PATH] = {};
	GetModuleFileNameA(nullptr, path, MAX_PATH);
	if (char *slash = strrchr(path, '\\')) slash[1] = 0;
	strcat_s(path, "rhi-live.debug");

	cached = GetFileAttributesA(path) != INVALID_FILE_ATTRIBUTES ? 1 : 0;
	return cached == 1;
}

void Log(const char *text) {
	if (!LogEnabled()) return;

	char path[MAX_PATH] = {};
	GetModuleFileNameA(nullptr, path, MAX_PATH);
	if (char *slash = strrchr(path, '\\')) slash[1] = 0;
	strcat_s(path, "rhi-live.log");

	HANDLE file = CreateFileA(path, FILE_APPEND_DATA, FILE_SHARE_READ, nullptr, OPEN_ALWAYS, FILE_ATTRIBUTE_NORMAL, nullptr);
	if (file == INVALID_HANDLE_VALUE) return;

	LARGE_INTEGER size = {};
	if (GetFileSizeEx(file, &size) && size.QuadPart > 256 * 1024) {
		CloseHandle(file);
		return;
	}

	DWORD written = 0;
	WriteFile(file, text, (DWORD)strlen(text), &written, nullptr);
	WriteFile(file, "\r\n", 2, &written, nullptr);
	CloseHandle(file);
}
std::string Ok(const std::string &body) { return "{\"ok\":true," + body + "}"; }
std::string Fail(const std::string &message) { return "{\"ok\":false,\"error\":\"" + Escape(message) + "\"}"; }

std::string PresetPath(reshade::api::effect_runtime *runtime) {
	char path[2048] = {};
	runtime->get_current_preset_path(path);
	return path;
}

struct TechniqueList {
	std::vector<std::string> items;
};

void CollectTechnique(reshade::api::effect_runtime *runtime, reshade::api::effect_technique technique, void *user_data) {
	auto *list = static_cast<TechniqueList *>(user_data);
	char name[512] = {};
	runtime->get_technique_name(technique, name);
	list->items.push_back(std::string("{\"name\":\"") + Escape(name) + "\",\"enabled\":" +
		(runtime->get_technique_state(technique) ? "true" : "false") + "}");
}

struct TechniqueMatch {
	std::string wanted;
	reshade::api::effect_technique found = {};
	bool matched = false;
};

void MatchTechnique(reshade::api::effect_runtime *runtime, reshade::api::effect_technique technique, void *user_data) {
	auto *match = static_cast<TechniqueMatch *>(user_data);
	if (match->matched) return;
	char name[512] = {};
	runtime->get_technique_name(technique, name);
	if (match->wanted == name) {
		match->matched = true;
		match->found = technique;
	}
}

std::string Execute(const std::string &request, reshade::api::effect_runtime *runtime) {
	std::string line = request;
	while (!line.empty() && (line.back() == '\r' || line.back() == '\n')) line.pop_back();

	std::string verb = line;
	std::string rest;
	const auto space = line.find(' ');
	if (space != std::string::npos) {
		verb = line.substr(0, space);
		rest = line.substr(space + 1);
	}

	if (verb == "ping") {
		return Ok("\"addon\":\"rhi-live\",\"effects\":" + std::string(runtime->get_effects_state() ? "true" : "false") +
			",\"preset\":\"" + Escape(PresetPath(runtime)) + "\"");
	}

	if (verb == "effects") {
		if (rest == "on" || rest == "off") runtime->set_effects_state(rest == "on");
		return Ok("\"enabled\":" + std::string(runtime->get_effects_state() ? "true" : "false"));
	}

	if (verb == "techniques") {
		TechniqueList list;
		runtime->enumerate_techniques(nullptr, &CollectTechnique, &list);
		std::string body = "\"count\":" + std::to_string(list.items.size()) + ",\"techniques\":[";
		for (size_t i = 0; i < list.items.size(); ++i) {
			if (i > 0) body += ",";
			body += list.items[i];
		}
		body += "]";
		return Ok(body);
	}

	if (verb == "technique") {
		const auto state = rest.rfind(' ');
		if (state == std::string::npos) return Fail("expected: technique <name> on|off");
		const std::string name = rest.substr(0, state);
		const std::string value = rest.substr(state + 1);

		TechniqueMatch match;
		match.wanted = name;
		runtime->enumerate_techniques(nullptr, &MatchTechnique, &match);
		if (!match.matched) return Fail("no technique named '" + name + "'");

		const bool enabled = value == "on";
		runtime->set_technique_state(match.found, enabled);
		return Ok("\"technique\":\"" + Escape(name) + "\",\"enabled\":" + (enabled ? "true" : "false"));
	}

	if (verb == "preset") {
		if (!rest.empty()) runtime->set_current_preset_path(rest.c_str());
		return Ok("\"path\":\"" + Escape(PresetPath(runtime)) + "\"");
	}

	return Fail("unknown command '" + verb + "'");
}

void OnPresent(reshade::api::effect_runtime *runtime) {
	std::string request;
	{
		std::lock_guard<std::mutex> lock(g_mutex);
		if (!g_has_work || g_ready) return;
		request = g_request;
	}

	const std::string response = Execute(request, runtime);

	{
		std::lock_guard<std::mutex> lock(g_mutex);
		g_response = response;
		g_ready = true;
	}
	g_cv.notify_all();
}

void OnDestroyRuntime(reshade::api::effect_runtime *) {
	std::lock_guard<std::mutex> lock(g_mutex);
	if (g_has_work && !g_ready) {
		g_response = Fail("the effect runtime went away");
		g_ready = true;
	}
	g_cv.notify_all();
}

DWORD WINAPI PipeLoop(LPVOID) {
	while (!g_stop) {
		HANDLE pipe = CreateNamedPipeW(kPipeName, PIPE_ACCESS_DUPLEX,
			PIPE_TYPE_BYTE | PIPE_READMODE_BYTE | PIPE_WAIT,
			PIPE_UNLIMITED_INSTANCES, 8192, 8192, 0, nullptr);

		if (pipe == INVALID_HANDLE_VALUE) {
			Sleep(250);
			continue;
		}

		const bool connected = ConnectNamedPipe(pipe, nullptr) != 0 || GetLastError() == ERROR_PIPE_CONNECTED;
		Log(connected ? "client connected" : "connect failed");
		if (!connected) {
			CloseHandle(pipe);
			continue;
		}

		char buffer[1024] = {};
		DWORD read = 0;
		std::string received;
		while (received.find('\n') == std::string::npos && received.size() < 4000) {
			if (!ReadFile(pipe, buffer, sizeof(buffer), &read, nullptr) || read == 0) break;
			received.append(buffer, read);
		}
		Log(("request: " + received).c_str());

		if (!received.empty()) {
			{
				std::lock_guard<std::mutex> lock(g_mutex);
				g_request = received;
				g_response.clear();
				g_has_work = true;
				g_ready = false;
			}
			g_cv.notify_all();

			std::string response;
			{
				std::unique_lock<std::mutex> lock(g_mutex);
				g_cv.wait_for(lock, std::chrono::seconds(10), [] { return g_ready; });
				response = g_ready ? g_response : Fail("no frame was rendered in time (is the game paused?)");
				g_has_work = false;
				g_ready = false;
			}

			DWORD written = 0;
			const BOOL wrote = WriteFile(pipe, response.data(), static_cast<DWORD>(response.size()), &written, nullptr);
			Log(("reply: " + response + " wrote=" + std::to_string(wrote ? written : 0) +
				" err=" + std::to_string(wrote ? 0 : GetLastError())).c_str());
			FlushFileBuffers(pipe);
		}

		DisconnectNamedPipe(pipe);
		CloseHandle(pipe);
	}
	return 0;
}

} // namespace

BOOL APIENTRY DllMain(HMODULE module, DWORD reason, LPVOID) {
	if (reason == DLL_PROCESS_ATTACH) {
		DisableThreadLibraryCalls(module);
		if (!reshade::register_addon(module))
			return FALSE;

		reshade::register_event<reshade::addon_event::reshade_present>(&OnPresent);
		reshade::register_event<reshade::addon_event::destroy_effect_runtime>(&OnDestroyRuntime);

		CreateThread(nullptr, 0, PipeLoop, nullptr, 0, nullptr);
	} else if (reason == DLL_PROCESS_DETACH) {
		g_stop = true;
		reshade::unregister_addon(module);
	}
	return TRUE;
}
