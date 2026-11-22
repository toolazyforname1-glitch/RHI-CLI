using System.Text;
using RenoDXCommander.Models;
using RenoDXCommander.Services;
using RenoDXCommander.ViewModels;

namespace RenoDXCommander.Cli;

/// <summary>
/// Component installs that need nothing but a game name and an install path. Components whose
/// installer takes a GameCardViewModel (DXVK, OptiScaler) are not wired yet and say so.
/// </summary>
internal static class CliComponents
{
    internal static readonly CliCommand[] All =
    [
        InstallRef(), UninstallRef(), InstallDof(), UninstallDof(), InstallAsi(), UninstallAsi(),
        Component("displaycommander", displayCommander: true), Component("relimiter", displayCommander: false),
    ];

    private static CliCommand Component(string name, bool displayCommander) => new()
    {
        Name = "toggle " + name,
        Group = "install",
        Summary = (displayCommander ? "Install or remove Display Commander" : "Install or remove ReLimiter") + " for a game.",
        Usage = $"rhi toggle {name} <game> on|off [--dry-run] [--yes]",
        Examples = [$"rhi toggle {name} cyberpunk on --dry-run"],
        Destructive = true,
        Run = invocation => ComponentAsync(invocation, displayCommander).GetAwaiter().GetResult(),
    };

    private static async Task<CliResult> ComponentAsync(CliInvocation invocation, bool displayCommander)
    {
        var label = displayCommander ? "Display Commander" : "ReLimiter";
        var args = invocation.Args;
        if (args.Length < 2) return CliResult.Usage($"usage: rhi toggle {(displayCommander ? "displaycommander" : "relimiter")} <game> on|off [--dry-run] [--yes]");

        var install = args[1].ToLowerInvariant() is "on" or "install";
        if (!install && args[1].ToLowerInvariant() is not ("off" or "uninstall"))
            return CliResult.Usage("value must be on or off");

        var game = CliGames.ResolveOne(args[0], out var failure);
        if (game is null) return failure!;

        if (invocation.DryRun)
            return CliResult.Ok($"would {(install ? "install" : "remove")} {label} for '{game.Name}'",
                new { game = game.Name, component = label, install, dryRun = true });

        if (!install && !invocation.Confirmed)
            return CliResult.Usage($"refusing to remove without --yes: rhi toggle {(displayCommander ? "displaycommander" : "relimiter")} \"{game.Name}\" off --yes");

        var card = CliCards.Build(game, out var cardError);
        if (card is null) return CliResult.Fail(cardError!);

        var installedBefore = displayCommander ? card.IsDcInstalled : card.IsUlInstalled;
        if (!install && !installedBefore)
            return CliResult.Fail($"{label} is not installed for '{game.Name}'");

        var viewModel = CliServices.Get<MainViewModel>();

        if (displayCommander)
        {
            if (install) await viewModel.InstallDcAsync(card);
            else viewModel.UninstallDc(card);
        }
        else
        {
            if (install) await viewModel.InstallUlAsync(card);
            else viewModel.UninstallUl(card);
        }

        CliCards.RefreshAddons(card);
        var installedAfter = displayCommander ? card.IsDcInstalled : card.IsUlInstalled;
        var message = displayCommander ? card.DcActionMessage : card.UlActionMessage;

        return install == installedAfter
            ? CliResult.Ok($"{label} {(install ? "installed" : "removed")} for '{game.Name}'",
                new { game = game.Name, component = label, installed = install })
            : CliResult.Fail($"{label} {(install ? "install" : "remove")} failed for '{game.Name}': {message}");
    }

    private static CliCommand InstallAsi() => new()
    {
        Name = "install asi",
        Group = "install",
        Summary = "Install Ultimate ASI Loader (a dinput8.dll proxy) into a game folder.",
        Usage = "rhi install asi <game> [--dll <name>] [--dry-run]",
        Examples = ["rhi install asi \"GTA San Andreas\" --dry-run"],
        Destructive = true,
        Run = invocation => AsiAsync(invocation).GetAwaiter().GetResult(),
    };

    private static CliCommand UninstallAsi() => new()
    {
        Name = "uninstall asi",
        Group = "install",
        Summary = "Remove Ultimate ASI Loader from a game, restoring a replaced proxy. Needs --yes.",
        Usage = "rhi uninstall asi <game> [--yes] [--dry-run]",
        Destructive = true,
        Run = AsiUninstall,
    };

