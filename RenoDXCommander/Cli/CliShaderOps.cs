// CliShaderOps.cs — per-game shader selection and ReShade channel overrides.
// Both commands write the same settings.json keys the cards write, so a terminal change is
// indistinguishable from a GUI change. Neither needs admin and neither touches C:\ProgramData.
using System.Text.Json;
using RenoDXCommander.Models;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// Shader and channel state for one game. Destructive stays false on both commands because the
/// CLI's global --yes gate only covers commands that touch game folders; these two re-check it
/// themselves before writing settings or redeploying anything.
/// </summary>
internal static class CliShaderOps
{
    internal static readonly CliCommand[] All = [ShadersSet(), Channel()];

    private const string ModeKey = "PerGameShaderMode";
    private const string SelectionKey = "PerGameShaderSelection";
    private const string ChannelKey = "ReShadeChannelOverrides";
    private const string CustomKey = "CustomReShadeSelection";
    private const string NormalKey = "NormalReShadeGames";

    // ── reshade shaders set ───────────────────────────────────────────────────────

    private static CliCommand ShadersSet() => new()
    {
        Name = "reshade shaders set",
        Group = "config",
        Summary = "Choose the shader packs a game deploys, or turn managed shaders off.",
        Usage = "rhi reshade shaders set <game> <id,id|off> [--dry-run] [--yes]",
        Examples =
        [
            "rhi reshade shaders set \"Cyberpunk 2077\" off --yes",
            "rhi reshade shaders set cyberpunk \"OtisFX,Depth3D\" --yes",
        ],
        Destructive = false,
        RunAsync = ShadersSetAsync,
    };

    private static async Task<CliResult> ShadersSetAsync(CliInvocation invocation)
    {
        var args = invocation.Args;
        if (args.Length < 2)
            return CliResult.Usage("usage: rhi reshade shaders set <game> <id,id|off> [--dry-run] [--yes]");

        // The id list never contains spaces, so the last word is the value and the rest is the game.
        var wanted = args[^1].Trim();
        var name = string.Join(' ', args[..^1]).Trim();
        if (string.IsNullOrWhiteSpace(name))
            return CliResult.Usage("usage: rhi reshade shaders set <game> <id,id|off> [--dry-run] [--yes]");

        var game = CliGames.ResolveOne(name, out var failure);
        if (game is null) return failure!;

        var off = string.Equals(wanted, "off", StringComparison.OrdinalIgnoreCase);
        var ids = off
            ? new List<string>()
            : wanted.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();

        if (!off && ids.Count == 0)
            return CliResult.Usage("no pack ids given - pass a comma-separated list of pack ids, or 'off'");

        var store = game.Source ?? "";
        var key = GameKey.From(game.Name, store).ToKey();
        var settings = SettingsFile.Load();
        var previousMode = FirstObjectValue(ReadObject(settings, ModeKey), key, game.Name) ?? "Global";

        var plan = new
        {
            game = game.Name,
            store,
            key,
            mode = off ? "Off" : "Select",
            packs = ids,
            previousMode,
            installPath = game.InstallPath,
        };

        if (invocation.DryRun)
            return CliResult.Ok(
                $"would set shaders for '{game.Name}' to {(off ? "off" : string.Join(", ", ids))} (was {previousMode}) - nothing written",
                new { plan, dryRun = true });

        if (!invocation.Confirmed)
            return CliResult.Usage($"refusing to change shader settings without --yes: rhi reshade shaders set \"{game.Name}\" {wanted} --yes");

        // Only these two keys are written. Do NOT route this through GameNameService.SaveNameMappings:
        // that serialises the service's in-memory snapshot, and in a CLI process LoadNameMappings never
        // ran, so it would replace every other key in settings.json with defaults.
        var saved = SettingsFile.Update(s =>
        {
            var modes = ReadObject(s, ModeKey);
            modes[key] = off ? "Off" : "Select";
            s[ModeKey] = JsonSerializer.Serialize(modes);

            var selections = ReadSelections(s);
            selections.Remove(key);
            selections.Remove(game.Name); // legacy name-only entry
            if (!off) selections[key] = ids;
            s[SelectionKey] = JsonSerializer.Serialize(selections);
        }, "cli:reshade shaders set");

        if (!saved)
            return CliResult.Fail($"could not write {SettingsFile.FilePath} - file locked or unreadable", plan);

        var shaders = CliServices.Get<IShaderPackService>();

        // "off" -> null selection removes the managed reshade-shaders folder and restores the user's.
        if (off)
        {
            shaders.SyncGameFolder(game.InstallPath, null, null);
            return CliResult.Ok(
                $"shader packs are off for '{game.Name}' - managed reshade-shaders removed",
                new { game = game.Name, store, key, mode = "Off", packs = Array.Empty<string>() });
        }

        // Ids are passed through unchanged: __custom__, __custom_files__ and __custom_files__* are
        // sentinels ShaderPackService understands, so they are not filtered or validated here.
        await shaders.EnsurePacksAsync(ids);
        var exclusions = ids.ToDictionary(id => id, id => shaders.GetExcludedFiles(id), StringComparer.OrdinalIgnoreCase);
        shaders.SyncGameFolder(game.InstallPath, ids, exclusions);

        var files = CountShaderFiles(game.InstallPath);
        return CliResult.Ok(
            $"shader packs set for '{game.Name}': {string.Join(", ", ids)} ({files} shader file(s) in the game folder)",
            new { game = game.Name, store, key, mode = "Select", packs = ids, files });
    }

