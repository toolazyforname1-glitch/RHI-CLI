# RHI 2.0.0 - developer guide: modifying the app

This guide is for changing RHI itself: adding or changing a mod install flow, a settings toggle, a
detail-panel action, a live ReShade capability, or the terminal command that exposes it. Version 2.0.0
contains a 97-command CLI that shares one implementation with the GUI - any change to a GUI action
should be mirrored in `RenoDXCommander/Cli/`.

## 1. Source map

### Entry and shell
| File | Responsibility |
|---|---|
| `RenoDXCommander/Program.cs` | Entry point. Intercepts `--cli` before `XamlGeneratedMain()`, then `CliHost.Run(args)`; otherwise starts WinUI. Returns 0/1/2. |
| `RenoDXCommander/App.xaml.cs` | Application resources; delegates DI to `Services/ServiceRegistration.Configure` / `AfterBuild`. |
| `RenoDXCommander/MainWindow*.xaml` and `MainWindow.Events.*.cs` | The window, its event handlers, install buttons, settings handlers, overlay. Partial classes split by area. |
| `RenoDXCommander/WindowStateManager.cs`, `UIFactory.cs`, `TabsTheme.cs` | Window state, theme/resource helpers, consistent controls. |
| `RenoDXCommander/SettingsHandler.cs` + `SettingsHandler.*.cs` | Settings-page events grouped by topic (General, Hotkeys, Channels, DataTools, Accounts, Overlay, DisplayModes, ...). |
| `RenoDXCommander/InstallEventHandler.cs` | The main install/update button handlers and the post-install hooks. |
| `RenoDXCommander/MassDeployHandler.cs`, `PresetPopupHelper.cs`, `ShaderPopupHelper*.cs` | Mass INI deployment, preset picker/deploy, shader picker. |
| `RenoDXCommander/DetailPanelBuilder*.cs` | Builds the per-game detail panel: rows, cog menus, buttons, sections (components, extras, neural rendering, overrides, frame gen, RTX Encore, DXVK, ...). |
| `RenoDXCommander/DragDropHandler.Addon.cs` | Addon drag-and-drop install path. |
| `RenoDXCommander/GameReportEncoder.cs` | Diagnostic report (GUI-bound: needs `XamlRoot` + `MainViewModel`). |

### View models
- `ViewModels/MainViewModel.cs` plus ~30 partials: `Init`, `BuildCards`, `Settings`, `Install`,
  `Install.Components`, `Install.Luma`, `Install.Nexus`, `DisplayModes`, `VulkanLayer`, `SourceEngine`,
  `Dxvk`, `MfgPlacement`, `ReShadeAsi`, `Uplift`, `Update*` (heartbeat, check, all, native stack,
  ReLimiter/DisplayCommander), `BackgroundScan`, `AutoStaging`, `CacheLoad`, `GameMatching`,
  `FolderMigration`, `RemovedGames`, `Collections`, `CustomFolders`, `Activity`, `UalHooked`.
- `ViewModels/GameCardViewModel.cs` plus per-component partials (`ReShade`, `RenoDX`, `REFramework`,
  `OptiScaler`, `Luma`, `Dxvk`, `DofFix`, `DlssStreamline`, `DisplayCommander`, `NvidiaProfile`,
  `NormalReShade`, `UltraLimiter`, `UI`): per-game status, action messages, installed records and
  commands.
- `ViewModels/SettingsViewModel.cs` - `ObservableProperty` settings that serialize to
  `settings.json`; `FilterViewModel.cs` - library filtering state.

### Services (headless-safe unless marked otherwise)
Key ones to know before adding anything:
- **Install/file state**: `IAuxInstallService`/`AuxInstallService` (+ `.Ini.cs`, `.GacSymlink.cs`
  partials), `IModInstallService`, `AuxInstalledRecord`, `AuxFileService`, `StaleRecords`,
  `SettingsFile`, `GameNameService`, `DllOverrideService`, `SentinelBackup`/`SentinelRestore`.
- **ReShade**: `ReShadeUpdateService`, `NormalReShadeUpdateService`, `ReShadeNightlyService`,
  `VulkanLayerService`, `VulkanFootprintService`, `OsIniEditor`, `PresetPopupHelper`,
  `ShaderPackService`/`IShaderPackService`, `ShaderResolver`, `TechniquesParser`, `UpliftService`.
