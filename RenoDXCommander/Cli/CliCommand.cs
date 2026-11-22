namespace RenoDXCommander.Cli;

/// <summary>One terminal command: how it is named, documented and run.</summary>
internal sealed class CliCommand
{
    internal required string Name { get; init; }
    internal string[] Aliases { get; init; } = [];
    internal string Group { get; init; } = "core";
    internal string Summary { get; init; } = "";
    internal string Usage { get; init; } = "";
    internal string[] Examples { get; init; } = [];
    internal Func<CliInvocation, Task<CliResult>>? RunAsync { get; init; }

    internal Func<CliInvocation, CliResult> Run { get; init; } = _ => CliResult.Fail("not implemented");

    /// <summary>True for commands that change something on disk, a game folder or the registry.</summary>
    internal bool Destructive { get; init; }

    /// <summary>True when the command needs a live RHI window and is forwarded over the pipe.</summary>
    internal bool RequiresGui { get; init; }
}
