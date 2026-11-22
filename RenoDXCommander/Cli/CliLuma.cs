using System.Text.Json;
using RenoDXCommander.Models;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>Luma Framework install, resolved from the Luma wiki the same way the card does.</summary>
internal static class CliLuma
{
    internal static readonly CliCommand[] All = [InstallLuma(), UninstallLuma(), ModeCommand()];

    private static CliCommand UninstallLuma() => new()
    {
        Name = "uninstall luma",
        Group = "install",
        Summary = "Remove the Luma Framework mod from a game. Needs --yes.",
        Usage = "rhi uninstall luma <game> [--yes] [--dry-run]",
        Destructive = true,
        Run = invocation => Uninstall(invocation).GetAwaiter().GetResult(),
    };

    private static async Task<CliResult> Uninstall(CliInvocation invocation)
    {
        var game = CliGames.ResolveOne(invocation, out var failure);
        if (game is null) return failure!;

        var records = LumaService.GetAllRecords();
        var record = records.FirstOrDefault(r =>
                         string.Equals(r.GameName, game.Name, StringComparison.OrdinalIgnoreCase) &&
                         string.Equals(r.InstallPath, game.InstallPath, StringComparison.OrdinalIgnoreCase))
                     ?? records.FirstOrDefault(r => string.Equals(r.GameName, game.Name, StringComparison.OrdinalIgnoreCase));

        if (record is null) return CliResult.Fail($"Luma is not installed for '{game.Name}'");

        if (invocation.DryRun)
            return CliResult.Ok($"would remove {record.InstalledFiles.Count} Luma file(s) from {record.InstallPath}",
                new { game = game.Name, files = record.InstalledFiles.Count, path = record.InstallPath, dryRun = true });

        if (!invocation.Confirmed)
            return CliResult.Usage($"refusing to remove without --yes: rhi uninstall luma \"{game.Name}\" --yes");

        var service = CliServices.Get<ILumaService>();
        var settings = SettingsFile.Load();
        var key = $"{game.Name}|{game.Source ?? ""}";

        var wasDgVoodoo = record.InstalledFiles.Any(f => f.Equals("D3D9.dll", StringComparison.OrdinalIgnoreCase));
        var keptDgVoodoo = false;

        if (wasDgVoodoo)
        {
            var feeder = File.Exists(Path.Combine(game.InstallPath, "dlss5-feed.addon32"))
                      || File.Exists(Path.Combine(game.InstallPath, "dlss5-feed.addon64"));
            var standalone = ArrayContains(settings, "DgVoodooStandaloneGames", key)
                          || ArrayContains(settings, "DgVoodooStandaloneGames", game.Name);

            if (feeder || standalone)
            {
                record.InstalledFiles.RemoveAll(f => f.Equals("D3D9.dll", StringComparison.OrdinalIgnoreCase)
                                                  || f.Equals("dgVoodoo.conf", StringComparison.OrdinalIgnoreCase));
                wasDgVoodoo = false;
                keptDgVoodoo = true;
            }
        }

        service.Uninstall(record);
        service.RemoveLumaRecord(record.GameName, record.InstallPath);

        RestoreNvngxDlss(game.InstallPath);

        var installer = CliServices.Get<IAuxInstallService>();
        var reshade = installer.FindRecord(game.Name, game.InstallPath, AuxInstallService.TypeReShade)
                   ?? installer.FindRecord(game.Name, game.InstallPath, AuxInstallService.TypeReShadeNormal);

        var reshadeNote = "ReShade is not installed, so nothing to refresh";

        if (reshade is not null)
        {
            if (wasDgVoodoo)
            {
                await ReinstallReShadeAsync(game, reshade, settings);
                await SyncShadersAsync(game, settings);
                reshadeNote = "ReShade reinstalled with the auto-detected proxy (dgVoodoo needed the dxgi override)";
            }
            else
            {
                AuxInstallService.EnsureInisDir();
                AuxInstallService.MergeRsIni(game.InstallPath, ScreenshotPath(settings, game.Name),
                    Value(settings, "OverlayHotkey"), Value(settings, "ScreenshotHotkey"), game.Name);
                await SyncShadersAsync(game, settings);
                reshadeNote = "reshade.ini re-merged and managed shaders redeployed";
            }
        }

        return CliResult.Ok(
            $"Luma removed from '{game.Name}' ({record.InstalledFiles.Count} files{(keptDgVoodoo ? ", dgVoodoo2 kept for Feeder/standalone" : "")}); {reshadeNote}",
            new { game = game.Name, files = record.InstalledFiles.Count, removed = true, dgVoodooKept = keptDgVoodoo, reshade = reshadeNote });
    }

