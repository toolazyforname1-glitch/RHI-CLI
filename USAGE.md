# rhi — terminal control for RHI

> Version 2.0.0 · Windows 10/11 · works with RHI 2.9.2 and newer

RHI finds every game on your PC and manages ReShade, RenoDX, Luma, OptiScaler, DXVK, DLSS files,
NVIDIA driver settings and DLDSR for all of them. This is all of that, from a terminal, without
opening the window.

Every command runs through the same services, settings file and file locks as the GUI, so a
terminal install and a clicked install produce identical results — and a terminal command and a
click cannot corrupt each other's work.

## What changed in 2.0.0

The 1.x line shipped `rhictl`, a **separate** program that re-implemented part of the app and left
the WinUI app untouched (the fork's README advertised `git diff` on `RenoDXCommander/` as empty).
That ceiling is why 1.x could not do things like live ReShade control.

2.0.0 inverts it: the command layer lives **inside RHI.exe**. It is reached before WinUI starts, it
resolves the app's own dependency graph, and it adds live control over a running ReShade through a
small addon. The app source is no longer unmodified — that is the point. See `CHANGELOG.md`.

## Requirements

- Windows 10 1809 or later, or Windows 11. x64 host; the app can be built 32- or 64-bit.
- .NET 8 (RHI already ships what it needs).
- An RHI build that includes this tree. The commands are part of `RHI.exe`; there is no second
  binary to install.
- A game to act on — a Steam/Epic/GOG/Xbox install, or a manual folder added with `game add`.
- Optional: NVIDIA driver for `nvidia …` and `dldsr`, and an elevated shell for the settings that
  need it.

## Install

1. Apply `patches/upstream-changes.diff` to the RHI 2.9.2 source, or copy the files in this tree
   into your checkout (the layout mirrors the repo, so it is a straight drop-in).
2. Build the app (below). The build also copies `rhi-live.addon32/64` next to `RHI.exe`.
3. Put `cli/` wherever you like — it only contains batch files.
4. Point the dispatcher at your build, once:

```bat
setx RHI_CLI_EXE "C:\path\to\RHI.exe"
```

`rhi.bat` looks for the executable in this order: `RHI_CLI_EXE`, next to itself, the repo build
output (`bin\x64\Release` then `Debug`), `%LOCALAPPDATA%\Programs\RHI`, `%ProgramFiles%\RHI`.

5. Optional: add the `cli` folder to `PATH` so `rhi` works from anywhere.

## Build

```bat
dotnet build RenoDXCommander\RenoDXCommander.csproj -c Release -p:Platform=x64
LiveAddon\build.bat     :: 32-bit addon  (rhi-live.addon32)
LiveAddon\build64.bat   :: 64-bit addon  (rhi-live.addon64)
```

The addon builds need the MSVC Build Tools C++ workload; the ReShade headers under
`LiveAddon/include` are the 6.8.0 SDK and are already here.

## How a command runs

| situation | what happens | speed |
|---|---|---|
| RHI is closed, command needs no window | `RHI.exe --cli …` runs the command before WinUI starts — no window, no tray, no rhi UI | ~1 s |
| RHI is running, command needs no window | runs headless in a new short-lived process; safe while the GUI is open | ~1 s |
| command needs the live window (`overlay toggle/state`, `game launch`, `game refresh`, `update all`, `update <component>`, `luma mode`) | forwarded over the `RenoDXCommander_CliPipe` named pipe and run on RHI's UI thread | ~1 s + work |
| live ReShade command (`reshade live …`) | sent to `rhi-live.addon64/32` inside the running game, executed on the render thread | one frame |

The GUI-side listener answers with the same JSON envelope the CLI prints, so `--json` is identical
on both paths.

## Output contract

- exit `0` success, `1` failed, `2` bad usage.
- `--json` prints exactly one envelope and nothing else:

```json
{
  "ok": true,
  "command": "game list",
  "exitCode": 0,
  "message": "2 of 2 games",
  "data": { "count": 2, "total": 2, "games": [] }
}
```

- human output is the `message`; errors are messages too, never stack traces.
- `--quiet` suppresses the message, on success and on failure (the exit code still tells you).

## Safety model

- **`--dry-run` never writes.** Every command that can change something prints the plan and stops,
  including the live ReShade channel and the credential commands.
- **`--yes` is mandatory for destructive commands** - installs, removals, overrides, driver changes
  and credential storage. A mistyped script cannot uninstall anything by accident. Plain settings
  writes (`setting set`, `ui ...`) apply immediately, because they are one reversible value.
- **Backups.** `ini set` writes a `.bak` next to the file and replaces it atomically — a failed
  write leaves the original untouched. `nvidia profile import` overwrites driver profiles, so
  export first.
- **No secrets on the command line.** `auth github` reads the token from `GH_TOKEN`; passing it as
  an argument is refused on purpose (shell history, process lists).
- **Refusals over guesses.** `install reshade` will not pick a proxy DLL for a Vulkan/OpenGL game;
  `game remove` will not remove a detected game; `reshade live` says "no live channel" rather than
  pretending.

## Quick start

```bat
rhi doctor                              :: is everything wired up?
rhi game list                           :: what does RHI see?
rhi game show "Cyberpunk 2077"          :: versions, engine, install path
rhi install reshade "Cyberpunk 2077" --yes
rhi dlss preset sr "Cyberpunk 2077" J --yes
rhi reshade live ping                   :: with the game running
```

<!-- BEGIN COMMAND REFERENCE -->
## Command reference

97 commands, generated from the binary by `tools\New-UsageReference.ps1`.

Every command accepts `--json` (one envelope: `ok`, `command`, `exitCode`, `message`, `data`) and
returns exit `0` ok, `1` failed, `2` bad usage. Commands marked **write** change files or
settings and require `--yes`; add `--dry-run` to preview without changing anything.

### Auth

#### `rhi auth github`

Store a GitHub token taken from an environment variable, after GitHub confirms it.

```
set GH_TOKEN=<token> && rhi auth github [--from-env NAME] [--yes] [--json]   |   rhi auth github --clear --yes
```

```bat
set GH_TOKEN=ghp_xxx && rhi auth github --yes
```
```bat
rhi auth github --clear --yes
```

> **write** - needs `--yes`

#### `rhi auth nexus`

Sign in to Nexus through its SSO page and store the API key.

```
rhi auth nexus [--open] [--timeout 120] [--json]
```

```bat
rhi auth nexus --open
```

#### `rhi auth status`

Show whether GitHub and Nexus are connected (never prints the stored secrets).

```
rhi auth status [--json]
```

```bat
rhi auth status
```
```bat
rhi auth status --json
```

### Config

#### `rhi ini get`

Read one Section.Key from a game's ini file.

```
rhi ini get <game> <Section.Key> [--name <file>] [--json]
```

```bat
rhi ini get cyberpunk GENERAL.KeyOverlay
```

#### `rhi ini list`

List the keys in a game's ini file, or in a shipped template.

```
rhi ini list <game> [--name <file>] [--section <name>] [--json]
```

```bat
rhi ini list "Cyberpunk 2077"
```
```bat
rhi ini list cyberpunk --name ReShade.ini --section GENERAL
```

#### `rhi ini set`

Write one Section.Key in a game's ini file. Keeps a .bak.

```
rhi ini set <game> <Section.Key> <value> [--name <file>] [--dry-run] [--yes]
```

```bat
rhi ini set cyberpunk GENERAL.KeyOverlay 36,0,0,0 --yes
```
```bat
rhi ini set cyberpunk DXGI.MaxFrameLatency 1 --dry-run
```

> **write** - needs `--yes`

#### `rhi ini template get`

Read one Section.Key from a shipped template.

```
rhi ini template get <file> <Section.Key> [--json]
```

#### `rhi ini template list`

List keys in a shipped template (ReShade.ini, OptiScaler_*.ini, dxvk.conf).

```
rhi ini template list [--file <name>] [--section <name>] [--json]
```

```bat
rhi ini template list --file ReShade.ini
```

#### `rhi nvidia peak-nits`

Read the brightest monitor's peak luminance and optionally apply it as the PeakNits setting.

```
rhi nvidia peak-nits [--set] [--json]
```

```bat
rhi nvidia peak-nits
```
```bat
rhi nvidia peak-nits --set --dry-run
```
```bat
rhi nvidia peak-nits --set --yes
```

#### `rhi optiscaler deploy-flag`

Set the per-game 'deploy Streamline' / 'deploy DLSS Enabler' flags the next OptiScaler install uses.

```
rhi optiscaler deploy-flag <game> <streamline|dlss-enabler> <on|off> [--dry-run] [--yes]
```

```bat
rhi optiscaler deploy-flag cyberpunk streamline on --yes
```

#### `rhi optiscaler fg`

Show or set a game's frame-generation input/output/Nvngx replacement and apply it to OptiScaler.ini.

```
rhi optiscaler fg <game> [<input> [<output> [<nvngx>]]] [--dry-run] [--yes]
```

```bat
rhi optiscaler fg cyberpunk
```
```bat
rhi optiscaler fg cyberpunk dlssg dlssg auto --yes
```

#### `rhi optiscaler ini`

Remember a game's OptiScaler.ini profile and edits; --apply deploys the profile (or RHI's template) and re-applies them now.

