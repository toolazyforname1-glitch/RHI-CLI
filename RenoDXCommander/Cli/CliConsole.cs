using System.Runtime.InteropServices;
using System.Text;

namespace RenoDXCommander.Cli;

/// <summary>
/// Makes stdout usable when RHI.exe (a GUI-subsystem executable) is started from a batch file.
/// A WinExe gets no console of its own, so the parent console is attached when stdout is a real
/// console; when stdout is redirected the inherited handle is used as-is. A private writer is used
/// instead of Console.Out so a GUI-subsystem start can never fall back to a null stream and
/// silently swallow the output.
/// </summary>
internal static class CliConsole
{
    private const int AttachParentProcess = -1;

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool AttachConsole(int processId);

    private static TextWriter? _writer;
    private static bool _attached;

    private static TextWriter Writer
    {
        get
        {
            if (_writer != null) return _writer;

            Attach();
            try
            {
                var stream = Console.OpenStandardOutput();
                _writer = new StreamWriter(stream, new UTF8Encoding(false), 4096) { AutoFlush = true };
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                _writer = TextWriter.Null;
            }

            return _writer;
        }
    }

    internal static void Attach()
    {
        if (_attached) return;
        _attached = true;
        if (Console.IsOutputRedirected) return;
        try { AttachConsole(AttachParentProcess); }
        catch (Exception ex) when (ex is DllNotFoundException or EntryPointNotFoundException) { }
    }

    internal static void WriteLine(string text)
    {
        try { Writer.WriteLine(text); }
        catch (IOException) { }
        catch (ObjectDisposedException) { }
    }

    internal static void Write(string text)
    {
        try { Writer.Write(text); }
        catch (IOException) { }
        catch (ObjectDisposedException) { }
    }
}
