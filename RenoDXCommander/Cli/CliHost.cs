using System.Text.Json;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// Entry point for terminal use: <c>RHI.exe --cli &lt;command&gt; ...</c>. Runs before WinUI starts,
/// so a batch file never opens a window. Commands that need the live window are forwarded to a
/// running RHI over the named pipe instead.
/// </summary>
internal static class CliHost
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    /// <summary>True when the first argument asks for terminal mode.</summary>
    internal static bool IsEntryPoint(string argument) =>
        string.Equals(argument, "--cli", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(argument, "-cli", StringComparison.OrdinalIgnoreCase);

    internal static int Run(string[] args)
    {
        CliConsole.Attach();

        CliInvocation invocation;
        try
        {
            invocation = CliInvocation.Parse(args);
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            CliConsole.WriteLine("rhi: could not parse the command line - " + ex.Message);
            return 2;
        }

        var (command, resolvedInvocation, name) = Resolve(invocation);

        if (command is { RequiresGui: true } && CliRuntime.Window is null &&
            CliPipe.TrySend(args, out var response, out var pipedCode))
            return ReportPiped(invocation, response, pipedCode);

        return command is null
            ? Emit(invocation, name, CliResult.Usage($"Unknown command '{name}'. Run: rhi commands"))
            : Emit(invocation, name, RunOne(command, resolvedInvocation));
    }

    /// <summary>Runs a command without printing anything - used by the pipe server inside the GUI.</summary>
    internal static CliResult ExecuteInProcess(string[] args) =>
        ExecuteInProcessAsync(args).GetAwaiter().GetResult().Result;

    internal static async Task<(string Name, CliResult Result)> ExecuteInProcessAsync(string[] args)
    {
        var (command, resolvedInvocation, name) = Resolve(CliInvocation.Parse(args));
        if (command is null)
            return (name, CliResult.Usage($"Unknown command '{name}'. Run: rhi commands"));

        return (name, await RunOneAsync(command, resolvedInvocation));
    }

    private static (CliCommand? Command, CliInvocation Invocation, string Name) Resolve(CliInvocation invocation)
    {
        if (invocation.Verb.Length == 0)
            return ResolveNamed(invocation, invocation.Has("version") ? "version" : "help");

        CliCommand? command = null;
        if (invocation.Positional.Length > 1)
        {
            for (var words = Math.Min(3, invocation.Positional.Length - 1); words >= 1; words--)
            {
                var candidate = CliRegistry.Find(string.Join(' ', invocation.Positional.Take(words + 1)));
                if (candidate is null) continue;
                command = candidate;
                invocation = invocation.WithConsumed(words);
                break;
            }
        }

        command ??= CliRegistry.Find(invocation.Verb);
        if (command is null) return (null, invocation, invocation.Verb);

        if (invocation.Has("help") || invocation.Has("h") || invocation.Has("?"))
        {
            var helpArgs = new List<string> { "help", command.Name };
            if (invocation.Json) helpArgs.Add("--json");
            return ResolveNamed(CliInvocation.Parse(helpArgs.ToArray()), "help");
        }

        return (command, invocation, command.Name);
    }

    private static (CliCommand? Command, CliInvocation Invocation, string Name) ResolveNamed(CliInvocation invocation, string name) =>
        CliRegistry.Find(name) is { } command ? (command, invocation, name) : (null, invocation, name);

    private static CliResult RunOne(CliCommand command, CliInvocation invocation) =>
        command.RunAsync is null ? RunSync(command, invocation) : RunOneAsync(command, invocation).GetAwaiter().GetResult();

    private static async Task<CliResult> RunOneAsync(CliCommand command, CliInvocation invocation)
    {
        if (command.Destructive && !invocation.DryRun && !invocation.Confirmed)
            return CliResult.Usage($"refusing: {command.Name} changes files or settings and needs --yes (or --dry-run to preview)");

        try
        {
            return command.RunAsync is not null
                ? await command.RunAsync(invocation) ?? CliResult.Fail("the command returned no result")
                : RunSync(command, invocation);
        }
        catch (Exception ex)
        {
            try { CrashReporter.Log($"[CliHost] {command.Name} failed - {ex.GetType().Name}: {ex.Message}"); }
            catch (IOException) { }
            return CliResult.Fail($"{ex.GetType().Name}: {ex.Message}");
        }
    }

    private static CliResult RunSync(CliCommand command, CliInvocation invocation)
    {
        if (command.Destructive && !invocation.DryRun && !invocation.Confirmed)
            return CliResult.Usage($"refusing: {command.Name} changes files or settings and needs --yes (or --dry-run to preview)");

        try
        {
            return command.Run(invocation) ?? CliResult.Fail("the command returned no result");
        }
        catch (Exception ex)
        {
            try { CrashReporter.Log($"[CliHost] {command.Name} failed - {ex.GetType().Name}: {ex.Message}"); }
            catch (IOException) { }
            return CliResult.Fail($"{ex.GetType().Name}: {ex.Message}");
        }
    }

    private static int ReportPiped(CliInvocation invocation, string response, int exitCode)
    {
        if (invocation.Json)
        {
            CliConsole.WriteLine(response);
            return exitCode;
        }

        try
        {
            using var document = JsonDocument.Parse(response);
            var message = document.RootElement.TryGetProperty("message", out var value) ? value.GetString() : response;
            if (!invocation.Quiet && !string.IsNullOrWhiteSpace(message)) CliConsole.WriteLine(message!);
        }
        catch (JsonException)
        {
            CliConsole.WriteLine(response);
        }

        return exitCode;
    }

    private static int Emit(CliInvocation invocation, string commandName, CliResult result)
    {
        if (invocation.Json)
        {
            var payload = new
            {
                ok = result.ExitCode == 0,
                command = commandName,
                exitCode = result.ExitCode,
                message = result.Message,
                data = result.Data,
            };
            CliConsole.WriteLine(JsonSerializer.Serialize(payload, JsonOptions));
            return result.ExitCode;
        }

        if (!invocation.Quiet && !string.IsNullOrWhiteSpace(result.Message))
            CliConsole.WriteLine(result.Message);

        return result.ExitCode;
    }
}
