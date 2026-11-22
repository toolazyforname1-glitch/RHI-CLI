using System.Text;
using System.Text.Json;
using RenoDXCommander.Models;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// Display modes: a per-game preset of HDR, resolution, ReShade keys, frame-limiter and NVIDIA values
/// that can be switched as one unit. The mode list is global; the active mode is per game, and Off is
/// the absence of a mode. Values live in settings.json under "DisplayModes"; the switch writes the
/// game's ini/driver values.
/// </summary>
internal static class CliDisplay
{
    internal static readonly CliCommand[] All = [ModesCommand(), ShowCommand(), SetCommand()];

    private static CliCommand ModesCommand() => new()
    {
        Name = "display modes",
        Group = "display",
        Summary = "List the display modes and which one this game is in.",
        Usage = "rhi display modes <game> [--json]",
        Examples = ["rhi display modes cyberpunk"],
        Run = Modes,
    };

    private static CliCommand ShowCommand() => new()
    {
        Name = "display show",
        Group = "display",
        Summary = "Show every display-mode item for a game: its current value, or why it is unavailable.",
        Usage = "rhi display show <game> [--json]",
        Examples = ["rhi display show cyberpunk"],
        Run = Show,
    };

    private static CliCommand SetCommand() => new()
    {
        Name = "display set",
        Group = "display",
        Summary = "Switch a game to a display mode, or turn modes off.",
        Usage = "rhi display set <game> <mode|off> [--dry-run] [--yes] [--json]",
        Examples = ["rhi display set cyberpunk monitor --yes", "rhi display set cyberpunk off --dry-run"],
        Destructive = true,
        Run = Set,
    };

    private static CliResult Modes(CliInvocation invocation)
    {
        var game = CliGames.ResolveOne(invocation.Operand(0), out var failure);
        if (game is null) return failure!;

        var store = LoadStore();
        var key = GameKey.From(game.Name, game.Source ?? "").ToKey();
        var active = store.ActiveModeId(key);

        var text = new StringBuilder();
        text.AppendLine("ID".PadRight(14) + "NAME".PadRight(28) + "ACTIVE");
        foreach (var mode in store.Modes)
            text.AppendLine(mode.Id.PadRight(14) + mode.Name.PadRight(28) + (string.Equals(mode.Id, active, StringComparison.OrdinalIgnoreCase) ? "yes" : ""));

        text.AppendLine();
        text.Append(active is null ? "currently Off" : "currently " + (store.NameOf(active) ?? active));

        return CliResult.Ok(text.ToString(), new
        {
            game = game.Name,
            activeModeId = active,
            modes = store.Modes.Select(m => new { m.Id, m.Name, active = string.Equals(m.Id, active, StringComparison.OrdinalIgnoreCase) }).ToArray(),
        });
    }

    private static CliResult Show(CliInvocation invocation)
    {
        var game = CliGames.ResolveOne(invocation.Operand(0), out var failure);
        if (game is null) return failure!;

        var store = LoadStore();
        var context = BuildContext(game);
        var service = new DisplayModeService(store);

        var items = DisplayModeItems.All;
        var current = service.ReadCurrent(context, items);

        var text = new StringBuilder();
        var rows = new List<object>();

        foreach (var item in items)
        {
            current.TryGetValue(item.Key, out var value);
            var reason = DisplayModeService.Unavailable(context, item);
            text.AppendLine(item.Key.PadRight(20) + (reason ?? value ?? "(launch item)"));
            rows.Add(new { key = item.Key, label = item.Label, value, reason, atLaunch = item.AtLaunch });
        }

        return CliResult.Ok(text.ToString().TrimEnd(), new { game = game.Name, items = rows });
    }

