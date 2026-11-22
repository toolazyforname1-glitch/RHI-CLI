using RenoDXCommander.Models;
using RenoDXCommander.Services;
using RenoDXCommander.ViewModels;

namespace RenoDXCommander.Cli;

/// <summary>
/// Builds the GameCardViewModel the installers expect, from library data plus real detection.
/// This is what lets the CLI run each installer through its normal GUI entry point instead of
/// reimplementing the install logic.
/// </summary>
internal static class CliCards
{
    internal static GameCardViewModel? Build(SavedGame game, out string? error)
    {
        error = null;
        if (string.IsNullOrWhiteSpace(game.InstallPath) || !Directory.Exists(game.InstallPath))
        {
            error = $"install path for '{game.Name}' does not exist: {game.InstallPath}";
            return null;
        }

        var header = CliServices.Get<IPeHeaderService>();
        var card = new GameCardViewModel
        {
            GameName = game.Name,
            InstallPath = game.InstallPath,
            Source = game.Source,
        };

        try
        {
            card.Is32Bit = header.DetectGameArchitecture(game.InstallPath) == MachineType.I386;
            var exe = header.FindGameExe(game.InstallPath);
            if (!string.IsNullOrWhiteSpace(exe))
                card.DetectedApis = GraphicsApiDetector.DetectAllApis(exe);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or BadImageFormatException
                                       or ArgumentException or NotSupportedException)
        {
            error = $"could not inspect '{game.Name}': {ex.Message}";
            return null;
        }

        RefreshAddons(card);
        return card;
    }

    /// <summary>
    /// Display Commander and ReLimiter have no install records - the app detects them from the addon
    /// files on disk, so the headless card has to do the same or every check reports "not installed".
    /// </summary>
    internal static void RefreshAddons(GameCardViewModel card)
    {
        if (string.IsNullOrWhiteSpace(card.InstallPath) || !Directory.Exists(card.InstallPath)) return;

        var deployPath = ModInstallService.GetAddonDeployPath(card.InstallPath);

        var ulMatch = FindIn(deployPath, card.InstallPath,
            [MainViewModel.GetUlFileName(card.Is32Bit), card.Is32Bit ? "ultra_limiter.addon32" : "ultra_limiter.addon64"]);

        var dcMatch = FindIn(deployPath, card.InstallPath,
            [MainViewModel.GetDcFileName(card.Is32Bit), MainViewModel.GetLegacyDcFileName(card.Is32Bit)]);

        card.UlStatus = ulMatch is null ? GameStatus.NotInstalled : GameStatus.Installed;
        card.UlInstalledFile = ulMatch;

        card.DcStatus = dcMatch is null ? GameStatus.NotInstalled : GameStatus.Installed;
        card.DcInstalledFile = dcMatch;
    }

    private static string? FindIn(string deployPath, string installPath, string[] names)
    {
        foreach (var name in names)
        {
            if (File.Exists(Path.Combine(deployPath, name))) return name;
        }

        foreach (var name in names)
        {
            if (File.Exists(Path.Combine(installPath, name))) return name;
        }

        return null;
    }

    internal static string Describe(GameCardViewModel card) =>
        $"{card.GameName}: {string.Join(",", card.DetectedApis.Select(GraphicsApiDetector.GetLabel))}" +
        (card.Is32Bit ? " (32-bit)" : " (64-bit)");
}
