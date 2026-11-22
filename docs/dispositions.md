# Handler dispositions

Every handler in `gui-inventory.json` (175) grouped by the file that implements it, with what it
does and how the terminal reaches it. Reviewed by hand against the command list; regenerate the
inventory any time, but this table is the judgement call and has to be updated with it.

Statuses:

- **covered** — a command performs the same action
- **covered-generic** — the value it writes is reachable through `setting set` or `ini set`
- **ui-only** — navigation, dialog internals, visual state: no headless meaning
- **gap** — an action with no command yet

| file | handlers | what it is | status |
|---|---|---|---|
| MainWindow.Events.Settings.cs | 80 | settings page navigation, toggles and list buttons | covered-generic for persisted values; `ui settings <section>` for the page; ui-only for navigation |
| MainWindow.Events.cs | 25 | refresh, external links, Discord/donate | `link ultrawide` / `link ultra-plus` for those pages; other links and donate are ui-only; refresh has no headless equivalent yet |
| MainWindow.Events.Install.cs | 22 | Update All, launch, hide, open folders, engine badges | `game launch` covered (attach path); hidden is `setting set HiddenGames`; **gap**: `update all`; badges are visual |
| InstallEventHandler.cs | 14 | per-component install/uninstall buttons | covered by `install`/`uninstall <component>`; DisplayCommander and ReLimiter by `toggle displaycommander` / `toggle relimiter` |
| SettingsHandler.Accounts.cs | 6 | GitHub / Nexus connect, disconnect, NXM register | **gap**: interactive auth (OAuth / token entry) |
| MainWindow.Events.SettingsInputs.cs | 5 | text boxes and sliders writing settings | covered-generic (`setting set`, e.g. `ScreenshotPath`, `PeakNitsEnabled`) |
| MainWindow.Events.Components.cs | 4 | support links, component cog buttons | ui-only (links); cog dialogs are visual entry points to the same ini values |
| MassDeployHandler.cs | 4 | deploy one ini to every game | **gap**: mass deploy is a bulk action with no command |
| SettingsHandler.Hotkeys.cs | 4 | hotkey capture boxes | covered-generic (`setting set` for the key strings) |
| SettingsHandler.DataTools.cs | 3 | cache clear / data tools | **gap**: no command yet |
| SettingsHandler.Channels.cs | 2 | ReShade channel switches | covered by `install reshade --channel` / `setting set ReShadeChannel` |
| MainWindow.UISync.cs | 2 | window sync callbacks | ui-only |
| MainWindow.Events.Components.{DisplayCommander,FrameLimiters,Rdx}.cs | 3 | component-specific install buttons | covered: `toggle displaycommander` / `toggle relimiter`, `install renodx`, `ini set` for frame limiters |
| MainWindow.xaml.cs | 1 | window event | ui-only |

## Open gaps

1. Live ReShade control while a game runs - waiting for a test game. The attach pipe and the command
   registry are in place, so this becomes an addon plus a transport once one is available.