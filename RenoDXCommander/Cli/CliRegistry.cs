namespace RenoDXCommander.Cli;

/// <summary>Name and alias lookup for every registered command.</summary>
internal static class CliRegistry
{
    private static readonly List<CliCommand> Commands = [];
    private static readonly Dictionary<string, CliCommand> Index = new(StringComparer.OrdinalIgnoreCase);

    static CliRegistry()
    {
        RegisterAll(CliBuiltins.All);
        RegisterAll(CliSettings.All);
        RegisterAll(CliGames.All);
        RegisterAll(CliStatus.All);
        RegisterAll(CliInstall.All);
        RegisterAll(CliShots.All);
        RegisterAll(CliUpdate.All);
        RegisterAll(CliComponents.All);
        RegisterAll(CliScalers.All);
        RegisterAll(CliMods.All);
        RegisterAll(CliLuma.All);
        RegisterAll(CliUi.All);
        RegisterAll(CliIni.All);
        RegisterAll(CliOverlay.All);
        RegisterAll(CliDlss.All);
        RegisterAll(CliHdr.All);
        RegisterAll(CliLinks.All);
        RegisterAll(CliMassDeploy.All);
        RegisterAll(CliAuth.All);
        RegisterAll(CliLive.All);
        RegisterAll(CliNvidia.All);
        RegisterAll(CliLogs.All);
        RegisterAll(CliSystem.All);
        RegisterAll(CliShaderPacks.All);
        RegisterAll(CliAddons.All);
        RegisterAll(CliDldsr.All);
        RegisterAll(CliDisplay.All);
        RegisterAll(CliOptiscaler.All);
        RegisterAll(CliReShadePreset.All);
        RegisterAll(CliFrameGen.All);
        RegisterAll(CliRtxEncore.All);
        RegisterAll(CliShaderOps.All);
        RegisterAll(CliCache.All);
        RegisterAll(CliMassShaders.All);
        RegisterAll(CliPeakNits.All);
    }

    private static void RegisterAll(IEnumerable<CliCommand> commands)
    {
        foreach (var command in commands) Register(command);
    }

    internal static IReadOnlyList<CliCommand> All => Commands;

    /// <summary>First registration wins, so a later feature cannot silently shadow a builtin.</summary>
    internal static void Register(CliCommand command)
    {
        if (Index.ContainsKey(command.Name)) return;
        Commands.Add(command);
        Index[command.Name] = command;
        foreach (var alias in command.Aliases)
            Index.TryAdd(alias, command);
    }

    internal static CliCommand? Find(string name) =>
        !string.IsNullOrWhiteSpace(name) && Index.TryGetValue(name, out var command) ? command : null;
}

