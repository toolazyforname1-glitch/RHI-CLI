using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// OptiScaler's add-ons that are plain service calls: the Streamline side-copy (the GUI does it as
/// part of the OptiScaler install), DLSS Enabler, and the PD-Upscaler REFramework swap.
/// </summary>
internal static class CliOptiscaler
{
    internal static readonly CliCommand[] All = [Streamline(), DlssEnabler(), PdUpscaler(), Fg(), DeployFlag(), Ini()];

    private static CliCommand Streamline() => new()
    {
        Name = "optiscaler streamline",
        Group = "install",
        Summary = "Copy the Streamline DLLs into a game's OptiScaler folder, or remove them.",
        Usage = "rhi optiscaler streamline <game> [--version <v>] [--remove] [--dry-run] [--yes]",
        Examples = ["rhi optiscaler streamline cyberpunk --yes", "rhi optiscaler streamline cyberpunk --remove --yes"],
        Destructive = true,
        Run = Run,
    };

    private static CliResult Run(CliInvocation invocation)
    {
        var game = CliGames.ResolveOne(invocation, out var failure);
        if (game is null) return failure!;

        var remove = invocation.Has("remove");
        var version = invocation.Flag("version");

        if (invocation.DryRun)
            return remove
                ? CliResult.Ok($"would remove OptiScaler\\Streamline from {game.InstallPath}", new { game = game.Name, remove = true, dryRun = true })
                : CliResult.Ok($"would deploy Streamline{(version is null ? "" : " " + version)} into {game.InstallPath}",
                    new { game = game.Name, version, dryRun = true });

        var service = CliServices.Get<IOptiScalerService>();

        try
        {
            if (remove) service.RemoveStreamlineFromGame(game.InstallPath);
            else service.DeployStreamlineToGame(game.InstallPath, version);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or InvalidOperationException)
        {
            return CliResult.Fail($"{(remove ? "remove" : "deploy")} failed for '{game.Name}': {ex.Message}");
        }

        return remove
            ? CliResult.Ok($"removed Streamline from '{game.Name}'", new { game = game.Name, removed = true })
            : CliResult.Ok($"deployed Streamline{(version is null ? "" : " " + version)} into '{game.Name}'",
                new { game = game.Name, version, deployed = true });
    }

    private static CliCommand DlssEnabler() => new()
    {
        Name = "optiscaler dlss-enabler",
        Group = "install",
        Summary = "Deploy the DLSS Enabler headless DLL into a game's OptiScaler folder, or remove it.",
        Usage = "rhi optiscaler dlss-enabler <game> [--remove] [--dry-run] [--yes]",
        Examples = ["rhi optiscaler dlss-enabler cyberpunk --yes", "rhi optiscaler dlss-enabler cyberpunk --remove --yes"],
        Destructive = true,
        Run = invocation => DlssEnablerRun(invocation).GetAwaiter().GetResult(),
    };

    private static async Task<CliResult> DlssEnablerRun(CliInvocation invocation)
    {
        var game = CliGames.ResolveOne(invocation, out var failure);
        if (game is null) return failure!;

        var remove = invocation.Has("remove");
        var folder = Path.Combine(game.InstallPath, "OptiScaler");
        var service = CliServices.Get<DlssEnablerService>();
        var installed = service.IsInstalledIn(folder);

        if (invocation.DryRun)
            return CliResult.Ok(
                remove ? $"would remove DLSS Enabler from {folder}" : $"would deploy DLSS Enabler into {folder}",
                new { game = game.Name, path = folder, installed, dryRun = true });

        if (!invocation.Confirmed)
            return CliResult.Usage($"refusing to change {folder} without --yes: rhi optiscaler dlss-enabler {game.Name}{(remove ? " --remove" : "")} --yes");

        if (remove)
        {
            service.Uninstall(folder);
            return service.IsInstalledIn(folder)
                ? CliResult.Fail($"could not remove dlss-enabler-headless.dll from {folder}")
                : CliResult.Ok($"removed DLSS Enabler from '{game.Name}'", new { game = game.Name, removed = true });
        }

        await service.InstallAsync(folder);

        if (!service.IsInstalledIn(folder))
            return CliResult.Fail(service.IsStagingReady
                ? $"DLSS Enabler staging is ready but the copy into {folder} failed"
                : "DLSS Enabler is not staged - the download failed (network or GitHub rate limit)");

        return CliResult.Ok($"deployed DLSS Enabler into '{game.Name}'",
            new { game = game.Name, deployed = true, version = service.StagedVersion, path = folder });
    }

