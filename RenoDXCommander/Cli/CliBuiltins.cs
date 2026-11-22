using System.Runtime.InteropServices;
using System.Text;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>The builtin commands that need no services: help, commands, version, doctor.</summary>
internal static class CliBuiltins
{
    internal sealed record DoctorCheck(string Name, bool Ok, string Detail);

    internal static readonly CliCommand[] All = [HelpCommand(), ListCommand(), VersionCommand(), DoctorCommand()];

    private static CliCommand HelpCommand() => new()
    {
        Name = "help",
        Aliases = ["?"],
        Group = "core",
        Summary = "List every command, or explain one command.",
        Usage = "rhi help [command]",
        Examples = ["rhi help", "rhi help doctor"],
        Run = Help,
    };

    private static CliCommand ListCommand() => new()
    {
        Name = "commands",
        Group = "core",
        Summary = "Machine-readable list of every command (use with --json).",
        Usage = "rhi commands [--json]",
        Examples = ["rhi commands --json"],
        Run = _ =>
        {
            var text = new StringBuilder();
            var nameWidth = CliRegistry.All.Max(c => c.Name.Length) + 2;
            foreach (var command in CliRegistry.All.OrderBy(c => c.Group).ThenBy(c => c.Name, StringComparer.OrdinalIgnoreCase))
                text.AppendLine(command.Group.PadRight(14) + command.Name.PadRight(nameWidth) + command.Summary);

            return CliResult.Ok(
                text.ToString().TrimEnd() + Environment.NewLine + Environment.NewLine + CliRegistry.All.Count + " commands",
                new { commands = CliRegistry.All.OrderBy(c => c.Group).ThenBy(c => c.Name).Select(Describe).ToArray() });
        },
    };

    private static CliCommand VersionCommand() => new()
    {
        Name = "version",
        Aliases = ["ver"],
        Group = "core",
        Summary = "Show the RHI version, executable path and platform.",
        Usage = "rhi version [--json]",
        Run = _ =>
        {
            var exe = Environment.ProcessPath ?? "unknown";
            var version = CrashReporter.AppVersion;
            var os = Environment.OSVersion.ToString();
            var arch = RuntimeInformation.ProcessArchitecture.ToString();
            return CliResult.Ok($"RHI {version}\n{exe}\n{os} ({arch})", new { version, exe, os, arch });
        },
    };

    private static CliCommand DoctorCommand() => new()
    {
        Name = "doctor",
        Group = "core",
        Summary = "Check the install: exe, settings, data folder, 7-Zip, elevation.",
        Usage = "rhi doctor [--json]",
        Examples = ["rhi doctor", "rhi doctor --json"],
        Run = Doctor,
    };

    private static CliResult Help(CliInvocation invocation)
    {
        var wanted = invocation.Args.Length == 0 ? "" : string.Join(' ', invocation.Args);
        if (!string.IsNullOrWhiteSpace(wanted))
        {
            var single = CliRegistry.Find(wanted);
            if (single is null) return CliResult.Usage($"Unknown command '{wanted}'. Run: rhi commands");
            return CliResult.Ok(RenderCommand(single), new { command = Describe(single) });
        }

        var text = new StringBuilder();
        text.AppendLine("rhi - terminal control for RHI");
        text.AppendLine();
        text.AppendLine("usage: rhi <command> [args] [--json] [--yes] [--dry-run]");
        text.AppendLine();
        var nameWidth = CliRegistry.All.Max(c => c.Name.Length) + 2;
        foreach (var group in CliRegistry.All.GroupBy(c => c.Group).OrderBy(g => g.Key, StringComparer.OrdinalIgnoreCase))
        {
            text.AppendLine(group.Key + ":");
            foreach (var command in group.OrderBy(c => c.Name, StringComparer.OrdinalIgnoreCase))
                text.AppendLine("  " + command.Name.PadRight(nameWidth) + command.Summary);
            text.AppendLine();
        }
        text.AppendLine("Every command takes --json. Commands that change something take --dry-run and need --yes.");

        return CliResult.Ok(
            text.ToString().TrimEnd(),
            new
            {
                groups = CliRegistry.All
                    .GroupBy(c => c.Group)
                    .OrderBy(g => g.Key, StringComparer.OrdinalIgnoreCase)
                    .Select(g => new { group = g.Key, commands = g.OrderBy(c => c.Name).Select(Describe).ToArray() })
                    .ToArray(),
            });
    }

