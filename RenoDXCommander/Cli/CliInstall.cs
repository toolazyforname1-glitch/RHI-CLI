using System.Text.Json;
using RenoDXCommander.Models;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// Install and remove components. Calls the same services the cards call, with the same settings
/// the GUI reads, so a terminal install produces the same files as clicking the button.
/// </summary>
internal static class CliInstall
{
    internal static readonly CliCommand[] All = [InstallReShade(), UninstallReShade()];

    private static CliCommand InstallReShade() => new()
    {
        Name = "install reshade",
        Group = "install",
        Summary = "Install ReShade into a game folder.",
        Usage = "rhi install reshade <game> [--channel Stable|Nightly] [--normal] [--dll <name>] [--packs <id,id>] [--force] [--dry-run] [--yes]",
        Examples = ["rhi install reshade \"Cyberpunk 2077\" --yes", "rhi install reshade cyberpunk --dry-run"],
        Destructive = true,
        Run = invocation => InstallReShadeAsync(invocation).GetAwaiter().GetResult(),
    };

    private static CliCommand UninstallReShade() => new()
    {
        Name = "uninstall reshade",
        Group = "install",
        Summary = "Remove ReShade from a game folder. Needs --yes.",
        Usage = "rhi uninstall reshade <game> [--dry-run] [--yes]",
        Examples = ["rhi uninstall reshade \"Cyberpunk 2077\" --yes"],
        Destructive = true,
        Run = invocation => UninstallReShadeAsync(invocation).GetAwaiter().GetResult(),
    };