```
rhi optiscaler ini <game> [<profile>|none] [--edits "Section|Key=value;Section|Key2=value2"] [--apply] [--dry-run] [--yes]
```

```bat
rhi optiscaler ini cyberpunk
```
```bat
rhi optiscaler ini cyberpunk MyPreset --edits "FrameGen|FGOutput=nvngxfg" --apply --yes
```

#### `rhi reshade addons`

List the addon catalogue, or download one addon.

```
rhi reshade addons [list] | rhi reshade addons download <id|name> [--dry-run] [--yes]
```

```bat
rhi reshade addons
```
```bat
rhi reshade addons download luma --yes
```

#### `rhi reshade channel`

Set a game's ReShade channel or version, then reinstall ReShade when it is installed.

```
rhi reshade channel <game> <stable|nightly|custom:<dll>|legacy:<version>|noaddons> [--dry-run] [--yes]
```

```bat
rhi reshade channel "Cyberpunk 2077" nightly --yes
```
```bat
rhi reshade channel cyberpunk legacy:6.4.1 --yes
```
```bat
rhi reshade channel cyberpunk custom:ReShade64.dll --yes
```
```bat
rhi reshade channel cyberpunk stable --dry-run
```

#### `rhi reshade live`

Control ReShade inside a running game (ping, effects, techniques, technique, preset) or install the addon.

