using System.Text.Json;
using RenoDXCommander.Models;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// Presets. `show` reports the shared ReShadePreset.ini; `deploy` copies it, or copies named presets
/// from the preset library and optionally installs the shader packs those presets use.
/// </summary>
internal static class CliReShadePreset
{
    internal static readonly CliCommand[] All = [Show(), Deploy()];

    private static CliCommand Show() => new()
    {
        Name = "reshade preset show",
        Group = "config",
        Summary = "Show the shared ReShadePreset.ini RHI ships and whether the game has a copy.",
        Usage = "rhi reshade preset show <game> [--json]",
        Examples = ["rhi reshade preset show cyberpunk"],
        Run = ShowRun,
    };

    private static CliResult ShowRun(CliInvocation invocation)
    {
        var game = CliGames.ResolveOne(invocation, out var failure);
        if (game is null) return failure!;

        var source = AuxInstallService.RsPresetIniPath;
        var destination = Path.Combine(game.InstallPath, "ReShadePreset.ini");
        var sourceExists = File.Exists(source);
        var installed = File.Exists(destination);
        var library = Directory.Exists(PresetPopupHelper.PresetsDir)
            ? Directory.GetFiles(PresetPopupHelper.PresetsDir, "*.ini").Select(Path.GetFileName).ToArray()
            : [];

        return CliResult.Ok(
            $"shared preset: {(sourceExists ? source : source + " (missing)")}; game copy: {(installed ? "present" : "absent")}; library: {library.Length} preset(s) in {PresetPopupHelper.PresetsDir}",
            new { game = game.Name, source, sourceExists, destination, installed, library });
    }

    private static CliCommand Deploy() => new()
    {
        Name = "reshade preset deploy",
        Group = "config",
        Summary = "Copy the shared ReShadePreset.ini into a game, or deploy named presets from the preset library (optionally with their shader packs).",
        Usage = "rhi reshade preset deploy <game|--all> [--presets a,b] [--shaders] [--dry-run] [--yes]",
        Examples =
        [
            "rhi reshade preset deploy cyberpunk --yes",
            "rhi reshade preset deploy cyberpunk --presets NaturalVision,ReshadeReal --shaders --yes",
            "rhi reshade preset deploy --all --presets NaturalVision --yes",
        ],
        Destructive = true,
        Run = invocation => DeployRun(invocation).GetAwaiter().GetResult(),
    };

    private static async Task<CliResult> DeployRun(CliInvocation invocation)
    {
        var installer = CliServices.Get<IAuxInstallService>();
        var library = CliGames.LoadLibrary(out var failure);
        if (library is null) return failure!;

        List<SavedGame> targets;
        if (invocation.Has("all"))
        {
            targets = CliGames.AllGames(library)
                .Where(game => installer.FindRecord(game.Name, game.InstallPath, AuxInstallService.TypeReShade) is not null
                            || installer.FindRecord(game.Name, game.InstallPath, AuxInstallService.TypeReShadeNormal) is not null)
                .ToList();

            if (targets.Count == 0)
                return CliResult.Fail("no games with ReShade installed - install ReShade first");
        }
        else
        {
            var game = CliGames.ResolveOne(invocation.Operand(0) ?? "", out var resolveFailure);
            if (game is null) return resolveFailure!;
            targets = [game];
        }

        var presetFiles = new List<string>();
        if (!string.IsNullOrWhiteSpace(invocation.Flag("presets")))
        {
            foreach (var raw in invocation.Flag("presets")!.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            {
                var path = Path.IsPathRooted(raw) ? raw : Path.Combine(PresetPopupHelper.PresetsDir, raw);
                if (!File.Exists(path) && !Path.HasExtension(path)) path += ".ini";
                if (!File.Exists(path)) return CliResult.Fail($"preset not found: {path}");
                presetFiles.Add(path);
            }
        }

        var applyShaders = invocation.Has("shaders");
        var summary = new List<object>();

        if (invocation.DryRun)
        {
            foreach (var game in targets)
                summary.Add(new { game = game.Name, presets = presetFiles.Select(Path.GetFileName).ToArray(), shaders = applyShaders });

            return CliResult.Ok(
                presetFiles.Count == 0
                    ? $"would copy the shared ReShadePreset.ini into {targets.Count} game(s)"
                    : $"would deploy {presetFiles.Count} preset(s){(applyShaders ? " and their shader packs" : "")} into {targets.Count} game(s)",
                new { dryRun = true, targets = summary });
        }

        if (!invocation.Confirmed)
            return CliResult.Usage($"refusing to write presets without --yes: rhi reshade preset deploy {(invocation.Has("all") ? "--all" : "\"" + targets[0].Name + "\"")}{(presetFiles.Count > 0 ? " --presets " + string.Join(',', presetFiles.Select(Path.GetFileName)) : "")} --yes");

        foreach (var game in targets)
        {
            if (presetFiles.Count == 0)
            {
                if (!File.Exists(AuxInstallService.RsPresetIniPath))
                    return CliResult.Fail($"no shared preset to deploy - {AuxInstallService.RsPresetIniPath} does not exist");

                AuxInstallService.CopyRsPresetIniIfPresent(game.InstallPath);
            }
            else
            {
                var copied = PresetPopupHelper.DeployPresets(presetFiles.Select(Path.GetFileName).ToList(), game.InstallPath);
                if (copied == 0)
                    return CliResult.Fail($"no preset was copied into {game.InstallPath}");

                if (applyShaders)
                    await ApplyPresetShadersAsync(game, presetFiles);
            }

            summary.Add(new { game = game.Name, presets = presetFiles.Select(Path.GetFileName).ToArray(), shaders = applyShaders });
        }

        return CliResult.Ok(
            presetFiles.Count == 0
                ? $"deployed the shared ReShade preset into {targets.Count} game(s)"
                : $"deployed {presetFiles.Count} preset(s){(applyShaders ? " and their shader packs" : "")} into {targets.Count} game(s)",
            new { deployed = summary });
    }

