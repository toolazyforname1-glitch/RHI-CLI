using System.Globalization;
using System.Text;
using System.Text.Json;
using RenoDXCommander.Models;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// RTX Encore per-game settings: every key the ⚙ dialog edits, read from and written to the game's
/// rtx-encore.jsonc beside the mod, plus the per-game Neural Rendering DLL flag.
///
/// <c>show</c> prints the install state and the stored value of all 39 settings, <c>set section.key value</c>
/// changes one (the value is the file's own token: "true", "3", "xefg"), and <c>set --nr-dll on|off</c>
/// copies or removes nvngx_dlssnr.dll the way the dialog's "Deploy NR DLL" setting does.
///
/// Writes follow the dialog's rule exactly: boolean tokens go through SetBool, integers through SetInt,
/// everything else through SetString (quoted and escaped), so the file keeps its comments.
/// </summary>
internal static class CliRtxEncore
{
    internal static readonly CliCommand[] All = [Encore()];

    private static CliCommand Encore() => new()
    {
        Name = "rtx encore",
        Group = "rtx",
        Summary = "Install or remove RTX Encore, and read or change one game's settings (rtx-encore.jsonc) and its Neural Rendering DLL.",
        Usage = "rhi rtx encore <game> [show] | rhi rtx encore <game> set <section.key> <value> [--dry-run] [--yes] | " +
                "rhi rtx encore <game> set --nr-dll on|off [--dry-run] [--yes] | " +
                "rhi rtx encore <game> install [--dll <name>] [--dry-run] [--yes] | " +
                "rhi rtx encore <game> remove [--dry-run] [--yes]",
        Examples =
        [
            "rhi rtx encore \"Cyberpunk 2077\"",
            "rhi rtx encore \"Cyberpunk 2077\" install --yes",
            "rhi rtx encore \"Cyberpunk 2077\" install --dll winmm.dll --dry-run",
            "rhi rtx encore \"Cyberpunk 2077\" remove --yes",
            "rhi rtx encore \"Cyberpunk 2077\" set frameGeneration.multiplier 3 --yes",
            "rhi rtx encore \"Cyberpunk 2077\" set neuralRendering/core.nrEnabled on --yes",
            "rhi rtx encore \"Cyberpunk 2077\" set --nr-dll on --yes",
        ],
        RunAsync = ExecuteAsync,
    };

    private enum SettingKind { Bool, Int, String }

    private sealed record Setting(string Section, string Key, SettingKind Kind)
    {
        internal string Path => Section + "." + Key;
    }

