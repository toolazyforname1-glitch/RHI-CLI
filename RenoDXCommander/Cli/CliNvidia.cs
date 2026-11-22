using System.Globalization;
using System.Text.Json;
using System.Text;
using RenoDXCommander.Models;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// The global NVIDIA driver settings the Settings page exposes (shader cache, G-Sync, FPS cap,
/// DMFG, ReBAR, VSync, power, indicator). Values are the raw driver enums: `nvidia show` prints
/// what is currently stored so a value can be set directly.
/// </summary>
internal static class CliNvidia
{
    private static readonly string[] Names =
    [
        "shader-cache", "precompile", "gsync-mode", "gsync", "fps-cap", "refresh-rate",
        "dmfg-frames", "dmfg-fps", "rebar", "rebar-size", "vsync", "power", "gsync-indicator",
    ];

    internal static readonly CliCommand[] All = [Show(), Set(), ClearCache(), HdrCommand(), ProfileCommand(), GameCommand()];

    private static CliCommand Show() => new()
    {
        Name = "nvidia show",
        Group = "nvidia",
        Summary = "Print every global NVIDIA driver setting the app controls.",
        Usage = "rhi nvidia show [--json]",
        Examples = ["rhi nvidia show"],
        Run = _ =>
        {
            var service = CliServices.Get<DlssPresetService>();
            var text = new StringBuilder();
            void Line(string name, string value) => text.AppendLine(name.PadRight(18) + value);

            Line("shader-cache", service.GetShaderCacheSize().ToString());
            Line("precompile", service.GetShaderPrecompile().ToString());
            Line("gsync-mode", service.GetGSyncMode().ToString());
            Line("gsync", service.GetGlobalGSyncEnabled().ToString());
            Line("fps-cap", service.GetGlobalFpsLimit().ToString());
            Line("refresh-rate", service.GetPreferredRefreshRate().ToString());
            Line("dmfg-frames", service.GetGlobalDmfgFrameCount().ToString());
            Line("dmfg-fps", service.GetGlobalDmfgTargetFps().ToString());
            Line("rebar", service.GetGlobalReBarEnableMode().ToString());
            Line("rebar-size", service.GetGlobalReBarSizeLimit().ToString());
            Line("vsync", service.GetGlobalVSyncMode()?.ToString() ?? "-");
            Line("power", service.GetGlobalPowerMode()?.ToString() ?? "-");
            Line("gsync-indicator", service.GetGSyncIndicator() ? "on" : "off");

            return CliResult.Ok(text.ToString().TrimEnd(), new
            {
                shaderCache = service.GetShaderCacheSize(),
                precompile = service.GetShaderPrecompile(),
                gsyncMode = service.GetGSyncMode(),
                gsync = service.GetGlobalGSyncEnabled(),
                fpsCap = service.GetGlobalFpsLimit(),
                refreshRate = service.GetPreferredRefreshRate(),
                dmfgFrames = service.GetGlobalDmfgFrameCount(),
                dmfgFps = service.GetGlobalDmfgTargetFps(),
                rebar = service.GetGlobalReBarEnableMode(),
                rebarSize = service.GetGlobalReBarSizeLimit(),
                vsync = service.GetGlobalVSyncMode(),
                power = service.GetGlobalPowerMode(),
                gsyncIndicator = service.GetGSyncIndicator(),
            });
        },
    };

    private static CliCommand Set() => new()
    {
        Name = "nvidia set",
        Group = "nvidia",
        Summary = "Change one global NVIDIA driver setting. Values are raw driver enums (see nvidia show).",
        Usage = "rhi nvidia set <setting> <value> [--dry-run] [--yes] [--json]",
        Examples = ["rhi nvidia set fps-cap 120 --yes", "rhi nvidia set gsync-indicator off --yes", "rhi nvidia set shader-cache 10737418240 --dry-run"],
        Destructive = true,
        Run = SetValue,
    };