    private static CliCommand PdUpscaler() => new()
    {
        Name = "optiscaler pdupscaler",
        Group = "install",
        Summary = "Replace a RE Engine game's REFramework with PD-Upscaler (only games the manifest lists).",
        Usage = "rhi optiscaler pdupscaler <game> [--dry-run] [--yes]",
        Examples = ["rhi optiscaler pdupscaler pragmata --yes"],
        Destructive = true,
        Run = invocation => PdUpscalerRun(invocation).GetAwaiter().GetResult(),
    };

    private static async Task<CliResult> PdUpscalerRun(CliInvocation invocation)
    {
        var game = CliGames.ResolveOne(invocation, out var failure);
        if (game is null) return failure!;

        var manifestService = CliServices.Get<IManifestService>();
        var manifest = manifestService.LoadCached();

        if (manifest?.PdUpscalerGames is null || !manifest.PdUpscalerGames.ContainsKey(game.Name))
        {
            try { manifest = await manifestService.FetchAsync() ?? manifest; }
            catch (Exception) { /* the cached manifest is still usable */ }
        }

        if (manifest?.PdUpscalerGames is null
            || !manifest.PdUpscalerGames.TryGetValue(game.Name, out var artifact)
            || string.IsNullOrWhiteSpace(artifact))
        {
            return CliResult.Fail($"'{game.Name}' is not a PD-Upscaler game in the RHI manifest");
        }

        var dinput = Path.Combine(game.InstallPath, "dinput8.dll");
        if (!File.Exists(dinput))
            return CliResult.Fail($"REFramework is not installed in '{game.Name}' (no dinput8.dll) - PD-Upscaler replaces it");

        if (invocation.DryRun)
            return CliResult.Ok($"would install PD-Upscaler REFramework ({artifact}) into {game.InstallPath}",
                new { game = game.Name, artifact, dryRun = true });

        if (!invocation.Confirmed)
            return CliResult.Usage($"refusing to replace dinput8.dll without --yes: rhi optiscaler pdupscaler {game.Name} --yes");

        var service = CliServices.Get<IREFrameworkService>();

        try
        {
            await service.InstallPdUpscalerAsync(game.Name, game.InstallPath, artifact);
        }
        catch (Exception ex)
        {
            return CliResult.Fail($"PD-Upscaler install failed for '{game.Name}': {ex.Message}");
        }

        return CliResult.Ok($"installed PD-Upscaler REFramework into '{game.Name}'",
            new { game = game.Name, artifact, installed = true });
    }

    private static CliCommand Fg() => new()
    {
        Name = "optiscaler fg",
        Group = "config",
        Summary = "Show or set a game's frame-generation input/output/Nvngx replacement and apply it to OptiScaler.ini.",
        Usage = "rhi optiscaler fg <game> [<input> [<output> [<nvngx>]]] [--dry-run] [--yes]",
        Examples = ["rhi optiscaler fg cyberpunk", "rhi optiscaler fg cyberpunk dlssg dlssg auto --yes"],
        Run = FgRun,
    };

    private static readonly string[] FgValues = ["auto", "upscaler", "dlssg", "nvngxfg", "fsrfg", "fsrfg30", "xefg"];