```
rhi reshade live <ping|effects [on|off]|techniques|technique <name> <on|off>|preset [path]>  |  rhi reshade live install <game> [--yes]
```

```bat
rhi reshade live install "Mini Motorways"
```
```bat
rhi reshade live ping
```
```bat
rhi reshade live effects off
```
```bat
rhi reshade live techniques
```
```bat
rhi reshade live technique SMAA on
```

#### `rhi reshade preset deploy`

Copy the shared ReShadePreset.ini into a game, or deploy named presets from the preset library (optionally with their shader packs).

```
rhi reshade preset deploy <game|--all> [--presets a,b] [--shaders] [--dry-run] [--yes]
```

```bat
rhi reshade preset deploy cyberpunk --yes
```
```bat
rhi reshade preset deploy cyberpunk --presets NaturalVision,ReshadeReal --shaders --yes
```
```bat
rhi reshade preset deploy --all --presets NaturalVision --yes
```

> **write** - needs `--yes`

#### `rhi reshade preset show`

Show the shared ReShadePreset.ini RHI ships and whether the game has a copy.

```
rhi reshade preset show <game> [--json]
```

```bat
rhi reshade preset show cyberpunk
```

#### `rhi reshade shaders`

List the shader packs RHI can install, and whether each is already cached.

```
rhi reshade shaders [--json]
```

```bat
rhi reshade shaders
```

#### `rhi reshade shaders set`

Choose the shader packs a game deploys, or turn managed shaders off.

```
rhi reshade shaders set <game> <id,id|off> [--dry-run] [--yes]
```

```bat
rhi reshade shaders set "Cyberpunk 2077" off --yes
```
```bat
rhi reshade shaders set cyberpunk "OtisFX,Depth3D" --yes
```

### Core

#### `rhi commands`

Machine-readable list of every command (use with --json).

```
rhi commands [--json]
```

```bat
rhi commands --json
```

#### `rhi doctor`

Check the install: exe, settings, data folder, 7-Zip, elevation.

```
rhi doctor [--json]
```

```bat
rhi doctor
```
```bat
rhi doctor --json
```

#### `rhi help`

List every command, or explain one command.

```
rhi help [command]
```

```bat
rhi help
```
```bat
rhi help doctor
```

#### `rhi status`

One-screen summary: version, library, installed components, pending updates, overlays.

```
rhi status [--json]
```

```bat
rhi status
```

#### `rhi version`

Show the RHI version, executable path and platform.

```
rhi version [--json]
```

### Data

#### `rhi cache purge`

Delete cached DLSS/Streamline/component downloads to free space (keeps shaders, addons in use, and metadata).

```
rhi cache purge [--dry-run] [--yes]
```

```bat
rhi cache purge --dry-run
```
```bat
rhi cache purge --yes
```

> **write** - needs `--yes`

#### `rhi logs archive`

Zip the RHI session logs to a file.

```
rhi logs archive [--out <path>] [--json]
```

```bat
rhi logs archive
```
```bat
rhi logs archive --out C:\temp\rhi-logs.zip
```

### Display

#### `rhi display modes`

List the display modes and which one this game is in.

```
rhi display modes <game> [--json]
```

```bat
rhi display modes cyberpunk
```

#### `rhi display set`

Switch a game to a display mode, or turn modes off.

```
rhi display set <game> <mode|off> [--dry-run] [--yes] [--json]
```

```bat
rhi display set cyberpunk monitor --yes
```
```bat
rhi display set cyberpunk off --dry-run
```

> **write** - needs `--yes`

#### `rhi display show`

Show every display-mode item for a game: its current value, or why it is unavailable.

```
rhi display show <game> [--json]
```

```bat
rhi display show cyberpunk
```

#### `rhi dldsr`

Show DLDSR state, list captures, capture/apply/delete a state, or set smoothness.

```
rhi dldsr [show|captures|apply <label> [--smoothness n]|capture <label>|delete <label>|smoothness <n>] [--dry-run] [--yes]
```

```bat
rhi dldsr show
```
```bat
rhi dldsr captures
```
```bat
rhi dldsr apply 4K --smoothness 50 --yes
```

### Dlss

#### `rhi dlss driver`

Turn the driver-level preset override on or off (SR, RR or FG).

```
rhi dlss driver <sr|rr|fg> <game> <on|off> [--dry-run] [--yes]
```