- **Other mods**: `OptiScalerService`, `LumaService`, `DxvkService`, `DofFixService`, `DgVoodooService`,
  `UltimateAsiLoaderService`, `REFrameworkService`, `Dlssg20_30Service`, `Rtx40MfgService`,
  `RtxEncoreService`, `DlssEnablerService`, `RtxEncoreConfig`, `DldsrService`, `AutoHdrService`,
  `DisplayModeService`/`DisplayModeStore`, `UltrawideFixService`, `UltraPlusService`, `SourceEngineSetup`.
- **Platform/data**: `ManifestService`/`IManifestService`, `GameLibraryService`, `WikiService`,
  `IPeHeaderService`, `GraphicsApiDetector`, `SteamAppIdResolver`, `Steam*Service`, `XboxPaths`,
  `NexusModsService`, `NexusUpdateService`, `GitHubAuthService`, `UpdateService`,
  `UpdateOrchestrationService`, `AutoUpdateService`.
- `Services/ServiceRegistration.cs` - the single DI composition root for GUI **and** CLI.

### CLI (`RenoDXCommander/Cli/`, 45 files)
`CliHost` (parse/dispatch/gate), `CliRegistry` (command table), `CliCommand`, `CliInvocation`,
`CliResult`, `CliServices` (DI resolve), `CliConsole`, `CliPipe` + `CliRuntime` (attach tier), and
one or more `CliXxx.cs` per area (games, install, mods, settings, nvidia, framegen, encore, shaders,
display, preset, luma, mass deploy, live, auth, updates, UI, logs, cache, ...).

### Addon (`LiveAddon/`)
`RhiLiveAddon.cpp`, ReShade 6.8 headers, `build.bat`/`build64.bat` and `find-msvc.bat`. The csproj
copies the compiled `rhi-live.addon32/64` next to `RHI.exe`.

### Tests and docs
`RenoDXCommander.Tests/` (`CliHostTests`, `CliInvocationTests`, `CliRegistryTests`, `CliIniTests`,
`ServiceRegistrationTests`). `docs/` holds the command reference and generated inventories;
`tools/` holds the four generators; `patches/upstream-changes.diff` is the overlay diff.

## 2. How one GUI action flows

```
XAML click
  -> MainWindow.Events.*.cs / SettingsHandler.*.cs / DetailPanelBuilder* button
  -> InstallEventHandler.cs or MainViewModel*.cs command
  -> service method (file writes, registry/NVAPI calls, staging)
  -> GameCardViewModel status + record
  -> RequestDetailPanelRebuild / card.NotifyAll / status text
```

When you change an action, decide which layer owns it:
- **Pure file/registry work belongs in a service.** The CLI can call it headlessly.
- **Live card state belongs in `GameCardViewModel`/`MainViewModel`.** The CLI reaches it only via
  the attach pipe (`RequiresGui = true`), or must be given a headless equivalent.
- **Dialogs/pickers belong in the window layer** (`PresetPopupHelper`, `ShaderPopupHelper`,
  `DialogService.ShowSafeAsync`). For a CLI equivalent, expose the non-UI parts through a service.

## 3. Adding or changing a component - end-to-end checklist

1. **Model/record**: add the persisted shape (often in `Models/`) and the aux record type string if
   the component is installed into a game folder (`AuxInstallService.Type*`).
2. **Service**: add the method to the interface and implementation; use the existing helpers for
   staging (`EnsureLatestAsync`), ini merges (`MergeRsIni`, `MergeRsVulkanIni`, `WriteIniValue`),
   sentinel backups (`SentinelBackup`/`SentinelRestore`), and record persistence (`LoadAll`,
   `SaveRecord`). Do not write the same file two different ways.
3. **DI**: register the service in `Services/ServiceRegistration.cs`. Registering only in
   `App.xaml.cs` would break the CLI.
4. **State**: add status/action properties to the relevant `GameCardViewModel.*` partial and the
   command/property to `MainViewModel.Install.*` or `MainViewModel.Settings.cs`.
5. **UI**: add the row/cog menu in the matching `DetailPanelBuilder*.cs`, using `UIFactory` and
   existing `CogButtonKind`/resource keys; wire the click in `MainWindow.Events.Components.*` or
   `SettingsHandler.*.cs`; rebuild the panel via `RequestDetailPanelRebuild` and `NotifyAll`.
