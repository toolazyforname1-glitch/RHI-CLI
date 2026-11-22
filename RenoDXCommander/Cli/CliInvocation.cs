namespace RenoDXCommander.Cli;

/// <summary>
/// A parsed command line. Positional[0] is the verb; everything after it is an operand.
/// Flags are accepted as <c>--name</c>, <c>--name=value</c> or <c>--name value</c>; the value form
/// is only used for flags that are not in the boolean set, so <c>--json game list</c> still parses.
/// </summary>
internal sealed class CliInvocation
{
    private static readonly HashSet<string> BooleanFlags = new(StringComparer.OrdinalIgnoreCase)
    {
        "json", "j", "quiet", "q", "help", "h", "?", "yes", "y",
        "dry-run", "dryrun", "force", "all", "offline", "verbose", "v", "attach", "no-gui",
    };

    internal string[] Positional { get; init; } = [];
    internal Dictionary<string, string?> Flags { get; init; } = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>Extra words that belong to the command name itself ("get" in "setting get").</summary>
    internal int Consumed { get; init; }

    internal string Verb => Positional.Length > 0 ? Positional[0] : "";

    internal string[] Args => Positional.Length > 1 + Consumed ? Positional[(1 + Consumed)..] : [];

    internal bool Has(string name) => Flags.ContainsKey(name);

    internal string? Flag(string name) => Flags.TryGetValue(name, out var value) ? value : null;

    internal string? Operand(int index) => index < Args.Length ? Args[index] : null;

    internal string OperandText => Args.Length == 0 ? "" : string.Join(' ', Args);

    internal CliInvocation WithConsumed(int words) =>
        words == Consumed ? this : new CliInvocation { Positional = Positional, Flags = Flags, Consumed = words };

    internal bool Json => Has("json") || Has("j");
    internal bool Quiet => Has("quiet") || Has("q");
    internal bool DryRun => Has("dry-run") || Has("dryrun");
    internal bool Confirmed => Has("yes") || Has("y");

    private static bool IsFlagToken(string token) =>
        token.StartsWith("--", StringComparison.Ordinal) ||
        token is "-h" or "-?" or "-v" or "-y" or "-q" or "-j";

    internal static CliInvocation Parse(string[] args)
    {
        var positional = new List<string>(args.Length);
        var endOfFlags = false;
        var flags = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);

        for (var i = 0; i < args.Length; i++)
        {
            var token = args[i];
            if (string.IsNullOrEmpty(token)) continue;

            if (endOfFlags) { positional.Add(token); continue; }
            if (token == "--") { endOfFlags = true; continue; }

            if (IsFlagToken(token))
            {
                var name = token.TrimStart('-', '/');
                if (name.Length == 0) continue;

                string? value = null;
                var split = name.IndexOf('=');
                if (split >= 0)
                {
                    value = name[(split + 1)..];
                    name = name[..split];
                }
                else if (!BooleanFlags.Contains(name) && i + 1 < args.Length
                         && args[i + 1].Length > 0
                         && !args[i + 1].StartsWith("--", StringComparison.Ordinal))
                {
                    value = args[++i];
                }

                flags[name] = value;
                continue;
            }

            positional.Add(token);
        }

        return new CliInvocation { Positional = positional.ToArray(), Flags = flags };
    }
}