    private static CliResult Doctor(CliInvocation invocation)
    {
        var checks = new List<DoctorCheck>();
        void Add(string name, bool ok, string detail) => checks.Add(new DoctorCheck(name, ok, detail));

        var exe = Environment.ProcessPath ?? "";
        Add("exe", exe.Length > 0 && File.Exists(exe), exe);
        Add("version", true, CrashReporter.AppVersion);
        Add("os", true, $"{Environment.OSVersion} ({RuntimeInformation.ProcessArchitecture})");

        var elevated = VulkanLayerService.IsRunningAsAdmin();
        Add("elevation", true, elevated ? "running as administrator" : "not elevated - DLSS/registry commands will need it");

        var settingsPath = SettingsFile.FilePath;
        var hasSettings = File.Exists(settingsPath);
        var keyCount = 0;
        var loadStatus = "missing";
        if (hasSettings)
        {
            var settings = SettingsFile.Load(out var status);
            keyCount = settings.Count;
            loadStatus = status.ToString();
        }
        var settingsOk = hasSettings && loadStatus is "Ok" or "Recovered";
        Add("settings", settingsOk, $"{settingsPath} ({keyCount} keys, {loadStatus})");

        var dataRoot = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RHI");
        var subfolders = 0;
        try { if (Directory.Exists(dataRoot)) subfolders = Directory.GetDirectories(dataRoot).Length; }
        catch (IOException) { }
        catch (UnauthorizedAccessException) { }
        Add("data folder", Directory.Exists(dataRoot), $"{dataRoot} ({subfolders} subfolders)");

        bool sevenZipOk;
        string sevenZipDetail;
        try
        {
            var found = AppPaths.Find7Zip();
            sevenZipOk = !string.IsNullOrWhiteSpace(found);
            sevenZipDetail = found ?? "not found (needed for archive extraction)";
        }
        catch (IOException ex)
        {
            sevenZipOk = false;
            sevenZipDetail = ex.Message;
        }
        Add("7-zip", sevenZipOk, sevenZipDetail);

        var failed = checks.Count(c => !c.Ok);
        var text = new StringBuilder();
        foreach (var check in checks)
            text.AppendLine((check.Ok ? "  ok    " : "  FAIL  ") + check.Name.PadRight(12) + check.Detail);
        text.AppendLine();
        text.Append(failed == 0 ? "doctor: all checks passed" : $"doctor: {failed} check(s) failed");

        var data = new { passed = checks.Count - failed, failed, checks = checks.Select(c => new { c.Name, c.Ok, c.Detail }).ToArray() };
        return failed == 0 ? CliResult.Ok(text.ToString(), data) : CliResult.Fail(text.ToString(), data);
    }

    private static string RenderCommand(CliCommand command)
    {
        var text = new StringBuilder();
        text.AppendLine(command.Name + " - " + command.Summary);
        text.AppendLine();
        text.AppendLine("usage: " + (string.IsNullOrWhiteSpace(command.Usage) ? "rhi " + command.Name : command.Usage));
        if (command.Aliases.Length > 0) text.AppendLine("aliases: " + string.Join(", ", command.Aliases));
        if (command.Examples.Length > 0)
        {
            text.AppendLine();
            text.AppendLine("examples:");
            foreach (var example in command.Examples) text.AppendLine("  " + example);
        }
        return text.ToString().TrimEnd();
    }

    private static object Describe(CliCommand command) => new
    {
        name = command.Name,
        aliases = command.Aliases,
        group = command.Group,
        summary = command.Summary,
        usage = string.IsNullOrWhiteSpace(command.Usage) ? "rhi " + command.Name : command.Usage,
        examples = command.Examples,
        destructive = command.Destructive,
        requiresGui = command.RequiresGui,
    };
}