    private static async Task<CliResult> InstallReShadeAsync(CliInvocation invocation)
    {
        var name = invocation.OperandText;
        if (string.IsNullOrWhiteSpace(name)) return CliResult.Usage("usage: rhi install reshade <game> [--dry-run]");

        var library = CliGames.LoadLibrary(out var failure);
        if (library is null) return failure!;

        var game = Match(library, name, out var candidates);
        if (game is null) return candidates.Count > 0
            ? CliResult.Usage($"'{name}' matches {candidates.Count} games: {string.Join(", ", candidates)}")
            : CliResult.Fail($"no game matching '{name}' - run: rhi game list");

        var installer = CliServices.Get<IAuxInstallService>();
        var directoryExists = !string.IsNullOrWhiteSpace(game.InstallPath) && Directory.Exists(game.InstallPath);

        var existing = FindReShade(installer, game.Name, game.InstallPath);
        if (existing is not null && !invocation.Has("force"))
            return CliResult.Fail($"ReShade is already installed for '{game.Name}' ({existing.InstalledAs}) - use --force to reinstall, or: rhi update reshade");

        var settings = SettingsFile.Load();
        var key = game.Name + "|" + game.Source;
        var channel = invocation.Flag("channel") ?? Value(settings, "ReShadeChannel") ?? "Stable";
        var normal = invocation.Has("normal") || List(settings, "NormalReShadeGames").Contains(key) || List(settings, "NormalReShadeGames").Contains(game.Name);
        var dll = invocation.Flag("dll");
        var packIds = invocation.Flag("packs")?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var resolved = CliGames.LookupString(library.ResolvedPathCache, game.InstallPath);
        if (resolved.Length == 0) resolved = game.InstallPath;
        var is32Bit = ResolveIs32Bit(library, resolved, game.InstallPath);

        var gameExe = CliServices.Get<IPeHeaderService>().FindGameExe(game.InstallPath);
        var apis = string.IsNullOrWhiteSpace(gameExe) ? new HashSet<GraphicsApiType>() : GraphicsApiDetector.DetectAllApis(gameExe);
        var hasDirectX = apis.Any(a => a is GraphicsApiType.DirectX8 or GraphicsApiType.DirectX9 or GraphicsApiType.DirectX10 or GraphicsApiType.DirectX11 or GraphicsApiType.DirectX12);
        var nonDx = apis.Where(a => a is GraphicsApiType.Vulkan or GraphicsApiType.OpenGL).ToList();

        var vulkanInstall = !hasDirectX && nonDx.Contains(GraphicsApiType.Vulkan) && !nonDx.Contains(GraphicsApiType.OpenGL);
        var gacDirectory = vulkanInstall ? null : await LookupGacDirectoryAsync(game.Name);

        if (!hasDirectX && nonDx.Count == 1 && nonDx[0] == GraphicsApiType.OpenGL && string.IsNullOrWhiteSpace(dll))
            dll = "opengl32.dll";

        var installMode = vulkanInstall ? "vulkan-layer" : gacDirectory is not null ? "gac-symlink" : "proxy";

        var plan = new
        {
            game = game.Name,
            path = game.InstallPath,
            pathExists = directoryExists,
            store = game.Source,
            channel = normal ? "Normal/NoAddons" : channel,
            dll = dll ?? "auto (service default)",
            is32Bit,
            install = installMode,
            gac = gacDirectory,
            overlayHotkey = Value(settings, "OverlayHotkey"),
            screenshotHotkey = Value(settings, "ScreenshotHotkey"),
        };

        if (invocation.DryRun)
            return CliResult.Ok($"would install ReShade for '{game.Name}' into {game.InstallPath} ({installMode}, channel {plan.channel}, 32-bit {is32Bit}, path exists {directoryExists})", new { plan, dryRun = true });

        if (!directoryExists)
            return CliResult.Fail($"install path for '{game.Name}' does not exist: {game.InstallPath}", plan);

        if (vulkanInstall)
            return await InstallVulkanReShadeAsync(game, channel, packIds, settings, plan);

        if (gacDirectory is not null)
            return InstallGacReShade(game, gacDirectory, dll, is32Bit, packIds, settings, plan, invocation.Has("force"));

        if (!invocation.Json) CliConsole.WriteLine("staging ReShade (downloads on first use)...");
        if (!await EnsureStagedAsync(normal, channel, is32Bit))
            return CliResult.Fail("could not stage ReShade - check the connection and try again", plan);

        var record = await installer.InstallReShadeAsync(
            game.Name,
            game.InstallPath,
            shaderModeOverride: null,
            use32Bit: is32Bit,
            filenameOverride: dll,
            selectedPackIds: packIds,
            progress: null,
            screenshotSavePath: null,
            useNormalReShade: normal,
            overlayHotkey: Value(settings, "OverlayHotkey"),
            screenshotHotkey: Value(settings, "ScreenshotHotkey"),
            channel: normal ? null : channel,
            store: game.Source,
            mergeIni: true);

        return CliResult.Ok(
            $"ReShade installed for '{game.Name}': {record.InstalledAs} (channel {record.Channel ?? channel})",
            new { game = game.Name, record.InstalledAs, record.InstallPath, record.Channel, record.InstalledAt, plan });
    }

