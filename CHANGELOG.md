# Changelog

## 2.0.0 — 2026-11-13

### Changed — this is a different tool, not an increment

- **The CLI now lives inside `RHI.exe`.** `rhictl` (the 1.x separate process) is gone. Commands are
  reached from `Program.Main` before WinUI starts, and resolve the app's own dependency graph, so a
  terminal action and a click run identical code.
- **The app source is no longer unmodified.** 2.0.0 adds a `Cli/` layer, extracts the DI
  registration into `Services/ServiceRegistration.cs`, adds an entry-point switch in `Program.cs`,
  a pipe listener in `MainWindow.Overlay.cs`, and makes `LaunchGameAsync` report success or failure.
  The five upstream touch points are in `patches/upstream-changes.diff`.
- **New command surface:** 97 commands (from ~40), each with `--json` and `--dry-run`;
  destructive ones require `--yes`.

### Added

- **Live ReShade control** — `rhi-live.addon32/64`, a byte-mode named pipe, and commands
  (`reshade live ping|effects|techniques|technique|preset|install`). Work executes on ReShade's
  render thread; verified on a 32-bit Unity game (global effects toggle) and a 64-bit Unity game
  (per-technique toggle).
- **Attach path** — commands that need the live window (`overlay toggle/state`, `game launch`,
  `game refresh`, `update all`, `update <component>`, `luma mode`) are forwarded over
  `RenoDXCommander_CliPipe` and answered with the same JSON envelope.
- **NVIDIA** — `nvidia show` / `nvidia set` (13 global driver settings), `nvidia hdr` (per-game RTX
  HDR), `nvidia profile create-missing|export|import|reset`, `nvidia clear-cache`.
- **DLDSR** — `dldsr show|captures|capture|apply|delete|smoothness`.
- **ReShade depth** — `reshade shaders`, `reshade addons`, `reshade preset show|deploy`, `install reshade --packs`, and a guard
  that refuses Vulkan/OpenGL games unless `--dll` names the proxy.
- **Config** — `ini list|get|set` for any game ini (line-preserving, encoding-preserving, atomic,
  `.bak`), `ini template list|get`, `dlss presets|preset|scale|driver`, `hdr auto`.
- **OptiScaler extras** — `optiscaler streamline|dlss-enabler|pdupscaler`; `mass deploy reshade` also applies the effect-list style and peak nits from settings.
- **Mass shader deploy** — `mass deploy shaders` resolves each game's selection exactly like the app and syncs every ReShade game in one pass.
- **Peak nits** — `nvidia peak-nits` reads the monitors' max luminance and optionally applies it as `PeakNits`.
- **Generic Luma** — `install luma --generic` installs a generic Unreal Engine entry (wiki Engine.ini keys, TAA flag, launch args, dgVoodoo/ReShade steps).
- **Preset library** — `reshade preset deploy --presets/--all/--shaders` resolves the .fx files a preset references to shader packs and installs them, matching the GUI's mass preset install.
- **Per-game OptiScaler** — `optiscaler fg` (input/output/Nvngx in OptiScaler.ini), `optiscaler ini` (profile + edits, `--apply`), `optiscaler deploy-flag`.
- **Cache purge** — `cache purge` clears DLSS/Streamline/component staging, keeping shaders, metadata, and in-use addons.
- **Luma uninstall** — restores the nvngx sentinel, keeps dgVoodoo2 when Feeder or standalone needs it, re-merges or reinstalls ReShade, and redeploys the managed shaders.
- **Frame generation** — `framegen dlssg` (install/on/off/remove), `framegen mfg` (placement; options stay in the mod menu), `rtx encore` (39 settings in `rtx-encore.jsonc`, NR DLL, install/remove with Intel runtimes).
- **Shaders and channels** — `reshade shaders set` writes the per-game pack selection and syncs `reshade-shaders`; `reshade channel` stores the per-game channel and reinstalls ReShade.
- **Install depth** — `install reshade` now handles the global Vulkan layer and XNA/GAC symlink branches, and OpenGL selects `opengl32.dll`; `uninstall reshade` detects all three.
- **OptiScaler uninstall** — restores the standard REFramework when PD-Upscaler was applied, removes the Engine.ini keys the toggles added, and clears the per-game OptiScaler settings.
- **Display modes** — `display modes|show|set` (per-item current value and the reason it is unavailable; switch or off with `--yes`).
- **Batch** — `update check|all|<component>`, `mass deploy reshade|relimiter|displaycommander|optiscaler`.
- **Library** — `game add|remove|refresh`, `game launch`, `game list|show`.
- **System** — `system startup`, `system nxm`, `system nexus disconnect`, `logs archive`,
  `update app`.
- **Accounts** — `auth status`, `auth github` (token from `GH_TOKEN`, validated against GitHub
  before anything is saved), `auth nexus` (SSO URL printed, key validated).
- **Tooling** — GUI inventory generator, parity report, handler map, and a usage-reference generator
  (`tools/`).

### Fixed

- Named-pipe commands could deadlock the UI thread; they now dispatch asynchronously.
- A malformed client could kill the pipe listener for the rest of the session.
- `-h` / `-?` were parsed but ignored, so `rhi ini set … -h` wrote the file.
- Write commands marked destructive did not all require `--yes`; one central gate now enforces it.
- `ini set` corrupted non-UTF-8 (ANSI) files and swallowed backup failures; it now preserves the
  encoding, replaces atomically, and fails loudly.
- `game launch` reported success when nothing launched.
- DisplayCommander/ReLimiter detection read install records that do not exist; it now detects the
  addon files, and records the filename so uninstall actually removes them.
- ReShade installs ignored `ReShadeNormal` records.
- Screenshot listing and deletion could touch another game's files in a shared screenshot folder.
- `install reshade` guessed a proxy DLL for games it could not inspect.

### Known limits

- Luma/PD-Upscaler/DLSS-Enabler inline install flows, the Discord game-data export, app self-update
  install, and peak-nits auto-detection are outside the CLI — see `docs/parity-gaps.md`.
- Some commands need RHI running, because they mutate the live view model.
- NVAPI writes can require an elevated shell.

