using System.Text;
using System.Text.Json;
using RenoDXCommander.Models;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// Frame generation commands. <c>framegen dlssg</c> reads the 20/30-series DLSS-G unlocker and flips
/// its ini switch; <c>framegen mfg</c> reports or changes the Universal MFG Unlocker placement only -
/// that mod has no settings file, its 2x/3x/dynamic options live in the in-game menu.
/// </summary>
internal static class CliFrameGen
{
    private const string DlssgSettingsKey = "Dlssg2030InstalledAs";
    private const string MfgSettingsKey = "Rtx40MfgInstalledAs";

    internal static readonly CliCommand[] All = [Dlssg(), Mfg()];

    /// <summary>The "Name|Store" settings key the GUI uses for per-game install records.</summary>
    private static string KeyFor(SavedGame game) => GameKey.From(game.Name, game.Source ?? "").ToKey();

    /// <summary>Reads one JSON-object settings value into a mutable, case-insensitive map.</summary>
    private static Dictionary<string, string> ReadMap(string settingsKey)
    {
        var settings = SettingsFile.Load();
        if (!settings.TryGetValue(settingsKey, out var json) || string.IsNullOrWhiteSpace(json))
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        try
        {
            var map = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            return map is null
                ? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                : new Dictionary<string, string>(map, StringComparer.OrdinalIgnoreCase);
        }
        catch (JsonException)
        {
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }
    }

    /// <summary>Composite "Name|Store" first, then the bare game name for legacy entries.</summary>
    private static string? InstalledAs(Dictionary<string, string> map, SavedGame game)
    {
        if (map.TryGetValue(KeyFor(game), out var exact) && !string.IsNullOrWhiteSpace(exact)) return exact;
        if (map.TryGetValue(game.Name, out var bare) && !string.IsNullOrWhiteSpace(bare)) return bare;
        return null;
    }

    /// <summary>Writes the map back, dropping both the composite and bare keys when clearing.</summary>
    private static void Persist(string settingsKey, SavedGame game, string? installedAs)
    {
        var map = ReadMap(settingsKey);
        if (string.IsNullOrWhiteSpace(installedAs))
        {
            map.Remove(KeyFor(game));
            map.Remove(game.Name);
        }
        else
        {
            map[KeyFor(game)] = installedAs;
        }

        SettingsFile.Update(d =>
        {
            if (map.Count > 0) d[settingsKey] = JsonSerializer.Serialize(map);
            else d.Remove(settingsKey);
        }, "cli:framegen");
    }

    private static CliCommand Dlssg() => new()
    {
        Name = "framegen dlssg",
        Group = "dlss",
        Summary = "Show, install, remove or switch the 20/30-series DLSS-G unlocker for a game.",
        Usage = "rhi framegen dlssg <game> [show|on|off|install [--dll <name>]|remove] [--dry-run] [--yes] [--json]",
        Examples =
        [
            "rhi framegen dlssg cyberpunk",
            "rhi framegen dlssg cyberpunk on --yes",
            "rhi framegen dlssg cyberpunk install --yes",
            "rhi framegen dlssg cyberpunk install --dll dxgi.dll --yes",
            "rhi framegen dlssg cyberpunk remove --yes",
        ],
        Destructive = false,
        Run = invocation => DlssgRunAsync(invocation).GetAwaiter().GetResult(),
    };

