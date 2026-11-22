using System.Text;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// The ReShade addon catalogue: what RHI can download, what is already local, and a way to fetch one.
/// Deployment into a game stays with `install reshade`, which applies the global addon selection.
/// </summary>
internal static class CliAddons
{
    internal static readonly CliCommand[] All = [Command()];

    private static CliCommand Command() => new()
    {
        Name = "reshade addons",
        Group = "config",
        Summary = "List the addon catalogue, or download one addon.",
        Usage = "rhi reshade addons [list] | rhi reshade addons download <id|name> [--dry-run] [--yes]",
        Examples = ["rhi reshade addons", "rhi reshade addons download luma --yes"],
        RunAsync = Run,
    };

    private static async Task<CliResult> Run(CliInvocation invocation)
    {
        var service = new AddonPackService(CliServices.Get<HttpClient>());
        var action = (invocation.Operand(0) ?? "list").ToLowerInvariant();

        if (action is "list" or "")
        {
            await service.EnsureLatestAsync();

            var packs = service.AvailablePacks;
            if (packs.Count == 0) return CliResult.Fail("no addons available - check the connection");

            var downloaded = new HashSet<string>(service.DownloadedAddonNames, StringComparer.OrdinalIgnoreCase);
            var text = new StringBuilder();
            var rows = new List<object>();

            foreach (var pack in packs.OrderBy(p => p.PackageName, StringComparer.OrdinalIgnoreCase))
            {
                var isLocal = downloaded.Contains(pack.PackageName) || downloaded.Contains(pack.SectionId);
                text.AppendLine(pack.SectionId.PadRight(28) + (isLocal ? "downloaded" : "-").PadRight(12) + pack.PackageName);
                rows.Add(new { id = pack.SectionId, name = pack.PackageName, downloaded = isLocal });
            }

            return CliResult.Ok(text.ToString().TrimEnd(), new { count = packs.Count, addons = rows });
        }

        if (action == "deploy")
        {
            var game = CliGames.ResolveOne(invocation.Operand(1), out var gameFailure);
            if (game is null) return gameFailure!;

            bool is32Bit;
            try { is32Bit = CliServices.Get<IPeHeaderService>().DetectGameArchitecture(game.InstallPath) == MachineType.I386; }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or BadImageFormatException or ArgumentException) { is32Bit = false; }

            var selection = invocation.Flag("select")?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var useGlobalSet = selection is null || selection.Length == 0;

            if (invocation.DryRun)
                return CliResult.Ok($"would deploy {(useGlobalSet ? "the global addon set" : selection!.Length + " selected addon(s)")} to '{game.Name}' ({(is32Bit ? "32" : "64")}-bit)",
                    new { game = game.Name, is32Bit, useGlobalSet, selection, dryRun = true });

            if (!invocation.Confirmed)
                return CliResult.Usage($"refusing to deploy addons without --yes: rhi reshade addons deploy '{game.Name}' --yes");

            service.DeployAddonsForGame(game.Name, game.InstallPath, is32Bit, useGlobalSet, selection?.ToList());
            return CliResult.Ok($"addons deployed to '{game.Name}'", new { game = game.Name, useGlobalSet, deployed = true });
        }

        if (action != "download")
            return CliResult.Usage("usage: rhi reshade addons [list|download <id|name>|deploy <game> [--select id,id]] [--dry-run] [--yes]");

        var wanted = invocation.Operand(1);
        if (string.IsNullOrWhiteSpace(wanted))
            return CliResult.Usage("usage: rhi reshade addons download <id|name> [--dry-run] [--yes]");

        await service.EnsureLatestAsync();

        var match = service.AvailablePacks.FirstOrDefault(p => string.Equals(p.SectionId, wanted, StringComparison.OrdinalIgnoreCase))
                    ?? service.AvailablePacks.FirstOrDefault(p => p.PackageName.Contains(wanted, StringComparison.OrdinalIgnoreCase));

        if (match is null) return CliResult.Fail($"no addon matching '{wanted}' - run: rhi reshade addons");

        if (invocation.DryRun)
            return CliResult.Ok($"would download '{match.PackageName}'", new { id = match.SectionId, name = match.PackageName, dryRun = true });

        if (!invocation.Confirmed)
            return CliResult.Usage($"refusing to download without --yes: rhi reshade addons download {match.SectionId} --yes");

        await service.DownloadAddonAsync(match);
        return CliResult.Ok($"downloaded '{match.PackageName}'", new { id = match.SectionId, name = match.PackageName, downloaded = true });
    }
}