    // ── reshade channel ───────────────────────────────────────────────────────────

    private static CliCommand Channel() => new()
    {
        Name = "reshade channel",
        Group = "config",
        Summary = "Set a game's ReShade channel or version, then reinstall ReShade when it is installed.",
        Usage = "rhi reshade channel <game> <stable|nightly|custom:<dll>|legacy:<version>|noaddons> [--dry-run] [--yes]",
        Examples =
        [
            "rhi reshade channel \"Cyberpunk 2077\" nightly --yes",
            "rhi reshade channel cyberpunk legacy:6.4.1 --yes",
            "rhi reshade channel cyberpunk custom:ReShade64.dll --yes",
            "rhi reshade channel cyberpunk stable --dry-run",
        ],
        Destructive = false,
        RunAsync = ChannelAsync,
    };

    private static async Task<CliResult> ChannelAsync(CliInvocation invocation)
    {
        var args = invocation.Args;
        if (args.Length < 2)
            return CliResult.Usage("usage: rhi reshade channel <game> <stable|nightly|custom:<dll>|legacy:<version>|noaddons> [--dry-run] [--yes]");

        // The channel token never contains spaces, so the last word is the value and the rest is the game.
        var raw = args[^1].Trim();
        var name = string.Join(' ', args[..^1]).Trim();
        if (string.IsNullOrWhiteSpace(name))
            return CliResult.Usage("usage: rhi reshade channel <game> <stable|nightly|custom:<dll>|legacy:<version>|noaddons> [--dry-run] [--yes]");

        var game = CliGames.ResolveOne(name, out var failure);
        if (game is null) return failure!;

        var store = game.Source ?? "";
        var key = GameKey.From(game.Name, store).ToKey();

        string? channel;
        string? customDll = null;
        var noAddons = false;

        if (raw.Equals("stable", StringComparison.OrdinalIgnoreCase))
        {
            channel = null; // override removed -> global Stable
        }
        else if (raw.Equals("nightly", StringComparison.OrdinalIgnoreCase))
        {
            channel = AuxInstallService.ChannelNightly;
        }
        else if (raw.Equals("noaddons", StringComparison.OrdinalIgnoreCase))
        {
            noAddons = true;
            channel = null;
        }
        else if (raw.StartsWith("custom:", StringComparison.OrdinalIgnoreCase))
        {
            customDll = raw["custom:".Length..].Trim();
            if (customDll.Length == 0)
                return CliResult.Usage("custom: needs a DLL filename, e.g. custom:ReShade64.dll");

            var customPath = AuxInstallService.GetCustomReShadePathForFile(customDll);
            if (!File.Exists(customPath))
                return CliResult.Fail($"custom ReShade DLL not found: {customPath}");

            channel = AuxInstallService.ChannelCustom;
        }
        else if (raw.StartsWith("legacy:", StringComparison.OrdinalIgnoreCase))
        {
            channel = raw["legacy:".Length..].Trim();
            if (channel.Length == 0)
                return CliResult.Usage("legacy: needs a version, e.g. legacy:6.4.1");
        }
        else
        {
            return CliResult.Usage($"unknown ReShade channel '{raw}' - use stable, nightly, custom:<dll>, legacy:<version> or noaddons");
        }

        var label = noAddons
            ? "normal ReShade (no addons)"
            : channel is null
                ? "stable (override removed)"
                : string.Equals(channel, AuxInstallService.ChannelCustom, StringComparison.OrdinalIgnoreCase)
                    ? $"custom ({customDll})"
                    : string.Equals(channel, AuxInstallService.ChannelNightly, StringComparison.OrdinalIgnoreCase)
                        ? "nightly"
                        : $"legacy {channel}";

        var installer = CliServices.Get<IAuxInstallService>();
        var existing = installer.FindRecord(game.Name, game.InstallPath, AuxInstallService.TypeReShade)
                       ?? installer.FindRecord(game.Name, game.InstallPath, AuxInstallService.TypeReShadeNormal);
        var settings = SettingsFile.Load();

        var plan = new
        {
            game = game.Name,
            store,
            key,
            channel = noAddons ? null : channel,
            customDll,
            noAddons,
            installPath = game.InstallPath,
            installedAs = existing?.InstalledAs,
            currentChannel = FirstObjectValue(ReadObject(settings, ChannelKey), key, game.Name),
        };

        if (invocation.DryRun)
            return CliResult.Ok(
                existing is null
                    ? $"would set the ReShade channel for '{game.Name}' to {label}; ReShade is not installed, so nothing would be reinstalled"
                    : $"would set the ReShade channel for '{game.Name}' to {label} and reinstall {existing.InstalledAs}",
                new { plan, dryRun = true });

        if (!invocation.Confirmed)
            return CliResult.Usage($"refusing to change the ReShade channel without --yes: rhi reshade channel \"{game.Name}\" {raw} --yes");

        var saved = SettingsFile.Update(s =>
        {
            var channels = ReadObject(s, ChannelKey);
            channels.Remove(key);
            channels.Remove(game.Name); // legacy name-only entry; "stable" must clear both
            if (channel is not null) channels[key] = channel;
            s[ChannelKey] = JsonSerializer.Serialize(channels);

            // Only custom:<dll> sets a pick; switching away keeps the last one, like the GUI. The map
            // is always serialised so the key survives exactly as GameNameService.SaveNameMappings writes it.
            var custom = ReadObject(s, CustomKey);
            if (customDll is not null)
            {
                custom.Remove(game.Name); // legacy name-only entry
                custom[key] = customDll;
            }
            s[CustomKey] = JsonSerializer.Serialize(custom);

            var normal = ReadList(s, NormalKey);
            normal.RemoveAll(v => string.Equals(v, key, StringComparison.OrdinalIgnoreCase)
                               || string.Equals(v, game.Name, StringComparison.OrdinalIgnoreCase));
            if (noAddons) normal.Add(key);
            s[NormalKey] = JsonSerializer.Serialize(normal);
        }, "cli:reshade channel");

        if (!saved)
            return CliResult.Fail($"could not write {SettingsFile.FilePath} - file locked or unreadable", plan);

        // No install record: do not guess at a Vulkan layer (that writes under C:\ProgramData and
        // needs admin). Point at the dedicated installer path instead.
        if (existing is null)
            return CliResult.Ok(
                $"ReShade channel for '{game.Name}' set to {label}; ReShade is not installed here, so nothing was reinstalled. " +
                $"Vulkan layer sync is handled by: rhi install reshade \"{game.Name}\" --vulkan",
                new { game = game.Name, store, key, channel, customDll, noAddons, reinstalled = false });

        var is32Bit = ResolveIs32Bit(game.InstallPath);
        var stagingChannel = noAddons
            ? AuxInstallService.ChannelStable
            : channel ?? AuxInstallService.ChannelStable;

        if (noAddons
            || string.Equals(channel, AuxInstallService.ChannelStable, StringComparison.OrdinalIgnoreCase)
            || string.Equals(channel, AuxInstallService.ChannelNightly, StringComparison.OrdinalIgnoreCase))
        {
            if (!invocation.Json) CliConsole.WriteLine("staging ReShade (downloads on first use)...");
            if (!await EnsureStagedAsync(noAddons, stagingChannel, is32Bit))
                return CliResult.Fail("could not stage ReShade - check the connection and try again", plan);
        }
        // custom: the DLL was verified above. legacy: InstallReShadeAsync downloads it on demand.

        // The GUI installs through this hook, but a CLI process never runs the GUI initialiser, so
        // wire it to settings.json when absent (a live GUI's own resolver still wins).
        AuxInstallService.CustomReShadeSelectionResolver ??= (gameName, st) =>
            FirstObjectValue(ReadObject(SettingsFile.Load(), CustomKey),
                GameKey.From(gameName, st ?? "").ToKey(), gameName);

        var hotkeys = SettingsFile.Load();
        var record = await installer.InstallReShadeAsync(
            game.Name,
            game.InstallPath,
            shaderModeOverride: null,
            use32Bit: is32Bit,
            filenameOverride: null,
            selectedPackIds: null,
            progress: null,
            screenshotSavePath: Value(hotkeys, "ScreenshotPath"),
            useNormalReShade: noAddons,
            overlayHotkey: Value(hotkeys, "OverlayHotkey"),
            screenshotHotkey: Value(hotkeys, "ScreenshotHotkey"),
            channel: noAddons ? null : channel,
            store: store,
            mergeIni: true);

        return CliResult.Ok(
            $"ReShade channel for '{game.Name}' is now {label}; reinstalled as {record.InstalledAs} (channel {record.Channel ?? label})",
            new { game = game.Name, store, key, channel, customDll, noAddons, reinstalled = true, record.InstalledAs, record.Channel, record.InstalledAt });
    }

