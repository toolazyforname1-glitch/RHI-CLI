using Microsoft.Win32;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// Windows integration the Settings page exposes: the HKCU Run key, the nxm:// protocol handler and
/// disconnecting the Nexus account. Read-only subcommands work without confirmation; the ones that
/// touch the registry or credentials require --yes.
/// </summary>
internal static class CliSystem
{
    private const string RunKey = @"Software\Microsoft\Windows\CurrentVersion\Run";
    private const string RunValueName = "RHI";

    internal static readonly CliCommand[] All = [Command()];

    private static CliCommand Command() => new()
    {
        Name = "system",
        Group = "system",
        Summary = "Start with Windows, the nxm:// handler, and Nexus disconnect.",
        Usage = "rhi system startup show|on|off | rhi system nxm status|register|unregister | rhi system nexus disconnect",
        Examples = ["rhi system startup show", "rhi system nxm status", "rhi system startup on --yes"],
        Run = Run,
    };

    private static CliResult Run(CliInvocation invocation)
    {
        var args = invocation.Args;
        if (args.Length == 0)
            return CliResult.Usage("usage: rhi system <startup|nxm|nexus> <action> [--yes]");

        var area = args[0].ToLowerInvariant();
        var action = args.Length > 1 ? args[1].ToLowerInvariant() : "";

        return area switch
        {
            "startup" => Startup(invocation, action),
            "nxm" => Nxm(invocation, action),
            "nexus" => Nexus(invocation, action),
            _ => CliResult.Usage("usage: rhi system <startup|nxm|nexus> <action> [--yes]"),
        };
    }

    private static CliResult Startup(CliInvocation invocation, string action)
    {
        using var key = Registry.CurrentUser.OpenSubKey(RunKey, writable: action is "on" or "off");
        if (key is null) return CliResult.Fail($"cannot open HKCU\\{RunKey}");

        var current = key.GetValue(RunValueName) as string;

        if (action is "" or "show")
        {
            return CliResult.Ok(
                current is null ? "RHI does not start with Windows" : "RHI starts with Windows: " + current,
                new { enabled = current is not null, command = current });
        }

        if (action is not ("on" or "off"))
            return CliResult.Usage("usage: rhi system startup <show|on|off>");

        if (!invocation.DryRun && !invocation.Confirmed)
            return CliResult.Usage($"refusing to change the Run key without --yes: rhi system startup {action} --yes");

        if (action == "off")
        {
            if (invocation.DryRun)
                return CliResult.Ok($"would remove the Run value '{RunValueName}'", new { enabled = false, dryRun = true });

            key.DeleteValue(RunValueName, throwOnMissingValue: false);
        }
        else
        {
            var command = $"\"{Environment.ProcessPath}\" --minimized";
            if (invocation.DryRun)
                return CliResult.Ok($"would set the Run value to {command}", new { enabled = true, command, dryRun = true });

            key.SetValue(RunValueName, command);
        }

        SettingsFile.Update(store => store["StartWithWindows"] = action == "on" ? "true" : "false", "cli:system startup");

        return CliResult.Ok($"start with Windows: {action}", new { enabled = action == "on" });
    }

    private static CliResult Nxm(CliInvocation invocation, string action)
    {
        if (action is "" or "status")
        {
            var registered = NxmProtocolHandler.IsRegistered();
            return CliResult.Ok(registered ? "RHI is the active nxm:// handler" : "nxm:// is not registered",
                new { registered });
        }

        if (action is not ("register" or "unregister"))
            return CliResult.Usage("usage: rhi system nxm <status|register|unregister>");

        if (!invocation.DryRun && !invocation.Confirmed)
            return CliResult.Usage($"refusing to change the nxm:// handler without --yes: rhi system nxm {action} --yes");

        if (invocation.DryRun)
            return CliResult.Ok($"would {action} the nxm:// handler", new { registered = action == "register", dryRun = true });

        if (action == "register") NxmProtocolHandler.RegisterProtocolHandler();
        else NxmProtocolHandler.UnregisterProtocolHandler();

        return CliResult.Ok($"nxm:// handler {action}ed", new { registered = NxmProtocolHandler.IsRegistered() });
    }

    private static CliResult Nexus(CliInvocation invocation, string action)
    {
        if (action is not ("" or "disconnect"))
            return CliResult.Usage("usage: rhi system nexus disconnect --yes");

        if (!invocation.DryRun && !invocation.Confirmed)
            return CliResult.Usage("refusing to clear the Nexus credentials without --yes: rhi system nexus disconnect --yes");

        if (invocation.DryRun)
            return CliResult.Ok("would clear the Nexus API key, username and premium flag", new { disconnected = true, dryRun = true });

        SettingsFile.Update(store =>
        {
            store["NexusApiKey"] = "";
            store["NexusUsername"] = "";
            store["NexusIsPremium"] = "false";
        }, "cli:system nexus disconnect");

        return CliResult.Ok("disconnected from Nexus Mods", new { disconnected = true });
    }
}