6. **Settings**: add an `ObservableProperty` to `SettingsViewModel` for global settings, or a
   set/dictionary to `GameNameService` for per-game settings, and reuse the exact JSON key that
   `SaveNameMappings` writes. Do not create a second settings store.
7. **CLI mirror**: add/extend a `Cli/CliXxx.cs` command that calls the same service method in the
   same order. Add `RegisterAll(X.All)` in `CliRegistry.cs`. If the action needs live card state,
   mark `RequiresGui` and implement the GUI-side handler in `MainWindow.Overlay.cs`/pipe.
8. **Tests**: add host/invocation tests for a new command shape; run the 526-test suite.
9. **Docs**: add the row to `docs/CLI.md`, update the count in `docs/CLI.md` and `README.md`, add a
   changelog entry, regenerate `docs/parity.md` and `USAGE.md`.
10. **Release**: re-copy `Cli/*.cs` into the release package, regenerate the upstream patch and
    re-run the fresh-checkout build proof.

## 4. Install-flow specifics

### ReShade
`IAuxInstallService.InstallReShadeAsync(gameName, installPath, shaderModeOverride, use32Bit,
filenameOverride, selectedPackIds, progress, screenshotSavePath, useNormalReShade, overlayHotkey,
screenshotHotkey, channel, store, mergeIni)` is the single entry point for the proxy install. It
stages the right build (stable/nightly/normal), writes the proxy DLL, merges `reshade.ini`, deploys
the shared preset and syncs shader packs, then saves an `AuxInstalledRecord`.
- Bitness: use `IPeHeaderService.DetectGameArchitecture` or the library cache; never guess x64.
- Vulkan-only games need the global layer (`VulkanLayerService.InstallLayer`, `SyncLayerToChannel`,
  `VulkanFootprintService.Create`) and a `MergeRsVulkanIni`; this writes under `C:\ProgramData` and
  HKLM and needs elevation.
- XNA/GAC games use `AuxInstallService.InstallGacSymlink`; uninstall locates links by their target.
- Uninstall: `IAuxInstallService.Uninstall(record)` + `RemoveRecord(record)` for proxy installs;
  footprint/link detection for the other two.

### OptiScaler
`OptiScalerService.InstallAsync(card, gpuType, dlssInputs, hotkey, variant)`; post-install state is
in `OsIniLibrary` (saved ini profiles), `OsIniEditor` (per-game edits in `Section|Key` ids), and
`GameNameService` dicts (`OsFg*`, `OsDeploy*`, `OsIni*`). Frame-generation values are written with
`OptiScalerService.ApplyFgSettings(installPath, input, output, nvngx)`.
Extras are service calls: `IOptiScalerService.DeployStreamlineToGame`,
`DlssEnablerService.InstallAsync`, `IREFrameworkService.InstallPdUpscalerAsync`,
`Dlssg20_30Service`, `Rtx40MfgService`, `RtxEncoreService`. The uninstall side must restore the
standard REFramework when PD-Upscaler was applied, move the MFG unlocker out of `OptiScaler\plugins`,
clean the conditional `Engine.ini` keys and clear the per-game settings.

### Luma
Named mods come from `ILumaService.FetchCompletedModsAsync`; generic Unreal entries from
`FetchGenericUeTableAsync` (134 rows). Install writes the Luma record, deploys DLSS with a sentinel
backup, forces `dxgi.dll` when dgVoodoo is needed, sets `EnableHDR`/`DisplayMode` in `reshade.ini`,
applies wiki `Engine.ini` keys and launch args, and tracks `LumaTaaEnabled`. Uninstall must preserve
dgVoodoo when Feeder or a standalone install needs it, restore/delete the `nvngx_dlss.dll` sentinel,
redeploy ReShade and re-sync shaders. The wiki `Engine.ini` keys and auto-set `-dx11` arg exist only
in the live card entry - neither GUI nor CLI can clean them after a restart.