    private static void RestoreNvngxDlss(string installPath)
    {
        var dlss = Path.Combine(installPath, "nvngx_dlss.dll");
        var sentinel = dlss + ".original";
        if (!File.Exists(sentinel)) return;

        if (new FileInfo(sentinel).Length == 0)
        {
            try { File.Delete(dlss); } catch (Exception ex) when (ex is IOException or UnauthorizedAccessException) { }
            try { File.Delete(sentinel); } catch (Exception ex) when (ex is IOException or UnauthorizedAccessException) { }
        }
        else
        {
            AuxInstallService.SentinelRestore(dlss);
        }
    }

    private static async Task ReinstallReShadeAsync(SavedGame game, AuxInstalledRecord reshade, Dictionary<string, string> settings)
    {
        var is32Bit = CliServices.Get<IPeHeaderService>().DetectGameArchitecture(game.InstallPath) == MachineType.I386;

        await CliServices.Get<IAuxInstallService>().InstallReShadeAsync(
            game.Name,
            game.InstallPath,
            shaderModeOverride: null,
            use32Bit: is32Bit,
            filenameOverride: null,
            selectedPackIds: null,
            progress: null,
            screenshotSavePath: ScreenshotPath(settings, game.Name),
            useNormalReShade: reshade.AddonType == AuxInstallService.TypeReShadeNormal,
            overlayHotkey: Value(settings, "OverlayHotkey"),
            screenshotHotkey: Value(settings, "ScreenshotHotkey"),
            channel: reshade.Channel,
            store: game.Source,
            mergeIni: true);
    }

    private static async Task SyncShadersAsync(SavedGame game, Dictionary<string, string> settings)
    {
        var key = $"{game.Name}|{game.Source ?? ""}";
        var mode = MapValue(settings, "PerGameShaderMode", key, game.Name);

        IEnumerable<string>? selection;

        if (string.Equals(mode, "Off", StringComparison.OrdinalIgnoreCase)) selection = null;
        else if (string.Equals(mode, "Custom", StringComparison.OrdinalIgnoreCase)) selection = [ShaderPackService.CustomShaderSentinel];
        else if (string.Equals(mode, "Select", StringComparison.OrdinalIgnoreCase)) selection = ListMapValue(settings, "PerGameShaderSelection", key, game.Name);
        else if (Flag(settings, "GlobalShadersOff")) selection = null;
        else if (Flag(settings, "UseCustomShaders")) selection = [ShaderPackService.CustomShaderSentinel];
        else selection = ReadList(settings, "SelectedShaderPacks");

        var packs = CliServices.Get<IShaderPackService>();
        if (selection is not null) await packs.EnsurePacksAsync(selection);

        var exclusions = selection?.ToDictionary(id => id, id => packs.GetExcludedFiles(id));
        packs.SyncGameFolder(game.InstallPath, selection, exclusions);
    }

    private static string? ScreenshotPath(Dictionary<string, string> settings, string gameName)
    {
        var basePath = Value(settings, "ScreenshotPath");
        if (string.IsNullOrEmpty(basePath)) return null;
        if (!Flag(settings, "PerGameScreenshotFolders")) return basePath;

        var sanitized = AuxInstallService.SanitizeDirectoryName(gameName);
        return string.IsNullOrEmpty(sanitized) ? basePath : basePath + @"\" + sanitized;
    }

    private static string? MapValue(Dictionary<string, string> settings, string settingKey, string key, string gameName)
    {
        var map = ReadObject(settings, settingKey);
        if (map.TryGetValue(key, out var value)) return value;
        return map.TryGetValue(gameName, out var legacy) ? legacy : null;
    }