    /// <summary>Every setting the ⚙ dialog edits, in the dialog's own order and sections.</summary>
    private static readonly Setting[] Keys =
    [
        // Frame generation (8)
        new Setting("renderingEngines",     "frameGenerationEngine",    SettingKind.String),
        new Setting("frameGeneration",      "mode",                     SettingKind.String),
        new Setting("frameGeneration",      "multiplier",               SettingKind.Int),
        new Setting("dlssPresets",          "dlssFgPreset",             SettingKind.String),
        new Setting("frameGeneration",      "flipMeteringBypass",       SettingKind.Bool),
        new Setting("nativeMenuFeatures",   "reflexFrameLimit",         SettingKind.Int),
        new Setting("frameGeneration",      "dynamicTargetFrameRate",   SettingKind.Int),
        new Setting("frameGeneration",      "dynamicExperimental56",    SettingKind.Bool),

        // XeFG engine (4)
        new Setting("xefg",                 "xefgTensorCores",          SettingKind.Bool),
        new Setting("xefg",                 "xefgMfg4VariablePacing",   SettingKind.Bool),
        new Setting("xefg",                 "xefgUiHandling",           SettingKind.String),
        new Setting("xefg",                 "xefgUiAlpha",              SettingKind.String),

        // Generated frame quality (4)
        new Setting("imageQuality",         "highMultiplierQuality",    SettingKind.Bool),
        new Setting("imageQuality",         "frameProtection",          SettingKind.Bool),
        new Setting("imageQuality",         "fastFrameGeneration",      SettingKind.Bool),
        new Setting("imageQuality",         "protectionTuning",         SettingKind.String),

        // HUD and UI (3)
        new Setting("hudUi",                "autoUiRecomposition",      SettingKind.Bool),
        new Setting("hudUi",                "uiAssist",                 SettingKind.Bool),
        new Setting("hudUi",                "forceUiRecomposition",     SettingKind.Bool),

        // Upscaling (9)
        new Setting("dlssSuperResolution",  "dlssCustomScale",          SettingKind.Int),
        new Setting("dlssSuperResolution",  "dlssRenderScale",          SettingKind.String),
        new Setting("xess",                 "xessTensorCores",          SettingKind.Bool),
        new Setting("renderingEngines",     "reconstructionEngine",     SettingKind.String),
        new Setting("dlssSuperResolution",  "fastSuperResolution",      SettingKind.Bool),
        new Setting("dlssPresets",          "dlssSrPreset",             SettingKind.String),
        new Setting("dlssPresets",          "dlssRrPreset",             SettingKind.String),
        new Setting("superResolutionInputs", "srHdr",                   SettingKind.String),
        new Setting("superResolutionInputs", "srAutoExposure",          SettingKind.String),

        // Smooth Motion (2)
        new Setting("smoothMotion",         "smoothMotionEnabled",      SettingKind.Bool),
        new Setting("smoothMotion",         "smoothMotionApi",          SettingKind.String),

        // Neural Rendering (4) — the nested sections are literal "parent/child" strings
        new Setting("neuralRendering/core",             "nrEnabled",        SettingKind.Bool),
        new Setting("dlssPresets",                      "dlssNrPreset",     SettingKind.String),
        new Setting("neuralRendering/optimizations",    "nrPrecision",      SettingKind.String),
        new Setting("neuralRendering/optimizations",    "nrFastProcessing", SettingKind.Bool),

        // Compatibility (5)
        new Setting("general",              "gpuSeries",                SettingKind.String),
        new Setting("compatibility",        "forceOTA",                 SettingKind.Bool),
        new Setting("compatibility",        "disableInGameMenu",        SettingKind.Bool),
        new Setting("compatibility",        "disableMenuDetection",     SettingKind.Bool),
        new Setting("nativeMenuFeatures",   "hairEnabled",              SettingKind.Bool),
    ];

    private const string SettingsKeyInstalledAs = "RtxEncoreInstalledAs";
    private const string SettingsKeyNrDll = "RtxEncoreNrDllDeployed";

    private const string DefaultProxyName = "dxgi.dll";

    private const string UsageText =
        "usage: rhi rtx encore <game> [show] | rhi rtx encore <game> set <section.key> <value> [--dry-run] [--yes] | " +
        "rhi rtx encore <game> set --nr-dll on|off [--dry-run] [--yes] | " +
        "rhi rtx encore <game> install [--dll <name>] [--dry-run] [--yes] | " +
        "rhi rtx encore <game> remove [--dry-run] [--yes]";

    private static async Task<CliResult> ExecuteAsync(CliInvocation invocation)
    {
        var args = invocation.Args;
        if (args.Length == 0) return CliResult.Usage(UsageText);

        var game = CliGames.ResolveOne(args[0], out var failure);
        if (game is null) return failure!;

        var action = args.Length > 1 ? args[1].ToLowerInvariant() : "show";

        if (action == "show")
        {
            if (args.Length > 2)
                return CliResult.Usage($"'{args[2]}' is not an action - try: rhi rtx encore \"{game.Name}\" [show]");
            return Show(game);
        }

        if (action == "install") return await InstallAsync(invocation, game).ConfigureAwait(false);
        if (action == "remove") return Remove(invocation, game);

        if (action != "set")
            return CliResult.Usage($"unknown action '{action}' - {UsageText}");

        return invocation.Has("nr-dll")
            ? await SetNrDllAsync(invocation, game).ConfigureAwait(false)
            : SetValue(invocation, game);
    }

    // ── show ──────────────────────────────────────────────────────────────────

