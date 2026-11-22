using System.Text.Json;
using RenoDXCommander.Models;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>DXVK and OptiScaler install/uninstall, driven through their normal card-based entry points.</summary>
internal static class CliScalers
{
    internal static readonly CliCommand[] All = [InstallDxvk(), UninstallDxvk(), InstallOpti(), UninstallOpti()];

    private static CliCommand InstallDxvk() => new()
    {
        Name = "install dxvk",
        Group = "install",
        Summary = "Install DXVK into a game folder using the configured variant.",
        Usage = "rhi install dxvk <game> [--dry-run]",
        Examples = ["rhi install dxvk \"Cyberpunk 2077\" --dry-run"],
        Destructive = true,
        Run = invocation => DxvkAsync(invocation, install: true).GetAwaiter().GetResult(),
    };

    private static CliCommand UninstallDxvk() => new()
    {
        Name = "uninstall dxvk",
        Group = "install",
        Summary = "Remove DXVK and restore the backed-up DLLs. Needs --yes.",
        Usage = "rhi uninstall dxvk <game> [--yes] [--dry-run]",
        Destructive = true,
        Run = invocation => DxvkAsync(invocation, install: false).GetAwaiter().GetResult(),
    };

    private static CliCommand InstallOpti() => new()
    {
        Name = "install optiscaler",
        Group = "install",
        Summary = "Install OptiScaler (upscaler bridge) into a game folder.",
        Usage = "rhi install optiscaler <game> [--variant Stable|Nightly] [--gpu NVIDIA|AMD] [--dry-run]",
        Destructive = true,
        Run = invocation => OptiAsync(invocation, install: true).GetAwaiter().GetResult(),
    };

    private static CliCommand UninstallOpti() => new()
    {
        Name = "uninstall optiscaler",
        Group = "install",
        Summary = "Remove OptiScaler from a game folder. Needs --yes.",
        Usage = "rhi uninstall optiscaler <game> [--yes] [--dry-run]",
        Destructive = true,
        Run = invocation => OptiAsync(invocation, install: false).GetAwaiter().GetResult(),
    };

    private static async Task<CliResult> DxvkAsync(CliInvocation invocation, bool install)
    {
        var game = CliGames.ResolveOne(invocation, out var failure);
        if (game is null) return failure!;

        var service = CliServices.Get<IDxvkService>();
        var settings = SettingsFile.Load();

        if (install)
        {
            var variantText = Value(settings, "DxvkVariant") ?? "Stable";
            if (!Enum.TryParse<DxvkVariant>(variantText, ignoreCase: true, out var variant)) variant = DxvkVariant.Stable;

            if (invocation.DryRun)
                return CliResult.Ok($"would install DXVK ({variant}) for '{game.Name}' into {game.InstallPath}", new { game = game.Name, path = game.InstallPath, variant = variant.ToString(), dryRun = true });

            var card = CliCards.Build(game, out var cardError);
            if (card is null) return CliResult.Fail(cardError!);

            service.SelectedVariant = variant;
            await service.InstallAsync(card, progress: null, screenshotSavePath: null,
                overlayHotkey: Value(settings, "OverlayHotkey"), screenshotHotkey: Value(settings, "ScreenshotHotkey"));
            return CliResult.Ok($"DXVK ({variant}) installed for '{game.Name}' ({CliCards.Describe(card)})", new { game = game.Name, variant = variant.ToString(), card = CliCards.Describe(card) });
        }

        if (service.FindRecord(game.Name, game.InstallPath) is null) return CliResult.Fail($"DXVK is not installed for '{game.Name}'");
        if (invocation.DryRun) return CliResult.Ok($"would remove DXVK from {game.InstallPath}", new { game = game.Name, dryRun = true });
        if (!invocation.Confirmed) return CliResult.Usage($"refusing to remove without --yes: rhi uninstall dxvk \"{game.Name}\" --yes");

        var uninstallCard = CliCards.Build(game, out var uninstallError);
        if (uninstallCard is null) return CliResult.Fail(uninstallError!);
        await service.UninstallAsync(uninstallCard);
        return CliResult.Ok($"DXVK removed from '{game.Name}'", new { game = game.Name, removed = true });
    }