    private static async Task<CliResult> DlssgRunAsync(CliInvocation invocation)
    {
        var args = invocation.Args;
        if (args.Length == 0)
            return CliResult.Usage("usage: rhi framegen dlssg <game> [show|on|off|install [--dll <name>]|remove]");

        var game = CliGames.ResolveOne(args[0], out var failure);
        if (game is null) return failure!;

        var action = args.Length > 1 ? args[1].ToLowerInvariant() : "show";
        if (action is not ("show" or "on" or "off" or "install" or "remove"))
            return CliResult.Usage($"unknown action '{action}' - use show, on, off, install or remove");

        var installPath = game.InstallPath;
        if (string.IsNullOrWhiteSpace(installPath))
            return CliResult.Fail($"'{game.Name}' has no install folder - nothing to read or write");

        var service = CliServices.Get<Dlssg20_30Service>();
        var installedAs = InstalledAs(ReadMap(DlssgSettingsKey), game);
        var iniPath = Path.Combine(installPath, Dlssg20_30Service.IniFileName);

        if (action == "show") return DlssgShow(game, installPath, iniPath, installedAs, service);
        if (action == "install") return await DlssgInstallAsync(invocation, game, installPath, installedAs, service);
        if (action == "remove") return DlssgRemove(invocation, game, installPath, installedAs, service);

        if (!invocation.DryRun && !invocation.Confirmed)
            return CliResult.Usage(
                $"refusing to write {Dlssg20_30Service.IniFileName} without --yes: rhi framegen dlssg {game.Name} {action} --yes");

        if (!service.IsInstalledIn(installPath, installedAs))
            return CliResult.Fail(
                $"20/30 FG Unlock is not installed in '{game.Name}' - install it from RHI's Extras panel first");

        if (!File.Exists(iniPath))
            return CliResult.Fail(
                $"{Dlssg20_30Service.IniFileName} is missing from {installPath} - reinstall the mod first");

        var value = action == "on" ? "1" : "0";
        var previous = Dlssg20_30Service.ReadIniValue(iniPath, "General", "Enabled") ?? "unset";

        if (invocation.DryRun)
            return CliResult.Ok(
                $"would set General.Enabled = {value} in {Dlssg20_30Service.IniFileName} (was {previous})",
                new { game = game.Name, installPath, section = "General", key = "Enabled", value, previous, dryRun = true });

        try
        {
            Dlssg20_30Service.WriteIniValue(iniPath, "General", "Enabled", value);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            return CliResult.Fail($"writing {Dlssg20_30Service.IniFileName} failed for '{game.Name}': {ex.Message}");
        }

        return CliResult.Ok(
            $"General.Enabled = {value} in '{game.Name}' (was {previous})",
            new { game = game.Name, installPath, section = "General", key = "Enabled", value, previous, changed = true });
    }

    private static async Task<CliResult> DlssgInstallAsync(
        CliInvocation invocation, SavedGame game, string installPath, string? installedAs, Dlssg20_30Service service)
    {
        var requested = invocation.Flag("dll")?.Trim();
        if (!string.IsNullOrEmpty(requested) && !Dlssg20_30Service.IsSupportedProxyName(requested))
            return CliResult.Usage(
                $"'{requested}' is not a supported proxy name - pick one of: {string.Join(", ", Dlssg20_30Service.ProxyNames)}");

        var chosen = string.IsNullOrEmpty(requested) ? FreeProxyName(installPath) : requested;
        var isInstalled = service.IsInstalledIn(installPath, installedAs);

        if (!invocation.DryRun && !invocation.Confirmed)
            return CliResult.Usage(
                $"refusing to install the 20/30 FG Unlock without --yes: rhi framegen dlssg {game.Name} install --yes");

        if (invocation.DryRun)
            return CliResult.Ok(
                $"would install the 20/30 FG Unlock as {chosen} in '{game.Name}' (staging ready: {service.IsStagingReady})",
                new
                {
                    game = game.Name,
                    installPath,
                    installedAs = chosen,
                    replaced = isInstalled ? installedAs : null,
                    stagingReady = service.IsStagingReady,
                    dryRun = true,
                });

        try
        {
            if (isInstalled && !string.IsNullOrEmpty(installedAs)
                && !installedAs.Equals(chosen, StringComparison.OrdinalIgnoreCase))
                AuxInstallService.SentinelRestore(Path.Combine(installPath, installedAs));

            if (!service.IsStagingReady || service.HasUpdate) await service.EnsureStagingAsync();

            if (!service.IsStagingReady)
                return CliResult.Fail(
                    $"20/30 FG Unlock staging is not ready (staged: {service.StagedVersion ?? "none"}) - download it from RHI's Extras panel first");

            if (!service.Install(installPath, chosen))
                return CliResult.Fail($"installing the 20/30 FG Unlock as {chosen} failed for '{game.Name}'");
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or InvalidOperationException or HttpRequestException)
        {
            return CliResult.Fail($"installing the 20/30 FG Unlock as {chosen} failed for '{game.Name}': {ex.Message}");
        }

        Persist(DlssgSettingsKey, game, chosen);
        return CliResult.Ok($"20/30 FG Unlock installed as {chosen} in '{game.Name}'",
            new { game = game.Name, installPath, installedAs = chosen, stagedVersion = service.StagedVersion, changed = true });
    }

    private static CliResult DlssgRemove(
        CliInvocation invocation, SavedGame game, string installPath, string? installedAs, Dlssg20_30Service service)
    {
        if (!invocation.DryRun && !invocation.Confirmed)
            return CliResult.Usage(
                $"refusing to remove the 20/30 FG Unlock without --yes: rhi framegen dlssg {game.Name} remove --yes");

        if (string.IsNullOrWhiteSpace(installedAs) && !service.IsInstalledIn(installPath, installedAs))
            return CliResult.Fail($"20/30 FG Unlock is not installed in '{game.Name}' - nothing to remove");

        if (invocation.DryRun)
            return CliResult.Ok($"would remove {installedAs} from '{game.Name}'",
                new { game = game.Name, installPath, installedAs, dryRun = true });

        try
        {
            service.Uninstall(installPath, installedAs);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            return CliResult.Fail($"removing the 20/30 FG Unlock failed for '{game.Name}': {ex.Message}");
        }

        Persist(DlssgSettingsKey, game, null);
        return CliResult.Ok($"removed {installedAs} from '{game.Name}'",
            new { game = game.Name, installPath, removed = installedAs, changed = true });
    }

