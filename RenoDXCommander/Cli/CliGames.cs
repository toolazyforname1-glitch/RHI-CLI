using System.Text;
using RenoDXCommander.Models;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// Read-only view of the game library the GUI builds. Everything comes from game_library.json and
/// the same IGameLibraryService the app uses, so names, stores and versions match the cards.
/// </summary>
internal static class CliGames
{
    internal static readonly CliCommand[] All = [ListCommand(), ShowCommand(), LaunchCommand(), AddCommand(), RemoveCommand(), RefreshCommand()];

    private static CliCommand LaunchCommand() => new()
    {
        Name = "game launch",
        Group = "games",
        Summary = "Launch a game the way the Launch button does (store, exe, HDR and resolution toggles).",
        Usage = "rhi game launch <name> [--json]",
        Examples = ["rhi game launch \"Cyberpunk 2077\""],
        RequiresGui = true,
        RunAsync = LaunchAsync,
    };

    private static async Task<CliResult> LaunchAsync(CliInvocation invocation)
    {
        var window = CliRuntime.Window;
        if (window is null)
            return CliResult.Fail("RHI is not running - launching goes through RHI's window (store launch, HDR/resolution toggles). Start RHI and run this again.");

        var name = invocation.OperandText;
        if (string.IsNullOrWhiteSpace(name)) return CliResult.Usage("usage: rhi game launch <name>");

        var cards = window.ViewModel.AllCards;
        var card = cards.FirstOrDefault(c => string.Equals(c.GameName, name, StringComparison.OrdinalIgnoreCase))
                   ?? cards.FirstOrDefault(c => c.GameName.Contains(name, StringComparison.OrdinalIgnoreCase));

        if (card is null) return CliResult.Fail($"no game matching '{name}' in RHI - run: rhi game list");

        if (!await window.LaunchGameAsync(card))
            return CliResult.Fail($"could not launch '{card.GameName}' - no launch method matched (store id, exe override or game exe)");

        return CliResult.Ok($"launching '{card.GameName}'", new { game = card.GameName, launched = true });
    }

    private sealed record GameRow(string Name, string Store, string Path, string ReShade, string RenoDX, string Dxvk, bool Hidden, bool Favourite, string Updates);

    private static CliCommand ListCommand() => new()
    {
        Name = "game list",
        Group = "games",
        Summary = "List every game with its store and installed components.",
        Usage = "rhi game list [--installed] [--hidden] [--json]",
        Examples = ["rhi game list", "rhi game list --installed", "rhi game list --json"],
        Run = List,
    };

    private static CliCommand ShowCommand() => new()
    {
        Name = "game show",
        Group = "games",
        Summary = "Show one game: path, store, engine, bitness and installed components.",
        Usage = "rhi game show <name> [--json]",
        Examples = ["rhi game show \"Cyberpunk 2077\"", "rhi game show cyberpunk"],
        Run = Show,
    };

    private static CliResult List(CliInvocation invocation)
    {
        var library = LoadLibrary(out var failure);
        if (library is null) return failure!;

        var rows = new List<GameRow>();
        foreach (var game in AllGames(library))
        {
            var row = Describe(library, game);
            if (invocation.Has("installed") && row.ReShade.Length == 0 && row.RenoDX.Length == 0 && row.Dxvk.Length == 0) continue;
            if (!invocation.Has("hidden") && row.Hidden) continue;
            rows.Add(row);
        }

        var text = new StringBuilder();
        text.AppendLine("NAME".PadRight(34) + "STORE".PadRight(9) + "RESHADE".PadRight(10) + "RENODX".PadRight(10) + "DXVK".PadRight(9) + "PATH");
        foreach (var row in rows)
        {
            text.AppendLine(
                Clip(row.Name, 33).PadRight(34) +
                Clip(row.Store, 8).PadRight(9) +
                (row.ReShade.Length == 0 ? "-" : row.ReShade).PadRight(10) +
                (row.RenoDX.Length == 0 ? "-" : row.RenoDX).PadRight(10) +
                (row.Dxvk.Length == 0 ? "-" : row.Dxvk).PadRight(9) +
                row.Path);
        }
        text.AppendLine();
        text.Append($"{rows.Count} of {AllGames(library).Count()} games");

        return CliResult.Ok(text.ToString(), new
        {
            count = rows.Count,
            total = AllGames(library).Count(),
            lastScanned = library.LastScanned,
            games = rows.Select(r => new { r.Name, r.Store, r.Path, r.ReShade, r.RenoDX, r.Dxvk, r.Hidden, r.Favourite, r.Updates }).ToArray(),
        });
    }