    private static Task<CliResult> UninstallReShadeAsync(CliInvocation invocation)
    {
        var name = invocation.OperandText;
        if (string.IsNullOrWhiteSpace(name)) return Task.FromResult(CliResult.Usage("usage: rhi uninstall reshade <game> --yes"));

        var library = CliGames.LoadLibrary(out var failure);
        if (library is null) return Task.FromResult(failure!);

        var game = Match(library, name, out var candidates);
        if (game is null) return Task.FromResult(candidates.Count > 0
            ? CliResult.Usage($"'{name}' matches {candidates.Count} games: {string.Join(", ", candidates)}")
            : CliResult.Fail($"no game matching '{name}' - run: rhi game list"));

        var installer = CliServices.Get<IAuxInstallService>();
        var record = FindReShade(installer, game.Name, game.InstallPath);
        var vulkan = record is null && VulkanFootprintService.Exists(game.InstallPath);
        var gacDirectory = record is null && !vulkan ? LookupGacDirectoryAsync(game.Name).GetAwaiter().GetResult() : null;

        if (record is null && !vulkan && gacDirectory is null)
            return Task.FromResult(CliResult.Fail($"ReShade is not installed for '{game.Name}'"));

        var target = record is not null ? $"{record.InstalledAs} in {record.InstallPath}"
                   : vulkan ? "the Vulkan layer plus the game-side ReShade.ini and shaders"
                   : $"the GAC symlinks in {gacDirectory}";

        if (invocation.DryRun)
            return Task.FromResult(CliResult.Ok($"would remove ReShade ({target}) for '{game.Name}'", new { game = game.Name, target, dryRun = true }));

        if (!invocation.Confirmed)
            return Task.FromResult(CliResult.Usage($"refusing to remove without --yes: rhi uninstall reshade \"{game.Name}\" --yes"));

        if (record is not null)
        {
            installer.Uninstall(record);
            installer.RemoveRecord(record);
        }
        else if (vulkan)
        {
            var ini = Path.Combine(game.InstallPath, "ReShade.ini");
            if (File.Exists(ini)) File.Delete(ini);

            VulkanFootprintService.Delete(game.InstallPath);
            CliServices.Get<IShaderPackService>().RemoveFromGameFolder(game.InstallPath);
        }
        else
        {
            var removedLinks = UninstallGacForGame(game.InstallPath, gacDirectory!);
            var ini = Path.Combine(game.InstallPath, "ReShade.ini");
            if (File.Exists(ini)) File.Delete(ini);

            CliServices.Get<IShaderPackService>().RemoveFromGameFolder(game.InstallPath);

            if (!removedLinks)
                return Task.FromResult(CliResult.Fail($"no GAC symlink pointing at {game.InstallPath} was found in {gacDirectory}"));
        }

        return Task.FromResult(CliResult.Ok($"ReShade removed from '{game.Name}' ({target})", new { game = game.Name, removed = true, target }));
    }
    private static async Task<CliResult> InstallVulkanReShadeAsync(
        SavedGame game, string channel, IEnumerable<string>? packIds,
        Dictionary<string, string> settings, object plan)
    {
        if (VulkanFootprintService.Exists(game.InstallPath))
            return CliResult.Fail($"ReShade is already installed for '{game.Name}' as the Vulkan layer - use --force to resync", plan);

        if (!VulkanLayerService.IsLayerInstalled())
        {
            if (!VulkanLayerService.IsRunningAsAdmin())
                return CliResult.Fail("the Vulkan layer writes to C:\\ProgramData and HKLM\\SOFTWARE\\Khronos\\Vulkan\\ImplicitLayers, so it needs an elevated terminal", plan);

            if (!await EnsureStagedAsync(useNormal: false, channel: "Stable", is32Bit: false))
                return CliResult.Fail("could not stage the stable ReShade build the layer install requires", plan);

            try { VulkanLayerService.InstallLayer(); }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                return CliResult.Fail($"installing the Vulkan layer failed: {ex.Message}", plan);
            }
        }

        if (AuxInstallService.IsLegacyVersion(channel))
        {
            if (!AuxInstallService.IsLegacyVersionCached(channel)
                && !await AuxInstallService.DownloadLegacyReShadeAsync(channel, CliServices.Get<HttpClient>()))
                return CliResult.Fail($"could not download ReShade {channel} for the Vulkan layer", plan);
        }
        else if (string.Equals(channel, AuxInstallService.ChannelCustom, StringComparison.OrdinalIgnoreCase))
        {
            return CliResult.Fail("the Custom channel is per-game; pick it in the app's channel section, which copies that DLL into the shared layer", plan);
        }
        else if (!await EnsureStagedAsync(useNormal: false, channel, is32Bit: false))
        {
            return CliResult.Fail($"could not stage ReShade ({channel}) for the Vulkan layer", plan);
        }

        VulkanLayerService.SyncLayerToChannel(channel);

        AuxInstallService.EnsureInisDir();
        AuxInstallService.MergeRsVulkanIni(game.InstallPath, game.Name, null,
            Value(settings, "OverlayHotkey"), Value(settings, "ScreenshotHotkey"));
        VulkanFootprintService.Create(game.InstallPath);
        SyncPacks(game.InstallPath, packIds);

