using System.Diagnostics;
using System.Text;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// Reads and writes %LocalAppData%\RHI\settings.json - the same store the GUI uses, through the
/// same SettingsFile lock, so a terminal change and a GUI save cannot corrupt each other.
/// </summary>
internal static class CliSettings
{
    internal static readonly CliCommand[] All = [ListCommand(), GetCommand(), SetCommand()];

    private static CliCommand ListCommand() => new()
    {
        Name = "setting list",
        Group = "settings",
        Summary = "Show every persisted setting and its value.",
        Usage = "rhi setting list [--json]",
        Examples = ["rhi setting list", "rhi setting list --json"],
        Run = _ =>
        {
            var settings = SettingsFile.Load();
            var ordered = settings.OrderBy(kv => kv.Key, StringComparer.OrdinalIgnoreCase).ToList();
            var text = new StringBuilder();
            foreach (var pair in ordered) text.AppendLine(pair.Key.PadRight(38) + pair.Value);
            text.AppendLine();
            text.Append(ordered.Count + " settings");

            return CliResult.Ok(text.ToString(), new { settingsFile = SettingsFile.FilePath, count = ordered.Count, settings });
        },
    };

    private static CliCommand GetCommand() => new()
    {
        Name = "setting get",
        Group = "settings",
        Summary = "Show one setting's value.",
        Usage = "rhi setting get <Key> [--json]",
        Examples = ["rhi setting get OverlayHotkey"],
        Run = invocation =>
        {
            var key = invocation.Operand(0);
            if (string.IsNullOrWhiteSpace(key)) return CliResult.Usage("usage: rhi setting get <Key>");

            var settings = SettingsFile.Load();
            if (!settings.TryGetValue(key, out var value))
                return CliResult.Fail($"no setting '{key}' - run: rhi setting list");

            return CliResult.Ok(value, new { key, value, settingsFile = SettingsFile.FilePath });
        },
    };

    private static CliCommand SetCommand() => new()
    {
        Name = "setting set",
        Group = "settings",
        Summary = "Change a persisted setting. Takes effect immediately; a running RHI may overwrite it when it saves.",
        Usage = "rhi setting set <Key> <Value> [--dry-run] [--json]",
        Examples = ["rhi setting set OverlayHotkey 36,0,0,0", "rhi setting set OverlayHotkey 35,0,0,0 --dry-run"],
        Run = Set,
    };

    private static CliResult Set(CliInvocation invocation)
    {
        var args = invocation.Args;
        if (args.Length < 2) return CliResult.Usage("usage: rhi setting set <Key> <Value> [--dry-run]");

        var key = args[0];
        var value = string.Join(' ', args[1..]);
        var settings = SettingsFile.Load();
        var existed = settings.TryGetValue(key, out var previous);

        if (existed && string.Equals(previous, value, StringComparison.Ordinal))
            return CliResult.Ok($"'{key}' is already '{value}' - nothing to change", new { key, value, previous, changed = false });

        if (invocation.DryRun)
            return CliResult.Ok($"would set '{key}' to '{value}' (was '{previous ?? "<unset>"}')",
                new { key, value, previous, changed = true, dryRun = true });

        if (!SettingsFile.Update(store => store[key] = value, "cli:setting set"))
            return CliResult.Fail($"could not write {SettingsFile.FilePath} - file locked or unreadable");

        var guiRunning = IsAnotherRhiRunning();
        var note = guiRunning ? " (RHI is running - it may overwrite this when it saves)" : "";
        if (!existed) note += " (new key - RHI only reads keys it knows)";
        return CliResult.Ok($"'{key}' = '{value}' (was '{previous ?? "<unset>"}'){note}",
            new { key, value, previous, changed = true, guiRunning });
    }

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