    private static CliResult FgRun(CliInvocation invocation)
    {
        var game = CliGames.ResolveOne(invocation.Operand(0) ?? "", out var failure);
        if (game is null) return failure!;

        var key = $"{game.Name}|{game.Source ?? ""}";
        var settings = SettingsFile.Load();
        var input = MapValue(settings, "OsFgInput", key, game.Name) ?? "auto";
        var output = MapValue(settings, "OsFgOutput", key, game.Name) ?? "auto";
        var nvngx = MapValue(settings, "OsFgNvngxReplacement", key, game.Name) ?? "auto";
        var ini = Path.Combine(game.InstallPath, "OptiScaler.ini");

        if (invocation.Args.Length <= 1)
            return CliResult.Ok(
                $"frame generation for '{game.Name}': input {input}, output {output}, nvngx {nvngx}; OptiScaler.ini {(File.Exists(ini) ? "present" : "missing")}",
                new { game = game.Name, input, output, nvngx, ini, installed = File.Exists(ini) });

        var values = invocation.Args[1..];
        if (values.Length > 3) return CliResult.Usage("usage: rhi optiscaler fg <game> [<input> [<output> [<nvngx>]]]");

        if (values.Length > 0) input = values[0];
        if (values.Length > 1) output = values[1];
        if (values.Length > 2) nvngx = values[2];

        foreach (var (label, value) in new[] { ("input", input), ("output", output), ("nvngx", nvngx) })
            if (!FgValues.Contains(value, StringComparer.OrdinalIgnoreCase))
                return CliResult.Usage($"unknown frame-generation {label} '{value}' - one of: {string.Join(", ", FgValues)}");

        if (!File.Exists(ini))
            return CliResult.Fail($"OptiScaler.ini is not in {game.InstallPath} - install OptiScaler first");

        if (invocation.DryRun)
            return CliResult.Ok($"would set input {input}, output {output}, nvngx {nvngx} in {ini}",
                new { game = game.Name, input, output, nvngx, dryRun = true });

        if (!invocation.Confirmed)
            return CliResult.Usage($"refusing to write OptiScaler.ini without --yes: rhi optiscaler fg {game.Name} {input} {output} {nvngx} --yes");

        OptiScalerService.ApplyFgSettings(game.InstallPath, input, output, nvngx);

        SettingsFile.Update(s =>
        {
            WriteMap(s, "OsFgInput", key, game.Name, input);
            WriteMap(s, "OsFgOutput", key, game.Name, output);
            WriteMap(s, "OsFgNvngxReplacement", key, game.Name, nvngx);
        }, "cli:optiscaler fg");

        return CliResult.Ok($"frame generation for '{game.Name}' set to input {input}, output {output}, nvngx {nvngx}",
            new { game = game.Name, input, output, nvngx, written = true });
    }

    private static CliCommand DeployFlag() => new()
    {
        Name = "optiscaler deploy-flag",
        Group = "config",
        Summary = "Set the per-game 'deploy Streamline' / 'deploy DLSS Enabler' flags the next OptiScaler install uses.",
        Usage = "rhi optiscaler deploy-flag <game> <streamline|dlss-enabler> <on|off> [--dry-run] [--yes]",
        Examples = ["rhi optiscaler deploy-flag cyberpunk streamline on --yes"],
        Run = DeployFlagRun,
    };

    private static CliResult DeployFlagRun(CliInvocation invocation)
    {
        var args = invocation.Args;
        if (args.Length < 3) return CliResult.Usage("usage: rhi optiscaler deploy-flag <game> <streamline|dlss-enabler> <on|off>");

        var game = CliGames.ResolveOne(args[0], out var failure);
        if (game is null) return failure!;

        var which = args[1].ToLowerInvariant() switch
        {
            "streamline" => "OsDeployStreamline",
            "dlss-enabler" => "OsDeployDlssEnabler",
            _ => null,
        };

        if (which is null) return CliResult.Usage("flag must be streamline or dlss-enabler");

        var on = args[2].ToLowerInvariant() switch
        {
            "on" or "true" or "yes" or "1" => true,
            "off" or "false" or "no" or "0" => false,
            _ => (bool?)null,
        };

        if (on is null) return CliResult.Usage("value must be on or off");

        var key = $"{game.Name}|{game.Source ?? ""}";

        if (invocation.DryRun)
            return CliResult.Ok($"would set {which} {(on.Value ? "on" : "off")} for '{game.Name}'",
                new { game = game.Name, flag = which, enabled = on.Value, dryRun = true });

        if (!invocation.Confirmed)
            return CliResult.Usage($"refusing to change {which} without --yes: rhi optiscaler deploy-flag {game.Name} {args[1]} {(on.Value ? "on" : "off")} --yes");

        SettingsFile.Update(s => WriteFlag(s, which, key, game.Name, on.Value), "cli:optiscaler deploy-flag");

        return CliResult.Ok($"{which} {(on.Value ? "on" : "off")} for '{game.Name}'",
            new { game = game.Name, flag = which, enabled = on.Value });
    }

