using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// Purges the same staging caches as the GUI's "Purge Cached Files" button: the DLSS/DLSS-D/
/// DLSS-G/Streamline folders and the downloads tree except shaders, JSON metadata, and addons
/// currently installed on a game.
/// </summary>
internal static class CliCache
{
    internal static readonly CliCommand[] All = [Purge()];

    private static CliCommand Purge() => new()
    {
        Name = "cache purge",
        Group = "data",
        Summary = "Delete cached DLSS/Streamline/component downloads to free space (keeps shaders, addons in use, and metadata).",
        Usage = "rhi cache purge [--dry-run] [--yes]",
        Examples = ["rhi cache purge --dry-run", "rhi cache purge --yes"],
        Destructive = true,
        Run = RunPurge,
    };

    private static CliResult RunPurge(CliInvocation invocation)
    {
        if (invocation.DryRun)
        {
            var (dryFiles, dryBytes) = Purge(delete: false);
            return CliResult.Ok($"would delete {dryFiles} cached file(s), {Size(dryBytes)}",
                new { files = dryFiles, bytes = dryBytes, dryRun = true });
        }

        if (!invocation.Confirmed)
            return CliResult.Usage("refusing to delete cached downloads without --yes: rhi cache purge --yes");

        var (files, bytes) = Purge(delete: true);
        return CliResult.Ok($"purged {files} cached file(s), freed {Size(bytes)}",
            new { files, bytes, purged = true });
    }

    private static (int Files, long Bytes) Purge(bool delete)
    {
        var files = 0;
        long bytes = 0;
        var root = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RHI");

        foreach (var folder in new[] { "DLSS", "DLSS-D", "DLSS-G", "Streamline" }.Select(name => Path.Combine(root, name)))
        {
            if (!Directory.Exists(folder)) continue;

            foreach (var dir in Directory.GetDirectories(folder))
            {
                try
                {
                    var info = new DirectoryInfo(dir);
                    var size = info.EnumerateFiles("*", SearchOption.AllDirectories).Sum(f => f.Length);
                    var count = info.EnumerateFiles("*", SearchOption.AllDirectories).Count();
                    if (delete) info.Delete(recursive: true);
                    files += count;
                    bytes += size;
                }
                catch (Exception ex) when (ex is IOException or UnauthorizedAccessException) { }
            }

            foreach (var file in Directory.GetFiles(folder))
            {
                try
                {
                    var fi = new FileInfo(file);
                    if (delete) fi.Delete();
                    files++;
                    bytes += fi.Length;
                }
                catch (Exception ex) when (ex is IOException or UnauthorizedAccessException) { }
            }
        }

        var downloads = DownloadPaths.Root;
        if (Directory.Exists(downloads))
        {
            var inUse = InstalledAddonNames();

            foreach (var dir in Directory.GetDirectories(downloads))
            {
                var name = Path.GetFileName(dir);
                if (name.Equals("shaders", StringComparison.OrdinalIgnoreCase)) continue;

                var isRenodx = name.Equals("renodx", StringComparison.OrdinalIgnoreCase);

                foreach (var file in Directory.GetFiles(dir, "*", SearchOption.AllDirectories))
                {
                    if (Path.GetExtension(file).Equals(".json", StringComparison.OrdinalIgnoreCase)) continue;
                    if (isRenodx && inUse.Contains(Path.GetFileName(file))) continue;

                    try
                    {
                        var fi = new FileInfo(file);
                        if (delete) fi.Delete();
                        files++;
                        bytes += fi.Length;
                    }
                    catch (Exception ex) when (ex is IOException or UnauthorizedAccessException) { }
                }
            }

            foreach (var file in Directory.GetFiles(downloads))
            {
                try
                {
                    var fi = new FileInfo(file);
                    if (delete) fi.Delete();
                    files++;
                    bytes += fi.Length;
                }
                catch (Exception ex) when (ex is IOException or UnauthorizedAccessException) { }
            }
        }

        return (files, bytes);
    }

    private static HashSet<string> InstalledAddonNames() =>
        new(CliServices.Get<IAuxInstallService>().LoadAll()
                .Select(record => record.InstalledAs)
                .Where(name => !string.IsNullOrEmpty(name))
                .Select(name => name!),
            StringComparer.OrdinalIgnoreCase);

    private static string Size(long bytes) => bytes switch
    {
        >= 1_073_741_824 => $"{bytes / 1_073_741_824.0:F1} GB",
        >= 1_048_576 => $"{bytes / 1_048_576.0:F1} MB",
        >= 1024 => $"{bytes / 1024.0:F1} KB",
        _ => $"{bytes} bytes",
    };
}