    /// <summary>version.dll when free, else the first proxy name not already in the folder.</summary>
    private static string FreeProxyName(string installPath)
    {
        var version = Dlssg20_30Service.ProxyNames
            .FirstOrDefault(name => name.Equals("version.dll", StringComparison.OrdinalIgnoreCase));
        if (version is not null && !File.Exists(Path.Combine(installPath, version))) return version;

        foreach (var name in Dlssg20_30Service.ProxyNames)
            if (!File.Exists(Path.Combine(installPath, name))) return name;

        return version ?? Dlssg20_30Service.ProxyNames[0];
    }

    private static CliResult DlssgShow(
        SavedGame game, string installPath, string iniPath, string? installedAs, Dlssg20_30Service service)
    {
        var installed = service.IsInstalledIn(installPath, installedAs);
        var stagedVersion = service.StagedVersion;

        var values = new Dictionary<string, string?>(StringComparer.Ordinal);
        foreach (var key in Dlssg20_30Service.ManagedKeys)
            values[$"{key.Section}.{key.Key}"] = Dlssg20_30Service.ReadIniValue(iniPath, key.Section, key.Key);

        var text = new StringBuilder();
        void Line(string name, string value) => text.AppendLine(name.PadRight(16) + value);
        Line("game", game.Name);
        Line("installed", installed ? "yes" : "no");
        Line("installed-as", installedAs ?? "-");
        Line("staged", stagedVersion ?? "-");
        foreach (var pair in values) Line(pair.Key, pair.Value ?? "-");

        return CliResult.Ok(text.ToString().TrimEnd(), new
        {
            game = game.Name,
            installPath,
            installed,
            installedAs,
            stagedVersion,
            settings = values,
        });
    }

    private static CliCommand Mfg() => new()
    {
        Name = "framegen mfg",
        Group = "install",
        Summary = "Show or change the Universal MFG Unlocker placement in a game.",
        Usage = "rhi framegen mfg <game> [show] | rhi framegen mfg <game> set --dll <name>|--plugin|--remove [--yes]",
        Examples =
        [
            "rhi framegen mfg cyberpunk",
            "rhi framegen mfg cyberpunk set --plugin --yes",
            "rhi framegen mfg cyberpunk set --dll dxgi.dll --yes",
            "rhi framegen mfg cyberpunk set --remove --yes",
        ],
        Destructive = false,
        Run = invocation => MfgRunAsync(invocation).GetAwaiter().GetResult(),
    };