    private static CliResult SetValue(CliInvocation invocation)
    {
        var args = invocation.Args;
        if (args.Length < 2)
            return CliResult.Usage($"usage: rhi nvidia set <{string.Join("|", Names)}> <value> [--dry-run] [--yes]");

        var name = args[0].ToLowerInvariant();
        var raw = args[1];
        var service = CliServices.Get<DlssPresetService>();

        if (!Names.Contains(name))
            return CliResult.Usage($"unknown setting '{name}' - one of: {string.Join(", ", Names)}");

        if (name == "gsync-indicator")
        {
            var enable = raw.Equals("on", StringComparison.OrdinalIgnoreCase) || raw.Equals("true", StringComparison.OrdinalIgnoreCase) || raw == "1";
            var disable = raw.Equals("off", StringComparison.OrdinalIgnoreCase) || raw.Equals("false", StringComparison.OrdinalIgnoreCase) || raw == "0";
            if (!enable && !disable) return CliResult.Usage("gsync-indicator takes on or off");

            var was = service.GetGSyncIndicator() ? "on" : "off";
            if (invocation.DryRun) return CliResult.Ok($"would set gsync-indicator to {(enable ? "on" : "off")} (was {was})", new { setting = name, value = enable, previous = was, dryRun = true });

            return service.SetGSyncIndicator(enable)
                ? CliResult.Ok($"gsync-indicator = {(enable ? "on" : "off")} (was {was})", new { setting = name, value = enable, previous = was, changed = true })
                : CliResult.Fail("the driver refused the G-Sync indicator change");
        }

        if (name == "rebar-size")
        {
            if (!ulong.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out var bytes))
                return CliResult.Usage("rebar-size takes a byte count");

            var was = service.GetGlobalReBarSizeLimit();
            if (invocation.DryRun) return CliResult.Ok($"would set rebar-size to {bytes} (was {was})", new { setting = name, value = bytes, previous = was, dryRun = true });

            return service.SetGlobalReBarSizeLimit(bytes)
                ? CliResult.Ok($"rebar-size = {bytes} (was {was})", new { setting = name, value = bytes, previous = was, changed = true })
                : CliResult.Fail("the driver refused the ReBAR size change");
        }

        if (!uint.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
            return CliResult.Usage($"'{raw}' is not a number - run: rhi nvidia show to see the current values");

        var previous = ReadValue(service, name);
        if (invocation.DryRun) return CliResult.Ok($"would set {name} to {value} (was {previous})", new { setting = name, value, previous, dryRun = true });

        var applied = WriteValue(service, name, value);
        return applied
            ? CliResult.Ok($"{name} = {value} (was {previous})", new { setting = name, value, previous, changed = true })
            : CliResult.Fail($"the driver refused the {name} change (some need an elevated process)");
    }

    private static string ReadValue(DlssPresetService service, string name) => name switch
    {
        "shader-cache" => service.GetShaderCacheSize().ToString(),
        "precompile" => service.GetShaderPrecompile().ToString(),
        "gsync-mode" => service.GetGSyncMode().ToString(),
        "gsync" => service.GetGlobalGSyncEnabled().ToString(),
        "fps-cap" => service.GetGlobalFpsLimit().ToString(),
        "refresh-rate" => service.GetPreferredRefreshRate().ToString(),
        "dmfg-frames" => service.GetGlobalDmfgFrameCount().ToString(),
        "dmfg-fps" => service.GetGlobalDmfgTargetFps().ToString(),
        "rebar" => service.GetGlobalReBarEnableMode().ToString(),
        "vsync" => service.GetGlobalVSyncMode()?.ToString() ?? "-",
        _ => service.GetGlobalPowerMode()?.ToString() ?? "-",
    };

    private static bool WriteValue(DlssPresetService service, string name, uint value) => name switch
    {
        "shader-cache" => service.SetShaderCacheSize(value),
        "precompile" => service.SetShaderPrecompile(value),
        "gsync-mode" => service.SetGSyncMode(value),
        "gsync" => service.SetGlobalGSyncEnabled(value),
        "fps-cap" => service.SetGlobalFpsLimit(value),
        "refresh-rate" => service.SetPreferredRefreshRate(value),
        "dmfg-frames" => service.SetGlobalDmfgFrameCount(value),
        "dmfg-fps" => service.SetGlobalDmfgTargetFps(value),
        "rebar" => service.SetGlobalReBarEnableMode(value),
        "vsync" => service.SetGlobalVSyncMode(value),
        _ => service.SetGlobalPowerMode(value),
    };

    private static CliCommand ClearCache() => new()
    {
        Name = "nvidia clear-cache",
        Group = "nvidia",
        Summary = "Delete the NVIDIA DXCache and GLCache folders; games rebuild shaders on next launch.",
        Usage = "rhi nvidia clear-cache [--dry-run] [--yes]",
        Examples = ["rhi nvidia clear-cache --dry-run", "rhi nvidia clear-cache --yes"],
        Destructive = true,
        Run = ClearCacheRun,
    };