    private static CliResult Show(SavedGame game)
    {
        var svc         = CliServices.Get<RtxEncoreService>();
        var installPath = game.InstallPath;
        var store       = game.Source ?? "";
        var installedAs = ReadInstalledAs(game.Name, store);
        var nrDeployed  = ReadNrDllFlag(game.Name, store);
        var installed   = svc.IsInstalledIn(installPath, installedAs);
        var modDir      = installedAs is null ? null : svc.ModDir(installPath, installedAs);
        var cfgPath     = installedAs is null || modDir is null ? null : svc.ConfigPath(installPath, installedAs);
        var hasConfig   = cfgPath is not null && File.Exists(cfgPath);

        var text = new StringBuilder();
        void Line(string name, string value) => text.AppendLine(name.PadRight(26) + value);

        Line("installed", installed ? "yes" : "no");
        Line("installed-as", installedAs ?? "(none)");
        Line("mod-folder", modDir ?? "(none)");
        Line("config", cfgPath is null ? "(none)" : hasConfig ? cfgPath! : cfgPath + " (not created yet)");
        Line("nr-dll", nrDeployed ? "deployed" : "not deployed");

        bool? intelComplete = null;
        var intelInGame = 0;
        if (installed && modDir is not null)
        {
            var (complete, inGameFolders) = Rtx40MfgService.GetEncoreIntelState(modDir);
            intelComplete = complete;
            intelInGame   = inGameFolders;
            Line("xess-files", complete
                ? $"installed ({inGameFolders} in-game)"
                : "missing (reinstall RTX Encore)");
        }

        text.AppendLine();
        var settings = new Dictionary<string, string?>(StringComparer.Ordinal);
        foreach (var setting in Keys)
        {
            var value = hasConfig ? RtxEncoreConfig.Get(cfgPath!, setting.Section, setting.Key) : null;
            settings[setting.Path] = value;
            text.AppendLine(setting.Path.PadRight(50) + (value ?? "(default)"));
        }

        return CliResult.Ok(text.ToString().TrimEnd(), new
        {
            game = game.Name,
            installedAs,
            installed,
            modDir,
            configPath = cfgPath,
            nrDll = nrDeployed,
            intelComplete,
            intelInGame,
            settings,
        });
    }

    // ── settings.json: the per-game choice the dialog saves ───────────────────