    private static IEnumerable<string>? ListMapValue(Dictionary<string, string> settings, string settingKey, string key, string gameName)
    {
        var map = ReadObjectArray(settings, settingKey);
        if (map.TryGetValue(key, out var value)) return value;
        return map.TryGetValue(gameName, out var legacy) ? legacy : null;
    }

    private static Dictionary<string, string> ReadObject(Dictionary<string, string> settings, string settingKey)
    {
        if (!settings.TryGetValue(settingKey, out var raw) || string.IsNullOrWhiteSpace(raw)) return new(StringComparer.OrdinalIgnoreCase);

        try { return JsonSerializer.Deserialize<Dictionary<string, string>>(raw) ?? new(StringComparer.OrdinalIgnoreCase); }
        catch (JsonException) { return new(StringComparer.OrdinalIgnoreCase); }
    }

    private static Dictionary<string, List<string>> ReadObjectArray(Dictionary<string, string> settings, string settingKey)
    {
        if (!settings.TryGetValue(settingKey, out var raw) || string.IsNullOrWhiteSpace(raw)) return new(StringComparer.OrdinalIgnoreCase);

        try { return JsonSerializer.Deserialize<Dictionary<string, List<string>>>(raw) ?? new(StringComparer.OrdinalIgnoreCase); }
        catch (JsonException) { return new(StringComparer.OrdinalIgnoreCase); }
    }

    private static List<string> ReadList(Dictionary<string, string> settings, string settingKey)
    {
        if (!settings.TryGetValue(settingKey, out var raw) || string.IsNullOrWhiteSpace(raw)) return [];

        try { return JsonSerializer.Deserialize<List<string>>(raw) ?? []; }
        catch (JsonException) { return []; }
    }

    private static bool Flag(Dictionary<string, string> settings, string settingKey) =>
        settings.TryGetValue(settingKey, out var raw) && string.Equals(raw, "true", StringComparison.OrdinalIgnoreCase);

    private static bool ArrayContains(Dictionary<string, string> settings, string settingKey, string value) =>
        ReadList(settings, settingKey).Any(v => string.Equals(v, value, StringComparison.OrdinalIgnoreCase));

    private static CliCommand InstallLuma() => new()
    {
        Name = "install luma",
        Group = "install",
        Summary = "Install the Luma Framework mod for a game from the Luma wiki (named mod, or --generic for the Unreal Engine build).",
        Usage = "rhi install luma <game> [--generic] [--dry-run] [--yes]",
        Examples = ["rhi install luma \"Alan Wake 2\" --dry-run", "rhi install luma \"Borderlands 3\" --generic --yes"],
        Destructive = true,
        Run = invocation => LumaAsync(invocation).GetAwaiter().GetResult(),
    };

    private static async Task<CliResult> LumaAsync(CliInvocation invocation)
    {
        var game = CliGames.ResolveOne(invocation, out var failure);
        if (game is null) return failure!;

        if (invocation.Has("generic")) return await GenericAsync(invocation, game);

        var service = CliServices.Get<ILumaService>();
        var mods = await service.FetchCompletedModsAsync();
        if (mods.Count == 0)
            return CliResult.Fail("the Luma wiki returned no mods - check the connection");

        var mod = mods.FirstOrDefault(m => m.Name.Equals(game.Name, StringComparison.OrdinalIgnoreCase))
                  ?? mods.FirstOrDefault(m => m.Name.Contains(game.Name, StringComparison.OrdinalIgnoreCase));
        if (mod is null)
            return CliResult.Fail($"'{game.Name}' is not a named Luma mod - try --generic for the Unreal Engine build");

        var settings = SettingsFile.Load();
        if (invocation.DryRun)
            return CliResult.Ok($"would install Luma '{mod.Name}' into {game.InstallPath} (download {mod.DownloadUrl ?? "none"}, dgVoodoo {mod.RequiresDgVoodoo})",
                new { game = game.Name, mod = mod.Name, download = mod.DownloadUrl, mod.RequiresDgVoodoo, path = game.InstallPath, dryRun = true });

        var record = await service.InstallAsync(
            mod,
            game.InstallPath,
            selectedShaderPacks: null,
            screenshotSavePath: null,
            overlayHotkey: Value(settings, "OverlayHotkey"),
            screenshotHotkey: Value(settings, "ScreenshotHotkey"),
            gameName: game.Name,
            progress: null,
            store: game.Source);

        return CliResult.Ok(
            $"Luma installed for '{game.Name}' (build {record.InstalledBuildNumber}, {record.InstalledFiles.Count} files)",
            new { game = game.Name, mod = mod.Name, record.InstalledBuildNumber, files = record.InstalledFiles.Count, record.InstallPath });
    }