    private static async Task<CliResult> OptiAsync(CliInvocation invocation, bool install)
    {
        var game = CliGames.ResolveOne(invocation, out var failure);
        if (game is null) return failure!;

        var service = CliServices.Get<IOptiScalerService>();
        var installer = CliServices.Get<IAuxInstallService>();
        var settings = SettingsFile.Load();

        if (install)
        {
            var variant = invocation.Flag("variant") ?? Value(settings, "OsDefaultVariant") ?? "Stable";
            var gpu = invocation.Flag("gpu") ?? Value(settings, "OsGpuType") ?? "NVIDIA";
            var dlssInputs = !string.Equals(Value(settings, "OsDlssInputs"), "false", StringComparison.OrdinalIgnoreCase);
            var hotkey = Value(settings, "OsHotkey");

            if (invocation.DryRun)
                return CliResult.Ok($"would install OptiScaler ({variant}, GPU {gpu}, DLSS inputs {dlssInputs}) for '{game.Name}'", new { game = game.Name, path = game.InstallPath, variant, gpu, dlssInputs, hotkey, dryRun = true });

            var card = CliCards.Build(game, out var cardError);
            if (card is null) return CliResult.Fail(cardError!);

            var record = await service.InstallAsync(card, progress: null, gpuType: gpu, dlssInputs: dlssInputs, hotkey: hotkey, variant: variant);
            return record is null
                ? CliResult.Fail($"OptiScaler install returned nothing for '{game.Name}' - see the log")
                : CliResult.Ok($"OptiScaler installed for '{game.Name}': {record.InstalledAs}", new { game = game.Name, record.InstalledAs, record.Channel });
        }

        if (installer.FindRecord(game.Name, game.InstallPath, OptiScalerService.AddonType) is null
            && !File.Exists(Path.Combine(game.InstallPath, "OptiScaler.ini"))
            && !File.Exists(Path.Combine(game.InstallPath, "OptiScaler", "OptiScaler.dll")))
            return CliResult.Fail($"OptiScaler is not installed for '{game.Name}'");

        var store = game.Source ?? "";
        var key = $"{game.Name}|{store}";
        var manifest = CliServices.Get<IManifestService>().LoadCached();
        var pdUpscaler = manifest?.PdUpscalerGames?.ContainsKey(game.Name) == true;
        var engineKeys = EngineIniKeysFor(settings, key, game.Name);

        if (invocation.DryRun)
            return CliResult.Ok(
                $"would {(pdUpscaler ? "restore the standard REFramework, then " : "")}remove OptiScaler from {game.InstallPath}"
                + $"{(engineKeys.Count > 0 ? $", clean {engineKeys.Count} Engine.ini key(s)" : "")}"
                + $", and clear the per-game OptiScaler settings",
                new { game = game.Name, path = game.InstallPath, pdUpscaler, engineIniKeys = engineKeys, dryRun = true });

        if (!invocation.Confirmed)
            return CliResult.Usage($"refusing to remove without --yes: rhi uninstall optiscaler \"{game.Name}\" --yes");

        var uninstallCard = CliCards.Build(game, out var uninstallError);
        if (uninstallCard is null) return CliResult.Fail(uninstallError!);

        if (pdUpscaler)
        {
            try
            {
                CliServices.Get<IREFrameworkService>().RestoreStandardREFramework(game.Name, game.InstallPath);
            }
            catch (Exception ex)
            {
                return CliResult.Fail($"could not restore the standard REFramework for '{game.Name}': {ex.Message}");
            }
        }

        // Mirrors the GUI's BeforeOptiScalerFolderRemovedHook: move the MFG Unlocker out before deletion.
        if (Directory.Exists(Path.Combine(game.InstallPath, "OptiScaler", "plugins")))
        {
            try { CliServices.Get<Rtx40MfgService>().MoveOutOfOptiScalerPlugin(game.InstallPath); }
            catch (Exception ex) { CrashReporter.Log($"[uninstall optiscaler] MFG move-out failed: {ex.Message}"); }
        }

        service.Uninstall(uninstallCard);

        if (File.Exists(Path.Combine(game.InstallPath, "OptiScaler.ini"))
            || Directory.Exists(Path.Combine(game.InstallPath, "OptiScaler")))
            return CliResult.Fail($"OptiScaler files are still present in {game.InstallPath} - see the crash log");

        if (engineKeys.Count > 0)
            AuxInstallService.RemoveEngineIniCustomKeys(game.InstallPath, engineKeys, null, game.Name, store);

        ClearOsSettings(key, game.Name);

        return CliResult.Ok(
            $"OptiScaler removed from '{game.Name}'"
            + (pdUpscaler ? ", standard REFramework restored" : "")
            + (engineKeys.Count > 0 ? $", {engineKeys.Count} Engine.ini key(s) cleaned" : "")
            + ", per-game OptiScaler settings cleared",
            new { game = game.Name, removed = true, pdUpscaler, engineIniKeys = engineKeys, settingsCleared = true });
    }

    private static bool OsFlag(Dictionary<string, string> settings, string settingKey, string key, string gameName)
        => ArrayContains(settings, settingKey, key) || ArrayContains(settings, settingKey, gameName);

    private static string? OsValue(Dictionary<string, string> settings, string settingKey, string key, string gameName)
        => ObjectValue(settings, settingKey, key) ?? ObjectValue(settings, settingKey, gameName);

