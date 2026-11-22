using RenoDXCommander.Cli;
using Xunit;

namespace RenoDXCommander.Tests;

/// <summary>The ini writer used by `rhi ini set`: values change in place, comments and order survive,
/// and a missing key or section is created rather than appended blindly.</summary>
public class CliIniTests : IDisposable
{
    private readonly string _dir = Path.Combine(Path.GetTempPath(), "rhi-cli-ini-" + Guid.NewGuid().ToString("N"));
    private string FilePath => Path.Combine(_dir, "ReShade.ini");

    public CliIniTests()
    {
        Directory.CreateDirectory(_dir);
        File.WriteAllText(FilePath,
            "[GENERAL]" + Environment.NewLine +
            "KeyOverlay=36,0,0,0" + Environment.NewLine +
            "; keep this comment" + Environment.NewLine +
            "PerformanceMode=1" + Environment.NewLine +
            Environment.NewLine +
            "[SCREENSHOT]" + Environment.NewLine +
            "SavePath=C:\\shots" + Environment.NewLine);
    }

    public void Dispose()
    {
        try { Directory.Delete(_dir, recursive: true); } catch (IOException) { }
    }

    [Fact]
    public void ReadsValuesCaseInsensitively()
    {
        var ini = CliIni.IniFile.Load(FilePath);

        Assert.Equal("36,0,0,0", ini.Get("general", "keyoverlay"));
        Assert.Equal("C:\\shots", ini.Get("SCREENSHOT", "savepath"));
        Assert.Null(ini.Get("GENERAL", "missing"));
    }

    [Fact]
    public void SettingAnExistingKeyKeepsCommentsAndOrder()
    {
        var ini = CliIni.IniFile.Load(FilePath);
        ini.Set("GENERAL", "KeyOverlay", "35,0,0,0");
        ini.Save();

        var lines = File.ReadAllLines(FilePath);
        Assert.Equal("KeyOverlay=35,0,0,0", lines[1]);
        Assert.Equal("; keep this comment", lines[2]);
        Assert.Equal("PerformanceMode=1", lines[3]);
        Assert.True(File.Exists(FilePath + ".bak"));
    }

    [Fact]
    public void NewKeyGoesIntoItsSection()
    {
        var ini = CliIni.IniFile.Load(FilePath);
        ini.Set("GENERAL", "NewKey", "7");
        ini.Save();

        var lines = File.ReadAllLines(FilePath);
        Assert.Equal("NewKey=7", lines[1]);
        Assert.Equal("[GENERAL]", lines[0]);
    }

    [Fact]
    public void UnknownSectionIsAppended()
    {
        var ini = CliIni.IniFile.Load(FilePath);
        ini.Set("DXGI", "MaxFrameLatency", "1");
        ini.Save();

        var text = File.ReadAllText(FilePath);
        Assert.Contains("[DXGI]", text);
        Assert.Contains("MaxFrameLatency=1", text);
    }
}