```bat
rhi dlss driver sr cyberpunk on --yes
```

> **write** - needs `--yes`

#### `rhi dlss preset`

Set a DLSS preset for a game. Accepts a letter (J, D), a name fragment, or a number.

```
rhi dlss preset <sr|rr|fg|nr> <game> <value> [--dry-run] [--yes]
```

```bat
rhi dlss preset sr cyberpunk J --yes
```
```bat
rhi dlss preset rr alan-wake-2 D --yes
```
```bat
rhi dlss preset sr cyberpunk 0 --dry-run
```

> **write** - needs `--yes`

#### `rhi dlss presets`

List the preset tables (SR, RR, FG, NR) the app offers.

```
rhi dlss presets [--json]
```

```bat
rhi dlss presets
```

#### `rhi dlss scale`

Set the DLSS render scale percentage (SR or RR).

```
rhi dlss scale <sr|rr> <game> <percent> [--dry-run] [--yes]
```

```bat
rhi dlss scale sr cyberpunk 67 --yes
```

> **write** - needs `--yes`

#### `rhi framegen dlssg`

Show, install, remove or switch the 20/30-series DLSS-G unlocker for a game.

```
rhi framegen dlssg <game> [show|on|off|install [--dll <name>]|remove] [--dry-run] [--yes] [--json]
```

```bat
rhi framegen dlssg cyberpunk
```
```bat
rhi framegen dlssg cyberpunk on --yes
```
```bat
rhi framegen dlssg cyberpunk install --yes
```
```bat
rhi framegen dlssg cyberpunk install --dll dxgi.dll --yes
```
```bat
rhi framegen dlssg cyberpunk remove --yes
```

### Games

#### `rhi game add`

Add a game folder to the library as a manual entry.

```
rhi game add <name> <path> [--dry-run] [--yes] [--json]
```

```bat
rhi game add "My Game" "D:\Games\MyGame" --yes
```

> **write** - needs `--yes`

#### `rhi game launch`

Launch a game the way the Launch button does (store, exe, HDR and resolution toggles).

```
rhi game launch <name> [--json]
```

```bat
rhi game launch "Cyberpunk 2077"
```

> needs RHI running (runs through the attach pipe)

#### `rhi game list`

List every game with its store and installed components.

```
rhi game list [--installed] [--hidden] [--json]
```

```bat
rhi game list
```
```bat
rhi game list --installed
```
```bat
rhi game list --json
```

#### `rhi game refresh`

Rescan the library, exactly like the Refresh button.

```
rhi game refresh [--json]
```

```bat
rhi game refresh
```

> needs RHI running (runs through the attach pipe)

#### `rhi game remove`

Remove a manual library entry (detected games return on the next scan).

```
rhi game remove <name> [--dry-run] [--yes] [--json]
```

```bat
rhi game remove "My Game" --yes
```

> **write** - needs `--yes`

#### `rhi game show`

Show one game: path, store, engine, bitness and installed components.

```
rhi game show <name> [--json]
```

```bat
rhi game show "Cyberpunk 2077"
```
```bat
rhi game show cyberpunk
```

### Hdr

#### `rhi hdr auto`

Read or change Windows Auto HDR for a game.

```
rhi hdr auto <game> show|on|off|strength [--strength 0-100] [--dry-run] [--yes]
```

```bat
rhi hdr auto cyberpunk show
```
```bat
rhi hdr auto cyberpunk on --strength 70 --yes
```
```bat
rhi hdr auto cyberpunk off --dry-run
```

> **write** - needs `--yes`

### Install

#### `rhi framegen mfg`

Show or change the Universal MFG Unlocker placement in a game.

```
rhi framegen mfg <game> [show] | rhi framegen mfg <game> set --dll <name>|--plugin|--remove [--yes]
```

```bat
rhi framegen mfg cyberpunk
```
```bat
rhi framegen mfg cyberpunk set --plugin --yes
```
```bat
rhi framegen mfg cyberpunk set --dll dxgi.dll --yes
```
```bat
rhi framegen mfg cyberpunk set --remove --yes
```

#### `rhi install asi`

Install Ultimate ASI Loader (a dinput8.dll proxy) into a game folder.

```
rhi install asi <game> [--dll <name>] [--dry-run]
```

```bat
rhi install asi "GTA San Andreas" --dry-run
```

> **write** - needs `--yes`

#### `rhi install dof-fix`

Install the UE depth-of-field fix into a game folder.

```
rhi install dof-fix <game> [--dry-run]
```

```bat
rhi install dof-fix "Stalker 2" --dry-run
```

> **write** - needs `--yes`

#### `rhi install dxvk`

Install DXVK into a game folder using the configured variant.

```
rhi install dxvk <game> [--dry-run]
```

```bat
rhi install dxvk "Cyberpunk 2077" --dry-run
```

> **write** - needs `--yes`

#### `rhi install luma`

Install the Luma Framework mod for a game from the Luma wiki (named mod, or --generic for the Unreal Engine build).

```
rhi install luma <game> [--generic] [--dry-run] [--yes]
```