    /// <summary>Installed proxy/ASI name for this game: "Name|Store" first, then the bare name.</summary>
    private static string? ReadInstalledAs(string gameName, string store)
    {
        var settings = SettingsFile.Load();
        if (!settings.TryGetValue(SettingsKeyInstalledAs, out var raw) || string.IsNullOrWhiteSpace(raw))
            return null;

        Dictionary<string, string>? map;
        try { map = JsonSerializer.Deserialize<Dictionary<string, string>>(raw); }
        catch (JsonException) { return null; }
        if (map is null) return null;

        var key = gameName + "|" + store;
        foreach (var pair in map)
            if (string.Equals(pair.Key, key, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(pair.Value))
                return pair.Value;
        foreach (var pair in map)
            if (string.Equals(pair.Key, gameName, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(pair.Value))
                return pair.Value;
        return null;
    }

    /// <summary>True when the dialog's "Deploy NR DLL" was left on for this game.</summary>
    private static bool ReadNrDllFlag(string gameName, string store)
    {
        var settings = SettingsFile.Load();
        if (!settings.TryGetValue(SettingsKeyNrDll, out var raw) || string.IsNullOrWhiteSpace(raw))
            return false;

        List<string>? list;
        try { list = JsonSerializer.Deserialize<List<string>>(raw); }
        catch (JsonException) { return false; }
        if (list is null) return false;

        var key = gameName + "|" + store;
        return list.Any(k => string.Equals(k, key, StringComparison.OrdinalIgnoreCase)
                          || string.Equals(k, gameName, StringComparison.OrdinalIgnoreCase));
    }

    private static bool WriteNrDllFlag(string gameName, string store, bool deployed)
    {
        var key = gameName + "|" + store;
        return SettingsFile.Update(values =>
        {
            var list = new List<string>();
            if (values.TryGetValue(SettingsKeyNrDll, out var raw) && !string.IsNullOrWhiteSpace(raw))
            {
                try { list = JsonSerializer.Deserialize<List<string>>(raw) ?? new List<string>(); }
                catch (JsonException) { list = new List<string>(); }
            }

            list.RemoveAll(k => string.Equals(k, key, StringComparison.OrdinalIgnoreCase)
                             || string.Equals(k, gameName, StringComparison.OrdinalIgnoreCase));
            if (deployed) list.Add(key);

            if (list.Count > 0) values[SettingsKeyNrDll] = JsonSerializer.Serialize(list);
            else values.Remove(SettingsKeyNrDll);
        }, "cli:rtx encore nr-dll");
    }

    // ── set <section.key> <value> ─────────────────────────────────────────────

    private static CliResult SetValue(CliInvocation invocation, SavedGame game)
    {
        var args = invocation.Args;
        if (args.Length < 4)
            return CliResult.Usage("usage: rhi rtx encore <game> set <section.key> <value> [--dry-run] [--yes]");

        var path = args[2];
        var raw  = args[3];

        // Section and key split on the LAST dot: nested sections are literal "parent/child" strings.
        var split = path.LastIndexOf('.');
        if (split <= 0 || split == path.Length - 1)
            return CliResult.Usage($"'{path}' is not section.key - example: rhi rtx encore \"{game.Name}\" set frameGeneration.multiplier 3 --yes");

        var section = path[..split];
        var key     = path[(split + 1)..];

        var svc         = CliServices.Get<RtxEncoreService>();
        var installedAs = ReadInstalledAs(game.Name, game.Source ?? "");
        if (installedAs is null || !svc.IsInstalledIn(game.InstallPath, installedAs))
            return CliResult.Fail($"RTX Encore is not installed for '{game.Name}' - install it from the game's card first");

        var cfgPath = svc.ConfigPath(game.InstallPath, installedAs);
        var known = Keys.FirstOrDefault(k =>
            string.Equals(k.Section, section, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(k.Key, key, StringComparison.OrdinalIgnoreCase));
        var settingsSection = known?.Section ?? section;
        var settingsKey     = known?.Key ?? key;

        // The value as rtx-encore.jsonc holds it, written the way the dialog writes it.
        string token;
        Action write;
        if (known?.Kind == SettingKind.Bool)
        {
            if (!TryBool(raw, out var flag))
                return CliResult.Usage($"{settingsSection}.{settingsKey} takes on or off (true or false)");
            token = flag ? "true" : "false";
            write = () => RtxEncoreConfig.SetBool(cfgPath, settingsSection, settingsKey, flag);
        }
        else if (known?.Kind == SettingKind.Int)
        {
            if (!int.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out var number))
                return CliResult.Usage($"{settingsSection}.{settingsKey} takes a whole number");
            token = number.ToString(CultureInfo.InvariantCulture);
            write = () => RtxEncoreConfig.SetInt(cfgPath, settingsSection, settingsKey, number);
        }
        else if (known?.Kind == SettingKind.String)
        {
            token = raw;
            write = () => RtxEncoreConfig.SetString(cfgPath, settingsSection, settingsKey, raw);
        }
        else
        {
            // Unknown key: allowed (the file is user-editable), written with the dialog's own rule.
            var isFlag   = raw is "true" or "false";
            var isNumber = int.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out var number);
            token = raw;
            write = isFlag
                ? () => RtxEncoreConfig.SetBool(cfgPath, settingsSection, settingsKey, raw == "true")
                : isNumber
                    ? () => RtxEncoreConfig.SetInt(cfgPath, settingsSection, settingsKey, number)
                    : () => RtxEncoreConfig.SetString(cfgPath, settingsSection, settingsKey, raw);
        }

        var warning  = known is null ? UnknownKeyWarning(section, key) : null;
        var previous = RtxEncoreConfig.Get(cfgPath, settingsSection, settingsKey);
        var before   = previous ?? "(default)";

        if (invocation.DryRun)
            return CliResult.Ok(
                Prefixed(warning, $"would set {settingsSection}.{settingsKey} to {token} (was {before})"),
                new { game = game.Name, setting = $"{settingsSection}.{settingsKey}", value = token, previous, warning, dryRun = true });

        if (!invocation.Confirmed)
            return CliResult.Usage(
                $"refusing to write {settingsSection}.{settingsKey} without --yes: rhi rtx encore \"{game.Name}\" set {path} {token} --yes");

        try { write(); }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or InvalidDataException)
        {
            return CliResult.Fail($"couldn't write {settingsSection}.{settingsKey}: {ex.Message}");
        }

        return CliResult.Ok(
            Prefixed(warning, $"{settingsSection}.{settingsKey} = {token} (was {before})"),
            new { game = game.Name, setting = $"{settingsSection}.{settingsKey}", value = token, previous, warning, changed = true });
    }

