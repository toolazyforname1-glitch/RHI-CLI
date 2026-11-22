using System.Text;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// DLDSR states. Reading is free; applying or changing smoothness writes driver registry values and
/// needs SYSTEM level access through RHI's scheduled task, so those require --yes (and the screen
/// blanks for a few seconds when a state is applied).
/// </summary>
internal static class CliDldsr
{
    internal static readonly CliCommand[] All = [Command()];

    private static CliCommand Command() => new()
    {
        Name = "dldsr",
        Group = "display",
        Summary = "Show DLDSR state, list captures, capture/apply/delete a state, or set smoothness.",
        Usage = "rhi dldsr [show|captures|apply <label> [--smoothness n]|capture <label>|delete <label>|smoothness <n>] [--dry-run] [--yes]",
        Examples = ["rhi dldsr show", "rhi dldsr captures", "rhi dldsr apply 4K --smoothness 50 --yes"],
        RunAsync = Run,
    };

    private static async Task<CliResult> Run(CliInvocation invocation)
    {
        var service = CliServices.Get<IDldsrService>();
        var action = (invocation.Operand(0) ?? "show").ToLowerInvariant();

        if (action is "show" or "captures")
        {
            var states = service.GetCurrentState();
            var captures = service.LoadCaptures();
            var text = new StringBuilder();

            if (action == "show")
            {
                text.AppendLine("driver     " + (service.GetCurrentDriverVersion() ?? "unknown"));
                foreach (var state in states)
                {
                    text.AppendLine(state.MonitorId.PadRight(30) +
                        "factors=" + (state.EnabledFactors ?? "-") +
                        "  smoothness=" + state.Smoothness +
                        "  matches=" + (state.MatchesCapture ?? "-"));
                }
                text.AppendLine();
            }

            text.AppendLine(captures.Count + " capture(s) in " + service.CapturesFilePath);
            foreach (var capture in captures)
                text.AppendLine("  " + capture.Label.PadRight(26) + capture.Time.ToString("yyyy-MM-dd HH:mm"));

            return CliResult.Ok(text.ToString().TrimEnd(), new
            {
                driver = service.GetCurrentDriverVersion(),
                states,
                captures,
                capturesFile = service.CapturesFilePath,
            });
        }

        var label = invocation.Operand(1) ?? "";

        switch (action)
        {
            case "capture":
            {
                if (string.IsNullOrWhiteSpace(label))
                    return CliResult.Usage("usage: rhi dldsr capture <label> [--yes]");

                if (invocation.DryRun)
                    return CliResult.Ok($"would capture the current state as '{label}'", new { label, dryRun = true });

                if (!invocation.Confirmed)
                    return CliResult.Usage($"refusing to write a capture without --yes: rhi dldsr capture {label} --yes");

                service.CaptureCurrentState(label);
                return CliResult.Ok($"captured the current state as '{label}'", new { label, captured = true });
            }

            case "delete":
            {
                if (string.IsNullOrWhiteSpace(label))
                    return CliResult.Usage("usage: rhi dldsr delete <label> [--yes]");

                if (invocation.DryRun)
                    return CliResult.Ok($"would delete the capture '{label}'", new { label, dryRun = true });

                if (!invocation.Confirmed)
                    return CliResult.Usage($"refusing to delete a capture without --yes: rhi dldsr delete {label} --yes");

                service.DeleteCapture(label);
                return CliResult.Ok($"deleted the capture '{label}'", new { label, deleted = true });
            }

            case "apply":
            {
                if (string.IsNullOrWhiteSpace(label))
                    return CliResult.Usage("usage: rhi dldsr apply <label> [--smoothness 0-100] [--yes]");

                int? smoothness = null;
                if (invocation.Flag("smoothness") is { } raw)
                {
                    if (!int.TryParse(raw, out var parsed) || parsed is < 0 or > 100)
                        return CliResult.Usage("--smoothness must be 0-100");
                    smoothness = parsed;
                }

                if (invocation.DryRun)
                    return CliResult.Ok($"would apply DLDSR state '{label}'" + (smoothness is null ? "" : $" with smoothness {smoothness}"),
                        new { label, smoothness, dryRun = true });

                if (!invocation.Confirmed)
                    return CliResult.Usage($"refusing to apply a DLDSR state without --yes (the screen blanks for a few seconds): rhi dldsr apply {label} --yes");

                var applied = await service.ApplyStateAsync(label, progress: null, smoothnessOverride: smoothness);
                return applied
                    ? CliResult.Ok($"applied DLDSR state '{label}'", new { label, smoothness, applied = true })
                    : CliResult.Fail($"the driver refused the DLDSR state '{label}' - it may need RHI's SYSTEM task (run RHI once as admin)");
            }

            case "smoothness":
            {
                if (!int.TryParse(label, out var value) || value is < 0 or > 100)
                    return CliResult.Usage("usage: rhi dldsr smoothness <0-100> [--yes]");

                if (invocation.DryRun)
                    return CliResult.Ok($"would set DLDSR smoothness to {value}", new { smoothness = value, dryRun = true });

                if (!invocation.Confirmed)
                    return CliResult.Usage($"refusing to change smoothness without --yes: rhi dldsr smoothness {value} --yes");

                var ok = await service.SetSmoothnessAsync(value, progress: null);
                return ok
                    ? CliResult.Ok($"DLDSR smoothness set to {value}", new { smoothness = value, changed = true })
                    : CliResult.Fail("the driver refused the smoothness change - it may need RHI's SYSTEM task");
            }

            default:
                return CliResult.Usage("usage: rhi dldsr [show|captures|apply <label>|capture <label>|delete <label>|smoothness <n>]");
        }
    }
}
