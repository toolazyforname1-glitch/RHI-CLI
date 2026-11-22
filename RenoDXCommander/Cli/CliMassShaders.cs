// CliMassShaders.cs — `mass deploy shaders`: the terminal twin of MainViewModel.DeployAllShaders().
// Every branch mirrors ResolveShaderSelection, but reads settings.json instead of view-model state:
// per-game Off -> null, per-game Custom -> sentinel, per-game Select -> that game's pack list,
// global Off -> null, global UseCustomShaders -> sentinel, otherwise the global pack selection.
using System.Text.Json;
using RenoDXCommander.Models;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// Syncs ReShade shader packs into every game that has ReShade installed, using each game's
/// resolved shader selection - the same work the GUI's "deploy all shaders" button does.
/// </summary>
internal static class CliMassShaders
{
    internal static readonly CliCommand[] All = [Deploy()];

    private const string ModeKey = "PerGameShaderMode";
    private const string SelectionKey = "PerGameShaderSelection";

    private sealed record Target(SavedGame Game, string Mode, string[]? Selection);

    private static CliCommand Deploy() => new()
    {
        Name = "mass deploy shaders",
        Group = "install",
        Summary = "Sync ReShade shader packs into every game that has ReShade installed, using each game's resolved selection.",
        Usage = "rhi mass deploy shaders [--dry-run] [--yes]",
        Examples = ["rhi mass deploy shaders --dry-run", "rhi mass deploy shaders --yes"],
        Destructive = true,
        RunAsync = DeployAsync,
    };

    private static async Task<CliResult> DeployAsync(CliInvocation invocation)
    {
        var library = CliGames.LoadLibrary(out var failure);
        if (library is null) return failure!;

        var settings = SettingsFile.Load();
        var modes = ReadObject(settings, ModeKey);
        var selections = ReadObjectArray(settings, SelectionKey);
        var globalOff = IsTrue(settings, "GlobalShadersOff");
        var useCustom = IsTrue(settings, "UseCustomShaders");
        var globalPacks = ReadList(settings, "SelectedShaderPacks");

        // Load the ReShade install records once: a per-game FindRecord would re-read the db every call.
        var aux = CliServices.Get<IAuxInstallService>();
        var records = aux.LoadAll()
            .Where(record => IsReShadeRecord(record.AddonType))
            .ToList();

        var targets = new List<Target>();
        var skipped = new List<string>();

        foreach (var game in CliGames.AllGames(library))
        {
            if (string.IsNullOrEmpty(game.InstallPath)) continue; // mirrors DeployAllShaders

            if (!IsReShadeInstalled(game, records))
            {
                skipped.Add(game.Name);
                continue;
            }

            var mode = ResolveMode(modes, game.Name, game.Source ?? "");
            var selection = ResolveSelection(mode, game.Name, game.Source ?? "", selections, globalOff, useCustom, globalPacks);
            targets.Add(new Target(game, mode, selection));
        }

        if (targets.Count == 0)
            return CliResult.Ok("no game has ReShade installed - nothing to deploy",
                new { processed = 0, skipped = skipped.Count, packsEnsured = 0 });

        // One download pass for the union, exactly like the GUI, before any game is touched.
        var union = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var target in targets)
            if (target.Selection is not null) union.UnionWith(target.Selection);

        if (invocation.DryRun)
        {
            var rows = targets.Select(t => new
            {
                game = t.Game.Name,
                store = t.Game.Source ?? "",
                path = t.Game.InstallPath,
                mode = t.Mode,
                packs = t.Selection,
                sync = t.Selection is not null,
            }).ToArray();

            return CliResult.Ok(
                $"would sync shaders for {targets.Count} game(s) using {union.Count} pack(s); {skipped.Count} skipped (no ReShade) - nothing downloaded or written",
                new
                {
                    dryRun = true,
                    count = targets.Count,
                    packsEnsured = union.Count,
                    packs = union.ToArray(),
                    games = rows,
                    skipped = skipped.ToArray(),
                });
        }

        if (!invocation.Confirmed)
            return CliResult.Usage($"refusing to sync shaders for {targets.Count} game(s) without --yes: rhi mass deploy shaders --yes");

        var packs = CliServices.Get<IShaderPackService>();