```bat
rhi install luma "Alan Wake 2" --dry-run
```
```bat
rhi install luma "Borderlands 3" --generic --yes
```

> **write** - needs `--yes`

#### `rhi install optiscaler`

Install OptiScaler (upscaler bridge) into a game folder.

```
rhi install optiscaler <game> [--variant Stable|Nightly] [--gpu NVIDIA|AMD] [--dry-run]
```

> **write** - needs `--yes`

#### `rhi install reframework`

Install RE Framework (dinput8.dll) into an RE Engine game.

```
rhi install reframework <game> [--dry-run]
```

```bat
rhi install reframework "Resident Evil 4" --dry-run
```

> **write** - needs `--yes`

#### `rhi install renodx`

Install the RenoDX addon for a game from the RenoDX database.

```
rhi install renodx <game> [--dry-run]
```

```bat
rhi install renodx "Cyberpunk 2077" --dry-run
```

> **write** - needs `--yes`

#### `rhi install reshade`

Install ReShade into a game folder.

```
rhi install reshade <game> [--channel Stable|Nightly] [--normal] [--dll <name>] [--packs <id,id>] [--force] [--dry-run] [--yes]
```

```bat
rhi install reshade "Cyberpunk 2077" --yes
```
```bat
rhi install reshade cyberpunk --dry-run
```

> **write** - needs `--yes`

#### `rhi luma mode`

Turn Luma mode on or off for a game (on removes RenoDX and ReShade first).

```
rhi luma mode <game> on|off [--json]
```

```bat
rhi luma mode cyberpunk on --yes
```

> **write** - needs `--yes` · needs RHI running (runs through the attach pipe)

#### `rhi mass deploy`

Rewrite a component's ini for every game that has it installed.

```
rhi mass deploy <reshade|relimiter|displaycommander|optiscaler> [--dry-run] [--yes]
```

```bat
rhi mass deploy reshade --dry-run
```
```bat
rhi mass deploy optiscaler --yes
```

> **write** - needs `--yes`

#### `rhi mass deploy shaders`

Sync ReShade shader packs into every game that has ReShade installed, using each game's resolved selection.

```
rhi mass deploy shaders [--dry-run] [--yes]
```

```bat
rhi mass deploy shaders --dry-run
```
```bat
rhi mass deploy shaders --yes
```

> **write** - needs `--yes`

#### `rhi optiscaler dlss-enabler`

Deploy the DLSS Enabler headless DLL into a game's OptiScaler folder, or remove it.

```
rhi optiscaler dlss-enabler <game> [--remove] [--dry-run] [--yes]
```

```bat
rhi optiscaler dlss-enabler cyberpunk --yes
```
```bat
rhi optiscaler dlss-enabler cyberpunk --remove --yes
```

> **write** - needs `--yes`

#### `rhi optiscaler pdupscaler`

Replace a RE Engine game's REFramework with PD-Upscaler (only games the manifest lists).

```
rhi optiscaler pdupscaler <game> [--dry-run] [--yes]
```

```bat
rhi optiscaler pdupscaler pragmata --yes
```

> **write** - needs `--yes`

#### `rhi optiscaler streamline`

Copy the Streamline DLLs into a game's OptiScaler folder, or remove them.

```
rhi optiscaler streamline <game> [--version <v>] [--remove] [--dry-run] [--yes]
```

```bat
rhi optiscaler streamline cyberpunk --yes
```
```bat
rhi optiscaler streamline cyberpunk --remove --yes
```

> **write** - needs `--yes`

#### `rhi toggle displaycommander`

Install or remove Display Commander for a game.

```
rhi toggle displaycommander <game> on|off [--dry-run] [--yes]
```

```bat
rhi toggle displaycommander cyberpunk on --dry-run
```

> **write** - needs `--yes`

#### `rhi toggle relimiter`

Install or remove ReLimiter for a game.

```
rhi toggle relimiter <game> on|off [--dry-run] [--yes]
```

```bat
rhi toggle relimiter cyberpunk on --dry-run
```

> **write** - needs `--yes`

#### `rhi uninstall asi`

Remove Ultimate ASI Loader from a game, restoring a replaced proxy. Needs --yes.

```
rhi uninstall asi <game> [--yes] [--dry-run]
```

> **write** - needs `--yes`

#### `rhi uninstall dof-fix`

Remove the UE depth-of-field fix. Needs --yes.

```
rhi uninstall dof-fix <game> [--yes] [--dry-run]
```

> **write** - needs `--yes`

#### `rhi uninstall dxvk`

Remove DXVK and restore the backed-up DLLs. Needs --yes.

```
rhi uninstall dxvk <game> [--yes] [--dry-run]
```

> **write** - needs `--yes`

#### `rhi uninstall luma`

Remove the Luma Framework mod from a game. Needs --yes.

```
rhi uninstall luma <game> [--yes] [--dry-run]
```

> **write** - needs `--yes`

#### `rhi uninstall optiscaler`

Remove OptiScaler from a game folder. Needs --yes.