    private static CliResult ClearCacheRun(CliInvocation invocation)
    {
        var local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var folders = new[] { Path.Combine(local, "NVIDIA", "DXCache"), Path.Combine(local, "NVIDIA", "GLCache") };

        var files = new List<string>();
        foreach (var folder in folders)
        {
            if (!Directory.Exists(folder)) continue;
            try { files.AddRange(Directory.EnumerateFiles(folder, "*", SearchOption.AllDirectories)); }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException) { }
        }

        if (invocation.DryRun)
            return CliResult.Ok($"would delete {files.Count} shader cache file(s) from {folders.Length} folder(s)",
                new { files = files.Count, folders, dryRun = true });

        var deleted = 0;
        var skipped = 0;

        foreach (var file in files)
        {
            try { File.Delete(file); deleted++; }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException) { skipped++; }
        }

        var foldersLeft = 0;
        foreach (var folder in folders)
        {
            if (!Directory.Exists(folder)) continue;
            try
            {
                foreach (var dir in Directory.EnumerateDirectories(folder, "*", SearchOption.AllDirectories).OrderByDescending(d => d.Length))
                {
                    try { Directory.Delete(dir); }
                    catch (Exception ex) when (ex is IOException or UnauthorizedAccessException) { foldersLeft++; }
                }
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException) { foldersLeft++; }
        }

        return CliResult.Ok(
            $"cleared {deleted} shader cache file(s)" + (skipped > 0 ? $", {skipped} skipped (in use by a running game)" : ""),
            new { deleted, skipped, foldersLeft });
    }

    private static readonly string[] HdrFields = ["enable", "contrast", "saturation", "peak", "middle-grey", "debanding"];

    private static CliCommand HdrCommand() => new()
    {
        Name = "nvidia hdr",
        Group = "nvidia",
        Summary = "Read or change per-game RTX HDR values.",
        Usage = "rhi nvidia hdr <game> [show] | rhi nvidia hdr <game> set <enable|contrast|saturation|peak|middle-grey|debanding> <value> [--dry-run] [--yes]",
        Examples = ["rhi nvidia hdr cyberpunk", "rhi nvidia hdr cyberpunk set peak 1000 --yes"],
        Run = Hdr,
    };

    private static CliResult Hdr(CliInvocation invocation)
    {
        var args = invocation.Args;
        if (args.Length == 0)
            return CliResult.Usage("usage: rhi nvidia hdr <game> [show] | rhi nvidia hdr <game> set <field> <value>");

        var game = CliGames.ResolveOne(args[0], out var failure);
        if (game is null) return failure!;

        var service = CliServices.Get<DlssPresetService>();
        var action = args.Length > 1 ? args[1].ToLowerInvariant() : "show";

        if (action == "show")
        {
            var enable = service.GetRtxHdrEnable(game.Name, game.InstallPath);
            var contrast = service.GetRtxHdrContrast(game.Name, game.InstallPath);
            var saturation = service.GetRtxHdrSaturation(game.Name, game.InstallPath);
            var peak = service.GetRtxHdrPeakBrightness(game.Name, game.InstallPath);
            var middle = service.GetRtxHdrMiddleGrey(game.Name, game.InstallPath);
            var debanding = service.GetRtxHdrDebanding(game.Name, game.InstallPath);

            var text = new StringBuilder();
            text.AppendLine("enable".PadRight(14) + enable);
            text.AppendLine("contrast".PadRight(14) + contrast);
            text.AppendLine("saturation".PadRight(14) + saturation);
            text.AppendLine("peak".PadRight(14) + peak);
            text.AppendLine("middle-grey".PadRight(14) + middle);
            text.AppendLine("debanding".PadRight(14) + debanding);

            return CliResult.Ok(text.ToString().TrimEnd(),
                new { game = game.Name, enable, contrast, saturation, peak, middleGrey = middle, debanding });
        }

        if (action != "set" || args.Length < 4)
            return CliResult.Usage("usage: rhi nvidia hdr <game> set <enable|contrast|saturation|peak|middle-grey|debanding> <value> [--dry-run] [--yes]");

        var field = args[2].ToLowerInvariant();
        if (!HdrFields.Contains(field))
            return CliResult.Usage($"unknown field '{field}' - one of: {string.Join(", ", HdrFields)}");

        if (!uint.TryParse(args[3], NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
            return CliResult.Usage("value must be a number");

        if (!invocation.DryRun && !invocation.Confirmed)
            return CliResult.Usage($"refusing to change {field} without --yes: rhi nvidia hdr {game.Name} set {field} {value} --yes");

        var previous = ReadHdr(service, game, field);
        if (invocation.DryRun)
            return CliResult.Ok($"would set {field} to {value} (was {previous})",
                new { game = game.Name, field, value, previous, dryRun = true });

        var applied = WriteHdr(service, game, field, value);
        return applied
            ? CliResult.Ok($"{field} = {value} (was {previous})", new { game = game.Name, field, value, previous, changed = true })
            : CliResult.Fail($"the driver refused the {field} change for '{game.Name}'");
    }

    private static uint ReadHdr(DlssPresetService service, SavedGame game, string field) => field switch
    {
        "enable" => service.GetRtxHdrEnable(game.Name, game.InstallPath),
        "contrast" => service.GetRtxHdrContrast(game.Name, game.InstallPath),
        "saturation" => service.GetRtxHdrSaturation(game.Name, game.InstallPath),
        "peak" => service.GetRtxHdrPeakBrightness(game.Name, game.InstallPath),
        "middle-grey" => service.GetRtxHdrMiddleGrey(game.Name, game.InstallPath),
        _ => service.GetRtxHdrDebanding(game.Name, game.InstallPath),
    };

    private static bool WriteHdr(DlssPresetService service, SavedGame game, string field, uint value) => field switch
    {
        "enable" => service.SetRtxHdrEnable(game.Name, game.InstallPath, value),
        "contrast" => service.SetRtxHdrContrast(game.Name, game.InstallPath, value),
        "saturation" => service.SetRtxHdrSaturation(game.Name, game.InstallPath, value),
        "peak" => service.SetRtxHdrPeakBrightness(game.Name, game.InstallPath, value),
        "middle-grey" => service.SetRtxHdrMiddleGrey(game.Name, game.InstallPath, value),
        _ => service.SetRtxHdrDebanding(game.Name, game.InstallPath, value),
    };

    private static string BackupPath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RHI", "nvidia_profiles_backup.json");

    private static CliCommand ProfileCommand() => new()
    {
        Name = "nvidia profile",
        Group = "nvidia",
        Summary = "Create missing driver profiles, back them up / restore them, or reset them.",
        Usage = "rhi nvidia profile <create-missing|export [--out <path>]|import [--in <path>]|reset> [--dry-run] [--yes]",
        Examples = ["rhi nvidia profile export", "rhi nvidia profile import --yes", "rhi nvidia profile reset --yes"],
        Run = Profile,
    };

    private static CliResult Profile(CliInvocation invocation)
    {
        var action = (invocation.Operand(0) ?? "").ToLowerInvariant();

        var library = CliGames.LoadLibrary(out var failure);
        if (library is null) return failure!;

        var hidden = library.HiddenGames ?? new HashSet<string>();
        var games = CliGames.AllGames(library)
            .Where(g => !string.IsNullOrWhiteSpace(g.InstallPath))
            .Where(g => !hidden.Contains(g.Name + "|" + g.Source) && !hidden.Contains(g.Name))
            .ToList();

        if (games.Count == 0) return CliResult.Ok("no visible games in the library");

        var service = CliServices.Get<DlssPresetService>();
        service.Initialize();
        if (!service.IsSupported) return CliResult.Fail("NVIDIA driver settings are not available on this machine (NVAPI/DRS could not be opened)");

        var pairs = games.Select(g => (g.Name, g.InstallPath)).ToList();

        switch (action)
        {
            case "create-missing":
            {
                if (invocation.DryRun)
                    return CliResult.Ok($"would check {games.Count} game(s) for a missing driver profile", new { games = games.Count, dryRun = true });

                if (!invocation.Confirmed)
                    return CliResult.Usage("refusing to write driver profiles without --yes: rhi nvidia profile create-missing --yes");

                var created = new List<string>();
                foreach (var game in games)
                    if (service.EnsureProfileExists(game.Name, game.InstallPath)) created.Add(game.Name);

                return CliResult.Ok(
                    created.Count == 0 ? "every game already has a driver profile" : $"created {created.Count} profile(s): {string.Join(", ", created)}",
                    new { created = created.Count, games = created });
            }

            case "export":
            {
                var target = invocation.Flag("out") ?? BackupPath;
                var folder = Path.GetDirectoryName(target);
                if (!string.IsNullOrEmpty(folder)) Directory.CreateDirectory(folder);

                var data = service.ExportProfiles(pairs);
                File.WriteAllText(target, JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true }));

                return CliResult.Ok($"exported {data.Count} profile(s) to {target}", new { target, count = data.Count });
            }

            case "import":
            {
                var source = invocation.Flag("in") ?? BackupPath;
                if (!File.Exists(source)) return CliResult.Fail($"no profile backup at {source} - export one first");

                if (invocation.DryRun)
                    return CliResult.Ok($"would import driver profiles from {source}", new { source, dryRun = true });

                if (!invocation.Confirmed)
                    return CliResult.Usage($"refusing to overwrite driver profiles without --yes: rhi nvidia profile import --yes");

                var data = JsonSerializer.Deserialize<Dictionary<string, object>>(File.ReadAllText(source));
                if (data is null) return CliResult.Fail($"{source} is not a driver profile backup");

                var imported = service.ImportProfiles(data);
                return CliResult.Ok($"imported {imported} profile(s) from {source}", new { source, imported });
            }

            case "reset":
            {
                if (invocation.DryRun)
                    return CliResult.Ok($"would reset {games.Count} driver profile(s) to defaults", new { games = games.Count, dryRun = true });

                if (!invocation.Confirmed)
                    return CliResult.Usage("refusing to reset driver profiles without --yes: rhi nvidia profile reset --yes");

                var reset = service.ResetAllProfiles(pairs);
                return CliResult.Ok($"reset {reset} driver profile(s) to defaults", new { reset });
            }

            default:
                return CliResult.Usage("usage: rhi nvidia profile <create-missing|export|import|reset> [--dry-run] [--yes]");
        }
    }

    private static readonly string[] GameFields =
    [
        "smooth-motion", "smooth-motion-apis", "smooth-motion-flip-fs", "smooth-motion-flip-win",
        "vulkan-present", "vulkan-present-flags", "rebar", "rebar-mode", "rebar-size",
    ];

    private static CliCommand GameCommand() => new()
    {
        Name = "nvidia game",
        Group = "nvidia",
        Summary = "Read or change per-game NVIDIA driver values: Smooth Motion, Vulkan present, ReBAR.",
        Usage = "rhi nvidia game <game> [show] | rhi nvidia game <game> set <field> <value> [--dry-run] [--yes]",
        Examples = ["rhi nvidia game cyberpunk", "rhi nvidia game cyberpunk set smooth-motion 1 --yes"],
        Run = Game,
    };

    private static CliResult Game(CliInvocation invocation)
    {
        var args = invocation.Args;
        if (args.Length == 0)
            return CliResult.Usage("usage: rhi nvidia game <game> [show] | rhi nvidia game <game> set <field> <value>");

        var game = CliGames.ResolveOne(args[0], out var failure);
        if (game is null) return failure!;

        var service = CliServices.Get<DlssPresetService>();
        var action = args.Length > 1 ? args[1].ToLowerInvariant() : "show";

        if (action == "show")
        {
            var text = new StringBuilder();
            void Line(string name, string value) => text.AppendLine(name.PadRight(24) + value);

            Line("smooth-motion", service.GetSmoothMotionEnable(game.Name, game.InstallPath).ToString());
            Line("smooth-motion-apis", service.GetSmoothMotionApis(game.Name, game.InstallPath).ToString());
            Line("smooth-motion-flip-fs", service.GetSmoothMotionFlipPacingFs(game.Name, game.InstallPath).ToString());
            Line("smooth-motion-flip-win", "(write only)");
            Line("vulkan-present", service.GetVulkanPresentMethod(game.Name, game.InstallPath).ToString());
            Line("vulkan-present-flags", service.GetVulkanPresentMethodFlags(game.Name, game.InstallPath).ToString());
            Line("rebar", service.GetReBarEnableMode(game.Name, game.InstallPath).ToString());
            Line("rebar-mode", service.GetReBarMode(game.Name, game.InstallPath).ToString());
            Line("rebar-size", service.GetReBarSizeLimit(game.Name, game.InstallPath).ToString());

            return CliResult.Ok(text.ToString().TrimEnd(), new
            {
                game = game.Name,
                smoothMotion = service.GetSmoothMotionEnable(game.Name, game.InstallPath),
                smoothMotionApis = service.GetSmoothMotionApis(game.Name, game.InstallPath),
                smoothMotionFlipPacingFs = service.GetSmoothMotionFlipPacingFs(game.Name, game.InstallPath),
                vulkanPresentMethod = service.GetVulkanPresentMethod(game.Name, game.InstallPath),
                vulkanPresentFlags = service.GetVulkanPresentMethodFlags(game.Name, game.InstallPath),
                rebar = service.GetReBarEnableMode(game.Name, game.InstallPath),
                rebarMode = service.GetReBarMode(game.Name, game.InstallPath),
                rebarSize = service.GetReBarSizeLimit(game.Name, game.InstallPath),
            });
        }

        if (action != "set" || args.Length < 4)
            return CliResult.Usage("usage: rhi nvidia game <game> set <field> <value> [--dry-run] [--yes]");

        var field = args[2].ToLowerInvariant();
        if (!GameFields.Contains(field))
            return CliResult.Usage($"unknown field '{field}' - one of: {string.Join(", ", GameFields)}");

        if (!ulong.TryParse(args[3], NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
            return CliResult.Usage("value must be a number");
        if (field != "rebar-size" && value > uint.MaxValue)
            return CliResult.Usage("value is too large for this field");

        if (!invocation.DryRun && !invocation.Confirmed)
            return CliResult.Usage($"refusing to change {field} without --yes: rhi nvidia game {game.Name} set {field} {value} --yes");

        var previous = ReadGameValue(service, game, field);
        if (invocation.DryRun)
            return CliResult.Ok($"would set {field} to {value} (was {previous})",
                new { game = game.Name, field, value, previous, dryRun = true });

        var applied = WriteGameValue(service, game, field, value);
        return applied
            ? CliResult.Ok($"{field} = {value} (was {previous})", new { game = game.Name, field, value, previous, changed = true })
            : CliResult.Fail($"the driver refused the {field} change for '{game.Name}' (some need an elevated process)");
    }

    private static string ReadGameValue(DlssPresetService service, SavedGame game, string field) => field switch
    {
        "smooth-motion" => service.GetSmoothMotionEnable(game.Name, game.InstallPath).ToString(),
        "smooth-motion-apis" => service.GetSmoothMotionApis(game.Name, game.InstallPath).ToString(),
        "smooth-motion-flip-fs" => service.GetSmoothMotionFlipPacingFs(game.Name, game.InstallPath).ToString(),
        "smooth-motion-flip-win" => "(write only)",
        "vulkan-present" => service.GetVulkanPresentMethod(game.Name, game.InstallPath).ToString(),
        "vulkan-present-flags" => service.GetVulkanPresentMethodFlags(game.Name, game.InstallPath).ToString(),
        "rebar" => service.GetReBarEnableMode(game.Name, game.InstallPath).ToString(),
        "rebar-mode" => service.GetReBarMode(game.Name, game.InstallPath).ToString(),
        _ => service.GetReBarSizeLimit(game.Name, game.InstallPath).ToString(),
    };

    private static bool WriteGameValue(DlssPresetService service, SavedGame game, string field, ulong value) => field switch
    {
        "smooth-motion" => service.SetSmoothMotionEnable(game.Name, game.InstallPath, (uint)value),
        "smooth-motion-apis" => service.SetSmoothMotionApis(game.Name, game.InstallPath, (uint)value),
        "smooth-motion-flip-fs" => service.SetSmoothMotionFlipPacingFs(game.Name, game.InstallPath, (uint)value),
        "smooth-motion-flip-win" => service.SetSmoothMotionFlipPacingWin(game.Name, game.InstallPath, (uint)value),
        "vulkan-present" => service.SetVulkanPresentMethod(game.Name, game.InstallPath, (uint)value),
        "vulkan-present-flags" => service.SetVulkanPresentMethodFlags(game.Name, game.InstallPath, (uint)value),
        "rebar" => service.SetReBarEnableMode(game.Name, game.InstallPath, (uint)value),
        "rebar-mode" => service.SetReBarMode(game.Name, game.InstallPath, (uint)value),
        _ => service.SetReBarSizeLimit(game.Name, game.InstallPath, value),
    };
}