    private static CliResult Show(CliInvocation invocation)
    {
        var wanted = invocation.OperandText;
        if (string.IsNullOrWhiteSpace(wanted)) return CliResult.Usage("usage: rhi game show <name>");

        var library = LoadLibrary(out var failure);
        if (library is null) return failure!;

        var games = AllGames(library).ToList();
        var match = games.FirstOrDefault(g => string.Equals(g.Name, wanted, StringComparison.OrdinalIgnoreCase))
                    ?? games.Where(g => g.Name.Contains(wanted, StringComparison.OrdinalIgnoreCase)).Take(2).ToList() switch
                    {
                        { Count: 1 } single => single[0],
                        _ => null,
                    };

        if (match is null)
        {
            var candidates = games
                .Where(g => g.Name.Contains(wanted, StringComparison.OrdinalIgnoreCase))
                .Select(g => g.Name)
                .Take(8)
                .ToList();
            return candidates.Count == 0
                ? CliResult.Fail($"no game matching '{wanted}' - run: rhi game list")
                : CliResult.Usage($"'{wanted}' matches {candidates.Count} games: {string.Join(", ", candidates)}");
        }

        var row = Describe(library, match);
        var engine = LookupString(library.EngineTypeCache, match.InstallPath);
        if (engine.Length == 0) engine = "unknown";
        var resolved = LookupString(library.ResolvedPathCache, match.InstallPath);
        if (resolved.Length == 0) resolved = match.InstallPath;
        var bitness = LookupBitness(library.BitnessCache, resolved);
        if (bitness.Length == 0) bitness = "unknown";

        var text = new StringBuilder();
        void Line(string label, string value) => text.AppendLine(label.PadRight(14) + value);
        Line("name", row.Name);
        Line("store", row.Store);
        Line("path", row.Path);
        Line("engine", engine);
        Line("bitness", bitness);
        Line("reshade", row.ReShade.Length == 0 ? "-" : row.ReShade);
        Line("renodx", row.RenoDX.Length == 0 ? "-" : row.RenoDX);
        Line("dxvk", row.Dxvk.Length == 0 ? "-" : row.Dxvk);
        Line("updates", row.Updates.Length == 0 ? "-" : row.Updates);
        Line("hidden", row.Hidden ? "yes" : "no");
        Line("favourite", row.Favourite ? "yes" : "no");
        Line("manual", match.IsManuallyAdded ? "yes" : "no");

        return CliResult.Ok(text.ToString().TrimEnd(), new
        {
            name = row.Name,
            store = row.Store,
            path = row.Path,
            engine,
            bitness,
            reshade = row.ReShade,
            renodx = row.RenoDX,
            dxvk = row.Dxvk,
            updates = row.Updates,
            hidden = row.Hidden,
            favourite = row.Favourite,
            manual = match.IsManuallyAdded,
            steamAppId = match.SteamAppId,
        });
    }

    internal static SavedGameLibrary? LoadLibrary(out CliResult? failure)
    {
        failure = null;
        var library = CliServices.Get<IGameLibraryService>().Load();
        if (library is not null) return library;
        failure = CliResult.Fail("no game library yet - open RHI and let it scan, then try again");
        return null;
    }

    internal static IEnumerable<SavedGame> AllGames(SavedGameLibrary library) =>
        library.Games.Concat(library.ManualGames)
            .GroupBy(g => Key(g.Name, g.Source), StringComparer.OrdinalIgnoreCase)
            .Select(group => group.First());

    private static GameRow Describe(SavedGameLibrary library, SavedGame game)
    {
        var key = Key(game.Name, game.Source);
        return new GameRow(
            game.Name,
            string.IsNullOrWhiteSpace(game.Source) ? "manual" : game.Source,
            game.InstallPath,
            Version(library.RsInstalledVersions, key, game.Name),
            Version(library.RdxInstalledVersions, key, game.Name),
            Version(library.DxvkInstalledVersions, key, game.Name),
            Contains(library.HiddenGames, key, game.Name),
            Contains(library.FavouriteGames, key, game.Name),
            Value(library.UpdateAvailableSnapshot, key, game.Name));
    }