    private static async Task ApplyPresetShadersAsync(SavedGame game, List<string> presetFiles)
    {
        var fxFiles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var preset in presetFiles)
        {
            foreach (var line in File.ReadAllLines(preset))
            {
                var trimmed = line.Trim();
                if (!trimmed.StartsWith("Techniques=", StringComparison.OrdinalIgnoreCase)
                    && !trimmed.StartsWith("Techniques =", StringComparison.OrdinalIgnoreCase))
                    continue;

                var eq = trimmed.IndexOf('=');
                if (eq >= 0) fxFiles.UnionWith(TechniquesParser.ExtractFxFiles(trimmed[(eq + 1)..]));
            }
        }

        if (fxFiles.Count == 0) return;

        var packs = CliServices.Get<IShaderPackService>();
        var fileLists = BuildPackFileLists(packs);
        var missing = packs.AvailablePacks.Select(p => p.Id).Where(id => !fileLists.ContainsKey(id)).ToList();

        if (missing.Count > 0)
        {
            await packs.EnsurePacksAsync(missing);
            fileLists = BuildPackFileLists(packs);
        }

        var (matched, _) = ShaderResolver.Resolve(fxFiles, fileLists);
        if (matched.Count == 0) return;

        var expanded = packs.ExpandPackDependencies(matched).ToList();
        await packs.EnsurePacksAsync(expanded);

        var key = $"{game.Name}|{game.Source ?? ""}";
        SettingsFile.Update(settings =>
        {
            var mode = ReadObject(settings, "PerGameShaderMode");
            mode[key] = "Select";
            settings["PerGameShaderMode"] = JsonSerializer.Serialize(mode);

            var selections = ReadObjectArray(settings, "PerGameShaderSelection");
            var merged = selections.TryGetValue(key, out var existing)
                ? new HashSet<string>(existing, StringComparer.OrdinalIgnoreCase)
                : new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            merged.UnionWith(expanded);
            selections[key] = merged.ToList();
            settings["PerGameShaderSelection"] = JsonSerializer.Serialize(selections);
        }, "cli:preset deploy");

        var exclusions = expanded.ToDictionary(id => id, id => packs.GetExcludedFiles(id));
        packs.SyncGameFolder(game.InstallPath, expanded, exclusions);
    }

    private static Dictionary<string, IReadOnlyList<string>> BuildPackFileLists(IShaderPackService packs)
    {
        var settings = SettingsFile.Snapshot();
        var result = new Dictionary<string, IReadOnlyList<string>>(StringComparer.Ordinal);

        foreach (var (id, _, _) in packs.AvailablePacks)
        {
            if (!settings.TryGetValue($"ShaderPack_{id}_Files", out var json) || string.IsNullOrWhiteSpace(json)) continue;

            try
            {
                var files = JsonSerializer.Deserialize<List<string>>(json);
                if (files is not null) result[id] = files;
            }
            catch (JsonException) { }
        }

        return result;
    }

    private static Dictionary<string, string> ReadObject(Dictionary<string, string> settings, string key)
    {
        if (!settings.TryGetValue(key, out var json) || string.IsNullOrWhiteSpace(json)) return new(StringComparer.OrdinalIgnoreCase);

        try { return JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new(StringComparer.OrdinalIgnoreCase); }
        catch (JsonException) { return new(StringComparer.OrdinalIgnoreCase); }
    }

    private static Dictionary<string, List<string>> ReadObjectArray(Dictionary<string, string> settings, string key)
    {
        if (!settings.TryGetValue(key, out var json) || string.IsNullOrWhiteSpace(json)) return new(StringComparer.OrdinalIgnoreCase);

        try { return JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json) ?? new(StringComparer.OrdinalIgnoreCase); }
        catch (JsonException) { return new(StringComparer.OrdinalIgnoreCase); }
    }
}