    /// <summary>
    /// Generic Unreal Engine Luma install - mirrors the GUI's generic card install
    /// (Luma-Unreal_Engine.zip, Engine.ini keys, TAA flag, launch args, dgVoodoo2, DLSS, ReShade).
    /// </summary>
    private static async Task<CliResult> GenericAsync(CliInvocation invocation, SavedGame game)
    {
        var service = CliServices.Get<ILumaService>();
        var table = await service.FetchGenericUeTableAsync();
        if (table.Count == 0)
            return CliResult.Fail("the Luma wiki returned no generic Unreal Engine entries - check the connection");

        var entry = table.TryGetValue(game.Name, out var exact)
            ? exact
            : table.Values.FirstOrDefault(e => e.Name.Contains(game.Name, StringComparison.OrdinalIgnoreCase));

        if (entry is null)
            return CliResult.Fail($"'{game.Name}' is not in the Luma generic Unreal Engine table ({table.Count} entries available)");

        // The GUI only offers generic Luma when the wiki does not mark the game as blocked for both paths.
        if (entry.DlssFsrBlocked && !entry.HdrSupported)
            return CliResult.Fail($"'{entry.Name}' is marked DLSS/FSR-blocked without HDR in the Luma wiki, so the GUI offers no generic Luma for it");

        const string genericLumaUrl = "https://github.com/Filoppi/Luma-Framework/releases/latest/download/Luma-Unreal_Engine.zip";
        var settings = SettingsFile.Load();

        if (invocation.DryRun)
            return CliResult.Ok(
                $"would install generic Luma for '{entry.Name}' into {game.InstallPath} (UE {entry.UeVersion ?? "unknown"}, " +
                $"HDR {entry.HdrSupported}, DLSS/FSR {entry.DlssFsrSupported}{(entry.DlssFsrBlocked ? " blocked" : "")}, " +
                $"{entry.EngineIniKeys.Count} Engine.ini key(s), launch args {(string.IsNullOrWhiteSpace(entry.LaunchArgs) ? "none" : entry.LaunchArgs)})",
                new
                {
                    game = game.Name,
                    entry = entry.Name,
                    generic = true,
                    download = genericLumaUrl,
                    ueVersion = entry.UeVersion,
                    hdr = entry.HdrSupported,
                    dlssFsr = entry.DlssFsrSupported,
                    dlssFsrBlocked = entry.DlssFsrBlocked,
                    engineIniKeys = entry.EngineIniKeys.Count,
                    launchArgs = entry.LaunchArgs,
                    notes = entry.Notes,
                    path = game.InstallPath,
                    dryRun = true,
                });

        var is32Bit = false;
        try { is32Bit = CliServices.Get<IPeHeaderService>().DetectGameArchitecture(game.InstallPath) == MachineType.I386; }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or BadImageFormatException
                                       or ArgumentException or NotSupportedException) { }

        var manifest = LoadManifest();
        var needsDgVoodoo = manifest?.LumaRequiresDgVoodoo?.Contains(game.Name, StringComparer.OrdinalIgnoreCase) == true;

        // Same synthetic mod the GUI builds for a generic card (Name = game name -> record.GameName).
        var mod = new LumaMod
        {
            Name = game.Name,
            IsGenericLuma = true,
            Author = "Pumbo",
            DownloadUrl = genericLumaUrl,
            SpecialNotes = entry.Notes,
        };

        var record = await service.InstallAsync(
            mod,
            game.InstallPath,
            selectedShaderPacks: null,
            screenshotSavePath: ScreenshotPath(settings, game.Name),
            overlayHotkey: Value(settings, "OverlayHotkey"),
            screenshotHotkey: Value(settings, "ScreenshotHotkey"),
            gameName: game.Name,
            progress: null,
            store: game.Source);

