using System.Text;
using RenoDXCommander.Models;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// Reads and writes any ini/conf in a game folder - ReShade.ini, OptiScaler.ini, dxvk.conf,
/// DisplayCommander.ini. Line-preserving: comments, order and unknown keys survive a write,
/// and every write leaves a .bak next to the file.
/// </summary>
internal static class CliIni
{
    private static readonly string[] KnownFiles =
        ["ReShade.ini", "reshade.ini", "ReShade64.ini", "ReShade.Vulkan.ini", "OptiScaler.ini", "dxvk.conf", "DisplayCommander.ini"];

    internal static readonly CliCommand[] All =
    [
        ListCommand(), GetCommand(), SetCommand(), TemplateList(), TemplateGet(),
    ];

    private static CliCommand ListCommand() => new()
    {
        Name = "ini list",
        Group = "config",
        Summary = "List the keys in a game's ini file, or in a shipped template.",
        Usage = "rhi ini list <game> [--name <file>] [--section <name>] [--json]",
        Examples = ["rhi ini list \"Cyberpunk 2077\"", "rhi ini list cyberpunk --name ReShade.ini --section GENERAL"],
        Run = List,
    };

    private static CliCommand GetCommand() => new()
    {
        Name = "ini get",
        Group = "config",
        Summary = "Read one Section.Key from a game's ini file.",
        Usage = "rhi ini get <game> <Section.Key> [--name <file>] [--json]",
        Examples = ["rhi ini get cyberpunk GENERAL.KeyOverlay"],
        Run = invocation => Read(invocation, isTemplate: false),
    };

    private static CliCommand SetCommand() => new()
    {
        Name = "ini set",
        Group = "config",
        Summary = "Write one Section.Key in a game's ini file. Keeps a .bak.",
        Usage = "rhi ini set <game> <Section.Key> <value> [--name <file>] [--dry-run] [--yes]",
        Examples = ["rhi ini set cyberpunk GENERAL.KeyOverlay 36,0,0,0 --yes", "rhi ini set cyberpunk DXGI.MaxFrameLatency 1 --dry-run"],
        Destructive = true,
        Run = Set,
    };

    private static CliCommand TemplateList() => new()
    {
        Name = "ini template list",
        Group = "config",
        Summary = "List keys in a shipped template (ReShade.ini, OptiScaler_*.ini, dxvk.conf).",
        Usage = "rhi ini template list [--file <name>] [--section <name>] [--json]",
        Examples = ["rhi ini template list --file ReShade.ini"],
        Run = invocation => ListShipped(invocation),
    };

    private static CliCommand TemplateGet() => new()
    {
        Name = "ini template get",
        Group = "config",
        Summary = "Read one Section.Key from a shipped template.",
        Usage = "rhi ini template get <file> <Section.Key> [--json]",
        Run = invocation => TemplateRead(invocation),
    };

    private static CliResult List(CliInvocation invocation)
    {
        var game = CliGames.ResolveOne(invocation.Args.FirstOrDefault(), out var gameFailure);
        if (game is null) return gameFailure!;
        var path = ResolveFile(invocation, game, out var error);
        if (path is null) return error!;

        var ini = IniFile.Load(path);
        var section = invocation.Flag("section");
        var rows = ini.Entries.Where(e => section is null || e.Section.Equals(section, StringComparison.OrdinalIgnoreCase)).ToList();

        var text = new StringBuilder();
        foreach (var entry in rows) text.AppendLine(entry.Section.PadRight(24) + entry.Key.PadRight(40) + entry.Value);
        text.AppendLine();
        text.Append(rows.Count + " keys in " + path);

        return CliResult.Ok(text.ToString(), new
        {
            file = path,
            section,
            count = rows.Count,
            keys = rows.Select(e => new { e.Section, e.Key, e.Value }).ToArray(),
        });
    }

    private static CliResult Read(CliInvocation invocation, bool isTemplate)
    {
        var target = invocation.Operand(1);
        if (string.IsNullOrWhiteSpace(target)) return CliResult.Usage("usage: rhi ini get <game> <Section.Key>");
        if (!TrySplitKey(target, out var section, out var key)) return CliResult.Usage("key must be written as Section.Key");

        var game = CliGames.ResolveOne(invocation.Operand(0), out var gameFailure);
        if (game is null) return gameFailure!;
        var path = ResolveFile(invocation, game, out var error);
        if (path is null) return error!;

        var ini = IniFile.Load(path);
        var value = ini.Get(section, key);
        return value is null
            ? CliResult.Fail($"{section}.{key} is not present in {path}")
            : CliResult.Ok(value, new { file = path, section, key, value });
    }