### Shader packs
`IShaderPackService.AvailablePacks`, `EnsurePacksAsync`, `IsPackCachedAsync`, `ExpandPackDependencies`,
`GetExcludedFiles`, `SyncGameFolder(gameDir, selectedPackIds, exclusions)`. Selection resolution:
per-game `Off` -> none; `Custom` -> `ShaderPackService.CustomShaderSentinel`; `Select` ->
`PerGameShaderSelection[Name|Store]`; otherwise global `GlobalShadersOff` / `UseCustomShaders` /
`SelectedShaderPacks`. Preset shader installation parses `Techniques=` with `TechniquesParser`,
maps `.fx` files to packs with `ShaderResolver.Resolve`, expands dependencies, then syncs.

### NVIDIA / display
`IDldsrService`, `IAutoHdrService`, `DlssPresetService`, `DlssNrCostScalerService`, NVAPI wrappers
and `DisplayModeService` back the driver/profile/DLDSR/display-mode commands. Peak nits reads
`Windows.Devices.Display.DisplayMonitor.MaxLuminanceInNits` via WinRT (same code in the CLI).

## 5. Settings and persistence rules

- `%LOCALAPPDATA%\RHI\settings.json` holds global settings; `SettingsFile.Load()` /
  `Snapshot()` / `Update(mutator, tag)` are the only APIs to use. Objects and arrays are JSON
  strings inside the dictionary.
- Per-game state lives in `GameNameService` (sets/dicts) and is persisted by `SaveNameMappings`,
  which needs the full ViewModel graph. Headless code must write the same JSON keys directly with
  `SettingsFile.Update`; calling `SaveNameMappings` from a CLI command would wipe unrelated state.
- Game key = `"Name|Store"`; look up the composite first, then the legacy bare name.
- Aux install records are in `%LOCALAPPDATA%\RHI\aux_installed.json`; Luma records are managed by
  `LumaService.GetAllRecords`/`SaveLumaRecord`.
- Always back up before overwriting a DLL (`SentinelBackup`) and restore with `SentinelRestore`;
  a zero-byte sentinel means RHI placed the file and it should be deleted on uninstall.
- `ini set` preserves encoding, writes a `.bak` and replaces atomically; never hand-roll a weaker
  version in a new command.

## 6. UI conventions

- Build rows with `UIFactory` and existing resource keys; cog menus use `CogButtonKind`.
- Dialogs go through `DialogService.ShowSafeAsync` so exceptions cannot strand a modal window.
- After changing state: `card.NotifyAll()`, `card.FadeMessage(...)`, `RequestDetailPanelRebuild`.
- Hotkeys are formatted via `HotkeyManager`; settings text boxes save through their
  `SettingsHandler.*` handlers (for example `TrySavePeakNitsFromBox`).
- The tray/overlay are initialized in `MainWindow.Overlay.cs`: `CliRuntime.Window = this` and
  `CliPipe.StartServer(DispatcherQueue)`. Add attach-only actions there and expose them as
  `internal` methods the pipe can call on the UI thread.

## 7. Live ReShade addon

- Protocol: named pipe `RHI_ReShadeAddon`, byte mode, newline-terminated UTF-8 requests/responses.
  The addon must call `ConnectNamedPipe` before reading and performs work on
  `addon_event::reshade_present` (never from the pipe thread).
- To add a capability: extend the command dispatcher in `LiveAddon/RhiLiveAddon.cpp`, add the CLI
  verb in `Cli/CliLive.cs`, and keep the response shape in sync.
- Logs are written only when `rhi-live.debug` exists next to the addon.
- Build with `LiveAddon/build.bat` (x86) and `build64.bat` (x64); `find-msvc.bat` locates
  `vcvarsall.bat` through `%VCVARS%`, `vswhere` then common VS 2019/2022 paths. The csproj copies
  both `.addon` files to the output directory.

## 8. Testing and verification

```powershell
$env:DOTNET_NOLOGO = '1'
$dotnet = "$env:USERPROFILE\.dotnet\dotnet.exe"   # SDK 8.0.427; no dotnet on PATH
& $dotnet build RenoDXCommander\RenoDXCommander.csproj -c Debug -p:Platform=x64
& $dotnet test  RenoDXCommander.Tests\RenoDXCommander.Tests.csproj -c Debug -p:Platform=x64
```

Expect 0 build errors and 526 passing tests. Then:
1. `rhi commands --json` - count 97 and parse the envelope.
2. `rhi help <name>` for every command - all exit 0.
3. One real invocation of the changed command with `--json` and one with `--dry-run`.
4. Fixture library: write a temporary `%LOCALAPPDATA%\RHI\game_library.json` pointing at copies under
   `TestGames`, then delete it. Never test against the user's installed games.