    // ── set --nr-dll on|off ───────────────────────────────────────────────────

    private static async Task<CliResult> SetNrDllAsync(CliInvocation invocation, SavedGame game)
    {
        var raw = invocation.Flag("nr-dll");
        if (string.IsNullOrWhiteSpace(raw))
            return CliResult.Usage("--nr-dll takes on or off: rhi rtx encore <game> set --nr-dll on --yes");
        if (!TryBool(raw, out var enable))
            return CliResult.Usage("--nr-dll takes on or off");

        var state       = enable ? "on" : "off";
        var svc         = CliServices.Get<RtxEncoreService>();
        var installPath = game.InstallPath;
        var store       = game.Source ?? "";
        var installedAs = ReadInstalledAs(game.Name, store);
        var installed   = svc.IsInstalledIn(installPath, installedAs);

        if (invocation.DryRun)
            return CliResult.Ok(
                $"would set the NR DLL for '{game.Name}' to {state}"
                + (installed ? "" : " (saved now, deployed when RTX Encore is installed)"),
                new { game = game.Name, nrDll = enable, installed, dryRun = true });

        if (!invocation.Confirmed)
            return CliResult.Usage(
                $"refusing to change the NR DLL without --yes: rhi rtx encore \"{game.Name}\" set --nr-dll {state} --yes");

        // Saved first, as the dialog does: the choice applies when RTX Encore is installed even if it isn't yet.
        if (!WriteNrDllFlag(game.Name, store, enable))
            return CliResult.Fail("couldn't save the NR DLL choice - settings.json is unreadable or locked");

        if (!installed || installedAs is null)
            return CliResult.Ok(
                $"saved: RTX Encore will {(enable ? "deploy" : "not deploy")} nvngx_dlssnr.dll "
                + $"{RtxEncoreService.NrRequiredVer} when it's installed for '{game.Name}'",
                new { game = game.Name, nrDll = enable, installed = false, changed = true });

        if (!enable)
        {
            svc.RemoveNrDll(installPath, installedAs);
            return CliResult.Ok(
                $"removed nvngx_dlssnr.dll from '{game.Name}'",
                new { game = game.Name, nrDll = false, installed = true, changed = true });
        }

        var cached = await svc.EnsureNrDllCachedAsync(CliServices.Get<IDlssStreamlineService>()).ConfigureAwait(false);
        if (cached is null)
        {
            WriteNrDllFlag(game.Name, store, false);
            return CliResult.Fail(
                $"couldn't download nvngx_dlssnr.dll {RtxEncoreService.NrRequiredVer} - nothing was deployed");
        }

        if (!svc.DeployNrDll(installPath, installedAs))
        {
            WriteNrDllFlag(game.Name, store, false);
            return CliResult.Fail("couldn't copy nvngx_dlssnr.dll beside RTX Encore - is the game folder writable?");
        }

        return CliResult.Ok(
            $"deployed nvngx_dlssnr.dll {RtxEncoreService.NrRequiredVer} beside RTX Encore for '{game.Name}'",
            new
            {
                game = game.Name,
                nrDll = true,
                installed = true,
                path = svc.NrRelPath(installPath, installedAs),
                changed = true,
            });
    }

    // ── install [--dll <name>] ────────────────────────────────────────────────