    private static CliResult Set(CliInvocation invocation)
    {
        var args = invocation.Args;
        if (args.Length < 3) return CliResult.Usage("usage: rhi ini set <game> <Section.Key> <value> [--name <file>] [--dry-run]");
        if (!TrySplitKey(args[1], out var section, out var key)) return CliResult.Usage("key must be written as Section.Key");

        var value = string.Join(' ', args[2..]);
        var game = CliGames.ResolveOne(args[0], out var gameFailure);
        if (game is null) return gameFailure!;
        var path = ResolveFile(invocation, game, out var error);
        if (path is null) return error!;

        var ini = IniFile.Load(path);
        var previous = ini.Get(section, key);
        if (previous is not null && string.Equals(previous, value, StringComparison.Ordinal))
            return CliResult.Ok($"{section}.{key} is already '{value}' - nothing to change", new { file = path, section, key, value, changed = false });

        if (invocation.DryRun)
            return CliResult.Ok($"would set {section}.{key} = '{value}' in {path} (was '{previous ?? "<absent>"}')",
                new { file = path, section, key, value, previous, changed = true, dryRun = true });

        if (!invocation.Confirmed)
            return CliResult.Usage($"refusing to rewrite {path} without --yes: rhi ini set <game> {section}.{key} {value} --yes");

        ini.Set(section, key, value);
        if (!ini.Save())
            return CliResult.Fail($"could not write {path} - the backup or atomic replace failed, nothing was changed");

        return CliResult.Ok($"{section}.{key} = '{value}' in {path} (was '{previous ?? "<absent>"}'); backup kept",
            new { file = path, section, key, value, previous, changed = true });
    }

    private static CliResult ListShipped(CliInvocation invocation)
    {
        var name = invocation.Flag("file");
        if (string.IsNullOrWhiteSpace(name))
        {
            var shipped = ShippedTemplates();
            var text = new StringBuilder();
            foreach (var file in shipped) text.AppendLine(file);
            text.AppendLine();
            text.Append(shipped.Length + " templates next to the exe");
            return CliResult.Ok(text.ToString(), new { templates = shipped });
        }

        var path = Path.Combine(AppPaths.ExeDir, name);
        if (!File.Exists(path)) return CliResult.Fail($"no shipped template '{name}' next to the exe");

        var ini = IniFile.Load(path);
        var section = invocation.Flag("section");
        var rows = ini.Entries.Where(e => section is null || e.Section.Equals(section, StringComparison.OrdinalIgnoreCase)).ToList();
        var body = new StringBuilder();
        foreach (var entry in rows) body.AppendLine(entry.Section.PadRight(24) + entry.Key.PadRight(40) + entry.Value);
        body.AppendLine();
        body.Append(rows.Count + " keys in " + path);

        return CliResult.Ok(body.ToString(), new { file = path, section, count = rows.Count, keys = rows.Select(e => new { e.Section, e.Key, e.Value }).ToArray() });
    }

    private static CliResult TemplateRead(CliInvocation invocation)
    {
        var file = invocation.Operand(0);
        var target = invocation.Operand(1);
        if (string.IsNullOrWhiteSpace(file) || string.IsNullOrWhiteSpace(target))
            return CliResult.Usage("usage: rhi ini template get <file> <Section.Key>");
        if (!TrySplitKey(target, out var section, out var key)) return CliResult.Usage("key must be written as Section.Key");

        var path = Path.Combine(AppPaths.ExeDir, file);
        if (!File.Exists(path)) return CliResult.Fail($"no shipped template '{file}' next to the exe");

        var value = IniFile.Load(path).Get(section, key);
        return value is null
            ? CliResult.Fail($"{section}.{key} is not present in {path}")
            : CliResult.Ok(value, new { file = path, section, key, value });
    }

    private static string? ResolveFile(CliInvocation invocation, SavedGame game, out CliResult? error)
    {
        error = null;
        var name = invocation.Flag("name");
        if (!string.IsNullOrWhiteSpace(name))
        {
            var explicitPath = Path.Combine(game.InstallPath, name);
            if (File.Exists(explicitPath)) return explicitPath;
            error = CliResult.Fail($"no '{name}' in {game.InstallPath}");
            return null;
        }

        foreach (var candidate in KnownFiles)
        {
            var path = Path.Combine(game.InstallPath, candidate);
            if (File.Exists(path)) return path;
        }

        error = CliResult.Fail($"no known ini in {game.InstallPath} (looked for {string.Join(", ", KnownFiles)}) - pass --name <file>");
        return null;
    }

