# patches

`upstream-changes.diff` is the complete set of changes this release makes to the upstream RHI
source. It touches five existing files only:

| file | why |
|---|---|
| `RenoDXCommander/Program.cs` | routes `--cli` before WinUI starts |
| `RenoDXCommander/App.xaml.cs` | `Services` becomes assignable so the CLI host can set the provider |
| `RenoDXCommander/MainWindow.Overlay.cs` | starts the CLI pipe listener, exposes the overlay toggle/state |
| `RenoDXCommander/MainWindow.Events.Install.cs` | `LaunchGameAsync` returns whether anything launched |
| `RenoDXCommander/RenoDXCommander.csproj` | copies `rhi-live.addon32/64` next to the exe |

Everything else in this tree is new files:

- `RenoDXCommander/Cli/` — the command layer
- `RenoDXCommander/Services/ServiceRegistration.cs` — the DI registration moved out of `App`
- `RenoDXCommander.Tests/*Cli*.cs`, `ServiceRegistrationTests.cs` — 21 test methods (25 xUnit cases) for the parser, the ini
  round-trip, the registry contract, the attach entry point and the service graph
- `LiveAddon/` — the ReShade addon source, build scripts, headers and the two built `.addon` files
- `cli/` — `rhi.bat` and the per-verb batch files

Apply against RHI 2.9.2 (`5f409de9`):

```bat
git apply --check patches\upstream-changes.diff
git apply patches\upstream-changes.diff
```

If your checkout has moved, copy the files from this tree instead — the paths match the repo.


