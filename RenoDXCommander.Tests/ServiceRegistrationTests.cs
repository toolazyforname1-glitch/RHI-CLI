using Microsoft.Extensions.DependencyInjection;
using RenoDXCommander.Services;
using Xunit;

namespace RenoDXCommander.Tests;

/// <summary>
/// The CLI resolves services from the same registration the GUI uses. This locks that contract:
/// if the shared registration loses one of these, every command that depends on it breaks.
/// </summary>
public class ServiceRegistrationTests
{
    [Fact]
    public void RegistersEveryServiceTheCliResolves()
    {
        var services = new ServiceCollection();
        ServiceRegistration.Configure(services);
        using var provider = services.BuildServiceProvider();

        Assert.NotNull(provider.GetService<IGameLibraryService>());
        Assert.NotNull(provider.GetService<IAuxInstallService>());
        Assert.NotNull(provider.GetService<IModInstallService>());
        Assert.NotNull(provider.GetService<IRenoDXDbService>());
        Assert.NotNull(provider.GetService<ILumaService>());
        Assert.NotNull(provider.GetService<IDxvkService>());
        Assert.NotNull(provider.GetService<IOptiScalerService>());
        Assert.NotNull(provider.GetService<IREFrameworkService>());
        Assert.NotNull(provider.GetService<IPeHeaderService>());
        Assert.NotNull(provider.GetService<IAutoHdrService>());
        Assert.NotNull(provider.GetService<IUltraPlusService>());
        Assert.NotNull(provider.GetService<IUltrawideFixService>());
        Assert.NotNull(provider.GetService<DlssPresetService>());
        Assert.NotNull(provider.GetService<DofFixService>());
        Assert.NotNull(provider.GetService<UltimateAsiLoaderService>());
    }

    [Fact]
    public void RegistersKeyServicesAsSingletons()
    {
        var services = new ServiceCollection();
        ServiceRegistration.Configure(services);
        using var provider = services.BuildServiceProvider();

        Assert.Same(provider.GetService<IAuxInstallService>(), provider.GetService<IAuxInstallService>());
        Assert.Same(provider.GetService<IGameLibraryService>(), provider.GetService<IGameLibraryService>());
    }
}