    private static async Task<CliResult> InstallAsync(CliInvocation invocation, SavedGame game)
    {
        if (invocation.Args.Length > 2)
            return CliResult.Usage($"install takes no positional arguments - use --dll <name>: rhi rtx encore \"{game.Name}\" install --dll version.dll --yes");

        var installPath = game.InstallPath;
        if (string.IsNullOrWhiteSpace(installPath))
            return CliResult.Fail($"'{game.Name}' has no install folder in the library - add the game again, then install");

        var svc   = CliServices.Get<RtxEncoreService>();
        var store = game.Source ?? "";

        var requested = invocation.Flag("dll");
        if (invocation.Has("dll") && string.IsNullOrWhiteSpace(requested))
            return CliResult.Usage("--dll takes a proxy file name, e.g. --dll version.dll");

        var installedAs = ReadInstalledAs(game.Name, store);
        var chosen      = !string.IsNullOrWhiteSpace(requested) ? requested!.Trim() : installedAs ?? DefaultProxyName;

        var nrDeployed = ReadNrDllFlag(game.Name, store);
        var oldModDir  = installedAs is null ? null : svc.ModDir(installPath, installedAs);
        var newModDir  = svc.ModDir(installPath, chosen);
        var replacing  = installedAs is not null && !string.Equals(installedAs, chosen, StringComparison.OrdinalIgnoreCase);

        if (invocation.DryRun)
            return CliResult.Ok(
                $"would install RTX Encore as '{chosen}' in {installPath}"
                + (replacing ? $" (replacing '{installedAs}')" : "")
                + (nrDeployed ? ", deploy nvngx_dlssnr.dll" : "")
                + ", then the Intel XeSS files",
                new { game = game.Name, dll = chosen, previous = installedAs, modDir = newModDir, nrDll = nrDeployed, xessFiles = true, dryRun = true });

        if (!invocation.Confirmed)
            return CliResult.Usage($"refusing to install RTX Encore without --yes: rhi rtx encore \"{game.Name}\" install --dll {chosen} --yes");

        if (!await svc.InstallAsync(installPath, chosen, existingInstalledAs: installedAs, progress: null).ConfigureAwait(false))
            return CliResult.Fail("RTX Encore install returned false - see the log");

        if (!WriteInstalledAs(game.Name, store, chosen))
            return CliResult.Fail(
                $"installed as '{chosen}' in {newModDir}, but couldn't save the choice to settings.json - check the file, then run: rhi rtx encore \"{game.Name}\" show");

        var nrNote = "";
        if (nrDeployed)
        {
            var cached = await svc.EnsureNrDllCachedAsync(CliServices.Get<IDlssStreamlineService>()).ConfigureAwait(false);
            if (cached is null)
                nrNote = $" (nvngx_dlssnr.dll {RtxEncoreService.NrRequiredVer} couldn't be downloaded, so it wasn't deployed)";
            else if (!svc.DeployNrDll(installPath, chosen))
                nrNote = " (nvngx_dlssnr.dll couldn't be copied beside RTX Encore)";
        }

        var mfg        = CliServices.Get<Rtx40MfgService>();
        var xessStaged = await mfg.EnsureEncoreIntelStagingAsync().ConfigureAwait(false);
        var xessOk     = xessStaged && DeployIntelFiles(mfg, installPath, oldModDir, newModDir);

        var version = svc.StagedVersion;
        var message = $"installed RTX Encore{(version is null ? "" : " " + version)} as '{chosen}' in {newModDir}";
        if (!xessOk)
            message += " (the Intel XeSS files for the XeSS upscaler and XeFG weren't installed - reinstall to add them)";
        message += nrNote;

        return CliResult.Ok(message, new
        {
            game = game.Name,
            dll = chosen,
            version,
            modDir = newModDir,
            nrDll = nrDeployed && nrNote.Length == 0,
            xessFiles = xessOk,
            changed = true,
        });
    }

    private static bool DeployIntelFiles(Rtx40MfgService mfg, string installPath, string? oldModDir, string newModDir)
    {
        if (oldModDir is not null && !string.Equals(oldModDir, newModDir, StringComparison.OrdinalIgnoreCase))
            mfg.RemoveEncoreIntelFiles(installPath, oldModDir);
        return mfg.DeployEncoreIntelFiles(installPath, newModDir);
    }

    // ── remove ────────────────────────────────────────────────────────────────