        var dlssNote = await DeployNewestDlssAsync(game.InstallPath);

        AuxInstallService.SetLumaReshadeIniValue(game.InstallPath, "EnableHDR", "1");
        AuxInstallService.SetLumaReshadeIniValue(game.InstallPath, "DisplayMode", "1");

        var reshadeNote = await EnsureReShadeAsync(game, settings, needsDgVoodoo, is32Bit);
        var dgNote = await EnsureDgVoodooAsync(service, record, game, manifest, needsDgVoodoo, is32Bit);

        var iniNote = "no Engine.ini keys in the wiki entry";
        var taaEnabled = false;
        if (entry.EngineIniKeys.Count > 0)
        {
            // EngineIniProjectOverride is live card state with no persisted form - the GUI default (null) is used.
            AuxInstallService.ApplyEngineIniCustomKeys(game.InstallPath, entry.EngineIniKeys, projectNameOverride: null, gameName: game.Name, store: game.Source);
            iniNote = $"{entry.EngineIniKeys.Count} Engine.ini key(s) written";
            taaEnabled = entry.EngineIniKeys.Any(k =>
                k.Key.Equals("r.DefaultFeature.AntiAliasing", StringComparison.OrdinalIgnoreCase)
                || k.Key.Equals("r.PostProcessAAQuality", StringComparison.OrdinalIgnoreCase));
        }

        var autoArgs = "";
        if (!string.IsNullOrWhiteSpace(entry.LaunchArgs)) autoArgs = entry.LaunchArgs.Trim();
        else if (HasDualApi(game.InstallPath)) autoArgs = "-dx11";

