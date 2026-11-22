# RHI terminal interface

`rhi <command> [args] [--json] [--dry-run] [--yes]`

The batch layer (`rhi.bat` + `commands\*.bat`) forwards everything to `RHI.exe --cli …`.
`Program.Main` handles `--cli` before WinUI starts, so a command never opens a window.

## Output contract

- exit `0` done, `1` failed, `2` bad usage
- `--json` prints one envelope: `{ ok, command, exitCode, message, data }`
- `--dry-run` prints the plan and changes nothing
- `--yes` is required by every command that deletes or overwrites (`Destructive` in the registry)

## Commands (implemented)

| command | what it does |
|---|---|
| `help [command]` | command list, or one command's usage and examples |
| `commands [--json]` | machine-readable list straight from the registry |
| `version` | app version, exe path, OS/arch |
| `doctor` | exe, version, elevation, settings, data folder, 7-Zip |
| `status` | games, per-component counts, pending updates, overlay hotkeys, GUI running |
| `setting list` | every persisted key in `%LocalAppData%\RHI\settings.json` |
| `setting get <Key>` | one value |
| `setting set <Key> <Value>` | write through `SettingsFile.Update` (same lock as the GUI) |
| `game list [--installed] [--hidden]` | name, store, ReShade/RenoDX/DXVK versions, path |
| `game show <name>` | path, store, engine, bitness, versions, update flags, hidden/favourite |
| `game launch <name>` |
| `game add <name> <path>` |
| `game remove <name>` |
| `game refresh` | rescans the library (the Refresh button); needs RHI running | removes a manual entry; detected games are refused | adds a manual library entry; `--dry-run`, `--yes` | store/exe launch with the HDR and resolution toggles; needs RHI running |
| `install reshade <game>` | `--channel`, `--normal`, `--dll`, `--packs`, `--force`, `--dry-run`; Vulkan/OpenGL games need `--dll` |
| `uninstall reshade <game>` | `--yes`, `--dry-run` |
| `install reframework <game>` | RE Engine prerequisite |
| `uninstall reframework <game>` | `--yes`, `--dry-run` |
| `install dof-fix <game>` | UE depth-of-field fix |
| `uninstall dof-fix <game>` | `--yes`, `--dry-run` |
| `install renodx <game>` | named RenoDX mod from the database, matched by name |
| `uninstall renodx <game>` | `--yes`, `--dry-run` |
| `install luma <game>` | named Luma mod from the wiki |
| `uninstall luma <game>` |
| `luma mode <game> on/off` | flips Luma mode on the live card (on removes RenoDX + ReShade); needs RHI running, `--yes` | `--yes`, `--dry-run`; records come from `LumaService.GetAllRecords` |
| `install dxvk <game>` | uses the configured variant, builds a real card (exe/API/bitness) |
| `uninstall dxvk <game>` | restores backups, `--yes`, `--dry-run` |
| `install optiscaler <game>` | `--variant`, `--gpu`, DLSS inputs from settings |
| `uninstall optiscaler <game>` |
| `optiscaler streamline <game> [--remove]` | copies/removes the Streamline DLLs under `OptiScaler\Streamline` (`--yes`) | `--yes`, `--dry-run` |
| `install asi <game> [--dll <name>]` | Ultimate ASI Loader proxy, default `dinput8.dll` |
| `uninstall asi <game>` | `--yes`, `--dry-run`; restores a proxy it replaced |
| `toggle displaycommander <game> on/off` | Display Commander; `--dry-run`, `--yes` to remove |
| `toggle relimiter <game> on/off` | ReLimiter; mutually exclusive with Display Commander |
| `dlss presets` | the SR/RR/FG/NR preset tables the app offers |
| `dlss preset <sr/rr/fg/nr> <game> <value>` | value as a letter (J, D), a name fragment, or a number |
| `dlss scale <sr/rr> <game> <percent>` | render scale |
| `dlss driver <sr/rr/fg> <game> <on/off>` | driver-level preset override |
| `hdr auto <game> show/on/off/strength` | Windows Auto HDR, `--strength 0-100`, `--dry-run` |
| `nvidia show` | every global NVIDIA driver setting the app controls (shader cache, G-Sync, FPS cap, DMFG, ReBAR, VSync, power, indicator) |
| `nvidia set <setting> <value>` |
| `nvidia clear-cache` |
| `nvidia hdr <game> [show\|set <field> <value>]` |
| `nvidia profile <create-missing\|export\|import\|reset>` |
| `dldsr show/captures/capture/apply/delete/smoothness` | DLDSR states; apply blanks the screen ~15s; writes need `--yes` | driver profile maintenance; backup at `%LocalAppData%\RHI\nvidia_profiles_backup.json` (`--yes` to write) | per-game RTX HDR: enable, contrast, saturation, peak, middle-grey, debanding (`--yes`) | deletes the NVIDIA DXCache/GLCache folders; `--dry-run`, `--yes` | raw driver value from `nvidia show`; `--dry-run`, `--yes`; some need elevation |
| `link ultrawide <game>` | resolves the ultrawide fix page (the button opens it in a browser) |
| `link ultra-plus <game>` | resolves the Ultra+ page |
| `shot list [<game>]` | newest first, uses the same folder resolution as the overlay |
| `shot folder <game>` | the folder RHI resolves (`reshade.ini` SavePath, else the game folder) |
| `update check [<game>] [--offline]` | live aux checks; `--offline` reports cached flags only |
| `update app` | checks GitHub for a newer RHI release (does not install) |
| `update all` | ReShade pass then RenoDX pass over every scanned game; needs RHI running; `--dry-run`, `--yes` |
| `mass deploy <reshade/relimiter/displaycommander/optiscaler>` | rewrites that component's ini for every game that has it installed; `--dry-run`, `--yes` |
| `update <component>` | one component's batch update: reshade, renodx, relimiter, displaycommander, dxvk, optiscaler, reframework, luma, dof-fix; needs RHI running |
| `system startup show/on/off` | the HKCU Run key (`--yes` to change) |
| `system nxm status/register/unregister` | nxm:// handler (`--yes` to change) |
| `system nexus disconnect` | clears the Nexus key, username and premium flag (`--yes`) |
| `logs archive [--out <path>]` | zips the session logs to a file (the button copies them to the clipboard) |
| `reshade shaders` |
| `reshade addons [list\|download <id>]` | the addon catalogue, and fetching one addon (`--yes`) | lists the shader pack catalogue and whether each is cached |
| `auth status` | whether GitHub / Nexus are connected; never prints a secret |
| `auth github` | stores a GitHub token from `GH_TOKEN` after GitHub confirms it; `--clear --yes`; refuses a token passed as an argument |
| `auth nexus` | SSO sign-in from the terminal: prints the authorise URL (`--open` launches it), `--timeout`, saves nothing unless the key validates. Refuses `--json` - it needs you to open a URL |
| `ui show` | stored layout, view, scales, selected tab, settings section |
| `ui layout <Classic/Tabs/Spotlight>` | saved, applies at next launch, and the output says so |
| `ui view <Detail/Compact>` | saved, applies at next launch |
| `ui scale <75-200>` / `ui list-scale <75-200>` | interface / game-list scale |
| `ui tab <name>` / `ui settings <section>` | what opens on the next launch |
| `ui spotlight-cover <0.5-2.0>` | Spotlight cover scale |
| `ini list <game> [--name <file>] [--section <s>]` | every key in the game's ReShade/OptiScaler/dxvk/DisplayCommander ini |
| `ini get <game> <Section.Key>` | one value |
| `ini set <game> <Section.Key> <value>` | line-preserving write, keeps a `.bak`, `--dry-run` |
| `ini template list` / `ini template get <file> <Section.Key>` | the shipped templates next to the exe |
| `reshade live <ping/effects/techniques/technique/preset/install>` | talks to rhi-live.addon inside a running ReShade; `effects off` changes live state |
| `overlay toggle` | opens or closes RHI's own overlay, exactly like its hotkey |
| `overlay state` | open or closed |

