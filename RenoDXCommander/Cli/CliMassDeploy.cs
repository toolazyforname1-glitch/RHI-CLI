using RenoDXCommander.Models;
using RenoDXCommander.Services;
using RenoDXCommander.ViewModels;

namespace RenoDXCommander.Cli;

/// <summary>
/// Bulk rewrite of ReShade.ini for every game with ReShade installed - the same static merges
/// MassDeployHandler runs, keeping each game's hotkey and screenshot path.
/// </summary>
internal static class CliMassDeploy
{
    internal static readonly CliCommand[] All = [Command()];

    private static CliCommand Command() => new()
    {
        Name = "mass deploy",
        Group = "install",
        Summary = "Rewrite a component's ini for every game that has it installed.",
        Usage = "rhi mass deploy <reshade|relimiter|displaycommander|optiscaler> [--dry-run] [--yes]",
        Examples = ["rhi mass deploy reshade --dry-run", "rhi mass deploy optiscaler --yes"],
        Destructive = true,
        Run = Run,
    };

    private static CliResult Run(CliInvocation invocation)
    {
        var target = invocation.Operand(0)?.ToLowerInvariant();
        return target switch
        {
            "reshade" => DeployReShade(invocation),
            "relimiter" => DeployAddonIni(invocation, displayCommander: false),
            "displaycommander" => DeployAddonIni(invocation, displayCommander: true),
            "optiscaler" => DeployOptiIni(invocation),
            _ => CliResult.Usage("usage: rhi mass deploy <reshade|relimiter|displaycommander|optiscaler> [--dry-run] [--yes]"),
        };
    }

