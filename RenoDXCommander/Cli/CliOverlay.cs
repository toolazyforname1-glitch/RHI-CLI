namespace RenoDXCommander.Cli;

/// <summary>
/// RHI's own overlay (the Alt+` window). It lives in the app's process, so these commands are
/// forwarded over the named pipe and run there; without a running app they report that clearly.
/// </summary>
internal static class CliOverlay
{
    internal static readonly CliCommand[] All = [Toggle(), State()];

    private static CliCommand Toggle() => new()
    {
        Name = "overlay toggle",
        Group = "overlay",
        Summary = "Open or close RHI's in-game overlay, exactly like its hotkey.",
        Usage = "rhi overlay toggle [--json]",
        Examples = ["rhi overlay toggle"],
        RequiresGui = true,
        Run = _ =>
        {
            var window = CliRuntime.Window;
            if (window is null) return NotRunning();

            window.OverlayToggleFromCli();
            return CliResult.Ok("overlay toggled", new { toggled = true, visible = window.OverlayVisible });
        },
    };

    private static CliCommand State() => new()
    {
        Name = "overlay state",
        Group = "overlay",
        Summary = "Report whether RHI's overlay is open.",
        Usage = "rhi overlay state [--json]",
        RequiresGui = true,
        Run = _ =>
        {
            var window = CliRuntime.Window;
            if (window is null) return NotRunning();

            return CliResult.Ok(window.OverlayVisible ? "overlay is open" : "overlay is closed", new { visible = window.OverlayVisible });
        },
    };

    private static CliResult NotRunning() =>
        CliResult.Fail("RHI is not running - the overlay lives inside RHI's process. Start RHI and run this again.");
}