## Not wired yet, and why

| area | blocker |
|---|---|
| `mass deploy reshade` | the other mass-deploy targets (ReLimiter, DisplayCommander, OptiScaler inis) are not wired |
| `overlay panel <name>` | OverlayWindow has no panel-selection API: the left menu's buttons own their handlers |
| live UI apply (`ui …` on an open window) | the `ui` commands write settings and take effect at next launch; pushing them into a running window needs the window's own setters |
| live ReShade control | deferred until a test game is supplied (addon + pipe) |

Card-based installers run through `Cli\CliCards.cs`, which builds a real
`GameCardViewModel` from library data plus exe/API/bitness detection — so DXVK, OptiScaler and
anything else taking a card executes the normal GUI path, not a copy of it.

## Attach path

Commands that need the live window (`overlay toggle`, `overlay state`) are marked `RequiresGui`.
When RHI is running, the CLI sends `["<args>..."]` on the `RenoDXCommander_CliPipe` named pipe;
RHI's listener runs the same registry entry on its UI thread and answers with
`{ ok, exitCode, message, data }`. Probe timeout is 750 ms, so a command with no app running fails
immediately instead of hanging.

## How parity is measured

`tools\New-GuiInventory.ps1` regenerates `docs\gui-inventory.{json,md}` from the app source:
271 named XAML controls, 175 handlers (0 unresolved), 307 code-built controls, 108 persisted
settings, 4054 ini keys, 32 enums, 110 string sets.

`tools\New-ParityReport.ps1` cross-references that inventory with the command list the built
exe reports and writes `docs\parity.md`. Current numbers: 78 commands, 108/108 settings
reachable, every key in a game's ini reachable (`ini get/set`), 12/12 components, 175 handlers
still marked manual review.

`tools\New-HandlerMap.ps1` writes `docs\handler-map.md`: all 175 XAML handlers with file:line,
the services each one touches, and a keyword-matched command suggestion (42 of 175 today). A
blank suggestion is either dialog-internal (ui-only) or a real gap — that list is the work queue.

Handler-level gaps and the remaining work queue: `docs\parity-gaps.md`.