    // ── Shared helpers ────────────────────────────────────────────────────────────

    private static Task<bool> EnsureStagedAsync(bool useNormal, string channel, bool is32Bit)
    {
        var nightly = string.Equals(channel, AuxInstallService.ChannelNightly, StringComparison.OrdinalIgnoreCase);
        return EnsureStagedCoreAsync(useNormal, nightly, is32Bit);
    }

    private static async Task<bool> EnsureStagedCoreAsync(bool useNormal, bool nightly, bool is32Bit)
    {
        try
        {
            if (useNormal) await CliServices.Get<INormalReShadeUpdateService>().EnsureLatestAsync();
            else if (nightly) await CliServices.Get<ReShadeNightlyService>().EnsureLatestAsync();
            else await CliServices.Get<IReShadeUpdateService>().EnsureLatestAsync();
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or IOException)
        {
            return false;
        }

        var staged = useNormal
            ? is32Bit ? AuxInstallService.RsNormalStagedPath32 : AuxInstallService.RsNormalStagedPath64
            : nightly
                ? is32Bit ? AuxInstallService.RsNightlyStagedPath32 : AuxInstallService.RsNightlyStagedPath64
                : is32Bit ? AuxInstallService.RsStagedPath32 : AuxInstallService.RsStagedPath64;

        return File.Exists(staged) && new FileInfo(staged).Length > AuxInstallService.MinReShadeSize;
    }