    private static string[] ShippedTemplates()
    {
        try
        {
            return Directory.GetFiles(AppPaths.ExeDir, "*.ini")
                .Select(Path.GetFileName)
                .Concat(Directory.GetFiles(AppPaths.ExeDir, "*.conf").Select(Path.GetFileName))
                .OfType<string>()
                .OrderBy(n => n, StringComparer.OrdinalIgnoreCase)
                .ToArray();
        }
        catch (IOException)
        {
            return [];
        }
    }

    private static bool TrySplitKey(string text, out string section, out string key)
    {
        section = "";
        key = "";
        var dot = text.IndexOf('.');
        if (dot <= 0 || dot == text.Length - 1) return false;
        section = text[..dot];
        key = text[(dot + 1)..];
        return true;
    }

    internal sealed class IniFile
    {
        private readonly string _path;
        private readonly List<string> _lines;
        private readonly Encoding _encoding;

        internal sealed record Entry(string Section, string Key, string Value);

        private IniFile(string path, List<string> lines, Encoding encoding)
        {
            _path = path;
            _lines = lines;
            _encoding = encoding;
        }

        internal static IniFile Load(string path)
        {
            var bytes = File.ReadAllBytes(path);
            var encoding = DetectEncoding(bytes);
            var text = encoding.GetString(bytes).TrimStart('\uFEFF');
            var lines = text.Replace("\r\n", "\n").Split('\n').ToList();
            if (lines.Count > 0 && lines[^1].Length == 0) lines.RemoveAt(lines.Count - 1);
            return new IniFile(path, lines, encoding);
        }

        private static Encoding DetectEncoding(byte[] bytes)
        {
            if (bytes.Length >= 3 && bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF) return new UTF8Encoding(true);
            if (bytes.Length >= 2 && bytes[0] == 0xFF && bytes[1] == 0xFE) return new UnicodeEncoding(false, true);
            if (bytes.Length >= 2 && bytes[0] == 0xFE && bytes[1] == 0xFF) return new UnicodeEncoding(true, true);

            try
            {
                _ = new UTF8Encoding(false, true).GetString(bytes);
                return new UTF8Encoding(false);
            }
            catch (DecoderFallbackException)
            {
                return Encoding.Latin1;
            }
        }

        internal List<Entry> Entries
        {
            get
            {
                var entries = new List<Entry>();
                var section = "";
                foreach (var raw in _lines)
                {
                    var line = raw.Trim();
                    if (line.Length == 0 || line[0] is ';' or '#') continue;
                    if (line[0] == '[' && line[^1] == ']') { section = line[1..^1].Trim(); continue; }
                    var eq = line.IndexOf('=');
                    if (eq <= 0) continue;
                    entries.Add(new Entry(section, line[..eq].Trim(), line[(eq + 1)..].Trim()));
                }
                return entries;
            }
        }

        internal string? Get(string section, string key)
        {
            var entry = Entries.FirstOrDefault(e =>
                e.Section.Equals(section, StringComparison.OrdinalIgnoreCase) && e.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
            return entry?.Value;
        }

        internal void Set(string section, string key, string value)
        {
            var current = "";
            var sectionEnd = -1;

            for (var i = 0; i < _lines.Count; i++)
            {
                var line = _lines[i].Trim();
                if (line.Length == 0 || line[0] is ';' or '#') continue;

                if (line[0] == '[' && line[^1] == ']')
                {
                    current = line[1..^1].Trim();
                    if (current.Equals(section, StringComparison.OrdinalIgnoreCase)) sectionEnd = i;
                    continue;
                }

                if (!current.Equals(section, StringComparison.OrdinalIgnoreCase)) continue;

                var eq = line.IndexOf('=');
                if (eq <= 0) continue;
                if (!line[..eq].Trim().Equals(key, StringComparison.OrdinalIgnoreCase)) continue;

                _lines[i] = key + "=" + value;
                return;
            }

            var entryLine = key + "=" + value;
            if (sectionEnd >= 0)
            {
                _lines.Insert(sectionEnd + 1, entryLine);
                return;
            }

            if (_lines.Count > 0 && _lines[^1].Trim().Length > 0) _lines.Add("");
            _lines.Add("[" + section + "]");
            _lines.Add(entryLine);
        }

        internal bool Save()
        {
            var backup = _path + ".bak";
            try
            {
                File.Copy(_path, backup, overwrite: true);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                return false;
            }

            var temp = _path + ".tmp";
            try
            {
                File.WriteAllLines(temp, _lines, _encoding);
                File.Move(temp, _path, overwrite: true);
                return true;
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                try { File.Delete(temp); } catch (IOException) { }
                return false;
            }
        }
    }
}