    private static CliResult Set(CliInvocation invocation)
    {
        var args = invocation.Args;
        if (args.Length < 2)
            return CliResult.Usage("usage: rhi display set <game> <mode|off> [--dry-run] [--yes]");

        var game = CliGames.ResolveOne(args[0], out var failure);
        if (game is null) return failure!;

        var store = LoadStore();
        var context = BuildContext(game);
        var service = new DisplayModeService(store);

        if (args[1].Equals("off", StringComparison.OrdinalIgnoreCase))
        {
            if (invocation.DryRun)
                return CliResult.Ok($"would turn display modes off for '{game.Name}'", new { game = game.Name, mode = "off", dryRun = true });

            service.TurnOff(context);
            if (!Persist(store)) return CliResult.Fail($"could not save the display-mode state to {SettingsFile.FilePath}");

            return CliResult.Ok($"display modes are off for '{game.Name}'", new { game = game.Name, mode = "off", turnedOff = true });
        }

        var wanted = args[1];
        var mode = store.Modes.FirstOrDefault(m => m.Id.Equals(wanted, StringComparison.OrdinalIgnoreCase))
                   ?? store.Modes.FirstOrDefault(m => m.Name.Equals(wanted, StringComparison.OrdinalIgnoreCase))
                   ?? store.Modes.FirstOrDefault(m => m.Name.Contains(wanted, StringComparison.OrdinalIgnoreCase));

        if (mode is null)
            return CliResult.Fail($"no display mode matching '{wanted}' - run: rhi display modes \"{game.Name}\"");

        var items = DisplayModeItems.All;

        if (invocation.DryRun)
        {
            var current = service.ReadCurrent(context, items);
            var text = new StringBuilder();

            foreach (var item in items)
            {
                var reason = DisplayModeService.Unavailable(context, item);
                if (reason is not null)
                {
                    text.AppendLine(item.Key.PadRight(20) + "skip: " + reason);
                    continue;
                }

                var planned = service.ValueFor(context, mode.Id, item);
                current.TryGetValue(item.Key, out var now);

                if (item.AtLaunch)
                {
                    text.AppendLine(item.Key.PadRight(20) + (planned ?? "(no value)") + "  (applied when RHI launches the game)");
                    continue;
                }

                if (planned is null) continue;
                text.AppendLine(item.Key.PadRight(20) + (now ?? "-") + " -> " + planned);
            }

            return CliResult.Ok($"would switch '{game.Name}' to '{mode.Name}'\n" + text.ToString().TrimEnd(),
                new { game = game.Name, mode = mode.Id, dryRun = true });
        }

        var result = service.Switch(context, mode.Id);
        if (!Persist(store)) return CliResult.Fail($"could not save the display-mode state to {SettingsFile.FilePath}");

        var problems = result.Items.Where(i => !string.IsNullOrEmpty(i.Problem))
            .Select(i => i.Item.Key + ": " + i.Problem)
            .ToList();

        var note = SettingsFile.Load().TryGetValue("DisplayModesEnabled", out var enabled) && enabled == "true"
            ? ""
            : " (DisplayModesEnabled is off, so RHI will not apply launch items until it is enabled)";

        var message = $"'{game.Name}' is now on '{result.ModeName}' - {result.WrittenCount} item(s) written{note}";
        if (problems.Count > 0) message += "\nskipped: " + string.Join("\n", problems);

        return CliResult.Ok(message, new { game = game.Name, mode = result.ModeName, written = result.WrittenCount, problems });
    }

    private static DisplayModeStore LoadStore()
    {
        var store = CliServices.Get<IGameNameService>().DisplayModes;
        var settings = SettingsFile.Load();

        if (settings.TryGetValue("DisplayModes", out var json) && !string.IsNullOrWhiteSpace(json))
        {
            try
            {
                var data = JsonSerializer.Deserialize<DisplayModeData>(json);
                if (data is not null) store.Load(data);
            }
            catch (JsonException ex)
            {
                CrashReporter.Log($"[cli:display] could not parse the DisplayModes blob - {ex.Message}");
            }
        }

        return store;
    }

    private static DisplayModeContext BuildContext(SavedGame game)
    {
        var names = CliServices.Get<IGameNameService>();
        var dlss = CliServices.Get<DlssPresetService>();
        dlss.Initialize();

        var key = GameKey.From(game.Name, game.Source ?? "").ToKey();
        var hasRenoDx = CliServices.Get<IModInstallService>().FindRecord(game.Name, game.InstallPath) is not null;
        var rtxHdr = names.RtxHdrGames.Contains(key) || names.RtxHdrGames.Contains(game.Name);

        return new DisplayModeContext
        {
            GameName = game.Name,
            Store = game.Source ?? "",
            InstallPath = game.InstallPath,
            HasRenoDx = hasRenoDx,
            IsRtxHdrEnabled = rtxHdr,
            Nvidia = dlss,
        };
    }

    private static bool Persist(DisplayModeStore store) =>
        SettingsFile.Update(s => s["DisplayModes"] = JsonSerializer.Serialize(store.Snapshot()), "cli:display");
}