5. Fresh-checkout proof: overlay the release files on upstream `5f409de9` in a detached worktree,
   build x64, confirm 0 errors, both addons present, and `RHI.exe --cli version` answers.
6. Live addon: start a test game, wait for it to load, then `rhi reshade live ping`, `techniques`,
   `technique <name> off/on`.

## 9. Documentation generators and release

| Script | Output |
|---|---|
| `tools/New-UsageReference.ps1 -RhiBat <path>` | `USAGE.md`, spliced between `<!-- BEGIN/END COMMAND REFERENCE -->` |
| `tools/New-ParityReport.ps1` | `docs/parity.md` (97 commands / 108 settings / 4054 ini / 12 of 12 components) |
| `tools/New-GuiInventory.ps1` | `docs/gui-inventory.json` + `.md` |
| `tools/New-HandlerMap.ps1` | `docs/handler-map.md` |

Release package layout: root `README.md`, `USAGE.md`, `CHANGELOG.md`, `dev.md`, `LICENSE`,
`.gitignore`; `cli/`; `docs/`; `tools/`; `LiveAddon/`; `patches/`; `RenoDXCommander/` (five patched
upstream files, all `Cli/*.cs`, `Services/ServiceRegistration.cs`); `RenoDXCommander.Tests/`.

After code changes, re-copy `RenoDXCommander/Cli/*.cs`, regenerate `USAGE.md`, and regenerate the
patch with:

```powershell
git -c core.autocrlf=false diff 5f409de9..HEAD -- `
  RenoDXCommander/App.xaml.cs `
  RenoDXCommander/MainWindow.Events.Install.cs `
  RenoDXCommander/MainWindow.Overlay.cs `
  RenoDXCommander/Program.cs `
  RenoDXCommander/RenoDXCommander.csproj `
  --output=patches/upstream-changes.diff
```

`git apply --stat` is the structural check; a reverse apply can fail on the mixed line endings and
mojibake present in two upstream files.

## 10. Gotchas

- `cli/rhi.bat` ends with `< nul` so `cmd` waits for the GUI-subsystem exe and preserves
  `%ERRORLEVEL%`. Removing it breaks every command.
- On `cmd /c`, quoted arguments inside a quoted path are stripped; use a small `.cmd` wrapper.
- PowerShell `$args` is reserved - never use it as a generator parameter name.
- Do not set `ReadTimeout` on named-pipe streams (throws); use a `Task` with bounded `Wait`.
- Protocol writes must be raw UTF-8 bytes without a BOM; a `StreamWriter` BOM breaks parsing and its
  dispose-time flush can throw after a peer disconnect.
- The repo mixes CRLF/LF and contains a few mojibake strings; prefer line-based edits, and rebuild
  after every scripted change. Interpolated ternary expressions need parentheses:
  `{(cond ? "a" : "b")}`.
- Subcommands do not add commands: count registry entries with `rhi commands --json`.
- `--dry-run` must never write; `Destructive` commands are gated centrally, write-capable
  non-destructive commands must gate themselves with `invocation.Confirmed`.
- Never commit tokens, `.env` files, private keys or local machine paths. GitHub tokens come from
  `GH_TOKEN`/`gh auth`; local paths in generated docs should be scrubbed before release.

## 11. Known limits and deliberate exclusions

- Implemented but untested on matching hardware: Vulkan/GAC install and uninstall, legacy channel
  downloads, real shader-pack selection, MFG plugin placement, NR DLL deployment, PD-Upscaler
  restore, generic Luma positive install, DLSSG fallback proxy name.
- GUI-bound: `GameReportEncoder` diagnostic report, app self-update install (`AutoUpdateService`
  needs a configured `MainViewModel`), clipboard log copy, dialogs/pickers.
- Out of scope by design: game-data export, peak-nits auto-detect as a button equivalent (the value
  is settable with `setting set PeakNits`), the admin scheduled task (its capability is
  `dldsr apply`).

## Credits

RHI by [RankFTW](https://github.com/RankFTW/RHI). ReShade addon headers are the 6.8.0 SDK from
[crosire/reshade](https://github.com/crosire/reshade). ReShade itself is by crosire.
