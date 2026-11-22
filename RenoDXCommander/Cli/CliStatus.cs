using System.Diagnostics;
using System.Text;
using RenoDXCommander.Models;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

internal static class CliStatus
{
    internal static readonly CliCommand[] All = [StatusCommand()];

    private static CliCommand StatusCommand() => new()
    {
        Name = "status",
        Group = "core",
        Summary = "One-screen summary: version, library, installed components, pending updates, overlays.",
        Usage = "rhi status [--json]",
        Examples = ["rhi status"],
        Run = _ =>
        {
            var settings = SettingsFile.Load();
            var library = CliServices.Get<IGameLibraryService>().Load();
            var text = new StringBuilder();

            void Line(string label, string value) => text.AppendLine(label.PadRight(18) + value);

            Line("version", CrashReporter.AppVersion);
            Line("exe", Environment.ProcessPath ?? "");
            Line("gui running", IsAnotherRhiRunning() ? "yes" : "no");
            Line("settings", settings.Count + " keys");
            Line("reshade overlay", Value(settings, "OverlayHotkey"));
            Line("rhi overlay", RhiOverlayHotkey(settings));

            var games = library?.Games.Concat(library.ManualGames).GroupBy(g => g.Name + "|" + g.Source).Select(g => g.First()).ToList() ?? new List<SavedGame>();
            var reshade = library?.RsInstalledVersions?.Count ?? 0;
            var renodx = library?.RdxInstalledVersions?.Count ?? 0;
            var dxvk = library?.DxvkInstalledVersions?.Count ?? 0;
            var updates = library?.UpdateAvailableSnapshot?.Count ?? 0;

            Line("games", games.Count.ToString());
            Line("last scanned", library is null || library.LastScanned == default ? "never" : library.LastScanned.ToString("yyyy-MM-dd HH:mm"));
            Line("with reshade", reshade.ToString());
            Line("with renodx", renodx.ToString());
            Line("with dxvk", dxvk.ToString());
            Line("pending updates", updates.ToString());

            return CliResult.Ok(text.ToString().TrimEnd(), new
            {
                version = CrashReporter.AppVersion,
                exe = Environment.ProcessPath,
                guiRunning = IsAnotherRhiRunning(),
                settingsKeys = settings.Count,
                reshadeOverlayHotkey = Value(settings, "OverlayHotkey"),
                rhiOverlayHotkey = RhiOverlayHotkey(settings),
                games = games.Count,
                lastScanned = library?.LastScanned,
                withReShade = reshade,
                withRenoDx = renodx,
                withDxvk = dxvk,
                pendingUpdates = updates,
            });
        },
    };

    private static string Value(Dictionary<string, string> settings, string key) =>
        settings.TryGetValue(key, out var value) ? value : "-";

    private static string RhiOverlayHotkey(Dictionary<string, string> settings) =>
        settings.TryGetValue("RhiOverlayHotkey", out var value) && !string.IsNullOrWhiteSpace(value)
            ? value
            : ViewModels.SettingsViewModel.DefaultRhiOverlayHotkey + " (default)";

    private static bool IsAnotherRhiRunning()
    {
        var self = Environment.ProcessId;
        foreach (var process in Process.GetProcessesByName("RHI"))
        {
            using (process)
            {
                if (process.Id != self) return true;
            }
        }
        return false;
    }
}