    private static string Key(string name, string store) => name + "|" + (store ?? "");

    private static string Version(Dictionary<string, string>? map, string key, string name) =>
        map is null ? "" : Value(map, key, name);

    private static string Value(Dictionary<string, string>? map, string key, string name)
    {
        if (map is null) return "";
        if (map.TryGetValue(key, out var exact)) return exact ?? "";
        if (map.TryGetValue(name, out var bare)) return bare ?? "";
        foreach (var pair in map)
        {
            if (string.Equals(pair.Key, key, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(pair.Key, name, StringComparison.OrdinalIgnoreCase))
                return pair.Value ?? "";
        }
        return "";
    }

    private static bool Contains(HashSet<string> set, string key, string name) =>
        set.Contains(key) || set.Contains(name);

    internal static string LookupString(Dictionary<string, string>? map, string path)
    {
        if (map is null || string.IsNullOrWhiteSpace(path)) return "";
        if (map.TryGetValue(path, out var exact)) return exact ?? "";
        foreach (var pair in map)
        {
            if (string.Equals(pair.Key, path, StringComparison.OrdinalIgnoreCase)) return pair.Value ?? "";
        }
        return "";
    }

    internal static string LookupBitness(Dictionary<string, MachineType>? map, string path)
    {
        if (map is null || string.IsNullOrWhiteSpace(path)) return "";
        if (map.TryGetValue(path, out var exact)) return exact.ToString();
        foreach (var pair in map)
        {
            if (string.Equals(pair.Key, path, StringComparison.OrdinalIgnoreCase)) return pair.Value.ToString();
        }
        return "";
    }

    internal static SavedGame? ResolveOne(CliInvocation invocation, out CliResult? failure) =>
        ResolveOne(invocation.OperandText, out failure);

    internal static SavedGame? ResolveOne(string name, out CliResult? failure)
    {
        failure = null;
        if (string.IsNullOrWhiteSpace(name))
        {
            failure = CliResult.Usage("a game name is required");
            return null;
        }

        var library = LoadLibrary(out var loadFailure);
        if (library is null)
        {
            failure = loadFailure;
            return null;
        }

        var games = AllGames(library).ToList();
        var exact = games.FirstOrDefault(g => string.Equals(g.Name, name, StringComparison.OrdinalIgnoreCase));
        if (exact is not null) return exact;

        var partial = games.Where(g => g.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        if (partial.Count == 1) return partial[0];

        failure = partial.Count == 0
            ? CliResult.Fail($"no game matching '{name}' - run: rhi game list")
            : CliResult.Usage($"'{name}' matches {partial.Count} games: {string.Join(", ", partial.Take(8).Select(g => g.Name))}");
        return null;
    }

    private static string Clip(string text, int width) =>
        text.Length <= width ? text : text[..(width - 1)] + "\u2026";

    private static CliCommand AddCommand() => new()
    {
        Name = "game add",
        Group = "games",
        Summary = "Add a game folder to the library as a manual entry.",
        Usage = "rhi game add <name> <path> [--dry-run] [--yes] [--json]",
        Examples = ["rhi game add \"My Game\" \"D:\\Games\\MyGame\" --yes"],
        Destructive = true,
        Run = Add,
    };

    private static CliResult Add(CliInvocation invocation)
    {
        var args = invocation.Args;
        if (args.Length < 2) return CliResult.Usage("usage: rhi game add <name> <path> [--dry-run] [--yes]");

        var name = args[0];
        var path = string.Join(' ', args[1..]).Trim().Trim('"');

        if (!Directory.Exists(path)) return CliResult.Fail($"folder does not exist: {path}");
        if (!Directory.EnumerateFiles(path, "*.exe", SearchOption.TopDirectoryOnly).Any())
            return CliResult.Fail($"no .exe directly in {path} - point at the game folder, not its parent");

        var service = CliServices.Get<IGameLibraryService>();
        var library = service.Load();
        var games = library is null ? new List<DetectedGame>() : service.ToDetectedGames(library);
        var manual = library is null ? new List<DetectedGame>() : service.ToManualGames(library);

        if (games.Concat(manual).Any(g => string.Equals(g.Name, name, StringComparison.OrdinalIgnoreCase)
                                       && string.Equals(g.InstallPath, path, StringComparison.OrdinalIgnoreCase)))
            return CliResult.Fail($"'{name}' is already in the library at {path}");

        if (games.Concat(manual).Any(g => string.Equals(g.InstallPath, path, StringComparison.OrdinalIgnoreCase)))
            return CliResult.Fail($"another entry already uses {path}");

        if (invocation.DryRun)
            return CliResult.Ok($"would add '{name}' at {path}", new { name, path, dryRun = true });

        manual.Add(new DetectedGame
        {
            Name = name,
            InstallPath = path,
            Source = "Manual",
            IsManuallyAdded = true,
        });

        SaveLibrary(service, library, games, manual);

        return CliResult.Ok($"added '{name}' at {path} (manual entry)", new { name, path, manual = true });
    }

    private static void SaveLibrary(IGameLibraryService service, SavedGameLibrary? library, List<DetectedGame> games, List<DetectedGame> manual)
    {
        service.Save(games, library?.AddonScanCache ?? new Dictionary<string, bool>(),
            library?.HiddenGames ?? new HashSet<string>(), library?.FavouriteGames ?? new HashSet<string>(), manual,
            library?.EngineTypeCache, library?.ResolvedPathCache, library?.AddonFileCache, library?.BitnessCache,
            library?.LastSelectedGame, library?.DxvkEnabledGames, library?.DxvkInstalledVersions,
            library?.ExcludeFromUpdateAllDxvk, library?.UpdateAvailableSnapshot, library?.DlssPathsCache,
            library?.RsInstalledVersions, library?.RdxInstalledVersions, library?.EngineFolderStamps);
    }

    private static CliCommand RemoveCommand() => new()
    {
        Name = "game remove",
        Group = "games",
        Summary = "Remove a manual library entry (detected games return on the next scan).",
        Usage = "rhi game remove <name> [--dry-run] [--yes] [--json]",
        Examples = ["rhi game remove \"My Game\" --yes"],
        Destructive = true,
        Run = Remove,
    };

    private static CliResult Remove(CliInvocation invocation)
    {
        var name = invocation.OperandText;
        if (string.IsNullOrWhiteSpace(name)) return CliResult.Usage("usage: rhi game remove <name> [--dry-run] [--yes]");

        var service = CliServices.Get<IGameLibraryService>();
        var library = service.Load();
        if (library is null) return CliResult.Fail("no game library yet - nothing to remove");

        var manual = service.ToManualGames(library);
        var match = manual.FirstOrDefault(g => string.Equals(g.Name, name, StringComparison.OrdinalIgnoreCase))
                    ?? manual.FirstOrDefault(g => g.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

        if (match is null)
        {
            var detected = service.ToDetectedGames(library).Any(g => string.Equals(g.Name, name, StringComparison.OrdinalIgnoreCase));
            return detected
                ? CliResult.Fail($"'{name}' is a detected game - the CLI removes manual entries only (a detected game returns on the next scan)")
                : CliResult.Fail($"no manual game matching '{name}'");
        }

        if (invocation.DryRun)
            return CliResult.Ok($"would remove '{match.Name}' from the library", new { name = match.Name, dryRun = true });

        manual.RemoveAll(g => string.Equals(g.Name, match.Name, StringComparison.OrdinalIgnoreCase));
        SaveLibrary(service, library, service.ToDetectedGames(library), manual);

        return CliResult.Ok($"removed '{match.Name}' from the library", new { name = match.Name, removed = true });
    }

    private static CliCommand RefreshCommand() => new()
    {
        Name = "game refresh",
        Group = "games",
        Summary = "Rescan the library, exactly like the Refresh button.",
        Usage = "rhi game refresh [--json]",
        Examples = ["rhi game refresh"],
        RequiresGui = true,
        Run = invocation => RefreshLibrary(invocation).GetAwaiter().GetResult(),
    };

    private static async Task<CliResult> RefreshLibrary(CliInvocation invocation)
    {
        var window = CliRuntime.Window;
        if (window is null)
            return CliResult.Fail("RHI is not running - a rescan needs RHI's scanner. Start RHI and run this again.");

        await window.ViewModel.RefreshAsync();
        return CliResult.Ok("library refreshed", new { refreshed = true, games = window.ViewModel.AllCards.Count });
    }
}
