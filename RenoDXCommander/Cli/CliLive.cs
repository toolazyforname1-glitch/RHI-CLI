using System.IO.Pipes;
using System.Text;
using System.Text.Json;
using RenoDXCommander.Models;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// Talks to rhi-live.addon inside a running ReShade: the addon answers on a local pipe and does the
/// actual effect work on the render thread, so these commands change a running game, not its files.
/// </summary>
internal static class CliLive
{
    private const string PipeName = "RHI_ReShadeAddon";

    internal static readonly CliCommand[] All = [Command()];

    private static CliCommand Command() => new()
    {
        Name = "reshade live",
        Group = "config",
        Summary = "Control ReShade inside a running game (ping, effects, techniques, technique, preset) or install the addon.",
        Usage = "rhi reshade live <ping|effects [on|off]|techniques|technique <name> <on|off>|preset [path]>  |  rhi reshade live install <game> [--yes]",
        Examples =
        [
            "rhi reshade live install \"Mini Motorways\"",
            "rhi reshade live ping",
            "rhi reshade live effects off",
            "rhi reshade live techniques",
            "rhi reshade live technique SMAA on",
        ],

        Run = invocation => string.Equals(invocation.Operand(0), "install", StringComparison.OrdinalIgnoreCase)
            ? Install(invocation)
            : Send(invocation),
    };

    private static CliResult Send(CliInvocation invocation)
    {
        if (invocation.Args.Length == 0)
            return CliResult.Usage("usage: rhi reshade live <ping|effects|techniques|technique|preset>");

        if (invocation.DryRun)
            return CliResult.Ok($"would send '{string.Join(' ', invocation.Args)}' to ReShade", new { request = string.Join(' ', invocation.Args), dryRun = true });

        var request = string.Join(' ', invocation.Args);

        try
        {
            using var client = new NamedPipeClientStream(".", PipeName, PipeDirection.InOut);
            client.Connect(2000);

            var payload = Encoding.UTF8.GetBytes(request + "\n");
            client.Write(payload, 0, payload.Length);
            client.Flush();

            using var reader = new StreamReader(client, new UTF8Encoding(false), false, 4096, leaveOpen: true);

            var read = Task.Run(reader.ReadLine);
            string? line;
            try { line = read.Wait(TimeSpan.FromSeconds(10)) ? read.Result : null; }
            catch (AggregateException) { line = null; }

            if (string.IsNullOrWhiteSpace(line)) return CliResult.Fail("the addon did not answer");

            var document = JsonDocument.Parse(line);
            var ok = !document.RootElement.TryGetProperty("ok", out var status) || status.GetBoolean();
            var data = document.RootElement.Clone();

            return ok ? CliResult.Ok(line, data) : CliResult.Fail(line, data);
        }
        catch (Exception ex) when (ex is IOException or TimeoutException or UnauthorizedAccessException or JsonException)
        {
            return CliResult.Fail($"no live channel ({ex.GetType().Name}: {ex.Message}) - install the addon and start the game: rhi reshade live install <game>");
        }
    }

    private static CliResult Install(CliInvocation invocation)
    {
        var game = CliGames.ResolveOne(invocation.Operand(1), out var failure);
        if (game is null) return failure!;

        bool is32Bit;
        try
        {
            is32Bit = CliServices.Get<IPeHeaderService>().DetectGameArchitecture(game.InstallPath) == MachineType.I386;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or BadImageFormatException or ArgumentException)
        {
            return CliResult.Fail($"could not read the game's architecture: {ex.Message}");
        }

        var fileName = is32Bit ? "rhi-live.addon32" : "rhi-live.addon64";
        var source = Path.Combine(AppPaths.ExeDir, fileName);

        if (!File.Exists(source))
            return CliResult.Fail($"{fileName} is not next to RHI.exe - build it first (RHI\\LiveAddon\\build.bat)");

        if (!invocation.Confirmed)
            return CliResult.Usage($"refusing to copy {fileName} into the game folder without --yes: rhi reshade live install {game.Name} --yes");

        var target = Path.Combine(game.InstallPath, fileName);

        if (invocation.DryRun)
            return CliResult.Ok($"would copy {fileName} into {game.InstallPath}", new { game = game.Name, file = target, dryRun = true });

        try
        {
            File.Copy(source, target, overwrite: true);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            return CliResult.Fail($"could not copy {fileName}: {ex.Message}");
        }

        return CliResult.Ok($"live addon installed for '{game.Name}' ({fileName}) - start the game to use rhi reshade live",
            new { game = game.Name, file = target, bitness = is32Bit ? 32 : 64 });
    }
}
