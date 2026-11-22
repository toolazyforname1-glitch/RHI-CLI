namespace RenoDXCommander.Cli;

/// <summary>
/// Handles the terminal host shares with the command implementations. Set once by MainWindow, so a
/// command running inside the GUI (over the pipe) can reach the live window.
/// </summary>
internal static class CliRuntime
{
    internal static MainWindow? Window { get; set; }
}
