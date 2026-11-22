# Parity gaps (handler audit)

Three parallel audits covered every event handler in the files the generated inventory had
only grouped. Counts below are per handler, not per file.

| area | handlers | mapped to a command | generic setting | ui-only | gap |
|---|---|---|---|---|---|
| MainWindow.Events.Settings.cs | 94 | 2 | 32 | 20 | 40 |
| MainWindow.Events.cs + MainWindow.Events.Install.cs | 88 | 30 | 8 | 34 | 16 |
| InstallEventHandler.cs, MassDeployHandler.cs, SettingsHandler.{Accounts,Hotkeys,DataTools,Channels}.cs | 39 | 19 | 6 | 0 | 14 |
| **total** | **221** | **51** | **46** | **54** | **70** |

`docs/handler-map.md` lists 175 handlers; the audits found 221 event-handler methods in these
files. The generated map only covers handlers wired in XAML, so its count is a floor, not the
total.

## Gaps, grouped by what a terminal user cannot do yet

### Mass deploy and presets
- `relimiter.ini`, `DisplayCommander.ini`, `OptiScaler.ini` mass deploy; `mass deploy` accepts
  only `reshade`.
- ReShade preset mass install (pick presets, choose games, optionally install their shaders).
- Batch DLSS/Streamline version and preset deploy.

### ReShade
- Vulkan and GAC install/uninstall branches (`RequiresVulkanInstall` games).
- Shader-pack selection and `DeployAllShaders`.
- ReShade addon manager and global addon deploy.
- `mass deploy reshade` does not write the effect-list style (`RsVariableListUseTabs`), so the
  "Apply hotkeys and paths" button is only partly covered.
- `mass deploy reshade` does not load `PeakNits` into `AuxInstallService.GlobalPeakNits`, so
  "Apply peak nits to all" is not reachable.

### Components
- OptiScaler post-install: PD-Upscaler REFramework swap, Streamline/DLSS Enabler deploy, FG
  apply, INI profile/edits; uninstall side restores PD-Upscaler and cleans Engine.ini and
  per-game settings.
- Luma mode toggle (enable removes RenoDX/ReShade; disable removes Luma and artifacts).
- Luma uninstall cleanup: dgVoodoo/nvngx restore, ReShade redeploy, Engine.ini cleanup.

### NVIDIA driver globals (no CLI surface at all)
Shader cache size, shader precompile, G-Sync mode, G-Sync enable, global FPS cap, preferred
refresh rate, DMFG frame count, DMFG target FPS, ReBAR enable, ReBAR size, global VSync, power
mode, DLSS indicator (HKLM), G-Sync indicator.

### NVIDIA profiles and DLDSR
- Create missing profiles, export/import profile backup, reset all profiles, clear the NVIDIA
  shader caches.
- DLDSR apply, capture, delete and smoothness.

### Data and system
- Logs archive to clipboard, game-library JSON export, staging-cache purge.
- Admin-mode scheduled task create/delete.
- Checking the RHI app's own update (component checks exist; app self-update does not).
- `StartWithWindows` writes an HKCU Run key; `setting set` only persists the flag.
- Peak-nits auto-detect from the monitor.
- Nexus disconnect, `nxm://` protocol register/unregister.
- Add/remove games manually, library refresh and full cache rebuild.
## Closed since the audit

- 13 global NVIDIA driver settings - `nvidia show` / `nvidia set`.
- Per-game RTX HDR (enable, contrast, saturation, peak, middle grey, debanding) - `nvidia hdr`.
- Driver profiles - `nvidia profile create-missing|export|import|reset`.
- NVIDIA shader caches - `nvidia clear-cache`.
- `relimiter` / `displaycommander` / `optiscaler` ini mass deploy - `mass deploy <target>`.
- Shader pack catalogue - `reshade shaders`; install picks - `install reshade --packs`.
- Addon catalogue + single download - `reshade addons`.
- DLDSR show / captures / capture / apply / delete / smoothness - `dldsr`.
- Manual library entries - `game add` / `game remove`; rescan - `game refresh`.
- Start with Windows, nxm:// handler, Nexus disconnect - `system ...`.
- Session logs - `logs archive`.
- Luma mode toggle - `luma mode <game> on|off` (attach path; calls `ToggleLumaMode` on the live card).
- OptiScaler Streamline copy/remove - `optiscaler streamline`.
- RHI app update check - `update app` (check only; installing stays in the app).

## Remaining, and why they stay out of the CLI

- **ReShade Vulkan/GAC install** - a branch inside `InstallReShadeAsync`; the CLI refuses such
  games rather than half-installing them. Needs the installer branch lifted, not a new command.
- **OptiScaler post-install / Luma mode toggle and cleanup** - multi-step flows that mutate ReShade
  and component state; each needs its own read-then-verify pass.
- **Game-data export** - produces a Discord-share blob built from live card state.
- **App self-update** - a dialog flow; running it headless would prompt into nowhere.
- **Peak-nits auto-detect** - a slider action reading the monitor descriptor; the setting itself is
  already reachable with `setting set PeakNits`.
- **Admin scheduled task** - internal to `DldsrService`; the capability it serves is exposed via
  `dldsr apply` / `dldsr smoothness`.