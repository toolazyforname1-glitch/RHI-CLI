# RHI CLI 2.0.0

> **Terminal control for [RHI](https://github.com/RankFTW/RHI).** Every install, setting and
> in-game control, from a prompt — including live changes while a game is running.
>
> `rhi install reshade "Cyberpunk 2077" --yes` · `rhi dlss preset sr "…" J --yes` ·
> `rhi nvidia set fps-cap 120 --yes` · `rhi reshade live effects off`
>
> **[Usage guide](USAGE.md)** · **[Coverage](docs/parity.md)** · **[What is missing](docs/parity-gaps.md)** · **[Handler map](docs/handler-map.md)**

RHI finds your games across eight storefronts and manages ReShade, RenoDX, Luma, OptiScaler, DXVK,
DLSS/Streamline files, NVIDIA driver settings and DLDSR for all of them. This drives the same
machinery from a terminal, without opening the window.

## Why 2.0.0 is a different tool

1.x shipped `rhictl`: a **separate** process that re-implemented part of the app, and whose README
had to advertise `git diff` on `RenoDXCommander/` as empty. That boundary is exactly why 1.x could
not reach things like the live ReShade state.

2.0.0 puts the commands **inside `RHI.exe`**:

| | 1.x (`rhictl`) | 2.0.0 |
|---|---|---|
| where the logic lives | separate process, app unmodified | inside the app, before WinUI starts |
| how it reaches app state | re-implemented service calls | the app's own service graph and file locks |
| live in-game control | not possible | `rhi-live.addon32/64` + a local pipe, executed on ReShade's render thread |
| commands that need the window | not supported | forwarded over `RenoDXCommander_CliPipe` and run on the UI thread |
| command count | ~40 | **78**, each with `--json` and `--dry-run`; destructive ones need `--yes` |

## Highlights

- **Installs, removals and updates** for ReShade, RenoDX, Luma, OptiScaler, DXVK, RE Framework,
  DOF fix, ASI loader, DisplayCommander and ReLimiter — including ReShade channel, 32/64-bit
  detection, proxy name and shader pack selection.
- **Live ReShade control** while a game runs: list techniques, toggle one, flip effects, load a
  preset. The addon does the work on the render thread, so it is safe mid-frame.
- **NVIDIA driver surface**: 13 global settings, per-game RTX HDR (peak, contrast, saturation,
  middle grey, debanding), profile create/backup/restore/reset, shader-cache purge.
- **DLDSR**: show current state, capture, apply, delete, set smoothness.
- **Config**: any key in any game's ini (line-preserving, atomic, `.bak`), the shipped ini
  templates, DLSS presets/scales/driver overrides, Auto HDR.
- **Batch**: `update all`, per-component updates, `mass deploy` for four components.
- **Diagnostics**: `doctor`, `status`, `logs archive`, `update app`.
- **Accounts**: `auth status`, `auth github` (token from `GH_TOKEN`, validated first), `auth nexus`.

## Install

1. Apply `patches/upstream-changes.diff` to RHI 2.9.2, or drop this tree over your checkout — the
   layout mirrors the repo.
2. Build the app; `rhi-live.addon32/64` are copied next to `RHI.exe` automatically.
3. `setx RHI_CLI_EXE "C:\path\to\RHI.exe"`, then call `cli\rhi.bat`.

Full instructions, build steps and every command: **[USAGE.md](USAGE.md)**.

## How a command runs

| situation | path |
|---|---|
| app closed, no window needed | `RHI.exe --cli …`, handled before WinUI starts |
| app open, no window needed | headless process; the GUI and the CLI share the same settings file and locks |
| needs the live window | named pipe to the running app, executed on its UI thread |
| live ReShade | pipe to the addon inside the game, executed on the render thread |

## Safety

`--dry-run` never writes, including the live channel and the credential commands. `--yes` is
required for destructive commands (installs, removals, overrides, driver changes, storing a
token), so a mistyped script cannot uninstall anything; plain settings writes apply immediately. `ini set` keeps a `.bak` and replaces atomically. Credentials never travel on
the command line. Commands refuse rather than guess — a Vulkan game will not get a guessed proxy, a
detected game will not be removed, and a missing live channel is reported instead of faked.

## What it does not do

Deliberately outside the CLI, with reasons in [docs/parity-gaps.md](docs/parity-gaps.md): the
Luma/PD-Upscaler/DLSS-Enabler inline install flows, the Discord game-data export, installing an RHI
app update, and peak-nits auto-detection. Some commands also require RHI running, because they
mutate the live view model.

## Credits

RHI by [RankFTW](https://github.com/RankFTW/RHI). ReShade addon headers are the 6.8.0 SDK from
[crosire/reshade](https://github.com/crosire/reshade). ReShade itself is by crosire.

If this saved you a week of waiting, or a few hundred gigabytes or dealing with other issues and you'd like to support me:

Bitcoin   bc1qv7f4v8djjw46wyhw0c8nfdufdy9mgnwcndvyyc
Solana    7uCSuGumHHAKLw8KAwEthhwJgGvFprjZs8pQ4c7vvNud
EVM       0x876Dbb0D860e813Ea57885B717fc860039Db7337
Tron      TCU9ruV2qAbdfPkby28WXNGwv7WDDexXsV
Ton       UQBmx9jzmKV6hhUF6mZ-2DIHKSBc8zwYtqAZZRL6UPAApFmx