        var taaSaved = false;
        var argsSaved = false;
        SettingsFile.Update(s =>
        {
            if (taaEnabled)
            {
                var taa = ReadList(s, "LumaTaaEnabled");
                if (!taa.Any(g => string.Equals(g, game.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    taa.Add(game.Name);
                    s["LumaTaaEnabled"] = JsonSerializer.Serialize(taa);
                    taaSaved = true;
                }
            }

            if (!string.IsNullOrWhiteSpace(autoArgs))
            {
                var map = ReadObject(s, "LaunchArgsOverrides");
                var existingKey = map.Keys.FirstOrDefault(k => string.Equals(k, game.Name, StringComparison.OrdinalIgnoreCase));
                if (existingKey is null)
                {
                    map[game.Name] = autoArgs;
                    s["LaunchArgsOverrides"] = JsonSerializer.Serialize(map);
                    argsSaved = true;
                }
                else if (string.IsNullOrWhiteSpace(map[existingKey]))
                {
                    map[existingKey] = autoArgs;
                    s["LaunchArgsOverrides"] = JsonSerializer.Serialize(map);
                    argsSaved = true;
                }
            }
        }, "cli:install luma --generic");

        return CliResult.Ok(
            $"generic Luma installed for '{game.Name}' (build {record.InstalledBuildNumber}, {record.InstalledFiles.Count} files; " +
            $"UE {entry.UeVersion ?? "unknown"}; {iniNote}{(taaSaved ? ", TAA flag saved" : "")}" +
            $"{(argsSaved ? $", launch args '{autoArgs}' saved" : "")}; {reshadeNote}; {dgNote}; {dlssNote})",
            new
            {
                game = game.Name,
                entry = entry.Name,
                generic = true,
                record.InstalledBuildNumber,
                files = record.InstalledFiles.Count,
                record.InstallPath,
                ueVersion = entry.UeVersion,
                hdr = entry.HdrSupported,
                dlssFsr = entry.DlssFsrSupported,
                engineIniKeys = entry.EngineIniKeys.Count,
                taaFlagSaved = taaSaved,
                launchArgs = autoArgs,
                launchArgsSaved = argsSaved,
                reshade = reshadeNote,
                dgVoodoo = dgNote,
                dlss = dlssNote,
            });
    }

    /// <summary>Same DLSS step as ApplyLumaPostInstallAsync: newest cached DLSS, sentinel-backed copy.</summary>
    private static async Task<string> DeployNewestDlssAsync(string installPath)
    {
        try
        {
            var newest = await CliServices.Get<IDlssStreamlineService>().EnsureNewestDlssCachedAsync();
            if (string.IsNullOrEmpty(newest) || !File.Exists(newest)) return "no DLSS to deploy";

            var target = Path.Combine(installPath, "nvngx_dlss.dll");
            AuxInstallService.SentinelBackup(target);
            File.Copy(newest, target, overwrite: true);
            return "newest DLSS deployed";
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or HttpRequestException or InvalidOperationException)
        {
            return $"DLSS deploy failed ({ex.Message})";
        }
    }

    /// <summary>Installs ReShade the way the Luma post-install does, forcing dxgi.dll when dgVoodoo2 is needed.</summary>
    private static async Task<string> EnsureReShadeAsync(SavedGame game, Dictionary<string, string> settings, bool needsDgVoodoo, bool is32Bit)
    {
        var installer = CliServices.Get<IAuxInstallService>();
        var existing = installer.FindRecord(game.Name, game.InstallPath, AuxInstallService.TypeReShade)
                    ?? installer.FindRecord(game.Name, game.InstallPath, AuxInstallService.TypeReShadeNormal);

        var key = $"{game.Name}|{game.Source ?? ""}";
        var channel = MapValue(settings, "ReShadeChannelOverrides", key, game.Name) ?? AuxInstallService.ChannelStable;
        var normalGames = ReadList(settings, "NormalReShadeGames");
        var normal = existing?.AddonType == AuxInstallService.TypeReShadeNormal
                  || normalGames.Any(g => string.Equals(g, $"{game.Name}|{game.Source ?? ""}", StringComparison.OrdinalIgnoreCase))
                  || normalGames.Any(g => string.Equals(g, game.Name, StringComparison.OrdinalIgnoreCase));

        if (!await EnsureReShadeStagedAsync(normal, channel, is32Bit))
            return "ReShade staging failed - run: rhi install reshade";

        try
        {
            var record = await installer.InstallReShadeAsync(
                game.Name,
                game.InstallPath,
                shaderModeOverride: null,
                use32Bit: is32Bit,
                filenameOverride: needsDgVoodoo ? "dxgi.dll" : null,
                selectedPackIds: null,
                progress: null,
                screenshotSavePath: ScreenshotPath(settings, game.Name),
                useNormalReShade: normal,
                overlayHotkey: Value(settings, "OverlayHotkey"),
                screenshotHotkey: Value(settings, "ScreenshotHotkey"),
                channel: normal ? null : channel,
                store: game.Source,
                mergeIni: true);

            return $"ReShade {record.InstalledAs} {(existing is null ? "installed" : "refreshed")}";
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or HttpRequestException or InvalidOperationException)
        {
            return $"ReShade install failed ({ex.Message}) - run: rhi install reshade \"{game.Name}\" --yes";
        }
    }

    private static async Task<bool> EnsureReShadeStagedAsync(bool useNormal, string channel, bool is32Bit)
    {
        try
        {
            if (useNormal) await CliServices.Get<INormalReShadeUpdateService>().EnsureLatestAsync();
            else if (string.Equals(channel, AuxInstallService.ChannelNightly, StringComparison.OrdinalIgnoreCase))
                await CliServices.Get<ReShadeNightlyService>().EnsureLatestAsync();
            else await CliServices.Get<IReShadeUpdateService>().EnsureLatestAsync();
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or IOException)
        {
            return false;
        }

        var staged = useNormal
            ? is32Bit ? AuxInstallService.RsNormalStagedPath32 : AuxInstallService.RsNormalStagedPath64
            : AuxInstallService.GetStagedPathForChannel(channel, is32Bit);

        return File.Exists(staged) && new FileInfo(staged).Length > AuxInstallService.MinReShadeSize;
    }

    /// <summary>dgVoodoo2 deploy exactly as ApplyLumaPostInstallAsync does it (manifest-driven, no wiki version for generic).</summary>
    private static async Task<string> EnsureDgVoodooAsync(
        ILumaService service,
        LumaInstalledRecord record,
        SavedGame game,
        RemoteManifest? manifest,
        bool needsDgVoodoo,
        bool is32Bit)
    {
        if (!needsDgVoodoo) return "dgVoodoo2 not required";
        if (manifest?.DgVoodooVersions is not { Count: > 0 } versions)
            return "dgVoodoo2 required by the manifest but no dgVoodoo version is listed - skipped";

        try
        {
            var version = versions.First();
            var dgVoodoo = CliServices.Get<DgVoodooService>();
            await dgVoodoo.EnsureStagedAsync(version.Key, version.Value);
            var deployed = dgVoodoo.DeployToGame(game.InstallPath, version.Key, is64Bit: !is32Bit);

            foreach (var file in deployed)
                if (!record.InstalledFiles.Contains(file, StringComparer.OrdinalIgnoreCase))
                    record.InstalledFiles.Add(file);

            service.SaveLumaRecord(record);
            return $"dgVoodoo2 v{version.Key} deployed";
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or HttpRequestException or InvalidOperationException)
        {
            return $"dgVoodoo2 deploy failed ({ex.Message})";
        }
    }

    private static RemoteManifest? LoadManifest()
    {
        try { return CliServices.Get<IManifestService>().LoadCached(); }
        catch (Exception ex) when (ex is IOException or JsonException or InvalidOperationException) { return null; }
    }

    private static bool HasDualApi(string installPath)
    {
        try
        {
            var exe = CliServices.Get<IPeHeaderService>().FindGameExe(installPath);
            if (string.IsNullOrWhiteSpace(exe)) return false;
            var apis = GraphicsApiDetector.DetectAllApis(exe);
            return apis.Contains(GraphicsApiType.DirectX11) && apis.Contains(GraphicsApiType.DirectX12);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or ArgumentException or NotSupportedException)
        {
            return false;
        }
    }

    private static string? Value(Dictionary<string, string> settings, string key) =>
        settings.TryGetValue(key, out var value) && !string.IsNullOrWhiteSpace(value) ? value : null;

    private static CliCommand ModeCommand() => new()
    {
        Name = "luma mode",
        Group = "install",
        Summary = "Turn Luma mode on or off for a game (on removes RenoDX and ReShade first).",
        Usage = "rhi luma mode <game> on|off [--json]",
        Examples = ["rhi luma mode cyberpunk on --yes"],
        Destructive = true,
        RequiresGui = true,
        Run = Mode,
    };

    private static CliResult Mode(CliInvocation invocation)
    {
        var args = invocation.Args;
        if (args.Length < 2) return CliResult.Usage("usage: rhi luma mode <game> on|off");

        var want = args[1].ToLowerInvariant();
        if (want is not ("on" or "off")) return CliResult.Usage("value must be on or off");

        if (invocation.DryRun)
            return CliResult.Ok($"would turn Luma mode {want} for '{args[0]}' (needs RHI running)", new { game = args[0], lumaMode = want == "on", dryRun = true });

        var window = CliRuntime.Window;
        if (window is null)
            return CliResult.Fail("RHI is not running - Luma mode changes live app state. Start RHI and run this again.");

        var cards = window.ViewModel.AllCards;
        var card = cards.FirstOrDefault(c => string.Equals(c.GameName, args[0], StringComparison.OrdinalIgnoreCase))
                   ?? cards.FirstOrDefault(c => c.GameName.Contains(args[0], StringComparison.OrdinalIgnoreCase));

        if (card is null) return CliResult.Fail($"no game matching '{args[0]}' in RHI - run: rhi game list");
        if (card.LumaMod is null) return CliResult.Fail($"'{card.GameName}' has no Luma mod matched - install Luma first: rhi install luma \"{card.GameName}\"");

        var desired = want == "on";
        if (card.IsLumaMode == desired)
            return CliResult.Ok($"Luma mode is already {want} for '{card.GameName}'", new { game = card.GameName, lumaMode = desired, changed = false });

        window.ViewModel.ToggleLumaMode(card);
        var now = card.IsLumaMode;

        return now == desired
            ? CliResult.Ok($"Luma mode is now {(now ? "on" : "off")} for '{card.GameName}'", new { game = card.GameName, lumaMode = now, changed = true })
            : CliResult.Fail($"toggling Luma mode for '{card.GameName}' did not reach the requested state - check the RHI log");
    }
}