    private static List<string> EngineIniKeysFor(Dictionary<string, string> settings, string key, string gameName)
    {
        var keys = new List<string>();

        if (OsFlag(settings, "OsDilatedMotionVectorsOff", key, gameName))
        {
            keys.Add("r.NGX.DLSS.DilateMotionVectors");
            keys.Add("r.Streamline.DilateMotionVectors");
        }

        var fsrFix = OsValue(settings, "OsFsrCrashFix", key, gameName);
        if (!string.IsNullOrEmpty(fsrFix) && !string.Equals(fsrFix, "None", StringComparison.OrdinalIgnoreCase))
        {
            keys.Add("r.FidelityFX.FSR2.UseNativeDX12");
            keys.Add("r.FidelityFX.FSR3.UseNativeDX12");
            keys.Add("r.FidelityFX.FSR3.UseRHI");
        }

        if (OsFlag(settings, "OsFsrFgSwapchain", key, gameName))
            keys.Add("r.FidelityFX.FI.OverrideSwapChainDX12");

        if (OsFlag(settings, "OsUpscalerPlugin", key, gameName))
        {
            keys.Add("r.AntiAliasingMethod");
            keys.Add("r.TemporalAA.Upscaler");
        }

        return keys;
    }

    private static bool ArrayContains(Dictionary<string, string> settings, string settingKey, string value)
    {
        if (!settings.TryGetValue(settingKey, out var raw) || string.IsNullOrWhiteSpace(raw)) return false;

        try
        {
            var values = JsonSerializer.Deserialize<string[]>(raw);
            return values is not null && values.Any(v => string.Equals(v, value, StringComparison.OrdinalIgnoreCase));
        }
        catch (JsonException) { return false; }
    }

    private static string? ObjectValue(Dictionary<string, string> settings, string settingKey, string value)
    {
        if (!settings.TryGetValue(settingKey, out var raw) || string.IsNullOrWhiteSpace(raw)) return null;

        try
        {
            var map = JsonSerializer.Deserialize<Dictionary<string, string?>>(raw);
            return map is not null && map.TryGetValue(value, out var found) ? found : null;
        }
        catch (JsonException) { return null; }
    }

    private static void ClearOsSettings(string key, string gameName)
    {
        SettingsFile.Update(settings =>
        {
            RemoveArrayEntry(settings, "OsDeployStreamline", key, gameName);
            RemoveArrayEntry(settings, "OsDeployDlssEnabler", key, gameName);
            RemoveObjectEntry(settings, "OsFgInput", key, gameName);
            RemoveObjectEntry(settings, "OsIniEdits", key, gameName);
            RemoveObjectEntry(settings, "OsIniProfile", key, gameName);
            RemoveObjectEntry(settings, "OsFgOutput", key, gameName);
            RemoveObjectEntry(settings, "OsFgNvngxReplacement", key, gameName);
            RemoveArrayEntry(settings, "OsDilatedMotionVectorsOff", key, gameName);
            RemoveObjectEntry(settings, "OsFsrCrashFix", key, gameName);
            RemoveArrayEntry(settings, "OsFsrFgSwapchain", key, gameName);
            RemoveArrayEntry(settings, "OsUpscalerPlugin", key, gameName);
            RemoveObjectEntry(settings, "OsStreamlineVersion", key, gameName);
        }, "cli:uninstall optiscaler");
    }

    private static void RemoveArrayEntry(Dictionary<string, string> settings, string settingKey, string key, string gameName)
    {
        if (!settings.TryGetValue(settingKey, out var raw) || string.IsNullOrWhiteSpace(raw)) return;

        try
        {
            var values = JsonSerializer.Deserialize<string[]>(raw);
            if (values is null) return;

            settings[settingKey] = JsonSerializer.Serialize(values
                .Where(v => !string.Equals(v, key, StringComparison.OrdinalIgnoreCase)
                         && !string.Equals(v, gameName, StringComparison.OrdinalIgnoreCase))
                .ToArray());
        }
        catch (JsonException) { return; }
    }

    private static void RemoveObjectEntry(Dictionary<string, string> settings, string settingKey, string key, string gameName)
    {
        if (!settings.TryGetValue(settingKey, out var raw) || string.IsNullOrWhiteSpace(raw)) return;

        try
        {
            var map = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(raw);
            if (map is null) return;

            map.Remove(key);
            map.Remove(gameName);
            settings[settingKey] = JsonSerializer.Serialize(map);
        }
        catch (JsonException) { return; }
    }

    private static string? Value(Dictionary<string, string> settings, string key) =>
        settings.TryGetValue(key, out var value) && !string.IsNullOrWhiteSpace(value) ? value : null;
}