    private static CliResult Remove(CliInvocation invocation, SavedGame game)
    {
        if (invocation.Args.Length > 2)
            return CliResult.Usage($"remove takes no arguments: rhi rtx encore \"{game.Name}\" remove --yes");

        var installPath = game.InstallPath;
        var store       = game.Source ?? "";
        var installedAs = ReadInstalledAs(game.Name, store);
        if (installedAs is null)
            return CliResult.Fail($"RTX Encore is not installed for '{game.Name}' - nothing to remove");

        var svc        = CliServices.Get<RtxEncoreService>();
        var nrDeployed = ReadNrDllFlag(game.Name, store);
        var modDir     = svc.ModDir(installPath, installedAs);

        if (invocation.DryRun)
            return CliResult.Ok(
                $"would remove '{installedAs}'" + (nrDeployed ? " and nvngx_dlssnr.dll" : "") + $" and the Intel XeSS files from {installPath}",
                new { game = game.Name, dll = installedAs, nrDll = nrDeployed, modDir, dryRun = true });

        if (!invocation.Confirmed)
            return CliResult.Usage($"refusing to remove RTX Encore without --yes: rhi rtx encore \"{game.Name}\" remove --yes");

        svc.Uninstall(installPath, installedAs, removeNrDll: nrDeployed);
        CliServices.Get<Rtx40MfgService>().RemoveEncoreIntelFiles(installPath, modDir);
        WriteInstalledAs(game.Name, store, null);
        if (nrDeployed) WriteNrDllFlag(game.Name, store, false);

        return CliResult.Ok(
            $"removed RTX Encore ('{installedAs}') from '{game.Name}'",
            new { game = game.Name, removed = installedAs, nrDll = nrDeployed, cleanedModDir = modDir, changed = true });
    }

    private static bool WriteInstalledAs(string gameName, string store, string? dllName)
    {
        var key = gameName + "|" + store;
        return SettingsFile.Update(values =>
        {
            var map = new Dictionary<string, string>();
            if (values.TryGetValue(SettingsKeyInstalledAs, out var raw) && !string.IsNullOrWhiteSpace(raw))
            {
                try { map = JsonSerializer.Deserialize<Dictionary<string, string>>(raw) ?? new Dictionary<string, string>(); }
                catch (JsonException) { map = new Dictionary<string, string>(); }
            }

            if (string.IsNullOrEmpty(dllName))
            {
                map.Remove(key);
                map.Remove(gameName);
            }
            else
            {
                map[key] = dllName;
            }

            if (map.Count > 0) values[SettingsKeyInstalledAs] = JsonSerializer.Serialize(map);
            else values.Remove(SettingsKeyInstalledAs);
        }, "cli:rtx encore installed-as");
    }

    // ── helpers ───────────────────────────────────────────────────────────────

    private static bool TryBool(string raw, out bool value)
    {
        if (raw.Equals("true", StringComparison.OrdinalIgnoreCase) || raw.Equals("on", StringComparison.OrdinalIgnoreCase) || raw == "1")
        {
            value = true;
            return true;
        }
        if (raw.Equals("false", StringComparison.OrdinalIgnoreCase) || raw.Equals("off", StringComparison.OrdinalIgnoreCase) || raw == "0")
        {
            value = false;
            return true;
        }
        value = false;
        return false;
    }

    private static string Prefixed(string? warning, string message) =>
        warning is null ? message : warning + Environment.NewLine + message;

    private static string UnknownKeyWarning(string section, string key)
    {
        var typed = (section + "." + key).ToLowerInvariant();
        var closest = Keys
            .Select(k => (k.Path, Distance: Distance(k.Path.ToLowerInvariant(), typed)))
            .OrderBy(c => c.Distance)
            .First();

        return $"warning: '{section}.{key}' is not one of the settings the RTX Encore dialog edits - " +
               $"closest known key is '{closest.Path}'. Writing it anyway (rtx-encore.jsonc is user-editable).";
    }

    private static int Distance(string a, string b)
    {
        var previous = new int[b.Length + 1];
        var current  = new int[b.Length + 1];
        for (var j = 0; j <= b.Length; j++) previous[j] = j;

        for (var i = 1; i <= a.Length; i++)
        {
            current[0] = i;
            for (var j = 1; j <= b.Length; j++)
            {
                var cost = a[i - 1] == b[j - 1] ? 0 : 1;
                current[j] = Math.Min(Math.Min(current[j - 1] + 1, previous[j] + 1), previous[j - 1] + cost);
            }
            (previous, current) = (current, previous);
        }

        return previous[b.Length];
    }
}
