using System.Text;
using RenoDXCommander.Models;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

internal static class CliShots
{
    internal static readonly CliCommand[] All = [ListCommand(), FolderCommand(), DeleteCommand()];

    private static CliCommand DeleteCommand() => new()
    {
        Name = "shot delete",
        Group = "screenshots",
        Summary = "Delete one screenshot. Needs --yes.",
        Usage = "rhi shot delete <game> <name> [--yes] [--dry-run]",
        Examples = ["rhi shot delete \"Alan Wake 2\" \"AlanWake2 2026-01-01 12-00-00.png\" --yes"],
        Destructive = true,
        Run = Delete,
    };

    private static CliResult Delete(CliInvocation invocation)
    {
        var args = invocation.Args;
        if (args.Length < 2) return CliResult.Usage("usage: rhi shot delete <game> <name> [--yes] [--dry-run]");

        var game = CliGames.ResolveOne(args[0], out var failure);
        if (game is null) return failure!;

        var folder = ResolveFolder(SettingsFile.Load(), game);
        if (folder is null) return CliResult.Fail($"no screenshot folder for '{game.Name}'");

        var shots = ListShots(game, folder);
        var wanted = string.Join(' ', args[1..]);
        var matches = shots.Where(s => s.Name.Equals(wanted, StringComparison.OrdinalIgnoreCase)).ToList();
        if (matches.Count == 0) matches = shots.Where(s => s.Name.Contains(wanted, StringComparison.OrdinalIgnoreCase)).ToList();

        if (matches.Count == 0) return CliResult.Fail($"no screenshot matching '{wanted}' in {folder}");
        if (matches.Count > 1)
            return CliResult.Usage($"'{wanted}' matches {matches.Count} screenshots: {string.Join(", ", matches.Take(5).Select(m => m.Name))}");

        var file = matches[0];
        if (invocation.DryRun)
            return CliResult.Ok($"would delete {file.Name} ({file.Length} bytes)", new { file = file.FullName, bytes = file.Length, dryRun = true });

        if (!invocation.Confirmed)
            return CliResult.Usage($"refusing to delete without --yes: rhi shot delete \"{game.Name}\" \"{file.Name}\" --yes");

        try
        {
            file.Delete();
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            return CliResult.Fail($"could not delete {file.Name}: {ex.Message}");
        }

        return CliResult.Ok($"deleted {file.Name}", new { file = file.FullName, deleted = true });
    }

    private static CliCommand ListCommand() => new()
    {
        Name = "shot list",
        Group = "screenshots",
        Summary = "List screenshots for one game, or for every game with a screenshot folder.",
        Usage = "rhi shot list [<game>] [--json]",
        Examples = ["rhi shot list", "rhi shot list \"Alan Wake 2\""],
        Run = List,
    };

    private static CliCommand FolderCommand() => new()
    {
        Name = "shot folder",
        Group = "screenshots",
        Summary = "Show the screenshot folder RHI resolves for a game.",
        Usage = "rhi shot folder <game> [--json]",
        Run = Folder,
    };

    private static CliResult List(CliInvocation invocation)
    {
        var library = CliGames.LoadLibrary(out var failure);
        if (library is null) return failure!;

        var wanted = invocation.OperandText;
        var games = CliGames.AllGames(library).ToList();
        if (!string.IsNullOrWhiteSpace(wanted))
        {
            var match = games.FirstOrDefault(g => string.Equals(g.Name, wanted, StringComparison.OrdinalIgnoreCase))
                        ?? games.FirstOrDefault(g => g.Name.Contains(wanted, StringComparison.OrdinalIgnoreCase));
            if (match is null) return CliResult.Fail($"no game matching '{wanted}' - run: rhi game list");
            games = [match];
        }

        var settings = SettingsFile.Load();
        var rows = new List<object>();
        var text = new StringBuilder();
        var total = 0;

        foreach (var game in games)
        {
            var folder = ResolveFolder(settings, game);
            if (folder is null) continue;

            var shots = ListShots(game, folder);
            total += shots.Count;
            if (shots.Count == 0) continue;

            text.AppendLine(game.Name + "  (" + folder + ")");
            foreach (var shot in shots.Take(20))
                text.AppendLine("  " + shot.LastWriteTime.ToString("yyyy-MM-dd HH:mm") + "  " + (shot.Length / 1024).ToString().PadLeft(7) + " KB  " + shot.Name);
            if (shots.Count > 20) text.AppendLine("  ... " + (shots.Count - 20) + " more");
            text.AppendLine();

            rows.Add(new
            {
                game = game.Name,
                folder,
                count = shots.Count,
                files = shots.Take(50).Select(s => new { s.Name, bytes = s.Length, modified = s.LastWriteTime, path = s.FullName }).ToArray(),
            });
        }

        var message = rows.Count == 0 ? "no screenshots found" : text.ToString().TrimEnd() + Environment.NewLine + Environment.NewLine + total + " screenshots";
        return CliResult.Ok(message, new { total, games = rows });
    }

    private static CliResult Folder(CliInvocation invocation)
    {
        var wanted = invocation.OperandText;
        if (string.IsNullOrWhiteSpace(wanted)) return CliResult.Usage("usage: rhi shot folder <game>");

        var library = CliGames.LoadLibrary(out var failure);
        if (library is null) return failure!;

        var game = CliGames.AllGames(library).FirstOrDefault(g => string.Equals(g.Name, wanted, StringComparison.OrdinalIgnoreCase))
                   ?? CliGames.AllGames(library).FirstOrDefault(g => g.Name.Contains(wanted, StringComparison.OrdinalIgnoreCase));
        if (game is null) return CliResult.Fail($"no game matching '{wanted}' - run: rhi game list");

        var folder = ResolveFolder(SettingsFile.Load(), game);
        return folder is null
            ? CliResult.Fail($"no screenshot folder for '{game.Name}' (set one in RHI > Settings > ReShade, or take a screenshot in game)")
            : CliResult.Ok(folder, new { game = game.Name, folder, exists = Directory.Exists(folder) });
    }

    private static List<FileInfo> ListShots(SavedGame game, string folder)
    {
        var exe = CliServices.Get<IPeHeaderService>().FindGameExe(game.InstallPath);
        var folderIsGamesOwn = string.Equals(
            Path.TrimEndingDirectorySeparator(Path.GetFullPath(folder)),
            Path.TrimEndingDirectorySeparator(Path.GetFullPath(game.InstallPath)),
            StringComparison.OrdinalIgnoreCase);

        return ScreenshotLibraryService.List(folder, exe, folderIsGamesOwn);
    }

    private static string? ResolveFolder(Dictionary<string, string> settings, SavedGame game)
    {
        var basePath = settings.TryGetValue("ScreenshotPath", out var configured) ? configured : null;
        var perGame = settings.TryGetValue("PerGameScreenshotFolders", out var flag) && flag == "true";

        string? rhiFolder = null;
        if (!string.IsNullOrWhiteSpace(basePath))
        {
            rhiFolder = basePath;
            if (perGame)
            {
                var sanitized = AuxInstallService.SanitizeDirectoryName(game.Name);
                if (!string.IsNullOrEmpty(sanitized)) rhiFolder = Path.Combine(basePath, sanitized);
            }
        }

        return ScreenshotLibraryService.FindFolder(game.InstallPath, rhiFolder);
    }
}