    /// <summary>
    /// Same lookup CliInstall uses: the library's bitness cache first, the PE header as a fallback.
    /// A wrong answer here installs a proxy the game's loader silently skips.
    /// </summary>
    private static bool ResolveIs32Bit(string installPath)
    {
        var library = CliGames.LoadLibrary(out _);
        if (library is not null)
        {
            var resolved = CliGames.LookupString(library.ResolvedPathCache, installPath);
            if (resolved.Length == 0) resolved = installPath;
            var cached = CliGames.LookupBitness(library.BitnessCache, resolved);
            if (cached.Length > 0)
                return string.Equals(cached, MachineType.I386.ToString(), StringComparison.OrdinalIgnoreCase);
        }

        try
        {
            return CliServices.Get<IPeHeaderService>().DetectGameArchitecture(installPath) == MachineType.I386;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or BadImageFormatException
                                       or ArgumentException or NotSupportedException)
        {
            return false;
        }
    }

    private static int CountShaderFiles(string installPath)
    {
        try
        {
            var dir = Path.Combine(installPath, ShaderPackService.GameReShadeShaders, "Shaders");
            return Directory.Exists(dir)
                ? Directory.EnumerateFiles(dir, "*", SearchOption.AllDirectories).Count()
                : 0;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            return 0;
        }
    }

    private static Dictionary<string, string> ReadObject(Dictionary<string, string> settings, string settingKey)
    {
        if (!settings.TryGetValue(settingKey, out var raw) || string.IsNullOrWhiteSpace(raw))
            return new(StringComparer.OrdinalIgnoreCase);

        try
        {
            return new(JsonSerializer.Deserialize<Dictionary<string, string>>(raw) ?? new(),
                StringComparer.OrdinalIgnoreCase);
        }
        catch (JsonException) { return new(StringComparer.OrdinalIgnoreCase); }
    }

    private static Dictionary<string, List<string>> ReadSelections(Dictionary<string, string> settings)
    {
        if (!settings.TryGetValue(SelectionKey, out var raw) || string.IsNullOrWhiteSpace(raw))
            return new(StringComparer.OrdinalIgnoreCase);

        try
        {
            var map = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(raw);
            return map is null ? new(StringComparer.OrdinalIgnoreCase) : new(map, StringComparer.OrdinalIgnoreCase);
        }
        catch (JsonException) { return new(StringComparer.OrdinalIgnoreCase); }
    }

    private static List<string> ReadList(Dictionary<string, string> settings, string settingKey)
    {
        if (!settings.TryGetValue(settingKey, out var raw) || string.IsNullOrWhiteSpace(raw)) return [];

        try { return JsonSerializer.Deserialize<List<string>>(raw) ?? []; }
        catch (JsonException) { return []; }
    }

    private static string? FirstObjectValue(Dictionary<string, string> map, string key, string name) =>
        map.TryGetValue(key, out var value) ? value
        : map.TryGetValue(name, out var bare) ? bare
        : null;

    private static string? Value(Dictionary<string, string> settings, string key) =>
        settings.TryGetValue(key, out var value) && !string.IsNullOrWhiteSpace(value) ? value : null;
}
