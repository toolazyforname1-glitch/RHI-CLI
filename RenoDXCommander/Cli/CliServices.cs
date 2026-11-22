using Microsoft.Extensions.DependencyInjection;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// The service graph for terminal use. Built from the same registration the GUI uses, so a command
/// drives exactly the services the app drives - no second implementation to drift.
/// </summary>
internal static class CliServices
{
    private static IServiceProvider? _provider;

    internal static IServiceProvider Provider => _provider ??= Build();

    internal static T Get<T>() where T : notnull => Provider.GetRequiredService<T>();

    private static IServiceProvider Build()
    {
        var services = new ServiceCollection();
        ServiceRegistration.Configure(services);
        var provider = services.BuildServiceProvider();
        App.Services = provider;
        ServiceRegistration.AfterBuild(provider);
        return provider;
    }
}