        var layerDll = Path.Combine(VulkanLayerService.LayerDirectory, VulkanLayerService.LayerDllName);
        if (!File.Exists(layerDll))
            return CliResult.Fail($"the Vulkan layer DLL is missing after the install: {layerDll}", plan);

        var version = AuxInstallService.ReadInstalledVersion(VulkanLayerService.LayerDirectory, VulkanLayerService.LayerDllName);
        return CliResult.Ok(
            $"ReShade {version ?? channel} installed for '{game.Name}' as the Vulkan layer ({layerDll}); ReShade.ini and shaders are in the game folder",
            new { game = game.Name, install = "vulkan-layer", layer = layerDll, version, channel, plan });
    }

    private static CliResult InstallGacReShade(
        SavedGame game, string gacDirectory, string? dll, bool is32Bit,
        IEnumerable<string>? packIds, Dictionary<string, string> settings, object plan, bool force)
    {
        if (File.Exists(Path.Combine(game.InstallPath, "ReShade.ini")) && !force)
            return CliResult.Fail($"ReShade is already installed for '{game.Name}' (the game folder has ReShade.ini) - use --force to reinstall", plan);

        if (!VulkanLayerService.IsRunningAsAdmin())
            return CliResult.Fail("the GAC symlink install writes into the Global Assembly Cache, so it needs an elevated terminal", plan);

        if (!AuxInstallService.EnsureReShadeStaging())
            return CliResult.Fail("ReShade is not staged (no ReShade64.dll/ReShade32.dll under %LOCALAPPDATA%\\RHI\\reshade) - stage it with a normal install first", plan);

        var fileName = string.IsNullOrWhiteSpace(dll) ? "dxgi.dll" : dll;

        try
        {
            AuxInstallService.InstallGacSymlink(game.InstallPath, gacDirectory, fileName, is32Bit, null,
                Value(settings, "OverlayHotkey"), Value(settings, "ScreenshotHotkey"));
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or DirectoryNotFoundException)
        {
            return CliResult.Fail($"GAC symlink install failed for '{game.Name}': {ex.Message}", plan);
        }

        SyncPacks(game.InstallPath, packIds);
        var version = AuxInstallService.ReadInstalledVersion(game.InstallPath, fileName);
        return CliResult.Ok(
            $"ReShade {version} installed for '{game.Name}' into the GAC as {fileName} (symlinked from {game.InstallPath})",
            new { game = game.Name, install = "gac-symlink", gac = gacDirectory, dll = fileName, version, plan });
    }

    private static void SyncPacks(string installPath, IEnumerable<string>? packIds)
    {
        var ids = packIds?.ToArray() ?? [];
        if (ids.Length == 0) return;

        var packs = CliServices.Get<IShaderPackService>();
        packs.EnsurePacksAsync(ids).GetAwaiter().GetResult();
        var exclusions = ids.ToDictionary(id => id, id => packs.GetExcludedFiles(id));
        packs.SyncGameFolder(installPath, ids, exclusions);
    }

    private static async Task<string?> LookupGacDirectoryAsync(string gameName)
    {
        try
        {
            var manifestService = CliServices.Get<IManifestService>();
            var manifest = manifestService.LoadCached();
            var games = manifest?.GacSymlinkGames;

            if (games is null || games.Count == 0)
            {
                manifest = await manifestService.FetchAsync() ?? manifest;
                games = manifest?.GacSymlinkGames;
            }

            if (games is null || games.Count == 0) return null;
            if (games.TryGetValue(gameName, out var exact)) return exact;

            var stripped = StripMarks(gameName);
            foreach (var (name, path) in games)
                if (string.Equals(StripMarks(name), stripped, StringComparison.OrdinalIgnoreCase)) return path;

            return null;
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or JsonException or InvalidOperationException)
        {
            return null;
        }
    }

    private static string StripMarks(string name) =>
        name.Replace("™", "").Replace("®", "").Replace("©", "").Trim();

    private static bool UninstallGacForGame(string installPath, string gacDirectory)
    {
        if (!Directory.Exists(gacDirectory)) return false;

        var wanted = Path.GetFullPath(installPath).TrimEnd(Path.DirectorySeparatorChar);
        var names = new List<string>();

        foreach (var link in Directory.EnumerateFiles(gacDirectory))
        {
            string? target;
            try { target = File.ResolveLinkTarget(link, returnFinalTarget: true)?.FullName; }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException) { continue; }

            if (target is null) continue;
            if (!string.Equals(Path.GetDirectoryName(target), wanted, StringComparison.OrdinalIgnoreCase)) continue;
            names.Add(Path.GetFileName(link));
        }

        foreach (var name in names.Distinct(StringComparer.OrdinalIgnoreCase))
            AuxInstallService.UninstallGacSymlink(installPath, gacDirectory, name);

        return names.Count > 0;
    }


    /// <summary>
    /// The library's bitness cache only exists for games RHI has scanned; a manual or never-scanned
    /// game has no entry, and guessing 64-bit installs a proxy the game's loader silently skips.
    /// </summary>
    private static bool ResolveIs32Bit(SavedGameLibrary library, string resolvedPath, string installPath)
    {
        var cached = CliGames.LookupBitness(library.BitnessCache, resolvedPath);
        if (cached.Length > 0)
            return string.Equals(cached, MachineType.I386.ToString(), StringComparison.OrdinalIgnoreCase);

        try
        {
            return CliServices.Get<IPeHeaderService>().DetectGameArchitecture(installPath) == MachineType.I386;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or BadImageFormatException
                                       or ArgumentException or NotSupportedException)
        {
            return false;
        }
    }

    private static async Task<bool> EnsureStagedAsync(bool useNormal, string channel, bool is32Bit)
    {
        var nightly = string.Equals(channel, "Nightly", StringComparison.OrdinalIgnoreCase);

        try
        {
            if (useNormal) await CliServices.Get<INormalReShadeUpdateService>().EnsureLatestAsync();
            else if (nightly) await CliServices.Get<ReShadeNightlyService>().EnsureLatestAsync();
            else await CliServices.Get<IReShadeUpdateService>().EnsureLatestAsync();
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or IOException)
        {
            return false;
        }

        var staged = useNormal
            ? is32Bit ? AuxInstallService.RsNormalStagedPath32 : AuxInstallService.RsNormalStagedPath64
            : nightly
                ? is32Bit ? AuxInstallService.RsNightlyStagedPath32 : AuxInstallService.RsNightlyStagedPath64
                : is32Bit ? AuxInstallService.RsStagedPath32 : AuxInstallService.RsStagedPath64;

        return File.Exists(staged) && new FileInfo(staged).Length > AuxInstallService.MinReShadeSize;
    }

    private static AuxInstalledRecord? FindReShade(IAuxInstallService installer, string gameName, string installPath) =>
        installer.FindRecord(gameName, installPath, AuxInstallService.TypeReShade)
        ?? installer.FindRecord(gameName, installPath, AuxInstallService.TypeReShadeNormal);

    private static SavedGame? Match(SavedGameLibrary library, string wanted, out List<string> candidates)
    {
        var games = CliGames.AllGames(library).ToList();
        candidates = games.Where(g => g.Name.Contains(wanted, StringComparison.OrdinalIgnoreCase)).Select(g => g.Name).Take(8).ToList();
        return games.FirstOrDefault(g => string.Equals(g.Name, wanted, StringComparison.OrdinalIgnoreCase))
               ?? (candidates.Count == 1 ? games.First(g => g.Name.Contains(wanted, StringComparison.OrdinalIgnoreCase)) : null);
    }

    private static string? Value(Dictionary<string, string> settings, string key) =>
        settings.TryGetValue(key, out var value) && !string.IsNullOrWhiteSpace(value) ? value : null;

    private static List<string> List(Dictionary<string, string> settings, string key)
    {
        if (!settings.TryGetValue(key, out var json) || string.IsNullOrWhiteSpace(json)) return [];
        try { return JsonSerializer.Deserialize<List<string>>(json) ?? []; }
        catch (JsonException) { return []; }
    }
}
