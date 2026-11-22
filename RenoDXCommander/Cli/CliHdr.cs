using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>Windows Auto HDR, per game exe - the same registry writes the HDR toggle makes.</summary>
internal static class CliHdr
{
    internal static readonly CliCommand[] All = [AutoHdr()];

    private static CliCommand AutoHdr() => new()
    {
        Name = "hdr auto",
        Group = "hdr",
        Summary = "Read or change Windows Auto HDR for a game.",
        Usage = "rhi hdr auto <game> show|on|off|strength [--strength 0-100] [--dry-run] [--yes]",
        Examples = ["rhi hdr auto cyberpunk show", "rhi hdr auto cyberpunk on --strength 70 --yes", "rhi hdr auto cyberpunk off --dry-run"],
        Destructive = true,
        Run = Run,
    };

    private static CliResult Run(CliInvocation invocation)
    {
        var args = invocation.Args;
        if (args.Length < 2) return CliResult.Usage("usage: rhi hdr auto <game> show|on|off|strength [--strength 0-100]");

        var game = CliGames.ResolveOne(args[0], out var failure);
        if (game is null) return failure!;

        var service = CliServices.Get<IAutoHdrService>();
        var exe = service.ResolveExePath(game.Name, game.InstallPath, null, null);
        if (string.IsNullOrWhiteSpace(exe)) return CliResult.Fail($"could not find a game exe in {game.InstallPath}");

        var action = args[1].ToLowerInvariant();
        var strengthFlag = invocation.Flag("strength");
        int? strength = null;
        if (strengthFlag is not null)
        {
            if (!int.TryParse(strengthFlag, out var parsed) || parsed is < 0 or > 100)
                return CliResult.Usage("--strength must be a number between 0 and 100");
            strength = parsed;
        }

        if (action == "show")
        {
            var enabled = service.IsEnabled(exe);
            var current = service.GetStrength(exe);
            return CliResult.Ok($"Auto HDR is {(enabled ? "on" : "off")} for '{game.Name}' (strength {(current < 0 ? "unset" : current.ToString())})\n{exe}",
                new { game = game.Name, exe, enabled, strength = current });
        }

        if (action is not ("on" or "off" or "strength"))
            return CliResult.Usage("action must be show, on, off or strength");

        if (action == "strength" && strength is null)
            return CliResult.Usage("strength needs --strength <0-100>");

        if (invocation.DryRun)
            return CliResult.Ok(action switch
                {
                    "on" => $"would turn Auto HDR on for '{game.Name}' (strength {strength ?? 50})",
                    "off" => $"would turn Auto HDR off for '{game.Name}'",
                    _ => $"would set the Auto HDR strength for '{game.Name}' to {strength}",
                },
                new { game = game.Name, exe, action, strength, dryRun = true });

        var ok = action switch
        {
            "on" => service.Enable(exe, strength ?? 50),
            "off" => service.Disable(exe),
            _ => service.SetStrength(exe, strength!.Value),
        };

        return ok
            ? CliResult.Ok($"Auto HDR {action} applied for '{game.Name}'", new { game = game.Name, exe, action, strength, changed = true })
            : CliResult.Fail($"could not change Auto HDR for '{game.Name}' - the registry write was refused");
    }
}