```
rhi uninstall optiscaler <game> [--yes] [--dry-run]
```

> **write** - needs `--yes`

#### `rhi uninstall reframework`

Remove RE Framework from a game. Needs --yes.

```
rhi uninstall reframework <game> [--yes] [--dry-run]
```

> **write** - needs `--yes`

#### `rhi uninstall renodx`

Remove the RenoDX addon from a game. Needs --yes.

```
rhi uninstall renodx <game> [--yes] [--dry-run]
```

> **write** - needs `--yes`

#### `rhi uninstall reshade`

Remove ReShade from a game folder. Needs --yes.

```
rhi uninstall reshade <game> [--dry-run] [--yes]
```

```bat
rhi uninstall reshade "Cyberpunk 2077" --yes
```

> **write** - needs `--yes`

### Links

#### `rhi link ultra-plus`

Resolve the Ultra+ page for a game.

```
rhi link ultra-plus <game> [--json]
```

```bat
rhi link ultra-plus "Cyberpunk 2077"
```

#### `rhi link ultrawide`

Resolve the ultrawide fix page for a game.

```
rhi link ultrawide <game> [--json]
```

```bat
rhi link ultrawide "Alan Wake 2"
```

### Nvidia

#### `rhi nvidia clear-cache`

Delete the NVIDIA DXCache and GLCache folders; games rebuild shaders on next launch.

```
rhi nvidia clear-cache [--dry-run] [--yes]
```

```bat
rhi nvidia clear-cache --dry-run
```
```bat
rhi nvidia clear-cache --yes
```

> **write** - needs `--yes`

#### `rhi nvidia game`

Read or change per-game NVIDIA driver values: Smooth Motion, Vulkan present, ReBAR.

```
rhi nvidia game <game> [show] | rhi nvidia game <game> set <field> <value> [--dry-run] [--yes]
```

```bat
rhi nvidia game cyberpunk
```
```bat
rhi nvidia game cyberpunk set smooth-motion 1 --yes
```

#### `rhi nvidia hdr`

Read or change per-game RTX HDR values.

```
rhi nvidia hdr <game> [show] | rhi nvidia hdr <game> set <enable|contrast|saturation|peak|middle-grey|debanding> <value> [--dry-run] [--yes]
```

```bat
rhi nvidia hdr cyberpunk
```
```bat
rhi nvidia hdr cyberpunk set peak 1000 --yes
```

#### `rhi nvidia profile`

Create missing driver profiles, back them up / restore them, or reset them.

```
rhi nvidia profile <create-missing|export [--out <path>]|import [--in <path>]|reset> [--dry-run] [--yes]
```

```bat
rhi nvidia profile export
```
```bat
rhi nvidia profile import --yes
```
```bat
rhi nvidia profile reset --yes
```

#### `rhi nvidia set`

Change one global NVIDIA driver setting. Values are raw driver enums (see nvidia show).

```
rhi nvidia set <setting> <value> [--dry-run] [--yes] [--json]
```

```bat
rhi nvidia set fps-cap 120 --yes
```
```bat
rhi nvidia set gsync-indicator off --yes
```
```bat
rhi nvidia set shader-cache 10737418240 --dry-run
```

> **write** - needs `--yes`

#### `rhi nvidia show`

Print every global NVIDIA driver setting the app controls.

```
rhi nvidia show [--json]
```

```bat
rhi nvidia show
```

### Overlay

#### `rhi overlay state`

Report whether RHI's overlay is open.

```
rhi overlay state [--json]
```

> needs RHI running (runs through the attach pipe)

#### `rhi overlay toggle`

Open or close RHI's in-game overlay, exactly like its hotkey.

```
rhi overlay toggle [--json]
```

```bat
rhi overlay toggle
```

> needs RHI running (runs through the attach pipe)

### Rtx

#### `rhi rtx encore`

Install or remove RTX Encore, and read or change one game's settings (rtx-encore.jsonc) and its Neural Rendering DLL.

```
rhi rtx encore <game> [show] | rhi rtx encore <game> set <section.key> <value> [--dry-run] [--yes] | rhi rtx encore <game> set --nr-dll on|off [--dry-run] [--yes] | rhi rtx encore <game> install [--dll <name>] [--dry-run] [--yes] | rhi rtx encore <game> remove [--dry-run] [--yes]
```

```bat
rhi rtx encore "Cyberpunk 2077"
```
```bat
rhi rtx encore "Cyberpunk 2077" install --yes
```
```bat
rhi rtx encore "Cyberpunk 2077" install --dll winmm.dll --dry-run
```
```bat
rhi rtx encore "Cyberpunk 2077" remove --yes
```
```bat
rhi rtx encore "Cyberpunk 2077" set frameGeneration.multiplier 3 --yes
```
```bat
rhi rtx encore "Cyberpunk 2077" set neuralRendering/core.nrEnabled on --yes
```
```bat
rhi rtx encore "Cyberpunk 2077" set --nr-dll on --yes
```

### Screenshots

#### `rhi shot delete`

