using RenoDXCommander.Cli;
using Xunit;

namespace RenoDXCommander.Tests;

/// <summary>
/// The terminal parser: flags never steal a positional argument, and a two-word command name
/// ("install reshade") shifts what counts as an operand.
/// </summary>
public class CliInvocationTests
{
    /// <summary>Parses the way the dispatcher does: a registered two-word command name consumes one operand.</summary>
    private static CliInvocation Parse(params string[] args)
    {
        var invocation = CliInvocation.Parse(args);
        if (CliRegistry.Find(invocation.Verb) is not null) return invocation;

        return args.Length > 1 && CliRegistry.Find(args[0] + " " + args[1]) is not null
            ? invocation.WithConsumed(1)
            : invocation;
    }

    [Fact]
    public void CompoundNameConsumesItsSubVerb()
    {
        var invocation = Parse("game", "list", "--json");

        Assert.Equal("game", invocation.Verb);
        Assert.Empty(invocation.Args);
        Assert.True(invocation.Json);
    }

    [Fact]
    public void BooleanFlagDoesNotSwallowTheNextWord()
    {
        var invocation = Parse("--json", "game", "list");

        Assert.Equal("game", invocation.Verb);
        Assert.True(invocation.Json);
    }

    [Fact]
    public void ForceNeverConsumesTheGameName()
    {
        var invocation = Parse("install", "reshade", "FakeGame", "--force");

        Assert.Equal("FakeGame", invocation.Operand(0));
        Assert.True(invocation.Has("force"));
    }

    [Theory]
    [InlineData("--name=ReShade.ini")]
    [InlineData("--name", "ReShade.ini")]
    public void ValueFlagAcceptsBothForms(params string[] tail)
    {
        var args = new[] { "ini", "list", "FakeGame" }.Concat(tail).ToArray();
        var invocation = Parse(args);

        Assert.Equal("ReShade.ini", invocation.Flag("name"));
        Assert.Equal("FakeGame", invocation.Operand(0));
    }

    [Fact]
    public void ThreeWordNameConsumesTwoOperands()
    {
        var invocation = CliInvocation.Parse(["ini", "template", "list"]).WithConsumed(2);

        Assert.Empty(invocation.Args);
    }

    [Fact]
    public void OperandTextKeepsSpacesInGameNames()
    {
        var invocation = Parse("game", "show", "Alan", "Wake", "2");

        Assert.Equal("Alan Wake 2", invocation.OperandText);
    }

    [Fact]
    public void UnconsumedSubVerbStaysInOperands()
    {
        var raw = CliInvocation.Parse(["install", "reshade", "FakeGame"]);

        Assert.Equal("reshade FakeGame", raw.OperandText);
    }
}