    private static CliResult AsiUninstall(CliInvocation invocation)
    {
        var game = CliGames.ResolveOne(invocation, out var failure);
        if (game is null) return failure!;

        var aux = CliServices.Get<IAuxInstallService>();
        var record = aux.FindRecord(game.Name, game.InstallPath, UltimateAsiLoaderService.AddonType);
        if (record is null) return CliResult.Fail($"Ultimate ASI Loader is not installed for '{game.Name}'");

        if (invocation.DryRun)
            return CliResult.Ok($"would remove {record.InstalledAs} from {record.InstallPath}", new { game = game.Name, file = record.InstalledAs, dryRun = true });

        if (!invocation.Confirmed)
            return CliResult.Usage($"refusing to remove without --yes: rhi uninstall asi \"{game.Name}\" --yes");

        var card = CliCards.Build(game, out var cardError);
        if (card is null) return CliResult.Fail(cardError!);

        var removed = CliServices.Get<UltimateAsiLoaderService>().Uninstall(card);

        return removed
            ? CliResult.Ok($"Ultimate ASI Loader removed from '{game.Name}'", new { game = game.Name, removed = record.InstalledAs })
            : CliResult.Fail($"could not remove the ASI loader from '{game.Name}' - see the log");
    }

    private static async Task<CliResult> AsiAsync(CliInvocation invocation)
    {
        var game = CliGames.ResolveOne(invocation, out var failure);
        if (game is null) return failure!;

        var dllName = invocation.Flag("dll") ?? "dinput8.dll";

        if (invocation.DryRun)
            return CliResult.Ok($"would install Ultimate ASI Loader as {dllName} into {game.InstallPath}",
                new { game = game.Name, path = game.InstallPath, dll = dllName, dryRun = true });

        var card = CliCards.Build(game, out var cardError);
        if (card is null) return CliResult.Fail(cardError!);

        var service = CliServices.Get<UltimateAsiLoaderService>();
        var (success, hookedOriginal) = await service.InstallAsync(card, dllName, progress: null);

        return success
            ? CliResult.Ok($"Ultimate ASI Loader installed for '{game.Name}' as {dllName}" +
                           (hookedOriginal is null ? "" : $" (replaced {hookedOriginal})"),
                new { game = game.Name, dll = dllName, hookedOriginal })
            : CliResult.Fail($"ASI loader install returned false for '{game.Name}' - see the log");
    }

    private static CliCommand InstallRef() => new()
    {
        Name = "install reframework",
        Group = "install",
        Summary = "Install RE Framework (dinput8.dll) into an RE Engine game.",
        Usage = "rhi install reframework <game> [--dry-run]",
        Examples = ["rhi install reframework \"Resident Evil 4\" --dry-run"],
        Destructive = true,
        Run = invocation => RefAsync(invocation, install: true).GetAwaiter().GetResult(),
    };

    private static CliCommand UninstallRef() => new()
    {
        Name = "uninstall reframework",
        Group = "install",
        Summary = "Remove RE Framework from a game. Needs --yes.",
        Usage = "rhi uninstall reframework <game> [--yes] [--dry-run]",
        Destructive = true,
        Run = invocation => RefAsync(invocation, install: false).GetAwaiter().GetResult(),
    };

    private static CliCommand InstallDof() => new()
    {
        Name = "install dof-fix",
        Group = "install",
        Summary = "Install the UE depth-of-field fix into a game folder.",
        Usage = "rhi install dof-fix <game> [--dry-run]",
        Examples = ["rhi install dof-fix \"Stalker 2\" --dry-run"],
        Destructive = true,
        Run = invocation => DofAsync(invocation, install: true),
    };

    private static CliCommand UninstallDof() => new()
    {
        Name = "uninstall dof-fix",
        Group = "install",
        Summary = "Remove the UE depth-of-field fix. Needs --yes.",
        Usage = "rhi uninstall dof-fix <game> [--yes] [--dry-run]",
        Destructive = true,
        Run = invocation => DofAsync(invocation, install: false),
    };