Delete one screenshot. Needs --yes.

```
rhi shot delete <game> <name> [--yes] [--dry-run]
```

```bat
rhi shot delete "Alan Wake 2" "AlanWake2 2026-01-01 12-00-00.png" --yes
```

> **write** - needs `--yes`

#### `rhi shot folder`

Show the screenshot folder RHI resolves for a game.

```
rhi shot folder <game> [--json]
```

#### `rhi shot list`

List screenshots for one game, or for every game with a screenshot folder.

```
rhi shot list [<game>] [--json]
```

```bat
rhi shot list
```
```bat
rhi shot list "Alan Wake 2"
```

### Settings

#### `rhi setting get`

Show one setting's value.

```
rhi setting get <Key> [--json]
```

```bat
rhi setting get OverlayHotkey
```

#### `rhi setting list`

Show every persisted setting and its value.

```
rhi setting list [--json]
```

```bat
rhi setting list
```
```bat
rhi setting list --json
```

#### `rhi setting set`

Change a persisted setting. Takes effect immediately; a running RHI may overwrite it when it saves.

```
rhi setting set <Key> <Value> [--dry-run] [--json]
```

```bat
rhi setting set OverlayHotkey 36,0,0,0
```
```bat
rhi setting set OverlayHotkey 35,0,0,0 --dry-run
```

### System

#### `rhi system`

Start with Windows, the nxm:// handler, and Nexus disconnect.

```
rhi system startup show|on|off | rhi system nxm status|register|unregister | rhi system nexus disconnect
```

```bat
rhi system startup show
```
```bat
rhi system nxm status
```
```bat
rhi system startup on --yes
```

### Ui

#### `rhi ui layout`

Set the main layout (Classic, Tabs, Spotlight). Applies at next launch.

```
rhi ui layout <Classic|Tabs|Spotlight> [--json]
```

```bat
rhi ui layout Tabs
```

#### `rhi ui list-scale`

Set the game list scale percentage (75-200). Applies at next launch.

```
rhi ui list-scale <75-200> [--json]
```

#### `rhi ui scale`

Set the interface scale percentage (75-200). Applies at next launch.

```
rhi ui scale <75-200> [--json]
```

#### `rhi ui settings`

Set the settings section RHI opens on next launch.

```
rhi ui settings <section> [--json]
```

```bat
rhi ui settings ReShade
```

#### `rhi ui show`

Show the stored UI state (layout, scale, selected tab, last settings section).

```
rhi ui show [--json]
```

```bat
rhi ui show
```

#### `rhi ui spotlight-cover`

Set the Spotlight cover scale (0.5-2.0). Applies at next launch.

```
rhi ui spotlight-cover <0.5-2.0> [--json]
```

#### `rhi ui tab`

Set the tab the detail panel opens on next launch.

```
rhi ui tab <name> [--json]
```

```bat
rhi ui tab Overview
```

#### `rhi ui view`

Set the game list view (Detail, Compact). Applies at next launch.

```
rhi ui view <Detail|Compact> [--json]
```

### Update

#### `rhi update`

Run one component's batch update over every game (reshade, renodx, dxvk, optiscaler, ...).

```
rhi update <component> [--dry-run] [--yes] [--json]
```

```bat
rhi update reshade --dry-run
```
```bat
rhi update dxvk --yes
```

> **write** - needs `--yes` · needs RHI running (runs through the attach pipe)

#### `rhi update all`

Run Update All: the ReShade pass, then the RenoDX pass, over every eligible game.

```
rhi update all [--dry-run] [--yes] [--json]
```

```bat
rhi update all --dry-run
```
```bat
rhi update all --yes
```

> **write** - needs `--yes` · needs RHI running (runs through the attach pipe)

#### `rhi update app`

Check GitHub for a newer RHI release (does not download or install it).

```
rhi update app [--json]
```

```bat
rhi update app
```

#### `rhi update check`

Check installed components for updates. Heavier ones report the last known state with --offline.

```
rhi update check [<game>] [--offline] [--json]
```

```bat
rhi update check
```
```bat
rhi update check "Cyberpunk 2077" --offline
```
<!-- END COMMAND REFERENCE -->

## Workflows

### A game, from scratch

```bat
rhi game add "My Game" "D:\Games\MyGame" --yes   :: only if RHI does not see it yet
rhi game show "My Game"
rhi install reshade "My Game" --yes
rhi install renodx "My Game" --yes                :: when a RenoDX mod exists for it
rhi reshade live install "My Game" --yes          :: optional: live control addon
```

### Switch a game to Luma

```bat
rhi install luma "My Game" --yes
rhi luma mode "My Game" on --yes
```

`luma mode on` removes RenoDX and ReShade first, exactly like the detail-panel toggle - so run it
with RHI open. `rhi luma mode "My Game" off --yes` reverses it.

### Tune DLSS

```bat
rhi dlss presets                                   :: the tables the app offers
rhi dlss preset sr "My Game" J --yes               :: letter, name fragment, or number
rhi dlss scale sr "My Game" 67 --yes
rhi dlss driver rr "My Game" on --yes
```

