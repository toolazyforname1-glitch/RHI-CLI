using RenoDXCommander.Cli;
using Xunit;

namespace RenoDXCommander.Tests;

/// <summary>The command registry is the contract the batch layer exposes: names, aliases and
/// documentation must stay unambiguous as commands are added.</summary>
public class CliRegistryTests
{
    [Fact]
    public void NamesAreUniqueAndDocumented()
    {
        var names = CliRegistry.All.Select(c => c.Name).ToList();

        Assert.Equal(names.Count, names.Distinct(StringComparer.OrdinalIgnoreCase).Count());
        Assert.All(CliRegistry.All, command =>
        {
            Assert.False(string.IsNullOrWhiteSpace(command.Summary));
            Assert.False(string.IsNullOrWhiteSpace(command.Usage));
        });
    }

    [Fact]
    public void AliasesDoNotShadowOtherCommands()
    {
        var names = CliRegistry.All.Select(c => c.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);

        foreach (var command in CliRegistry.All)
            foreach (var alias in command.Aliases)
                Assert.False(names.Contains(alias) && !string.Equals(alias, command.Name, StringComparison.OrdinalIgnoreCase),
                    $"alias '{alias}' of '{command.Name}' shadows another command");
    }

    [Theory]
    [InlineData("setting get", "setting")]
    [InlineData("ini template list", "ini")]
    [InlineData("install reshade", "install")]
    [InlineData("overlay toggle", "overlay")]
    public void CompoundNamesResolve(string name, string firstWord)
    {
        var command = CliRegistry.Find(name);

        Assert.NotNull(command);
        Assert.StartsWith(firstWord, command!.Name, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void LookupIsCaseInsensitiveAndUnknownReturnsNull()
    {
        Assert.NotNull(CliRegistry.Find("SETTING LIST"));
        Assert.Null(CliRegistry.Find("no-such-command"));
    }
}
