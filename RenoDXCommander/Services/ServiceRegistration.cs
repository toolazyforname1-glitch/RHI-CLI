using Microsoft.Extensions.DependencyInjection;
using RenoDXCommander.ViewModels;

namespace RenoDXCommander.Services;

public static class ServiceRegistration
{
    public static void Configure(IServiceCollection services)
    {

        // Shared HttpClient — singleton with UserAgent header and optimised connection settings
        services.AddSingleton<HttpClient>(sp =>
        {
            var handler = new SocketsHttpHandler
            {
                // Allow modern protocols — HTTP/2 multiplexes streams over a single
                // TCP connection which avoids head-of-line blocking and dramatically
                // improves throughput from CDNs like GitHub Pages / Releases.
                EnableMultipleHttp2Connections = true,

                // Raise the per-server connection cap so parallel downloads from the
                // same host aren't serialised behind two sockets.
                MaxConnectionsPerServer = 16,

                // Keep connections alive between downloads so subsequent requests
                // skip the TCP + TLS handshake.
                PooledConnectionLifetime  = TimeSpan.FromMinutes(10),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(2),

                // Larger initial receive buffer reduces syscall overhead on fast links.
                InitialHttp2StreamWindowSize = 1024 * 1024, // 1 MB
            };

            // GitHub API token — raises rate limit from 60 to 5000 req/hour. Attached per request,
            // for GitHub hosts only, by GitHubTokenHandler (never a default header: this client
            // also talks to non-GitHub hosts and manifest URLs).
            GitHubTokenHandler.SetToken(DevUnlockService.GitHubApiToken);
            var client = new HttpClient(new GitHubTokenHandler(handler));
            client.DefaultRequestHeaders.Add("User-Agent", "RHI/2.0");
            // Per-request timeout — generous enough for large files on slow connections.
            // Individual services can set per-request timeouts via CancellationTokenSource
            // if they need tighter control.
            client.Timeout = TimeSpan.FromMinutes(10);
            client.DefaultRequestVersion = new Version(2, 0);
            client.DefaultVersionPolicy  = HttpVersionPolicy.RequestVersionOrLower;
            return client;
        });

        // Shared ETag cache for GitHub API conditional requests (304 Not Modified)
        services.AddSingleton<GitHubETagCache>();

        // Services — all singletons
        services.AddSingleton<IModInstallService, ModInstallService>();
        services.AddSingleton<IAuxInstallService, AuxInstallService>();
        services.AddSingleton<IWikiService, WikiService>();
        services.AddSingleton<IManifestService, ManifestService>();
        services.AddSingleton<IGameLibraryService, GameLibraryService>();
        services.AddSingleton<IReShadeUpdateService, ReShadeUpdateService>();
        services.AddSingleton<INormalReShadeUpdateService, NormalReShadeUpdateService>();
        services.AddSingleton<ReShadeNightlyService>();
        services.AddSingleton<IUpdateService, UpdateService>();
        services.AddSingleton<ILumaService, LumaService>();
        services.AddSingleton<IShaderPackService, ShaderPackService>();
        services.AddSingleton<IGameDetectionService, GameDetectionService>();
        services.AddSingleton<IPeHeaderService, PeHeaderService>();
        services.AddSingleton<ICrashReporter, CrashReporterService>();
        services.AddSingleton<IAuxFileService>(sp => sp.GetRequiredService<IAuxInstallService>() as AuxInstallService
            ?? throw new InvalidOperationException("IAuxInstallService must be AuxInstallService"));
        services.AddSingleton<IREFrameworkService, REFrameworkService>();
        services.AddSingleton<INexusModsService, NexusModsService>();
        services.AddSingleton<INexusUpdateService, NexusUpdateService>();
        services.AddSingleton<ISteamAppIdResolver, SteamAppIdResolver>();
        services.AddSingleton<IPcgwService, PcgwService>();
        services.AddSingleton<ICustomFolderScanService, CustomFolderScanService>();
        services.AddSingleton<IUltrawideFixService, UltrawideFixService>();
        services.AddSingleton<IUltraPlusService, UltraPlusService>();

        // ViewModels
        services.AddSingleton<SettingsViewModel>();
        services.AddSingleton<FilterViewModel>();

        // Extracted services
        services.AddSingleton<IUpdateOrchestrationService, UpdateOrchestrationService>();
        services.AddSingleton<IDllOverrideService, DllOverrideService>();
        services.AddSingleton<IGameNameService, GameNameService>();
        services.AddSingleton<IGameInitializationService, GameInitializationService>();
        services.AddSingleton<ISevenZipExtractor, ReShadeExtractor>();
        services.AddSingleton<IOptiScalerService, OptiScalerService>();
        services.AddSingleton<IOptiScalerWikiService, OptiScalerWikiService>();
        services.AddSingleton<IHdrDatabaseService, HdrDatabaseService>();
        services.AddSingleton<IDxvkService, DxvkService>();
        services.AddSingleton<IInstallFolderMigrationService, InstallFolderMigrationService>();
        // Lazy<IDxvkService> breaks the circular dependency between OptiScalerService ↔ DxvkService
        services.AddSingleton<Lazy<IDxvkService>>(sp => new Lazy<IDxvkService>(() => sp.GetRequiredService<IDxvkService>()));
        services.AddSingleton<IDlssStreamlineService, DlssStreamlineService>();
        services.AddSingleton<DlssPresetService>();
        services.AddSingleton<DofFixService>();
        services.AddSingleton<UltimateAsiLoaderService>();
        services.AddSingleton<DlssNrCostScalerService>();
        services.AddSingleton<Rtx40MfgService>();
        services.AddSingleton<Dlssg20_30Service>();
        services.AddSingleton<AutoUpdateService>();
        services.AddSingleton<DlssEnablerService>();
        services.AddSingleton<DgVoodooService>();
        services.AddSingleton<Renodx5AddonService>();
        services.AddSingleton<CustomReShadeHashService>();
        services.AddSingleton<SeenWikiModsService>();
        services.AddSingleton<SeenUltraPlusModsService>();
        services.AddSingleton<SeenLumaModsService>();
        services.AddSingleton<IUpdateLogService, UpdateLogService>();
        services.AddSingleton<IRenoDXDbService, RenoDXDbService>();
        services.AddSingleton<NexusDownloadService>();
        services.AddSingleton<NexusSsoService>();
        services.AddSingleton<GitHubAuthService>();
        services.AddSingleton<IDldsrService, DldsrService>();
        services.AddSingleton<IAutoHdrService, AutoHdrService>();
        services.AddSingleton<RtxEncoreService>();
        services.AddSingleton<UpliftService>();
        // Lazy<IDlssStreamlineService> breaks the circular dependency between OptiScalerService ↔ DlssStreamlineService
        services.AddSingleton<Lazy<IDlssStreamlineService>>(sp => new Lazy<IDlssStreamlineService>(() => sp.GetRequiredService<IDlssStreamlineService>()));

        services.AddSingleton<MainViewModel>();

        // Window — transient so each request creates a new instance
        services.AddTransient<MainWindow>();

    }

    public static void AfterBuild(IServiceProvider services)
    {
        var uplift = services.GetRequiredService<UpliftService>();
        ShaderPackService.ExtraGameFiles = uplift.ShaderFilesFor;
    }
}
