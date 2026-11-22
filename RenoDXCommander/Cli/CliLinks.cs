using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// Mod pages the GUI opens in a browser (ultrawide fix, Ultra+). The CLI prints the resolved URL
/// instead, which is what a script wants; nothing is downloaded or deployed for these.
/// </summary>
internal static class CliLinks
{
    internal static readonly CliCommand[] All = [Ultrawide(), UltraPlus()];

    private static CliCommand Ultrawide() => new()
    {
        Name = "link ultrawide",
        Group = "links",
        Summary = "Resolve the ultrawide fix page for a game.",
        Usage = "rhi link ultrawide <game> [--json]",
        Examples = ["rhi link ultrawide \"Alan Wake 2\""],
        Run = invocation => Resolve(invocation, ultrawide: true).GetAwaiter().GetResult(),
    };

    private static CliCommand UltraPlus() => new()
    {
        Name = "link ultra-plus",
        Group = "links",
        Summary = "Resolve the Ultra+ page for a game.",
        Usage = "rhi link ultra-plus <game> [--json]",
        Examples = ["rhi link ultra-plus \"Cyberpunk 2077\""],
        Run = invocation => Resolve(invocation, ultrawide: false).GetAwaiter().GetResult(),
    };

    private static async Task<CliResult> Resolve(CliInvocation invocation, bool ultrawide)
    {
        var game = CliGames.ResolveOne(invocation, out var failure);
        if (game is null) return failure!;

        string? url;
        string source = "";

        if (ultrawide)
        {
            var service = CliServices.Get<IUltrawideFixService>();
            await service.InitAsync();
            url = service.ResolveUrl(game.Name, null);
            source = service.ResolveSource(game.Name, null) ?? "";
        }
        else
        {
            var service = CliServices.Get<IUltraPlusService>();
            await service.InitAsync();
            url = service.ResolveUrl(game.Name, null);
        }

        var kind = ultrawide ? "ultrawide fix" : "Ultra+";

        if (string.IsNullOrWhiteSpace(url))
            return CliResult.Fail($"no {kind} page found for '{game.Name}'");

        return CliResult.Ok(source.Length > 0 ? $"{url}  ({source})" : url,
            new { game = game.Name, kind, url, source });
    }
}