        if (union.Count > 0)
        {
            try
            {
                await packs.EnsurePacksAsync(union);
            }
            catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or IOException)
            {
                return CliResult.Fail($"could not download the shader packs: {ex.Message}", new { packs = union.ToArray() });
            }
        }

        var processed = 0;
        var failed = new List<string>();

        foreach (var target in targets)
        {
            try
            {
                // Same exclusions dictionary the GUI builds: one per selected pack id.
                var exclusions = target.Selection?.ToDictionary(
                    id => id,
                    id => packs.GetExcludedFiles(id),
                    StringComparer.OrdinalIgnoreCase);

                packs.SyncGameFolder(target.Game.InstallPath, target.Selection, exclusions);
                processed++;
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                failed.Add(target.Game.Name + ": " + ex.Message);
            }
        }

        var summary = new
        {
            processed,
            skipped = skipped.Count,
            skippedGames = skipped.ToArray(),
            packsEnsured = union.Count,
            packs = union.ToArray(),
            failed,
        };

        return failed.Count == 0
            ? CliResult.Ok($"{processed} game(s) synced, {union.Count} pack(s) ensured, {skipped.Count} skipped (no ReShade)", summary)
            : CliResult.Fail(
                $"synced {processed} game(s), {failed.Count} failed, {skipped.Count} skipped (no ReShade) - {string.Join("; ", failed.Take(5))}",
                summary);
    }

    private static bool IsReShadeRecord(string addonType) =>
        string.Equals(addonType, AuxInstallService.TypeReShade, StringComparison.OrdinalIgnoreCase)
        || string.Equals(addonType, AuxInstallService.TypeReShadeNormal, StringComparison.OrdinalIgnoreCase);

    /// <summary>Same match as IAuxInstallService.FindRecord (name + path), plus the Vulkan footprint.</summary>
    private static bool IsReShadeInstalled(SavedGame game, List<AuxInstalledRecord> records) =>
        records.Any(record =>
            string.Equals(record.GameName, game.Name, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(record.InstallPath, game.InstallPath, StringComparison.OrdinalIgnoreCase))
        || VulkanFootprintService.Exists(game.InstallPath);

    /// <summary>Same lookup as GameNameService.GetPerGameShaderMode: composite key, then legacy name-only.</summary>
    private static string ResolveMode(Dictionary<string, string> modes, string gameName, string store)
    {
        var key = GameKey.From(gameName, store).ToKey();
        if (modes.TryGetValue(key, out var mode)) return mode;
        return modes.TryGetValue(gameName, out mode) ? mode : "Global";
    }

    /// <summary>
    /// Literal copy of MainViewModel.ResolveShaderSelection. Priority chain:
    /// per-game Off -> null, per-game Custom -> sentinel, per-game Select -> its pack list,
    /// global Off -> null, global UseCustomShaders -> sentinel, else the global pack selection.
    /// </summary>
    private static string[]? ResolveSelection(
        string mode,
        string gameName,
        string store,
        Dictionary<string, List<string>> selections,
        bool globalOff,
        bool useCustom,
        List<string> globalPacks)
    {
        // 0. Per-game "Off" mode -> null (removes managed shaders)
        if (string.Equals(mode, "Off", StringComparison.OrdinalIgnoreCase))
            return null;

        // 1. Per-game "Custom" mode -> custom shader sentinel
        if (string.Equals(mode, "Custom", StringComparison.OrdinalIgnoreCase))
            return [ShaderPackService.CustomShaderSentinel];

        // 2. Per-game "Select" mode -> per-game selection (composite key first, name-only fallback)
        if (string.Equals(mode, "Select", StringComparison.OrdinalIgnoreCase))
        {
            var compositeKey = GameKey.From(gameName, store).ToKey();
            if (selections.TryGetValue(compositeKey, out var perGame) || selections.TryGetValue(gameName, out perGame))
                return perGame?.ToArray();
        }

        // 0b. Global "Off" applies only when no per-game override was honoured above.
        if (globalOff)
            return null;

        // 3. Global UseCustomShaders -> custom shader sentinel
        if (useCustom)
            return [ShaderPackService.CustomShaderSentinel];

        // 4. Fallback -> global pack selection
        return globalPacks.ToArray();
    }

    private static bool IsTrue(Dictionary<string, string> settings, string key) =>
        settings.TryGetValue(key, out var raw) && string.Equals(raw, "true", StringComparison.OrdinalIgnoreCase);

    private static Dictionary<string, string> ReadObject(Dictionary<string, string> settings, string key)
    {
        if (!settings.TryGetValue(key, out var json) || string.IsNullOrWhiteSpace(json))
            return new(StringComparer.OrdinalIgnoreCase);

        try
        {
            return new(JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new(),
                StringComparer.OrdinalIgnoreCase);
        }
        catch (JsonException) { return new(StringComparer.OrdinalIgnoreCase); }
    }

    private static Dictionary<string, List<string>> ReadObjectArray(Dictionary<string, string> settings, string key)
    {
        if (!settings.TryGetValue(key, out var json) || string.IsNullOrWhiteSpace(json))
            return new(StringComparer.OrdinalIgnoreCase);

        try
        {
            var map = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json);
            return map is null ? new(StringComparer.OrdinalIgnoreCase) : new(map, StringComparer.OrdinalIgnoreCase);
        }
        catch (JsonException) { return new(StringComparer.OrdinalIgnoreCase); }
    }

    private static List<string> ReadList(Dictionary<string, string> settings, string key)
    {
        if (!settings.TryGetValue(key, out var json) || string.IsNullOrWhiteSpace(json)) return [];

        try { return JsonSerializer.Deserialize<List<string>>(json) ?? []; }
        catch (JsonException) { return []; }
    }
}
