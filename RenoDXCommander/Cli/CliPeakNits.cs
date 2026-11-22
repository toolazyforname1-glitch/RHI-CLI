using System.Globalization;
using System.Text;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// Reads the peak luminance Windows reports for every attached display - the same WinRT calls the
/// Settings page's Auto button uses - and can persist the brightest one as the PeakNits setting.
/// </summary>
internal static class CliPeakNits
{
    private const string SettingKey = "PeakNits";

    internal static readonly CliCommand[] All = [PeakNits()];

    private static CliCommand PeakNits() => new()
    {
        Name = "nvidia peak-nits",
        Group = "config",
        Summary = "Read the brightest monitor's peak luminance and optionally apply it as the PeakNits setting.",
        Usage = "rhi nvidia peak-nits [--set] [--json]",
        Examples =
        [
            "rhi nvidia peak-nits",
            "rhi nvidia peak-nits --set --dry-run",
            "rhi nvidia peak-nits --set --yes",
        ],
        Destructive = false,
        Run = invocation => RunAsync(invocation).GetAwaiter().GetResult(),
    };

    private static async Task<CliResult> RunAsync(CliInvocation invocation)
    {
        var settings = SettingsFile.Load();
        var previous = settings.TryGetValue(SettingKey, out var raw)
            && int.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out var stored)
                ? stored
                : 0;

        var monitors = new List<(string Id, string Name, float Nits)>();
        float maxNits = 0;

        try
        {
            var devices = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(
                Windows.Devices.Display.DisplayMonitor.GetDeviceSelector());

            foreach (var device in devices)
            {
                try
                {
                    var monitor = await Windows.Devices.Display.DisplayMonitor.FromInterfaceIdAsync(device.Id);
                    monitors.Add((device.Id, device.Name, monitor.MaxLuminanceInNits));
                    if (monitor.MaxLuminanceInNits > maxNits) maxNits = monitor.MaxLuminanceInNits;
                }
                catch (Exception ex)
                {
                    CrashReporter.Log($"[CliPeakNits] Failed to read monitor peak luminance - {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            CrashReporter.Log($"[CliPeakNits] Failed to enumerate monitors - {ex.Message}");
            return CliResult.Fail($"could not enumerate monitors: {ex.Message}");
        }

        if (monitors.Count == 0 || (int)maxNits <= 0)
            return CliResult.Fail("no monitor reported a peak luminance", new
            {
                settingsFile = SettingsFile.FilePath,
                monitors = monitors.Count,
                peakLuminance = Math.Round(maxNits, 1),
                PeakNits = previous,
            });

        var peakNits = (int)maxNits;
        var text = new StringBuilder();
        foreach (var monitor in monitors)
            text.AppendLine($"{monitor.Id}  {monitor.Name}  {monitor.Nits.ToString("0.#", CultureInfo.InvariantCulture)} nits");
        text.AppendLine();
        text.AppendLine("peak".PadRight(10) + peakNits + " nits");
        text.AppendLine("PeakNits".PadRight(10) + Old(previous));
        text.AppendLine("settings".PadRight(10) + SettingsFile.FilePath);

        if (!invocation.Has("set"))
            return CliResult.Ok(text.ToString().TrimEnd(), new
            {
                settingsFile = SettingsFile.FilePath,
                monitors = monitors.Select(m => new { id = m.Id, name = m.Name, nits = Math.Round(m.Nits, 1) }).ToArray(),
                peakNits,
                PeakNits = previous,
                changed = false,
            });

        if (settings.ContainsKey(SettingKey) && previous == peakNits)
            return CliResult.Ok($"PeakNits is already {peakNits} - nothing to change",
                new { settingsFile = SettingsFile.FilePath, peakNits, previous, changed = false });

        if (invocation.DryRun)
            return CliResult.Ok($"would set PeakNits to {peakNits} (was {Old(previous)})",
                new { settingsFile = SettingsFile.FilePath, value = peakNits, previous, changed = false, dryRun = true });

        if (!invocation.Confirmed)
            return CliResult.Usage("refusing to write PeakNits without --yes: rhi nvidia peak-nits --set --yes");

        if (!SettingsFile.Update(store => store[SettingKey] = peakNits.ToString(CultureInfo.InvariantCulture), "cli:nvidia peak-nits"))
            return CliResult.Fail($"could not write {SettingsFile.FilePath} - file locked or unreadable");

        return CliResult.Ok($"PeakNits = {peakNits} (was {Old(previous)})",
            new { settingsFile = SettingsFile.FilePath, value = peakNits, previous, changed = true });
    }

    private static string Old(int value) => value > 0 ? value.ToString(CultureInfo.InvariantCulture) : "<unset>";
}
