using System.Diagnostics;
using System.Text;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// UI-state commands. These write the settings the window reads on startup, so the change lands
/// at the next launch - and the result says so instead of pretending it applied live.
/// </summary>
internal static class CliUi
{
    private static readonly string[] Layouts = ["Classic", "Tabs", "Spotlight"];
    private static readonly string[] Views = ["Detail", "Compact"];

    internal static readonly CliCommand[] All =
    [
        Show(), Layout(), Scale(), ListScale(), Tab(), View(), SettingsSection(), SpotlightCover(),
    ];

    private static CliCommand Show() => new()
    {
        Name = "ui show",
        Group = "ui",
        Summary = "Show the stored UI state (layout, scale, selected tab, last settings section).",
        Usage = "rhi ui show [--json]",
        Examples = ["rhi ui show"],
        Run = _ =>
        {
            var settings = SettingsFile.Load();
            var text = new StringBuilder();
            foreach (var key in new[] { "MainLayout", "ViewLayout", "UiScale", "GameListScale", "DetailSelectedTab", "SettingsLastSection", "SpotlightCoverScale" })
                text.AppendLine(key.PadRight(22) + Read(settings, key));

            return CliResult.Ok(text.ToString().TrimEnd(), new
            {
                mainLayout = Read(settings, "MainLayout"),
                viewLayout = Read(settings, "ViewLayout"),
                uiScale = Read(settings, "UiScale"),
                gameListScale = Read(settings, "GameListScale"),
                selectedTab = Read(settings, "DetailSelectedTab"),
                settingsSection = Read(settings, "SettingsLastSection"),
                spotlightCoverScale = Read(settings, "SpotlightCoverScale"),
            });
        },
    };

    private static CliCommand Layout() => new()
    {
        Name = "ui layout",
        Group = "ui",
        Summary = "Set the main layout (Classic, Tabs, Spotlight). Applies at next launch.",
        Usage = "rhi ui layout <Classic|Tabs|Spotlight> [--json]",
        Examples = ["rhi ui layout Tabs"],
        Run = invocation => SetChoice(invocation, "MainLayout", "layout", Layouts),
    };

    private static CliCommand View() => new()
    {
        Name = "ui view",
        Group = "ui",
        Summary = "Set the game list view (Detail, Compact). Applies at next launch.",
        Usage = "rhi ui view <Detail|Compact> [--json]",
        Run = invocation =>
        {
            var wanted = invocation.Operand(0);
            if (wanted is null) return CliResult.Usage("usage: rhi ui view <Detail|Compact>");
            var match = Views.FirstOrDefault(v => v.Equals(wanted, StringComparison.OrdinalIgnoreCase));
            if (match is null) return CliResult.Usage($"view must be one of: {string.Join(", ", Views)}");
            return Write("ViewLayout", match == "Compact" ? "2" : "0", "view", match);
        },
    };

    private static CliCommand Scale() => new()
    {
        Name = "ui scale",
        Group = "ui",
        Summary = "Set the interface scale percentage (75-200). Applies at next launch.",
        Usage = "rhi ui scale <75-200> [--json]",
        Run = invocation => SetNumber(invocation, "UiScale", "interface scale", 75, 200),
    };

    private static CliCommand ListScale() => new()
    {
        Name = "ui list-scale",
        Group = "ui",
        Summary = "Set the game list scale percentage (75-200). Applies at next launch.",
        Usage = "rhi ui list-scale <75-200> [--json]",
        Run = invocation => SetNumber(invocation, "GameListScale", "game list scale", 75, 200),
    };

    private static CliCommand Tab() => new()
    {
        Name = "ui tab",
        Group = "ui",
        Summary = "Set the tab the detail panel opens on next launch.",
        Usage = "rhi ui tab <name> [--json]",
        Examples = ["rhi ui tab Overview"],
        Run = invocation => SetText(invocation, "DetailSelectedTab", "detail tab"),
    };

    private static CliCommand SettingsSection() => new()
    {
        Name = "ui settings",
        Group = "ui",
        Summary = "Set the settings section RHI opens on next launch.",
        Usage = "rhi ui settings <section> [--json]",
        Examples = ["rhi ui settings ReShade"],
        Run = invocation => SetText(invocation, "SettingsLastSection", "settings section"),
    };

    private static CliCommand SpotlightCover() => new()
    {
        Name = "ui spotlight-cover",
        Group = "ui",
        Summary = "Set the Spotlight cover scale (0.5-2.0). Applies at next launch.",
        Usage = "rhi ui spotlight-cover <0.5-2.0> [--json]",
        Run = invocation =>
        {
            var raw = invocation.Operand(0);
            if (!double.TryParse(raw, System.Globalization.CultureInfo.InvariantCulture, out var value) || value < 0.5 || value > 2.0)
                return CliResult.Usage("usage: rhi ui spotlight-cover <0.5-2.0>");
            return Write("SpotlightCoverScale", value.ToString(System.Globalization.CultureInfo.InvariantCulture), "spotlight cover scale");
        },
    };

    private static CliResult SetChoice(CliInvocation invocation, string key, string label, string[] allowed)
    {
        var wanted = invocation.Operand(0);
        if (wanted is null) return CliResult.Usage($"usage: rhi ui {label} <{string.Join("|", allowed)}>");
        var match = allowed.FirstOrDefault(a => a.Equals(wanted, StringComparison.OrdinalIgnoreCase));
        return match is null
            ? CliResult.Usage($"{label} must be one of: {string.Join(", ", allowed)}")
            : Write(key, match, label);
    }

    private static CliResult SetNumber(CliInvocation invocation, string key, string label, int min, int max)
    {
        var raw = invocation.Operand(0);
        if (!int.TryParse(raw, out var value) || value < min || value > max)
            return CliResult.Usage($"usage: rhi ui {(key == "UiScale" ? "scale" : "list-scale")} <{min}-{max}>");
        return Write(key, value.ToString(), label);
    }

    private static CliResult SetText(CliInvocation invocation, string key, string label)
    {
        var text = invocation.OperandText;
        if (string.IsNullOrWhiteSpace(text)) return CliResult.Usage($"usage: rhi ui {(key == "DetailSelectedTab" ? "tab" : "settings")} <name>");
        return Write(key, text, label);
    }

    private static CliResult Write(string key, string value, string label, string? display = null)
    {
        var shown = display ?? value;
        var settings = SettingsFile.Load();
        var previous = settings.TryGetValue(key, out var old) ? old : null;

        if (previous is not null && string.Equals(previous, value, StringComparison.Ordinal))
            return CliResult.Ok($"{label} is already '{shown}' - nothing to change", new { key, value, previous, changed = false });

        if (!SettingsFile.Update(store => store[key] = value, "cli:ui"))
            return CliResult.Fail($"could not write {SettingsFile.FilePath} - file locked or unreadable");

        var note = IsAnotherRhiRunning()
            ? " RHI is running: close it first or it will overwrite this when it saves."
            : " Takes effect at the next launch of RHI.";

        return CliResult.Ok($"{label} set to '{shown}' (was '{previous ?? "<unset>"}').{note}",
            new { key, value, previous, changed = true, appliesNextLaunch = true });
    }

    private static string Read(Dictionary<string, string> settings, string key) =>
        settings.TryGetValue(key, out var value) && !string.IsNullOrWhiteSpace(value) ? value : "(default)";

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
