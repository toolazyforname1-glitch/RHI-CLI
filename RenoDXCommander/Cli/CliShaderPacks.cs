using System.Text;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// The shader packs RHI can deploy with ReShade. Listing is read-only; installing them happens
/// through `install reshade --packs`, which passes the ids to the same installer the GUI uses.
/// </summary>
internal static class CliShaderPacks
{
    internal static readonly CliCommand[] All = [ListCommand()];

    private static CliCommand ListCommand() => new()
    {
        Name = "reshade shaders",
        Group = "config",
        Summary = "List the shader packs RHI can install, and whether each is already cached.",
        Usage = "rhi reshade shaders [--json]",
        Examples = ["rhi reshade shaders"],
        RunAsync = List,
    };

    private static async Task<CliResult> List(CliInvocation invocation)
    {
        var service = CliServices.Get<IShaderPackService>();
        await service.EnsureLatestAsync();

        var packs = service.AvailablePacks;
        if (packs.Count == 0) return CliResult.Fail("no shader packs available - check the connection");

        var rows = new List<object>();
        var text = new StringBuilder();

        foreach (var pack in packs.OrderBy(p => p.Category).ThenBy(p => p.DisplayName, StringComparer.OrdinalIgnoreCase))
        {
            var cached = await service.IsPackCachedAsync(pack.Id);
            text.AppendLine(pack.Id.PadRight(28) + (cached ? "cached" : "-").PadRight(9) + pack.DisplayName);
            rows.Add(new { id = pack.Id, name = pack.DisplayName, category = pack.Category.ToString(), cached });
        }

        return CliResult.Ok(text.ToString().TrimEnd(), new { count = packs.Count, packs = rows });
    }
}