    private static CliResult DeployReShade(CliInvocation invocation)
    {
        var library = CliGames.LoadLibrary(out var failure);
        if (library is null) return failure!;

        var settings = SettingsFile.Load();
        var aux = CliServices.Get<IAuxInstallService>();
        var records = aux.LoadAll()
            .Where(record => string.Equals(record.AddonType, AuxInstallService.TypeReShade, StringComparison.OrdinalIgnoreCase)
                          || string.Equals(record.AddonType, AuxInstallService.TypeReShadeNormal, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (records.Count == 0) return CliResult.Ok("no game has ReShade installed - nothing to deploy");

        var games = CliGames.AllGames(library).ToList();
        var targets = new List<(SavedGame Game, AuxInstalledRecord Record)>();

        foreach (var record in records)
        {
            var game = games.FirstOrDefault(g =>
                           string.Equals(g.Name, record.GameName, StringComparison.OrdinalIgnoreCase) &&
                           string.Equals(g.InstallPath, record.InstallPath, StringComparison.OrdinalIgnoreCase))
                       ?? games.FirstOrDefault(g => string.Equals(g.Name, record.GameName, StringComparison.OrdinalIgnoreCase));

            if (game is not null) targets.Add((game, record));
        }

        if (targets.Count == 0)
            return CliResult.Fail($"{records.Count} ReShade install(s) found, but none match a game in the library");

        if (invocation.DryRun)
            return CliResult.Ok($"would rewrite ReShade.ini for {targets.Count} game(s): {string.Join(", ", targets.Take(10).Select(t => t.Game.Name))}",
                new { count = targets.Count, games = targets.Select(t => t.Game.Name).ToArray(), dryRun = true });

        if (!invocation.Confirmed)
            return CliResult.Usage($"refusing to rewrite ReShade.ini for {targets.Count} game(s) without --yes: rhi mass deploy reshade --yes");

        var overlayHotkey = settings.TryGetValue("OverlayHotkey", out var oh) ? oh : null;
        var screenshotHotkey = settings.TryGetValue("ScreenshotHotkey", out var sh) ? sh : null;
        var perGameFolders = settings.TryGetValue("PerGameScreenshotFolders", out var pg) && pg == "true";
        var basePath = settings.TryGetValue("ScreenshotPath", out var sp) && !string.IsNullOrWhiteSpace(sp) ? sp : null;

        var peakNits = settings.TryGetValue("PeakNits", out var pnRaw) && int.TryParse(pnRaw, out var pn) ? pn : 0;
        var peakEnabled = !(settings.TryGetValue("PeakNitsEnabled", out var peRaw) && string.Equals(peRaw, "false", StringComparison.OrdinalIgnoreCase));
        AuxInstallService.GlobalPeakNits = peakNits;
        AuxInstallService.GlobalPeakNitsEnabled = peakEnabled;

        var useTabs = !(settings.TryGetValue("RsVariableListUseTabs", out var tabsRaw) && string.Equals(tabsRaw, "false", StringComparison.OrdinalIgnoreCase));

        var written = 0;
        var failed = new List<string>();

        foreach (var (game, _) in targets)
        {
            try
            {
                var folder = basePath;
                if (perGameFolders && basePath is not null)
                {
                    var sanitized = AuxInstallService.SanitizeDirectoryName(game.Name);
                    if (!string.IsNullOrEmpty(sanitized)) folder = Path.Combine(basePath, sanitized);
                }

                var vulkan = CliCards.Build(game, out _)?.RequiresVulkanInstall == true;
                if (vulkan)
                    AuxInstallService.MergeRsVulkanIni(game.InstallPath, game.Name, folder, overlayHotkey, screenshotHotkey);
                else
                    AuxInstallService.MergeRsIni(game.InstallPath, folder, overlayHotkey, screenshotHotkey);


                var iniPath = Path.Combine(game.InstallPath, "reshade.ini");
                if (File.Exists(iniPath)) AuxInstallService.ApplyVariableListUseTabs(iniPath, useTabs);
                written++;
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                failed.Add(game.Name + ": " + ex.Message);
            }
        }

        return failed.Count == 0
            ? CliResult.Ok($"ReShade.ini rewritten for {written} game(s)", new { written, failed })
            : CliResult.Fail($"rewrote {written} game(s), {failed.Count} failed - {string.Join("; ", failed.Take(5))}",
                new { written, failed });
    }

    private static CliResult DeployAddonIni(CliInvocation invocation, bool displayCommander)
    {
        var label = displayCommander ? "Display Commander" : "ReLimiter";
        var library = CliGames.LoadLibrary(out var failure);
        if (library is null) return failure!;

        var targets = new List<SavedGame>();
        foreach (var game in CliGames.AllGames(library))
        {
            var card = CliCards.Build(game, out _);
            if (card is null) continue;
            if (displayCommander ? card.IsDcInstalled : card.IsUlInstalled) targets.Add(game);
        }

        if (targets.Count == 0) return CliResult.Ok($"no game has {label} installed - nothing to deploy");

        if (invocation.DryRun)
            return CliResult.Ok($"would rewrite the {label} ini for {targets.Count} game(s): {string.Join(", ", targets.Take(10).Select(g => g.Name))}",
                new { target = label, count = targets.Count, games = targets.Select(g => g.Name).ToArray(), dryRun = true });

        var written = 0;
        var failed = new List<string>();

        foreach (var game in targets)
        {
            try
            {
                if (displayCommander) AuxInstallService.CopyDcIni(game.InstallPath);
                else AuxInstallService.CopyUlIni(game.InstallPath);
                written++;
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                failed.Add(game.Name + ": " + ex.Message);
            }
        }

        return failed.Count == 0
            ? CliResult.Ok($"{label} ini rewritten for {written} game(s)", new { written, failed })
            : CliResult.Fail($"rewrote {written} game(s), {failed.Count} failed - {string.Join("; ", failed.Take(5))}", new { written, failed });
    }

    private static CliResult DeployOptiIni(CliInvocation invocation)
    {
        if (!File.Exists(OptiScalerService.OsIniPath))
            return CliResult.Fail($"no OptiScaler.ini to deploy ({OptiScalerService.OsIniPath}) - install OptiScaler once through RHI first");

        var library = CliGames.LoadLibrary(out var failure);
        if (library is null) return failure!;

        var settings = SettingsFile.Load();
        var hotkey = settings.TryGetValue("OsHotkey", out var h) ? h : null;
        var gpuType = settings.TryGetValue("OsGpuType", out var g) && !string.IsNullOrWhiteSpace(g) ? g : "NVIDIA";
        var dlssInputs = !(settings.TryGetValue("OsDlssInputs", out var d) && string.Equals(d, "false", StringComparison.OrdinalIgnoreCase));
        var aux = CliServices.Get<IAuxInstallService>();
        var service = CliServices.Get<IOptiScalerService>();

        var targets = new List<(SavedGame Game, GameCardViewModel Card)>();
        foreach (var game in CliGames.AllGames(library))
        {
            if (aux.FindRecord(game.Name, game.InstallPath, OptiScalerService.AddonType) is null) continue;
            var card = CliCards.Build(game, out _);
            if (card is not null) targets.Add((game, card));
        }

        if (targets.Count == 0) return CliResult.Ok("no game has OptiScaler installed - nothing to deploy");

        if (invocation.DryRun)
            return CliResult.Ok($"would rewrite OptiScaler.ini for {targets.Count} game(s): {string.Join(", ", targets.Take(10).Select(t => t.Game.Name))}",
                new { target = "OptiScaler", count = targets.Count, games = targets.Select(t => t.Game.Name).ToArray(), dryRun = true });

        var written = 0;
        var failed = new List<string>();

        foreach (var (game, card) in targets)
        {
            try
            {
                service.CopyIniToGame(card, hotkey, gpuType, dlssInputs);
                written++;
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                failed.Add(game.Name + ": " + ex.Message);
            }
        }

        return failed.Count == 0
            ? CliResult.Ok($"OptiScaler.ini rewritten for {written} game(s)", new { written, failed })
            : CliResult.Fail($"rewrote {written} game(s), {failed.Count} failed - {string.Join("; ", failed.Take(5))}", new { written, failed });
    }
}
