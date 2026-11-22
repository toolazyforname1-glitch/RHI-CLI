namespace RenoDXCommander.Cli;

/// <summary>Outcome of one command: exit code, human message, machine-readable payload.</summary>
internal sealed class CliResult
{
    internal int ExitCode { get; init; }
    internal string Message { get; init; } = "";
    internal object? Data { get; init; }

    /// <summary>0 — the command did what was asked.</summary>
    internal static CliResult Ok(string message, object? data = null) => new() { ExitCode = 0, Message = message, Data = data };

    /// <summary>1 — the command ran but failed.</summary>
    internal static CliResult Fail(string message, object? data = null, int exitCode = 1) =>
        new() { ExitCode = exitCode, Message = message, Data = data };

    /// <summary>2 — bad usage: unknown command, missing argument, wrong value.</summary>
    internal static CliResult Usage(string message, object? data = null) => new() { ExitCode = 2, Message = message, Data = data };
}
