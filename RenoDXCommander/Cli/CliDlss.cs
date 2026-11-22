using System.Text;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// DLSS/Streamline preset and render-scale control through DlssPresetService - the same calls the
/// NVIDIA override section makes, including its preset tables.
/// </summary>
internal static class CliDlss
{
    internal static readonly CliCommand[] All = [Presets(), Preset(), Scale(), Driver()];

    private static CliCommand Presets() => new()
    {
        Name = "dlss presets",
        Group = "dlss",
        Summary = "List the preset tables (SR, RR, FG, NR) the app offers.",
        Usage = "rhi dlss presets [--json]",
        Examples = ["rhi dlss presets"],
        Run = _ =>
        {
            var text = new StringBuilder();
            foreach (var family in new[] { "sr", "rr", "fg", "nr" })
            {
                text.AppendLine(family.ToUpperInvariant() + ":");
                foreach (var option in PresetsFor(family)) text.AppendLine("  " + option.Name.PadRight(26) + "0x" + option.Value.ToString("X8"));
            }
            return CliResult.Ok(text.ToString().TrimEnd(), new
            {
                sr = Describe("sr"), rr = Describe("rr"), fg = Describe("fg"), nr = Describe("nr"),
            });
        },
    };

    private static CliCommand Preset() => new()
    {
        Name = "dlss preset",
        Group = "dlss",
        Summary = "Set a DLSS preset for a game. Accepts a letter (J, D), a name fragment, or a number.",
        Usage = "rhi dlss preset <sr|rr|fg|nr> <game> <value> [--dry-run] [--yes]",
        Examples = ["rhi dlss preset sr cyberpunk J --yes", "rhi dlss preset rr alan-wake-2 D --yes", "rhi dlss preset sr cyberpunk 0 --dry-run"],
        Destructive = true,
        Run = invocation => Apply(invocation, kind: "preset"),
    };

    private static CliCommand Scale() => new()
    {
        Name = "dlss scale",
        Group = "dlss",
        Summary = "Set the DLSS render scale percentage (SR or RR).",
        Usage = "rhi dlss scale <sr|rr> <game> <percent> [--dry-run] [--yes]",
        Examples = ["rhi dlss scale sr cyberpunk 67 --yes"],
        Destructive = true,
        Run = invocation => Apply(invocation, kind: "scale"),
    };

    private static CliCommand Driver() => new()
    {
        Name = "dlss driver",
        Group = "dlss",
        Summary = "Turn the driver-level preset override on or off (SR, RR or FG).",
        Usage = "rhi dlss driver <sr|rr|fg> <game> <on|off> [--dry-run] [--yes]",
        Examples = ["rhi dlss driver sr cyberpunk on --yes"],
        Destructive = true,
        Run = invocation => Apply(invocation, kind: "driver"),
    };