    private static CliCommand Ini() => new()
    {
        Name = "optiscaler ini",
        Group = "config",
        Summary = "Remember a game's OptiScaler.ini profile and edits; --apply deploys the profile (or RHI's template) and re-applies them now.",
        Usage = "rhi optiscaler ini <game> [<profile>|none] [--edits \"Section|Key=value;Section|Key2=value2\"] [--apply] [--dry-run] [--yes]",
        Examples = ["rhi optiscaler ini cyberpunk", "rhi optiscaler ini cyberpunk MyPreset --edits \"FrameGen|FGOutput=nvngxfg\" --apply --yes"],
        Run = IniRun,
    };

    private static CliResult IniRun(CliInvocation invocation)
    {
        var game = CliGames.ResolveOne(invocation.Operand(0) ?? "", out var failure);
        if (game is null) return failure!;

        var key = $"{game.Name}|{game.Source ?? ""}";
        var settings = SettingsFile.Load();
        var profile = MapValue(settings, "OsIniProfile", key, game.Name);
        var edits = NestedValue(settings, "OsIniEdits", key, game.Name);

        var profileArg = invocation.Args.Length > 1 ? invocation.Args[1] : null;
        var editsArg = invocation.Flag("edits");

        if (profileArg is null && editsArg is null && !invocation.Has("apply"))
            return CliResult.Ok($"profile {(profile ?? "RHI template")}, {edits.Count} saved edit(s)",
                new { game = game.Name, profile, edits });

        if (profileArg is not null)
            profile = profileArg.Equals("none", StringComparison.OrdinalIgnoreCase) ? null : profileArg;

        if (editsArg is not null)
        {
            edits = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var pair in editsArg.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            {
                var eq = pair.IndexOf('=');
                if (eq <= 0) return CliResult.Usage($"'{pair}' is not key=value");
                edits[pair[..eq].Trim()] = pair[(eq + 1)..].Trim();
            }
        }

        if (invocation.DryRun)
            return CliResult.Ok($"would set profile {profile ?? "RHI template"}{(editsArg is null ? "" : $", {edits.Count} edit(s)")}{(invocation.Has("apply") ? ", then redeploy OptiScaler.ini" : "")}",
                new { game = game.Name, profile, edits, dryRun = true });

        if (!invocation.Confirmed)
            return CliResult.Usage($"refusing to change OptiScaler settings without --yes: rhi optiscaler ini {game.Name} --yes");

        SettingsFile.Update(s =>
        {
            if (profile is null) RemoveEntry(s, "OsIniProfile", key, game.Name);
            else WriteMap(s, "OsIniProfile", key, game.Name, profile);

            if (edits.Count == 0) RemoveEntry(s, "OsIniEdits", key, game.Name);
            else WriteNested(s, "OsIniEdits", key, edits);
        }, "cli:optiscaler ini");

        if (!invocation.Has("apply"))
            return CliResult.Ok($"saved profile {profile ?? "RHI template"} and {edits.Count} edit(s) for '{game.Name}'",
                new { game = game.Name, profile, edits });

        var card = CliCards.Build(game, out var cardError);
        if (card is null) return CliResult.Fail(cardError!);

        if (profile is not null && OsIniLibrary.Exists(profile))
            OsIniLibrary.Deploy(profile, game.InstallPath, Value(settings, "OsHotkey"));
        else
            CliServices.Get<IOptiScalerService>().CopyIniToGame(card, Value(settings, "OsHotkey"),
                Value(settings, "OsGpuType") ?? "NVIDIA",
                !string.Equals(Value(settings, "OsDlssInputs"), "false", StringComparison.OrdinalIgnoreCase));

        OptiScalerService.ApplyFgSettings(game.InstallPath,
            MapValue(settings, "OsFgInput", key, game.Name) ?? "auto",
            MapValue(settings, "OsFgOutput", key, game.Name) ?? "auto",
            MapValue(settings, "OsFgNvngxReplacement", key, game.Name) ?? "auto");

        var kept = OsIniEditor.ApplySaved(game.InstallPath, edits.Count == 0 ? null : edits);

        return CliResult.Ok($"OptiScaler.ini redeployed for '{game.Name}' with profile {profile ?? "RHI template"} and {kept} saved edit(s) re-applied",
            new { game = game.Name, profile, edits = kept, applied = true });
    }