    private static async Task<CliResult> MfgRunAsync(CliInvocation invocation)
    {
        var args = invocation.Args;
        if (args.Length == 0)
            return CliResult.Usage("usage: rhi framegen mfg <game> [show] | rhi framegen mfg <game> set --dll <name>|--plugin|--remove");

        var game = CliGames.ResolveOne(args[0], out var failure);
        if (game is null) return failure!;

        var installPath = game.InstallPath;
        if (string.IsNullOrWhiteSpace(installPath))
            return CliResult.Fail($"'{game.Name}' has no install folder - nothing to read or write");

        var action = args.Length > 1 ? args[1].ToLowerInvariant() : "show";
        if (action is not ("show" or "set"))
            return CliResult.Usage($"unknown action '{action}' - use show or set");

        var service = CliServices.Get<Rtx40MfgService>();
        var installedAs = InstalledAs(ReadMap(MfgSettingsKey), game);

        if (action == "show") return MfgShow(game, installPath, installedAs, service);

        // The mod keeps no settings file, so multipliers are menu-only: refuse them loudly.
        var extra = args.Length > 2 ? string.Join(' ', args[2..]) : "";
        var dllName = invocation.Flag("dll")?.Trim();
        if (IsMultiplierToken(extra) || IsMultiplierToken(dllName))
            return CliResult.Usage(
                "the Universal MFG Unlocker has no settings file - 2x/3x/dynamic are picked in its in-game menu (Backspace), not from the CLI");

        var wantRemove = invocation.Has("remove");
        var wantPlugin = invocation.Has("plugin");
        var wantDll = !string.IsNullOrWhiteSpace(dllName);
        if ((wantRemove ? 1 : 0) + (wantPlugin ? 1 : 0) + (wantDll ? 1 : 0) != 1)
            return CliResult.Usage("set needs exactly one of --dll <name>, --plugin or --remove");

        if (!invocation.DryRun && !invocation.Confirmed)
            return CliResult.Usage(
                $"refusing to change the MFG placement without --yes: rhi framegen mfg {game.Name} set ... --yes");

        if (wantRemove)
        {
            if (invocation.DryRun)
                return CliResult.Ok($"would remove {installedAs ?? "the MFG install"} from '{game.Name}'",
                    new { game = game.Name, installPath, installedAs, dryRun = true });

            try
            {
                service.Uninstall(installPath, installedAs);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                return CliResult.Fail($"removing the MFG install failed for '{game.Name}': {ex.Message}");
            }

            Persist(MfgSettingsKey, game, null);
            return CliResult.Ok($"removed {installedAs ?? "the MFG install"} from '{game.Name}'",
                new { game = game.Name, installPath, removed = installedAs, changed = true });
        }

        var target = wantPlugin ? Rtx40MfgService.PluginRelPath : dllName!;
        if (!wantPlugin && !IsKnownProxyName(target))
            return CliResult.Usage(
                $"'{target}' is not a supported proxy name - pick one of: {string.Join(", ", Rtx40MfgService.KnownProxyNames)}");

        if (invocation.DryRun)
            return CliResult.Ok(
                $"would install MFG as {target} in '{game.Name}' (staging ready: {service.IsStagingReady})",
                new { game = game.Name, installPath, installedAs = target, stagingReady = service.IsStagingReady, dryRun = true });

        try
        {
            if (!service.IsStagingReady || service.HasUpdate) await service.EnsureStagingAsync();
            if (!service.IsStagingReady)
                return CliResult.Fail(
                    $"MFG staging is not ready (staged: {service.StagedVersion ?? "none"}) - download it from RHI's Extras panel first");

            if (!wantPlugin && !service.IsXessStagingReady) await service.EnsureXessStagingAsync();

            if (!string.IsNullOrWhiteSpace(installedAs) && !string.Equals(installedAs, target, StringComparison.OrdinalIgnoreCase))
                service.Uninstall(installPath, installedAs);

            if (!service.Install(installPath, target))
                return CliResult.Fail($"MFG install as {target} failed for '{game.Name}'");
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or InvalidOperationException or HttpRequestException)
        {
            return CliResult.Fail($"MFG install as {target} failed for '{game.Name}': {ex.Message}");
        }

        Persist(MfgSettingsKey, game, target);
        return CliResult.Ok($"MFG installed as {target} in '{game.Name}'",
            new { game = game.Name, installPath, installedAs = target, plugin = wantPlugin, changed = true });
    }

    private static CliResult MfgShow(
        SavedGame game, string installPath, string? installedAs, Rtx40MfgService service)
    {
        var installed = service.IsInstalledIn(installPath, installedAs);
        var plugin = Rtx40MfgService.IsPluginInstall(installedAs);
        var stagedVersion = service.StagedVersion;
        var xessReady = service.IsXessStagingReady;
        var needsXess = Rtx40MfgService.NeedsXessFiles(installPath);

        var text = new StringBuilder();
        void Line(string name, string value) => text.AppendLine(name.PadRight(16) + value);
        Line("game", game.Name);
        Line("installed", installed ? "yes" : "no");
        Line("installed-as", installedAs ?? "-");
        Line("plugin", plugin ? "yes" : "no");
        Line("staged", stagedVersion ?? "-");
        Line("xess-staged", xessReady ? "yes" : "no");
        Line("needs-xess", needsXess ? "yes" : "no");

        return CliResult.Ok(text.ToString().TrimEnd(), new
        {
            game = game.Name,
            installPath,
            installed,
            installedAs,
            plugin,
            stagedVersion,
            xessStagingReady = xessReady,
            needsXessFiles = needsXess,
        });
    }

    /// <summary>Base-name check against the service's public proxy list.</summary>
    private static bool IsKnownProxyName(string? name) =>
        !string.IsNullOrEmpty(name)
        && Rtx40MfgService.KnownProxyNames.Contains(HookedDllNames.BaseOf(name), StringComparer.OrdinalIgnoreCase);

    private static bool IsMultiplierToken(string? token)
    {
        if (string.IsNullOrWhiteSpace(token)) return false;
        var value = token.Trim().ToLowerInvariant();
        return value is "2x" or "3x" or "4x" or "5x" or "6x" or "dynamic" or "follow" or "fixed"
            or "2" or "3" or "4" or "5" or "6" or "multiplier";
    }
}
