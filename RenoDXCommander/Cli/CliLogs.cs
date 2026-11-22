using System.IO.Compression;

namespace RenoDXCommander.Cli;

/// <summary>
/// The GUI's "copy logs" button builds a zip and puts it on the clipboard. A terminal cannot use a
/// clipboard, so this writes the same archive to a file instead - which is what a script wants.
/// </summary>
internal static class CliLogs
{
    internal static readonly CliCommand[] All = [Archive()];

    private static CliCommand Archive() => new()
    {
        Name = "logs archive",
        Group = "data",
        Summary = "Zip the RHI session logs to a file.",
        Usage = "rhi logs archive [--out <path>] [--json]",
        Examples = ["rhi logs archive", "rhi logs archive --out C:\\temp\\rhi-logs.zip"],
        Run = Run,
    };

    private static CliResult Run(CliInvocation invocation)
    {
        var directory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RHI", "logs");

        if (!Directory.Exists(directory)) return CliResult.Fail($"no logs directory at {directory}");

        var files = Directory.GetFiles(directory, "*.txt");
        if (files.Length == 0) return CliResult.Fail($"no .txt session logs in {directory}");

        var target = invocation.Flag("out")
                     ?? Path.Combine(Path.GetTempPath(), $"RHI_Logs_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.zip");

        if (File.Exists(target)) return CliResult.Fail($"{target} already exists - choose another path with --out");

        var folder = Path.GetDirectoryName(target);
        if (!string.IsNullOrEmpty(folder) && !Directory.Exists(folder))
            return CliResult.Fail($"folder does not exist: {folder}");

        try
        {
            using var archive = ZipFile.Open(target, ZipArchiveMode.Create);
            foreach (var file in files) archive.CreateEntryFromFile(file, Path.GetFileName(file));
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            return CliResult.Fail($"could not create {target}: {ex.Message}");
        }

        return CliResult.Ok($"{files.Length} log file(s) archived to {target}", new { target, files = files.Length });
    }
}
