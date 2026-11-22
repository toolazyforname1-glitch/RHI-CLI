using RenoDXCommander.Models;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// RenoDX addon install/uninstall. The mod comes from the RenoDX database, matched by name the
/// same way the card lookup does (exact, then with trademark marks stripped).
/// </summary>
internal static class CliMods
{
    internal static readonly CliCommand[] All = [InstallRenoDx(), UninstallRenoDx()];

    private static CliCommand InstallRenoDx() => new()
    {
        Name = "install renodx",
        Group = "install",
        Summary = "Install the RenoDX addon for a game from the RenoDX database.",
        Usage = "rhi install renodx <game> [--dry-run]",
        Examples = ["rhi install renodx \"Cyberpunk 2077\" --dry-run"],
        Destructive = true,
        Run = invocation => RenoDxAsync(invocation, install: true).GetAwaiter().GetResult(),
    };

    private static CliCommand UninstallRenoDx() => new()
    {
        Name = "uninstall renodx",
        Group = "install",
        Summary = "Remove the RenoDX addon from a game. Needs --yes.",
        Usage = "rhi uninstall renodx <game> [--yes] [--dry-run]",
        Destructive = true,
        Run = invocation => RenoDxAsync(invocation, install: false).GetAwaiter().GetResult(),
    };

    private static async Task<CliResult> RenoDxAsync(CliInvocation invocation, bool install)
    {
        var game = CliGames.ResolveOne(invocation, out var failure);
        if (game is null) return failure!;

        var installer = CliServices.Get<IModInstallService>();

        if (!install)
        {
            var existing = installer.FindRecord(game.Name, game.InstallPath);
            if (existing is null) return CliResult.Fail($"RenoDX is not installed for '{game.Name}'");
            if (invocation.DryRun)
                return CliResult.Ok($"would remove {existing.AddonFileName} from {existing.InstallPath}", new { game = game.Name, addon = existing.AddonFileName, dryRun = true });
            if (!invocation.Confirmed)
                return CliResult.Usage($"refusing to remove without --yes: rhi uninstall renodx \"{game.Name}\" --yes");

            installer.Uninstall(existing);
            installer.RemoveRecord(existing);
            return CliResult.Ok($"RenoDX removed from '{game.Name}' ({existing.AddonFileName})", new { game = game.Name, removed = existing.AddonFileName });
        }

        var database = CliServices.Get<IRenoDXDbService>();
        var mods = (await database.FetchAllAsync()).Mods;
        if (mods.Count == 0)
            return CliResult.Fail("the RenoDX database returned no mods - check the connection, or run RHI once to populate the cache");

        var mod = mods.FirstOrDefault(m => m.Name.Equals(game.Name, StringComparison.OrdinalIgnoreCase))
                  ?? mods.FirstOrDefault(m => StripName(m.Name).Equals(StripName(game.Name), StringComparison.OrdinalIgnoreCase));

        if (mod is null)
            return CliResult.Fail($"'{game.Name}' is not a named RenoDX mod (generic Unreal/Unity/Source entries are not wired to the CLI yet)");

        if (invocation.DryRun)
            return CliResult.Ok($"would install RenoDX '{mod.Name}' ({mod.AddonFileName ?? "no snapshot"}) into {game.InstallPath}",
                new { game = game.Name, mod = mod.Name, addon = mod.AddonFileName, path = game.InstallPath, dryRun = true });

        if (string.IsNullOrWhiteSpace(mod.SnapshotUrl))
            return CliResult.Fail($"RenoDX entry for '{mod.Name}' has no snapshot URL - nothing to download");

        var record = await installer.InstallAsync(mod, game.InstallPath, progress: null, gameName: game.Name, store: game.Source);
        return CliResult.Ok($"RenoDX installed for '{game.Name}': {record.AddonFileName}",
            new { game = game.Name, addon = record.AddonFileName, record.InstallPath, record.InstalledAt });
    }

    private static string StripName(string name) =>
        name.Replace("\u2122", "").Replace("\u00ae", "").Trim();
}