    private static async Task<CliResult> RefAsync(CliInvocation invocation, bool install)
    {
        if (!TryResolve(invocation, out var game, out var failure)) return failure!;
        var service = CliServices.Get<IREFrameworkService>();

        if (install)
        {
            var engines = CliGames.LookupString(LoadLibrary().EngineTypeCache, game!.InstallPath);
            if (invocation.DryRun)
                return CliResult.Ok($"would install RE Framework for '{game.Name}' into {game.InstallPath} (engine {engines})", new { game = game.Name, path = game.InstallPath, engine = engines, dryRun = true });

            var record = await service.InstallAsync(game.Name, game.InstallPath, progress: null, store: game.Source);
            return CliResult.Ok($"RE Framework installed for '{game.Name}': {record.InstalledVersion}", new { game = game.Name, version = record.InstalledVersion, record.InstallPath });
        }

        var existing = service.GetRecords().FirstOrDefault(r => string.Equals(r.GameName, game!.Name, StringComparison.OrdinalIgnoreCase));
        if (existing is null) return CliResult.Fail($"RE Framework is not installed for '{game!.Name}'");
        if (invocation.DryRun) return CliResult.Ok($"would remove dinput8.dll from {game.InstallPath}", new { game = game.Name, dryRun = true });
        if (!invocation.Confirmed) return CliResult.Usage($"refusing to remove without --yes: rhi uninstall reframework \"{game.Name}\" --yes");

        service.Uninstall(game.Name, game.InstallPath);
        return CliResult.Ok($"RE Framework removed from '{game.Name}'", new { game = game.Name, removed = true });
    }

    private static CliResult DofAsync(CliInvocation invocation, bool install)
    {
        if (!TryResolve(invocation, out var game, out var failure)) return failure!;
        var service = CliServices.Get<DofFixService>();
        var installed = service.IsInstalledIn(game!.InstallPath);

        if (install)
        {
            if (installed && !invocation.Has("force")) return CliResult.Fail($"the DOF fix is already installed in {game.InstallPath} - use --force");
            if (invocation.DryRun) return CliResult.Ok($"would install the DOF fix into {game.InstallPath}", new { game = game.Name, path = game.InstallPath, dryRun = true });

            var ok = service.InstallAsync(game.InstallPath, progress: null).GetAwaiter().GetResult();
            return ok
                ? CliResult.Ok($"DOF fix installed for '{game.Name}'", new { game = game.Name, installed = true })
                : CliResult.Fail($"the DOF fix install returned false for '{game.Name}' - see the log");
        }

        if (!installed) return CliResult.Fail($"the DOF fix is not installed in {game.InstallPath}");
        if (invocation.DryRun) return CliResult.Ok($"would remove the DOF fix from {game.InstallPath}", new { game = game.Name, dryRun = true });
        if (!invocation.Confirmed) return CliResult.Usage($"refusing to remove without --yes: rhi uninstall dof-fix \"{game.Name}\" --yes");

        var removed = service.Uninstall(game.InstallPath);
        return removed
            ? CliResult.Ok($"DOF fix removed from '{game.Name}'", new { game = game.Name, removed = true })
            : CliResult.Fail($"the DOF fix uninstall returned false for '{game.Name}' - see the log");
    }

    private static SavedGameLibrary LoadLibrary()
    {
        var library = CliGames.LoadLibrary(out _);
        return library ?? new SavedGameLibrary();
    }

    private static bool TryResolve(CliInvocation invocation, out SavedGame? game, out CliResult? failure)
    {
        game = null;
        failure = null;
        var name = invocation.OperandText;
        if (string.IsNullOrWhiteSpace(name))
        {
            failure = CliResult.Usage("usage: rhi " + string.Join(' ', invocation.Positional) + " <game>");
            return false;
        }

        var library = CliGames.LoadLibrary(out var loadFailure);
        if (library is null)
        {
            failure = loadFailure;
            return false;
        }

        var games = CliGames.AllGames(library).ToList();
        game = games.FirstOrDefault(g => string.Equals(g.Name, name, StringComparison.OrdinalIgnoreCase))
               ?? games.FirstOrDefault(g => g.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

        if (game is null)
        {
            failure = CliResult.Fail($"no game matching '{name}' - run: rhi game list");
            return false;
        }

        if (string.IsNullOrWhiteSpace(game.InstallPath) || (!invocation.DryRun && !Directory.Exists(game.InstallPath)))
        {
            failure = CliResult.Fail($"install path for '{game.Name}' does not exist: {game.InstallPath}");
            game = null;
            return false;
        }

        return true;
    }
}