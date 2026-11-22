using RenoDXCommander.Cli;
using Xunit;

namespace RenoDXCommander.Tests;

/// <summary>
/// The attach path calls this instead of the CLI entry point: same registry, no printing. These
/// lock the contract the pipe server depends on.
/// </summary>
public class CliHostTests
{
    [Fact]
    public void RunsACommandAndReturnsItsResult()
    {
        var result = CliHost.ExecuteInProcess(["version"]);

        Assert.Equal(0, result.ExitCode);
        Assert.False(string.IsNullOrWhiteSpace(result.Message));
    }

    [Fact]
    public void UnknownCommandIsAUsageError()
    {
        var result = CliHost.ExecuteInProcess(["definitely-not-a-command"]);

        Assert.Equal(2, result.ExitCode);
    }

    [Fact]
    public void ResolvesCompoundNamesTheSameWayTheCliDoes()
    {
        var result = CliHost.ExecuteInProcess(["help", "setting", "set"]);

        Assert.Equal(0, result.ExitCode);
        Assert.Contains("setting set", result.Message);
    }

    [Fact]
    public void HelpFlagOnACommandReturnsThatCommandsHelp()
    {
        var result = CliHost.ExecuteInProcess(["doctor", "--help"]);

        Assert.Equal(0, result.ExitCode);
        Assert.Contains("doctor", result.Message);
    }
}