    private static string? MapValue(Dictionary<string, string> settings, string settingKey, string key, string gameName) =>
        ObjectValue(settings, settingKey, key) ?? ObjectValue(settings, settingKey, gameName);

    private static string? ObjectValue(Dictionary<string, string> settings, string settingKey, string key)
    {
        if (!settings.TryGetValue(settingKey, out var raw) || string.IsNullOrWhiteSpace(raw)) return null;

        try
        {
            var map = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(raw);
            return map is not null && map.TryGetValue(key, out var value) ? value : null;
        }
        catch (System.Text.Json.JsonException) { return null; }
    }

    private static Dictionary<string, string> NestedValue(Dictionary<string, string> settings, string settingKey, string key, string gameName)
    {
        if (!settings.TryGetValue(settingKey, out var raw) || string.IsNullOrWhiteSpace(raw)) return new(StringComparer.OrdinalIgnoreCase);

        try
        {
            var map = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(raw);
            if (map is not null && map.TryGetValue(key, out var edits)) return new(edits, StringComparer.OrdinalIgnoreCase);
        }
        catch (System.Text.Json.JsonException) { }

        return new(StringComparer.OrdinalIgnoreCase);
    }

    private static void WriteMap(Dictionary<string, string> settings, string settingKey, string key, string gameName, string value)
    {
        var map = ObjectValueMap(settings, settingKey);
        map.Remove(gameName);

        if (value.Equals("auto", StringComparison.OrdinalIgnoreCase)) map.Remove(key);
        else map[key] = value;

        settings[settingKey] = System.Text.Json.JsonSerializer.Serialize(map);
    }

    private static void WriteNested(Dictionary<string, string> settings, string settingKey, string key, Dictionary<string, string> edits)
    {
        Dictionary<string, Dictionary<string, string>> map;

        try
        {
            map = settings.TryGetValue(settingKey, out var raw) && !string.IsNullOrWhiteSpace(raw)
                ? System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(raw) ?? new()
                : new();
        }
        catch (System.Text.Json.JsonException) { map = new(); }

        map[key] = new Dictionary<string, string>(edits);
        settings[settingKey] = System.Text.Json.JsonSerializer.Serialize(map);
    }

    private static Dictionary<string, string> ObjectValueMap(Dictionary<string, string> settings, string settingKey)
    {
        try
        {
            return settings.TryGetValue(settingKey, out var raw) && !string.IsNullOrWhiteSpace(raw)
                ? System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(raw) ?? new()
                : new();
        }
        catch (System.Text.Json.JsonException) { return new(); }
    }

    private static void RemoveEntry(Dictionary<string, string> settings, string settingKey, string key, string gameName)
    {
        if (settingKey == "OsIniEdits")
        {
            try
            {
                var map = settings.TryGetValue(settingKey, out var raw) && !string.IsNullOrWhiteSpace(raw)
                    ? System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, System.Text.Json.JsonElement>>(raw)
                    : null;

                if (map is null) return;
                map.Remove(key);
                map.Remove(gameName);
                settings[settingKey] = System.Text.Json.JsonSerializer.Serialize(map);
            }
            catch (System.Text.Json.JsonException) { return; }

            return;
        }

        var simple = ObjectValueMap(settings, settingKey);
        simple.Remove(key);
        simple.Remove(gameName);
        settings[settingKey] = System.Text.Json.JsonSerializer.Serialize(simple);
    }

    private static void WriteFlag(Dictionary<string, string> settings, string settingKey, string key, string gameName, bool on)
    {
        List<string> values;

        try
        {
            values = settings.TryGetValue(settingKey, out var raw) && !string.IsNullOrWhiteSpace(raw)
                ? System.Text.Json.JsonSerializer.Deserialize<List<string>>(raw) ?? []
                : [];
        }
        catch (System.Text.Json.JsonException) { values = []; }

        values.RemoveAll(v => string.Equals(v, key, StringComparison.OrdinalIgnoreCase)
                           || string.Equals(v, gameName, StringComparison.OrdinalIgnoreCase));

        if (on) values.Add(key);

        settings[settingKey] = System.Text.Json.JsonSerializer.Serialize(values);
    }

    private static string? Value(Dictionary<string, string> settings, string key) =>
        settings.TryGetValue(key, out var value) && !string.IsNullOrWhiteSpace(value) ? value : null;
}
