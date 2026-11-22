using System.Text;
using RenoDXCommander.Models;
using RenoDXCommander.Services;
using RenoDXCommander.ViewModels;

namespace RenoDXCommander.Cli;

internal static class CliUpdate
{
    internal static readonly CliCommand[] All = [CheckCommand(), UpdateAllCommand(), UpdateComponentCommand(), AppCommand()];

    private static readonly Dictionary<string, (string Label, Func<MainViewModel, Task> Run)> Components =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["reshade"] = ("ReShade", vm => vm.UpdateAllReShadeAsync()),
            ["renodx"] = ("RenoDX", vm => vm.UpdateAllRenoDxAsync()),
            ["relimiter"] = ("ReLimiter", vm => vm.UpdateAllUlAsync()),
            ["displaycommander"] = ("Display Commander", vm => vm.UpdateAllDcAsync()),
            ["dxvk"] = ("DXVK", vm => vm.UpdateAllDxvkAsync()),
            ["optiscaler"] = ("OptiScaler", vm => vm.UpdateAllOsAsync()),
            ["reframework"] = ("RE Framework", vm => vm.UpdateAllRefAsync()),
            ["luma"] = ("Luma", vm => vm.UpdateAllLumaAsync()),
            ["dof-fix"] = ("DOF fix", vm => vm.UpdateAllDofFixAsync()),
        };

    private static CliCommand UpdateComponentCommand() => new()
    {
        Name = "update",
        Group = "update",
        Summary = "Run one component's batch update over every game (reshade, renodx, dxvk, optiscaler, ...).",
        Usage = "rhi update <component> [--dry-run] [--yes] [--json]",
        Examples = ["rhi update reshade --dry-run", "rhi update dxvk --yes"],
        Destructive = true,
        RequiresGui = true,
        RunAsync = UpdateComponent,
    };

    private static async Task<CliResult> UpdateComponent(CliInvocation invocation)
    {
        var component = invocation.Operand(0);
        if (string.IsNullOrWhiteSpace(component))
            return CliResult.Usage($"usage: rhi update <{string.Join("|", Components.Keys)}> [--dry-run] [--yes]");

        if (!Components.TryGetValue(component, out var entry))
            return CliResult.Usage($"unknown component '{component}' - one of: {string.Join(", ", Components.Keys)}");

        var window = CliRuntime.Window;
        if (window is null)
            return CliResult.Fail($"RHI is not running - updating {entry.Label} works on RHI's scanned game list. Start RHI and run this again.");

        var games = window.ViewModel.AllCards.Count;
        if (invocation.DryRun)
            return CliResult.Ok($"would update {entry.Label} for {games} game(s)", new { component = entry.Label, games, dryRun = true });

        if (!invocation.Confirmed)
            return CliResult.Usage($"updating {entry.Label} rewrites files for {games} game(s) - confirm with --yes, or preview with --dry-run");

        await entry.Run(window.ViewModel);
        return CliResult.Ok($"{entry.Label} update finished over {games} game(s)", new { component = entry.Label, games, done = true });
    }

    private static CliCommand UpdateAllCommand() => new()
    {
        Name = "update all",
        Group = "update",
        Summary = "Run Update All: the ReShade pass, then the RenoDX pass, over every eligible game.",
        Usage = "rhi update all [--dry-run] [--yes] [--json]",
        Examples = ["rhi update all --dry-run", "rhi update all --yes"],
        Destructive = true,
        RequiresGui = true,
        RunAsync = UpdateAll,
    };

    private static async Task<CliResult> UpdateAll(CliInvocation invocation)
    {
        var window = CliRuntime.Window;
        if (window is null)
            return CliResult.Fail("RHI is not running - Update All works on RHI's scanned game list. Start RHI and run this again.");

        var games = window.ViewModel.AllCards.Count;
        if (games == 0) return CliResult.Fail("RHI has no games loaded yet - let it finish scanning");

        if (invocation.DryRun)
            return CliResult.Ok($"would run Update All over {games} games (ReShade pass, then RenoDX pass)",
                new { games, dryRun = true });

        if (!invocation.Confirmed)
            return CliResult.Usage($"Update All changes installed files for {games} games - confirm with --yes, or preview with --dry-run");

        await window.ViewModel.UpdateAllReShadeAsync();
        await window.ViewModel.UpdateAllRenoDxAsync();

        return CliResult.Ok($"Update All finished over {games} games (ReShade, RenoDX)", new { games, done = true });
    }

    private static CliCommand CheckCommand() => new()
    {
        Name = "update check",
        Group = "update",
        Summary = "Check installed components for updates. Heavier ones report the last known state with --offline.",
        Usage = "rhi update check [<game>] [--offline] [--json]",
        Examples = ["rhi update check", "rhi update check \"Cyberpunk 2077\" --offline"],
        RunAsync = Check,
    };

    private static async Task<CliResult> Check(CliInvocation invocation)
    {
        var library = CliGames.LoadLibrary(out var failure);
        if (library is null) return failure!;

        var wanted = invocation.OperandText;
        var installer = CliServices.Get<IAuxInstallService>();
        var records = installer.LoadAll();

        if (records.Count == 0 && (library.UpdateAvailableSnapshot?.Count ?? 0) == 0)
            return CliResult.Ok("nothing installed to check");

        var offline = invocation.Has("offline");
        var rows = new List<UpdateRow>();
        var text = new StringBuilder();
        text.AppendLine("GAME".PadRight(34) + "COMPONENT".PadRight(18) + "FILE".PadRight(20) + "UPDATE");

        foreach (var record in records)
        {
            if (!string.IsNullOrWhiteSpace(wanted) && !record.GameName.Contains(wanted, StringComparison.OrdinalIgnoreCase)) continue;

            var update = false;
            var status = "unknown";
            if (offline)
            {
                status = "not checked (offline)";
            }
            else
            {
                try
                {
                    update = await installer.CheckForUpdateAsync(record);
                    status = update ? "available" : "up to date";
                }
                catch (Exception ex)
                {
                    status = "check failed: " + ex.Message;
                }
            }

            var row = new UpdateRow(record.GameName, record.AddonType, record.InstalledAs, record.Channel ?? "", status, update);
            rows.Add(row);
            text.AppendLine(Clip(row.Game, 33).PadRight(34) + Clip(row.Component, 17).PadRight(18) + Clip(row.File, 19).PadRight(20) + row.Status);
        }

        var cached = library.UpdateAvailableSnapshot ?? new Dictionary<string, string>();
        foreach (var pair in cached.OrderBy(p => p.Key, StringComparer.OrdinalIgnoreCase))
        {
            if (!string.IsNullOrWhiteSpace(wanted) && !pair.Key.Contains(wanted, StringComparison.OrdinalIgnoreCase)) continue;
            var row = new UpdateRow(pair.Key, "cached", "", "", pair.Value, pair.Value.Length > 0);
            rows.Add(row);
            text.AppendLine(Clip(row.Game, 33).PadRight(34) + "(cached flags)".PadRight(18) + "".PadRight(20) + row.Status);
        }

        var pending = rows.Count(r => r.Update);
        text.AppendLine();
        text.Append(pending == 0 ? "no updates pending" : pending + " update(s) pending");

        return CliResult.Ok(text.ToString(), new
        {
            pending,
            offline,
            components = rows.Select(r => new { game = r.Game, component = r.Component, file = r.File, channel = r.Channel, status = r.Status, update = r.Update }).ToArray(),
        });
    }

    private sealed record UpdateRow(string Game, string Component, string File, string Channel, string Status, bool Update);

    private static string Clip(string text, int width) =>
        text.Length <= width ? text : text[..(width - 1)] + "\u2026";

    private static CliCommand AppCommand() => new()
    {
        Name = "update app",
        Group = "update",
        Summary = "Check GitHub for a newer RHI release (does not download or install it).",
        Usage = "rhi update app [--json]",
        Examples = ["rhi update app"],
        RunAsync = App,
    };

    private static async Task<CliResult> App(CliInvocation invocation)
    {
        var service = CliServices.Get<IUpdateService>();
        var settings = SettingsFile.Load();
        var beta = settings.TryGetValue("BetaOptIn", out var value) && string.Equals(value, "true", StringComparison.OrdinalIgnoreCase);

        var info = await service.CheckForUpdateAsync(beta);

        if (info is null)
        {
            return CliResult.Ok($"RHI {service.CurrentVersion} is up to date (or the check failed - see the log)",
                new { current = service.CurrentVersion.ToString(), update = false, beta });
        }

        return CliResult.Ok(
            $"update available: {info.CurrentVersion} -> {info.RemoteVersion}{Environment.NewLine}{info.DownloadUrl}",
            new { current = info.CurrentVersion.ToString(), available = info.RemoteVersion.ToString(), display = info.DisplayVersion, url = info.DownloadUrl });
    }
}