    private static CliResult Apply(CliInvocation invocation, string kind)
    {
        var args = invocation.Args;
        if (args.Length < 3)
            return CliResult.Usage($"usage: rhi dlss {kind} <family> <game> <value> [--dry-run]");

        var family = args[0].ToLowerInvariant();
        if (PresetsFor(family).Length == 0) return CliResult.Usage($"family must be one of: sr, rr, fg, nr (got '{args[0]}')");

        var game = CliGames.ResolveOne(args[1], out var gameFailure);
        if (game is null) return gameFailure!;

        var raw = string.Join(' ', args[2..]);
        var service = CliServices.Get<DlssPresetService>();

        if (kind == "preset")
        {
            if (!TryResolveValue(family, raw, out var value, out var label))
                return CliResult.Usage($"'{raw}' is not a {family.ToUpperInvariant()} preset - run: rhi dlss presets");

            if (invocation.DryRun)
                return CliResult.Ok($"would set the {family.ToUpperInvariant()} preset to '{label}' for '{game.Name}'",
                    new { game = game.Name, family, value, label, dryRun = true });

            var ok = ApplyPreset(service, family, game.Name, game.InstallPath, value);
            return ok
                ? CliResult.Ok($"{family.ToUpperInvariant()} preset for '{game.Name}' set to '{label}'", new { game = game.Name, family, value, label, changed = true })
                : CliResult.Fail($"could not set the {family.ToUpperInvariant()} preset for '{game.Name}' - see the log");
        }

        if (kind == "scale")
        {
            if (!uint.TryParse(raw, out var percent) || percent is < 25 or > 100)
                return CliResult.Usage("percent must be a number between 25 and 100");

            if (family is not ("sr" or "rr")) return CliResult.Usage("render scale applies to sr and rr only");
            if (invocation.DryRun) return CliResult.Ok($"would set the {family.ToUpperInvariant()} render scale to {percent}% for '{game.Name}'", new { game = game.Name, family, percent, dryRun = true });

            var ok = family == "sr"
                ? service.SetSrRenderScale(game.Name, game.InstallPath, percent)
                : service.SetRrRenderScale(game.Name, game.InstallPath, percent);
            return ok
                ? CliResult.Ok($"{family.ToUpperInvariant()} render scale for '{game.Name}' set to {percent}%", new { game = game.Name, family, percent, changed = true })
                : CliResult.Fail($"could not set the render scale for '{game.Name}' - see the log");
        }

        var enable = args[2].Equals("on", StringComparison.OrdinalIgnoreCase) || args[2].Equals("true", StringComparison.OrdinalIgnoreCase);
        if (!enable && !args[2].Equals("off", StringComparison.OrdinalIgnoreCase) && !args[2].Equals("false", StringComparison.OrdinalIgnoreCase))
            return CliResult.Usage("value must be on or off");
        if (family == "nr") return CliResult.Usage("the driver override applies to sr, rr and fg only");

        if (invocation.DryRun)
            return CliResult.Ok($"would turn the {family.ToUpperInvariant()} driver override {(enable ? "on" : "off")} for '{game.Name}'",
                new { game = game.Name, family, enabled = enable, dryRun = true });

        var applied = family switch
        {
            "sr" => service.SetSrDriverOverride(game.Name, game.InstallPath, enable),
            "rr" => service.SetRrDriverOverride(game.Name, game.InstallPath, enable),
            _ => service.SetFgDriverOverride(game.Name, game.InstallPath, enable),
        };

        return applied
            ? CliResult.Ok($"{family.ToUpperInvariant()} driver override for '{game.Name}' is now {(enable ? "on" : "off")}", new { game = game.Name, family, enabled = enable, changed = true })
            : CliResult.Fail($"could not change the driver override for '{game.Name}' - see the log");
    }

    private static bool ApplyPreset(DlssPresetService service, string family, string game, string path, uint value) => family switch
    {
        "sr" => service.SetSrPreset(game, path, value),
        "rr" => service.SetRrPreset(game, path, value),
        "fg" => service.SetFgPreset(game, path, value),
        _ => service.SetNrPreset(game, path, value),
    };

    private static (string Name, uint Value)[] PresetsFor(string family) => family switch
    {
        "sr" => DlssPresetService.SrPresets,
        "rr" => DlssPresetService.RrPresets,
        "fg" => DlssPresetService.FgPresets,
        "nr" => DlssPresetService.NrPresets,
        _ => [],
    };

    private static object Describe(string family) =>
        PresetsFor(family).Select(o => new { o.Name, o.Value }).ToArray();

    private static bool TryResolveValue(string family, string text, out uint value, out string label)
    {
        value = 0;
        label = "";

        var options = PresetsFor(family);
        if (options.Length == 0) return false;

        if (uint.TryParse(text, out var number))
        {
            var named = options.FirstOrDefault(o => o.Value == number);
            value = number;
            label = named.Name ?? "0x" + number.ToString("X8");
            return true;
        }

        var match = options.FirstOrDefault(o => o.Name.StartsWith(text, StringComparison.OrdinalIgnoreCase));
        if (match.Name is null)
            match = options.FirstOrDefault(o => o.Name.Contains(text, StringComparison.OrdinalIgnoreCase));
        if (match.Name is null) return false;

        value = match.Value;
        label = match.Name;
        return true;
    }
}