### NVIDIA driver globals and RTX HDR

```bat
rhi nvidia show                                    :: current global values
rhi nvidia set fps-cap 120 --yes
rhi nvidia set shader-cache 10737418240 --yes
rhi nvidia hdr "My Game" show
rhi nvidia hdr "My Game" set peak 1000 --yes
rhi nvidia profile create-missing --yes
```

### Back up before you experiment

```bat
rhi nvidia profile export --out D:\backup\profiles.json
rhi logs archive --out D:\backup\logs.zip
rhi setting list --json > D:\backup\settings.json
copy "%LOCALAPPDATA%\RHI\game_library.json" D:\backup\game_library.json
```

`nvidia profile import --in <file> --yes` restores them.

### Keep a library current

```bat
rhi update check
rhi update all --yes                               :: needs RHI running
rhi update dxvk --yes                              :: one component
rhi mass deploy reshade --yes                      :: rewrite every game's ReShade.ini
rhi mass deploy optiscaler --yes
```

### One ini value, one game or all of them

```bat
rhi ini list "My Game"
rhi ini get "My Game" DXGI.MaxFrameLatency
rhi ini set "My Game" DXGI.MaxFrameLatency 1 --yes  (writes a .bak, atomic replace)
rhi ini template list                               :: the shipped templates
```

### Watch a running game

```bat
rhi reshade live ping
rhi reshade live effects off
rhi reshade live techniques
rhi reshade live technique SMAA on
rhi reshade live preset "C:\path\ReShadePreset.ini"
```

Live commands need the game running **and** the addon deployed (`rhi reshade live install <game>
--yes`). They execute on ReShade's render thread, so a change lands on the next frame. Reading and
toggling live state does not require `--yes`; deploying the addon does.

### Scripting

```powershell
$lib = rhi game list --json | ConvertFrom-Json
foreach ($g in $lib.data.games) {
  if (-not $g.reshade) { rhi install reshade $g.name --yes }
}
```

Game names with spaces must be quoted in `cmd`/`bat`; inside PowerShell, quote the whole argument
(`rhi game show "Alan Wake 2"`). `--json` never mixes prose into the output, so it is safe to pipe.

## Where RHI keeps its state

| path | contents |
|---|---|
| `%LOCALAPPDATA%\RHI\settings.json` | every persisted setting (`setting get/set`) |
| `%LOCALAPPDATA%\RHI\game_library.json` | scanned + manual games, versions, caches |
| `%LOCALAPPDATA%\RHI\logs\` | session logs (`logs archive`) |
| `%LOCALAPPDATA%\RHI\inis\` | staged ini templates (OptiScaler, ReLimiter, ...) |
| `%LOCALAPPDATA%\RHI\dldsr-captures.json` | DLDSR captures (`dldsr capture`) |
| `%LOCALAPPDATA%\RHI\nvidia_profiles_backup.json` | default `nvidia profile export` target |
| `<game>\ReShade.ini` | per-game ReShade config (`ini list/get/set`) |

## Troubleshooting

| symptom | likely cause | fix |
|---|---|---|
| `rhi: RHI.exe not found` | dispatcher cannot locate the app | `set RHI_CLI_EXE=C:\path\to\RHI.exe` |
| every command prints the help screen | a wrapper swallowed the arguments | call `rhi.bat` directly and pass `%*` through |
| `no game library yet` | RHI has never scanned | start RHI once, or `game add` |
| `RHI is not running` for `overlay toggle/state`, `game launch`, `game refresh`, `update all`, `update <component>`, `luma mode` | those need the live window | start RHI and retry |
| `no live channel` | game closed, addon missing, or ReShade absent | `reshade live install <game> --yes`, then start the game |
| `the driver refused ...` | NVAPI needs elevation | run the shell as administrator |
| `could not stage ReShade` | no network, or reshade.me unreachable | retry; the download is cached after one success |
| `refusing ... needs --yes` | write command without confirmation | add `--yes`, or preview with `--dry-run` |
| `doctor` reports settings FAIL | `settings.json` unreadable | back it up and let RHI recreate it |
| a `setting set` did not stick | RHI was open and saved over it | close RHI first (the command tells you) |
| a Vulkan game refuses to install ReShade | proxy cannot be guessed | add `--dll vulkan-1.dll` (or `opengl32.dll`) |

## Coverage and limits

- 78 commands over 108 persisted settings, every key in any game ini, 12/12 components, and the
  live ReShade channel.
- `docs/parity.md` counts what is reachable; `docs/parity-gaps.md` lists what is not, with reasons;
  `docs/handler-map.md` maps every GUI handler to a command or marks it UI-only.
- Deliberately outside the CLI: Luma/PD-Upscaler/DLSS-Enabler inline flows, the Discord game-data
  export blob, installing an RHI app update, and peak-nits auto-detection.
- Two behaviours worth knowing: some commands need RHI running (they mutate the live view model),
  and NVAPI writes may need an elevated shell.





