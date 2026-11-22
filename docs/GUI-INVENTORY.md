# GUI inventory

Generated 2026-11-11T19:09:53 from the RHI source tree @ `5f409de9`.

| area | count |
|---|---|
| x:Name controls | 271 |
| XAML nodes with name or events | 308 |
| XAML event references | 200 |
| unique handlers | 175 |
| unresolved handlers | 0 |
| controls built in C# | 307 |
| settings entries | 108 |
| persisted settings keys | 108 |
| ini keys (18 files) | 4054 |

## x:Name controls

| name | type | file | parents | labels | events |
|---|---|---|---|---|---|
| AboutPanel | Grid | MainWindow.xaml | Grid < Grid < Window |  |  |
| AboutVersionText | TextBlock | MainWindow.xaml | StackPanel < StackPanel < StackPanel < Border |  |  |
| AddGameBtn | Button | MainWindow.xaml | Grid < Border < Grid < Border | Content=+; ToolTipService.ToolTip=Manually add a game | Click->AddGameButton_Click |
| AddonWatchFolderBox | TextBox | MainWindow.xaml | Grid < Grid < StackPanel < Grid | PlaceholderText=Downloads (default); ToolTipService.ToolTip=RHI watches this folder for RenoDX addon files (.addon64/.addon32) and archives. Defaults to your Downloads folder. | KeyDown->AddonWatchFolderBox_KeyDown; LostFocus->AddonWatchFolderBox_LostFocus |
| AdminModeCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Creates a scheduled task that launches RHI with elevated privileges, so there is no prompt each time. Takes effect after RHI restarts. | SelectionChanged->AdminModeCombo_SelectionChanged |
| ApplyHotkeyBtn | Button | MainWindow.xaml | StackPanel < Grid < StackPanel < Border | Content=Write these to all games; ToolTipService.ToolTip=Writes the overlay key, screenshot key, screenshot folder and effect list style into every game's reshade.ini. Skips hidden games and games set to keep their own reshade.ini. | Click->ApplyScreenshotPath_Click |
| ApplyOsHotkeyBtn | Button | MainWindow.xaml | StackPanel < Grid < StackPanel < Border | Content=Write key to all OptiScaler games; ToolTipService.ToolTip=Writes the overlay key into OptiScaler.ini in every game that has OptiScaler installed. Skips hidden games. | Click->ApplyOsHotkey_Click |
| ApplyScreenshotPathBtn | Button | MainWindow.xaml | StackPanel < Grid < StackPanel < Border | Content=Write peak brightness to all games; ToolTipService.ToolTip=Writes the peak nits value into every game's reshade.ini. Skips hidden games and games set to keep their own reshade.ini. | Click->ApplyPeakNitsToAll_Click |
| ApplyUlHotkeyBtn | Button | MainWindow.xaml | StackPanel < Grid < StackPanel < Border | Content=Write to all ReLimiter games; ToolTipService.ToolTip=Writes the overlay key, Shared presets, DLSS hooks and Target FPS into every game's relimiter.ini. Skips hidden games. | Click->ApplyUlOsdHotkey_Click |
| AutoUpdateComponentsCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=When On, RHI silently installs component updates in the background after each update check. Games that are running when an update is detected are updated once they close. Does not apply to app updates. | SelectionChanged->AutoUpdateComponentsCombo_SelectionChanged |
| AutoUpdateDlssCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Swaps games to the newest DLSS when a new version is released. Only affects games on the previous latest version; older versions you picked yourself are left alone. | SelectionChanged->AutoUpdateDlssCombo_SelectionChanged |
| AutoUpdateStreamlineCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Swaps games to the newest Streamline when a new version is released. Only affects games on the previous latest version; older versions you picked yourself are left alone. | SelectionChanged->AutoUpdateStreamlineCombo_SelectionChanged |
| BackgroundUpdateChecksCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel |  | SelectionChanged->BackgroundUpdateChecksCombo_SelectionChanged |
| CacheAllShadersCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=When On, all shader packs are downloaded and cached at startup. When Off, packs are downloaded on demand when needed. | SelectionChanged->CacheAllShadersCombo_SelectionChanged |
| CheckForUpdatesBtn | Button | MainWindow.xaml | StackPanel < Grid < StackPanel < Border | Content=Check for updates; ToolTipService.ToolTip=Check all components and the app for available updates | Click->CheckForUpdatesButton_Click |
| ChooseShadersItem | MenuFlyoutItem | MainWindow.xaml | MenuFlyout < Button.Flyout < Button < StackPanel | Text=Global Shaders | Click->ChooseShadersButton_Click |
| CloseToTrayCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Closing the window keeps RHI running in the system tray. | SelectionChanged->CloseToTrayCombo_SelectionChanged |
| ColorApplyBtn | Button | MainWindow.xaml | StackPanel < Grid < StackPanel < Border | Content=Apply; ToolTipService.ToolTip=Apply the colour depth and dynamic range to the selected monitor. | Click->ColorApplyBtn_Click |
| ColorDepthCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Bits per colour channel sent to the monitor. 10 bpc or higher suits HDR. Only depths your monitor and cable support will work. |  |
| ColorDisplayCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Select which display to configure. | SelectionChanged->ColorDisplayCombo_SelectionChanged |
| ColorRangeCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Full suits PC monitors. Limited suits TVs that expect video levels. The wrong one makes blacks look grey or crushed. |  |
| CopyLogsArchiveBtn | Button | MainWindow.xaml | WrapPanel < StackPanel < Grid < StackPanel | Content=Copy logs; ToolTipService.ToolTip=Creates a zip archive of all logs and copies it to clipboard — paste directly into Discord | Click->CopyLogsArchive_Click |
| CustomFilterChipPanel | WrapPanel | MainWindow.xaml | StackPanel < Border < Grid < Border |  |  |
| CustomShadersCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=RHI Managed: deploy built-in shader packs to all games.
Custom: use your own shader directory.
Off: RHI never touches shaders on any game. | SelectionChanged->CustomShadersCombo_SelectionChanged |
| Detail32BitBadge | Border | MainWindow.xaml | StackPanel < Grid < Border < Grid |  |  |
| Detail64BitBadge | Border | MainWindow.xaml | StackPanel < Grid < Border < Grid |  |  |
| DetailAppDataBtn | Button | MainWindow.xaml | Grid < Border < Grid < StackPanel | ToolTipService.ToolTip=Open game config folder in Explorer | Click->OpenAppData_Click |
| DetailAuthorBadgePanel | StackPanel | MainWindow.xaml | Grid < StackPanel < ScrollViewer < Grid |  |  |
| DetailComponentsArrow | TextBlock | MainWindow.xaml | StackPanel < Grid < StackPanel < Border | Text=▼ |  |
| DetailComponentsBody | StackPanel | MainWindow.xaml | StackPanel < Border < StackPanel < ScrollViewer |  |  |
| DetailComponentSection | Border | MainWindow.xaml | StackPanel < ScrollViewer < Grid < Grid |  |  |
| DetailComponentsHeader | StackPanel | MainWindow.xaml | Grid < StackPanel < Border < StackPanel |  |  |
| DetailComponentsTitle | TextBlock | MainWindow.xaml | StackPanel < Grid < StackPanel < Border | Text=Components |  |
| DetailDcDeleteBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | ToolTipService.ToolTip=Remove Display Commander | Click->UninstallDcButton_Click |
| DetailDcInfoBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | Content=Info | Click->InfoButton_Click |
| DetailDcIniBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | ToolTipService.ToolTip=Display Commander Settings | Click->DcCogButton_Click |
| DetailDcInstallBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border |  | Click->InstallDcButton_Click |
| DetailDcLabel | TextBlock | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | Text=Display Commander |  |
| DetailDcMessage | TextBlock | MainWindow.xaml | StackPanel < StackPanel < StackPanel < Border |  |  |
| DetailDcProgress | ProgressBar | MainWindow.xaml | StackPanel < StackPanel < StackPanel < Border |  |  |
| DetailDcRow | Grid | MainWindow.xaml | StackPanel < StackPanel < Border < StackPanel |  |  |
| DetailDcStatus | TextBlock | MainWindow.xaml | Grid < StackPanel < StackPanel < Border |  | PointerPressed->DetailDcStatus_PointerPressed; PointerEntered->LinkText_PointerEntered; PointerExited->LinkText_PointerExited |
| DetailDofFixCogBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | ToolTipService.ToolTip=DOF Fix Settings | Click->DofFixCogButton_Click |
| DetailDofFixDeleteBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | ToolTipService.ToolTip=Remove DOF Fix | Click->UninstallDofFixButton_Click |
| DetailDofFixInfoBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | Content=Info | Click->DofFixInfoButton_Click |
| DetailDofFixInstallBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border |  | Click->InstallDofFixButton_Click |
| DetailDofFixLabel | TextBlock | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | Text=DOF Fix |  |
| DetailDofFixMessage | TextBlock | MainWindow.xaml | StackPanel < StackPanel < StackPanel < Border |  |  |
| DetailDofFixProgress | ProgressBar | MainWindow.xaml | StackPanel < StackPanel < StackPanel < Border |  |  |
| DetailDofFixRow | Grid | MainWindow.xaml | StackPanel < StackPanel < Border < StackPanel |  |  |
| DetailDofFixStatus | TextBlock | MainWindow.xaml | Grid < StackPanel < StackPanel < Border |  | PointerPressed->DetailDofFixStatus_PointerPressed; PointerEntered->LinkText_PointerEntered; PointerExited->LinkText_PointerExited |
| DetailDxvkMessage | TextBlock | MainWindow.xaml | StackPanel < StackPanel < StackPanel < Border |  |  |
| DetailDxvkProgress | ProgressBar | MainWindow.xaml | StackPanel < StackPanel < StackPanel < Border |  |  |
| DetailEngineBadge | Border | MainWindow.xaml | StackPanel < Grid < Border < Grid |  | PointerPressed->EngineBadge_PointerPressed; PointerEntered->EngineBadge_PointerEntered; PointerExited->EngineBadge_PointerExited |
| DetailEngineIcon | Image | MainWindow.xaml | StackPanel < Border < StackPanel < Grid |  |  |
| DetailEngineText | TextBlock | MainWindow.xaml | StackPanel < Border < StackPanel < Grid |  |  |
| DetailFavBtn | Button | MainWindow.xaml | Grid < Border < Grid < StackPanel | ToolTipService.ToolTip=Toggle favourite | Click->FavouriteButton_Click |
| DetailFavIcon | TextBlock | MainWindow.xaml | Button < Grid < Border < Grid | Text=Favourite |  |
| DetailFolderBtn | Button | MainWindow.xaml | Grid < Border < Grid < StackPanel | ToolTipService.ToolTip=Open game folder in Explorer | Click->OpenFolder_Click |
| DetailGameName | TextBlock | MainWindow.xaml | StackPanel < Grid < StackPanel < ScrollViewer | ToolTipService.ToolTip={Binding Text, RelativeSource={RelativeSource Self}} |  |
| DetailGenericBadge | Border | MainWindow.xaml | WrapPanel < Grid < Border < Grid |  |  |
| DetailGenericText | TextBlock | MainWindow.xaml | Border < WrapPanel < Grid < Border |  |  |
| DetailGraphicsApiBadgePanel | StackPanel | MainWindow.xaml | StackPanel < Grid < Border < Grid |  |  |
| DetailHdrModSeparator | Grid | MainWindow.xaml | StackPanel < StackPanel < Border < StackPanel |  |  |
| DetailHdrToggleBtn | Button | MainWindow.xaml | StackPanel < Grid < Border < Grid | ToolTipService.ToolTip=Toggle per-game HDR auto-switch. Purple = HDR enabled on launch, disabled on exit. | Click->HdrToggle_Click |
| DetailHdrToggleText | TextBlock | MainWindow.xaml | Button < StackPanel < Grid < Border | Text=HDR |  |
| DetailHideBtn | Button | MainWindow.xaml | Grid < Border < Grid < StackPanel | ToolTipService.ToolTip=Hide this game from the sidebar (find it again in the Hidden filter) | Click->HideButton_Click |
| DetailHideIcon | TextBlock | MainWindow.xaml | Button < Grid < Border < Grid |  |  |
| DetailInstalledFile | TextBlock | MainWindow.xaml | Border < WrapPanel < Grid < Border |  |  |
| DetailInstalledFileBadge | Border | MainWindow.xaml | WrapPanel < Grid < Border < Grid |  |  |
| DetailInstallPath | TextBlock | MainWindow.xaml | StackPanel < Grid < StackPanel < ScrollViewer |  |  |
| DetailLaunchBtn | Button | MainWindow.xaml | StackPanel < Grid < Border < Grid | ToolTipService.ToolTip=Launch this game | Click->LaunchGame_Click |
| DetailLumaDeleteBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | ToolTipService.ToolTip=Remove Luma mod | Click->UninstallLumaButton_Click |
| DetailLumaInfoBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | Content=Info | Click->InfoButton_Click |
| DetailLumaInfoText | TextBlock | MainWindow.xaml | Grid < StackPanel < Border < StackPanel |  |  |
| DetailLumaIniBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | ToolTipService.ToolTip=Luma Settings | Click->LumaCogButton_Click |
| DetailLumaInstallBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border |  | Click->InstallLumaButton_Click |
| DetailLumaLabel | TextBlock | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | Text=Luma |  |
| DetailLumaMessage | TextBlock | MainWindow.xaml | StackPanel < StackPanel < StackPanel < Border |  |  |
| DetailLumaProgress | ProgressBar | MainWindow.xaml | StackPanel < StackPanel < StackPanel < Border |  |  |
| DetailLumaRow | Grid | MainWindow.xaml | StackPanel < StackPanel < Border < StackPanel |  |  |
| DetailLumaStatus | TextBlock | MainWindow.xaml | Grid < StackPanel < StackPanel < Border |  | PointerPressed->DetailLumaStatus_PointerPressed; PointerEntered->LinkText_PointerEntered; PointerExited->LinkText_PointerExited |
| DetailLumaToggle | ToggleButton | MainWindow.xaml | Grid < StackPanel < Border < StackPanel |  | Click->LumaToggle_Click |
| DetailLumaToggleText | TextBlock | MainWindow.xaml | ToggleButton < Grid < StackPanel < Border |  |  |
| DetailModStatusIcon | TextBlock | MainWindow.xaml | WrapPanel < Grid < Border < Grid |  |  |
| DetailNexusModsBtn | Button | MainWindow.xaml | StackPanel < Grid < Border < Grid | ToolTipService.ToolTip=Open Nexus Mods page | Click->NexusModsLink_Click |
| DetailNoModMsg | Border | MainWindow.xaml | StackPanel < StackPanel < Border < StackPanel |  |  |
| DetailOsMessage | TextBlock | MainWindow.xaml | StackPanel < Border < StackPanel < ScrollViewer |  |  |
| DetailOsProgress | ProgressBar | MainWindow.xaml | StackPanel < Border < StackPanel < ScrollViewer |  |  |
| DetailPanel | StackPanel | MainWindow.xaml | ScrollViewer < Grid < Grid < Grid |  |  |
| DetailPcgwBtn | Button | MainWindow.xaml | StackPanel < Grid < Border < Grid | ToolTipService.ToolTip=Open PCGamingWiki page | Click->PcgwLink_Click |
| DetailProgressSection | StackPanel | MainWindow.xaml | StackPanel < StackPanel < Border < StackPanel |  |  |
| DetailRdxDeleteBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | ToolTipService.ToolTip=Remove RenoDX mod | Click->UninstallButton_Click |
| DetailRdxInfoBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | Content=Info | Click->InfoButton_Click |
| DetailRdxInstallBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border |  | Click->InstallButton_Click |
| DetailRdxLabel | TextBlock | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | Text=RenoDX HDR |  |
| DetailRdxMessage | TextBlock | MainWindow.xaml | StackPanel < StackPanel < StackPanel < Border |  |  |
| DetailRdxProgress | ProgressBar | MainWindow.xaml | StackPanel < StackPanel < StackPanel < Border |  |  |
| DetailRdxRow | Grid | MainWindow.xaml | StackPanel < StackPanel < Border < StackPanel |  |  |
| DetailRdxStatus | TextBlock | MainWindow.xaml | Grid < StackPanel < StackPanel < Border |  | PointerPressed->DetailRdxStatus_PointerPressed; PointerEntered->LinkText_PointerEntered; PointerExited->LinkText_PointerExited |
| DetailRecommendedSeparator | Grid | MainWindow.xaml | StackPanel < StackPanel < Border < StackPanel |  |  |
| DetailRefCogBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | ToolTipService.ToolTip=RE Framework Settings | Click->RefCogButton_Click |
| DetailRefDeleteBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | ToolTipService.ToolTip=Remove RE Framework | Click->UninstallRefButton_Click |
| DetailRefInfoBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | Content=Info | Click->InfoButton_Click |
| DetailRefInstallBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border |  | Click->InstallRefButton_Click |
| DetailRefMessage | TextBlock | MainWindow.xaml | StackPanel < StackPanel < StackPanel < Border |  |  |
| DetailRefProgress | ProgressBar | MainWindow.xaml | StackPanel < StackPanel < StackPanel < Border |  |  |
| DetailRefRow | Grid | MainWindow.xaml | StackPanel < StackPanel < Border < StackPanel |  |  |
| DetailRefStatus | TextBlock | MainWindow.xaml | Grid < StackPanel < StackPanel < Border |  | PointerPressed->DetailRefStatus_PointerPressed; PointerEntered->LinkText_PointerEntered; PointerExited->LinkText_PointerExited |
| DetailResToggleBtn | Button | MainWindow.xaml | StackPanel < Grid < Border < Grid | ToolTipService.ToolTip=Toggle per-game resolution switch. Purple = resolution changes on launch, restores on exit. | Click->ResToggle_Click |
| DetailResToggleText | TextBlock | MainWindow.xaml | Button < StackPanel < Grid < Border | Text=RES |  |
| DetailRsDeleteBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | ToolTipService.ToolTip=Remove ReShade | Click->UninstallRsButton_Click |
| DetailRsInfoBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | Content=Info | Click->InfoButton_Click |
| DetailRsIniBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | ToolTipService.ToolTip=ReShade Settings | Click->RsCogButton_Click |
| DetailRsInstallBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border |  | Click->InstallRsButton_Click |
| DetailRsMessage | TextBlock | MainWindow.xaml | StackPanel < StackPanel < StackPanel < Border |  |  |
| DetailRsProgress | ProgressBar | MainWindow.xaml | StackPanel < StackPanel < StackPanel < Border |  |  |
| DetailRsRow | Grid | MainWindow.xaml | StackPanel < StackPanel < Border < StackPanel |  |  |
| DetailRsStatus | TextBlock | MainWindow.xaml | Grid < StackPanel < StackPanel < Border |  | PointerPressed->DetailRsStatus_PointerPressed; PointerEntered->LinkText_PointerEntered; PointerExited->LinkText_PointerExited |
| DetailScrollViewer | ScrollViewer | MainWindow.xaml | Grid < Grid < Grid < Grid |  | SizeChanged->DetailScrollViewer_SizeChanged |
| DetailSepModPlatform | Border | MainWindow.xaml | WrapPanel < Grid < Border < Grid |  |  |
| DetailSepPlatformStatus | Border | MainWindow.xaml | WrapPanel < Grid < Border < Grid |  |  |
| DetailSourceBadge | Border | MainWindow.xaml | StackPanel < Grid < Border < Grid |  |  |
| DetailSourceIcon | Image | MainWindow.xaml | StackPanel < Border < StackPanel < Grid |  |  |
| DetailSourceText | TextBlock | MainWindow.xaml | StackPanel < Border < StackPanel < Grid |  |  |
| DetailUeExtendedBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | ToolTipService.ToolTip=RenoDX Settings | Click->RdxCogButton_Click |
| DetailUlDeleteBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | ToolTipService.ToolTip=Remove ReLimiter | Click->UninstallUlButton_Click |
| DetailUlInfoBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | Content=Info | Click->InfoButton_Click |
| DetailUlIniBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | ToolTipService.ToolTip=ReLimiter Settings | Click->UlCogButton_Click |
| DetailUlInstallBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < Border |  | Click->InstallUlButton_Click |
| DetailUlLabel | TextBlock | MainWindow.xaml | Grid < StackPanel < StackPanel < Border | Text=ReLimiter |  |
| DetailUlMessage | TextBlock | MainWindow.xaml | StackPanel < StackPanel < StackPanel < Border |  |  |
| DetailUlProgress | ProgressBar | MainWindow.xaml | StackPanel < StackPanel < StackPanel < Border |  |  |
| DetailUlRow | Grid | MainWindow.xaml | StackPanel < StackPanel < Border < StackPanel |  |  |
| DetailUlStatus | TextBlock | MainWindow.xaml | Grid < StackPanel < StackPanel < Border |  | PointerPressed->DetailUlStatus_PointerPressed; PointerEntered->LinkText_PointerEntered; PointerExited->LinkText_PointerExited |
| DetailUltraPlusBtn | Button | MainWindow.xaml | StackPanel < Grid < Border < Grid | ToolTipService.ToolTip=Open Ultra+ page for this game | Click->UltraPlusLink_Click |
| DetailUwFixBtn | Button | MainWindow.xaml | StackPanel < Grid < Border < Grid | ToolTipService.ToolTip=Open ultrawide fix page | Click->UwFixLink_Click |
| DetailWikiBadge | Border | MainWindow.xaml | WrapPanel < Grid < Border < Grid |  |  |
| DetailWikiText | TextBlock | MainWindow.xaml | Border < WrapPanel < Grid < Border |  |  |
| DldsrApplyBtn | Button | MainWindow.xaml | Grid < Grid < StackPanel < StackPanel | Content=Apply; ToolTipService.ToolTip=Apply the selected DLDSR state. Causes ~15 second display blackout. | Click->DldsrApplyBtn_Click |
| DldsrCaptureBtn | Button | MainWindow.xaml | WrapPanel < StackPanel < StackPanel < Border | Content=Capture current; ToolTipService.ToolTip=Capture the current DLDSR state from NVIDIA Control Panel. Configure DLDSR in NVIDIA CP first, then capture. | Click->DldsrCaptureBtn_Click |
| DldsrControlCard | Border | MainWindow.xaml | StackPanel < StackPanel < ScrollViewer < Grid |  |  |
| DldsrCurrentStateText | TextBlock | MainWindow.xaml | StackPanel < StackPanel < Border < StackPanel | Text=(not checked) |  |
| DldsrDeleteBtn | Button | MainWindow.xaml | WrapPanel < StackPanel < StackPanel < Border | Content=Delete; ToolTipService.ToolTip=Delete the currently selected saved state. | Click->DldsrDeleteBtn_Click |
| DldsrInfoBtn | Button | MainWindow.xaml | StackPanel < StackPanel < StackPanel < Border | Content=i; ToolTipService.ToolTip=How to use DLDSR Control | Click->DldsrInfoBtn_Click |
| DldsrRefreshBtn | Button | MainWindow.xaml | WrapPanel < StackPanel < StackPanel < Border | Content=Refresh; ToolTipService.ToolTip=Refresh the current DLDSR state from registry. | Click->DldsrRefreshBtn_Click |
| DldsrSmoothnessApplyBtn | Button | MainWindow.xaml | Grid < StackPanel < StackPanel < StackPanel | Content=Set; ToolTipService.ToolTip=Apply the selected smoothness value. Causes ~15 second display blackout. | Click->DldsrSmoothnessApplyBtn_Click |
| DldsrSmoothnessSlider | Slider | MainWindow.xaml | Grid < StackPanel < StackPanel < StackPanel | ToolTipService.ToolTip=Adjust DSR smoothness (0-100%). Lower values are sharper, higher values are smoother. ~15 second display blackout on apply. |  |
| DldsrSmoothnessValueText | TextBlock | MainWindow.xaml | StackPanel < StackPanel < StackPanel < StackPanel | Text=33% |  |
| DldsrStateCombo | ComboBox | MainWindow.xaml | Grid < Grid < StackPanel < StackPanel | ToolTipService.ToolTip=Select a previously captured DLDSR state to apply. |  |
| DlssDefaultsSummaryPanel | StackPanel | MainWindow.xaml | StackPanel < StackPanel < Border < StackPanel |  |  |
| DlssIndicatorCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Controls the DLSS text overlay NVIDIA shows in the corner of games. Global system setting — affects all games. Requires admin. | SelectionChanged->DlssIndicatorCombo_SelectionChanged |
| DmfgFrameCountCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Global DMFG max frame count. Games using Dynamic mode inherit this unless overridden per-game. | SelectionChanged->DmfgFrameCountCombo_SelectionChanged |
| DmfgTargetFpsCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Global DMFG target frame rate. Off = no target. Max Refresh Rate = match display. Or pick a specific FPS. | SelectionChanged->DmfgTargetFpsCombo_SelectionChanged |
| DonateBtn | Button | MainWindow.xaml | StackPanel < Grid < Border < Grid | Content=Donate; ToolTipService.ToolTip=Support the mod authors | Click->DonateButton_Click |
| DropHelperCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=The drop helper window enables Discord drag-and-drop in admin mode. Disable if you don't need it. Restart required. | SelectionChanged->DropHelperCombo_SelectionChanged |
| DxvkVariantCombo | ComboBox | MainWindow.xaml | StackPanel < StackPanel < ScrollViewer < Grid |  | SelectionChanged->DxvkVariantCombo_SelectionChanged |
| ExportGameDataBtn | Button | MainWindow.xaml | WrapPanel < StackPanel < Grid < StackPanel | Content=Export game data; ToolTipService.ToolTip=Gathers game library data (APIs, exe paths, engine info) and copies a JSON file to clipboard — paste into Discord to share with the RHI community database | Click->ExportGameData_Click |
| ExtrasContainer | Border | MainWindow.xaml | StackPanel < ScrollViewer < Grid < Grid |  |  |
| ExtrasPanel | StackPanel | MainWindow.xaml | StackPanel < Border < StackPanel < ScrollViewer |  |  |
| FaqBtn | Button | MainWindow.xaml | StackPanel < Grid < Border < Grid | Content=Quick Start; ToolTipService.ToolTip=Quick Start Guide | Click->FaqButton_Click |
| FaqContentPanel | StackPanel | MainWindow.xaml | ScrollViewer < Grid < Grid < Grid |  |  |
| FaqPanel | Grid | MainWindow.xaml | Grid < Grid < Window |  |  |
| FilterDetected | Button | MainWindow.xaml | StackPanel < StackPanel < Border < Grid | Content=All Games; ToolTipService.ToolTip=Show every game except hidden ones. | Click->Filter_Click |
| FilterFavourites | Button | MainWindow.xaml | StackPanel < StackPanel < Border < Grid | Content=Favourites; ToolTipService.ToolTip=Show your favourite games, including hidden ones. | Click->Filter_Click |
| FilterHidden | Button | MainWindow.xaml | StackPanel < StackPanel < Border < Grid | Content=Hidden; ToolTipService.ToolTip=Show games you've hidden. | Click->Filter_Click |
| FilterInstalled | Button | MainWindow.xaml | StackPanel < StackPanel < Border < Grid | Content=Installed; ToolTipService.ToolTip=Show games with ReShade installed. | Click->Filter_Click |
| FilterLuma | Button | MainWindow.xaml | StackPanel < StackPanel < Border < Grid | Content=Luma; ToolTipService.ToolTip=Show games with a Luma mod available. Combines with the other engine and mod filters. | Click->Filter_Click |
| FilterOther | Button | MainWindow.xaml | StackPanel < StackPanel < Border < Grid | Content=Other; ToolTipService.ToolTip=Show games that use neither Unreal nor Unity. Combines with the other engine and mod filters. | Click->Filter_Click |
| FilterRenoDX | Button | MainWindow.xaml | StackPanel < StackPanel < Border < Grid | Content=RenoDX; ToolTipService.ToolTip=Show games with a RenoDX HDR mod, including the generic Unreal and Unity mods. Combines with the other engine and mod filters. | Click->Filter_Click |
| FilterUnity | Button | MainWindow.xaml | StackPanel < StackPanel < Border < Grid | Content=Unity; ToolTipService.ToolTip=Show Unity games. Combines with the other engine and mod filters. | Click->Filter_Click |
| FilterUnreal | Button | MainWindow.xaml | StackPanel < StackPanel < Border < Grid | Content=Unreal; ToolTipService.ToolTip=Show Unreal Engine games. Combines with the other engine and mod filters. | Click->Filter_Click |
| FpsLimitCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Global FPS cap via NVIDIA driver. VRR-optimal presets cap just below refresh rate to stay in VRR range. Choosing a cap also turns the per-game FPS limit Off for every game with ReLimiter or Display Commander, so the two limiters don't fight. | SelectionChanged->FpsLimitCombo_SelectionChanged |
| FullRefreshBtn | Button | MainWindow.xaml | WrapPanel < StackPanel < Border < StackPanel | Content=Full refresh; ToolTipService.ToolTip=Clears all caches and re-scans everything from disk. Use as a last resort if games are missing, paths have changed, DLSS has been added to a game, or the DLSS section is missing from a game card. | Click->FullRefreshButton_Click |
| GameCountText | TextBlock | MainWindow.xaml | StackPanel < Border < Grid < Border | Text=0 shown; ToolTipService.ToolTip=Games shown with the current filters. |  |
| GameList | ListView | MainWindow.xaml | Grid < Border < Grid < Grid |  | SelectionChanged->GameList_SelectionChanged; DoubleTapped->GameList_DoubleTapped |
| GameViewPanel | Grid | MainWindow.xaml | Grid < Grid < Window |  |  |
| GitHubDisconnectRow | StackPanel | MainWindow.xaml | WrapPanel < StackPanel < Grid < StackPanel |  |  |
| GitHubPatBox | TextBox | MainWindow.xaml | Grid < Grid < StackPanel < Grid | PlaceholderText=ghp_...; ToolTipService.ToolTip=Paste a GitHub Personal Access Token (PAT). It stays valid until you revoke it. No scopes needed, just generate and paste. |  |
| GitHubPatClearBtn | Button | MainWindow.xaml | WrapPanel < StackPanel < Grid < StackPanel | Content=Remove; ToolTipService.ToolTip=Remove the saved token. | Click->GitHubPatClearBtn_Click |
| GitHubPatSaveBtn | Button | MainWindow.xaml | Grid < Grid < StackPanel < Grid | Content=Save | Click->GitHubPatSaveBtn_Click |
| GitHubPatStatusText | TextBlock | MainWindow.xaml | StackPanel < Grid < StackPanel < Border | Text= |  |
| GitHubStatusText | TextBlock | MainWindow.xaml | StackPanel < Grid < StackPanel < Border | Text=Not connected (60 req/hr) |  |
| GlobalPowerModeCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Global power management mode — applies to all games without a per-game override. | SelectionChanged->GlobalPowerModeCombo_SelectionChanged |
| GlobalReBarEnableCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Global Resizable BAR — enables large GPU memory transfers for all games. Requires admin and compatible hardware (RTX 30 Series+). | SelectionChanged->GlobalReBarEnableCombo_SelectionChanged |
| GlobalReBarSizeCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Maximum BAR transfer size. 1GB is optimal for most games. Decrease to 512MB if experiencing ReBAR-related stutters. | SelectionChanged->GlobalReBarSizeCombo_SelectionChanged |
| GlobalUpdateInclusionBtn | Button | MainWindow.xaml | StackPanel < Grid < StackPanel < Border | Content=Choose; ToolTipService.ToolTip=Choose which components RHI checks for updates. Components you turn off are not checked during startup or Update All. | Click->GlobalUpdateInclusion_Click |
| GlobalUpdateSummaryText | TextBlock | MainWindow.xaml | StackPanel < Grid < StackPanel < Border |  |  |
| GlobalVSyncCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Global VSync mode — applies to all games without a per-game override. | SelectionChanged->GlobalVSyncCombo_SelectionChanged |
| GSyncEnableCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Globally enables or disables G-Sync/FreeSync variable refresh rate. | SelectionChanged->GSyncEnableCombo_SelectionChanged |
| GSyncIndicatorCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Shows NVIDIA's G-Sync indicator in games when G-Sync is active. Global driver setting, affects all games. | SelectionChanged->GSyncIndicatorCombo_SelectionChanged |
| GSyncModeCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Controls G-Sync/FreeSync variable refresh rate. Fullscreen and Windowed recommended for seamless experience. | SelectionChanged->GSyncModeCombo_SelectionChanged |
| HdrAutoToggleCombo | ComboBox | MainWindow.xaml | Grid < Grid < StackPanel < Grid | ToolTipService.ToolTip=When On, Windows HDR is automatically enabled when launching a game through RHI and disabled when the game exits. | SelectionChanged->HdrAutoToggleCombo_SelectionChanged |
| HdrModsListBtn | Button | MainWindow.xaml | StackPanel < Grid < Border < Grid | Content=Available Mods; ToolTipService.ToolTip=Loading mod data, please wait... | Click->HdrModsListBtn_Click |
| HiddenCountText | TextBlock | MainWindow.xaml | StackPanel < Border < Grid < Border | Text=; ToolTipService.ToolTip=Hidden games. Use the Hidden filter to see them. |  |
| HotkeyBox | TextBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | PlaceholderText=Press a key...; ToolTipService.ToolTip=Opens the ReShade overlay in game. Click here and press a key to change it. |  |
| InstalledCountText | TextBlock | MainWindow.xaml | StackPanel < Border < Grid < Border | Text=0 ReShade; ToolTipService.ToolTip=Games with ReShade installed. |  |
| LinksBtn | Button | MainWindow.xaml | StackPanel < Grid < Border < Grid | Content=Links; ToolTipService.ToolTip=Useful links and resources |  |
| LoadingPanel | StackPanel | MainWindow.xaml | Grid < Grid < Window |  |  |
| LoadingRing | ProgressRing | MainWindow.xaml | StackPanel < Grid < Grid < Window |  |  |
| LoadingSubtitle | TextBlock | MainWindow.xaml | StackPanel < Grid < Grid < Window | Text= |  |
| LoadingTitle | TextBlock | MainWindow.xaml | StackPanel < Grid < Grid < Window | Text=Loading... |  |
| ManagementContainer | Border | MainWindow.xaml | StackPanel < ScrollViewer < Grid < Grid |  |  |
| ManagementPanel | StackPanel | MainWindow.xaml | Border < StackPanel < ScrollViewer < Grid |  |  |
| NeuralRenderingContainer | Border | MainWindow.xaml | StackPanel < ScrollViewer < Grid < Grid |  |  |
| NeuralRenderingPanel | StackPanel | MainWindow.xaml | Border < StackPanel < ScrollViewer < Grid |  |  |
| NewModsBtn | Button | MainWindow.xaml | StackPanel < Grid < Border < Grid | Content=New Mods Available; ToolTipService.ToolTip=New RenoDX mods have been added to the wiki | Click->NewModsButton_Click |
| NexusConnectBtn | Button | MainWindow.xaml | Grid < Grid < StackPanel < Border | Content=Connect; ToolTipService.ToolTip=Sign in to Nexus Mods in your browser so RHI can download mods from Nexus. | Click->NexusConnectBtn_Click |
| NexusDisconnectRow | StackPanel | MainWindow.xaml | StackPanel < Border < StackPanel < Grid |  |  |
| NexusModsCard | Border | MainWindow.xaml | StackPanel < Grid < StackPanel < Border |  |  |
| NexusStatusText | TextBlock | MainWindow.xaml | Grid < Grid < StackPanel < Border | Text=Not connected |  |
| NvidiaProfileContainer | Border | MainWindow.xaml | StackPanel < ScrollViewer < Grid < Grid |  |  |
| NvidiaProfileDlssContainer | Border | MainWindow.xaml | StackPanel < ScrollViewer < Grid < Grid |  |  |
| NvidiaProfileDlssPanel | StackPanel | MainWindow.xaml | Border < StackPanel < ScrollViewer < Grid |  |  |
| NvidiaProfileDriverContainer | Border | MainWindow.xaml | StackPanel < ScrollViewer < Grid < Grid |  |  |
| NvidiaProfileDriverPanel | StackPanel | MainWindow.xaml | Border < StackPanel < ScrollViewer < Grid |  |  |
| NxmRegisterBtn | Button | MainWindow.xaml | Grid < Grid < StackPanel < Border | Content=Register; ToolTipService.ToolTip=Open Nexus "Mod Manager Download" links in RHI. | Click->NxmRegisterBtn_Click |
| NxmStatusText | TextBlock | MainWindow.xaml | Grid < Grid < StackPanel < Border | Text=Not registered |  |
| OpenAppDataBtn | Button | MainWindow.xaml | WrapPanel < StackPanel < Grid < StackPanel | Content=RHI data; ToolTipService.ToolTip=Open RHI's data folder in AppData | Click->OpenAppDataFolder_Click |
| OpenCustomBtn | Button | MainWindow.xaml | WrapPanel < StackPanel < Grid < StackPanel | Content=Custom; ToolTipService.ToolTip=Open the Custom folder for your own ReShade, DLSS or Streamline DLLs | Click->OpenCustomFolder_Click |
| OpenDownloadsBtn | Button | MainWindow.xaml | WrapPanel < StackPanel < Grid < StackPanel | Content=Downloads; ToolTipService.ToolTip=Open RHI's downloads folder | Click->OpenDownloadsFolder_Click |
| OpenLogsBtn | Button | MainWindow.xaml | WrapPanel < StackPanel < Grid < StackPanel | Content=Logs; ToolTipService.ToolTip=Open the Logs folder | Click->OpenLogsFolder_Click |
| OsDlssInputsCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < StackPanel < Grid | ToolTipService.ToolTip=On: OptiScaler reports an NVIDIA GPU to the game, so the game offers DLSS and OptiScaler uses its inputs. Off: no GPU spoofing. Used when OptiScaler.ini is installed. | SelectionChanged->OsDlssInputsCombo_SelectionChanged |
| OsDlssInputsPanel | StackPanel | MainWindow.xaml | StackPanel < Grid < StackPanel < Border |  |  |
| OsGpuCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=The GPU brand in this PC. RHI uses it to pick which OptiScaler.ini to install. DLSS inputs appears for AMD and Intel. | SelectionChanged->OsGpuCombo_SelectionChanged |
| OsHotkeyCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=The key that opens the OptiScaler overlay in game. Saved straight away for new installs; use the button to update games that already have OptiScaler. | SelectionChanged->OsHotkeyCombo_SelectionChanged |
| OverridesContainer | Border | MainWindow.xaml | StackPanel < ScrollViewer < Grid < Grid |  |  |
| OverridesHeaderRow | StackPanel | MainWindow.xaml | StackPanel < Border < StackPanel < ScrollViewer |  |  |
| OverridesPanel | StackPanel | MainWindow.xaml | StackPanel < Border < StackPanel < ScrollViewer |  |  |
| PeakNitsAutoBtn | Button | MainWindow.xaml | Grid < Grid < StackPanel < Grid | Content=Auto; ToolTipService.ToolTip=Reads your monitor's peak brightness automatically. | Click->PeakNitsAuto_Click |
| PeakNitsBox | TextBox | MainWindow.xaml | Grid < Grid < StackPanel < Grid | PlaceholderText=nits; ToolTipService.ToolTip=Your monitor's peak brightness in nits, written to reshade.ini presets. Press Enter to save. | KeyDown->PeakNitsBox_KeyDown; LostFocus->PeakNitsBox_LostFocus |
| PerGameScreenshotCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=When On, each game gets its own screenshot subfolder. | SelectionChanged->PerGameScreenshotCombo_SelectionChanged |
| PreferredRefreshRateCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Highest available forces your max refresh rate. App Setting lets games choose their own refresh rate. | SelectionChanged->PreferredRefreshRateCombo_SelectionChanged |
| PurgeCacheBtn | Button | MainWindow.xaml | WrapPanel < StackPanel < Border < StackPanel | Content=Purge staging files; ToolTipService.ToolTip=Deletes cached DLSS, Streamline, and component staging files to free disk space. Shaders, installed addons, and version metadata are preserved. Files are re-downloaded automatically when needed. | Click->PurgeCachedFiles_Click |
| ReBarAdminWarning | TextBlock | MainWindow.xaml | Border < StackPanel < Grid < StackPanel | Text=Admin |  |
| RecentGamesCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Right-click the tray icon or pinned taskbar icon to quickly launch recent games. | SelectionChanged->RecentGamesCombo_SelectionChanged |
| RefreshBtn | Button | MainWindow.xaml | StackPanel < Grid < Border < Grid | Content=Refresh; ToolTipService.ToolTip=Rescan game library and fetch latest mod info | Click->RefreshButton_Click |
| RenoDxDbSourceCard | Border | MainWindow.xaml | StackPanel < StackPanel < ScrollViewer < Grid |  |  |
| RenoDxDbSourceCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=RHI Database uses the community-maintained RHI database (recommended). RenoDX Wiki falls back to the RenoDX wiki scraper. Changing this setting triggers an immediate refresh. |  |
| ResAutoToggleCombo | ComboBox | MainWindow.xaml | Grid < Grid < StackPanel < Grid | ToolTipService.ToolTip=When On, switches to the target resolution on game launch and restores it on exit. | SelectionChanged->ResAutoToggleCombo_SelectionChanged |
| ReShadeAddonsItem | MenuFlyoutItem | MainWindow.xaml | MenuFlyout < Button.Flyout < Button < StackPanel | Text=Global Addons | Click->ReShadeAddonsButton_Click |
| ReShadeChannelCombo | ComboBox | MainWindow.xaml | StackPanel < StackPanel < ScrollViewer < Grid |  | SelectionChanged->ReShadeChannelCombo_SelectionChanged |
| ResolutionControlCard | Border | MainWindow.xaml | StackPanel < StackPanel < ScrollViewer < Grid |  |  |
| ResolutionTargetCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Desktop resolution to switch to before a game launches. Restored on game exit. | SelectionChanged->ResolutionTargetCombo_SelectionChanged |
| RsVariableListUseTabsCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Controls how shaders are displayed in the ReShade overlay. Tabs groups effect files into named tabs; Tree shows a collapsible hierarchy. | SelectionChanged->RsVariableListUseTabsCombo_SelectionChanged |
| SaveFilterButton | Button | MainWindow.xaml | Grid < Border < Grid < Border | Content=+; ToolTipService.ToolTip=Save current search as a custom filter | Click->SaveFilterButton_Click |
| ScreenshotHotkeyBox | TextBox | MainWindow.xaml | Grid < Grid < StackPanel < Grid | PlaceholderText=Press a key...; ToolTipService.ToolTip=Press a key to assign it, or Backspace to clear it. |  |
| ScreenshotPathBox | TextBox | MainWindow.xaml | Grid < Grid < StackPanel < Grid | PlaceholderText=Type or choose a folder; ToolTipService.ToolTip=Where ReShade saves screenshots. Type a folder and press Enter, or use Browse. | KeyDown->ScreenshotPathBox_KeyDown; LostFocus->ScreenshotPathBox_LostFocus |
| SearchBox | TextBox | MainWindow.xaml | Grid < Border < Grid < Border | PlaceholderText=Filter games... | TextChanged->SearchBox_TextChanged |
| SettingsBackBtn | Button | MainWindow.xaml | StackPanel < Border < Grid < Grid | Content=← Back to Games | Click->SettingsBack_Click |
| SettingsBtn | Button | MainWindow.xaml | StackPanel < Grid < Border < Grid | Content=Settings; ToolTipService.ToolTip=Open settings | Click->SettingsButton_Click |
| SettingsCardsPanel | StackPanel | MainWindow.xaml | ScrollViewer < Grid < Grid < Grid |  |  |
| SettingsPanel | Grid | MainWindow.xaml | Grid < Grid < Window |  |  |
| ShaderCacheSizeCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Maximum disk space for compiled shader cache. Larger = fewer stutters on revisits. 16GB is the driver default. | SelectionChanged->ShaderCacheSizeCombo_SelectionChanged |
| ShaderPrecompileCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Auto-compiles shaders in the background when idle. Higher = more CPU usage during idle, faster first-time game loads. | SelectionChanged->ShaderPrecompileCombo_SelectionChanged |
| ShadersAddonsBtn | Button | MainWindow.xaml | StackPanel < Grid < Border < Grid | Content=Shaders/Addons; ToolTipService.ToolTip=Manage global shaders and ReShade addons |  |
| SkeletonDetailPanel | StackPanel | MainWindow.xaml | ScrollViewer < Grid < Grid < Grid |  |  |
| SkeletonDetailScrollViewer | ScrollViewer | MainWindow.xaml | Grid < Grid < Grid < Grid |  |  |
| SkeletonRowPanel | StackPanel | MainWindow.xaml | Grid < Border < Grid < Grid |  |  |
| StartWithWindowsCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel |  | SelectionChanged->StartWithWindowsCombo_SelectionChanged |
| StatusBarText | TextBlock | MainWindow.xaml | Grid < Border < Grid < Window | Text= |  |
| StatusBarVersionText | Button | MainWindow.xaml | StackPanel < Grid < Border < Grid | ToolTipService.ToolTip=Check for app updates | Click->StatusBarVersion_Click |
| StatusDot | Ellipse | MainWindow.xaml | StackPanel < Border < Grid < Border |  |  |
| SupportBtn | Button | MainWindow.xaml | StackPanel < Grid < Border < Grid | Content=Help; ToolTipService.ToolTip=Support and help resources |  |
| UlDlssHooksCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Shows DLSS quality mode, render resolution, and active features on the ReLimiter OSD. Disable if experiencing crashes. | SelectionChanged->UlDlssHooksCombo_SelectionChanged |
| UlHotkeyBox | TextBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | PlaceholderText=Press a key...; ToolTipService.ToolTip=The key that toggles the ReLimiter OSD overlay in-game. |  |
| UlSharedPresetsCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=When On, all games share the same OSD presets file. When Off, each game has its own presets. | SelectionChanged->UlSharedPresetsCombo_SelectionChanged |
| UlTargetFpsCombo | ComboBox | MainWindow.xaml | Grid < StackPanel < Grid < StackPanel | ToolTipService.ToolTip=Global FPS cap written to all relimiter.ini files. Select a VRR preset or Custom for a manual value. | SelectionChanged->UlTargetFpsCombo_SelectionChanged |
| UpdateBtn | Button | MainWindow.xaml | StackPanel < Grid < Border < Grid | Content=Update All; ToolTipService.ToolTip={x:Bind ViewModel.UpdateButtonTooltip, Mode=OneWay} | Click->UpdateAllButton_Click |
| UpdateLogBtn | Button | MainWindow.xaml | StackPanel < Grid < Border < Grid | Content=Updates; ToolTipService.ToolTip=View component update history | Click->UpdateLogButton_Click |
| RootContent | ContentPresenter | SetupWindow.xaml | Grid < Window |  |  |

## event handlers

| handler | event | control | defined in | services | calls |
|---|---|---|---|---|---|
| AboutBack_Click | Click |  | MainWindow.Events.Settings.cs:2247 |  |  |
| AboutButton_Click | Click |  | MainWindow.Events.Settings.cs:2234 |  |  |
| AddGameButton_Click | Click | AddGameBtn | MainWindow.Events.cs:1178 |  | DialogService.ShowSafeAsync |
| AddonWatchFolderBox_KeyDown | KeyDown | AddonWatchFolderBox | MainWindow.Events.SettingsInputs.cs:42 |  | DialogService.ShowSafeAsync |
| AddonWatchFolderBox_LostFocus | LostFocus | AddonWatchFolderBox | MainWindow.Events.SettingsInputs.cs:56 |  | DialogService.ShowSafeAsync |
| AdminModeCombo_SelectionChanged | SelectionChanged | AdminModeCombo | MainWindow.Events.Settings.cs:109 |  |  |
| ApplyOsHotkey_Click | Click | ApplyOsHotkeyBtn | MainWindow.Events.Settings.cs:178 | DlssStreamlineService |  |
| ApplyPeakNitsToAll_Click | Click | ApplyScreenshotPathBtn | MainWindow.Events.Settings.cs:127 | DlssStreamlineService |  |
| ApplyScreenshotPath_Click | Click | ApplyHotkeyBtn | MainWindow.Events.Settings.cs:124 |  |  |
| ApplyUlOsdHotkey_Click | Click | ApplyUlHotkeyBtn | SettingsHandler.Hotkeys.cs:199 |  | AuxInstallService.ApplyUlDlssHooks, AuxInstallService.ApplyUlOsdHotkey, AuxInstallService.ApplyUlSharedPresets, AuxInstallService.ApplyUlTargetFps, AuxInstallService.KeepDisplayModeValues, DialogService.ShowSafeAsync, ModInstallService.GetAddonDeployPath |
| AutoUpdateComponentsCombo_SelectionChanged | SelectionChanged | AutoUpdateComponentsCombo | MainWindow.Events.Settings.cs:1291 |  | DialogService.ShowSafeAsync, HdrToggleService.GetAllDisplays |
| AutoUpdateDlssCombo_SelectionChanged | SelectionChanged | AutoUpdateDlssCombo | MainWindow.Events.Settings.cs:1277 |  | DialogService.ShowSafeAsync, HdrToggleService.GetAllDisplays |
| AutoUpdateStreamlineCombo_SelectionChanged | SelectionChanged | AutoUpdateStreamlineCombo | MainWindow.Events.Settings.cs:1284 |  | DialogService.ShowSafeAsync, HdrToggleService.GetAllDisplays |
| BackgroundUpdateChecksCombo_SelectionChanged | SelectionChanged | BackgroundUpdateChecksCombo | MainWindow.Events.Settings.cs:1298 |  | DialogService.ShowSafeAsync, HdrToggleService.GetAllDisplays |
| BrowseAddonWatchFolder_Click | Click |  | MainWindow.Events.cs:131 |  | AuxInstallService.GetLumaReshadeIniValue, AuxInstallService.RemoveLumaReshadeIniValue, AuxInstallService.SetLumaReshadeIniValue |
| BrowseScreenshotPath_Click | Click |  | MainWindow.Events.Settings.cs:2064 |  |  |
| CacheAllShadersCombo_SelectionChanged | SelectionChanged | CacheAllShadersCombo | MainWindow.Events.Settings.cs:175 | DlssStreamlineService |  |
| CheckForUpdatesButton_Click | Click | CheckForUpdatesBtn | MainWindow.Events.cs:61 |  | dialogService.CheckForAppUpdateAsync, DialogService.ShowProgressAsync |
| ChooseShadersButton_Click | Click | ChooseShadersItem | InstallEventHandler.cs:491 |  | DialogService.ShowSafeAsync |
| ClearNvidiaShaderCache_Click | Click |  | MainWindow.Events.Settings.cs:1153 |  | DialogService.ShowSafeAsync |
| CloseToTrayCombo_SelectionChanged | SelectionChanged | CloseToTrayCombo | MainWindow.Events.Settings.cs:1394 |  | TrayIconService.ClearJumpList, TrayIconService.Initialize, TrayIconService.UpdateJumpList, TrayIconService.UpdateRecentGames |
| ColorApplyBtn_Click | Click | ColorApplyBtn | MainWindow.Events.Settings.cs:1550 |  | DialogService.ShowSafeAsync, HdrToggleService.GetAllDisplays, NvColorService.LabelToBpc, NvColorService.LabelToDynamicRange, NvColorService.SetColorData |
| ColorDisplayCombo_SelectionChanged | SelectionChanged | ColorDisplayCombo | MainWindow.Events.Settings.cs:1514 |  | DialogService.ShowSafeAsync, HdrToggleService.GetAllDisplays, NvColorService.BpcToLabel, NvColorService.DynamicRangeToLabel, NvColorService.GetColorData, NvColorService.LabelToBpc, NvColorService.LabelToDynamicRange, NvColorService.SetColorData |
| CopyLogsArchive_Click | Click | CopyLogsArchiveBtn | SettingsHandler.DataTools.cs:177 |  | DialogService.ShowSafeAsync |
| CreateMissingNvidiaProfiles_Click | Click |  | MainWindow.Events.Settings.cs:645 |  | DialogService.ShowSafeAsync, presetService.EnsureProfileExists, presetService.ExportProfiles |
| CustomShadersCombo_SelectionChanged | SelectionChanged | CustomShadersCombo | MainWindow.Events.Settings.cs:121 |  |  |
| DcCogButton_Click | Click | DetailDcIniBtn | MainWindow.Events.Components.DisplayCommander.cs:24 |  | Dlssg20_30Service.ReadIniValue, Dlssg20_30Service.WriteIniValue, ModInstallService.GetAddonDeployPath |
| DetailDcStatus_PointerPressed | PointerPressed | DetailDcStatus | MainWindow.Events.Install.cs:233 |  | dofFixService.InstallAsync |
| DetailDofFixStatus_PointerPressed | PointerPressed | DetailDofFixStatus | MainWindow.Events.Install.cs:409 |  | dofFixService.GetReleaseUrl |
| DetailLumaStatus_PointerPressed | PointerPressed | DetailLumaStatus | MainWindow.Events.Install.cs:250 |  | dofFixService.InstallAsync, dofFixService.Uninstall |
| DetailRdxStatus_PointerPressed | PointerPressed | DetailRdxStatus | MainWindow.Events.Install.cs:456 |  | dofFixService.IsForceEligible |
| DetailRefStatus_PointerPressed | PointerPressed | DetailRefStatus | MainWindow.Events.Install.cs:443 |  | dofFixService.IsForceEligible |
| DetailRsStatus_PointerPressed | PointerPressed | DetailRsStatus | MainWindow.Events.Install.cs:429 |  |  |
| DetailScrollViewer_SizeChanged | SizeChanged | DetailScrollViewer | MainWindow.Events.cs:970 |  |  |
| DetailUlStatus_PointerPressed | PointerPressed | DetailUlStatus | MainWindow.Events.Install.cs:214 |  |  |
| DigitalVibrance_Click | Click |  | MainWindow.Events.Settings.cs:856 |  | DialogService.ShowSafeAsync, DigitalVibranceService.EnumerateDisplays, DigitalVibranceService.GetLevel, DigitalVibranceService.SetLevel |
| DldsrApplyBtn_Click | Click | DldsrApplyBtn | MainWindow.Events.Settings.cs:1801 | DldsrService | DialogService.ShowSafeAsync, dldsrService.ApplyStateAsync, ResolutionToggleService.GetSupportedResolutions |
| DldsrCaptureBtn_Click | Click | DldsrCaptureBtn | MainWindow.Events.Settings.cs:1884 | DldsrService | DialogService.ShowSafeAsync, dldsrService.CaptureCurrentState, dldsrService.DeleteCapture |
| DldsrDeleteBtn_Click | Click | DldsrDeleteBtn | MainWindow.Events.Settings.cs:1962 | DldsrService | DialogService.ShowSafeAsync, dldsrService.DeleteCapture, dldsrService.SetSmoothnessAsync |
| DldsrInfoBtn_Click | Click | DldsrInfoBtn | MainWindow.Events.Settings.cs:1690 |  |  |
| DldsrRefreshBtn_Click | Click | DldsrRefreshBtn | MainWindow.Events.Settings.cs:2000 | DldsrService | DialogService.ShowSafeAsync, dldsrService.SetSmoothnessAsync |
| DldsrSmoothnessApplyBtn_Click | Click | DldsrSmoothnessApplyBtn | MainWindow.Events.Settings.cs:2006 | DldsrService | DialogService.ShowSafeAsync, dldsrService.SetSmoothnessAsync |
| DlssDefaults_Click | Click |  | MainWindow.Events.Settings.cs:227 | DlssStreamlineService |  |
| DlssIndicatorCombo_SelectionChanged | SelectionChanged | DlssIndicatorCombo | MainWindow.Events.Settings.cs:1221 |  | presetService.SetGSyncIndicator |
| DmfgFrameCountCombo_SelectionChanged | SelectionChanged | DmfgFrameCountCombo | MainWindow.Events.Settings.cs:500 |  | DialogService.ShowSafeAsync, presetService.SetGlobalDmfgFrameCount, presetService.SetGlobalDmfgTargetFps, presetService.SetGlobalReBarEnableMode |
| DmfgTargetFpsCombo_SelectionChanged | SelectionChanged | DmfgTargetFpsCombo | MainWindow.Events.Settings.cs:513 |  | DialogService.ShowSafeAsync, presetService.SetGlobalDmfgTargetFps, presetService.SetGlobalReBarEnableMode |
| DofFixCogButton_Click | Click | DetailDofFixCogBtn | MainWindow.Events.Install.cs:395 |  | dofFixService.GetReleaseUrl |
| DofFixInfoButton_Click | Click | DetailDofFixInfoBtn | MainWindow.Events.Install.cs:357 |  | DialogService.ShowSafeAsync, dofFixService.CheckForUpdateAsync, dofFixService.GetReleaseUrl |
| DonateButton_Click | Click | DonateBtn | MainWindow.Events.cs:287 |  | DialogService.ShowSafeAsync |
| DropHelperCombo_SelectionChanged | SelectionChanged | DropHelperCombo | MainWindow.Events.Settings.cs:1386 |  | TrayIconService.ClearJumpList, TrayIconService.Initialize, TrayIconService.UpdateJumpList, TrayIconService.UpdateRecentGames |
| DxvkVariantCombo_SelectionChanged | SelectionChanged | DxvkVariantCombo | SettingsHandler.Channels.cs:18 |  | DialogService.ShowSafeAsync, dxvkService.EnsureStagingAsync |
| EngineBadge_PointerEntered | PointerEntered | DetailEngineBadge | MainWindow.Events.Install.cs:602 |  | HdrToggleService.IsHdrEnabled |
| EngineBadge_PointerExited | PointerExited | DetailEngineBadge | MainWindow.Events.Install.cs:611 |  | HdrToggleService.EnableHdr, HdrToggleService.IsHdrEnabled |
| EngineBadge_PointerPressed | PointerPressed | DetailEngineBadge | MainWindow.Events.Install.cs:510 |  | DialogService.ShowSafeAsync, dofFixService.IsForceEligible, dofFixService.IsGameEligible, dofFixService.Uninstall |
| ExportGameData_Click | Click | ExportGameDataBtn | SettingsHandler.DataTools.cs:14 | GameNameService, PcgwService | DialogService.ShowSafeAsync |
| ExportNvidiaProfiles_Click | Click |  | MainWindow.Events.Settings.cs:707 |  | DialogService.ShowSafeAsync, presetService.ExportProfiles, VulkanLayerService.IsRunningAsAdmin |
| FaqBack_Click | Click |  | MainWindow.Events.Settings.cs:2427 |  | ToolTipService.SetToolTip |
| FaqButton_Click | Click | FaqBtn | MainWindow.Events.Settings.cs:2421 |  | ToolTipService.SetToolTip |
| FavouriteButton_Click | Click | DetailFavBtn | MainWindow.Events.cs:1272 |  | addonService.EnsureLatestAsync, DialogService.ShowSafeAsync |
| Filter_Click | Click | FilterDetected | MainWindow.Events.cs:1265 |  | addonService.EnsureLatestAsync, DialogService.ShowSafeAsync |
| FpsLimitCombo_SelectionChanged | SelectionChanged | FpsLimitCombo | MainWindow.Events.Settings.cs:392 |  | DialogService.ShowSafeAsync, presetService.SetGlobalFpsLimit, presetService.SetPerGameFpsLimit |
| FullRefreshButton_Click | Click | FullRefreshBtn | MainWindow.Events.cs:25 |  | DialogService.ShowProgressAsync, DialogService.ShowSafeAsync |
| GameList_DoubleTapped | DoubleTapped | GameList | MainWindow.Events.Install.cs:646 |  | HdrToggleService.EnableHdr, HdrToggleService.IsHdrEnabled |
| GameList_SelectionChanged | SelectionChanged | GameList | MainWindow.xaml.cs:696 |  |  |
| GameRow_RightTapped | RightTapped |  | MainWindow.Events.cs:657 |  | dialogService.ShowAddonInfoDialogAsync |
| GitHubDisconnectBtn_Click | Click |  | SettingsHandler.Accounts.cs:246 |  | DevUnlockService.ResetTokenCache, GitHubAuthService.ApplyTokenToHttpClient |
| GitHubPatClearBtn_Click | Click | GitHubPatClearBtn | SettingsHandler.Accounts.cs:362 |  | DevUnlockService.ResetTokenCache, GitHubAuthService.ApplyTokenToHttpClient |
| GitHubPatSaveBtn_Click | Click | GitHubPatSaveBtn | SettingsHandler.Accounts.cs:264 |  | DevUnlockService.ResetTokenCache, DevUnlockService.UpdateToken |
| GlobalPowerModeCombo_SelectionChanged | SelectionChanged | GlobalPowerModeCombo | MainWindow.Events.Settings.cs:632 |  | DialogService.ShowSafeAsync, presetService.EnsureProfileExists, presetService.ExportProfiles, presetService.SetGlobalPowerMode |
| GlobalReBarEnableCombo_SelectionChanged | SelectionChanged | GlobalReBarEnableCombo | MainWindow.Events.Settings.cs:576 |  | presetService.SetGlobalReBarEnableMode, presetService.SetGlobalReBarSizeLimit, presetService.SetGlobalVSyncMode |
| GlobalReBarSizeCombo_SelectionChanged | SelectionChanged | GlobalReBarSizeCombo | MainWindow.Events.Settings.cs:596 |  | DialogService.ShowSafeAsync, presetService.SetGlobalPowerMode, presetService.SetGlobalReBarSizeLimit, presetService.SetGlobalVSyncMode |
| GlobalUpdateInclusion_Click | Click | GlobalUpdateInclusionBtn | MainWindow.Events.Settings.cs:163 | DlssStreamlineService |  |
| GlobalVSyncCombo_SelectionChanged | SelectionChanged | GlobalVSyncCombo | MainWindow.Events.Settings.cs:614 |  | DialogService.ShowSafeAsync, presetService.EnsureProfileExists, presetService.SetGlobalPowerMode, presetService.SetGlobalVSyncMode |
| Grid_DragOver | DragOver |  | MainWindow.UISync.cs:320 |  | AuxInstallService.SanitizeDirectoryName |
| Grid_Drop | Drop |  | MainWindow.UISync.cs:323 |  | AuxInstallService.SanitizeDirectoryName |
| GSyncEnableCombo_SelectionChanged | SelectionChanged | GSyncEnableCombo | MainWindow.Events.Settings.cs:379 |  | DialogService.ShowSafeAsync, presetService.SetGlobalFpsLimit, presetService.SetGlobalGSyncEnabled, presetService.SetPerGameFpsLimit |
| GSyncIndicatorCombo_SelectionChanged | SelectionChanged | GSyncIndicatorCombo | MainWindow.Events.Settings.cs:1268 |  | DialogService.ShowSafeAsync, HdrToggleService.GetAllDisplays, presetService.SetGSyncIndicator |
| GSyncModeCombo_SelectionChanged | SelectionChanged | GSyncModeCombo | MainWindow.Events.Settings.cs:366 |  | DialogService.ShowSafeAsync, presetService.SetGlobalFpsLimit, presetService.SetGlobalGSyncEnabled, presetService.SetGSyncMode, presetService.SetPerGameFpsLimit |
| HdrAutoToggleCombo_SelectionChanged | SelectionChanged | HdrAutoToggleCombo | MainWindow.Events.Settings.cs:1305 |  | DialogService.ShowSafeAsync, HdrToggleService.GetAllDisplays |
| HdrModsListBtn_Click | Click | HdrModsListBtn | MainWindow.Events.Settings.cs:2434 |  | ToolTipService.SetToolTip |
| HdrSelectMonitors_Click | Click |  | MainWindow.Events.Settings.cs:1312 |  | DialogService.ShowSafeAsync, HdrToggleService.GetAllDisplays, TrayIconService.Initialize |
| HdrToggle_Click | Click | DetailHdrToggleBtn | MainWindow.Events.Settings.cs:1470 |  | NvColorService.BpcToLabel, NvColorService.DynamicRangeToLabel, NvColorService.GetColorData, ToolTipService.SetToolTip |
| HideButton_Click | Click | DetailHideBtn | MainWindow.Events.Install.cs:626 |  | HdrToggleService.EnableHdr, HdrToggleService.IsHdrEnabled |
| ImportNvidiaProfiles_Click | Click |  | MainWindow.Events.Settings.cs:763 |  | DialogService.ShowProgressAsync, DialogService.ShowSafeAsync, presetService.ImportProfiles, VulkanLayerService.IsRunningAsAdmin |
| InfoButton_Click | Click | DetailRefInfoBtn | MainWindow.Events.cs:787 |  | dialogService.ShowAddonInfoDialogAsync, NexusUpdateService.ParseNexusUrl |
| InstallButton_Click | Click | DetailRdxInstallBtn | InstallEventHandler.cs:36 |  | DialogService.ShowSafeAsync |
| InstallDcButton_Click | Click | DetailDcInstallBtn | InstallEventHandler.cs:183 |  | DialogService.ShowSafeAsync, optiScalerService.InstallAsync, OsReleaseService.SupportsPinning |
| InstallDofFixButton_Click | Click | DetailDofFixInstallBtn | MainWindow.Events.Install.cs:291 |  | DialogService.ShowSafeAsync, dofFixService.CheckForUpdateAsync, dofFixService.InstallAsync, dofFixService.Uninstall |
| InstallLumaButton_Click | Click | DetailLumaInstallBtn | InstallEventHandler.cs:464 |  |  |
| InstallRefButton_Click | Click | DetailRefInstallBtn | InstallEventHandler.cs:443 |  |  |
| InstallRsButton_Click | Click | DetailRsInstallBtn | InstallEventHandler.cs:136 |  | DialogService.ShowSafeAsync |
| InstallUlButton_Click | Click | DetailUlInstallBtn | InstallEventHandler.cs:162 |  | DialogService.ShowSafeAsync, optiScalerService.InstallAsync |
| LaunchGame_Click | Click | DetailLaunchBtn | MainWindow.Events.Install.cs:632 |  | HdrToggleService.EnableHdr, HdrToggleService.IsHdrEnabled |
| LinkLumaWiki_Click | Click |  | MainWindow.Events.cs:261 |  | DialogService.ShowSafeAsync |
| LinkReLimiterGithub_Click | Click |  | MainWindow.Events.cs:273 |  | DialogService.ShowSafeAsync |
| LinkRenoDxWiki_Click | Click |  | MainWindow.Events.cs:255 |  | DialogService.ShowSafeAsync |
| LinkRhiGithub_Click | Click |  | MainWindow.Events.cs:267 |  | DialogService.ShowSafeAsync |
| LinkText_PointerEntered | PointerEntered | DetailRefStatus | MainWindow.Events.Install.cs:490 |  | DialogService.ShowSafeAsync, dofFixService.IsForceEligible |
| LinkText_PointerExited | PointerExited | DetailRefStatus | MainWindow.Events.Install.cs:499 |  | DialogService.ShowSafeAsync, dofFixService.IsForceEligible, dofFixService.Uninstall |
| LumaCogButton_Click | Click | DetailLumaIniBtn | MainWindow.Events.cs:158 |  | AuxInstallService.ApplyEngineIniCustomKeys, AuxInstallService.GetLumaReshadeIniValue, AuxInstallService.RemoveLumaReshadeIniValue, AuxInstallService.SetLumaReshadeIniValue |
| LumaToggle_Click | Click | DetailLumaToggle | InstallEventHandler.cs:514 |  | DialogService.ShowSafeAsync |
| MassDeployDcIni_Click | Click |  | MassDeployHandler.cs:117 |  | AuxInstallService.CopyDcIni, DialogService.ShowSafeAsync, optiScalerService.CopyIniToGame |
| MassDeployOsIni_Click | Click |  | MassDeployHandler.cs:158 |  | DialogService.ShowSafeAsync, optiScalerService.CopyIniToGame |
| MassDeployRsIni_Click | Click |  | MassDeployHandler.cs:25 |  | AuxInstallService.CopyUlIni, AuxInstallService.MergeRsIni, AuxInstallService.MergeRsVulkanIni, DialogService.ShowSafeAsync |
| MassDeployUlIni_Click | Click |  | MassDeployHandler.cs:76 |  | AuxInstallService.CopyDcIni, AuxInstallService.CopyUlIni, DialogService.ShowSafeAsync |
| MassDlssDeploy_Click | Click |  | MainWindow.Events.Settings.cs:205 | DlssStreamlineService |  |
| MassPresetInstall_Click | Click |  | MainWindow.Events.Settings.cs:193 | DlssStreamlineService |  |
| MotdButton_Click | Click |  | MainWindow.Events.Settings.cs:62 | UpdateLogService | dialogService.ShowMotdDialogAsync |
| NewModsButton_Click | Click | NewModsBtn | MainWindow.Events.Settings.cs:2250 |  |  |
| NexusConnectBtn_Click | Click | NexusConnectBtn | SettingsHandler.Accounts.cs:51 |  |  |
| NexusDisconnectBtn_Click | Click |  | SettingsHandler.Accounts.cs:125 |  | DialogService.ShowSafeAsync |
| NexusModsLink_Click | Click | DetailNexusModsBtn | MainWindow.Events.cs:926 |  |  |
| NxmRegisterBtn_Click | Click | NxmRegisterBtn | SettingsHandler.Accounts.cs:135 |  | DialogService.ShowSafeAsync |
| OpenAppData_Click | Click | DetailAppDataBtn | MainWindow.Events.Install.cs:941 |  | AuxInstallService.ResolveEngineIniDir, AuxInstallService.ResolveUeProjectName |
| OpenAppDataFolder_Click | Click | OpenAppDataBtn | MainWindow.Events.Settings.cs:112 |  |  |
| OpenCustomFolder_Click | Click | OpenCustomBtn | MainWindow.Events.Settings.cs:115 |  |  |
| OpenDownloadsFolder_Click | Click | OpenDownloadsBtn | MainWindow.Events.Settings.cs:118 |  |  |
| OpenFolder_Click | Click | DetailFolderBtn | MainWindow.Events.Install.cs:933 |  | AuxInstallService.ResolveEngineIniDir, AuxInstallService.ResolveUeProjectName |
| OpenLogsFolder_Click | Click | OpenLogsBtn | MainWindow.Events.Settings.cs:97 |  |  |
| OpenScreenshotFolder_Click | Click |  | MainWindow.Events.Settings.cs:2082 |  |  |
| OsDlssInputsCombo_SelectionChanged | SelectionChanged | OsDlssInputsCombo | MainWindow.Events.Settings.cs:160 | DlssStreamlineService |  |
| OsGpuCombo_SelectionChanged | SelectionChanged | OsGpuCombo | MainWindow.Events.Settings.cs:157 | DlssStreamlineService |  |
| OsHotkeyCombo_SelectionChanged | SelectionChanged | OsHotkeyCombo | MainWindow.Events.Settings.cs:154 | DlssStreamlineService |  |
| PatchNotesLink_Click | Click |  | MainWindow.Events.Settings.cs:50 | UpdateLogService | dialogService.ShowMotdDialogAsync |
| PcgwLink_Click | Click | DetailPcgwBtn | MainWindow.Events.cs:889 |  |  |
| PeakNitsAuto_Click | Click | PeakNitsAutoBtn | MainWindow.Events.Settings.cs:2104 |  |  |
| PeakNitsBox_KeyDown | KeyDown | PeakNitsBox | MainWindow.Events.Settings.cs:2137 |  | DialogService.ShowSafeAsync |
| PeakNitsBox_LostFocus | LostFocus | PeakNitsBox | MainWindow.Events.SettingsInputs.cs:128 |  |  |
| PeakNitsCog_Click | Click |  | MainWindow.Events.Settings.cs:2150 |  | DialogService.ShowSafeAsync |
| PerGameScreenshotCombo_SelectionChanged | SelectionChanged | PerGameScreenshotCombo | MainWindow.Events.Settings.cs:1371 |  | TrayIconService.ClearJumpList, TrayIconService.Initialize, TrayIconService.UpdateJumpList, TrayIconService.UpdateRecentGames |
| PreferredRefreshRateCombo_SelectionChanged | SelectionChanged | PreferredRefreshRateCombo | MainWindow.Events.Settings.cs:487 |  | DialogService.ShowSafeAsync, presetService.SetGlobalDmfgFrameCount, presetService.SetGlobalDmfgTargetFps, presetService.SetPreferredRefreshRate |
| PurgeCachedFiles_Click | Click | PurgeCacheBtn | SettingsHandler.DataTools.cs:230 |  | DialogService.ShowSafeAsync |
| RdxCogButton_Click | Click | DetailUeExtendedBtn | MainWindow.Events.Components.Rdx.cs:19 |  | AuxInstallService.ParseIni |
| RecentGamesCombo_SelectionChanged | SelectionChanged | RecentGamesCombo | MainWindow.Events.Settings.cs:1420 |  | ToolTipService.SetToolTip, TrayIconService.ClearJumpList, TrayIconService.UpdateJumpList |
| RefCogButton_Click | Click | DetailRefCogBtn | MainWindow.Events.Components.cs:284 |  |  |
| RefreshButton_Click | Click | RefreshBtn | MainWindow.Events.cs:19 |  | DialogService.ShowProgressAsync, DialogService.ShowSafeAsync |
| ResAutoToggleCombo_SelectionChanged | SelectionChanged | ResAutoToggleCombo | MainWindow.Events.Settings.cs:1505 |  | DialogService.ShowSafeAsync, NvColorService.BpcToLabel, NvColorService.DynamicRangeToLabel, NvColorService.GetColorData, NvColorService.LabelToBpc, NvColorService.LabelToDynamicRange, NvColorService.SetColorData |
| ResetAddonWatchFolder_Click | Click |  | MainWindow.Events.cs:148 |  | AuxInstallService.GetLumaReshadeIniValue, AuxInstallService.RemoveLumaReshadeIniValue, AuxInstallService.SetLumaReshadeIniValue |
| ResetAllNvidiaProfiles_Click | Click |  | MainWindow.Events.Settings.cs:1037 |  | DialogService.ShowProgressAsync, DialogService.ShowSafeAsync |
| ResetScreenshotHotkey_Click | Click |  | SettingsHandler.Hotkeys.cs:141 |  | AuxInstallService.ApplyUlOsdHotkey, AuxInstallService.ApplyUlSharedPresets, AuxInstallService.KeepDisplayModeValues, ModInstallService.GetAddonDeployPath |
| ReShadeAddonsButton_Click | Click | ReShadeAddonsItem | MainWindow.Events.cs:1298 |  | addonService.EnsureLatestAsync, DialogService.ShowSafeAsync |
| ReShadeChannelCombo_SelectionChanged | SelectionChanged | ReShadeChannelCombo | SettingsHandler.Channels.cs:101 |  | AuxInstallService.GetStagedPathForChannel, reShadeNightlyService.EnsureLatestAsync, reShadeUpdateService.EnsureLatestAsync |
| ResolutionTargetCombo_SelectionChanged | SelectionChanged | ResolutionTargetCombo | MainWindow.Events.Settings.cs:1646 |  | ToolTipService.SetToolTip |
| ResSelectMonitors_Click | Click |  | MainWindow.Events.Settings.cs:1588 |  | DialogService.ShowSafeAsync, HdrToggleService.GetAllDisplays |
| ResToggle_Click | Click | DetailResToggleBtn | MainWindow.Events.Settings.cs:1656 |  | ToolTipService.SetToolTip |
| RsCogButton_Click | Click | DetailRsIniBtn | MainWindow.Events.Components.cs:22 |  | AuxInstallService.ParseIni |
| RsVariableListUseTabsCombo_SelectionChanged | SelectionChanged | RsVariableListUseTabsCombo | MainWindow.Events.Settings.cs:1378 |  | TrayIconService.ClearJumpList, TrayIconService.Initialize, TrayIconService.UpdateJumpList, TrayIconService.UpdateRecentGames |
| SaveFilterButton_Click | Click | SaveFilterButton | MainWindow.Events.cs:1003 |  | DialogService.ShowSafeAsync |
| ScreenshotPathBox_KeyDown | KeyDown | ScreenshotPathBox | MainWindow.Events.SettingsInputs.cs:14 |  |  |
| ScreenshotPathBox_LostFocus | LostFocus | ScreenshotPathBox | MainWindow.Events.SettingsInputs.cs:21 |  | DialogService.ShowSafeAsync |
| SearchBox_TextChanged | TextChanged | SearchBox | MainWindow.Events.cs:988 |  | DialogService.ShowSafeAsync |
| SettingsBack_Click | Click | SettingsBackBtn | MainWindow.Events.Settings.cs:2231 |  |  |
| SettingsButton_Click | Click | SettingsBtn | MainWindow.Events.Settings.cs:19 | UpdateLogService | dialogService.CheckForUpdateAndReturnAsync, dialogService.ShowMotdDialogAsync, DialogService.ShowSafeAsync, dialogService.ShowUpdateDialogAsync |
| ShaderCacheSizeCombo_SelectionChanged | SelectionChanged | ShaderCacheSizeCombo | MainWindow.Events.Settings.cs:340 |  | presetService.SetGlobalFpsLimit, presetService.SetGlobalGSyncEnabled, presetService.SetGSyncMode, presetService.SetShaderCacheSize, presetService.SetShaderPrecompile |
| ShaderPrecompileCombo_SelectionChanged | SelectionChanged | ShaderPrecompileCombo | MainWindow.Events.Settings.cs:353 |  | presetService.SetGlobalFpsLimit, presetService.SetGlobalGSyncEnabled, presetService.SetGSyncMode, presetService.SetPerGameFpsLimit, presetService.SetShaderPrecompile |
| StartWithWindowsCombo_SelectionChanged | SelectionChanged | StartWithWindowsCombo | MainWindow.Events.Settings.cs:1434 |  | ToolTipService.SetToolTip |
| StatusBarVersion_Click | Click | StatusBarVersionText | MainWindow.Events.Settings.cs:22 | UpdateLogService | dialogService.CheckForUpdateAndReturnAsync, dialogService.ShowMotdDialogAsync, DialogService.ShowSafeAsync, dialogService.ShowUpdateDialogAsync |
| SupportDiscord_Click | Click |  | MainWindow.Events.cs:247 |  | DialogService.ShowSafeAsync |
| SupportGuide_Click | Click |  | MainWindow.Events.Components.cs:354 |  |  |
| SupportKofi_Click | Click |  | MainWindow.Events.Components.cs:360 |  |  |
| UlCogButton_Click | Click | DetailUlIniBtn | MainWindow.Events.Components.FrameLimiters.cs:39 |  | AuxInstallService.ParseIni, ModInstallService.GetAddonDeployPath |
| UlDlssHooksCombo_SelectionChanged | SelectionChanged | UlDlssHooksCombo | SettingsHandler.Hotkeys.cs:293 |  |  |
| UlSharedPresetsCombo_SelectionChanged | SelectionChanged | UlSharedPresetsCombo | SettingsHandler.Hotkeys.cs:284 |  |  |
| UlTargetFpsCombo_SelectionChanged | SelectionChanged | UlTargetFpsCombo | MainWindow.Events.Settings.cs:151 | DlssStreamlineService |  |
| UltraPlusLink_Click | Click | DetailUltraPlusBtn | MainWindow.Events.cs:954 |  |  |
| UninstallButton_Click | Click | DetailRdxDeleteBtn | InstallEventHandler.cs:130 |  |  |
| UninstallDcButton_Click | Click | DetailDcDeleteBtn | InstallEventHandler.cs:198 |  | DialogService.ShowSafeAsync, optiScalerService.InstallAsync, OsReleaseService.SupportsPinning, reFrameworkService.InstallPdUpscalerAsync |
| UninstallDofFixButton_Click | Click | DetailDofFixDeleteBtn | MainWindow.Events.Install.cs:337 |  | DialogService.ShowSafeAsync, dofFixService.CheckForUpdateAsync, dofFixService.GetReleaseUrl, dofFixService.Uninstall |
| UninstallLumaButton_Click | Click | DetailLumaDeleteBtn | InstallEventHandler.cs:485 |  |  |
| UninstallRefButton_Click | Click | DetailRefDeleteBtn | InstallEventHandler.cs:458 |  |  |
| UninstallRsButton_Click | Click | DetailRsDeleteBtn | InstallEventHandler.cs:151 |  | DialogService.ShowSafeAsync |
| UninstallUlButton_Click | Click | DetailUlDeleteBtn | InstallEventHandler.cs:177 |  | DialogService.ShowSafeAsync, optiScalerService.InstallAsync, OsReleaseService.SupportsPinning |
| UpdateAllButton_Click | Click | UpdateBtn | MainWindow.Events.Install.cs:19 |  | DialogService.ShowProgressAsync |
| UpdateLogButton_Click | Click | UpdateLogBtn | MainWindow.Events.Settings.cs:74 | UpdateLogService |  |
| UwFixLink_Click | Click | DetailUwFixBtn | MainWindow.Events.cs:940 |  |  |

## controls built in C# (showing first 500 of 307)

| type | label | method | file |
|---|---|---|---|
| HyperlinkButton | Place custom .addon64/.addon32 files here | ShowAsync | AddonManagerDialog.cs:56 |
| HyperlinkButton | How to use | BuildAddonRow | AddonManagerDialog.cs:214 |
| ToggleSwitch | On | BuildAddonRow | AddonManagerDialog.cs:241 |
| HyperlinkButton | Repository | BuildAddonRow | AddonManagerDialog.cs:329 |
| HyperlinkButton | Place custom .addon64/.addon32 files here | ShowAsync | AddonPopupHelper.cs:74 |
| HyperlinkButton | How to use | ShowAsync | AddonPopupHelper.cs:156 |
| ToggleSwitch | On | ShowAsync | AddonPopupHelper.cs:197 |
| ToggleSwitch | On | ShowAsync | AddonPopupHelper.cs:359 |
| Button |  | StyledButton | CogDialog.cs:432 |
| Button | ⚙ | MakeCogButton | CogDialog.cs:700 |
| TextBox | Search achievements | BuildAchievementsPanel | DetailLayoutHost.Achievements.cs:430 |
| Button | Show hidden | MakeHiddenAchToggle | DetailLayoutHost.Achievements.cs:459 |
| Button |  | EnsurePanelCogView | DetailLayoutHost.CogHosts.cs:178 |
| Button |  | MakeApplyBar | DetailLayoutHost.CogHosts.cs:351 |
| Button |  | MakeApplyBar | DetailLayoutHost.CogHosts.cs:354 |
| ToggleSwitch | On | AddOverlayRow | DetailLayoutHost.GameSettings.cs:33 |
| Button | Choose... | AddDisplayModeRow | DetailLayoutHost.GameSettings.cs:61 |
| Button |  | BuildHeader | DetailLayoutHost.Header.cs:58 |
| Button |  | BuildHeader | DetailLayoutHost.Header.cs:62 |
| Button |  | BuildHeader | DetailLayoutHost.Header.cs:67 |
| Button |  | BuildHeader | DetailLayoutHost.Header.cs:74 |
| Button |  | BuildHeader | DetailLayoutHost.Header.cs:117 |
| Button |  | BuildHeader | DetailLayoutHost.Header.cs:127 |
| Button |  | MakeToggle | DetailLayoutHost.Header.cs:185 |
| HyperlinkButton |  | MakeLink | DetailLayoutHost.Header.cs:201 |
| MenuFlyoutItem | Executable:  | ShowLaunchMenu | DetailLayoutHost.Header.cs:231 |
| MenuFlyoutItem | Launch arguments... | ShowLaunchMenu | DetailLayoutHost.Header.cs:236 |
| MenuFlyoutItem | Create desktop shortcut | ShowLaunchMenu | DetailLayoutHost.Header.cs:238 |
| Button |  | MakeInstallButton | DetailLayoutHost.Overview.cs:519 |
| Button | Open folder | RenderShots | DetailLayoutHost.Screenshots.cs:99 |
| Button |  | AddShotThumbs | DetailLayoutHost.Screenshots.cs:146 |
| Button |  | MakeShotThumb | DetailLayoutHost.Screenshots.cs:179 |
| MenuFlyoutItem |  | MakeShotMenu | DetailLayoutHost.Screenshots.cs:203 |
| Button |  | ArrangeNeuralRendering | DetailLayoutHost.Sections.cs:730 |
| Button | Info | BuildDxvkRow | DetailPanelBuilder.Extras.ApiWrappers.cs:75 |
| Button |  | BuildDxvkRow | DetailPanelBuilder.Extras.ApiWrappers.cs:100 |
| Button | ✕ | BuildDxvkRow | DetailPanelBuilder.Extras.ApiWrappers.cs:132 |
| Button | Info | BuildDgVoodooRow | DetailPanelBuilder.Extras.ApiWrappers.cs:222 |
| Button |  | BuildDgVoodooRow | DetailPanelBuilder.Extras.ApiWrappers.cs:242 |
| Button | ✕ | BuildDgVoodooRow | DetailPanelBuilder.Extras.ApiWrappers.cs:430 |
| Button | Info | BuildUalRow | DetailPanelBuilder.Extras.AsiLoaders.cs:105 |
| Button |  | BuildUalRow | DetailPanelBuilder.Extras.AsiLoaders.cs:125 |
| Button | ✕ | BuildUalRow | DetailPanelBuilder.Extras.AsiLoaders.cs:210 |
| Button |  | ShowUalDllPickerAsync | DetailPanelBuilder.Extras.AsiLoaders.cs:282 |
| Button | Info | BuildRtxEncoreRow | DetailPanelBuilder.Extras.AsiLoaders.cs:412 |
| Button |  | BuildRtxEncoreRow | DetailPanelBuilder.Extras.AsiLoaders.cs:418 |
| Button | ✕ | BuildRtxEncoreRow | DetailPanelBuilder.Extras.AsiLoaders.cs:476 |
| Button |  | ShowRtxEncoreDllPickerAsync | DetailPanelBuilder.Extras.AsiLoaders.cs:537 |
| Button | Info | BuildOsRow | DetailPanelBuilder.Extras.cs:326 |
| Button |  | BuildOsRow | DetailPanelBuilder.Extras.cs:363 |
| Button | ✕ | BuildOsRow | DetailPanelBuilder.Extras.cs:401 |
| CheckBox | Don't show this again | ShowUmfgReShadeWarningAsync | DetailPanelBuilder.Extras.FrameGen.cs:39 |
| Button | Info | BuildDlssg2030Row | DetailPanelBuilder.Extras.FrameGen.cs:137 |
| Button |  | BuildDlssg2030Row | DetailPanelBuilder.Extras.FrameGen.cs:157 |
| Button | ✕ | BuildDlssg2030Row | DetailPanelBuilder.Extras.FrameGen.cs:240 |
| Button |  | ShowDlssg2030DllPickerAsync | DetailPanelBuilder.Extras.FrameGen.cs:287 |
| Button | Info | BuildRtx40MfgRow | DetailPanelBuilder.Extras.FrameGen.cs:417 |
| Button |  | BuildRtx40MfgRow | DetailPanelBuilder.Extras.FrameGen.cs:437 |
| Button | ✕ | BuildRtx40MfgRow | DetailPanelBuilder.Extras.FrameGen.cs:608 |
| Button |  | ShowMfgDllPickerAsync | DetailPanelBuilder.Extras.FrameGen.cs:661 |
| Button | Info | BuildMfgAdaUnlockRow | DetailPanelBuilder.Extras.Unlocks.cs:85 |
| Button |  | BuildMfgAdaUnlockRow | DetailPanelBuilder.Extras.Unlocks.cs:127 |
| Button | ✕ | BuildMfgAdaUnlockRow | DetailPanelBuilder.Extras.Unlocks.cs:221 |
| Button | Info | BuildDlssEnablerRow | DetailPanelBuilder.Extras.Unlocks.cs:513 |
| Button |  | BuildDlssEnablerRow | DetailPanelBuilder.Extras.Unlocks.cs:539 |
| Button | ✕ | BuildDlssEnablerRow | DetailPanelBuilder.Extras.Unlocks.cs:626 |
| Button |  | ShowDeDllPickerAsync | DetailPanelBuilder.Extras.Unlocks.cs:678 |
| ComboBox |  | BuildNeuralRenderingSectionWithData | DetailPanelBuilder.NeuralRendering.SectionUi.cs:191 |
| ComboBox |  | BuildNeuralRenderingSectionWithData | DetailPanelBuilder.NeuralRendering.SectionUi.cs:239 |
| ComboBox |  | BuildNeuralRenderingSectionWithData | DetailPanelBuilder.NeuralRendering.SectionUi.cs:430 |
| ComboBox |  | BuildNeuralRenderingSectionWithData | DetailPanelBuilder.NeuralRendering.SectionUi.cs:646 |
| HyperlinkButton |  | BuildNeuralRenderingSectionWithData | DetailPanelBuilder.NeuralRendering.SectionUi.cs:1043 |
| Button |  | BuildNeuralRenderingSectionWithData | DetailPanelBuilder.NeuralRendering.SectionUi.cs:1115 |
| Button | ✕ | BuildNeuralRenderingSectionWithData | DetailPanelBuilder.NeuralRendering.SectionUi.cs:1124 |
| ToggleSwitch | On | BuildNeuralRenderingSectionWithData | DetailPanelBuilder.NeuralRendering.SectionUi.cs:1738 |
| ToggleSwitch | On | BuildNeuralRenderingSectionWithData | DetailPanelBuilder.NeuralRendering.SectionUi.cs:1796 |
| ToggleSwitch | On | BuildNeuralRenderingSectionWithData | DetailPanelBuilder.NeuralRendering.SectionUi.cs:1877 |
| ToggleSwitch | On | BuildNeuralRenderingSectionWithData | DetailPanelBuilder.NeuralRendering.SectionUi.cs:1945 |
| HyperlinkButton |  | BuildNeuralRenderingSectionWithData | DetailPanelBuilder.NeuralRendering.SectionUi.cs:1992 |
| TextBox | Game name | BuildOverridesPanel | DetailPanelBuilder.Overrides.cs:155 |
| TextBox | Wiki mod name | BuildOverridesPanel | DetailPanelBuilder.Overrides.cs:164 |
| Button | Reset | BuildOverridesPanel | DetailPanelBuilder.Overrides.cs:179 |
| ComboBox |  | BuildOverridesPanel | DetailPanelBuilder.Overrides.cs:229 |
| ComboBox |  | BuildOverridesPanel | DetailPanelBuilder.Overrides.cs:255 |
| ComboBox | Select OptiScaler DLL name | BuildOverridesPanel | DetailPanelBuilder.Overrides.cs:291 |
| ComboBox |  | BuildOverridesPanel | DetailPanelBuilder.Overrides.cs:595 |
| Button | Reset DLL names | BuildOverridesPanel | DetailPanelBuilder.Overrides.cs:681 |
| ComboBox |  | BuildOverridesPanel | DetailPanelBuilder.Overrides.cs:816 |
| ComboBox |  | BuildOverridesPanel | DetailPanelBuilder.Overrides.cs:1002 |
| ComboBox |  | BuildOverridesPanel | DetailPanelBuilder.Overrides.cs:1127 |
| ComboBox |  | BuildDlssColumn | DetailPanelBuilder.Overrides.Dlss.cs:123 |
| ComboBox |  | BuildDlssColumn | DetailPanelBuilder.Overrides.Dlss.cs:201 |
| ComboBox |  | BuildDlssColumn | DetailPanelBuilder.Overrides.Dlss.cs:257 |
| TextBox | 33-100 | BuildDlssColumn | DetailPanelBuilder.Overrides.Dlss.cs:282 |
| ComboBox |  | BuildDriverProfileSectionWithData | DetailPanelBuilder.Overrides.DriverSettings.cs:384 |
| ComboBox |  | BuildDriverProfileSectionWithData | DetailPanelBuilder.Overrides.DriverSettings.cs:431 |
| ComboBox |  | BuildDriverProfileSectionWithData | DetailPanelBuilder.Overrides.DriverSettings.cs:471 |
| ComboBox |  | BuildDriverProfileSectionWithData | DetailPanelBuilder.Overrides.DriverSettings.cs:519 |
| ComboBox |  | BuildDriverProfileSectionWithData | DetailPanelBuilder.Overrides.DriverSettings.cs:582 |
| ComboBox |  | BuildDriverProfileSectionWithData | DetailPanelBuilder.Overrides.DriverSettings.cs:618 |
| ComboBox |  | BuildDriverProfileSectionWithData | DetailPanelBuilder.Overrides.DriverSettings.cs:668 |
| ComboBox |  | BuildDriverProfileSectionWithData | DetailPanelBuilder.Overrides.DriverSettings.cs:695 |
| ComboBox |  | BuildDriverProfileSectionWithData | DetailPanelBuilder.Overrides.DriverSettings.cs:742 |
| TextBox | 20-1000 | BuildDriverProfileSectionWithData | DetailPanelBuilder.Overrides.DriverSettings.cs:789 |
| Button | Restore Defaults | BuildDriverProfileSectionWithData | DetailPanelBuilder.Overrides.DriverSettings.cs:827 |
| ComboBox |  | BuildDriverProfileSectionWithData | DetailPanelBuilder.Overrides.DriverSettings.cs:903 |
| ComboBox |  | BuildDriverProfileSectionWithData | DetailPanelBuilder.Overrides.DriverSettings.cs:944 |
| ComboBox |  | BuildDriverProfileSectionWithData | DetailPanelBuilder.Overrides.DriverSettings.cs:991 |
| Button | Change install folder | BuildManagementSection | DetailPanelBuilder.Overrides.Dxvk.cs:84 |
| Button | Reset install folder | BuildManagementSection | DetailPanelBuilder.Overrides.Dxvk.cs:106 |
| Button | Remove from RHI | BuildManagementSection | DetailPanelBuilder.Overrides.Dxvk.cs:131 |
| Button | Reset Overrides | BuildManagementSection | DetailPanelBuilder.Overrides.Dxvk.cs:155 |
| Button | Copy Report | BuildManagementSection | DetailPanelBuilder.Overrides.Dxvk.cs:180 |
| Button | Multi Frame Gen | BuildNvidiaProfileBody | DetailPanelBuilder.Overrides.NvidiaProfile.cs:311 |
| Button | Restore DLSS/SL | BuildNvidiaProfileBody | DetailPanelBuilder.Overrides.NvidiaProfile.cs:467 |
| Button | Quick Apply | BuildNvidiaProfileBody | DetailPanelBuilder.Overrides.NvidiaProfile.cs:530 |
| ComboBox |  | BuildRsChannelSection | DetailPanelBuilder.Overrides.RsChannel.cs:71 |
| HyperlinkButton |  | BuildRsChannelSection | DetailPanelBuilder.Overrides.RsChannel.cs:254 |
| HyperlinkButton |  | BuildRsChannelSection | DetailPanelBuilder.Overrides.RsChannel.cs:300 |
| ComboBox |  | BuildShadersAddonsSection | DetailPanelBuilder.Overrides.ShadersAddons.cs:60 |
| Button | Select ReShade Preset | BuildShadersAddonsSection | DetailPanelBuilder.Overrides.ShadersAddons.cs:185 |
| TextBox | Auto-detect (or paste path) | BuildShadersAddonsSection | DetailPanelBuilder.Overrides.ShadersAddons.cs:330 |
| TextBox | Launch arguments | BuildShadersAddonsSection | DetailPanelBuilder.Overrides.ShadersAddons.cs:351 |
| Button | Browse | BuildShadersAddonsSection | DetailPanelBuilder.Overrides.ShadersAddons.cs:386 |
| Button | Reset launch options | BuildShadersAddonsSection | DetailPanelBuilder.Overrides.ShadersAddons.cs:428 |
| Button | Reset Overrides | BuildShadersAddonsSection | DetailPanelBuilder.Overrides.ShadersAddons.cs:455 |
| CheckBox | Don't show this again | ShowSourceEngineInstallDialogAsync | DialogService.Game.cs:83 |
| Button |  | ShowPatchNotesDialogAsync | DialogService.Update.cs:397 |
| TextBox | Search notes | ShowPatchNotesDialogAsync | DialogService.Update.cs:421 |
| Button |  | ShowPatchNotesDialogAsync | DialogService.Update.cs:466 |
| ComboBox |  | BuildCombo | DlssDefaultsDialog.cs:212 |
| ComboBox |  | BuildPresetComboBox | DlssDefaultsDialog.cs:241 |
| ComboBox | 33–100 | BuildRenderScaleComboBox | DlssDefaultsDialog.cs:267 |
| TextBox | 33–100 | BuildRenderScaleComboBox | DlssDefaultsDialog.cs:280 |
| ComboBox | Select addon to install... | ProcessDroppedArchive | DragDropHandler.Addon.cs:148 |
| ComboBox |  | ProcessDroppedArchive | DragDropHandler.Addon.cs:186 |
| ComboBox | Select a game... | ProcessDroppedAddon | DragDropHandler.Addon.cs:263 |
| ComboBox |  | ProcessDroppedUrl | DragDropHandler.Addon.cs:758 |
| TextBox | Game name: | ProcessDroppedExe | DragDropHandler.Exe.cs:70 |
| ComboBox | Select a game... | ProcessDroppedPreset | DragDropHandler.Preset.cs:95 |
| TextBox | Describe the issue (optional) | ShowAndCopyAsync | GameReportEncoder.cs:49 |
| CheckBox | Don't show this again | ShowLumaRenodxCombinedWarning | InstallEventHandler.cs:549 |
| Button |  | MakeCollectionChip | MainWindow.Collections.cs:178 |
| MenuFlyoutItem | Rename… | ShowCollectionMenu | MainWindow.Collections.cs:226 |
| MenuFlyoutItem | Move left | ShowCollectionMenu | MainWindow.Collections.cs:232 |
| MenuFlyoutItem | Move right | ShowCollectionMenu | MainWindow.Collections.cs:234 |
| MenuFlyoutItem | Delete… | ShowCollectionMenu | MainWindow.Collections.cs:236 |
| MenuFlyoutItem | New collection… | BuildCollectionsMenu | MainWindow.Collections.cs:300 |
| TextBox | e.g. Racing | PromptCollectionNameAsync | MainWindow.Collections.cs:368 |
| Button |  | UpdateCustomGamesNotice | MainWindow.CustomGames.cs:43 |
| CheckBox | Game name | ShowCustomGamesReviewAsync | MainWindow.CustomGames.cs:87 |
| TextBox | Game name | ShowCustomGamesReviewAsync | MainWindow.CustomGames.cs:89 |
| ComboBox |  | ShowCustomGamesReviewAsync | MainWindow.CustomGames.cs:91 |
| Button |  | BuildTabsDisplayModeSwitch | MainWindow.DisplayModes.cs:203 |
| Button |  | BuildTabsDisplayModeSwitch | MainWindow.DisplayModes.cs:219 |
| Button |  | BuildTabsDisplayModeSwitch | MainWindow.DisplayModes.cs:226 |
| MenuFlyoutItem | Display mode settings... | BuildDisplayModeMenu | MainWindow.DisplayModes.cs:329 |
| CheckBox |  | ShowDisplayModeGameDialogAsync | MainWindow.DisplayModes.cs:423 |
| Slider |  | RtxHdrConfigButton_Click | MainWindow.Events.Components.Hdr.cs:95 |
| Slider |  | RtxHdrConfigButton_Click | MainWindow.Events.Components.Hdr.cs:109 |
| Slider |  | RtxHdrConfigButton_Click | MainWindow.Events.Components.Hdr.cs:171 |
| Slider |  | RtxHdrConfigButton_Click | MainWindow.Events.Components.Hdr.cs:229 |
| Slider |  | AutoHdrConfigButton_Click | MainWindow.Events.Components.Hdr.cs:398 |
| Button |  | MakeOsPresetPill | MainWindow.Events.Components.OptiScaler.Presets.cs:280 |
| Button |  | DonateButton_Click | MainWindow.Events.cs:395 |
| MenuFlyoutItem | Open Folder | CardMoreMenu_Click | MainWindow.Events.cs:676 |
| MenuFlyoutItem |  | CardMoreMenu_Click | MainWindow.Events.cs:685 |
| MenuFlyoutItem |  | CardMoreMenu_Click | MainWindow.Events.cs:694 |
| MenuFlyoutItem | Game notes… | CardMoreMenu_Click | MainWindow.Events.cs:710 |
| MenuFlyoutItem | Discussion / Instructions | CardMoreMenu_Click | MainWindow.Events.cs:723 |
| MenuFlyoutItem | View Notes | CardMoreMenu_Click | MainWindow.Events.cs:739 |
| Button | Create Desktop Shortcut | CardMoreMenu_Click | MainWindow.Events.cs:748 |
| MenuFlyoutItem | Create Desktop Shortcut | CardMoreMenu_Click | MainWindow.Events.cs:756 |
| TextBox | Filter name | SaveFilterButton_Click | MainWindow.Events.cs:1010 |
| Button |  | RebuildCustomFilterChips | MainWindow.Events.cs:1098 |
| MenuFlyoutItem | Delete | RebuildCustomFilterChips | MainWindow.Events.cs:1121 |
| TextBox | Enter the game name: | AddGameButton_Click | MainWindow.Events.cs:1223 |
| CheckBox | Don't show this again | EngineBadge_PointerPressed | MainWindow.Events.Install.cs:530 |
| CheckBox | Also uninstall all RHI-managed components from game folder | RemoveManualGame_Click | MainWindow.Events.Install.cs:1025 |
| TextBox | 20-1000 | FpsLimitCombo_Custom | MainWindow.Events.Settings.cs:443 |
| TextBox | 20-1000 | DmfgTargetFpsCombo_Custom | MainWindow.Events.Settings.cs:538 |
| ComboBox | Monitor | DigitalVibrance_Click | MainWindow.Events.Settings.cs:878 |
| Slider | Digital Vibrance (0 = desaturated, 50 = neutral, 100 = maximum) | DigitalVibrance_Click | MainWindow.Events.Settings.cs:901 |
| Button | Save | DigitalVibrance_Click | MainWindow.Events.Settings.cs:947 |
| Button | Reset to 50 | DigitalVibrance_Click | MainWindow.Events.Settings.cs:958 |
| CheckBox | Leave all unchecked to enable HDR on the primary display only. | HdrSelectMonitors_Click | MainWindow.Events.Settings.cs:1325 |
| CheckBox | Leave all unchecked to change resolution on the primary display only. | ResSelectMonitors_Click | MainWindow.Events.Settings.cs:1601 |
| TextBox | e.g. DLDSR 2.25x + 1.78x | DldsrCaptureBtn_Click | MainWindow.Events.Settings.cs:1889 |
| ComboBox | Apply to presets: | PeakNitsCog_Click | MainWindow.Events.Settings.cs:2166 |
| CheckBox | Preset 1 | PeakNitsCog_Click | MainWindow.Events.Settings.cs:2182 |
| CheckBox | Preset 2 | PeakNitsCog_Click | MainWindow.Events.Settings.cs:2183 |
| CheckBox | Preset 3 | PeakNitsCog_Click | MainWindow.Events.Settings.cs:2184 |
| TextBox | Search games, or type RenoDX, Luma or Ultra+ to filter… | HdrModsListBtn_Click | MainWindow.Events.Settings.cs:2441 |
| HyperlinkButton |  | HdrModsListBtn_Click | MainWindow.Events.Settings.cs:2513 |
| HyperlinkButton | Join the Ultra+ Discord (main community) | BuildFaqLinksSection | MainWindow.FaqBuilder.cs:503 |
| HyperlinkButton | RenoDX Discord (mod development) | BuildFaqLinksSection | MainWindow.FaqBuilder.cs:516 |
| HyperlinkButton | Browse the RenoDX Mod Wiki | BuildFaqLinksSection | MainWindow.FaqBuilder.cs:529 |
| HyperlinkButton | RHI GitHub — Report issues or request features | BuildFaqLinksSection | MainWindow.FaqBuilder.cs:542 |
| TextBox | Codes, routes, settings to remember… | EditGameNotesAsync | MainWindow.GameNotes.cs:20 |
| MenuFlyoutItem |  | BuildArtworkMenu | MainWindow.SpotlightChrome.cs:607 |
| Button |  | BuildTabsChrome | MainWindow.TabsChrome.cs:149 |
| MenuFlyoutItem | Global Shaders | BuildTabsTopBar | MainWindow.TabsChrome.cs:236 |
| MenuFlyoutItem | Global Addons | BuildTabsTopBar | MainWindow.TabsChrome.cs:238 |
| Button | Help | BuildTabsTopBar | MainWindow.TabsChrome.cs:254 |
| Button |  | MakeNavItem | MainWindow.TabsChrome.cs:314 |
| MenuFlyoutItem |  | ShowHelpMenu | MainWindow.TabsChrome.cs:377 |
| Button | All up to date | MakeUpdateAllButton | MainWindow.TabsChrome.cs:627 |
| Button |  | MakeUpdateAllButton | MainWindow.TabsChrome.cs:638 |
| ComboBox |  | HandleArchiveFile | MainWindow.xaml.cs:618 |
| Button | Select All | MassPresetInstall_ClickAsync | MassDeployHandler.cs:256 |
| Button | Deselect All | MassPresetInstall_ClickAsync | MassDeployHandler.cs:263 |
| CheckBox |  | MassPresetInstall_ClickAsync | MassDeployHandler.cs:277 |
| CheckBox | Select All | ShowAsync | MassDlssDeployDialog.cs:63 |
| Button | Select All | ShowAsync | MassDlssDeployDialog.cs:78 |
| Button | Deselect All | ShowAsync | MassDlssDeployDialog.cs:87 |
| CheckBox | Auto-create NVIDIA profiles | ShowAsync | MassDlssDeployDialog.cs:153 |
| ComboBox |  | BuildVersionCombo | MassDlssDeployDialog.cs:641 |
| ComboBox |  | BuildPresetCombo | MassDlssDeployDialog.cs:678 |
| CheckBox | Don't show this warning again | ShowAsync | MfgDialog.cs:51 |
| ComboBox | Frame Count | ShowConfigDialogAsync | MfgDialog.cs:120 |
| ComboBox | Target Frame Rate | ShowConfigDialogAsync | MfgDialog.cs:141 |
| ComboBox | 20-1000 | ShowConfigDialogAsync | MfgDialog.cs:160 |
| TextBox | 20-1000 | ShowConfigDialogAsync | MfgDialog.cs:172 |
| Button | Set | ShowConfigDialogAsync | MfgDialog.cs:173 |
| TextBox | Name | AskNameAsync | OsCustomBuildDialogs.cs:15 |
| CheckBox |  | ShowAsync | PresetPopupHelper.cs:116 |
| ComboBox | Off | MakeAchievementRow | SettingsHandler.Achievements.cs:129 |
| ComboBox | Off | EnsureControllerSetting | SettingsHandler.Controller.cs:44 |
| Button |  | MakeCustomFoldersButton | SettingsHandler.CustomFolders.cs:122 |
| ComboBox | Off | RefreshDisplayModesCard | SettingsHandler.DisplayModes.cs:89 |
| ComboBox | Off | RefreshDisplayModesCard | SettingsHandler.DisplayModes.cs:191 |
| Button |  | DmButton | SettingsHandler.DisplayModes.cs:244 |
| CheckBox |  | ShowDefaultItemsAsync | SettingsHandler.DisplayModes.cs:327 |
| TextBox | e.g. Handheld | PromptModeNameAsync | SettingsHandler.DisplayModes.cs:396 |
| TextBox | e.g. hotkey, HDR, ReBAR | EnsureSettingsNavigation | SettingsHandler.Navigation.cs:136 |
| Button |  | CreateNavButton | SettingsHandler.Navigation.cs:274 |
| Button |  | RenderSearchResults | SettingsHandler.Navigation.cs:943 |
| Button | Remove NVIDIA App override limits... | EnsureNvAppLimitsPanel | SettingsHandler.NvAppLimits.cs:43 |
| TextBox | Paste a download link (GitHub release file, Discord attachment, ...) | EnsureOsCustomBuildsCard | SettingsHandler.OsCustomBuilds.cs:64 |
| CheckBox | DLSS NR DLL | MakeOsCustomRow | SettingsHandler.OsCustomBuilds.cs:189 |
| MenuFlyoutItem | From the link above | MakeOsCustomRow | SettingsHandler.OsCustomBuilds.cs:210 |
| MenuFlyoutItem | From a file... | MakeOsCustomRow | SettingsHandler.OsCustomBuilds.cs:217 |
| MenuFlyoutItem | From a folder... | MakeOsCustomRow | SettingsHandler.OsCustomBuilds.cs:223 |
| ComboBox |  | EnsureOsDefaultVariantRow | SettingsHandler.OsDefaultVariant.cs:45 |
| ComboBox |  | MakeOsNightlyCleanupRow | SettingsHandler.OsDefaultVariant.cs:107 |
| ComboBox | Off | EnsureRhiOverlaySection | SettingsHandler.Overlay.cs:87 |
| ComboBox | Off | EnsureRhiOverlaySection | SettingsHandler.Overlay.cs:112 |
| TextBox |  | EnsureRhiOverlaySection | SettingsHandler.Overlay.cs:136 |
| ComboBox | Off | EnsureRhiOverlaySection | SettingsHandler.Overlay.cs:152 |
| TextBox | e.g. 165 | UlTargetFpsCombo_SelectionChanged | SettingsHandler.ReLimiterOptiScaler.cs:89 |
| CheckBox | ReShade | GlobalUpdateInclusion_ClickAsync | SettingsHandler.ReLimiterOptiScaler.cs:171 |
| CheckBox | RenoDX | GlobalUpdateInclusion_ClickAsync | SettingsHandler.ReLimiterOptiScaler.cs:172 |
| CheckBox | ReLimiter | GlobalUpdateInclusion_ClickAsync | SettingsHandler.ReLimiterOptiScaler.cs:173 |
| CheckBox | Display Commander | GlobalUpdateInclusion_ClickAsync | SettingsHandler.ReLimiterOptiScaler.cs:174 |
| CheckBox | OptiScaler | GlobalUpdateInclusion_ClickAsync | SettingsHandler.ReLimiterOptiScaler.cs:175 |
| CheckBox | RE Framework | GlobalUpdateInclusion_ClickAsync | SettingsHandler.ReLimiterOptiScaler.cs:176 |
| Button |  | EnsureRemovedGamesButton | SettingsHandler.RemovedGames.cs:31 |
| Button | Restore | ShowRemovedGamesDialogAsync | SettingsHandler.RemovedGames.cs:119 |
| Button | Tabs | MakeTabsToggle | SettingsHandler.Tabs.cs:27 |
| Button |  | MakeSettingsTab | SettingsHandler.Tabs.cs:157 |
| ComboBox |  | EnsureUiScaleRow | SettingsHandler.UiScale.cs:38 |
| ComboBox |  | AddSizeRow | SettingsHandler.UiScale.cs:115 |
| Button | Manage ReShade for me | BuildContent | SetupWindow.xaml.cs:154 |
| Button | I'll manage it myself | BuildContent | SetupWindow.xaml.cs:167 |
| Button |  | MakeExpandButton | ShaderPopupHelper.Cards.cs:111 |
| Button | Expand All | ShowAsync | ShaderPopupHelper.cs:103 |
| Button | Deselect All | ShowAsync | ShaderPopupHelper.cs:110 |
| TextBox | Search packs or shaders... | ShowAsync | ShaderPopupHelper.cs:160 |
| CheckBox |  | ShowAsync | ShaderPopupHelper.cs:264 |
| CheckBox |  | ShowAsync | ShaderPopupHelper.cs:292 |
| CheckBox |  | ShowAsync | ShaderPopupHelper.cs:419 |
| CheckBox |  | ShowAsync | ShaderPopupHelper.cs:446 |
| TextBox | Profile name | ShowAsync | ShaderPopupHelper.cs:714 |
| Button |  | ShowAsync | ShaderPopupHelper.cs:861 |
| Button | ✎ | ShowAsync | ShaderPopupHelper.cs:888 |
| TextBox |  | ShowAsync | ShaderPopupHelper.cs:900 |
| Button | X | ShowAsync | ShaderPopupHelper.cs:943 |
| Button | Save | ShowAsync | ShaderPopupHelper.cs:999 |
| Button | New | ShowAsync | ShaderPopupHelper.cs:1041 |
| Button | Export | ShowAsync | ShaderPopupHelper.cs:1106 |
| Button | Import | ShowAsync | ShaderPopupHelper.cs:1169 |
| Button | Open Custom Folder | ShowAsync | ShaderPopupHelper.cs:1271 |
| Button |  | WrapTile | TabsTheme.cs:199 |
| Button |  | MakeIconButton | TabsTheme.cs:339 |
| Button |  | MakeTile | TabsTheme.cs:398 |
| Button |  | MakeSegmented | TabsTheme.cs:617 |
| Button |  | MakeActionButton | UIFactory.cs:134 |
| Button | Choose components... | RefreshSummary | UpdateInclusionHelper.cs:93 |
| CheckBox | ReShade | RefreshSummary | UpdateInclusionHelper.cs:116 |
| CheckBox | RenoDX | RefreshSummary | UpdateInclusionHelper.cs:117 |
| CheckBox | ReLimiter | RefreshSummary | UpdateInclusionHelper.cs:118 |
| CheckBox | Display Commander | RefreshSummary | UpdateInclusionHelper.cs:119 |
| CheckBox | OptiScaler | RefreshSummary | UpdateInclusionHelper.cs:120 |
| CheckBox | DXVK | RefreshSummary | UpdateInclusionHelper.cs:122 |
| CheckBox | RE Framework | RefreshSummary | UpdateInclusionHelper.cs:125 |
| Button | Clear History | BuildRoot | UpdateLogWindow.cs:117 |
| Button | Close | BuildRoot | UpdateLogWindow.cs:164 |
| TextBox | Search achievements | BuildAchievementsPanel | OverlayWindow.Achievements.cs:56 |
| Button | Show hidden | MakeHiddenAchToggle | OverlayWindow.Achievements.cs:104 |
| Button | Back to game |  | OverlayWindow.cs:66 |
| TextBox | Codes, routes, settings to remember… Only you see these. | BuildInfoPanel | OverlayWindow.Panels.cs:314 |
| Button |  | BuildInfoPanel | OverlayWindow.Panels.cs:365 |
| Button | Open folder | RenderShotGrid | OverlayWindow.Screenshots.cs:65 |
| Button |  | AddThumbs | OverlayWindow.Screenshots.cs:111 |
| Button |  | AddThumbs | OverlayWindow.Screenshots.cs:133 |
| Button |  | OpenViewer | OverlayWindow.Screenshots.cs:166 |
| CheckBox | Don't show this warning again | InstallDxvkAsync | MainViewModel.Dxvk.cs:31 |

## persisted settings

| key | type | default | loaded |
|---|---|---|---|
| AddonWarningDismissed |  |  | True |
| AddonWatchFolder | string | "" | True |
| AutoUpdateComponents |  |  | True |
| AutoUpdateDlss |  |  | True |
| AutoUpdateStreamline |  |  | True |
| BackgroundUpdateChecks | string | "On" | True |
| BetaOptIn |  |  | True |
| CacheAllShaders | bool | true | True |
| CloseToTray |  |  | True |
| CollapsedDetailSections |  |  | True |
| ControllerSupport | bool | true | True |
| DbSourceMigrated |  |  | False |
| DefaultDlssdVersion | string | "" | True |
| DefaultDlssgVersion | string | "" | True |
| DefaultDlssnrVersion | string | "" | True |
| DefaultDlssVersion | string | "" | True |
| DefaultFgDriverOverride | bool | false | True |
| DefaultFgPreset | uint | 0 | True |
| DefaultNrPreset | uint | 0 | True |
| DefaultRrDriverOverride | bool | false | True |
| DefaultRrPreset | uint | 0 | True |
| DefaultRrRenderScale | uint | 0 | True |
| DefaultSrDriverOverride | bool | false | True |
| DefaultSrPreset | uint | 0 | True |
| DefaultSrRenderScale | uint | 0 | True |
| DefaultStreamlineVersion | string | "" | True |
| DetailSectionOrder |  |  | True |
| DetailSelectedTab | string | "Overview" | True |
| DigitalVibrance |  |  | True |
| DisplayModesEnabled |  |  | True |
| DropHelperEnabled | bool | true | True |
| DxvkVariant | string | "LiliumHdr" | True |
| DxvkWarningDismissed |  |  | True |
| EnabledGlobalAddons | List<string> | new() | True |
| EngineBadgeWarningDismissed |  |  | True |
| FirstLaunchSetupDone |  |  | True |
| GameListScale | int | 100 | True |
| GitHubOAuthToken | string | "" | True |
| GitHubUsername | string | "" | True |
| GlobalShadersOff |  |  | True |
| GlobalSkipDcUpdates |  |  | True |
| GlobalSkipOsUpdates |  |  | True |
| GlobalSkipRdxUpdates |  |  | True |
| GlobalSkipRefUpdates |  |  | True |
| GlobalSkipRsUpdates |  |  | True |
| GlobalSkipUlUpdates |  |  | True |
| HdrAutoToggle |  |  | True |
| HdrTargetDisplays | List<uint> | new() | True |
| LastKnownNewestDlss | string | "" | True |
| LastKnownNewestStreamline | string | "" | True |
| LastSeenVersion | string | "" | True |
| LastUpdateCheckUtc | string | "" | True |
| MainLayout | string | "Classic" | True |
| MfgWarningDismissed |  |  | True |
| NexusApiKey | string | "" | True |
| NexusIsPremium |  |  | True |
| NexusUsername | string | "" | True |
| OsDefaultVariant | string | "Stable" | True |
| OsDlssInputs | bool | true | True |
| OsFirstTimeWarningDismissed |  |  | True |
| OsGpuType | string | "NVIDIA" | True |
| OsHotkey | string | "Insert" | True |
| OsNightlyCleanup |  |  | True |
| OverlayHotkey | string | "36,0,0,0" | True |
| PatchNotesFontSize | double | 12 | True |
| PatchNotesNavHidden |  |  | True |
| PeakNits |  |  | True |
| PeakNitsEnabled | bool | true | True |
| PeakNitsPresets | HashSet<int> | new() { 1, 2, 3 } | True |
| PerGameScreenshotFolders |  |  | True |
| RecentGamesMenu |  |  | True |
| RecentLaunches | List<string> | new() | True |
| RenoDxDbSource | string | "DbOnly" | True |
| ReShadeChannel | string | "Stable" | True |
| ResolutionAutoToggle |  |  | True |
| ResolutionTarget | string | "" | True |
| ResTargetDisplays | List<uint> | new() | True |
| RhiOverlayController | bool | true | True |
| RhiOverlayEnabled | bool | true | True |
| RhiOverlayHotkey | string | DefaultRhiOverlayHotkey | True |
| RhiOverlayPauseGame |  |  | True |
| RsVariableListUseTabs | bool | true | True |
| ScreenshotHotkey | string | "44,0,0,0" | True |
| ScreenshotPath | string | "" | True |
| SelectedShaderPacks | List<string> | new() | True |
| SettingsLastSection | string | "General" | True |
| SettingsTabbed |  |  | True |
| ShaderDeployMode |  |  | True |
| ShowHiddenAchievements |  |  | True |
| SkipUpdateCheck |  |  | True |
| SourceEngineNoticeDismissed |  |  | True |
| SpotlightCoverScale | double | 1.0 | True |
| SpotlightHeroHeight |  |  | True |
| SpotlightShelfAtBottom |  |  | True |
| SpotlightShelfPlain |  |  | True |
| StartWithWindows |  |  | True |
| SteamAchievements |  |  | True |
| TopBarScale | int | 100 | True |
| UeExtendedWarningDismissed |  |  | True |
| UiScale | int | 100 | True |
| UlDlssHooks | bool | true | True |
| UlOsdHotkey | string | "F12" | True |
| UlSharedPresets | bool | false | True |
| UlTargetFps |  |  | True |
| UmfgReShadeWarningDismissed |  |  | True |
| UseCustomShaders |  |  | True |
| VerboseLogging |  |  | True |
| XboxAchievements |  |  | True |

## ini keys

| file | section | key | value |
|---|---|---|---|
| dxvk.conf |  | d3d9.dpiAware | True |
| dxvk.conf |  | dxgi.enableHDR | True |
| dxvk.conf |  | dxvk.allowFse | False |
| dxvk.conf |  | dxvk.latencySleep | Auto |
| OptiScaler_dlssnr.amd-dlss.ini | Anisotropy | AnisotropyOverride | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Anisotropy | ModifyComparison | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Anisotropy | ModifyMinMax | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Anisotropy | SkipPointFilter | auto |
| OptiScaler_dlssnr.amd-dlss.ini | CAS | Contrast | auto |
| OptiScaler_dlssnr.amd-dlss.ini | CAS | ContrastEnabled | auto |
| OptiScaler_dlssnr.amd-dlss.ini | CAS | DAClampOutput | auto |
| OptiScaler_dlssnr.amd-dlss.ini | CAS | DADepthBias | auto |
| OptiScaler_dlssnr.amd-dlss.ini | CAS | DADepthScale | auto |
| OptiScaler_dlssnr.amd-dlss.ini | CAS | Enabled | auto |
| OptiScaler_dlssnr.amd-dlss.ini | CAS | MotionScaleLimit | auto |
| OptiScaler_dlssnr.amd-dlss.ini | CAS | MotionSharpness | auto |
| OptiScaler_dlssnr.amd-dlss.ini | CAS | MotionSharpnessEnabled | auto |
| OptiScaler_dlssnr.amd-dlss.ini | CAS | MotionThreshold | auto |
| OptiScaler_dlssnr.amd-dlss.ini | CAS | SharpenerDebug | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSS | Enabled | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSS | RenderPresetBalanced | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSS | RenderPresetDLAA | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSS | RenderPresetForAll | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSS | RenderPresetOverride | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSS | RenderPresetPerformance | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSS | RenderPresetQuality | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSS | RenderPresetUltraPerformance | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSS | RenderPresetUltraQuality | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSS | UseGenericAppIdWithDlss | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSSD | RenderPresetBalanced | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSSD | RenderPresetDLAA | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSSD | RenderPresetForAll | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSSD | RenderPresetOverride | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSSD | RenderPresetPerformance | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSSD | RenderPresetQuality | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSSD | RenderPresetUltraPerformance | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSSD | RenderPresetUltraQuality | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSSG | AdaBlackwellKernels | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSSG | AdaMfgUnlock | false |
| OptiScaler_dlssnr.amd-dlss.ini | DLSSG | AmpereMfgHardwareBilinear | false |
| OptiScaler_dlssnr.amd-dlss.ini | DLSSG | AmpereMfgKernelImage | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSSG | AmpereMfgMaxFrames | 3 |
| OptiScaler_dlssnr.amd-dlss.ini | DLSSG | AmpereMfgUnlock | false |
| OptiScaler_dlssnr.amd-dlss.ini | DLSSG | ForceDMFG | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSSG | FramerateTargetDMFG | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSSG | InterpolationCount | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSSG | OverrideForceDMFG | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSSG | OverrideInterpolationCount | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DLSSG | UseGamesReflexMarkers | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | AutoCapture | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | AutoMask | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | ColourStrength | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | DebugView | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | DeferredDLSS | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | Enabled | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | EnvironmentColour | 1.0 |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | EnvironmentDetail | 1.0 |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | FinishedPicture | false |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | Intensity | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | LocalStructure | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | LocalTone | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | MaxRatio | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | Pass2AutoMask | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | Pass2Intensity | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | Pass2LocalStructure | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | Pass2LocalTone | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | Pass2Preset | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | Pass2SkinStructure | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | Pass2Style | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | Pass3AutoMask | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | Pass3Intensity | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | Pass3LocalStructure | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | Pass3LocalTone | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | Pass3Preset | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | Pass3SkinStructure | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | Pass3Style | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | Passes | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | Precision | 0 |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | Preset | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | ResidualAcrossRR | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | ResidualAcrossRRBlend | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | ResidualFG | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | ResidualFGApproxCamera | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | RunBeforeSR | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | ScalingDownscaler | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | ShowSkinMask | false |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | SkinColour | 1.0 |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | SkinDetail | 1.0 |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | SkinProtection | false |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | SkinStructure | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | SkinToneEnabled | true |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | Style | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | ToggleKey | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | TransferStrength | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | UnlockPasses | false |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | WhitePointScale | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DlssNr | WorkingScale | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DRS | DrsMaxOverrideEnabled | auto |
| OptiScaler_dlssnr.amd-dlss.ini | DRS | DrsMinOverrideEnabled | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Dx11withDx12 | DontUseNTShared | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Dx11withDx12 | UseDelayedInit | auto |
| OptiScaler_dlssnr.amd-dlss.ini | fakenvapi | ForceLatencyFlex | auto |
| OptiScaler_dlssnr.amd-dlss.ini | fakenvapi | ForceReflex | auto |
| OptiScaler_dlssnr.amd-dlss.ini | fakenvapi | ForceXeLL | auto |
| OptiScaler_dlssnr.amd-dlss.ini | fakenvapi | LatencyFlexMode | auto |
| OptiScaler_dlssnr.amd-dlss.ini | fakenvapi | UseFakenvapi | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | AllowedFrameAhead | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | DebugView | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | DepthValidNow | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | DisableHudless | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | DisableUI | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | DrawUIOverFG | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | Enabled | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | External | false |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | FGInput | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | FGNvngxReplacement | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | FGOutput | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | FTInput | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | HudCutoff | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | HudlessValidNow | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | ModifyBufferState | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | ModifySCIndex | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | OnlyAcceptFirstHudless | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | PreserveSwapChain | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | RectHeight | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | RectLeft | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | RectTop | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | RectWidth | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | SkipReset | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | SkipResizeBuffers | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | UIPremultipliedAlpha | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FrameGen | VelocityValidNow | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Framerate | FramerateLimit | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSR | AccAddPerFrame | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSR | CameraFar | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSR | CameraNear | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSR | DebugView | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSR | DlssReactiveMaskBias | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSR | FGIndex | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSR | Fsr4DoNotLoadAmdxc64 | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSR | Fsr4EnableWatermark | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSR | Fsr4ForceModel | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSR | Fsr4Preset | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSR | FsrAgilitySDKUpgrade | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSR | FsrNonLinearColorSpace | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSR | FsrNonLinearPQ | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSR | FsrNonLinearSRGB | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSR | HorizontalFov | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSR | MinDisOccAcc | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSR | ReactiveScale | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSR | ShadingScale | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSR | UpscalerIndex | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSR | UseFsrInputValues | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSR | UseReactiveMaskForTransparency | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSR | VelocityFactor | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSR | VerticalFov | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSRFG | AllowAsync | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSRFG | DebugPacingLines | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSRFG | DebugResetLines | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSRFG | DebugTearLines | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSRFG | EnableWatermark | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSRFG | FPTHybridSpin | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSRFG | FPTHybridSpinTime | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSRFG | FPTSafetyMarginInMs | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSRFG | FPTVarianceFactor | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSRFG | FPTWaitForSingleObjectOnFence | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSRFG | FramePacingTuning | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSRFG | UseMutexForSwapchain | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSRFGInputs | SkipConfigForHudless | auto |
| OptiScaler_dlssnr.amd-dlss.ini | FSRFGInputs | SkipDispatchForHudless | auto |
| OptiScaler_dlssnr.amd-dlss.ini | HDR | ForceHDR | auto |
| OptiScaler_dlssnr.amd-dlss.ini | HDR | SkipColorSpace | auto |
| OptiScaler_dlssnr.amd-dlss.ini | HDR | UseHDR10 | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Hooks | EarlyHooking | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Hooks | HookOriginalNvngxOnly | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Hooks | UseNtdllHooks | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Hotfix | CheckForUpdate | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Hotfix | ColorMaskResourceBarrier | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Hotfix | ColorResourceBarrier | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Hotfix | CreateD3D12DeviceForLuma | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Hotfix | DepthResourceBarrier | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Hotfix | DisableOverlays | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Hotfix | ExposureResourceBarrier | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Hotfix | ExtendedStateRestore | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Hotfix | ManualInputPolling | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Hotfix | MotionVectorResourceBarrier | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Hotfix | OutputResourceBarrier | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Hotfix | PreferDedicatedGpu | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Hotfix | PreferFirstDedicatedGpu | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Hotfix | RestoreComputeSignature | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Hotfix | RestoreGraphicSignature | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Hotfix | RoundInternalResolution | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Hotfix | SimulateWaitableObject | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Hotfix | SkipFirstFrames | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Hotfix | UsePrecompiledShaders | auto |
| OptiScaler_dlssnr.amd-dlss.ini | InitFlags | AutoExposure | auto |
| OptiScaler_dlssnr.amd-dlss.ini | InitFlags | DepthInverted | auto |
| OptiScaler_dlssnr.amd-dlss.ini | InitFlags | DisableReactiveMask | auto |
| OptiScaler_dlssnr.amd-dlss.ini | InitFlags | DisplayResolution | auto |
| OptiScaler_dlssnr.amd-dlss.ini | InitFlags | HDR | auto |
| OptiScaler_dlssnr.amd-dlss.ini | InitFlags | JitterCancellation | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Inputs | EnableDlssInputs | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Inputs | EnableFfxInputs | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Inputs | EnableFsr2Inputs | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Inputs | EnableFsr3Inputs | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Inputs | EnableHotSwapping | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Inputs | EnableXeSSInputs | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Inputs | Fsr2Pattern | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Inputs | Fsr3Pattern | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Inputs | UseFfxInputs | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Inputs | UseFsr2Dx11Inputs | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Inputs | UseFsr2Inputs | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Inputs | UseFsr2VulkanInputs | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Inputs | UseFsr3Inputs | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Libraries | FfxDx12FGPath | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Libraries | FfxDx12Path | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Libraries | FfxDx12RCPath | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Libraries | FfxDx12RRPath | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Libraries | FfxDx12SRPath | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Libraries | FfxVkPath | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Libraries | NvapiPath | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Libraries | NvngxDlssPath | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Libraries | NvngxFeaturePath | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Libraries | NvngxPath | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Libraries | OptiDllPath | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Libraries | XeFGPath | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Libraries | XeLLPath | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Libraries | XeSSDx11Path | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Libraries | XeSSPath | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Log | LogAsync | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Log | LogAsyncThreads | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Log | LogFileName | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Log | LogLevel | 2 |
| OptiScaler_dlssnr.amd-dlss.ini | Log | LogToConsole | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Log | LogToDebug | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Log | LogToFile | true |
| OptiScaler_dlssnr.amd-dlss.ini | Log | LogToNGX | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Log | OpenConsole | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Log | SingleFile | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Magnifier | BorderSize | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Magnifier | CursorOffsetX | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Magnifier | CursorOffsetY | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Magnifier | Enabled | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Magnifier | Size | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Magnifier | StaticPosX | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Magnifier | StaticPosY | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Magnifier | ZoomFactor | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | AccentColorB | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | AccentColorG | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | AccentColorR | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | BGColorA | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | BGColorB | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | BGColorG | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | BGColorR | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | DisableSplash | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | ExtendedLimits | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | FGShortcutKey | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | FontSize | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | FpsCycleShortcutKey | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | FpsOverlayAlpha | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | FpsOverlayHorizontal | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | FpsOverlayPos | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | FpsOverlayType | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | FpsScale | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | FpsShortcutKey | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | LightTheme | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | OverlayMenu | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | OverlaysUseTheme | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | Scale | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | ShortcutKey | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | ShowFps | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | TTFFontPath | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Menu | UseHQFont | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Mipmap | MipmapBiasFixedOverride | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Mipmap | MipmapBiasOverride | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Mipmap | MipmapBiasOverrideAll | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Mipmap | MipmapBiasScaleOverride | auto |
| OptiScaler_dlssnr.amd-dlss.ini | NvApi | DisableFlipMetering | auto |
| OptiScaler_dlssnr.amd-dlss.ini | NvngxFG | DisableHudless | auto |
| OptiScaler_dlssnr.amd-dlss.ini | NvngxFG | DispatchFlags | auto |
| OptiScaler_dlssnr.amd-dlss.ini | NvngxFG | MakeDepthCopy | auto |
| OptiScaler_dlssnr.amd-dlss.ini | NvngxFG | ShowDebug | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | AlwaysCaptureFSRFGSwapchain | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | AlwaysTrackHeaps | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | DepthScaleMax | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | DisableHUDFix | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | EnableDepthScale | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | HUDFix | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | HudfixDisableDI | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | HudfixDisableDII | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | HudfixDisableDispatch | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | HudfixDisableOM | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | HudfixDisableRTV | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | HudfixDisableSCR | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | HudfixDisableSGR | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | HudfixDisableSRV | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | HudfixDisableUAV | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | HUDFixDontUseSwapchainBuffers | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | HUDFixExtended | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | HUDFixImmediate | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | HUDFixRelaxedResolutionCheck | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | HUDLimit | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | MakeDepthCopy | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | MakeMVCopy | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | ResourceBlocking | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | ResourceFlip | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | ResourceFlipOffset | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OptiFG | UseShards | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OutputScaling | Downscaler | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OutputScaling | Enabled | auto |
| OptiScaler_dlssnr.amd-dlss.ini | OutputScaling | Multiplier | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Plugins | LateAsiPluginsDelay | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Plugins | LoadAsiPlugins | true |
| OptiScaler_dlssnr.amd-dlss.ini | Plugins | LoadCustomAmdxc64OnRdna2 | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Plugins | LoadReshade | true |
| OptiScaler_dlssnr.amd-dlss.ini | Plugins | LoadSpecialK | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Plugins | Path | auto |
| OptiScaler_dlssnr.amd-dlss.ini | ProcessFilter | ProcessExclusionList | auto |
| OptiScaler_dlssnr.amd-dlss.ini | ProcessFilter | TargetProcessName | auto |
| OptiScaler_dlssnr.amd-dlss.ini | QualityOverrides | QualityRatioBalanced | auto |
| OptiScaler_dlssnr.amd-dlss.ini | QualityOverrides | QualityRatioDLAA | auto |
| OptiScaler_dlssnr.amd-dlss.ini | QualityOverrides | QualityRatioOverrideEnabled | auto |
| OptiScaler_dlssnr.amd-dlss.ini | QualityOverrides | QualityRatioPerformance | auto |
| OptiScaler_dlssnr.amd-dlss.ini | QualityOverrides | QualityRatioQuality | auto |
| OptiScaler_dlssnr.amd-dlss.ini | QualityOverrides | QualityRatioUltraPerformance | auto |
| OptiScaler_dlssnr.amd-dlss.ini | QualityOverrides | QualityRatioUltraQuality | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Sharpness | OverrideSharpness | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Sharpness | Shader | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Sharpness | Sharpness | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Spoofing | D3DFeatureLevel | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Spoofing | Dxgi | false |
| OptiScaler_dlssnr.amd-dlss.ini | Spoofing | DxgiBlacklist | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Spoofing | DxgiFactoryWrapping | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Spoofing | DxgiVRAM | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Spoofing | Registry | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Spoofing | RegistryDriver | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Spoofing | SpoofedDeviceId | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Spoofing | SpoofedGPUName | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Spoofing | SpoofedVendorId | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Spoofing | SpoofHAGS | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Spoofing | StreamlineSpoofing | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Spoofing | TargetDeviceId | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Spoofing | TargetVendorId | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Spoofing | UEIntelAtomics | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Spoofing | User32 | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Spoofing | Vulkan | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Spoofing | VulkanExtensionSpoofing | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Spoofing | VulkanVRAM | auto |
| OptiScaler_dlssnr.amd-dlss.ini | UpscaleRatio | UpscaleRatioOverrideEnabled | auto |
| OptiScaler_dlssnr.amd-dlss.ini | UpscaleRatio | UpscaleRatioOverrideValue | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Upscalers | Dx11Upscaler | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Upscalers | Dx12Upscaler | auto |
| OptiScaler_dlssnr.amd-dlss.ini | Upscalers | VulkanUpscaler | auto |
| OptiScaler_dlssnr.amd-dlss.ini | V-Sync | ForceVsync | auto |
| OptiScaler_dlssnr.amd-dlss.ini | V-Sync | OverrideVsync | auto |
| OptiScaler_dlssnr.amd-dlss.ini | V-Sync | SyncInterval | auto |
| OptiScaler_dlssnr.amd-dlss.ini | XeFG | DebugView | auto |
| OptiScaler_dlssnr.amd-dlss.ini | XeFG | DepthInverted | auto |
| OptiScaler_dlssnr.amd-dlss.ini | XeFG | ForceBorderless | auto |
| OptiScaler_dlssnr.amd-dlss.ini | XeFG | HighResMV | auto |
| OptiScaler_dlssnr.amd-dlss.ini | XeFG | IgnoreInitChecks | auto |
| OptiScaler_dlssnr.amd-dlss.ini | XeFG | InterpolationCount | auto |
| OptiScaler_dlssnr.amd-dlss.ini | XeFG | JitteredMV | auto |
| OptiScaler_dlssnr.amd-dlss.ini | XeFG | UIComposition | auto |
| OptiScaler_dlssnr.amd-dlss.ini | XeSS | BuildPipelines | auto |
| OptiScaler_dlssnr.amd-dlss.ini | XeSS | CreateHeaps | auto |
| OptiScaler_dlssnr.amd-dlss.ini | XeSS | NetworkModel | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Anisotropy | AnisotropyOverride | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Anisotropy | ModifyComparison | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Anisotropy | ModifyMinMax | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Anisotropy | SkipPointFilter | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | CAS | Contrast | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | CAS | ContrastEnabled | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | CAS | DAClampOutput | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | CAS | DADepthBias | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | CAS | DADepthScale | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | CAS | Enabled | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | CAS | MotionScaleLimit | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | CAS | MotionSharpness | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | CAS | MotionSharpnessEnabled | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | CAS | MotionThreshold | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | CAS | SharpenerDebug | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSS | Enabled | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSS | RenderPresetBalanced | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSS | RenderPresetDLAA | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSS | RenderPresetForAll | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSS | RenderPresetOverride | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSS | RenderPresetPerformance | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSS | RenderPresetQuality | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSS | RenderPresetUltraPerformance | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSS | RenderPresetUltraQuality | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSS | UseGenericAppIdWithDlss | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSSD | RenderPresetBalanced | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSSD | RenderPresetDLAA | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSSD | RenderPresetForAll | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSSD | RenderPresetOverride | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSSD | RenderPresetPerformance | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSSD | RenderPresetQuality | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSSD | RenderPresetUltraPerformance | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSSD | RenderPresetUltraQuality | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSSG | AdaBlackwellKernels | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSSG | AdaMfgUnlock | false |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSSG | AmpereMfgHardwareBilinear | false |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSSG | AmpereMfgKernelImage | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSSG | AmpereMfgMaxFrames | 3 |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSSG | AmpereMfgUnlock | false |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSSG | ForceDMFG | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSSG | FramerateTargetDMFG | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSSG | InterpolationCount | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSSG | OverrideForceDMFG | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSSG | OverrideInterpolationCount | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DLSSG | UseGamesReflexMarkers | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | AutoCapture | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | AutoMask | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | ColourStrength | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | DebugView | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | DeferredDLSS | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | Enabled | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | EnvironmentColour | 1.0 |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | EnvironmentDetail | 1.0 |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | FinishedPicture | false |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | Intensity | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | LocalStructure | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | LocalTone | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | MaxRatio | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | Pass2AutoMask | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | Pass2Intensity | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | Pass2LocalStructure | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | Pass2LocalTone | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | Pass2Preset | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | Pass2SkinStructure | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | Pass2Style | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | Pass3AutoMask | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | Pass3Intensity | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | Pass3LocalStructure | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | Pass3LocalTone | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | Pass3Preset | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | Pass3SkinStructure | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | Pass3Style | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | Passes | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | Precision | 0 |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | Preset | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | ResidualAcrossRR | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | ResidualAcrossRRBlend | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | ResidualFG | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | ResidualFGApproxCamera | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | RunBeforeSR | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | ScalingDownscaler | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | ShowSkinMask | false |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | SkinColour | 1.0 |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | SkinDetail | 1.0 |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | SkinProtection | false |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | SkinStructure | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | SkinToneEnabled | true |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | Style | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | ToggleKey | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | TransferStrength | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | UnlockPasses | false |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | WhitePointScale | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DlssNr | WorkingScale | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DRS | DrsMaxOverrideEnabled | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | DRS | DrsMinOverrideEnabled | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Dx11withDx12 | DontUseNTShared | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Dx11withDx12 | UseDelayedInit | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | fakenvapi | ForceLatencyFlex | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | fakenvapi | ForceReflex | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | fakenvapi | ForceXeLL | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | fakenvapi | LatencyFlexMode | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | fakenvapi | UseFakenvapi | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | AllowedFrameAhead | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | DebugView | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | DepthValidNow | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | DisableHudless | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | DisableUI | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | DrawUIOverFG | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | Enabled | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | External | false |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | FGInput | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | FGNvngxReplacement | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | FGOutput | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | FTInput | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | HudCutoff | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | HudlessValidNow | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | ModifyBufferState | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | ModifySCIndex | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | OnlyAcceptFirstHudless | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | PreserveSwapChain | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | RectHeight | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | RectLeft | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | RectTop | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | RectWidth | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | SkipReset | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | SkipResizeBuffers | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | UIPremultipliedAlpha | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FrameGen | VelocityValidNow | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Framerate | FramerateLimit | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSR | AccAddPerFrame | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSR | CameraFar | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSR | CameraNear | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSR | DebugView | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSR | DlssReactiveMaskBias | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSR | FGIndex | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSR | Fsr4DoNotLoadAmdxc64 | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSR | Fsr4EnableWatermark | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSR | Fsr4ForceModel | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSR | Fsr4Preset | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSR | FsrAgilitySDKUpgrade | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSR | FsrNonLinearColorSpace | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSR | FsrNonLinearPQ | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSR | FsrNonLinearSRGB | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSR | HorizontalFov | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSR | MinDisOccAcc | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSR | ReactiveScale | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSR | ShadingScale | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSR | UpscalerIndex | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSR | UseFsrInputValues | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSR | UseReactiveMaskForTransparency | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSR | VelocityFactor | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSR | VerticalFov | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSRFG | AllowAsync | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSRFG | DebugPacingLines | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSRFG | DebugResetLines | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSRFG | DebugTearLines | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSRFG | EnableWatermark | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSRFG | FPTHybridSpin | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSRFG | FPTHybridSpinTime | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSRFG | FPTSafetyMarginInMs | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSRFG | FPTVarianceFactor | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSRFG | FPTWaitForSingleObjectOnFence | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSRFG | FramePacingTuning | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSRFG | UseMutexForSwapchain | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSRFGInputs | SkipConfigForHudless | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | FSRFGInputs | SkipDispatchForHudless | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | HDR | ForceHDR | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | HDR | SkipColorSpace | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | HDR | UseHDR10 | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Hooks | EarlyHooking | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Hooks | HookOriginalNvngxOnly | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Hooks | UseNtdllHooks | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Hotfix | CheckForUpdate | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Hotfix | ColorMaskResourceBarrier | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Hotfix | ColorResourceBarrier | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Hotfix | CreateD3D12DeviceForLuma | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Hotfix | DepthResourceBarrier | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Hotfix | DisableOverlays | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Hotfix | ExposureResourceBarrier | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Hotfix | ExtendedStateRestore | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Hotfix | ManualInputPolling | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Hotfix | MotionVectorResourceBarrier | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Hotfix | OutputResourceBarrier | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Hotfix | PreferDedicatedGpu | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Hotfix | PreferFirstDedicatedGpu | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Hotfix | RestoreComputeSignature | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Hotfix | RestoreGraphicSignature | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Hotfix | RoundInternalResolution | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Hotfix | SimulateWaitableObject | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Hotfix | SkipFirstFrames | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Hotfix | UsePrecompiledShaders | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | InitFlags | AutoExposure | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | InitFlags | DepthInverted | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | InitFlags | DisableReactiveMask | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | InitFlags | DisplayResolution | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | InitFlags | HDR | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | InitFlags | JitterCancellation | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Inputs | EnableDlssInputs | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Inputs | EnableFfxInputs | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Inputs | EnableFsr2Inputs | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Inputs | EnableFsr3Inputs | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Inputs | EnableHotSwapping | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Inputs | EnableXeSSInputs | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Inputs | Fsr2Pattern | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Inputs | Fsr3Pattern | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Inputs | UseFfxInputs | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Inputs | UseFsr2Dx11Inputs | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Inputs | UseFsr2Inputs | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Inputs | UseFsr2VulkanInputs | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Inputs | UseFsr3Inputs | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Libraries | FfxDx12FGPath | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Libraries | FfxDx12Path | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Libraries | FfxDx12RCPath | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Libraries | FfxDx12RRPath | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Libraries | FfxDx12SRPath | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Libraries | FfxVkPath | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Libraries | NvapiPath | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Libraries | NvngxDlssPath | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Libraries | NvngxFeaturePath | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Libraries | NvngxPath | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Libraries | OptiDllPath | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Libraries | XeFGPath | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Libraries | XeLLPath | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Libraries | XeSSDx11Path | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Libraries | XeSSPath | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Log | LogAsync | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Log | LogAsyncThreads | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Log | LogFileName | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Log | LogLevel | 2 |
| OptiScaler_dlssnr.amd-nodlss.ini | Log | LogToConsole | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Log | LogToDebug | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Log | LogToFile | true |
| OptiScaler_dlssnr.amd-nodlss.ini | Log | LogToNGX | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Log | OpenConsole | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Log | SingleFile | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Magnifier | BorderSize | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Magnifier | CursorOffsetX | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Magnifier | CursorOffsetY | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Magnifier | Enabled | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Magnifier | Size | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Magnifier | StaticPosX | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Magnifier | StaticPosY | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Magnifier | ZoomFactor | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | AccentColorB | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | AccentColorG | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | AccentColorR | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | BGColorA | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | BGColorB | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | BGColorG | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | BGColorR | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | DisableSplash | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | ExtendedLimits | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | FGShortcutKey | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | FontSize | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | FpsCycleShortcutKey | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | FpsOverlayAlpha | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | FpsOverlayHorizontal | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | FpsOverlayPos | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | FpsOverlayType | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | FpsScale | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | FpsShortcutKey | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | LightTheme | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | OverlayMenu | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | OverlaysUseTheme | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | Scale | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | ShortcutKey | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | ShowFps | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | TTFFontPath | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Menu | UseHQFont | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Mipmap | MipmapBiasFixedOverride | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Mipmap | MipmapBiasOverride | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Mipmap | MipmapBiasOverrideAll | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Mipmap | MipmapBiasScaleOverride | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | NvApi | DisableFlipMetering | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | NvngxFG | DisableHudless | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | NvngxFG | DispatchFlags | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | NvngxFG | MakeDepthCopy | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | NvngxFG | ShowDebug | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | AlwaysCaptureFSRFGSwapchain | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | AlwaysTrackHeaps | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | DepthScaleMax | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | DisableHUDFix | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | EnableDepthScale | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | HUDFix | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | HudfixDisableDI | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | HudfixDisableDII | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | HudfixDisableDispatch | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | HudfixDisableOM | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | HudfixDisableRTV | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | HudfixDisableSCR | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | HudfixDisableSGR | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | HudfixDisableSRV | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | HudfixDisableUAV | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | HUDFixDontUseSwapchainBuffers | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | HUDFixExtended | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | HUDFixImmediate | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | HUDFixRelaxedResolutionCheck | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | HUDLimit | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | MakeDepthCopy | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | MakeMVCopy | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | ResourceBlocking | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | ResourceFlip | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | ResourceFlipOffset | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OptiFG | UseShards | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OutputScaling | Downscaler | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OutputScaling | Enabled | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | OutputScaling | Multiplier | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Plugins | LateAsiPluginsDelay | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Plugins | LoadAsiPlugins | true |
| OptiScaler_dlssnr.amd-nodlss.ini | Plugins | LoadCustomAmdxc64OnRdna2 | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Plugins | LoadReshade | true |
| OptiScaler_dlssnr.amd-nodlss.ini | Plugins | LoadSpecialK | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Plugins | Path | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | ProcessFilter | ProcessExclusionList | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | ProcessFilter | TargetProcessName | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | QualityOverrides | QualityRatioBalanced | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | QualityOverrides | QualityRatioDLAA | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | QualityOverrides | QualityRatioOverrideEnabled | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | QualityOverrides | QualityRatioPerformance | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | QualityOverrides | QualityRatioQuality | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | QualityOverrides | QualityRatioUltraPerformance | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | QualityOverrides | QualityRatioUltraQuality | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Sharpness | OverrideSharpness | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Sharpness | Shader | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Sharpness | Sharpness | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Spoofing | D3DFeatureLevel | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Spoofing | Dxgi | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Spoofing | DxgiBlacklist | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Spoofing | DxgiFactoryWrapping | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Spoofing | DxgiVRAM | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Spoofing | Registry | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Spoofing | RegistryDriver | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Spoofing | SpoofedDeviceId | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Spoofing | SpoofedGPUName | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Spoofing | SpoofedVendorId | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Spoofing | SpoofHAGS | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Spoofing | StreamlineSpoofing | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Spoofing | TargetDeviceId | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Spoofing | TargetVendorId | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Spoofing | UEIntelAtomics | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Spoofing | User32 | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Spoofing | Vulkan | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Spoofing | VulkanExtensionSpoofing | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Spoofing | VulkanVRAM | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | UpscaleRatio | UpscaleRatioOverrideEnabled | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | UpscaleRatio | UpscaleRatioOverrideValue | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Upscalers | Dx11Upscaler | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Upscalers | Dx12Upscaler | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | Upscalers | VulkanUpscaler | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | V-Sync | ForceVsync | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | V-Sync | OverrideVsync | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | V-Sync | SyncInterval | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | XeFG | DebugView | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | XeFG | DepthInverted | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | XeFG | ForceBorderless | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | XeFG | HighResMV | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | XeFG | IgnoreInitChecks | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | XeFG | InterpolationCount | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | XeFG | JitteredMV | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | XeFG | UIComposition | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | XeSS | BuildPipelines | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | XeSS | CreateHeaps | auto |
| OptiScaler_dlssnr.amd-nodlss.ini | XeSS | NetworkModel | auto |
| OptiScaler_dlssnr.nvidia.ini | Anisotropy | AnisotropyOverride | auto |
| OptiScaler_dlssnr.nvidia.ini | Anisotropy | ModifyComparison | auto |
| OptiScaler_dlssnr.nvidia.ini | Anisotropy | ModifyMinMax | auto |
| OptiScaler_dlssnr.nvidia.ini | Anisotropy | SkipPointFilter | auto |
| OptiScaler_dlssnr.nvidia.ini | CAS | Contrast | auto |
| OptiScaler_dlssnr.nvidia.ini | CAS | ContrastEnabled | auto |
| OptiScaler_dlssnr.nvidia.ini | CAS | DAClampOutput | auto |
| OptiScaler_dlssnr.nvidia.ini | CAS | DADepthBias | auto |
| OptiScaler_dlssnr.nvidia.ini | CAS | DADepthScale | auto |
| OptiScaler_dlssnr.nvidia.ini | CAS | Enabled | auto |
| OptiScaler_dlssnr.nvidia.ini | CAS | MotionScaleLimit | auto |
| OptiScaler_dlssnr.nvidia.ini | CAS | MotionSharpness | auto |
| OptiScaler_dlssnr.nvidia.ini | CAS | MotionSharpnessEnabled | auto |
| OptiScaler_dlssnr.nvidia.ini | CAS | MotionThreshold | auto |
| OptiScaler_dlssnr.nvidia.ini | CAS | SharpenerDebug | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSS | Enabled | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSS | RenderPresetBalanced | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSS | RenderPresetDLAA | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSS | RenderPresetForAll | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSS | RenderPresetOverride | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSS | RenderPresetPerformance | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSS | RenderPresetQuality | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSS | RenderPresetUltraPerformance | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSS | RenderPresetUltraQuality | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSS | UseGenericAppIdWithDlss | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSSD | RenderPresetBalanced | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSSD | RenderPresetDLAA | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSSD | RenderPresetForAll | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSSD | RenderPresetOverride | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSSD | RenderPresetPerformance | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSSD | RenderPresetQuality | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSSD | RenderPresetUltraPerformance | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSSD | RenderPresetUltraQuality | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSSG | AdaBlackwellKernels | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSSG | AdaMfgUnlock | false |
| OptiScaler_dlssnr.nvidia.ini | DLSSG | AmpereMfgHardwareBilinear | false |
| OptiScaler_dlssnr.nvidia.ini | DLSSG | AmpereMfgKernelImage | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSSG | AmpereMfgMaxFrames | 3 |
| OptiScaler_dlssnr.nvidia.ini | DLSSG | AmpereMfgUnlock | false |
| OptiScaler_dlssnr.nvidia.ini | DLSSG | ForceDMFG | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSSG | FramerateTargetDMFG | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSSG | InterpolationCount | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSSG | OverrideForceDMFG | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSSG | OverrideInterpolationCount | auto |
| OptiScaler_dlssnr.nvidia.ini | DLSSG | UseGamesReflexMarkers | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | AutoCapture | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | AutoMask | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | ColourStrength | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | DebugView | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | DeferredDLSS | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | Enabled | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | EnvironmentColour | 1.0 |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | EnvironmentDetail | 1.0 |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | FinishedPicture | false |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | Intensity | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | LocalStructure | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | LocalTone | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | MaxRatio | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | Pass2AutoMask | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | Pass2Intensity | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | Pass2LocalStructure | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | Pass2LocalTone | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | Pass2Preset | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | Pass2SkinStructure | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | Pass2Style | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | Pass3AutoMask | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | Pass3Intensity | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | Pass3LocalStructure | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | Pass3LocalTone | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | Pass3Preset | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | Pass3SkinStructure | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | Pass3Style | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | Passes | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | Precision | 0 |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | Preset | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | ResidualAcrossRR | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | ResidualAcrossRRBlend | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | ResidualFG | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | ResidualFGApproxCamera | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | RunBeforeSR | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | ScalingDownscaler | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | ShowSkinMask | false |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | SkinColour | 1.0 |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | SkinDetail | 1.0 |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | SkinProtection | false |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | SkinStructure | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | SkinToneEnabled | true |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | Style | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | ToggleKey | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | TransferStrength | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | UnlockPasses | false |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | WhitePointScale | auto |
| OptiScaler_dlssnr.nvidia.ini | DlssNr | WorkingScale | auto |
| OptiScaler_dlssnr.nvidia.ini | DRS | DrsMaxOverrideEnabled | auto |
| OptiScaler_dlssnr.nvidia.ini | DRS | DrsMinOverrideEnabled | auto |
| OptiScaler_dlssnr.nvidia.ini | Dx11withDx12 | DontUseNTShared | auto |
| OptiScaler_dlssnr.nvidia.ini | Dx11withDx12 | UseDelayedInit | auto |
| OptiScaler_dlssnr.nvidia.ini | fakenvapi | ForceLatencyFlex | auto |
| OptiScaler_dlssnr.nvidia.ini | fakenvapi | ForceReflex | auto |
| OptiScaler_dlssnr.nvidia.ini | fakenvapi | ForceXeLL | auto |
| OptiScaler_dlssnr.nvidia.ini | fakenvapi | LatencyFlexMode | auto |
| OptiScaler_dlssnr.nvidia.ini | fakenvapi | UseFakenvapi | auto |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | AllowedFrameAhead | auto |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | DebugView | auto |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | DepthValidNow | auto |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | DisableHudless | auto |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | DisableUI | auto |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | DrawUIOverFG | auto |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | Enabled | auto |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | External | false |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | FGInput | auto |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | FGNvngxReplacement | auto |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | FGOutput | auto |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | FTInput | auto |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | HudCutoff | auto |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | HudlessValidNow | auto |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | ModifyBufferState | auto |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | ModifySCIndex | auto |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | OnlyAcceptFirstHudless | auto |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | PreserveSwapChain | auto |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | RectHeight | auto |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | RectLeft | auto |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | RectTop | auto |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | RectWidth | auto |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | SkipReset | auto |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | SkipResizeBuffers | auto |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | UIPremultipliedAlpha | auto |
| OptiScaler_dlssnr.nvidia.ini | FrameGen | VelocityValidNow | auto |
| OptiScaler_dlssnr.nvidia.ini | Framerate | FramerateLimit | auto |
| OptiScaler_dlssnr.nvidia.ini | FSR | AccAddPerFrame | auto |
| OptiScaler_dlssnr.nvidia.ini | FSR | CameraFar | auto |
| OptiScaler_dlssnr.nvidia.ini | FSR | CameraNear | auto |
| OptiScaler_dlssnr.nvidia.ini | FSR | DebugView | auto |
| OptiScaler_dlssnr.nvidia.ini | FSR | DlssReactiveMaskBias | auto |
| OptiScaler_dlssnr.nvidia.ini | FSR | FGIndex | auto |
| OptiScaler_dlssnr.nvidia.ini | FSR | Fsr4DoNotLoadAmdxc64 | auto |
| OptiScaler_dlssnr.nvidia.ini | FSR | Fsr4EnableWatermark | auto |
| OptiScaler_dlssnr.nvidia.ini | FSR | Fsr4ForceModel | auto |
| OptiScaler_dlssnr.nvidia.ini | FSR | Fsr4Preset | auto |
| OptiScaler_dlssnr.nvidia.ini | FSR | FsrAgilitySDKUpgrade | auto |
| OptiScaler_dlssnr.nvidia.ini | FSR | FsrNonLinearColorSpace | auto |
| OptiScaler_dlssnr.nvidia.ini | FSR | FsrNonLinearPQ | auto |
| OptiScaler_dlssnr.nvidia.ini | FSR | FsrNonLinearSRGB | auto |
| OptiScaler_dlssnr.nvidia.ini | FSR | HorizontalFov | auto |
| OptiScaler_dlssnr.nvidia.ini | FSR | MinDisOccAcc | auto |
| OptiScaler_dlssnr.nvidia.ini | FSR | ReactiveScale | auto |
| OptiScaler_dlssnr.nvidia.ini | FSR | ShadingScale | auto |
| OptiScaler_dlssnr.nvidia.ini | FSR | UpscalerIndex | auto |
| OptiScaler_dlssnr.nvidia.ini | FSR | UseFsrInputValues | auto |
| OptiScaler_dlssnr.nvidia.ini | FSR | UseReactiveMaskForTransparency | auto |
| OptiScaler_dlssnr.nvidia.ini | FSR | VelocityFactor | auto |
| OptiScaler_dlssnr.nvidia.ini | FSR | VerticalFov | auto |
| OptiScaler_dlssnr.nvidia.ini | FSRFG | AllowAsync | auto |
| OptiScaler_dlssnr.nvidia.ini | FSRFG | DebugPacingLines | auto |
| OptiScaler_dlssnr.nvidia.ini | FSRFG | DebugResetLines | auto |
| OptiScaler_dlssnr.nvidia.ini | FSRFG | DebugTearLines | auto |
| OptiScaler_dlssnr.nvidia.ini | FSRFG | EnableWatermark | auto |
| OptiScaler_dlssnr.nvidia.ini | FSRFG | FPTHybridSpin | auto |
| OptiScaler_dlssnr.nvidia.ini | FSRFG | FPTHybridSpinTime | auto |
| OptiScaler_dlssnr.nvidia.ini | FSRFG | FPTSafetyMarginInMs | auto |
| OptiScaler_dlssnr.nvidia.ini | FSRFG | FPTVarianceFactor | auto |
| OptiScaler_dlssnr.nvidia.ini | FSRFG | FPTWaitForSingleObjectOnFence | auto |
| OptiScaler_dlssnr.nvidia.ini | FSRFG | FramePacingTuning | auto |
| OptiScaler_dlssnr.nvidia.ini | FSRFG | UseMutexForSwapchain | auto |
| OptiScaler_dlssnr.nvidia.ini | FSRFGInputs | SkipConfigForHudless | auto |
| OptiScaler_dlssnr.nvidia.ini | FSRFGInputs | SkipDispatchForHudless | auto |
| OptiScaler_dlssnr.nvidia.ini | HDR | ForceHDR | auto |
| OptiScaler_dlssnr.nvidia.ini | HDR | SkipColorSpace | auto |
| OptiScaler_dlssnr.nvidia.ini | HDR | UseHDR10 | auto |
| OptiScaler_dlssnr.nvidia.ini | Hooks | EarlyHooking | auto |
| OptiScaler_dlssnr.nvidia.ini | Hooks | HookOriginalNvngxOnly | auto |
| OptiScaler_dlssnr.nvidia.ini | Hooks | UseNtdllHooks | auto |
| OptiScaler_dlssnr.nvidia.ini | Hotfix | CheckForUpdate | auto |
| OptiScaler_dlssnr.nvidia.ini | Hotfix | ColorMaskResourceBarrier | auto |
| OptiScaler_dlssnr.nvidia.ini | Hotfix | ColorResourceBarrier | auto |
| OptiScaler_dlssnr.nvidia.ini | Hotfix | CreateD3D12DeviceForLuma | auto |
| OptiScaler_dlssnr.nvidia.ini | Hotfix | DepthResourceBarrier | auto |
| OptiScaler_dlssnr.nvidia.ini | Hotfix | DisableOverlays | auto |
| OptiScaler_dlssnr.nvidia.ini | Hotfix | ExposureResourceBarrier | auto |
| OptiScaler_dlssnr.nvidia.ini | Hotfix | ExtendedStateRestore | auto |
| OptiScaler_dlssnr.nvidia.ini | Hotfix | ManualInputPolling | auto |
| OptiScaler_dlssnr.nvidia.ini | Hotfix | MotionVectorResourceBarrier | auto |
| OptiScaler_dlssnr.nvidia.ini | Hotfix | OutputResourceBarrier | auto |
| OptiScaler_dlssnr.nvidia.ini | Hotfix | PreferDedicatedGpu | auto |
| OptiScaler_dlssnr.nvidia.ini | Hotfix | PreferFirstDedicatedGpu | auto |
| OptiScaler_dlssnr.nvidia.ini | Hotfix | RestoreComputeSignature | auto |
| OptiScaler_dlssnr.nvidia.ini | Hotfix | RestoreGraphicSignature | auto |
| OptiScaler_dlssnr.nvidia.ini | Hotfix | RoundInternalResolution | auto |
| OptiScaler_dlssnr.nvidia.ini | Hotfix | SimulateWaitableObject | auto |
| OptiScaler_dlssnr.nvidia.ini | Hotfix | SkipFirstFrames | auto |
| OptiScaler_dlssnr.nvidia.ini | Hotfix | UsePrecompiledShaders | auto |
| OptiScaler_dlssnr.nvidia.ini | InitFlags | AutoExposure | auto |
| OptiScaler_dlssnr.nvidia.ini | InitFlags | DepthInverted | auto |
| OptiScaler_dlssnr.nvidia.ini | InitFlags | DisableReactiveMask | auto |
| OptiScaler_dlssnr.nvidia.ini | InitFlags | DisplayResolution | auto |
| OptiScaler_dlssnr.nvidia.ini | InitFlags | HDR | auto |
| OptiScaler_dlssnr.nvidia.ini | InitFlags | JitterCancellation | auto |
| OptiScaler_dlssnr.nvidia.ini | Inputs | EnableDlssInputs | auto |
| OptiScaler_dlssnr.nvidia.ini | Inputs | EnableFfxInputs | auto |
| OptiScaler_dlssnr.nvidia.ini | Inputs | EnableFsr2Inputs | auto |
| OptiScaler_dlssnr.nvidia.ini | Inputs | EnableFsr3Inputs | auto |
| OptiScaler_dlssnr.nvidia.ini | Inputs | EnableHotSwapping | auto |
| OptiScaler_dlssnr.nvidia.ini | Inputs | EnableXeSSInputs | auto |
| OptiScaler_dlssnr.nvidia.ini | Inputs | Fsr2Pattern | auto |
| OptiScaler_dlssnr.nvidia.ini | Inputs | Fsr3Pattern | auto |
| OptiScaler_dlssnr.nvidia.ini | Inputs | UseFfxInputs | auto |
| OptiScaler_dlssnr.nvidia.ini | Inputs | UseFsr2Dx11Inputs | auto |
| OptiScaler_dlssnr.nvidia.ini | Inputs | UseFsr2Inputs | auto |
| OptiScaler_dlssnr.nvidia.ini | Inputs | UseFsr2VulkanInputs | auto |
| OptiScaler_dlssnr.nvidia.ini | Inputs | UseFsr3Inputs | auto |
| OptiScaler_dlssnr.nvidia.ini | Libraries | FfxDx12FGPath | auto |
| OptiScaler_dlssnr.nvidia.ini | Libraries | FfxDx12Path | auto |
| OptiScaler_dlssnr.nvidia.ini | Libraries | FfxDx12RCPath | auto |
| OptiScaler_dlssnr.nvidia.ini | Libraries | FfxDx12RRPath | auto |
| OptiScaler_dlssnr.nvidia.ini | Libraries | FfxDx12SRPath | auto |
| OptiScaler_dlssnr.nvidia.ini | Libraries | FfxVkPath | auto |
| OptiScaler_dlssnr.nvidia.ini | Libraries | NvapiPath | auto |
| OptiScaler_dlssnr.nvidia.ini | Libraries | NvngxDlssPath | auto |
| OptiScaler_dlssnr.nvidia.ini | Libraries | NvngxFeaturePath | auto |
| OptiScaler_dlssnr.nvidia.ini | Libraries | NvngxPath | auto |
| OptiScaler_dlssnr.nvidia.ini | Libraries | OptiDllPath | auto |
| OptiScaler_dlssnr.nvidia.ini | Libraries | XeFGPath | auto |
| OptiScaler_dlssnr.nvidia.ini | Libraries | XeLLPath | auto |
| OptiScaler_dlssnr.nvidia.ini | Libraries | XeSSDx11Path | auto |
| OptiScaler_dlssnr.nvidia.ini | Libraries | XeSSPath | auto |
| OptiScaler_dlssnr.nvidia.ini | Log | LogAsync | auto |
| OptiScaler_dlssnr.nvidia.ini | Log | LogAsyncThreads | auto |
| OptiScaler_dlssnr.nvidia.ini | Log | LogFileName | auto |
| OptiScaler_dlssnr.nvidia.ini | Log | LogLevel | 2 |
| OptiScaler_dlssnr.nvidia.ini | Log | LogToConsole | auto |
| OptiScaler_dlssnr.nvidia.ini | Log | LogToDebug | auto |
| OptiScaler_dlssnr.nvidia.ini | Log | LogToFile | true |
| OptiScaler_dlssnr.nvidia.ini | Log | LogToNGX | auto |
| OptiScaler_dlssnr.nvidia.ini | Log | OpenConsole | auto |
| OptiScaler_dlssnr.nvidia.ini | Log | SingleFile | auto |
| OptiScaler_dlssnr.nvidia.ini | Magnifier | BorderSize | auto |
| OptiScaler_dlssnr.nvidia.ini | Magnifier | CursorOffsetX | auto |
| OptiScaler_dlssnr.nvidia.ini | Magnifier | CursorOffsetY | auto |
| OptiScaler_dlssnr.nvidia.ini | Magnifier | Enabled | auto |
| OptiScaler_dlssnr.nvidia.ini | Magnifier | Size | auto |
| OptiScaler_dlssnr.nvidia.ini | Magnifier | StaticPosX | auto |
| OptiScaler_dlssnr.nvidia.ini | Magnifier | StaticPosY | auto |
| OptiScaler_dlssnr.nvidia.ini | Magnifier | ZoomFactor | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | AccentColorB | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | AccentColorG | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | AccentColorR | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | BGColorA | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | BGColorB | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | BGColorG | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | BGColorR | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | DisableSplash | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | ExtendedLimits | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | FGShortcutKey | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | FontSize | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | FpsCycleShortcutKey | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | FpsOverlayAlpha | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | FpsOverlayHorizontal | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | FpsOverlayPos | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | FpsOverlayType | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | FpsScale | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | FpsShortcutKey | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | LightTheme | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | OverlayMenu | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | OverlaysUseTheme | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | Scale | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | ShortcutKey | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | ShowFps | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | TTFFontPath | auto |
| OptiScaler_dlssnr.nvidia.ini | Menu | UseHQFont | auto |
| OptiScaler_dlssnr.nvidia.ini | Mipmap | MipmapBiasFixedOverride | auto |
| OptiScaler_dlssnr.nvidia.ini | Mipmap | MipmapBiasOverride | auto |
| OptiScaler_dlssnr.nvidia.ini | Mipmap | MipmapBiasOverrideAll | auto |
| OptiScaler_dlssnr.nvidia.ini | Mipmap | MipmapBiasScaleOverride | auto |
| OptiScaler_dlssnr.nvidia.ini | NvApi | DisableFlipMetering | auto |
| OptiScaler_dlssnr.nvidia.ini | NvngxFG | DisableHudless | auto |
| OptiScaler_dlssnr.nvidia.ini | NvngxFG | DispatchFlags | auto |
| OptiScaler_dlssnr.nvidia.ini | NvngxFG | MakeDepthCopy | auto |
| OptiScaler_dlssnr.nvidia.ini | NvngxFG | ShowDebug | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | AlwaysCaptureFSRFGSwapchain | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | AlwaysTrackHeaps | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | DepthScaleMax | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | DisableHUDFix | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | EnableDepthScale | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | HUDFix | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | HudfixDisableDI | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | HudfixDisableDII | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | HudfixDisableDispatch | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | HudfixDisableOM | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | HudfixDisableRTV | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | HudfixDisableSCR | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | HudfixDisableSGR | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | HudfixDisableSRV | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | HudfixDisableUAV | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | HUDFixDontUseSwapchainBuffers | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | HUDFixExtended | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | HUDFixImmediate | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | HUDFixRelaxedResolutionCheck | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | HUDLimit | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | MakeDepthCopy | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | MakeMVCopy | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | ResourceBlocking | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | ResourceFlip | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | ResourceFlipOffset | auto |
| OptiScaler_dlssnr.nvidia.ini | OptiFG | UseShards | auto |
| OptiScaler_dlssnr.nvidia.ini | OutputScaling | Downscaler | auto |
| OptiScaler_dlssnr.nvidia.ini | OutputScaling | Enabled | auto |
| OptiScaler_dlssnr.nvidia.ini | OutputScaling | Multiplier | auto |
| OptiScaler_dlssnr.nvidia.ini | Plugins | LateAsiPluginsDelay | auto |
| OptiScaler_dlssnr.nvidia.ini | Plugins | LoadAsiPlugins | true |
| OptiScaler_dlssnr.nvidia.ini | Plugins | LoadCustomAmdxc64OnRdna2 | auto |
| OptiScaler_dlssnr.nvidia.ini | Plugins | LoadReshade | true |
| OptiScaler_dlssnr.nvidia.ini | Plugins | LoadSpecialK | auto |
| OptiScaler_dlssnr.nvidia.ini | Plugins | Path | auto |
| OptiScaler_dlssnr.nvidia.ini | ProcessFilter | ProcessExclusionList | auto |
| OptiScaler_dlssnr.nvidia.ini | ProcessFilter | TargetProcessName | auto |
| OptiScaler_dlssnr.nvidia.ini | QualityOverrides | QualityRatioBalanced | auto |
| OptiScaler_dlssnr.nvidia.ini | QualityOverrides | QualityRatioDLAA | auto |
| OptiScaler_dlssnr.nvidia.ini | QualityOverrides | QualityRatioOverrideEnabled | auto |
| OptiScaler_dlssnr.nvidia.ini | QualityOverrides | QualityRatioPerformance | auto |
| OptiScaler_dlssnr.nvidia.ini | QualityOverrides | QualityRatioQuality | auto |
| OptiScaler_dlssnr.nvidia.ini | QualityOverrides | QualityRatioUltraPerformance | auto |
| OptiScaler_dlssnr.nvidia.ini | QualityOverrides | QualityRatioUltraQuality | auto |
| OptiScaler_dlssnr.nvidia.ini | Sharpness | OverrideSharpness | auto |
| OptiScaler_dlssnr.nvidia.ini | Sharpness | Shader | auto |
| OptiScaler_dlssnr.nvidia.ini | Sharpness | Sharpness | auto |
| OptiScaler_dlssnr.nvidia.ini | Spoofing | D3DFeatureLevel | auto |
| OptiScaler_dlssnr.nvidia.ini | Spoofing | Dxgi | false |
| OptiScaler_dlssnr.nvidia.ini | Spoofing | DxgiBlacklist | auto |
| OptiScaler_dlssnr.nvidia.ini | Spoofing | DxgiFactoryWrapping | auto |
| OptiScaler_dlssnr.nvidia.ini | Spoofing | DxgiVRAM | auto |
| OptiScaler_dlssnr.nvidia.ini | Spoofing | Registry | auto |
| OptiScaler_dlssnr.nvidia.ini | Spoofing | RegistryDriver | auto |
| OptiScaler_dlssnr.nvidia.ini | Spoofing | SpoofedDeviceId | auto |
| OptiScaler_dlssnr.nvidia.ini | Spoofing | SpoofedGPUName | auto |
| OptiScaler_dlssnr.nvidia.ini | Spoofing | SpoofedVendorId | auto |
| OptiScaler_dlssnr.nvidia.ini | Spoofing | SpoofHAGS | auto |
| OptiScaler_dlssnr.nvidia.ini | Spoofing | StreamlineSpoofing | auto |
| OptiScaler_dlssnr.nvidia.ini | Spoofing | TargetDeviceId | auto |
| OptiScaler_dlssnr.nvidia.ini | Spoofing | TargetVendorId | auto |
| OptiScaler_dlssnr.nvidia.ini | Spoofing | UEIntelAtomics | auto |
| OptiScaler_dlssnr.nvidia.ini | Spoofing | User32 | auto |
| OptiScaler_dlssnr.nvidia.ini | Spoofing | Vulkan | auto |
| OptiScaler_dlssnr.nvidia.ini | Spoofing | VulkanExtensionSpoofing | auto |
| OptiScaler_dlssnr.nvidia.ini | Spoofing | VulkanVRAM | auto |
| OptiScaler_dlssnr.nvidia.ini | UpscaleRatio | UpscaleRatioOverrideEnabled | auto |
| OptiScaler_dlssnr.nvidia.ini | UpscaleRatio | UpscaleRatioOverrideValue | auto |
| OptiScaler_dlssnr.nvidia.ini | Upscalers | Dx11Upscaler | auto |
| OptiScaler_dlssnr.nvidia.ini | Upscalers | Dx12Upscaler | auto |
| OptiScaler_dlssnr.nvidia.ini | Upscalers | VulkanUpscaler | auto |
| OptiScaler_dlssnr.nvidia.ini | V-Sync | ForceVsync | auto |
| OptiScaler_dlssnr.nvidia.ini | V-Sync | OverrideVsync | auto |
| OptiScaler_dlssnr.nvidia.ini | V-Sync | SyncInterval | auto |
| OptiScaler_dlssnr.nvidia.ini | XeFG | DebugView | auto |
| OptiScaler_dlssnr.nvidia.ini | XeFG | DepthInverted | auto |
| OptiScaler_dlssnr.nvidia.ini | XeFG | ForceBorderless | auto |
| OptiScaler_dlssnr.nvidia.ini | XeFG | HighResMV | auto |
| OptiScaler_dlssnr.nvidia.ini | XeFG | IgnoreInitChecks | auto |
| OptiScaler_dlssnr.nvidia.ini | XeFG | InterpolationCount | auto |
| OptiScaler_dlssnr.nvidia.ini | XeFG | JitteredMV | auto |
| OptiScaler_dlssnr.nvidia.ini | XeFG | UIComposition | auto |
| OptiScaler_dlssnr.nvidia.ini | XeSS | BuildPipelines | auto |
| OptiScaler_dlssnr.nvidia.ini | XeSS | CreateHeaps | auto |
| OptiScaler_dlssnr.nvidia.ini | XeSS | NetworkModel | auto |
| OptiScaler_f5.amd-dlss.ini | Anisotropy | AnisotropyOverride | auto |
| OptiScaler_f5.amd-dlss.ini | Anisotropy | ModifyComparison | auto |
| OptiScaler_f5.amd-dlss.ini | Anisotropy | ModifyMinMax | auto |
| OptiScaler_f5.amd-dlss.ini | Anisotropy | SkipPointFilter | auto |
| OptiScaler_f5.amd-dlss.ini | CAS | Contrast | auto |
| OptiScaler_f5.amd-dlss.ini | CAS | ContrastEnabled | auto |
| OptiScaler_f5.amd-dlss.ini | CAS | DAClampOutput | auto |
| OptiScaler_f5.amd-dlss.ini | CAS | DADepthBias | auto |
| OptiScaler_f5.amd-dlss.ini | CAS | DADepthScale | auto |
| OptiScaler_f5.amd-dlss.ini | CAS | Enabled | auto |
| OptiScaler_f5.amd-dlss.ini | CAS | MotionScaleLimit | auto |
| OptiScaler_f5.amd-dlss.ini | CAS | MotionSharpness | auto |
| OptiScaler_f5.amd-dlss.ini | CAS | MotionSharpnessEnabled | auto |
| OptiScaler_f5.amd-dlss.ini | CAS | MotionThreshold | auto |
| OptiScaler_f5.amd-dlss.ini | CAS | SharpenerDebug | auto |
| OptiScaler_f5.amd-dlss.ini | DLSS | Enabled | auto |
| OptiScaler_f5.amd-dlss.ini | DLSS | RenderPresetBalanced | auto |
| OptiScaler_f5.amd-dlss.ini | DLSS | RenderPresetDLAA | auto |
| OptiScaler_f5.amd-dlss.ini | DLSS | RenderPresetForAll | auto |
| OptiScaler_f5.amd-dlss.ini | DLSS | RenderPresetOverride | auto |
| OptiScaler_f5.amd-dlss.ini | DLSS | RenderPresetPerformance | auto |
| OptiScaler_f5.amd-dlss.ini | DLSS | RenderPresetQuality | auto |
| OptiScaler_f5.amd-dlss.ini | DLSS | RenderPresetUltraPerformance | auto |
| OptiScaler_f5.amd-dlss.ini | DLSS | RenderPresetUltraQuality | auto |
| OptiScaler_f5.amd-dlss.ini | DLSS | UseGenericAppIdWithDlss | auto |
| OptiScaler_f5.amd-dlss.ini | DLSSD | RenderPresetBalanced | auto |
| OptiScaler_f5.amd-dlss.ini | DLSSD | RenderPresetDLAA | auto |
| OptiScaler_f5.amd-dlss.ini | DLSSD | RenderPresetForAll | auto |
| OptiScaler_f5.amd-dlss.ini | DLSSD | RenderPresetOverride | auto |
| OptiScaler_f5.amd-dlss.ini | DLSSD | RenderPresetPerformance | auto |
| OptiScaler_f5.amd-dlss.ini | DLSSD | RenderPresetQuality | auto |
| OptiScaler_f5.amd-dlss.ini | DLSSD | RenderPresetUltraPerformance | auto |
| OptiScaler_f5.amd-dlss.ini | DLSSD | RenderPresetUltraQuality | auto |
| OptiScaler_f5.amd-dlss.ini | DLSSG | AdaBlackwellKernels | auto |
| OptiScaler_f5.amd-dlss.ini | DLSSG | AdaFlipMeteringPatch | false |
| OptiScaler_f5.amd-dlss.ini | DLSSG | AdaMfgUnlock | false |
| OptiScaler_f5.amd-dlss.ini | DLSSG | AdaTemporalFix | auto |
| OptiScaler_f5.amd-dlss.ini | DLSSG | AmpereMfgHardwareBilinear | false |
| OptiScaler_f5.amd-dlss.ini | DLSSG | AmpereMfgKernelImage | auto |
| OptiScaler_f5.amd-dlss.ini | DLSSG | AmpereMfgMaxFrames | 3 |
| OptiScaler_f5.amd-dlss.ini | DLSSG | AmpereMfgUnlock | false |
| OptiScaler_f5.amd-dlss.ini | DLSSG | ForceDMFG | auto |
| OptiScaler_f5.amd-dlss.ini | DLSSG | FramerateTargetDMFG | auto |
| OptiScaler_f5.amd-dlss.ini | DLSSG | InterpolationCount | auto |
| OptiScaler_f5.amd-dlss.ini | DLSSG | OverrideForceDMFG | auto |
| OptiScaler_f5.amd-dlss.ini | DLSSG | OverrideInterpolationCount | auto |
| OptiScaler_f5.amd-dlss.ini | DLSSG | UseGamesReflexMarkers | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | AutoCapture | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | AutoExposureFollowGame | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | AutoMask | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | ColourEncoding | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | ColourStrength | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | DebugView | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | DeferredDLSS | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | DetailReuse | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | DetailReuseClipFalloff | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | DetailReuseClipGamma | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | DetailReuseDebug | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | DetailReuseDepthTolerance | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | DetailReuseFill | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | DetailReuseMaxDropped | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | DetailReuseMinFps | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | DetailReuseSigmaFloor | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | DetailReuseSteady | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | DetailReuseWithFG | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | Enabled | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | EnvironmentColour | 1.0 |
| OptiScaler_f5.amd-dlss.ini | DlssNr | EnvironmentDetail | 1.0 |
| OptiScaler_f5.amd-dlss.ini | DlssNr | FinishedPicture | false |
| OptiScaler_f5.amd-dlss.ini | DlssNr | FrameStats | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | GameExposureScale | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | Intensity | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | KernelProfile | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | LocalStructure | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | LocalTone | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | LutFile |  |
| OptiScaler_f5.amd-dlss.ini | DlssNr | LutStrength | 1 |
| OptiScaler_f5.amd-dlss.ini | DlssNr | MaxRatio | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | ModelResolutionAuto | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | NativeDebugView | false |
| OptiScaler_f5.amd-dlss.ini | DlssNr | NativeDepthFinder | false |
| OptiScaler_f5.amd-dlss.ini | DlssNr | NativeDepthOverlay | false |
| OptiScaler_f5.amd-dlss.ini | DlssNr | NativeDepthVkCopyUsage | true |
| OptiScaler_f5.amd-dlss.ini | DlssNr | NativeDepthWarmupFrames | 300 |
| OptiScaler_f5.amd-dlss.ini | DlssNr | NativeDxvkVulkan | false |
| OptiScaler_f5.amd-dlss.ini | DlssNr | NativeFrameGenerationOnly | false |
| OptiScaler_f5.amd-dlss.ini | DlssNr | NativeInput | false |
| OptiScaler_f5.amd-dlss.ini | DlssNr | NativeLowLatency | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | NativeLowLatencyBoost | false |
| OptiScaler_f5.amd-dlss.ini | DlssNr | NativeMotion | false |
| OptiScaler_f5.amd-dlss.ini | DlssNr | NativeUpscaler | false |
| OptiScaler_f5.amd-dlss.ini | DlssNr | Pass2AutoMask | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | Pass2Intensity | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | Pass2LocalStructure | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | Pass2LocalTone | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | Pass2Preset | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | Pass2SkinStructure | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | Pass2Style | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | Pass3AutoMask | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | Pass3Intensity | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | Pass3LocalStructure | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | Pass3LocalTone | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | Pass3Preset | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | Pass3SkinStructure | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | Pass3Style | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | Passes | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | Precision | 0 |
| OptiScaler_f5.amd-dlss.ini | DlssNr | Preset | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | ReversibleMode | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | RunBeforeSR | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | ScalingDownscaler | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | SceneCut | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | ShowSkinMask | false |
| OptiScaler_f5.amd-dlss.ini | DlssNr | SkinColour | 1.0 |
| OptiScaler_f5.amd-dlss.ini | DlssNr | SkinDetail | 1.0 |
| OptiScaler_f5.amd-dlss.ini | DlssNr | SkinProtection | false |
| OptiScaler_f5.amd-dlss.ini | DlssNr | SkinStructure | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | SkinToneEnabled | true |
| OptiScaler_f5.amd-dlss.ini | DlssNr | SpatialCenterX | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | SpatialCenterY | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | SpatialCompression | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | SpatialOffsetX | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | SpatialOffsetY | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | SpatialShiftX | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | SpatialShiftY | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | SpatialWorkX | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | SpatialWorkY | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | Style | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | ToggleKey | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | TransferStrength | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | UnlockPasses | false |
| OptiScaler_f5.amd-dlss.ini | DlssNr | VitEvery | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | VitEveryPlain | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | WhitePointScale | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | WhitePointSource | auto |
| OptiScaler_f5.amd-dlss.ini | DlssNr | WorkingScale | auto |
| OptiScaler_f5.amd-dlss.ini | DRS | DrsMaxOverrideEnabled | auto |
| OptiScaler_f5.amd-dlss.ini | DRS | DrsMinOverrideEnabled | auto |
| OptiScaler_f5.amd-dlss.ini | Dx11withDx12 | DontUseNTShared | auto |
| OptiScaler_f5.amd-dlss.ini | Dx11withDx12 | UseDelayedInit | auto |
| OptiScaler_f5.amd-dlss.ini | fakenvapi | ForceLatencyFlex | auto |
| OptiScaler_f5.amd-dlss.ini | fakenvapi | ForceReflex | auto |
| OptiScaler_f5.amd-dlss.ini | fakenvapi | ForceXeLL | auto |
| OptiScaler_f5.amd-dlss.ini | fakenvapi | LatencyFlexMode | auto |
| OptiScaler_f5.amd-dlss.ini | fakenvapi | UseFakenvapi | auto |
| OptiScaler_f5.amd-dlss.ini | FrameGen | AllowedFrameAhead | auto |
| OptiScaler_f5.amd-dlss.ini | FrameGen | DebugView | auto |
| OptiScaler_f5.amd-dlss.ini | FrameGen | DepthValidNow | auto |
| OptiScaler_f5.amd-dlss.ini | FrameGen | DisableHudless | auto |
| OptiScaler_f5.amd-dlss.ini | FrameGen | DisableUI | auto |
| OptiScaler_f5.amd-dlss.ini | FrameGen | DrawUIOverFG | auto |
| OptiScaler_f5.amd-dlss.ini | FrameGen | Enabled | auto |
| OptiScaler_f5.amd-dlss.ini | FrameGen | External | false |
| OptiScaler_f5.amd-dlss.ini | FrameGen | FGInput | auto |
| OptiScaler_f5.amd-dlss.ini | FrameGen | FGNvngxReplacement | auto |
| OptiScaler_f5.amd-dlss.ini | FrameGen | FGOutput | auto |
| OptiScaler_f5.amd-dlss.ini | FrameGen | FTInput | auto |
| OptiScaler_f5.amd-dlss.ini | FrameGen | HudCutoff | auto |
| OptiScaler_f5.amd-dlss.ini | FrameGen | HudlessValidNow | auto |
| OptiScaler_f5.amd-dlss.ini | FrameGen | ModifyBufferState | auto |
| OptiScaler_f5.amd-dlss.ini | FrameGen | ModifySCIndex | auto |
| OptiScaler_f5.amd-dlss.ini | FrameGen | OnlyAcceptFirstHudless | auto |
| OptiScaler_f5.amd-dlss.ini | FrameGen | PreserveSwapChain | auto |
| OptiScaler_f5.amd-dlss.ini | FrameGen | RectHeight | auto |
| OptiScaler_f5.amd-dlss.ini | FrameGen | RectLeft | auto |
| OptiScaler_f5.amd-dlss.ini | FrameGen | RectTop | auto |
| OptiScaler_f5.amd-dlss.ini | FrameGen | RectWidth | auto |
| OptiScaler_f5.amd-dlss.ini | FrameGen | SkipReset | auto |
| OptiScaler_f5.amd-dlss.ini | FrameGen | SkipResizeBuffers | auto |
| OptiScaler_f5.amd-dlss.ini | FrameGen | StreamlineIgnoreOTA | false |
| OptiScaler_f5.amd-dlss.ini | FrameGen | UIPremultipliedAlpha | auto |
| OptiScaler_f5.amd-dlss.ini | FrameGen | VelocityValidNow | auto |
| OptiScaler_f5.amd-dlss.ini | Framerate | FramerateLimit | auto |
| OptiScaler_f5.amd-dlss.ini | FSR | AccAddPerFrame | auto |
| OptiScaler_f5.amd-dlss.ini | FSR | CameraFar | auto |
| OptiScaler_f5.amd-dlss.ini | FSR | CameraNear | auto |
| OptiScaler_f5.amd-dlss.ini | FSR | DebugView | auto |
| OptiScaler_f5.amd-dlss.ini | FSR | DlssReactiveMaskBias | auto |
| OptiScaler_f5.amd-dlss.ini | FSR | FGIndex | auto |
| OptiScaler_f5.amd-dlss.ini | FSR | Fsr4DoNotLoadAmdxc64 | auto |
| OptiScaler_f5.amd-dlss.ini | FSR | Fsr4EnableWatermark | auto |
| OptiScaler_f5.amd-dlss.ini | FSR | Fsr4ForceModel | auto |
| OptiScaler_f5.amd-dlss.ini | FSR | Fsr4Preset | auto |
| OptiScaler_f5.amd-dlss.ini | FSR | FsrAgilitySDKUpgrade | auto |
| OptiScaler_f5.amd-dlss.ini | FSR | FsrNonLinearColorSpace | auto |
| OptiScaler_f5.amd-dlss.ini | FSR | FsrNonLinearPQ | auto |
| OptiScaler_f5.amd-dlss.ini | FSR | FsrNonLinearSRGB | auto |
| OptiScaler_f5.amd-dlss.ini | FSR | HorizontalFov | auto |
| OptiScaler_f5.amd-dlss.ini | FSR | MinDisOccAcc | auto |
| OptiScaler_f5.amd-dlss.ini | FSR | ReactiveScale | auto |
| OptiScaler_f5.amd-dlss.ini | FSR | ShadingScale | auto |
| OptiScaler_f5.amd-dlss.ini | FSR | UpscalerIndex | auto |
| OptiScaler_f5.amd-dlss.ini | FSR | UseFsrInputValues | auto |
| OptiScaler_f5.amd-dlss.ini | FSR | UseReactiveMaskForTransparency | auto |
| OptiScaler_f5.amd-dlss.ini | FSR | VelocityFactor | auto |
| OptiScaler_f5.amd-dlss.ini | FSR | VerticalFov | auto |
| OptiScaler_f5.amd-dlss.ini | FSRFG | AllowAsync | auto |
| OptiScaler_f5.amd-dlss.ini | FSRFG | DebugPacingLines | auto |
| OptiScaler_f5.amd-dlss.ini | FSRFG | DebugResetLines | auto |
| OptiScaler_f5.amd-dlss.ini | FSRFG | DebugTearLines | auto |
| OptiScaler_f5.amd-dlss.ini | FSRFG | EnableWatermark | auto |
| OptiScaler_f5.amd-dlss.ini | FSRFG | FPTHybridSpin | auto |
| OptiScaler_f5.amd-dlss.ini | FSRFG | FPTHybridSpinTime | auto |
| OptiScaler_f5.amd-dlss.ini | FSRFG | FPTSafetyMarginInMs | auto |
| OptiScaler_f5.amd-dlss.ini | FSRFG | FPTVarianceFactor | auto |
| OptiScaler_f5.amd-dlss.ini | FSRFG | FPTWaitForSingleObjectOnFence | auto |
| OptiScaler_f5.amd-dlss.ini | FSRFG | FramePacingTuning | auto |
| OptiScaler_f5.amd-dlss.ini | FSRFG | UseMutexForSwapchain | auto |
| OptiScaler_f5.amd-dlss.ini | FSRFGInputs | SkipConfigForHudless | auto |
| OptiScaler_f5.amd-dlss.ini | FSRFGInputs | SkipDispatchForHudless | auto |
| OptiScaler_f5.amd-dlss.ini | HDR | ForceHDR | auto |
| OptiScaler_f5.amd-dlss.ini | HDR | SkipColorSpace | auto |
| OptiScaler_f5.amd-dlss.ini | HDR | UseHDR10 | auto |
| OptiScaler_f5.amd-dlss.ini | Hooks | D3D11FeatureLevelElevation | auto |
| OptiScaler_f5.amd-dlss.ini | Hooks | EarlyHooking | auto |
| OptiScaler_f5.amd-dlss.ini | Hooks | HookOriginalNvngxOnly | auto |
| OptiScaler_f5.amd-dlss.ini | Hooks | UseNtdllHooks | auto |
| OptiScaler_f5.amd-dlss.ini | Hotfix | CheckForUpdate | auto |
| OptiScaler_f5.amd-dlss.ini | Hotfix | ColorMaskResourceBarrier | auto |
| OptiScaler_f5.amd-dlss.ini | Hotfix | ColorResourceBarrier | auto |
| OptiScaler_f5.amd-dlss.ini | Hotfix | CreateD3D12DeviceForLuma | auto |
| OptiScaler_f5.amd-dlss.ini | Hotfix | DepthResourceBarrier | auto |
| OptiScaler_f5.amd-dlss.ini | Hotfix | DisableOverlays | auto |
| OptiScaler_f5.amd-dlss.ini | Hotfix | ExposureResourceBarrier | auto |
| OptiScaler_f5.amd-dlss.ini | Hotfix | ExtendedStateRestore | auto |
| OptiScaler_f5.amd-dlss.ini | Hotfix | ManualInputPolling | auto |
| OptiScaler_f5.amd-dlss.ini | Hotfix | MotionVectorResourceBarrier | auto |
| OptiScaler_f5.amd-dlss.ini | Hotfix | OutputResourceBarrier | auto |
| OptiScaler_f5.amd-dlss.ini | Hotfix | PreferDedicatedGpu | auto |
| OptiScaler_f5.amd-dlss.ini | Hotfix | PreferFirstDedicatedGpu | auto |
| OptiScaler_f5.amd-dlss.ini | Hotfix | REFrameworkCompat | auto |
| OptiScaler_f5.amd-dlss.ini | Hotfix | RestoreComputeSignature | auto |
| OptiScaler_f5.amd-dlss.ini | Hotfix | RestoreGraphicSignature | auto |
| OptiScaler_f5.amd-dlss.ini | Hotfix | RoundInternalResolution | auto |
| OptiScaler_f5.amd-dlss.ini | Hotfix | SimulateWaitableObject | auto |
| OptiScaler_f5.amd-dlss.ini | Hotfix | SkipFirstFrames | auto |
| OptiScaler_f5.amd-dlss.ini | Hotfix | UsePrecompiledShaders | auto |
| OptiScaler_f5.amd-dlss.ini | InitFlags | AutoExposure | auto |
| OptiScaler_f5.amd-dlss.ini | InitFlags | DepthInverted | auto |
| OptiScaler_f5.amd-dlss.ini | InitFlags | DisableReactiveMask | auto |
| OptiScaler_f5.amd-dlss.ini | InitFlags | DisplayResolution | auto |
| OptiScaler_f5.amd-dlss.ini | InitFlags | HDR | auto |
| OptiScaler_f5.amd-dlss.ini | InitFlags | JitterCancellation | auto |
| OptiScaler_f5.amd-dlss.ini | Inputs | EnableDlssInputs | auto |
| OptiScaler_f5.amd-dlss.ini | Inputs | EnableFfxInputs | auto |
| OptiScaler_f5.amd-dlss.ini | Inputs | EnableFsr2Inputs | auto |
| OptiScaler_f5.amd-dlss.ini | Inputs | EnableFsr3Inputs | auto |
| OptiScaler_f5.amd-dlss.ini | Inputs | EnableHotSwapping | auto |
| OptiScaler_f5.amd-dlss.ini | Inputs | EnableXeSSInputs | auto |
| OptiScaler_f5.amd-dlss.ini | Inputs | Fsr2Pattern | auto |
| OptiScaler_f5.amd-dlss.ini | Inputs | Fsr3Pattern | auto |
| OptiScaler_f5.amd-dlss.ini | Inputs | UseFfxInputs | auto |
| OptiScaler_f5.amd-dlss.ini | Inputs | UseFsr2Dx11Inputs | auto |
| OptiScaler_f5.amd-dlss.ini | Inputs | UseFsr2Inputs | auto |
| OptiScaler_f5.amd-dlss.ini | Inputs | UseFsr2VulkanInputs | auto |
| OptiScaler_f5.amd-dlss.ini | Inputs | UseFsr3Inputs | auto |
| OptiScaler_f5.amd-dlss.ini | Libraries | FfxDx12FGPath | auto |
| OptiScaler_f5.amd-dlss.ini | Libraries | FfxDx12Path | auto |
| OptiScaler_f5.amd-dlss.ini | Libraries | FfxDx12RCPath | auto |
| OptiScaler_f5.amd-dlss.ini | Libraries | FfxDx12RRPath | auto |
| OptiScaler_f5.amd-dlss.ini | Libraries | FfxDx12SRPath | auto |
| OptiScaler_f5.amd-dlss.ini | Libraries | FfxVkPath | auto |
| OptiScaler_f5.amd-dlss.ini | Libraries | NvapiPath | auto |
| OptiScaler_f5.amd-dlss.ini | Libraries | NvngxDlssPath | auto |
| OptiScaler_f5.amd-dlss.ini | Libraries | NvngxFeaturePath | auto |
| OptiScaler_f5.amd-dlss.ini | Libraries | NvngxPath | auto |
| OptiScaler_f5.amd-dlss.ini | Libraries | OptiDllPath | auto |
| OptiScaler_f5.amd-dlss.ini | Libraries | XeFGPath | auto |
| OptiScaler_f5.amd-dlss.ini | Libraries | XeLLPath | auto |
| OptiScaler_f5.amd-dlss.ini | Libraries | XeSSDx11Path | auto |
| OptiScaler_f5.amd-dlss.ini | Libraries | XeSSPath | auto |
| OptiScaler_f5.amd-dlss.ini | Log | LogAsync | auto |
| OptiScaler_f5.amd-dlss.ini | Log | LogAsyncThreads | auto |
| OptiScaler_f5.amd-dlss.ini | Log | LogFileName | auto |
| OptiScaler_f5.amd-dlss.ini | Log | LogLevel | 2 |
| OptiScaler_f5.amd-dlss.ini | Log | LogToConsole | auto |
| OptiScaler_f5.amd-dlss.ini | Log | LogToDebug | auto |
| OptiScaler_f5.amd-dlss.ini | Log | LogToFile | true |
| OptiScaler_f5.amd-dlss.ini | Log | LogToNGX | auto |
| OptiScaler_f5.amd-dlss.ini | Log | OpenConsole | auto |
| OptiScaler_f5.amd-dlss.ini | Log | SingleFile | auto |
| OptiScaler_f5.amd-dlss.ini | Magnifier | BorderSize | auto |
| OptiScaler_f5.amd-dlss.ini | Magnifier | CursorOffsetX | auto |
| OptiScaler_f5.amd-dlss.ini | Magnifier | CursorOffsetY | auto |
| OptiScaler_f5.amd-dlss.ini | Magnifier | Enabled | auto |
| OptiScaler_f5.amd-dlss.ini | Magnifier | Size | auto |
| OptiScaler_f5.amd-dlss.ini | Magnifier | StaticPosX | auto |
| OptiScaler_f5.amd-dlss.ini | Magnifier | StaticPosY | auto |
| OptiScaler_f5.amd-dlss.ini | Magnifier | ZoomFactor | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | AccentColorB | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | AccentColorG | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | AccentColorR | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | BGColorA | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | BGColorB | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | BGColorG | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | BGColorR | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | DisableSplash | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | ExtendedLimits | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | F5LowHint | true |
| OptiScaler_f5.amd-dlss.ini | Menu | FGShortcutKey | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | FontSize | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | FpsCycleShortcutKey | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | FpsOverlayAlpha | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | FpsOverlayHorizontal | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | FpsOverlayPos | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | FpsOverlayType | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | FpsScale | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | FpsShortcutKey | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | LightTheme | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | MenuHeight | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | MenuPage | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | MenuWidth | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | ModernTheme | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | OverlayMenu | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | OverlaysUseTheme | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | Scale | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | ShortcutKey | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | ShowFps | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | TTFFontPath | auto |
| OptiScaler_f5.amd-dlss.ini | Menu | UseHQFont | auto |
| OptiScaler_f5.amd-dlss.ini | Mipmap | MipmapBiasFixedOverride | auto |
| OptiScaler_f5.amd-dlss.ini | Mipmap | MipmapBiasOverride | auto |
| OptiScaler_f5.amd-dlss.ini | Mipmap | MipmapBiasOverrideAll | auto |
| OptiScaler_f5.amd-dlss.ini | Mipmap | MipmapBiasScaleOverride | auto |
| OptiScaler_f5.amd-dlss.ini | NvApi | DisableFlipMetering | auto |
| OptiScaler_f5.amd-dlss.ini | NvngxFG | DisableHudless | auto |
| OptiScaler_f5.amd-dlss.ini | NvngxFG | DispatchFlags | auto |
| OptiScaler_f5.amd-dlss.ini | NvngxFG | MakeDepthCopy | auto |
| OptiScaler_f5.amd-dlss.ini | NvngxFG | ShowDebug | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | AlwaysCaptureFSRFGSwapchain | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | AlwaysTrackHeaps | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | DepthScaleMax | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | DisableHUDFix | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | EnableDepthScale | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | HUDFix | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | HudfixDisableDI | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | HudfixDisableDII | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | HudfixDisableDispatch | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | HudfixDisableOM | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | HudfixDisableRTV | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | HudfixDisableSCR | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | HudfixDisableSGR | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | HudfixDisableSRV | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | HudfixDisableUAV | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | HUDFixDontUseSwapchainBuffers | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | HUDFixExtended | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | HUDFixImmediate | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | HUDFixPersistentBindings | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | HUDFixRelaxedResolutionCheck | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | HUDLimit | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | MakeDepthCopy | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | MakeMVCopy | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | ResourceBlocking | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | ResourceFlip | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | ResourceFlipOffset | auto |
| OptiScaler_f5.amd-dlss.ini | OptiFG | UseShards | auto |
| OptiScaler_f5.amd-dlss.ini | OutputScaling | Downscaler | auto |
| OptiScaler_f5.amd-dlss.ini | OutputScaling | Enabled | auto |
| OptiScaler_f5.amd-dlss.ini | OutputScaling | Multiplier | auto |
| OptiScaler_f5.amd-dlss.ini | Plugins | LateAsiPluginsDelay | auto |
| OptiScaler_f5.amd-dlss.ini | Plugins | LoadAsiPlugins | auto |
| OptiScaler_f5.amd-dlss.ini | Plugins | LoadCustomAmdxc64OnRdna2 | auto |
| OptiScaler_f5.amd-dlss.ini | Plugins | LoadReshade | auto |
| OptiScaler_f5.amd-dlss.ini | Plugins | LoadSpecialK | auto |
| OptiScaler_f5.amd-dlss.ini | Plugins | Path | auto |
| OptiScaler_f5.amd-dlss.ini | ProcessFilter | ProcessExclusionList | auto |
| OptiScaler_f5.amd-dlss.ini | ProcessFilter | TargetProcessName | auto |
| OptiScaler_f5.amd-dlss.ini | QualityOverrides | QualityRatioBalanced | auto |
| OptiScaler_f5.amd-dlss.ini | QualityOverrides | QualityRatioDLAA | auto |
| OptiScaler_f5.amd-dlss.ini | QualityOverrides | QualityRatioOverrideEnabled | auto |
| OptiScaler_f5.amd-dlss.ini | QualityOverrides | QualityRatioPerformance | auto |
| OptiScaler_f5.amd-dlss.ini | QualityOverrides | QualityRatioQuality | auto |
| OptiScaler_f5.amd-dlss.ini | QualityOverrides | QualityRatioUltraPerformance | auto |
| OptiScaler_f5.amd-dlss.ini | QualityOverrides | QualityRatioUltraQuality | auto |
| OptiScaler_f5.amd-dlss.ini | Sharpness | OverrideSharpness | auto |
| OptiScaler_f5.amd-dlss.ini | Sharpness | Shader | auto |
| OptiScaler_f5.amd-dlss.ini | Sharpness | Sharpness | auto |
| OptiScaler_f5.amd-dlss.ini | Spoofing | D3DFeatureLevel | auto |
| OptiScaler_f5.amd-dlss.ini | Spoofing | Dxgi | auto |
| OptiScaler_f5.amd-dlss.ini | Spoofing | DxgiBlacklist | auto |
| OptiScaler_f5.amd-dlss.ini | Spoofing | DxgiFactoryWrapping | auto |
| OptiScaler_f5.amd-dlss.ini | Spoofing | DxgiVRAM | auto |
| OptiScaler_f5.amd-dlss.ini | Spoofing | Registry | auto |
| OptiScaler_f5.amd-dlss.ini | Spoofing | RegistryDriver | auto |
| OptiScaler_f5.amd-dlss.ini | Spoofing | SpoofedDeviceId | auto |
| OptiScaler_f5.amd-dlss.ini | Spoofing | SpoofedGPUName | auto |
| OptiScaler_f5.amd-dlss.ini | Spoofing | SpoofedVendorId | auto |
| OptiScaler_f5.amd-dlss.ini | Spoofing | SpoofHAGS | auto |
| OptiScaler_f5.amd-dlss.ini | Spoofing | StreamlineSpoofing | auto |
| OptiScaler_f5.amd-dlss.ini | Spoofing | TargetDeviceId | auto |
| OptiScaler_f5.amd-dlss.ini | Spoofing | TargetVendorId | auto |
| OptiScaler_f5.amd-dlss.ini | Spoofing | UEIntelAtomics | auto |
| OptiScaler_f5.amd-dlss.ini | Spoofing | User32 | auto |
| OptiScaler_f5.amd-dlss.ini | Spoofing | Vulkan | auto |
| OptiScaler_f5.amd-dlss.ini | Spoofing | VulkanExtensionSpoofing | auto |
| OptiScaler_f5.amd-dlss.ini | Spoofing | VulkanVRAM | auto |
| OptiScaler_f5.amd-dlss.ini | UpscaleRatio | UpscaleRatioOverrideEnabled | auto |
| OptiScaler_f5.amd-dlss.ini | UpscaleRatio | UpscaleRatioOverrideValue | auto |
| OptiScaler_f5.amd-dlss.ini | Upscalers | Dx11Upscaler | auto |
| OptiScaler_f5.amd-dlss.ini | Upscalers | Dx12Upscaler | auto |
| OptiScaler_f5.amd-dlss.ini | Upscalers | VulkanUpscaler | auto |
| OptiScaler_f5.amd-dlss.ini | V-Sync | ForceVsync | auto |
| OptiScaler_f5.amd-dlss.ini | V-Sync | OverrideVsync | auto |
| OptiScaler_f5.amd-dlss.ini | V-Sync | SyncInterval | auto |
| OptiScaler_f5.amd-dlss.ini | XeFG | DebugView | auto |
| OptiScaler_f5.amd-dlss.ini | XeFG | DepthInverted | auto |
| OptiScaler_f5.amd-dlss.ini | XeFG | ForceBorderless | auto |
| OptiScaler_f5.amd-dlss.ini | XeFG | HighResMV | auto |
| OptiScaler_f5.amd-dlss.ini | XeFG | IgnoreInitChecks | auto |
| OptiScaler_f5.amd-dlss.ini | XeFG | InterpolationCount | auto |
| OptiScaler_f5.amd-dlss.ini | XeFG | JitteredMV | auto |
| OptiScaler_f5.amd-dlss.ini | XeFG | UIComposition | auto |
| OptiScaler_f5.amd-dlss.ini | XeSS | BuildPipelines | auto |
| OptiScaler_f5.amd-dlss.ini | XeSS | CreateHeaps | auto |
| OptiScaler_f5.amd-dlss.ini | XeSS | NetworkModel | auto |
| OptiScaler_f5.amd-nodlss.ini | Anisotropy | AnisotropyOverride | auto |
| OptiScaler_f5.amd-nodlss.ini | Anisotropy | ModifyComparison | auto |
| OptiScaler_f5.amd-nodlss.ini | Anisotropy | ModifyMinMax | auto |
| OptiScaler_f5.amd-nodlss.ini | Anisotropy | SkipPointFilter | auto |
| OptiScaler_f5.amd-nodlss.ini | CAS | Contrast | auto |
| OptiScaler_f5.amd-nodlss.ini | CAS | ContrastEnabled | auto |
| OptiScaler_f5.amd-nodlss.ini | CAS | DAClampOutput | auto |
| OptiScaler_f5.amd-nodlss.ini | CAS | DADepthBias | auto |
| OptiScaler_f5.amd-nodlss.ini | CAS | DADepthScale | auto |
| OptiScaler_f5.amd-nodlss.ini | CAS | Enabled | auto |
| OptiScaler_f5.amd-nodlss.ini | CAS | MotionScaleLimit | auto |
| OptiScaler_f5.amd-nodlss.ini | CAS | MotionSharpness | auto |
| OptiScaler_f5.amd-nodlss.ini | CAS | MotionSharpnessEnabled | auto |
| OptiScaler_f5.amd-nodlss.ini | CAS | MotionThreshold | auto |
| OptiScaler_f5.amd-nodlss.ini | CAS | SharpenerDebug | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSS | Enabled | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSS | RenderPresetBalanced | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSS | RenderPresetDLAA | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSS | RenderPresetForAll | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSS | RenderPresetOverride | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSS | RenderPresetPerformance | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSS | RenderPresetQuality | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSS | RenderPresetUltraPerformance | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSS | RenderPresetUltraQuality | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSS | UseGenericAppIdWithDlss | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSSD | RenderPresetBalanced | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSSD | RenderPresetDLAA | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSSD | RenderPresetForAll | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSSD | RenderPresetOverride | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSSD | RenderPresetPerformance | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSSD | RenderPresetQuality | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSSD | RenderPresetUltraPerformance | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSSD | RenderPresetUltraQuality | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSSG | AdaBlackwellKernels | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSSG | AdaFlipMeteringPatch | false |
| OptiScaler_f5.amd-nodlss.ini | DLSSG | AdaMfgUnlock | false |
| OptiScaler_f5.amd-nodlss.ini | DLSSG | AdaTemporalFix | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSSG | AmpereMfgHardwareBilinear | false |
| OptiScaler_f5.amd-nodlss.ini | DLSSG | AmpereMfgKernelImage | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSSG | AmpereMfgMaxFrames | 3 |
| OptiScaler_f5.amd-nodlss.ini | DLSSG | AmpereMfgUnlock | false |
| OptiScaler_f5.amd-nodlss.ini | DLSSG | ForceDMFG | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSSG | FramerateTargetDMFG | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSSG | InterpolationCount | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSSG | OverrideForceDMFG | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSSG | OverrideInterpolationCount | auto |
| OptiScaler_f5.amd-nodlss.ini | DLSSG | UseGamesReflexMarkers | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | AutoCapture | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | AutoExposureFollowGame | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | AutoMask | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | ColourEncoding | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | ColourStrength | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | DebugView | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | DeferredDLSS | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | DetailReuse | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | DetailReuseClipFalloff | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | DetailReuseClipGamma | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | DetailReuseDebug | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | DetailReuseDepthTolerance | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | DetailReuseFill | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | DetailReuseMaxDropped | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | DetailReuseMinFps | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | DetailReuseSigmaFloor | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | DetailReuseSteady | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | DetailReuseWithFG | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | Enabled | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | EnvironmentColour | 1.0 |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | EnvironmentDetail | 1.0 |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | FinishedPicture | false |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | FrameStats | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | GameExposureScale | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | Intensity | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | KernelProfile | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | LocalStructure | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | LocalTone | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | LutFile |  |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | LutStrength | 1 |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | MaxRatio | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | ModelResolutionAuto | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | NativeDebugView | false |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | NativeDepthFinder | false |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | NativeDepthOverlay | false |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | NativeDepthVkCopyUsage | true |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | NativeDepthWarmupFrames | 300 |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | NativeDxvkVulkan | false |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | NativeFrameGenerationOnly | false |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | NativeInput | false |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | NativeLowLatency | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | NativeLowLatencyBoost | false |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | NativeMotion | false |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | NativeUpscaler | false |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | Pass2AutoMask | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | Pass2Intensity | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | Pass2LocalStructure | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | Pass2LocalTone | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | Pass2Preset | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | Pass2SkinStructure | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | Pass2Style | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | Pass3AutoMask | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | Pass3Intensity | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | Pass3LocalStructure | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | Pass3LocalTone | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | Pass3Preset | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | Pass3SkinStructure | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | Pass3Style | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | Passes | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | Precision | 0 |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | Preset | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | ReversibleMode | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | RunBeforeSR | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | ScalingDownscaler | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | SceneCut | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | ShowSkinMask | false |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | SkinColour | 1.0 |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | SkinDetail | 1.0 |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | SkinProtection | false |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | SkinStructure | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | SkinToneEnabled | true |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | SpatialCenterX | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | SpatialCenterY | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | SpatialCompression | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | SpatialOffsetX | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | SpatialOffsetY | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | SpatialShiftX | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | SpatialShiftY | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | SpatialWorkX | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | SpatialWorkY | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | Style | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | ToggleKey | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | TransferStrength | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | UnlockPasses | false |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | VitEvery | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | VitEveryPlain | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | WhitePointScale | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | WhitePointSource | auto |
| OptiScaler_f5.amd-nodlss.ini | DlssNr | WorkingScale | auto |
| OptiScaler_f5.amd-nodlss.ini | DRS | DrsMaxOverrideEnabled | auto |
| OptiScaler_f5.amd-nodlss.ini | DRS | DrsMinOverrideEnabled | auto |
| OptiScaler_f5.amd-nodlss.ini | Dx11withDx12 | DontUseNTShared | auto |
| OptiScaler_f5.amd-nodlss.ini | Dx11withDx12 | UseDelayedInit | auto |
| OptiScaler_f5.amd-nodlss.ini | fakenvapi | ForceLatencyFlex | auto |
| OptiScaler_f5.amd-nodlss.ini | fakenvapi | ForceReflex | auto |
| OptiScaler_f5.amd-nodlss.ini | fakenvapi | ForceXeLL | auto |
| OptiScaler_f5.amd-nodlss.ini | fakenvapi | LatencyFlexMode | auto |
| OptiScaler_f5.amd-nodlss.ini | fakenvapi | UseFakenvapi | auto |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | AllowedFrameAhead | auto |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | DebugView | auto |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | DepthValidNow | auto |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | DisableHudless | auto |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | DisableUI | auto |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | DrawUIOverFG | auto |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | Enabled | auto |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | External | false |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | FGInput | auto |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | FGNvngxReplacement | auto |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | FGOutput | auto |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | FTInput | auto |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | HudCutoff | auto |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | HudlessValidNow | auto |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | ModifyBufferState | auto |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | ModifySCIndex | auto |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | OnlyAcceptFirstHudless | auto |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | PreserveSwapChain | auto |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | RectHeight | auto |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | RectLeft | auto |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | RectTop | auto |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | RectWidth | auto |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | SkipReset | auto |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | SkipResizeBuffers | auto |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | StreamlineIgnoreOTA | false |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | UIPremultipliedAlpha | auto |
| OptiScaler_f5.amd-nodlss.ini | FrameGen | VelocityValidNow | auto |
| OptiScaler_f5.amd-nodlss.ini | Framerate | FramerateLimit | auto |
| OptiScaler_f5.amd-nodlss.ini | FSR | AccAddPerFrame | auto |
| OptiScaler_f5.amd-nodlss.ini | FSR | CameraFar | auto |
| OptiScaler_f5.amd-nodlss.ini | FSR | CameraNear | auto |
| OptiScaler_f5.amd-nodlss.ini | FSR | DebugView | auto |
| OptiScaler_f5.amd-nodlss.ini | FSR | DlssReactiveMaskBias | auto |
| OptiScaler_f5.amd-nodlss.ini | FSR | FGIndex | auto |
| OptiScaler_f5.amd-nodlss.ini | FSR | Fsr4DoNotLoadAmdxc64 | auto |
| OptiScaler_f5.amd-nodlss.ini | FSR | Fsr4EnableWatermark | auto |
| OptiScaler_f5.amd-nodlss.ini | FSR | Fsr4ForceModel | auto |
| OptiScaler_f5.amd-nodlss.ini | FSR | Fsr4Preset | auto |
| OptiScaler_f5.amd-nodlss.ini | FSR | FsrAgilitySDKUpgrade | auto |
| OptiScaler_f5.amd-nodlss.ini | FSR | FsrNonLinearColorSpace | auto |
| OptiScaler_f5.amd-nodlss.ini | FSR | FsrNonLinearPQ | auto |
| OptiScaler_f5.amd-nodlss.ini | FSR | FsrNonLinearSRGB | auto |
| OptiScaler_f5.amd-nodlss.ini | FSR | HorizontalFov | auto |
| OptiScaler_f5.amd-nodlss.ini | FSR | MinDisOccAcc | auto |
| OptiScaler_f5.amd-nodlss.ini | FSR | ReactiveScale | auto |
| OptiScaler_f5.amd-nodlss.ini | FSR | ShadingScale | auto |
| OptiScaler_f5.amd-nodlss.ini | FSR | UpscalerIndex | auto |
| OptiScaler_f5.amd-nodlss.ini | FSR | UseFsrInputValues | auto |
| OptiScaler_f5.amd-nodlss.ini | FSR | UseReactiveMaskForTransparency | auto |
| OptiScaler_f5.amd-nodlss.ini | FSR | VelocityFactor | auto |
| OptiScaler_f5.amd-nodlss.ini | FSR | VerticalFov | auto |
| OptiScaler_f5.amd-nodlss.ini | FSRFG | AllowAsync | auto |
| OptiScaler_f5.amd-nodlss.ini | FSRFG | DebugPacingLines | auto |
| OptiScaler_f5.amd-nodlss.ini | FSRFG | DebugResetLines | auto |
| OptiScaler_f5.amd-nodlss.ini | FSRFG | DebugTearLines | auto |
| OptiScaler_f5.amd-nodlss.ini | FSRFG | EnableWatermark | auto |
| OptiScaler_f5.amd-nodlss.ini | FSRFG | FPTHybridSpin | auto |
| OptiScaler_f5.amd-nodlss.ini | FSRFG | FPTHybridSpinTime | auto |
| OptiScaler_f5.amd-nodlss.ini | FSRFG | FPTSafetyMarginInMs | auto |
| OptiScaler_f5.amd-nodlss.ini | FSRFG | FPTVarianceFactor | auto |
| OptiScaler_f5.amd-nodlss.ini | FSRFG | FPTWaitForSingleObjectOnFence | auto |
| OptiScaler_f5.amd-nodlss.ini | FSRFG | FramePacingTuning | auto |
| OptiScaler_f5.amd-nodlss.ini | FSRFG | UseMutexForSwapchain | auto |
| OptiScaler_f5.amd-nodlss.ini | FSRFGInputs | SkipConfigForHudless | auto |
| OptiScaler_f5.amd-nodlss.ini | FSRFGInputs | SkipDispatchForHudless | auto |
| OptiScaler_f5.amd-nodlss.ini | HDR | ForceHDR | auto |
| OptiScaler_f5.amd-nodlss.ini | HDR | SkipColorSpace | auto |
| OptiScaler_f5.amd-nodlss.ini | HDR | UseHDR10 | auto |
| OptiScaler_f5.amd-nodlss.ini | Hooks | D3D11FeatureLevelElevation | auto |
| OptiScaler_f5.amd-nodlss.ini | Hooks | EarlyHooking | auto |
| OptiScaler_f5.amd-nodlss.ini | Hooks | HookOriginalNvngxOnly | auto |
| OptiScaler_f5.amd-nodlss.ini | Hooks | UseNtdllHooks | auto |
| OptiScaler_f5.amd-nodlss.ini | Hotfix | CheckForUpdate | auto |
| OptiScaler_f5.amd-nodlss.ini | Hotfix | ColorMaskResourceBarrier | auto |
| OptiScaler_f5.amd-nodlss.ini | Hotfix | ColorResourceBarrier | auto |
| OptiScaler_f5.amd-nodlss.ini | Hotfix | CreateD3D12DeviceForLuma | auto |
| OptiScaler_f5.amd-nodlss.ini | Hotfix | DepthResourceBarrier | auto |
| OptiScaler_f5.amd-nodlss.ini | Hotfix | DisableOverlays | auto |
| OptiScaler_f5.amd-nodlss.ini | Hotfix | ExposureResourceBarrier | auto |
| OptiScaler_f5.amd-nodlss.ini | Hotfix | ExtendedStateRestore | auto |
| OptiScaler_f5.amd-nodlss.ini | Hotfix | ManualInputPolling | auto |
| OptiScaler_f5.amd-nodlss.ini | Hotfix | MotionVectorResourceBarrier | auto |
| OptiScaler_f5.amd-nodlss.ini | Hotfix | OutputResourceBarrier | auto |
| OptiScaler_f5.amd-nodlss.ini | Hotfix | PreferDedicatedGpu | auto |
| OptiScaler_f5.amd-nodlss.ini | Hotfix | PreferFirstDedicatedGpu | auto |
| OptiScaler_f5.amd-nodlss.ini | Hotfix | REFrameworkCompat | auto |
| OptiScaler_f5.amd-nodlss.ini | Hotfix | RestoreComputeSignature | auto |
| OptiScaler_f5.amd-nodlss.ini | Hotfix | RestoreGraphicSignature | auto |
| OptiScaler_f5.amd-nodlss.ini | Hotfix | RoundInternalResolution | auto |
| OptiScaler_f5.amd-nodlss.ini | Hotfix | SimulateWaitableObject | auto |
| OptiScaler_f5.amd-nodlss.ini | Hotfix | SkipFirstFrames | auto |
| OptiScaler_f5.amd-nodlss.ini | Hotfix | UsePrecompiledShaders | auto |
| OptiScaler_f5.amd-nodlss.ini | InitFlags | AutoExposure | auto |
| OptiScaler_f5.amd-nodlss.ini | InitFlags | DepthInverted | auto |
| OptiScaler_f5.amd-nodlss.ini | InitFlags | DisableReactiveMask | auto |
| OptiScaler_f5.amd-nodlss.ini | InitFlags | DisplayResolution | auto |
| OptiScaler_f5.amd-nodlss.ini | InitFlags | HDR | auto |
| OptiScaler_f5.amd-nodlss.ini | InitFlags | JitterCancellation | auto |
| OptiScaler_f5.amd-nodlss.ini | Inputs | EnableDlssInputs | auto |
| OptiScaler_f5.amd-nodlss.ini | Inputs | EnableFfxInputs | auto |
| OptiScaler_f5.amd-nodlss.ini | Inputs | EnableFsr2Inputs | auto |
| OptiScaler_f5.amd-nodlss.ini | Inputs | EnableFsr3Inputs | auto |
| OptiScaler_f5.amd-nodlss.ini | Inputs | EnableHotSwapping | auto |
| OptiScaler_f5.amd-nodlss.ini | Inputs | EnableXeSSInputs | auto |
| OptiScaler_f5.amd-nodlss.ini | Inputs | Fsr2Pattern | auto |
| OptiScaler_f5.amd-nodlss.ini | Inputs | Fsr3Pattern | auto |
| OptiScaler_f5.amd-nodlss.ini | Inputs | UseFfxInputs | auto |
| OptiScaler_f5.amd-nodlss.ini | Inputs | UseFsr2Dx11Inputs | auto |
| OptiScaler_f5.amd-nodlss.ini | Inputs | UseFsr2Inputs | auto |
| OptiScaler_f5.amd-nodlss.ini | Inputs | UseFsr2VulkanInputs | auto |
| OptiScaler_f5.amd-nodlss.ini | Inputs | UseFsr3Inputs | auto |
| OptiScaler_f5.amd-nodlss.ini | Libraries | FfxDx12FGPath | auto |
| OptiScaler_f5.amd-nodlss.ini | Libraries | FfxDx12Path | auto |
| OptiScaler_f5.amd-nodlss.ini | Libraries | FfxDx12RCPath | auto |
| OptiScaler_f5.amd-nodlss.ini | Libraries | FfxDx12RRPath | auto |
| OptiScaler_f5.amd-nodlss.ini | Libraries | FfxDx12SRPath | auto |
| OptiScaler_f5.amd-nodlss.ini | Libraries | FfxVkPath | auto |
| OptiScaler_f5.amd-nodlss.ini | Libraries | NvapiPath | auto |
| OptiScaler_f5.amd-nodlss.ini | Libraries | NvngxDlssPath | auto |
| OptiScaler_f5.amd-nodlss.ini | Libraries | NvngxFeaturePath | auto |
| OptiScaler_f5.amd-nodlss.ini | Libraries | NvngxPath | auto |
| OptiScaler_f5.amd-nodlss.ini | Libraries | OptiDllPath | auto |
| OptiScaler_f5.amd-nodlss.ini | Libraries | XeFGPath | auto |
| OptiScaler_f5.amd-nodlss.ini | Libraries | XeLLPath | auto |
| OptiScaler_f5.amd-nodlss.ini | Libraries | XeSSDx11Path | auto |
| OptiScaler_f5.amd-nodlss.ini | Libraries | XeSSPath | auto |
| OptiScaler_f5.amd-nodlss.ini | Log | LogAsync | auto |
| OptiScaler_f5.amd-nodlss.ini | Log | LogAsyncThreads | auto |
| OptiScaler_f5.amd-nodlss.ini | Log | LogFileName | auto |
| OptiScaler_f5.amd-nodlss.ini | Log | LogLevel | 2 |
| OptiScaler_f5.amd-nodlss.ini | Log | LogToConsole | auto |
| OptiScaler_f5.amd-nodlss.ini | Log | LogToDebug | auto |
| OptiScaler_f5.amd-nodlss.ini | Log | LogToFile | true |
| OptiScaler_f5.amd-nodlss.ini | Log | LogToNGX | auto |
| OptiScaler_f5.amd-nodlss.ini | Log | OpenConsole | auto |
| OptiScaler_f5.amd-nodlss.ini | Log | SingleFile | auto |
| OptiScaler_f5.amd-nodlss.ini | Magnifier | BorderSize | auto |
| OptiScaler_f5.amd-nodlss.ini | Magnifier | CursorOffsetX | auto |
| OptiScaler_f5.amd-nodlss.ini | Magnifier | CursorOffsetY | auto |
| OptiScaler_f5.amd-nodlss.ini | Magnifier | Enabled | auto |
| OptiScaler_f5.amd-nodlss.ini | Magnifier | Size | auto |
| OptiScaler_f5.amd-nodlss.ini | Magnifier | StaticPosX | auto |
| OptiScaler_f5.amd-nodlss.ini | Magnifier | StaticPosY | auto |
| OptiScaler_f5.amd-nodlss.ini | Magnifier | ZoomFactor | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | AccentColorB | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | AccentColorG | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | AccentColorR | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | BGColorA | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | BGColorB | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | BGColorG | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | BGColorR | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | DisableSplash | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | ExtendedLimits | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | F5LowHint | true |
| OptiScaler_f5.amd-nodlss.ini | Menu | FGShortcutKey | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | FontSize | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | FpsCycleShortcutKey | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | FpsOverlayAlpha | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | FpsOverlayHorizontal | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | FpsOverlayPos | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | FpsOverlayType | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | FpsScale | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | FpsShortcutKey | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | LightTheme | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | MenuHeight | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | MenuPage | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | MenuWidth | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | ModernTheme | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | OverlayMenu | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | OverlaysUseTheme | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | Scale | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | ShortcutKey | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | ShowFps | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | TTFFontPath | auto |
| OptiScaler_f5.amd-nodlss.ini | Menu | UseHQFont | auto |
| OptiScaler_f5.amd-nodlss.ini | Mipmap | MipmapBiasFixedOverride | auto |
| OptiScaler_f5.amd-nodlss.ini | Mipmap | MipmapBiasOverride | auto |
| OptiScaler_f5.amd-nodlss.ini | Mipmap | MipmapBiasOverrideAll | auto |
| OptiScaler_f5.amd-nodlss.ini | Mipmap | MipmapBiasScaleOverride | auto |
| OptiScaler_f5.amd-nodlss.ini | NvApi | DisableFlipMetering | auto |
| OptiScaler_f5.amd-nodlss.ini | NvngxFG | DisableHudless | auto |
| OptiScaler_f5.amd-nodlss.ini | NvngxFG | DispatchFlags | auto |
| OptiScaler_f5.amd-nodlss.ini | NvngxFG | MakeDepthCopy | auto |
| OptiScaler_f5.amd-nodlss.ini | NvngxFG | ShowDebug | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | AlwaysCaptureFSRFGSwapchain | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | AlwaysTrackHeaps | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | DepthScaleMax | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | DisableHUDFix | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | EnableDepthScale | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | HUDFix | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | HudfixDisableDI | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | HudfixDisableDII | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | HudfixDisableDispatch | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | HudfixDisableOM | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | HudfixDisableRTV | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | HudfixDisableSCR | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | HudfixDisableSGR | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | HudfixDisableSRV | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | HudfixDisableUAV | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | HUDFixDontUseSwapchainBuffers | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | HUDFixExtended | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | HUDFixImmediate | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | HUDFixPersistentBindings | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | HUDFixRelaxedResolutionCheck | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | HUDLimit | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | MakeDepthCopy | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | MakeMVCopy | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | ResourceBlocking | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | ResourceFlip | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | ResourceFlipOffset | auto |
| OptiScaler_f5.amd-nodlss.ini | OptiFG | UseShards | auto |
| OptiScaler_f5.amd-nodlss.ini | OutputScaling | Downscaler | auto |
| OptiScaler_f5.amd-nodlss.ini | OutputScaling | Enabled | auto |
| OptiScaler_f5.amd-nodlss.ini | OutputScaling | Multiplier | auto |
| OptiScaler_f5.amd-nodlss.ini | Plugins | LateAsiPluginsDelay | auto |
| OptiScaler_f5.amd-nodlss.ini | Plugins | LoadAsiPlugins | auto |
| OptiScaler_f5.amd-nodlss.ini | Plugins | LoadCustomAmdxc64OnRdna2 | auto |
| OptiScaler_f5.amd-nodlss.ini | Plugins | LoadReshade | auto |
| OptiScaler_f5.amd-nodlss.ini | Plugins | LoadSpecialK | auto |
| OptiScaler_f5.amd-nodlss.ini | Plugins | Path | auto |
| OptiScaler_f5.amd-nodlss.ini | ProcessFilter | ProcessExclusionList | auto |
| OptiScaler_f5.amd-nodlss.ini | ProcessFilter | TargetProcessName | auto |
| OptiScaler_f5.amd-nodlss.ini | QualityOverrides | QualityRatioBalanced | auto |
| OptiScaler_f5.amd-nodlss.ini | QualityOverrides | QualityRatioDLAA | auto |
| OptiScaler_f5.amd-nodlss.ini | QualityOverrides | QualityRatioOverrideEnabled | auto |
| OptiScaler_f5.amd-nodlss.ini | QualityOverrides | QualityRatioPerformance | auto |
| OptiScaler_f5.amd-nodlss.ini | QualityOverrides | QualityRatioQuality | auto |
| OptiScaler_f5.amd-nodlss.ini | QualityOverrides | QualityRatioUltraPerformance | auto |
| OptiScaler_f5.amd-nodlss.ini | QualityOverrides | QualityRatioUltraQuality | auto |
| OptiScaler_f5.amd-nodlss.ini | Sharpness | OverrideSharpness | auto |
| OptiScaler_f5.amd-nodlss.ini | Sharpness | Shader | auto |
| OptiScaler_f5.amd-nodlss.ini | Sharpness | Sharpness | auto |
| OptiScaler_f5.amd-nodlss.ini | Spoofing | D3DFeatureLevel | auto |
| OptiScaler_f5.amd-nodlss.ini | Spoofing | Dxgi | false |
| OptiScaler_f5.amd-nodlss.ini | Spoofing | DxgiBlacklist | auto |
| OptiScaler_f5.amd-nodlss.ini | Spoofing | DxgiFactoryWrapping | auto |
| OptiScaler_f5.amd-nodlss.ini | Spoofing | DxgiVRAM | auto |
| OptiScaler_f5.amd-nodlss.ini | Spoofing | Registry | auto |
| OptiScaler_f5.amd-nodlss.ini | Spoofing | RegistryDriver | auto |
| OptiScaler_f5.amd-nodlss.ini | Spoofing | SpoofedDeviceId | auto |
| OptiScaler_f5.amd-nodlss.ini | Spoofing | SpoofedGPUName | auto |
| OptiScaler_f5.amd-nodlss.ini | Spoofing | SpoofedVendorId | auto |
| OptiScaler_f5.amd-nodlss.ini | Spoofing | SpoofHAGS | auto |
| OptiScaler_f5.amd-nodlss.ini | Spoofing | StreamlineSpoofing | auto |
| OptiScaler_f5.amd-nodlss.ini | Spoofing | TargetDeviceId | auto |
| OptiScaler_f5.amd-nodlss.ini | Spoofing | TargetVendorId | auto |
| OptiScaler_f5.amd-nodlss.ini | Spoofing | UEIntelAtomics | auto |
| OptiScaler_f5.amd-nodlss.ini | Spoofing | User32 | auto |
| OptiScaler_f5.amd-nodlss.ini | Spoofing | Vulkan | auto |
| OptiScaler_f5.amd-nodlss.ini | Spoofing | VulkanExtensionSpoofing | auto |
| OptiScaler_f5.amd-nodlss.ini | Spoofing | VulkanVRAM | auto |
| OptiScaler_f5.amd-nodlss.ini | UpscaleRatio | UpscaleRatioOverrideEnabled | auto |
| OptiScaler_f5.amd-nodlss.ini | UpscaleRatio | UpscaleRatioOverrideValue | auto |
| OptiScaler_f5.amd-nodlss.ini | Upscalers | Dx11Upscaler | auto |
| OptiScaler_f5.amd-nodlss.ini | Upscalers | Dx12Upscaler | auto |
| OptiScaler_f5.amd-nodlss.ini | Upscalers | VulkanUpscaler | auto |
| OptiScaler_f5.amd-nodlss.ini | V-Sync | ForceVsync | auto |
| OptiScaler_f5.amd-nodlss.ini | V-Sync | OverrideVsync | auto |
| OptiScaler_f5.amd-nodlss.ini | V-Sync | SyncInterval | auto |
| OptiScaler_f5.amd-nodlss.ini | XeFG | DebugView | auto |
| OptiScaler_f5.amd-nodlss.ini | XeFG | DepthInverted | auto |
| OptiScaler_f5.amd-nodlss.ini | XeFG | ForceBorderless | auto |
| OptiScaler_f5.amd-nodlss.ini | XeFG | HighResMV | auto |
| OptiScaler_f5.amd-nodlss.ini | XeFG | IgnoreInitChecks | auto |
| OptiScaler_f5.amd-nodlss.ini | XeFG | InterpolationCount | auto |
| OptiScaler_f5.amd-nodlss.ini | XeFG | JitteredMV | auto |
| OptiScaler_f5.amd-nodlss.ini | XeFG | UIComposition | auto |
| OptiScaler_f5.amd-nodlss.ini | XeSS | BuildPipelines | auto |
| OptiScaler_f5.amd-nodlss.ini | XeSS | CreateHeaps | auto |
| OptiScaler_f5.amd-nodlss.ini | XeSS | NetworkModel | auto |
| OptiScaler_f5.nvidia.ini | Anisotropy | AnisotropyOverride | auto |
| OptiScaler_f5.nvidia.ini | Anisotropy | ModifyComparison | auto |
| OptiScaler_f5.nvidia.ini | Anisotropy | ModifyMinMax | auto |
| OptiScaler_f5.nvidia.ini | Anisotropy | SkipPointFilter | auto |
| OptiScaler_f5.nvidia.ini | CAS | Contrast | auto |
| OptiScaler_f5.nvidia.ini | CAS | ContrastEnabled | auto |
| OptiScaler_f5.nvidia.ini | CAS | DAClampOutput | auto |
| OptiScaler_f5.nvidia.ini | CAS | DADepthBias | auto |
| OptiScaler_f5.nvidia.ini | CAS | DADepthScale | auto |
| OptiScaler_f5.nvidia.ini | CAS | Enabled | auto |
| OptiScaler_f5.nvidia.ini | CAS | MotionScaleLimit | auto |
| OptiScaler_f5.nvidia.ini | CAS | MotionSharpness | auto |
| OptiScaler_f5.nvidia.ini | CAS | MotionSharpnessEnabled | auto |
| OptiScaler_f5.nvidia.ini | CAS | MotionThreshold | auto |
| OptiScaler_f5.nvidia.ini | CAS | SharpenerDebug | auto |
| OptiScaler_f5.nvidia.ini | DLSS | Enabled | auto |
| OptiScaler_f5.nvidia.ini | DLSS | RenderPresetBalanced | auto |
| OptiScaler_f5.nvidia.ini | DLSS | RenderPresetDLAA | auto |
| OptiScaler_f5.nvidia.ini | DLSS | RenderPresetForAll | auto |
| OptiScaler_f5.nvidia.ini | DLSS | RenderPresetOverride | auto |
| OptiScaler_f5.nvidia.ini | DLSS | RenderPresetPerformance | auto |
| OptiScaler_f5.nvidia.ini | DLSS | RenderPresetQuality | auto |
| OptiScaler_f5.nvidia.ini | DLSS | RenderPresetUltraPerformance | auto |
| OptiScaler_f5.nvidia.ini | DLSS | RenderPresetUltraQuality | auto |
| OptiScaler_f5.nvidia.ini | DLSS | UseGenericAppIdWithDlss | auto |
| OptiScaler_f5.nvidia.ini | DLSSD | RenderPresetBalanced | auto |
| OptiScaler_f5.nvidia.ini | DLSSD | RenderPresetDLAA | auto |
| OptiScaler_f5.nvidia.ini | DLSSD | RenderPresetForAll | auto |
| OptiScaler_f5.nvidia.ini | DLSSD | RenderPresetOverride | auto |
| OptiScaler_f5.nvidia.ini | DLSSD | RenderPresetPerformance | auto |
| OptiScaler_f5.nvidia.ini | DLSSD | RenderPresetQuality | auto |
| OptiScaler_f5.nvidia.ini | DLSSD | RenderPresetUltraPerformance | auto |
| OptiScaler_f5.nvidia.ini | DLSSD | RenderPresetUltraQuality | auto |
| OptiScaler_f5.nvidia.ini | DLSSG | AdaBlackwellKernels | auto |
| OptiScaler_f5.nvidia.ini | DLSSG | AdaFlipMeteringPatch | false |
| OptiScaler_f5.nvidia.ini | DLSSG | AdaMfgUnlock | false |
| OptiScaler_f5.nvidia.ini | DLSSG | AdaTemporalFix | auto |
| OptiScaler_f5.nvidia.ini | DLSSG | AmpereMfgHardwareBilinear | false |
| OptiScaler_f5.nvidia.ini | DLSSG | AmpereMfgKernelImage | auto |
| OptiScaler_f5.nvidia.ini | DLSSG | AmpereMfgMaxFrames | 3 |
| OptiScaler_f5.nvidia.ini | DLSSG | AmpereMfgUnlock | false |
| OptiScaler_f5.nvidia.ini | DLSSG | ForceDMFG | auto |
| OptiScaler_f5.nvidia.ini | DLSSG | FramerateTargetDMFG | auto |
| OptiScaler_f5.nvidia.ini | DLSSG | InterpolationCount | auto |
| OptiScaler_f5.nvidia.ini | DLSSG | OverrideForceDMFG | auto |
| OptiScaler_f5.nvidia.ini | DLSSG | OverrideInterpolationCount | auto |
| OptiScaler_f5.nvidia.ini | DLSSG | UseGamesReflexMarkers | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | AutoCapture | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | AutoExposureFollowGame | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | AutoMask | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | ColourEncoding | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | ColourStrength | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | DebugView | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | DeferredDLSS | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | DetailReuse | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | DetailReuseClipFalloff | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | DetailReuseClipGamma | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | DetailReuseDebug | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | DetailReuseDepthTolerance | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | DetailReuseFill | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | DetailReuseMaxDropped | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | DetailReuseMinFps | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | DetailReuseSigmaFloor | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | DetailReuseSteady | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | DetailReuseWithFG | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | Enabled | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | EnvironmentColour | 1.0 |
| OptiScaler_f5.nvidia.ini | DlssNr | EnvironmentDetail | 1.0 |
| OptiScaler_f5.nvidia.ini | DlssNr | FinishedPicture | false |
| OptiScaler_f5.nvidia.ini | DlssNr | FrameStats | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | GameExposureScale | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | Intensity | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | KernelProfile | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | LocalStructure | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | LocalTone | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | LutFile |  |
| OptiScaler_f5.nvidia.ini | DlssNr | LutStrength | 1 |
| OptiScaler_f5.nvidia.ini | DlssNr | MaxRatio | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | ModelResolutionAuto | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | NativeDebugView | false |
| OptiScaler_f5.nvidia.ini | DlssNr | NativeDepthFinder | false |
| OptiScaler_f5.nvidia.ini | DlssNr | NativeDepthOverlay | false |
| OptiScaler_f5.nvidia.ini | DlssNr | NativeDepthVkCopyUsage | true |
| OptiScaler_f5.nvidia.ini | DlssNr | NativeDepthWarmupFrames | 300 |
| OptiScaler_f5.nvidia.ini | DlssNr | NativeDxvkVulkan | false |
| OptiScaler_f5.nvidia.ini | DlssNr | NativeFrameGenerationOnly | false |
| OptiScaler_f5.nvidia.ini | DlssNr | NativeInput | false |
| OptiScaler_f5.nvidia.ini | DlssNr | NativeLowLatency | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | NativeLowLatencyBoost | false |
| OptiScaler_f5.nvidia.ini | DlssNr | NativeMotion | false |
| OptiScaler_f5.nvidia.ini | DlssNr | NativeUpscaler | false |
| OptiScaler_f5.nvidia.ini | DlssNr | Pass2AutoMask | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | Pass2Intensity | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | Pass2LocalStructure | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | Pass2LocalTone | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | Pass2Preset | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | Pass2SkinStructure | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | Pass2Style | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | Pass3AutoMask | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | Pass3Intensity | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | Pass3LocalStructure | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | Pass3LocalTone | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | Pass3Preset | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | Pass3SkinStructure | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | Pass3Style | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | Passes | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | Precision | 0 |
| OptiScaler_f5.nvidia.ini | DlssNr | Preset | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | ReversibleMode | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | RunBeforeSR | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | ScalingDownscaler | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | SceneCut | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | ShowSkinMask | false |
| OptiScaler_f5.nvidia.ini | DlssNr | SkinColour | 1.0 |
| OptiScaler_f5.nvidia.ini | DlssNr | SkinDetail | 1.0 |
| OptiScaler_f5.nvidia.ini | DlssNr | SkinProtection | false |
| OptiScaler_f5.nvidia.ini | DlssNr | SkinStructure | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | SkinToneEnabled | true |
| OptiScaler_f5.nvidia.ini | DlssNr | SpatialCenterX | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | SpatialCenterY | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | SpatialCompression | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | SpatialOffsetX | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | SpatialOffsetY | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | SpatialShiftX | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | SpatialShiftY | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | SpatialWorkX | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | SpatialWorkY | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | Style | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | ToggleKey | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | TransferStrength | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | UnlockPasses | false |
| OptiScaler_f5.nvidia.ini | DlssNr | VitEvery | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | VitEveryPlain | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | WhitePointScale | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | WhitePointSource | auto |
| OptiScaler_f5.nvidia.ini | DlssNr | WorkingScale | auto |
| OptiScaler_f5.nvidia.ini | DRS | DrsMaxOverrideEnabled | auto |
| OptiScaler_f5.nvidia.ini | DRS | DrsMinOverrideEnabled | auto |
| OptiScaler_f5.nvidia.ini | Dx11withDx12 | DontUseNTShared | auto |
| OptiScaler_f5.nvidia.ini | Dx11withDx12 | UseDelayedInit | auto |
| OptiScaler_f5.nvidia.ini | fakenvapi | ForceLatencyFlex | auto |
| OptiScaler_f5.nvidia.ini | fakenvapi | ForceReflex | auto |
| OptiScaler_f5.nvidia.ini | fakenvapi | ForceXeLL | auto |
| OptiScaler_f5.nvidia.ini | fakenvapi | LatencyFlexMode | auto |
| OptiScaler_f5.nvidia.ini | fakenvapi | UseFakenvapi | auto |
| OptiScaler_f5.nvidia.ini | FrameGen | AllowedFrameAhead | auto |
| OptiScaler_f5.nvidia.ini | FrameGen | DebugView | auto |
| OptiScaler_f5.nvidia.ini | FrameGen | DepthValidNow | auto |
| OptiScaler_f5.nvidia.ini | FrameGen | DisableHudless | auto |
| OptiScaler_f5.nvidia.ini | FrameGen | DisableUI | auto |
| OptiScaler_f5.nvidia.ini | FrameGen | DrawUIOverFG | auto |
| OptiScaler_f5.nvidia.ini | FrameGen | Enabled | auto |
| OptiScaler_f5.nvidia.ini | FrameGen | External | false |
| OptiScaler_f5.nvidia.ini | FrameGen | FGInput | auto |
| OptiScaler_f5.nvidia.ini | FrameGen | FGNvngxReplacement | auto |
| OptiScaler_f5.nvidia.ini | FrameGen | FGOutput | auto |
| OptiScaler_f5.nvidia.ini | FrameGen | FTInput | auto |
| OptiScaler_f5.nvidia.ini | FrameGen | HudCutoff | auto |
| OptiScaler_f5.nvidia.ini | FrameGen | HudlessValidNow | auto |
| OptiScaler_f5.nvidia.ini | FrameGen | ModifyBufferState | auto |
| OptiScaler_f5.nvidia.ini | FrameGen | ModifySCIndex | auto |
| OptiScaler_f5.nvidia.ini | FrameGen | OnlyAcceptFirstHudless | auto |
| OptiScaler_f5.nvidia.ini | FrameGen | PreserveSwapChain | auto |
| OptiScaler_f5.nvidia.ini | FrameGen | RectHeight | auto |
| OptiScaler_f5.nvidia.ini | FrameGen | RectLeft | auto |
| OptiScaler_f5.nvidia.ini | FrameGen | RectTop | auto |
| OptiScaler_f5.nvidia.ini | FrameGen | RectWidth | auto |
| OptiScaler_f5.nvidia.ini | FrameGen | SkipReset | auto |
| OptiScaler_f5.nvidia.ini | FrameGen | SkipResizeBuffers | auto |
| OptiScaler_f5.nvidia.ini | FrameGen | StreamlineIgnoreOTA | false |
| OptiScaler_f5.nvidia.ini | FrameGen | UIPremultipliedAlpha | auto |
| OptiScaler_f5.nvidia.ini | FrameGen | VelocityValidNow | auto |
| OptiScaler_f5.nvidia.ini | Framerate | FramerateLimit | auto |
| OptiScaler_f5.nvidia.ini | FSR | AccAddPerFrame | auto |
| OptiScaler_f5.nvidia.ini | FSR | CameraFar | auto |
| OptiScaler_f5.nvidia.ini | FSR | CameraNear | auto |
| OptiScaler_f5.nvidia.ini | FSR | DebugView | auto |
| OptiScaler_f5.nvidia.ini | FSR | DlssReactiveMaskBias | auto |
| OptiScaler_f5.nvidia.ini | FSR | FGIndex | auto |
| OptiScaler_f5.nvidia.ini | FSR | Fsr4DoNotLoadAmdxc64 | auto |
| OptiScaler_f5.nvidia.ini | FSR | Fsr4EnableWatermark | auto |
| OptiScaler_f5.nvidia.ini | FSR | Fsr4ForceModel | auto |
| OptiScaler_f5.nvidia.ini | FSR | Fsr4Preset | auto |
| OptiScaler_f5.nvidia.ini | FSR | FsrAgilitySDKUpgrade | auto |
| OptiScaler_f5.nvidia.ini | FSR | FsrNonLinearColorSpace | auto |
| OptiScaler_f5.nvidia.ini | FSR | FsrNonLinearPQ | auto |
| OptiScaler_f5.nvidia.ini | FSR | FsrNonLinearSRGB | auto |
| OptiScaler_f5.nvidia.ini | FSR | HorizontalFov | auto |
| OptiScaler_f5.nvidia.ini | FSR | MinDisOccAcc | auto |
| OptiScaler_f5.nvidia.ini | FSR | ReactiveScale | auto |
| OptiScaler_f5.nvidia.ini | FSR | ShadingScale | auto |
| OptiScaler_f5.nvidia.ini | FSR | UpscalerIndex | auto |
| OptiScaler_f5.nvidia.ini | FSR | UseFsrInputValues | auto |
| OptiScaler_f5.nvidia.ini | FSR | UseReactiveMaskForTransparency | auto |
| OptiScaler_f5.nvidia.ini | FSR | VelocityFactor | auto |
| OptiScaler_f5.nvidia.ini | FSR | VerticalFov | auto |
| OptiScaler_f5.nvidia.ini | FSRFG | AllowAsync | auto |
| OptiScaler_f5.nvidia.ini | FSRFG | DebugPacingLines | auto |
| OptiScaler_f5.nvidia.ini | FSRFG | DebugResetLines | auto |
| OptiScaler_f5.nvidia.ini | FSRFG | DebugTearLines | auto |
| OptiScaler_f5.nvidia.ini | FSRFG | EnableWatermark | auto |
| OptiScaler_f5.nvidia.ini | FSRFG | FPTHybridSpin | auto |
| OptiScaler_f5.nvidia.ini | FSRFG | FPTHybridSpinTime | auto |
| OptiScaler_f5.nvidia.ini | FSRFG | FPTSafetyMarginInMs | auto |
| OptiScaler_f5.nvidia.ini | FSRFG | FPTVarianceFactor | auto |
| OptiScaler_f5.nvidia.ini | FSRFG | FPTWaitForSingleObjectOnFence | auto |
| OptiScaler_f5.nvidia.ini | FSRFG | FramePacingTuning | auto |
| OptiScaler_f5.nvidia.ini | FSRFG | UseMutexForSwapchain | auto |
| OptiScaler_f5.nvidia.ini | FSRFGInputs | SkipConfigForHudless | auto |
| OptiScaler_f5.nvidia.ini | FSRFGInputs | SkipDispatchForHudless | auto |
| OptiScaler_f5.nvidia.ini | HDR | ForceHDR | auto |
| OptiScaler_f5.nvidia.ini | HDR | SkipColorSpace | auto |
| OptiScaler_f5.nvidia.ini | HDR | UseHDR10 | auto |
| OptiScaler_f5.nvidia.ini | Hooks | D3D11FeatureLevelElevation | auto |
| OptiScaler_f5.nvidia.ini | Hooks | EarlyHooking | auto |
| OptiScaler_f5.nvidia.ini | Hooks | HookOriginalNvngxOnly | auto |
| OptiScaler_f5.nvidia.ini | Hooks | UseNtdllHooks | auto |
| OptiScaler_f5.nvidia.ini | Hotfix | CheckForUpdate | auto |
| OptiScaler_f5.nvidia.ini | Hotfix | ColorMaskResourceBarrier | auto |
| OptiScaler_f5.nvidia.ini | Hotfix | ColorResourceBarrier | auto |
| OptiScaler_f5.nvidia.ini | Hotfix | CreateD3D12DeviceForLuma | auto |
| OptiScaler_f5.nvidia.ini | Hotfix | DepthResourceBarrier | auto |
| OptiScaler_f5.nvidia.ini | Hotfix | DisableOverlays | auto |
| OptiScaler_f5.nvidia.ini | Hotfix | ExposureResourceBarrier | auto |
| OptiScaler_f5.nvidia.ini | Hotfix | ExtendedStateRestore | auto |
| OptiScaler_f5.nvidia.ini | Hotfix | ManualInputPolling | auto |
| OptiScaler_f5.nvidia.ini | Hotfix | MotionVectorResourceBarrier | auto |
| OptiScaler_f5.nvidia.ini | Hotfix | OutputResourceBarrier | auto |
| OptiScaler_f5.nvidia.ini | Hotfix | PreferDedicatedGpu | auto |
| OptiScaler_f5.nvidia.ini | Hotfix | PreferFirstDedicatedGpu | auto |
| OptiScaler_f5.nvidia.ini | Hotfix | REFrameworkCompat | auto |
| OptiScaler_f5.nvidia.ini | Hotfix | RestoreComputeSignature | auto |
| OptiScaler_f5.nvidia.ini | Hotfix | RestoreGraphicSignature | auto |
| OptiScaler_f5.nvidia.ini | Hotfix | RoundInternalResolution | auto |
| OptiScaler_f5.nvidia.ini | Hotfix | SimulateWaitableObject | auto |
| OptiScaler_f5.nvidia.ini | Hotfix | SkipFirstFrames | auto |
| OptiScaler_f5.nvidia.ini | Hotfix | UsePrecompiledShaders | auto |
| OptiScaler_f5.nvidia.ini | InitFlags | AutoExposure | auto |
| OptiScaler_f5.nvidia.ini | InitFlags | DepthInverted | auto |
| OptiScaler_f5.nvidia.ini | InitFlags | DisableReactiveMask | auto |
| OptiScaler_f5.nvidia.ini | InitFlags | DisplayResolution | auto |
| OptiScaler_f5.nvidia.ini | InitFlags | HDR | auto |
| OptiScaler_f5.nvidia.ini | InitFlags | JitterCancellation | auto |
| OptiScaler_f5.nvidia.ini | Inputs | EnableDlssInputs | auto |
| OptiScaler_f5.nvidia.ini | Inputs | EnableFfxInputs | auto |
| OptiScaler_f5.nvidia.ini | Inputs | EnableFsr2Inputs | auto |
| OptiScaler_f5.nvidia.ini | Inputs | EnableFsr3Inputs | auto |
| OptiScaler_f5.nvidia.ini | Inputs | EnableHotSwapping | auto |
| OptiScaler_f5.nvidia.ini | Inputs | EnableXeSSInputs | auto |
| OptiScaler_f5.nvidia.ini | Inputs | Fsr2Pattern | auto |
| OptiScaler_f5.nvidia.ini | Inputs | Fsr3Pattern | auto |
| OptiScaler_f5.nvidia.ini | Inputs | UseFfxInputs | auto |
| OptiScaler_f5.nvidia.ini | Inputs | UseFsr2Dx11Inputs | auto |
| OptiScaler_f5.nvidia.ini | Inputs | UseFsr2Inputs | auto |
| OptiScaler_f5.nvidia.ini | Inputs | UseFsr2VulkanInputs | auto |
| OptiScaler_f5.nvidia.ini | Inputs | UseFsr3Inputs | auto |
| OptiScaler_f5.nvidia.ini | Libraries | FfxDx12FGPath | auto |
| OptiScaler_f5.nvidia.ini | Libraries | FfxDx12Path | auto |
| OptiScaler_f5.nvidia.ini | Libraries | FfxDx12RCPath | auto |
| OptiScaler_f5.nvidia.ini | Libraries | FfxDx12RRPath | auto |
| OptiScaler_f5.nvidia.ini | Libraries | FfxDx12SRPath | auto |
| OptiScaler_f5.nvidia.ini | Libraries | FfxVkPath | auto |
| OptiScaler_f5.nvidia.ini | Libraries | NvapiPath | auto |
| OptiScaler_f5.nvidia.ini | Libraries | NvngxDlssPath | auto |
| OptiScaler_f5.nvidia.ini | Libraries | NvngxFeaturePath | auto |
| OptiScaler_f5.nvidia.ini | Libraries | NvngxPath | auto |
| OptiScaler_f5.nvidia.ini | Libraries | OptiDllPath | auto |
| OptiScaler_f5.nvidia.ini | Libraries | XeFGPath | auto |
| OptiScaler_f5.nvidia.ini | Libraries | XeLLPath | auto |
| OptiScaler_f5.nvidia.ini | Libraries | XeSSDx11Path | auto |
| OptiScaler_f5.nvidia.ini | Libraries | XeSSPath | auto |
| OptiScaler_f5.nvidia.ini | Log | LogAsync | auto |
| OptiScaler_f5.nvidia.ini | Log | LogAsyncThreads | auto |
| OptiScaler_f5.nvidia.ini | Log | LogFileName | auto |
| OptiScaler_f5.nvidia.ini | Log | LogLevel | 2 |
| OptiScaler_f5.nvidia.ini | Log | LogToConsole | auto |
| OptiScaler_f5.nvidia.ini | Log | LogToDebug | auto |
| OptiScaler_f5.nvidia.ini | Log | LogToFile | true |
| OptiScaler_f5.nvidia.ini | Log | LogToNGX | auto |
| OptiScaler_f5.nvidia.ini | Log | OpenConsole | auto |
| OptiScaler_f5.nvidia.ini | Log | SingleFile | auto |
| OptiScaler_f5.nvidia.ini | Magnifier | BorderSize | auto |
| OptiScaler_f5.nvidia.ini | Magnifier | CursorOffsetX | auto |
| OptiScaler_f5.nvidia.ini | Magnifier | CursorOffsetY | auto |
| OptiScaler_f5.nvidia.ini | Magnifier | Enabled | auto |
| OptiScaler_f5.nvidia.ini | Magnifier | Size | auto |
| OptiScaler_f5.nvidia.ini | Magnifier | StaticPosX | auto |
| OptiScaler_f5.nvidia.ini | Magnifier | StaticPosY | auto |
| OptiScaler_f5.nvidia.ini | Magnifier | ZoomFactor | auto |
| OptiScaler_f5.nvidia.ini | Menu | AccentColorB | auto |
| OptiScaler_f5.nvidia.ini | Menu | AccentColorG | auto |
| OptiScaler_f5.nvidia.ini | Menu | AccentColorR | auto |
| OptiScaler_f5.nvidia.ini | Menu | BGColorA | auto |
| OptiScaler_f5.nvidia.ini | Menu | BGColorB | auto |
| OptiScaler_f5.nvidia.ini | Menu | BGColorG | auto |
| OptiScaler_f5.nvidia.ini | Menu | BGColorR | auto |
| OptiScaler_f5.nvidia.ini | Menu | DisableSplash | auto |
| OptiScaler_f5.nvidia.ini | Menu | ExtendedLimits | auto |
| OptiScaler_f5.nvidia.ini | Menu | F5LowHint | true |
| OptiScaler_f5.nvidia.ini | Menu | FGShortcutKey | auto |
| OptiScaler_f5.nvidia.ini | Menu | FontSize | auto |
| OptiScaler_f5.nvidia.ini | Menu | FpsCycleShortcutKey | auto |
| OptiScaler_f5.nvidia.ini | Menu | FpsOverlayAlpha | auto |
| OptiScaler_f5.nvidia.ini | Menu | FpsOverlayHorizontal | auto |
| OptiScaler_f5.nvidia.ini | Menu | FpsOverlayPos | auto |
| OptiScaler_f5.nvidia.ini | Menu | FpsOverlayType | auto |
| OptiScaler_f5.nvidia.ini | Menu | FpsScale | auto |
| OptiScaler_f5.nvidia.ini | Menu | FpsShortcutKey | auto |
| OptiScaler_f5.nvidia.ini | Menu | LightTheme | auto |
| OptiScaler_f5.nvidia.ini | Menu | MenuHeight | auto |
| OptiScaler_f5.nvidia.ini | Menu | MenuPage | auto |
| OptiScaler_f5.nvidia.ini | Menu | MenuWidth | auto |
| OptiScaler_f5.nvidia.ini | Menu | ModernTheme | auto |
| OptiScaler_f5.nvidia.ini | Menu | OverlayMenu | auto |
| OptiScaler_f5.nvidia.ini | Menu | OverlaysUseTheme | auto |
| OptiScaler_f5.nvidia.ini | Menu | Scale | auto |
| OptiScaler_f5.nvidia.ini | Menu | ShortcutKey | auto |
| OptiScaler_f5.nvidia.ini | Menu | ShowFps | auto |
| OptiScaler_f5.nvidia.ini | Menu | TTFFontPath | auto |
| OptiScaler_f5.nvidia.ini | Menu | UseHQFont | auto |
| OptiScaler_f5.nvidia.ini | Mipmap | MipmapBiasFixedOverride | auto |
| OptiScaler_f5.nvidia.ini | Mipmap | MipmapBiasOverride | auto |
| OptiScaler_f5.nvidia.ini | Mipmap | MipmapBiasOverrideAll | auto |
| OptiScaler_f5.nvidia.ini | Mipmap | MipmapBiasScaleOverride | auto |
| OptiScaler_f5.nvidia.ini | NvApi | DisableFlipMetering | auto |
| OptiScaler_f5.nvidia.ini | NvngxFG | DisableHudless | auto |
| OptiScaler_f5.nvidia.ini | NvngxFG | DispatchFlags | auto |
| OptiScaler_f5.nvidia.ini | NvngxFG | MakeDepthCopy | auto |
| OptiScaler_f5.nvidia.ini | NvngxFG | ShowDebug | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | AlwaysCaptureFSRFGSwapchain | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | AlwaysTrackHeaps | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | DepthScaleMax | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | DisableHUDFix | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | EnableDepthScale | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | HUDFix | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | HudfixDisableDI | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | HudfixDisableDII | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | HudfixDisableDispatch | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | HudfixDisableOM | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | HudfixDisableRTV | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | HudfixDisableSCR | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | HudfixDisableSGR | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | HudfixDisableSRV | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | HudfixDisableUAV | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | HUDFixDontUseSwapchainBuffers | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | HUDFixExtended | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | HUDFixImmediate | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | HUDFixPersistentBindings | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | HUDFixRelaxedResolutionCheck | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | HUDLimit | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | MakeDepthCopy | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | MakeMVCopy | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | ResourceBlocking | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | ResourceFlip | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | ResourceFlipOffset | auto |
| OptiScaler_f5.nvidia.ini | OptiFG | UseShards | auto |
| OptiScaler_f5.nvidia.ini | OutputScaling | Downscaler | auto |
| OptiScaler_f5.nvidia.ini | OutputScaling | Enabled | auto |
| OptiScaler_f5.nvidia.ini | OutputScaling | Multiplier | auto |
| OptiScaler_f5.nvidia.ini | Plugins | LateAsiPluginsDelay | auto |
| OptiScaler_f5.nvidia.ini | Plugins | LoadAsiPlugins | auto |
| OptiScaler_f5.nvidia.ini | Plugins | LoadCustomAmdxc64OnRdna2 | auto |
| OptiScaler_f5.nvidia.ini | Plugins | LoadReshade | auto |
| OptiScaler_f5.nvidia.ini | Plugins | LoadSpecialK | auto |
| OptiScaler_f5.nvidia.ini | Plugins | Path | auto |
| OptiScaler_f5.nvidia.ini | ProcessFilter | ProcessExclusionList | auto |
| OptiScaler_f5.nvidia.ini | ProcessFilter | TargetProcessName | auto |
| OptiScaler_f5.nvidia.ini | QualityOverrides | QualityRatioBalanced | auto |
| OptiScaler_f5.nvidia.ini | QualityOverrides | QualityRatioDLAA | auto |
| OptiScaler_f5.nvidia.ini | QualityOverrides | QualityRatioOverrideEnabled | auto |
| OptiScaler_f5.nvidia.ini | QualityOverrides | QualityRatioPerformance | auto |
| OptiScaler_f5.nvidia.ini | QualityOverrides | QualityRatioQuality | auto |
| OptiScaler_f5.nvidia.ini | QualityOverrides | QualityRatioUltraPerformance | auto |
| OptiScaler_f5.nvidia.ini | QualityOverrides | QualityRatioUltraQuality | auto |
| OptiScaler_f5.nvidia.ini | Sharpness | OverrideSharpness | auto |
| OptiScaler_f5.nvidia.ini | Sharpness | Shader | auto |
| OptiScaler_f5.nvidia.ini | Sharpness | Sharpness | auto |
| OptiScaler_f5.nvidia.ini | Spoofing | D3DFeatureLevel | auto |
| OptiScaler_f5.nvidia.ini | Spoofing | Dxgi | auto |
| OptiScaler_f5.nvidia.ini | Spoofing | DxgiBlacklist | auto |
| OptiScaler_f5.nvidia.ini | Spoofing | DxgiFactoryWrapping | auto |
| OptiScaler_f5.nvidia.ini | Spoofing | DxgiVRAM | auto |
| OptiScaler_f5.nvidia.ini | Spoofing | Registry | auto |
| OptiScaler_f5.nvidia.ini | Spoofing | RegistryDriver | auto |
| OptiScaler_f5.nvidia.ini | Spoofing | SpoofedDeviceId | auto |
| OptiScaler_f5.nvidia.ini | Spoofing | SpoofedGPUName | auto |
| OptiScaler_f5.nvidia.ini | Spoofing | SpoofedVendorId | auto |
| OptiScaler_f5.nvidia.ini | Spoofing | SpoofHAGS | auto |
| OptiScaler_f5.nvidia.ini | Spoofing | StreamlineSpoofing | auto |
| OptiScaler_f5.nvidia.ini | Spoofing | TargetDeviceId | auto |
| OptiScaler_f5.nvidia.ini | Spoofing | TargetVendorId | auto |
| OptiScaler_f5.nvidia.ini | Spoofing | UEIntelAtomics | auto |
| OptiScaler_f5.nvidia.ini | Spoofing | User32 | auto |
| OptiScaler_f5.nvidia.ini | Spoofing | Vulkan | auto |
| OptiScaler_f5.nvidia.ini | Spoofing | VulkanExtensionSpoofing | auto |
| OptiScaler_f5.nvidia.ini | Spoofing | VulkanVRAM | auto |
| OptiScaler_f5.nvidia.ini | UpscaleRatio | UpscaleRatioOverrideEnabled | auto |
| OptiScaler_f5.nvidia.ini | UpscaleRatio | UpscaleRatioOverrideValue | auto |
| OptiScaler_f5.nvidia.ini | Upscalers | Dx11Upscaler | auto |
| OptiScaler_f5.nvidia.ini | Upscalers | Dx12Upscaler | auto |
| OptiScaler_f5.nvidia.ini | Upscalers | VulkanUpscaler | auto |
| OptiScaler_f5.nvidia.ini | V-Sync | ForceVsync | auto |
| OptiScaler_f5.nvidia.ini | V-Sync | OverrideVsync | auto |
| OptiScaler_f5.nvidia.ini | V-Sync | SyncInterval | auto |
| OptiScaler_f5.nvidia.ini | XeFG | DebugView | auto |
| OptiScaler_f5.nvidia.ini | XeFG | DepthInverted | auto |
| OptiScaler_f5.nvidia.ini | XeFG | ForceBorderless | auto |
| OptiScaler_f5.nvidia.ini | XeFG | HighResMV | auto |
| OptiScaler_f5.nvidia.ini | XeFG | IgnoreInitChecks | auto |
| OptiScaler_f5.nvidia.ini | XeFG | InterpolationCount | auto |
| OptiScaler_f5.nvidia.ini | XeFG | JitteredMV | auto |
| OptiScaler_f5.nvidia.ini | XeFG | UIComposition | auto |
| OptiScaler_f5.nvidia.ini | XeSS | BuildPipelines | auto |
| OptiScaler_f5.nvidia.ini | XeSS | CreateHeaps | auto |
| OptiScaler_f5.nvidia.ini | XeSS | NetworkModel | auto |
| OptiScaler_nightly.amd-dlss.ini | Anisotropy | AnisotropyOverride | auto |
| OptiScaler_nightly.amd-dlss.ini | Anisotropy | ModifyComparison | auto |
| OptiScaler_nightly.amd-dlss.ini | Anisotropy | ModifyMinMax | auto |
| OptiScaler_nightly.amd-dlss.ini | Anisotropy | SkipPointFilter | auto |
| OptiScaler_nightly.amd-dlss.ini | CAS | Contrast | auto |
| OptiScaler_nightly.amd-dlss.ini | CAS | ContrastEnabled | auto |
| OptiScaler_nightly.amd-dlss.ini | CAS | DAClampOutput | auto |
| OptiScaler_nightly.amd-dlss.ini | CAS | DADepthBias | auto |
| OptiScaler_nightly.amd-dlss.ini | CAS | DADepthScale | auto |
| OptiScaler_nightly.amd-dlss.ini | CAS | Enabled | auto |
| OptiScaler_nightly.amd-dlss.ini | CAS | MotionScaleLimit | auto |
| OptiScaler_nightly.amd-dlss.ini | CAS | MotionSharpness | auto |
| OptiScaler_nightly.amd-dlss.ini | CAS | MotionSharpnessEnabled | auto |
| OptiScaler_nightly.amd-dlss.ini | CAS | MotionThreshold | auto |
| OptiScaler_nightly.amd-dlss.ini | CAS | SharpenerDebug | auto |
| OptiScaler_nightly.amd-dlss.ini | DLSS | Enabled | auto |
| OptiScaler_nightly.amd-dlss.ini | DLSS | RenderPresetBalanced | auto |
| OptiScaler_nightly.amd-dlss.ini | DLSS | RenderPresetDLAA | auto |
| OptiScaler_nightly.amd-dlss.ini | DLSS | RenderPresetForAll | auto |
| OptiScaler_nightly.amd-dlss.ini | DLSS | RenderPresetOverride | auto |
| OptiScaler_nightly.amd-dlss.ini | DLSS | RenderPresetPerformance | auto |
| OptiScaler_nightly.amd-dlss.ini | DLSS | RenderPresetQuality | auto |
| OptiScaler_nightly.amd-dlss.ini | DLSS | RenderPresetUltraPerformance | auto |
| OptiScaler_nightly.amd-dlss.ini | DLSS | RenderPresetUltraQuality | auto |
| OptiScaler_nightly.amd-dlss.ini | DLSS | UseGenericAppIdWithDlss | auto |
| OptiScaler_nightly.amd-dlss.ini | DLSSD | RenderPresetBalanced | auto |
| OptiScaler_nightly.amd-dlss.ini | DLSSD | RenderPresetDLAA | auto |
| OptiScaler_nightly.amd-dlss.ini | DLSSD | RenderPresetForAll | auto |
| OptiScaler_nightly.amd-dlss.ini | DLSSD | RenderPresetOverride | auto |
| OptiScaler_nightly.amd-dlss.ini | DLSSD | RenderPresetPerformance | auto |
| OptiScaler_nightly.amd-dlss.ini | DLSSD | RenderPresetQuality | auto |
| OptiScaler_nightly.amd-dlss.ini | DLSSD | RenderPresetUltraPerformance | auto |
| OptiScaler_nightly.amd-dlss.ini | DLSSD | RenderPresetUltraQuality | auto |
| OptiScaler_nightly.amd-dlss.ini | DLSSG | FramerateTargetDMFG | auto |
| OptiScaler_nightly.amd-dlss.ini | DLSSG | InterpolationCount | auto |
| OptiScaler_nightly.amd-dlss.ini | DLSSG | OverrideForceDMFG | auto |
| OptiScaler_nightly.amd-dlss.ini | DLSSG | OverrideInterpolationCount | auto |
| OptiScaler_nightly.amd-dlss.ini | DLSSG | UseGamesReflexMarkers | auto |
| OptiScaler_nightly.amd-dlss.ini | DRS | DrsMaxOverrideEnabled | auto |
| OptiScaler_nightly.amd-dlss.ini | DRS | DrsMinOverrideEnabled | auto |
| OptiScaler_nightly.amd-dlss.ini | Dx11withDx12 | DontUseNTShared | auto |
| OptiScaler_nightly.amd-dlss.ini | Dx11withDx12 | UseDelayedInit | auto |
| OptiScaler_nightly.amd-dlss.ini | fakenvapi | ForceLatencyFlex | auto |
| OptiScaler_nightly.amd-dlss.ini | fakenvapi | ForceReflex | auto |
| OptiScaler_nightly.amd-dlss.ini | fakenvapi | ForceXeLL | auto |
| OptiScaler_nightly.amd-dlss.ini | fakenvapi | LatencyFlexMode | auto |
| OptiScaler_nightly.amd-dlss.ini | fakenvapi | UseFakenvapi | auto |
| OptiScaler_nightly.amd-dlss.ini | FrameGen | AllowedFrameAhead | auto |
| OptiScaler_nightly.amd-dlss.ini | FrameGen | DebugView | auto |
| OptiScaler_nightly.amd-dlss.ini | FrameGen | DepthValidNow | auto |
| OptiScaler_nightly.amd-dlss.ini | FrameGen | DisableHudless | auto |
| OptiScaler_nightly.amd-dlss.ini | FrameGen | DisableUI | auto |
| OptiScaler_nightly.amd-dlss.ini | FrameGen | DrawUIOverFG | auto |
| OptiScaler_nightly.amd-dlss.ini | FrameGen | Enabled | auto |
| OptiScaler_nightly.amd-dlss.ini | FrameGen | FGInput | auto |
| OptiScaler_nightly.amd-dlss.ini | FrameGen | FGNvngxReplacement | auto |
| OptiScaler_nightly.amd-dlss.ini | FrameGen | FGOutput | auto |
| OptiScaler_nightly.amd-dlss.ini | FrameGen | FTInput | auto |
| OptiScaler_nightly.amd-dlss.ini | FrameGen | HudCutoff | auto |
| OptiScaler_nightly.amd-dlss.ini | FrameGen | HudlessValidNow | auto |
| OptiScaler_nightly.amd-dlss.ini | FrameGen | ModifyBufferState | auto |
| OptiScaler_nightly.amd-dlss.ini | FrameGen | ModifySCIndex | auto |
| OptiScaler_nightly.amd-dlss.ini | FrameGen | OnlyAcceptFirstHudless | auto |
| OptiScaler_nightly.amd-dlss.ini | FrameGen | PreserveSwapChain | auto |
| OptiScaler_nightly.amd-dlss.ini | FrameGen | RectHeight | auto |
| OptiScaler_nightly.amd-dlss.ini | FrameGen | RectLeft | auto |
| OptiScaler_nightly.amd-dlss.ini | FrameGen | RectTop | auto |
| OptiScaler_nightly.amd-dlss.ini | FrameGen | RectWidth | auto |
| OptiScaler_nightly.amd-dlss.ini | FrameGen | SkipReset | auto |
| OptiScaler_nightly.amd-dlss.ini | FrameGen | SkipResizeBuffers | auto |
| OptiScaler_nightly.amd-dlss.ini | FrameGen | UIPremultipliedAlpha | auto |
| OptiScaler_nightly.amd-dlss.ini | FrameGen | VelocityValidNow | auto |
| OptiScaler_nightly.amd-dlss.ini | Framerate | FramerateLimit | auto |
| OptiScaler_nightly.amd-dlss.ini | FSR | AccAddPerFrame | auto |
| OptiScaler_nightly.amd-dlss.ini | FSR | CameraFar | auto |
| OptiScaler_nightly.amd-dlss.ini | FSR | CameraNear | auto |
| OptiScaler_nightly.amd-dlss.ini | FSR | DebugView | auto |
| OptiScaler_nightly.amd-dlss.ini | FSR | DlssReactiveMaskBias | auto |
| OptiScaler_nightly.amd-dlss.ini | FSR | FGIndex | auto |
| OptiScaler_nightly.amd-dlss.ini | FSR | Fsr4DoNotLoadAmdxc64 | auto |
| OptiScaler_nightly.amd-dlss.ini | FSR | Fsr4EnableWatermark | auto |
| OptiScaler_nightly.amd-dlss.ini | FSR | Fsr4ForceModel | auto |
| OptiScaler_nightly.amd-dlss.ini | FSR | Fsr4Preset | auto |
| OptiScaler_nightly.amd-dlss.ini | FSR | FsrAgilitySDKUpgrade | auto |
| OptiScaler_nightly.amd-dlss.ini | FSR | FsrNonLinearColorSpace | auto |
| OptiScaler_nightly.amd-dlss.ini | FSR | FsrNonLinearPQ | auto |
| OptiScaler_nightly.amd-dlss.ini | FSR | FsrNonLinearSRGB | auto |
| OptiScaler_nightly.amd-dlss.ini | FSR | HorizontalFov | auto |
| OptiScaler_nightly.amd-dlss.ini | FSR | MinDisOccAcc | auto |
| OptiScaler_nightly.amd-dlss.ini | FSR | ReactiveScale | auto |
| OptiScaler_nightly.amd-dlss.ini | FSR | ShadingScale | auto |
| OptiScaler_nightly.amd-dlss.ini | FSR | UpscalerIndex | auto |
| OptiScaler_nightly.amd-dlss.ini | FSR | UseFsrInputValues | auto |
| OptiScaler_nightly.amd-dlss.ini | FSR | UseReactiveMaskForTransparency | auto |
| OptiScaler_nightly.amd-dlss.ini | FSR | VelocityFactor | auto |
| OptiScaler_nightly.amd-dlss.ini | FSR | VerticalFov | auto |
| OptiScaler_nightly.amd-dlss.ini | FSRFG | AllowAsync | auto |
| OptiScaler_nightly.amd-dlss.ini | FSRFG | DebugPacingLines | auto |
| OptiScaler_nightly.amd-dlss.ini | FSRFG | DebugResetLines | auto |
| OptiScaler_nightly.amd-dlss.ini | FSRFG | DebugTearLines | auto |
| OptiScaler_nightly.amd-dlss.ini | FSRFG | EnableWatermark | auto |
| OptiScaler_nightly.amd-dlss.ini | FSRFG | FPTHybridSpin | auto |
| OptiScaler_nightly.amd-dlss.ini | FSRFG | FPTHybridSpinTime | auto |
| OptiScaler_nightly.amd-dlss.ini | FSRFG | FPTSafetyMarginInMs | auto |
| OptiScaler_nightly.amd-dlss.ini | FSRFG | FPTVarianceFactor | auto |
| OptiScaler_nightly.amd-dlss.ini | FSRFG | FPTWaitForSingleObjectOnFence | auto |
| OptiScaler_nightly.amd-dlss.ini | FSRFG | FramePacingTuning | auto |
| OptiScaler_nightly.amd-dlss.ini | FSRFG | UseMutexForSwapchain | auto |
| OptiScaler_nightly.amd-dlss.ini | FSRFGInputs | SkipConfigForHudless | auto |
| OptiScaler_nightly.amd-dlss.ini | FSRFGInputs | SkipDispatchForHudless | auto |
| OptiScaler_nightly.amd-dlss.ini | HDR | ForceHDR | auto |
| OptiScaler_nightly.amd-dlss.ini | HDR | SkipColorSpace | auto |
| OptiScaler_nightly.amd-dlss.ini | HDR | UseHDR10 | auto |
| OptiScaler_nightly.amd-dlss.ini | Hooks | EarlyHooking | auto |
| OptiScaler_nightly.amd-dlss.ini | Hooks | HookOriginalNvngxOnly | auto |
| OptiScaler_nightly.amd-dlss.ini | Hooks | UseNtdllHooks | auto |
| OptiScaler_nightly.amd-dlss.ini | Hotfix | CheckForUpdate | auto |
| OptiScaler_nightly.amd-dlss.ini | Hotfix | ColorMaskResourceBarrier | auto |
| OptiScaler_nightly.amd-dlss.ini | Hotfix | ColorResourceBarrier | auto |
| OptiScaler_nightly.amd-dlss.ini | Hotfix | CreateD3D12DeviceForLuma | auto |
| OptiScaler_nightly.amd-dlss.ini | Hotfix | DepthResourceBarrier | auto |
| OptiScaler_nightly.amd-dlss.ini | Hotfix | DisableOverlays | auto |
| OptiScaler_nightly.amd-dlss.ini | Hotfix | ExposureResourceBarrier | auto |
| OptiScaler_nightly.amd-dlss.ini | Hotfix | ExtendedStateRestore | auto |
| OptiScaler_nightly.amd-dlss.ini | Hotfix | ManualInputPolling | auto |
| OptiScaler_nightly.amd-dlss.ini | Hotfix | MotionVectorResourceBarrier | auto |
| OptiScaler_nightly.amd-dlss.ini | Hotfix | OutputResourceBarrier | auto |
| OptiScaler_nightly.amd-dlss.ini | Hotfix | PreferDedicatedGpu | auto |
| OptiScaler_nightly.amd-dlss.ini | Hotfix | PreferFirstDedicatedGpu | auto |
| OptiScaler_nightly.amd-dlss.ini | Hotfix | RestoreComputeSignature | auto |
| OptiScaler_nightly.amd-dlss.ini | Hotfix | RestoreGraphicSignature | auto |
| OptiScaler_nightly.amd-dlss.ini | Hotfix | RoundInternalResolution | auto |
| OptiScaler_nightly.amd-dlss.ini | Hotfix | SimulateWaitableObject | auto |
| OptiScaler_nightly.amd-dlss.ini | Hotfix | SkipFirstFrames | auto |
| OptiScaler_nightly.amd-dlss.ini | Hotfix | UsePrecompiledShaders | auto |
| OptiScaler_nightly.amd-dlss.ini | InitFlags | AutoExposure | auto |
| OptiScaler_nightly.amd-dlss.ini | InitFlags | DepthInverted | auto |
| OptiScaler_nightly.amd-dlss.ini | InitFlags | DisableReactiveMask | auto |
| OptiScaler_nightly.amd-dlss.ini | InitFlags | DisplayResolution | auto |
| OptiScaler_nightly.amd-dlss.ini | InitFlags | HDR | auto |
| OptiScaler_nightly.amd-dlss.ini | InitFlags | JitterCancellation | auto |
| OptiScaler_nightly.amd-dlss.ini | Inputs | EnableDlssInputs | auto |
| OptiScaler_nightly.amd-dlss.ini | Inputs | EnableFfxInputs | auto |
| OptiScaler_nightly.amd-dlss.ini | Inputs | EnableFsr2Inputs | auto |
| OptiScaler_nightly.amd-dlss.ini | Inputs | EnableFsr3Inputs | auto |
| OptiScaler_nightly.amd-dlss.ini | Inputs | EnableHotSwapping | auto |
| OptiScaler_nightly.amd-dlss.ini | Inputs | EnableXeSSInputs | auto |
| OptiScaler_nightly.amd-dlss.ini | Inputs | Fsr2Pattern | auto |
| OptiScaler_nightly.amd-dlss.ini | Inputs | Fsr3Pattern | auto |
| OptiScaler_nightly.amd-dlss.ini | Inputs | UseFfxInputs | auto |
| OptiScaler_nightly.amd-dlss.ini | Inputs | UseFsr2Dx11Inputs | auto |
| OptiScaler_nightly.amd-dlss.ini | Inputs | UseFsr2Inputs | auto |
| OptiScaler_nightly.amd-dlss.ini | Inputs | UseFsr2VulkanInputs | auto |
| OptiScaler_nightly.amd-dlss.ini | Inputs | UseFsr3Inputs | auto |
| OptiScaler_nightly.amd-dlss.ini | Libraries | FfxDx12FGPath | auto |
| OptiScaler_nightly.amd-dlss.ini | Libraries | FfxDx12Path | auto |
| OptiScaler_nightly.amd-dlss.ini | Libraries | FfxDx12RCPath | auto |
| OptiScaler_nightly.amd-dlss.ini | Libraries | FfxDx12RRPath | auto |
| OptiScaler_nightly.amd-dlss.ini | Libraries | FfxDx12SRPath | auto |
| OptiScaler_nightly.amd-dlss.ini | Libraries | FfxVkPath | auto |
| OptiScaler_nightly.amd-dlss.ini | Libraries | NvapiPath | auto |
| OptiScaler_nightly.amd-dlss.ini | Libraries | NvngxDlssPath | auto |
| OptiScaler_nightly.amd-dlss.ini | Libraries | NvngxFeaturePath | auto |
| OptiScaler_nightly.amd-dlss.ini | Libraries | NvngxPath | auto |
| OptiScaler_nightly.amd-dlss.ini | Libraries | OptiDllPath | auto |
| OptiScaler_nightly.amd-dlss.ini | Libraries | XeFGPath | auto |
| OptiScaler_nightly.amd-dlss.ini | Libraries | XeLLPath | auto |
| OptiScaler_nightly.amd-dlss.ini | Libraries | XeSSDx11Path | auto |
| OptiScaler_nightly.amd-dlss.ini | Libraries | XeSSPath | auto |
| OptiScaler_nightly.amd-dlss.ini | Log | LogAsync | auto |
| OptiScaler_nightly.amd-dlss.ini | Log | LogAsyncThreads | auto |
| OptiScaler_nightly.amd-dlss.ini | Log | LogFileName | auto |
| OptiScaler_nightly.amd-dlss.ini | Log | LogLevel | auto |
| OptiScaler_nightly.amd-dlss.ini | Log | LogToConsole | auto |
| OptiScaler_nightly.amd-dlss.ini | Log | LogToDebug | auto |
| OptiScaler_nightly.amd-dlss.ini | Log | LogToFile | auto |
| OptiScaler_nightly.amd-dlss.ini | Log | LogToNGX | auto |
| OptiScaler_nightly.amd-dlss.ini | Log | OpenConsole | auto |
| OptiScaler_nightly.amd-dlss.ini | Log | SingleFile | auto |
| OptiScaler_nightly.amd-dlss.ini | Magnifier | BorderSize | auto |
| OptiScaler_nightly.amd-dlss.ini | Magnifier | CursorOffsetX | auto |
| OptiScaler_nightly.amd-dlss.ini | Magnifier | CursorOffsetY | auto |
| OptiScaler_nightly.amd-dlss.ini | Magnifier | Enabled | auto |
| OptiScaler_nightly.amd-dlss.ini | Magnifier | Size | auto |
| OptiScaler_nightly.amd-dlss.ini | Magnifier | StaticPosX | auto |
| OptiScaler_nightly.amd-dlss.ini | Magnifier | StaticPosY | auto |
| OptiScaler_nightly.amd-dlss.ini | Magnifier | ZoomFactor | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | AccentColorB | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | AccentColorG | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | AccentColorR | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | BGColorA | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | BGColorB | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | BGColorG | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | BGColorR | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | DisableSplash | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | ExtendedLimits | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | FGShortcutKey | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | FontSize | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | FpsCycleShortcutKey | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | FpsOverlayAlpha | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | FpsOverlayHorizontal | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | FpsOverlayPos | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | FpsOverlayType | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | FpsScale | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | FpsShortcutKey | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | LightTheme | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | OverlayMenu | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | OverlaysUseTheme | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | Scale | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | ShortcutKey | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | ShowFps | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | TTFFontPath | auto |
| OptiScaler_nightly.amd-dlss.ini | Menu | UseHQFont | auto |
| OptiScaler_nightly.amd-dlss.ini | Mipmap | MipmapBiasFixedOverride | auto |
| OptiScaler_nightly.amd-dlss.ini | Mipmap | MipmapBiasOverride | auto |
| OptiScaler_nightly.amd-dlss.ini | Mipmap | MipmapBiasOverrideAll | auto |
| OptiScaler_nightly.amd-dlss.ini | Mipmap | MipmapBiasScaleOverride | auto |
| OptiScaler_nightly.amd-dlss.ini | NvApi | DisableFlipMetering | auto |
| OptiScaler_nightly.amd-dlss.ini | NvngxFG | DisableHudless | auto |
| OptiScaler_nightly.amd-dlss.ini | NvngxFG | DispatchFlags | auto |
| OptiScaler_nightly.amd-dlss.ini | NvngxFG | MakeDepthCopy | auto |
| OptiScaler_nightly.amd-dlss.ini | NvngxFG | ShowDebug | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | AlwaysCaptureFSRFGSwapchain | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | AlwaysTrackHeaps | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | DepthScaleMax | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | DisableHUDFix | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | EnableDepthScale | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | HUDFix | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | HudfixDisableDI | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | HudfixDisableDII | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | HudfixDisableDispatch | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | HudfixDisableOM | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | HudfixDisableRTV | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | HudfixDisableSCR | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | HudfixDisableSGR | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | HudfixDisableSRV | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | HudfixDisableUAV | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | HUDFixDontUseSwapchainBuffers | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | HUDFixExtended | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | HUDFixImmediate | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | HUDFixRelaxedResolutionCheck | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | HUDLimit | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | MakeDepthCopy | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | MakeMVCopy | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | ResourceBlocking | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | ResourceFlip | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | ResourceFlipOffset | auto |
| OptiScaler_nightly.amd-dlss.ini | OptiFG | UseShards | auto |
| OptiScaler_nightly.amd-dlss.ini | OutputScaling | Downscaler | auto |
| OptiScaler_nightly.amd-dlss.ini | OutputScaling | Enabled | auto |
| OptiScaler_nightly.amd-dlss.ini | OutputScaling | Multiplier | auto |
| OptiScaler_nightly.amd-dlss.ini | Plugins | LateAsiPluginsDelay | auto |
| OptiScaler_nightly.amd-dlss.ini | Plugins | LoadAsiPlugins | auto |
| OptiScaler_nightly.amd-dlss.ini | Plugins | LoadCustomAmdxc64OnRdna2 | auto |
| OptiScaler_nightly.amd-dlss.ini | Plugins | LoadReshade | auto |
| OptiScaler_nightly.amd-dlss.ini | Plugins | LoadSpecialK | auto |
| OptiScaler_nightly.amd-dlss.ini | Plugins | Path | auto |
| OptiScaler_nightly.amd-dlss.ini | ProcessFilter | ProcessExclusionList | auto |
| OptiScaler_nightly.amd-dlss.ini | ProcessFilter | TargetProcessName | auto |
| OptiScaler_nightly.amd-dlss.ini | QualityOverrides | QualityRatioBalanced | auto |
| OptiScaler_nightly.amd-dlss.ini | QualityOverrides | QualityRatioDLAA | auto |
| OptiScaler_nightly.amd-dlss.ini | QualityOverrides | QualityRatioOverrideEnabled | auto |
| OptiScaler_nightly.amd-dlss.ini | QualityOverrides | QualityRatioPerformance | auto |
| OptiScaler_nightly.amd-dlss.ini | QualityOverrides | QualityRatioQuality | auto |
| OptiScaler_nightly.amd-dlss.ini | QualityOverrides | QualityRatioUltraPerformance | auto |
| OptiScaler_nightly.amd-dlss.ini | QualityOverrides | QualityRatioUltraQuality | auto |
| OptiScaler_nightly.amd-dlss.ini | Sharpness | OverrideSharpness | auto |
| OptiScaler_nightly.amd-dlss.ini | Sharpness | Shader | auto |
| OptiScaler_nightly.amd-dlss.ini | Sharpness | Sharpness | auto |
| OptiScaler_nightly.amd-dlss.ini | Spoofing | D3DFeatureLevel | auto |
| OptiScaler_nightly.amd-dlss.ini | Spoofing | Dxgi | auto |
| OptiScaler_nightly.amd-dlss.ini | Spoofing | DxgiBlacklist | auto |
| OptiScaler_nightly.amd-dlss.ini | Spoofing | DxgiFactoryWrapping | auto |
| OptiScaler_nightly.amd-dlss.ini | Spoofing | DxgiVRAM | auto |
| OptiScaler_nightly.amd-dlss.ini | Spoofing | Registry | auto |
| OptiScaler_nightly.amd-dlss.ini | Spoofing | RegistryDriver | auto |
| OptiScaler_nightly.amd-dlss.ini | Spoofing | SpoofedDeviceId | auto |
| OptiScaler_nightly.amd-dlss.ini | Spoofing | SpoofedGPUName | auto |
| OptiScaler_nightly.amd-dlss.ini | Spoofing | SpoofedVendorId | auto |
| OptiScaler_nightly.amd-dlss.ini | Spoofing | SpoofHAGS | auto |
| OptiScaler_nightly.amd-dlss.ini | Spoofing | StreamlineSpoofing | auto |
| OptiScaler_nightly.amd-dlss.ini | Spoofing | TargetDeviceId | auto |
| OptiScaler_nightly.amd-dlss.ini | Spoofing | TargetVendorId | auto |
| OptiScaler_nightly.amd-dlss.ini | Spoofing | UEIntelAtomics | auto |
| OptiScaler_nightly.amd-dlss.ini | Spoofing | User32 | auto |
| OptiScaler_nightly.amd-dlss.ini | Spoofing | Vulkan | auto |
| OptiScaler_nightly.amd-dlss.ini | Spoofing | VulkanExtensionSpoofing | auto |
| OptiScaler_nightly.amd-dlss.ini | Spoofing | VulkanVRAM | auto |
| OptiScaler_nightly.amd-dlss.ini | UpscaleRatio | UpscaleRatioOverrideEnabled | auto |
| OptiScaler_nightly.amd-dlss.ini | UpscaleRatio | UpscaleRatioOverrideValue | auto |
| OptiScaler_nightly.amd-dlss.ini | Upscalers | Dx11Upscaler | auto |
| OptiScaler_nightly.amd-dlss.ini | Upscalers | Dx12Upscaler | auto |
| OptiScaler_nightly.amd-dlss.ini | Upscalers | VulkanUpscaler | auto |
| OptiScaler_nightly.amd-dlss.ini | V-Sync | ForceVsync | auto |
| OptiScaler_nightly.amd-dlss.ini | V-Sync | OverrideVsync | auto |
| OptiScaler_nightly.amd-dlss.ini | V-Sync | SyncInterval | auto |
| OptiScaler_nightly.amd-dlss.ini | XeFG | DebugView | auto |
| OptiScaler_nightly.amd-dlss.ini | XeFG | DepthInverted | auto |
| OptiScaler_nightly.amd-dlss.ini | XeFG | ForceBorderless | auto |
| OptiScaler_nightly.amd-dlss.ini | XeFG | HighResMV | auto |
| OptiScaler_nightly.amd-dlss.ini | XeFG | IgnoreInitChecks | auto |
| OptiScaler_nightly.amd-dlss.ini | XeFG | InterpolationCount | auto |
| OptiScaler_nightly.amd-dlss.ini | XeFG | JitteredMV | auto |
| OptiScaler_nightly.amd-dlss.ini | XeFG | UIComposition | auto |
| OptiScaler_nightly.amd-dlss.ini | XeSS | BuildPipelines | auto |
| OptiScaler_nightly.amd-dlss.ini | XeSS | CreateHeaps | auto |
| OptiScaler_nightly.amd-dlss.ini | XeSS | NetworkModel | auto |
| OptiScaler_nightly.amd-nodlss.ini | Anisotropy | AnisotropyOverride | auto |
| OptiScaler_nightly.amd-nodlss.ini | Anisotropy | ModifyComparison | auto |
| OptiScaler_nightly.amd-nodlss.ini | Anisotropy | ModifyMinMax | auto |
| OptiScaler_nightly.amd-nodlss.ini | Anisotropy | SkipPointFilter | auto |
| OptiScaler_nightly.amd-nodlss.ini | CAS | Contrast | auto |
| OptiScaler_nightly.amd-nodlss.ini | CAS | ContrastEnabled | auto |
| OptiScaler_nightly.amd-nodlss.ini | CAS | DAClampOutput | auto |
| OptiScaler_nightly.amd-nodlss.ini | CAS | DADepthBias | auto |
| OptiScaler_nightly.amd-nodlss.ini | CAS | DADepthScale | auto |
| OptiScaler_nightly.amd-nodlss.ini | CAS | Enabled | auto |
| OptiScaler_nightly.amd-nodlss.ini | CAS | MotionScaleLimit | auto |
| OptiScaler_nightly.amd-nodlss.ini | CAS | MotionSharpness | auto |
| OptiScaler_nightly.amd-nodlss.ini | CAS | MotionSharpnessEnabled | auto |
| OptiScaler_nightly.amd-nodlss.ini | CAS | MotionThreshold | auto |
| OptiScaler_nightly.amd-nodlss.ini | CAS | SharpenerDebug | auto |
| OptiScaler_nightly.amd-nodlss.ini | DLSS | Enabled | auto |
| OptiScaler_nightly.amd-nodlss.ini | DLSS | RenderPresetBalanced | auto |
| OptiScaler_nightly.amd-nodlss.ini | DLSS | RenderPresetDLAA | auto |
| OptiScaler_nightly.amd-nodlss.ini | DLSS | RenderPresetForAll | auto |
| OptiScaler_nightly.amd-nodlss.ini | DLSS | RenderPresetOverride | auto |
| OptiScaler_nightly.amd-nodlss.ini | DLSS | RenderPresetPerformance | auto |
| OptiScaler_nightly.amd-nodlss.ini | DLSS | RenderPresetQuality | auto |
| OptiScaler_nightly.amd-nodlss.ini | DLSS | RenderPresetUltraPerformance | auto |
| OptiScaler_nightly.amd-nodlss.ini | DLSS | RenderPresetUltraQuality | auto |
| OptiScaler_nightly.amd-nodlss.ini | DLSS | UseGenericAppIdWithDlss | auto |
| OptiScaler_nightly.amd-nodlss.ini | DLSSD | RenderPresetBalanced | auto |
| OptiScaler_nightly.amd-nodlss.ini | DLSSD | RenderPresetDLAA | auto |
| OptiScaler_nightly.amd-nodlss.ini | DLSSD | RenderPresetForAll | auto |
| OptiScaler_nightly.amd-nodlss.ini | DLSSD | RenderPresetOverride | auto |
| OptiScaler_nightly.amd-nodlss.ini | DLSSD | RenderPresetPerformance | auto |
| OptiScaler_nightly.amd-nodlss.ini | DLSSD | RenderPresetQuality | auto |
| OptiScaler_nightly.amd-nodlss.ini | DLSSD | RenderPresetUltraPerformance | auto |
| OptiScaler_nightly.amd-nodlss.ini | DLSSD | RenderPresetUltraQuality | auto |
| OptiScaler_nightly.amd-nodlss.ini | DLSSG | FramerateTargetDMFG | auto |
| OptiScaler_nightly.amd-nodlss.ini | DLSSG | InterpolationCount | auto |
| OptiScaler_nightly.amd-nodlss.ini | DLSSG | OverrideForceDMFG | auto |
| OptiScaler_nightly.amd-nodlss.ini | DLSSG | OverrideInterpolationCount | auto |
| OptiScaler_nightly.amd-nodlss.ini | DLSSG | UseGamesReflexMarkers | auto |
| OptiScaler_nightly.amd-nodlss.ini | DRS | DrsMaxOverrideEnabled | auto |
| OptiScaler_nightly.amd-nodlss.ini | DRS | DrsMinOverrideEnabled | auto |
| OptiScaler_nightly.amd-nodlss.ini | Dx11withDx12 | DontUseNTShared | auto |
| OptiScaler_nightly.amd-nodlss.ini | Dx11withDx12 | UseDelayedInit | auto |
| OptiScaler_nightly.amd-nodlss.ini | fakenvapi | ForceLatencyFlex | auto |
| OptiScaler_nightly.amd-nodlss.ini | fakenvapi | ForceReflex | auto |
| OptiScaler_nightly.amd-nodlss.ini | fakenvapi | ForceXeLL | auto |
| OptiScaler_nightly.amd-nodlss.ini | fakenvapi | LatencyFlexMode | auto |
| OptiScaler_nightly.amd-nodlss.ini | fakenvapi | UseFakenvapi | auto |
| OptiScaler_nightly.amd-nodlss.ini | FrameGen | AllowedFrameAhead | auto |
| OptiScaler_nightly.amd-nodlss.ini | FrameGen | DebugView | auto |
| OptiScaler_nightly.amd-nodlss.ini | FrameGen | DepthValidNow | auto |
| OptiScaler_nightly.amd-nodlss.ini | FrameGen | DisableHudless | auto |
| OptiScaler_nightly.amd-nodlss.ini | FrameGen | DisableUI | auto |
| OptiScaler_nightly.amd-nodlss.ini | FrameGen | DrawUIOverFG | auto |
| OptiScaler_nightly.amd-nodlss.ini | FrameGen | Enabled | auto |
| OptiScaler_nightly.amd-nodlss.ini | FrameGen | FGInput | auto |
| OptiScaler_nightly.amd-nodlss.ini | FrameGen | FGNvngxReplacement | auto |
| OptiScaler_nightly.amd-nodlss.ini | FrameGen | FGOutput | auto |
| OptiScaler_nightly.amd-nodlss.ini | FrameGen | FTInput | auto |
| OptiScaler_nightly.amd-nodlss.ini | FrameGen | HudCutoff | auto |
| OptiScaler_nightly.amd-nodlss.ini | FrameGen | HudlessValidNow | auto |
| OptiScaler_nightly.amd-nodlss.ini | FrameGen | ModifyBufferState | auto |
| OptiScaler_nightly.amd-nodlss.ini | FrameGen | ModifySCIndex | auto |
| OptiScaler_nightly.amd-nodlss.ini | FrameGen | OnlyAcceptFirstHudless | auto |
| OptiScaler_nightly.amd-nodlss.ini | FrameGen | PreserveSwapChain | auto |
| OptiScaler_nightly.amd-nodlss.ini | FrameGen | RectHeight | auto |
| OptiScaler_nightly.amd-nodlss.ini | FrameGen | RectLeft | auto |
| OptiScaler_nightly.amd-nodlss.ini | FrameGen | RectTop | auto |
| OptiScaler_nightly.amd-nodlss.ini | FrameGen | RectWidth | auto |
| OptiScaler_nightly.amd-nodlss.ini | FrameGen | SkipReset | auto |
| OptiScaler_nightly.amd-nodlss.ini | FrameGen | SkipResizeBuffers | auto |
| OptiScaler_nightly.amd-nodlss.ini | FrameGen | UIPremultipliedAlpha | auto |
| OptiScaler_nightly.amd-nodlss.ini | FrameGen | VelocityValidNow | auto |
| OptiScaler_nightly.amd-nodlss.ini | Framerate | FramerateLimit | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSR | AccAddPerFrame | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSR | CameraFar | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSR | CameraNear | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSR | DebugView | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSR | DlssReactiveMaskBias | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSR | FGIndex | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSR | Fsr4DoNotLoadAmdxc64 | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSR | Fsr4EnableWatermark | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSR | Fsr4ForceModel | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSR | Fsr4Preset | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSR | FsrAgilitySDKUpgrade | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSR | FsrNonLinearColorSpace | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSR | FsrNonLinearPQ | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSR | FsrNonLinearSRGB | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSR | HorizontalFov | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSR | MinDisOccAcc | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSR | ReactiveScale | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSR | ShadingScale | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSR | UpscalerIndex | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSR | UseFsrInputValues | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSR | UseReactiveMaskForTransparency | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSR | VelocityFactor | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSR | VerticalFov | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSRFG | AllowAsync | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSRFG | DebugPacingLines | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSRFG | DebugResetLines | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSRFG | DebugTearLines | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSRFG | EnableWatermark | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSRFG | FPTHybridSpin | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSRFG | FPTHybridSpinTime | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSRFG | FPTSafetyMarginInMs | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSRFG | FPTVarianceFactor | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSRFG | FPTWaitForSingleObjectOnFence | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSRFG | FramePacingTuning | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSRFG | UseMutexForSwapchain | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSRFGInputs | SkipConfigForHudless | auto |
| OptiScaler_nightly.amd-nodlss.ini | FSRFGInputs | SkipDispatchForHudless | auto |
| OptiScaler_nightly.amd-nodlss.ini | HDR | ForceHDR | auto |
| OptiScaler_nightly.amd-nodlss.ini | HDR | SkipColorSpace | auto |
| OptiScaler_nightly.amd-nodlss.ini | HDR | UseHDR10 | auto |
| OptiScaler_nightly.amd-nodlss.ini | Hooks | EarlyHooking | auto |
| OptiScaler_nightly.amd-nodlss.ini | Hooks | HookOriginalNvngxOnly | auto |
| OptiScaler_nightly.amd-nodlss.ini | Hooks | UseNtdllHooks | auto |
| OptiScaler_nightly.amd-nodlss.ini | Hotfix | CheckForUpdate | auto |
| OptiScaler_nightly.amd-nodlss.ini | Hotfix | ColorMaskResourceBarrier | auto |
| OptiScaler_nightly.amd-nodlss.ini | Hotfix | ColorResourceBarrier | auto |
| OptiScaler_nightly.amd-nodlss.ini | Hotfix | CreateD3D12DeviceForLuma | auto |
| OptiScaler_nightly.amd-nodlss.ini | Hotfix | DepthResourceBarrier | auto |
| OptiScaler_nightly.amd-nodlss.ini | Hotfix | DisableOverlays | auto |
| OptiScaler_nightly.amd-nodlss.ini | Hotfix | ExposureResourceBarrier | auto |
| OptiScaler_nightly.amd-nodlss.ini | Hotfix | ExtendedStateRestore | auto |
| OptiScaler_nightly.amd-nodlss.ini | Hotfix | ManualInputPolling | auto |
| OptiScaler_nightly.amd-nodlss.ini | Hotfix | MotionVectorResourceBarrier | auto |
| OptiScaler_nightly.amd-nodlss.ini | Hotfix | OutputResourceBarrier | auto |
| OptiScaler_nightly.amd-nodlss.ini | Hotfix | PreferDedicatedGpu | auto |
| OptiScaler_nightly.amd-nodlss.ini | Hotfix | PreferFirstDedicatedGpu | auto |
| OptiScaler_nightly.amd-nodlss.ini | Hotfix | RestoreComputeSignature | auto |
| OptiScaler_nightly.amd-nodlss.ini | Hotfix | RestoreGraphicSignature | auto |
| OptiScaler_nightly.amd-nodlss.ini | Hotfix | RoundInternalResolution | auto |
| OptiScaler_nightly.amd-nodlss.ini | Hotfix | SimulateWaitableObject | auto |
| OptiScaler_nightly.amd-nodlss.ini | Hotfix | SkipFirstFrames | auto |
| OptiScaler_nightly.amd-nodlss.ini | Hotfix | UsePrecompiledShaders | auto |
| OptiScaler_nightly.amd-nodlss.ini | InitFlags | AutoExposure | auto |
| OptiScaler_nightly.amd-nodlss.ini | InitFlags | DepthInverted | auto |
| OptiScaler_nightly.amd-nodlss.ini | InitFlags | DisableReactiveMask | auto |
| OptiScaler_nightly.amd-nodlss.ini | InitFlags | DisplayResolution | auto |
| OptiScaler_nightly.amd-nodlss.ini | InitFlags | HDR | auto |
| OptiScaler_nightly.amd-nodlss.ini | InitFlags | JitterCancellation | auto |
| OptiScaler_nightly.amd-nodlss.ini | Inputs | EnableDlssInputs | auto |
| OptiScaler_nightly.amd-nodlss.ini | Inputs | EnableFfxInputs | auto |
| OptiScaler_nightly.amd-nodlss.ini | Inputs | EnableFsr2Inputs | auto |
| OptiScaler_nightly.amd-nodlss.ini | Inputs | EnableFsr3Inputs | auto |
| OptiScaler_nightly.amd-nodlss.ini | Inputs | EnableHotSwapping | auto |
| OptiScaler_nightly.amd-nodlss.ini | Inputs | EnableXeSSInputs | auto |
| OptiScaler_nightly.amd-nodlss.ini | Inputs | Fsr2Pattern | auto |
| OptiScaler_nightly.amd-nodlss.ini | Inputs | Fsr3Pattern | auto |
| OptiScaler_nightly.amd-nodlss.ini | Inputs | UseFfxInputs | auto |
| OptiScaler_nightly.amd-nodlss.ini | Inputs | UseFsr2Dx11Inputs | auto |
| OptiScaler_nightly.amd-nodlss.ini | Inputs | UseFsr2Inputs | auto |
| OptiScaler_nightly.amd-nodlss.ini | Inputs | UseFsr2VulkanInputs | auto |
| OptiScaler_nightly.amd-nodlss.ini | Inputs | UseFsr3Inputs | auto |
| OptiScaler_nightly.amd-nodlss.ini | Libraries | FfxDx12FGPath | auto |
| OptiScaler_nightly.amd-nodlss.ini | Libraries | FfxDx12Path | auto |
| OptiScaler_nightly.amd-nodlss.ini | Libraries | FfxDx12RCPath | auto |
| OptiScaler_nightly.amd-nodlss.ini | Libraries | FfxDx12RRPath | auto |
| OptiScaler_nightly.amd-nodlss.ini | Libraries | FfxDx12SRPath | auto |
| OptiScaler_nightly.amd-nodlss.ini | Libraries | FfxVkPath | auto |
| OptiScaler_nightly.amd-nodlss.ini | Libraries | NvapiPath | auto |
| OptiScaler_nightly.amd-nodlss.ini | Libraries | NvngxDlssPath | auto |
| OptiScaler_nightly.amd-nodlss.ini | Libraries | NvngxFeaturePath | auto |
| OptiScaler_nightly.amd-nodlss.ini | Libraries | NvngxPath | auto |
| OptiScaler_nightly.amd-nodlss.ini | Libraries | OptiDllPath | auto |
| OptiScaler_nightly.amd-nodlss.ini | Libraries | XeFGPath | auto |
| OptiScaler_nightly.amd-nodlss.ini | Libraries | XeLLPath | auto |
| OptiScaler_nightly.amd-nodlss.ini | Libraries | XeSSDx11Path | auto |
| OptiScaler_nightly.amd-nodlss.ini | Libraries | XeSSPath | auto |
| OptiScaler_nightly.amd-nodlss.ini | Log | LogAsync | auto |
| OptiScaler_nightly.amd-nodlss.ini | Log | LogAsyncThreads | auto |
| OptiScaler_nightly.amd-nodlss.ini | Log | LogFileName | auto |
| OptiScaler_nightly.amd-nodlss.ini | Log | LogLevel | auto |
| OptiScaler_nightly.amd-nodlss.ini | Log | LogToConsole | auto |
| OptiScaler_nightly.amd-nodlss.ini | Log | LogToDebug | auto |
| OptiScaler_nightly.amd-nodlss.ini | Log | LogToFile | auto |
| OptiScaler_nightly.amd-nodlss.ini | Log | LogToNGX | auto |
| OptiScaler_nightly.amd-nodlss.ini | Log | OpenConsole | auto |
| OptiScaler_nightly.amd-nodlss.ini | Log | SingleFile | auto |
| OptiScaler_nightly.amd-nodlss.ini | Magnifier | BorderSize | auto |
| OptiScaler_nightly.amd-nodlss.ini | Magnifier | CursorOffsetX | auto |
| OptiScaler_nightly.amd-nodlss.ini | Magnifier | CursorOffsetY | auto |
| OptiScaler_nightly.amd-nodlss.ini | Magnifier | Enabled | auto |
| OptiScaler_nightly.amd-nodlss.ini | Magnifier | Size | auto |
| OptiScaler_nightly.amd-nodlss.ini | Magnifier | StaticPosX | auto |
| OptiScaler_nightly.amd-nodlss.ini | Magnifier | StaticPosY | auto |
| OptiScaler_nightly.amd-nodlss.ini | Magnifier | ZoomFactor | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | AccentColorB | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | AccentColorG | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | AccentColorR | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | BGColorA | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | BGColorB | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | BGColorG | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | BGColorR | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | DisableSplash | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | ExtendedLimits | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | FGShortcutKey | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | FontSize | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | FpsCycleShortcutKey | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | FpsOverlayAlpha | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | FpsOverlayHorizontal | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | FpsOverlayPos | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | FpsOverlayType | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | FpsScale | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | FpsShortcutKey | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | LightTheme | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | OverlayMenu | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | OverlaysUseTheme | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | Scale | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | ShortcutKey | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | ShowFps | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | TTFFontPath | auto |
| OptiScaler_nightly.amd-nodlss.ini | Menu | UseHQFont | auto |
| OptiScaler_nightly.amd-nodlss.ini | Mipmap | MipmapBiasFixedOverride | auto |
| OptiScaler_nightly.amd-nodlss.ini | Mipmap | MipmapBiasOverride | auto |
| OptiScaler_nightly.amd-nodlss.ini | Mipmap | MipmapBiasOverrideAll | auto |
| OptiScaler_nightly.amd-nodlss.ini | Mipmap | MipmapBiasScaleOverride | auto |
| OptiScaler_nightly.amd-nodlss.ini | NvApi | DisableFlipMetering | auto |
| OptiScaler_nightly.amd-nodlss.ini | NvngxFG | DisableHudless | auto |
| OptiScaler_nightly.amd-nodlss.ini | NvngxFG | DispatchFlags | auto |
| OptiScaler_nightly.amd-nodlss.ini | NvngxFG | MakeDepthCopy | auto |
| OptiScaler_nightly.amd-nodlss.ini | NvngxFG | ShowDebug | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | AlwaysCaptureFSRFGSwapchain | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | AlwaysTrackHeaps | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | DepthScaleMax | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | DisableHUDFix | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | EnableDepthScale | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | HUDFix | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | HudfixDisableDI | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | HudfixDisableDII | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | HudfixDisableDispatch | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | HudfixDisableOM | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | HudfixDisableRTV | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | HudfixDisableSCR | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | HudfixDisableSGR | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | HudfixDisableSRV | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | HudfixDisableUAV | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | HUDFixDontUseSwapchainBuffers | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | HUDFixExtended | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | HUDFixImmediate | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | HUDFixRelaxedResolutionCheck | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | HUDLimit | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | MakeDepthCopy | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | MakeMVCopy | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | ResourceBlocking | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | ResourceFlip | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | ResourceFlipOffset | auto |
| OptiScaler_nightly.amd-nodlss.ini | OptiFG | UseShards | auto |
| OptiScaler_nightly.amd-nodlss.ini | OutputScaling | Downscaler | auto |
| OptiScaler_nightly.amd-nodlss.ini | OutputScaling | Enabled | auto |
| OptiScaler_nightly.amd-nodlss.ini | OutputScaling | Multiplier | auto |
| OptiScaler_nightly.amd-nodlss.ini | Plugins | LateAsiPluginsDelay | auto |
| OptiScaler_nightly.amd-nodlss.ini | Plugins | LoadAsiPlugins | auto |
| OptiScaler_nightly.amd-nodlss.ini | Plugins | LoadCustomAmdxc64OnRdna2 | auto |
| OptiScaler_nightly.amd-nodlss.ini | Plugins | LoadReshade | auto |
| OptiScaler_nightly.amd-nodlss.ini | Plugins | LoadSpecialK | auto |
| OptiScaler_nightly.amd-nodlss.ini | Plugins | Path | auto |
| OptiScaler_nightly.amd-nodlss.ini | ProcessFilter | ProcessExclusionList | auto |
| OptiScaler_nightly.amd-nodlss.ini | ProcessFilter | TargetProcessName | auto |
| OptiScaler_nightly.amd-nodlss.ini | QualityOverrides | QualityRatioBalanced | auto |
| OptiScaler_nightly.amd-nodlss.ini | QualityOverrides | QualityRatioDLAA | auto |
| OptiScaler_nightly.amd-nodlss.ini | QualityOverrides | QualityRatioOverrideEnabled | auto |
| OptiScaler_nightly.amd-nodlss.ini | QualityOverrides | QualityRatioPerformance | auto |
| OptiScaler_nightly.amd-nodlss.ini | QualityOverrides | QualityRatioQuality | auto |
| OptiScaler_nightly.amd-nodlss.ini | QualityOverrides | QualityRatioUltraPerformance | auto |
| OptiScaler_nightly.amd-nodlss.ini | QualityOverrides | QualityRatioUltraQuality | auto |
| OptiScaler_nightly.amd-nodlss.ini | Sharpness | OverrideSharpness | auto |
| OptiScaler_nightly.amd-nodlss.ini | Sharpness | Shader | auto |
| OptiScaler_nightly.amd-nodlss.ini | Sharpness | Sharpness | auto |
| OptiScaler_nightly.amd-nodlss.ini | Spoofing | D3DFeatureLevel | auto |
| OptiScaler_nightly.amd-nodlss.ini | Spoofing | Dxgi | auto |
| OptiScaler_nightly.amd-nodlss.ini | Spoofing | DxgiBlacklist | auto |
| OptiScaler_nightly.amd-nodlss.ini | Spoofing | DxgiFactoryWrapping | auto |
| OptiScaler_nightly.amd-nodlss.ini | Spoofing | DxgiVRAM | auto |
| OptiScaler_nightly.amd-nodlss.ini | Spoofing | Registry | auto |
| OptiScaler_nightly.amd-nodlss.ini | Spoofing | RegistryDriver | auto |
| OptiScaler_nightly.amd-nodlss.ini | Spoofing | SpoofedDeviceId | auto |
| OptiScaler_nightly.amd-nodlss.ini | Spoofing | SpoofedGPUName | auto |
| OptiScaler_nightly.amd-nodlss.ini | Spoofing | SpoofedVendorId | auto |
| OptiScaler_nightly.amd-nodlss.ini | Spoofing | SpoofHAGS | auto |
| OptiScaler_nightly.amd-nodlss.ini | Spoofing | StreamlineSpoofing | auto |
| OptiScaler_nightly.amd-nodlss.ini | Spoofing | TargetDeviceId | auto |
| OptiScaler_nightly.amd-nodlss.ini | Spoofing | TargetVendorId | auto |
| OptiScaler_nightly.amd-nodlss.ini | Spoofing | UEIntelAtomics | auto |
| OptiScaler_nightly.amd-nodlss.ini | Spoofing | User32 | auto |
| OptiScaler_nightly.amd-nodlss.ini | Spoofing | Vulkan | auto |
| OptiScaler_nightly.amd-nodlss.ini | Spoofing | VulkanExtensionSpoofing | auto |
| OptiScaler_nightly.amd-nodlss.ini | Spoofing | VulkanVRAM | auto |
| OptiScaler_nightly.amd-nodlss.ini | UpscaleRatio | UpscaleRatioOverrideEnabled | auto |
| OptiScaler_nightly.amd-nodlss.ini | UpscaleRatio | UpscaleRatioOverrideValue | auto |
| OptiScaler_nightly.amd-nodlss.ini | Upscalers | Dx11Upscaler | auto |
| OptiScaler_nightly.amd-nodlss.ini | Upscalers | Dx12Upscaler | auto |
| OptiScaler_nightly.amd-nodlss.ini | Upscalers | VulkanUpscaler | auto |
| OptiScaler_nightly.amd-nodlss.ini | V-Sync | ForceVsync | auto |
| OptiScaler_nightly.amd-nodlss.ini | V-Sync | OverrideVsync | auto |
| OptiScaler_nightly.amd-nodlss.ini | V-Sync | SyncInterval | auto |
| OptiScaler_nightly.amd-nodlss.ini | XeFG | DebugView | auto |
| OptiScaler_nightly.amd-nodlss.ini | XeFG | DepthInverted | auto |
| OptiScaler_nightly.amd-nodlss.ini | XeFG | ForceBorderless | auto |
| OptiScaler_nightly.amd-nodlss.ini | XeFG | HighResMV | auto |
| OptiScaler_nightly.amd-nodlss.ini | XeFG | IgnoreInitChecks | auto |
| OptiScaler_nightly.amd-nodlss.ini | XeFG | InterpolationCount | auto |
| OptiScaler_nightly.amd-nodlss.ini | XeFG | JitteredMV | auto |
| OptiScaler_nightly.amd-nodlss.ini | XeFG | UIComposition | auto |
| OptiScaler_nightly.amd-nodlss.ini | XeSS | BuildPipelines | auto |
| OptiScaler_nightly.amd-nodlss.ini | XeSS | CreateHeaps | auto |
| OptiScaler_nightly.amd-nodlss.ini | XeSS | NetworkModel | auto |
| OptiScaler_nightly.nvidia.ini | Anisotropy | AnisotropyOverride | auto |
| OptiScaler_nightly.nvidia.ini | Anisotropy | ModifyComparison | auto |
| OptiScaler_nightly.nvidia.ini | Anisotropy | ModifyMinMax | auto |
| OptiScaler_nightly.nvidia.ini | Anisotropy | SkipPointFilter | auto |
| OptiScaler_nightly.nvidia.ini | CAS | Contrast | auto |
| OptiScaler_nightly.nvidia.ini | CAS | ContrastEnabled | auto |
| OptiScaler_nightly.nvidia.ini | CAS | DAClampOutput | auto |
| OptiScaler_nightly.nvidia.ini | CAS | DADepthBias | auto |
| OptiScaler_nightly.nvidia.ini | CAS | DADepthScale | auto |
| OptiScaler_nightly.nvidia.ini | CAS | Enabled | auto |
| OptiScaler_nightly.nvidia.ini | CAS | MotionScaleLimit | auto |
| OptiScaler_nightly.nvidia.ini | CAS | MotionSharpness | auto |
| OptiScaler_nightly.nvidia.ini | CAS | MotionSharpnessEnabled | auto |
| OptiScaler_nightly.nvidia.ini | CAS | MotionThreshold | auto |
| OptiScaler_nightly.nvidia.ini | CAS | SharpenerDebug | auto |
| OptiScaler_nightly.nvidia.ini | DLSS | Enabled | auto |
| OptiScaler_nightly.nvidia.ini | DLSS | RenderPresetBalanced | auto |
| OptiScaler_nightly.nvidia.ini | DLSS | RenderPresetDLAA | auto |
| OptiScaler_nightly.nvidia.ini | DLSS | RenderPresetForAll | auto |
| OptiScaler_nightly.nvidia.ini | DLSS | RenderPresetOverride | auto |
| OptiScaler_nightly.nvidia.ini | DLSS | RenderPresetPerformance | auto |
| OptiScaler_nightly.nvidia.ini | DLSS | RenderPresetQuality | auto |
| OptiScaler_nightly.nvidia.ini | DLSS | RenderPresetUltraPerformance | auto |
| OptiScaler_nightly.nvidia.ini | DLSS | RenderPresetUltraQuality | auto |
| OptiScaler_nightly.nvidia.ini | DLSS | UseGenericAppIdWithDlss | auto |
| OptiScaler_nightly.nvidia.ini | DLSSD | RenderPresetBalanced | auto |
| OptiScaler_nightly.nvidia.ini | DLSSD | RenderPresetDLAA | auto |
| OptiScaler_nightly.nvidia.ini | DLSSD | RenderPresetForAll | auto |
| OptiScaler_nightly.nvidia.ini | DLSSD | RenderPresetOverride | auto |
| OptiScaler_nightly.nvidia.ini | DLSSD | RenderPresetPerformance | auto |
| OptiScaler_nightly.nvidia.ini | DLSSD | RenderPresetQuality | auto |
| OptiScaler_nightly.nvidia.ini | DLSSD | RenderPresetUltraPerformance | auto |
| OptiScaler_nightly.nvidia.ini | DLSSD | RenderPresetUltraQuality | auto |
| OptiScaler_nightly.nvidia.ini | DLSSG | FramerateTargetDMFG | auto |
| OptiScaler_nightly.nvidia.ini | DLSSG | InterpolationCount | auto |
| OptiScaler_nightly.nvidia.ini | DLSSG | OverrideForceDMFG | auto |
| OptiScaler_nightly.nvidia.ini | DLSSG | OverrideInterpolationCount | auto |
| OptiScaler_nightly.nvidia.ini | DLSSG | UseGamesReflexMarkers | auto |
| OptiScaler_nightly.nvidia.ini | DRS | DrsMaxOverrideEnabled | auto |
| OptiScaler_nightly.nvidia.ini | DRS | DrsMinOverrideEnabled | auto |
| OptiScaler_nightly.nvidia.ini | Dx11withDx12 | DontUseNTShared | auto |
| OptiScaler_nightly.nvidia.ini | Dx11withDx12 | UseDelayedInit | auto |
| OptiScaler_nightly.nvidia.ini | fakenvapi | ForceLatencyFlex | auto |
| OptiScaler_nightly.nvidia.ini | fakenvapi | ForceReflex | auto |
| OptiScaler_nightly.nvidia.ini | fakenvapi | ForceXeLL | auto |
| OptiScaler_nightly.nvidia.ini | fakenvapi | LatencyFlexMode | auto |
| OptiScaler_nightly.nvidia.ini | fakenvapi | UseFakenvapi | auto |
| OptiScaler_nightly.nvidia.ini | FrameGen | AllowedFrameAhead | auto |
| OptiScaler_nightly.nvidia.ini | FrameGen | DebugView | auto |
| OptiScaler_nightly.nvidia.ini | FrameGen | DepthValidNow | auto |
| OptiScaler_nightly.nvidia.ini | FrameGen | DisableHudless | auto |
| OptiScaler_nightly.nvidia.ini | FrameGen | DisableUI | auto |
| OptiScaler_nightly.nvidia.ini | FrameGen | DrawUIOverFG | auto |
| OptiScaler_nightly.nvidia.ini | FrameGen | Enabled | auto |
| OptiScaler_nightly.nvidia.ini | FrameGen | FGInput | auto |
| OptiScaler_nightly.nvidia.ini | FrameGen | FGNvngxReplacement | auto |
| OptiScaler_nightly.nvidia.ini | FrameGen | FGOutput | auto |
| OptiScaler_nightly.nvidia.ini | FrameGen | FTInput | auto |
| OptiScaler_nightly.nvidia.ini | FrameGen | HudCutoff | auto |
| OptiScaler_nightly.nvidia.ini | FrameGen | HudlessValidNow | auto |
| OptiScaler_nightly.nvidia.ini | FrameGen | ModifyBufferState | auto |
| OptiScaler_nightly.nvidia.ini | FrameGen | ModifySCIndex | auto |
| OptiScaler_nightly.nvidia.ini | FrameGen | OnlyAcceptFirstHudless | auto |
| OptiScaler_nightly.nvidia.ini | FrameGen | PreserveSwapChain | auto |
| OptiScaler_nightly.nvidia.ini | FrameGen | RectHeight | auto |
| OptiScaler_nightly.nvidia.ini | FrameGen | RectLeft | auto |
| OptiScaler_nightly.nvidia.ini | FrameGen | RectTop | auto |
| OptiScaler_nightly.nvidia.ini | FrameGen | RectWidth | auto |
| OptiScaler_nightly.nvidia.ini | FrameGen | SkipReset | auto |
| OptiScaler_nightly.nvidia.ini | FrameGen | SkipResizeBuffers | auto |
| OptiScaler_nightly.nvidia.ini | FrameGen | UIPremultipliedAlpha | auto |
| OptiScaler_nightly.nvidia.ini | FrameGen | VelocityValidNow | auto |
| OptiScaler_nightly.nvidia.ini | Framerate | FramerateLimit | auto |
| OptiScaler_nightly.nvidia.ini | FSR | AccAddPerFrame | auto |
| OptiScaler_nightly.nvidia.ini | FSR | CameraFar | auto |
| OptiScaler_nightly.nvidia.ini | FSR | CameraNear | auto |
| OptiScaler_nightly.nvidia.ini | FSR | DebugView | auto |
| OptiScaler_nightly.nvidia.ini | FSR | DlssReactiveMaskBias | auto |
| OptiScaler_nightly.nvidia.ini | FSR | FGIndex | auto |
| OptiScaler_nightly.nvidia.ini | FSR | Fsr4DoNotLoadAmdxc64 | auto |
| OptiScaler_nightly.nvidia.ini | FSR | Fsr4EnableWatermark | auto |
| OptiScaler_nightly.nvidia.ini | FSR | Fsr4ForceModel | auto |
| OptiScaler_nightly.nvidia.ini | FSR | Fsr4Preset | auto |
| OptiScaler_nightly.nvidia.ini | FSR | FsrAgilitySDKUpgrade | auto |
| OptiScaler_nightly.nvidia.ini | FSR | FsrNonLinearColorSpace | auto |
| OptiScaler_nightly.nvidia.ini | FSR | FsrNonLinearPQ | auto |
| OptiScaler_nightly.nvidia.ini | FSR | FsrNonLinearSRGB | auto |
| OptiScaler_nightly.nvidia.ini | FSR | HorizontalFov | auto |
| OptiScaler_nightly.nvidia.ini | FSR | MinDisOccAcc | auto |
| OptiScaler_nightly.nvidia.ini | FSR | ReactiveScale | auto |
| OptiScaler_nightly.nvidia.ini | FSR | ShadingScale | auto |
| OptiScaler_nightly.nvidia.ini | FSR | UpscalerIndex | auto |
| OptiScaler_nightly.nvidia.ini | FSR | UseFsrInputValues | auto |
| OptiScaler_nightly.nvidia.ini | FSR | UseReactiveMaskForTransparency | auto |
| OptiScaler_nightly.nvidia.ini | FSR | VelocityFactor | auto |
| OptiScaler_nightly.nvidia.ini | FSR | VerticalFov | auto |
| OptiScaler_nightly.nvidia.ini | FSRFG | AllowAsync | auto |
| OptiScaler_nightly.nvidia.ini | FSRFG | DebugPacingLines | auto |
| OptiScaler_nightly.nvidia.ini | FSRFG | DebugResetLines | auto |
| OptiScaler_nightly.nvidia.ini | FSRFG | DebugTearLines | auto |
| OptiScaler_nightly.nvidia.ini | FSRFG | EnableWatermark | auto |
| OptiScaler_nightly.nvidia.ini | FSRFG | FPTHybridSpin | auto |
| OptiScaler_nightly.nvidia.ini | FSRFG | FPTHybridSpinTime | auto |
| OptiScaler_nightly.nvidia.ini | FSRFG | FPTSafetyMarginInMs | auto |
| OptiScaler_nightly.nvidia.ini | FSRFG | FPTVarianceFactor | auto |
| OptiScaler_nightly.nvidia.ini | FSRFG | FPTWaitForSingleObjectOnFence | auto |
| OptiScaler_nightly.nvidia.ini | FSRFG | FramePacingTuning | auto |
| OptiScaler_nightly.nvidia.ini | FSRFG | UseMutexForSwapchain | auto |
| OptiScaler_nightly.nvidia.ini | FSRFGInputs | SkipConfigForHudless | auto |
| OptiScaler_nightly.nvidia.ini | FSRFGInputs | SkipDispatchForHudless | auto |
| OptiScaler_nightly.nvidia.ini | HDR | ForceHDR | auto |
| OptiScaler_nightly.nvidia.ini | HDR | SkipColorSpace | auto |
| OptiScaler_nightly.nvidia.ini | HDR | UseHDR10 | auto |
| OptiScaler_nightly.nvidia.ini | Hooks | EarlyHooking | auto |
| OptiScaler_nightly.nvidia.ini | Hooks | HookOriginalNvngxOnly | auto |
| OptiScaler_nightly.nvidia.ini | Hooks | UseNtdllHooks | auto |
| OptiScaler_nightly.nvidia.ini | Hotfix | CheckForUpdate | auto |
| OptiScaler_nightly.nvidia.ini | Hotfix | ColorMaskResourceBarrier | auto |
| OptiScaler_nightly.nvidia.ini | Hotfix | ColorResourceBarrier | auto |
| OptiScaler_nightly.nvidia.ini | Hotfix | CreateD3D12DeviceForLuma | auto |
| OptiScaler_nightly.nvidia.ini | Hotfix | DepthResourceBarrier | auto |
| OptiScaler_nightly.nvidia.ini | Hotfix | DisableOverlays | auto |
| OptiScaler_nightly.nvidia.ini | Hotfix | ExposureResourceBarrier | auto |
| OptiScaler_nightly.nvidia.ini | Hotfix | ExtendedStateRestore | auto |
| OptiScaler_nightly.nvidia.ini | Hotfix | ManualInputPolling | auto |
| OptiScaler_nightly.nvidia.ini | Hotfix | MotionVectorResourceBarrier | auto |
| OptiScaler_nightly.nvidia.ini | Hotfix | OutputResourceBarrier | auto |
| OptiScaler_nightly.nvidia.ini | Hotfix | PreferDedicatedGpu | auto |
| OptiScaler_nightly.nvidia.ini | Hotfix | PreferFirstDedicatedGpu | auto |
| OptiScaler_nightly.nvidia.ini | Hotfix | RestoreComputeSignature | auto |
| OptiScaler_nightly.nvidia.ini | Hotfix | RestoreGraphicSignature | auto |
| OptiScaler_nightly.nvidia.ini | Hotfix | RoundInternalResolution | auto |
| OptiScaler_nightly.nvidia.ini | Hotfix | SimulateWaitableObject | auto |
| OptiScaler_nightly.nvidia.ini | Hotfix | SkipFirstFrames | auto |
| OptiScaler_nightly.nvidia.ini | Hotfix | UsePrecompiledShaders | auto |
| OptiScaler_nightly.nvidia.ini | InitFlags | AutoExposure | auto |
| OptiScaler_nightly.nvidia.ini | InitFlags | DepthInverted | auto |
| OptiScaler_nightly.nvidia.ini | InitFlags | DisableReactiveMask | auto |
| OptiScaler_nightly.nvidia.ini | InitFlags | DisplayResolution | auto |
| OptiScaler_nightly.nvidia.ini | InitFlags | HDR | auto |
| OptiScaler_nightly.nvidia.ini | InitFlags | JitterCancellation | auto |
| OptiScaler_nightly.nvidia.ini | Inputs | EnableDlssInputs | auto |
| OptiScaler_nightly.nvidia.ini | Inputs | EnableFfxInputs | auto |
| OptiScaler_nightly.nvidia.ini | Inputs | EnableFsr2Inputs | auto |
| OptiScaler_nightly.nvidia.ini | Inputs | EnableFsr3Inputs | auto |
| OptiScaler_nightly.nvidia.ini | Inputs | EnableHotSwapping | auto |
| OptiScaler_nightly.nvidia.ini | Inputs | EnableXeSSInputs | auto |
| OptiScaler_nightly.nvidia.ini | Inputs | Fsr2Pattern | auto |
| OptiScaler_nightly.nvidia.ini | Inputs | Fsr3Pattern | auto |
| OptiScaler_nightly.nvidia.ini | Inputs | UseFfxInputs | auto |
| OptiScaler_nightly.nvidia.ini | Inputs | UseFsr2Dx11Inputs | auto |
| OptiScaler_nightly.nvidia.ini | Inputs | UseFsr2Inputs | auto |
| OptiScaler_nightly.nvidia.ini | Inputs | UseFsr2VulkanInputs | auto |
| OptiScaler_nightly.nvidia.ini | Inputs | UseFsr3Inputs | auto |
| OptiScaler_nightly.nvidia.ini | Libraries | FfxDx12FGPath | auto |
| OptiScaler_nightly.nvidia.ini | Libraries | FfxDx12Path | auto |
| OptiScaler_nightly.nvidia.ini | Libraries | FfxDx12RCPath | auto |
| OptiScaler_nightly.nvidia.ini | Libraries | FfxDx12RRPath | auto |
| OptiScaler_nightly.nvidia.ini | Libraries | FfxDx12SRPath | auto |
| OptiScaler_nightly.nvidia.ini | Libraries | FfxVkPath | auto |
| OptiScaler_nightly.nvidia.ini | Libraries | NvapiPath | auto |
| OptiScaler_nightly.nvidia.ini | Libraries | NvngxDlssPath | auto |
| OptiScaler_nightly.nvidia.ini | Libraries | NvngxFeaturePath | auto |
| OptiScaler_nightly.nvidia.ini | Libraries | NvngxPath | auto |
| OptiScaler_nightly.nvidia.ini | Libraries | OptiDllPath | auto |
| OptiScaler_nightly.nvidia.ini | Libraries | XeFGPath | auto |
| OptiScaler_nightly.nvidia.ini | Libraries | XeLLPath | auto |
| OptiScaler_nightly.nvidia.ini | Libraries | XeSSDx11Path | auto |
| OptiScaler_nightly.nvidia.ini | Libraries | XeSSPath | auto |
| OptiScaler_nightly.nvidia.ini | Log | LogAsync | auto |
| OptiScaler_nightly.nvidia.ini | Log | LogAsyncThreads | auto |
| OptiScaler_nightly.nvidia.ini | Log | LogFileName | auto |
| OptiScaler_nightly.nvidia.ini | Log | LogLevel | auto |
| OptiScaler_nightly.nvidia.ini | Log | LogToConsole | auto |
| OptiScaler_nightly.nvidia.ini | Log | LogToDebug | auto |
| OptiScaler_nightly.nvidia.ini | Log | LogToFile | auto |
| OptiScaler_nightly.nvidia.ini | Log | LogToNGX | auto |
| OptiScaler_nightly.nvidia.ini | Log | OpenConsole | auto |
| OptiScaler_nightly.nvidia.ini | Log | SingleFile | auto |
| OptiScaler_nightly.nvidia.ini | Magnifier | BorderSize | auto |
| OptiScaler_nightly.nvidia.ini | Magnifier | CursorOffsetX | auto |
| OptiScaler_nightly.nvidia.ini | Magnifier | CursorOffsetY | auto |
| OptiScaler_nightly.nvidia.ini | Magnifier | Enabled | auto |
| OptiScaler_nightly.nvidia.ini | Magnifier | Size | auto |
| OptiScaler_nightly.nvidia.ini | Magnifier | StaticPosX | auto |
| OptiScaler_nightly.nvidia.ini | Magnifier | StaticPosY | auto |
| OptiScaler_nightly.nvidia.ini | Magnifier | ZoomFactor | auto |
| OptiScaler_nightly.nvidia.ini | Menu | AccentColorB | auto |
| OptiScaler_nightly.nvidia.ini | Menu | AccentColorG | auto |
| OptiScaler_nightly.nvidia.ini | Menu | AccentColorR | auto |
| OptiScaler_nightly.nvidia.ini | Menu | BGColorA | auto |
| OptiScaler_nightly.nvidia.ini | Menu | BGColorB | auto |
| OptiScaler_nightly.nvidia.ini | Menu | BGColorG | auto |
| OptiScaler_nightly.nvidia.ini | Menu | BGColorR | auto |
| OptiScaler_nightly.nvidia.ini | Menu | DisableSplash | auto |
| OptiScaler_nightly.nvidia.ini | Menu | ExtendedLimits | auto |
| OptiScaler_nightly.nvidia.ini | Menu | FGShortcutKey | auto |
| OptiScaler_nightly.nvidia.ini | Menu | FontSize | auto |
| OptiScaler_nightly.nvidia.ini | Menu | FpsCycleShortcutKey | auto |
| OptiScaler_nightly.nvidia.ini | Menu | FpsOverlayAlpha | auto |
| OptiScaler_nightly.nvidia.ini | Menu | FpsOverlayHorizontal | auto |
| OptiScaler_nightly.nvidia.ini | Menu | FpsOverlayPos | auto |
| OptiScaler_nightly.nvidia.ini | Menu | FpsOverlayType | auto |
| OptiScaler_nightly.nvidia.ini | Menu | FpsScale | auto |
| OptiScaler_nightly.nvidia.ini | Menu | FpsShortcutKey | auto |
| OptiScaler_nightly.nvidia.ini | Menu | LightTheme | auto |
| OptiScaler_nightly.nvidia.ini | Menu | OverlayMenu | auto |
| OptiScaler_nightly.nvidia.ini | Menu | OverlaysUseTheme | auto |
| OptiScaler_nightly.nvidia.ini | Menu | Scale | auto |
| OptiScaler_nightly.nvidia.ini | Menu | ShortcutKey | auto |
| OptiScaler_nightly.nvidia.ini | Menu | ShowFps | auto |
| OptiScaler_nightly.nvidia.ini | Menu | TTFFontPath | auto |
| OptiScaler_nightly.nvidia.ini | Menu | UseHQFont | auto |
| OptiScaler_nightly.nvidia.ini | Mipmap | MipmapBiasFixedOverride | auto |
| OptiScaler_nightly.nvidia.ini | Mipmap | MipmapBiasOverride | auto |
| OptiScaler_nightly.nvidia.ini | Mipmap | MipmapBiasOverrideAll | auto |
| OptiScaler_nightly.nvidia.ini | Mipmap | MipmapBiasScaleOverride | auto |
| OptiScaler_nightly.nvidia.ini | NvApi | DisableFlipMetering | auto |
| OptiScaler_nightly.nvidia.ini | NvngxFG | DisableHudless | auto |
| OptiScaler_nightly.nvidia.ini | NvngxFG | DispatchFlags | auto |
| OptiScaler_nightly.nvidia.ini | NvngxFG | MakeDepthCopy | auto |
| OptiScaler_nightly.nvidia.ini | NvngxFG | ShowDebug | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | AlwaysCaptureFSRFGSwapchain | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | AlwaysTrackHeaps | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | DepthScaleMax | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | DisableHUDFix | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | EnableDepthScale | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | HUDFix | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | HudfixDisableDI | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | HudfixDisableDII | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | HudfixDisableDispatch | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | HudfixDisableOM | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | HudfixDisableRTV | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | HudfixDisableSCR | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | HudfixDisableSGR | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | HudfixDisableSRV | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | HudfixDisableUAV | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | HUDFixDontUseSwapchainBuffers | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | HUDFixExtended | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | HUDFixImmediate | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | HUDFixRelaxedResolutionCheck | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | HUDLimit | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | MakeDepthCopy | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | MakeMVCopy | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | ResourceBlocking | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | ResourceFlip | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | ResourceFlipOffset | auto |
| OptiScaler_nightly.nvidia.ini | OptiFG | UseShards | auto |
| OptiScaler_nightly.nvidia.ini | OutputScaling | Downscaler | auto |
| OptiScaler_nightly.nvidia.ini | OutputScaling | Enabled | auto |
| OptiScaler_nightly.nvidia.ini | OutputScaling | Multiplier | auto |
| OptiScaler_nightly.nvidia.ini | Plugins | LateAsiPluginsDelay | auto |
| OptiScaler_nightly.nvidia.ini | Plugins | LoadAsiPlugins | auto |
| OptiScaler_nightly.nvidia.ini | Plugins | LoadCustomAmdxc64OnRdna2 | auto |
| OptiScaler_nightly.nvidia.ini | Plugins | LoadReshade | auto |
| OptiScaler_nightly.nvidia.ini | Plugins | LoadSpecialK | auto |
| OptiScaler_nightly.nvidia.ini | Plugins | Path | auto |
| OptiScaler_nightly.nvidia.ini | ProcessFilter | ProcessExclusionList | auto |
| OptiScaler_nightly.nvidia.ini | ProcessFilter | TargetProcessName | auto |
| OptiScaler_nightly.nvidia.ini | QualityOverrides | QualityRatioBalanced | auto |
| OptiScaler_nightly.nvidia.ini | QualityOverrides | QualityRatioDLAA | auto |
| OptiScaler_nightly.nvidia.ini | QualityOverrides | QualityRatioOverrideEnabled | auto |
| OptiScaler_nightly.nvidia.ini | QualityOverrides | QualityRatioPerformance | auto |
| OptiScaler_nightly.nvidia.ini | QualityOverrides | QualityRatioQuality | auto |
| OptiScaler_nightly.nvidia.ini | QualityOverrides | QualityRatioUltraPerformance | auto |
| OptiScaler_nightly.nvidia.ini | QualityOverrides | QualityRatioUltraQuality | auto |
| OptiScaler_nightly.nvidia.ini | Sharpness | OverrideSharpness | auto |
| OptiScaler_nightly.nvidia.ini | Sharpness | Shader | auto |
| OptiScaler_nightly.nvidia.ini | Sharpness | Sharpness | auto |
| OptiScaler_nightly.nvidia.ini | Spoofing | D3DFeatureLevel | auto |
| OptiScaler_nightly.nvidia.ini | Spoofing | Dxgi | auto |
| OptiScaler_nightly.nvidia.ini | Spoofing | DxgiBlacklist | auto |
| OptiScaler_nightly.nvidia.ini | Spoofing | DxgiFactoryWrapping | auto |
| OptiScaler_nightly.nvidia.ini | Spoofing | DxgiVRAM | auto |
| OptiScaler_nightly.nvidia.ini | Spoofing | Registry | auto |
| OptiScaler_nightly.nvidia.ini | Spoofing | RegistryDriver | auto |
| OptiScaler_nightly.nvidia.ini | Spoofing | SpoofedDeviceId | auto |
| OptiScaler_nightly.nvidia.ini | Spoofing | SpoofedGPUName | auto |
| OptiScaler_nightly.nvidia.ini | Spoofing | SpoofedVendorId | auto |
| OptiScaler_nightly.nvidia.ini | Spoofing | SpoofHAGS | auto |
| OptiScaler_nightly.nvidia.ini | Spoofing | StreamlineSpoofing | auto |
| OptiScaler_nightly.nvidia.ini | Spoofing | TargetDeviceId | auto |
| OptiScaler_nightly.nvidia.ini | Spoofing | TargetVendorId | auto |
| OptiScaler_nightly.nvidia.ini | Spoofing | UEIntelAtomics | auto |
| OptiScaler_nightly.nvidia.ini | Spoofing | User32 | auto |
| OptiScaler_nightly.nvidia.ini | Spoofing | Vulkan | auto |
| OptiScaler_nightly.nvidia.ini | Spoofing | VulkanExtensionSpoofing | auto |
| OptiScaler_nightly.nvidia.ini | Spoofing | VulkanVRAM | auto |
| OptiScaler_nightly.nvidia.ini | UpscaleRatio | UpscaleRatioOverrideEnabled | auto |
| OptiScaler_nightly.nvidia.ini | UpscaleRatio | UpscaleRatioOverrideValue | auto |
| OptiScaler_nightly.nvidia.ini | Upscalers | Dx11Upscaler | auto |
| OptiScaler_nightly.nvidia.ini | Upscalers | Dx12Upscaler | auto |
| OptiScaler_nightly.nvidia.ini | Upscalers | VulkanUpscaler | auto |
| OptiScaler_nightly.nvidia.ini | V-Sync | ForceVsync | auto |
| OptiScaler_nightly.nvidia.ini | V-Sync | OverrideVsync | auto |
| OptiScaler_nightly.nvidia.ini | V-Sync | SyncInterval | auto |
| OptiScaler_nightly.nvidia.ini | XeFG | DebugView | auto |
| OptiScaler_nightly.nvidia.ini | XeFG | DepthInverted | auto |
| OptiScaler_nightly.nvidia.ini | XeFG | ForceBorderless | auto |
| OptiScaler_nightly.nvidia.ini | XeFG | HighResMV | auto |
| OptiScaler_nightly.nvidia.ini | XeFG | IgnoreInitChecks | auto |
| OptiScaler_nightly.nvidia.ini | XeFG | InterpolationCount | auto |
| OptiScaler_nightly.nvidia.ini | XeFG | JitteredMV | auto |
| OptiScaler_nightly.nvidia.ini | XeFG | UIComposition | auto |
| OptiScaler_nightly.nvidia.ini | XeSS | BuildPipelines | auto |
| OptiScaler_nightly.nvidia.ini | XeSS | CreateHeaps | auto |
| OptiScaler_nightly.nvidia.ini | XeSS | NetworkModel | auto |
| OptiScaler.amd-dlss.ini | Anisotropy | AnisotropyOverride | auto |
| OptiScaler.amd-dlss.ini | Anisotropy | ModifyComparison | auto |
| OptiScaler.amd-dlss.ini | Anisotropy | ModifyMinMax | auto |
| OptiScaler.amd-dlss.ini | Anisotropy | SkipPointFilter | auto |
| OptiScaler.amd-dlss.ini | CAS | Contrast | auto |
| OptiScaler.amd-dlss.ini | CAS | ContrastEnabled | auto |
| OptiScaler.amd-dlss.ini | CAS | Enabled | auto |
| OptiScaler.amd-dlss.ini | CAS | MotionScaleLimit | auto |
| OptiScaler.amd-dlss.ini | CAS | MotionSharpness | auto |
| OptiScaler.amd-dlss.ini | CAS | MotionSharpnessDebug | auto |
| OptiScaler.amd-dlss.ini | CAS | MotionSharpnessEnabled | auto |
| OptiScaler.amd-dlss.ini | CAS | MotionThreshold | auto |
| OptiScaler.amd-dlss.ini | DLSS | Enabled | auto |
| OptiScaler.amd-dlss.ini | DLSS | FeaturePath | auto |
| OptiScaler.amd-dlss.ini | DLSS | LibraryPath | auto |
| OptiScaler.amd-dlss.ini | DLSS | NVNGX_DLSS_Path | auto |
| OptiScaler.amd-dlss.ini | DLSS | RenderPresetBalanced | auto |
| OptiScaler.amd-dlss.ini | DLSS | RenderPresetDLAA | auto |
| OptiScaler.amd-dlss.ini | DLSS | RenderPresetForAll | auto |
| OptiScaler.amd-dlss.ini | DLSS | RenderPresetOverride | auto |
| OptiScaler.amd-dlss.ini | DLSS | RenderPresetPerformance | auto |
| OptiScaler.amd-dlss.ini | DLSS | RenderPresetQuality | auto |
| OptiScaler.amd-dlss.ini | DLSS | RenderPresetUltraPerformance | auto |
| OptiScaler.amd-dlss.ini | DLSS | RenderPresetUltraQuality | auto |
| OptiScaler.amd-dlss.ini | DLSS | UseGenericAppIdWithDlss | auto |
| OptiScaler.amd-dlss.ini | DLSSD | RenderPresetBalanced | auto |
| OptiScaler.amd-dlss.ini | DLSSD | RenderPresetDLAA | auto |
| OptiScaler.amd-dlss.ini | DLSSD | RenderPresetForAll | auto |
| OptiScaler.amd-dlss.ini | DLSSD | RenderPresetOverride | auto |
| OptiScaler.amd-dlss.ini | DLSSD | RenderPresetPerformance | auto |
| OptiScaler.amd-dlss.ini | DLSSD | RenderPresetQuality | auto |
| OptiScaler.amd-dlss.ini | DLSSD | RenderPresetUltraPerformance | auto |
| OptiScaler.amd-dlss.ini | DLSSD | RenderPresetUltraQuality | auto |
| OptiScaler.amd-dlss.ini | DRS | DrsMaxOverrideEnabled | auto |
| OptiScaler.amd-dlss.ini | DRS | DrsMinOverrideEnabled | auto |
| OptiScaler.amd-dlss.ini | Dx11withDx12 | DontUseNTShared | auto |
| OptiScaler.amd-dlss.ini | Dx11withDx12 | UseDelayedInit | auto |
| OptiScaler.amd-dlss.ini | FrameGen | AllowedFrameAhead | auto |
| OptiScaler.amd-dlss.ini | FrameGen | DebugView | auto |
| OptiScaler.amd-dlss.ini | FrameGen | DepthValidNow | auto |
| OptiScaler.amd-dlss.ini | FrameGen | DisableHudless | auto |
| OptiScaler.amd-dlss.ini | FrameGen | DisableUI | auto |
| OptiScaler.amd-dlss.ini | FrameGen | DrawUIOverFG | auto |
| OptiScaler.amd-dlss.ini | FrameGen | Enabled | auto |
| OptiScaler.amd-dlss.ini | FrameGen | FGInput | auto |
| OptiScaler.amd-dlss.ini | FrameGen | FGOutput | auto |
| OptiScaler.amd-dlss.ini | FrameGen | FTInput | auto |
| OptiScaler.amd-dlss.ini | FrameGen | HudlessValidNow | auto |
| OptiScaler.amd-dlss.ini | FrameGen | ModifyBufferState | auto |
| OptiScaler.amd-dlss.ini | FrameGen | ModifySCIndex | auto |
| OptiScaler.amd-dlss.ini | FrameGen | OnlyAcceptFirstHudless | auto |
| OptiScaler.amd-dlss.ini | FrameGen | PreserveSwapChain | auto |
| OptiScaler.amd-dlss.ini | FrameGen | RectHeight | auto |
| OptiScaler.amd-dlss.ini | FrameGen | RectLeft | auto |
| OptiScaler.amd-dlss.ini | FrameGen | RectTop | auto |
| OptiScaler.amd-dlss.ini | FrameGen | RectWidth | auto |
| OptiScaler.amd-dlss.ini | FrameGen | SkipReset | auto |
| OptiScaler.amd-dlss.ini | FrameGen | SkipResizeBuffers | auto |
| OptiScaler.amd-dlss.ini | FrameGen | UIPremultipliedAlpha | auto |
| OptiScaler.amd-dlss.ini | FrameGen | VelocityValidNow | auto |
| OptiScaler.amd-dlss.ini | Framerate | FramerateLimit | auto |
| OptiScaler.amd-dlss.ini | FSR | AccAddPerFrame | auto |
| OptiScaler.amd-dlss.ini | FSR | CameraFar | auto |
| OptiScaler.amd-dlss.ini | FSR | CameraNear | auto |
| OptiScaler.amd-dlss.ini | FSR | DebugView | auto |
| OptiScaler.amd-dlss.ini | FSR | DlssReactiveMaskBias | auto |
| OptiScaler.amd-dlss.ini | FSR | FfxDx12Path | auto |
| OptiScaler.amd-dlss.ini | FSR | FfxVkPath | auto |
| OptiScaler.amd-dlss.ini | FSR | FGIndex | auto |
| OptiScaler.amd-dlss.ini | FSR | Fsr4EnableDebugView | auto |
| OptiScaler.amd-dlss.ini | FSR | Fsr4EnableWatermark | auto |
| OptiScaler.amd-dlss.ini | FSR | Fsr4Model | auto |
| OptiScaler.amd-dlss.ini | FSR | Fsr4Update | auto |
| OptiScaler.amd-dlss.ini | FSR | FsrAgilitySDKUpgrade | auto |
| OptiScaler.amd-dlss.ini | FSR | FsrNonLinearColorSpace | auto |
| OptiScaler.amd-dlss.ini | FSR | FsrNonLinearPQ | auto |
| OptiScaler.amd-dlss.ini | FSR | FsrNonLinearSRGB | auto |
| OptiScaler.amd-dlss.ini | FSR | HorizontalFov | auto |
| OptiScaler.amd-dlss.ini | FSR | MinDisOccAcc | auto |
| OptiScaler.amd-dlss.ini | FSR | ReactiveScale | auto |
| OptiScaler.amd-dlss.ini | FSR | ShadingScale | auto |
| OptiScaler.amd-dlss.ini | FSR | UpscalerIndex | auto |
| OptiScaler.amd-dlss.ini | FSR | UseFsrInputValues | auto |
| OptiScaler.amd-dlss.ini | FSR | UseReactiveMaskForTransparency | auto |
| OptiScaler.amd-dlss.ini | FSR | VelocityFactor | auto |
| OptiScaler.amd-dlss.ini | FSR | VerticalFov | auto |
| OptiScaler.amd-dlss.ini | FSRFG | AllowAsync | auto |
| OptiScaler.amd-dlss.ini | FSRFG | DebugPacingLines | auto |
| OptiScaler.amd-dlss.ini | FSRFG | DebugResetLines | auto |
| OptiScaler.amd-dlss.ini | FSRFG | DebugTearLines | auto |
| OptiScaler.amd-dlss.ini | FSRFG | EnableWatermark | auto |
| OptiScaler.amd-dlss.ini | FSRFG | FPTHybridSpin | auto |
| OptiScaler.amd-dlss.ini | FSRFG | FPTHybridSpinTime | auto |
| OptiScaler.amd-dlss.ini | FSRFG | FPTSafetyMarginInMs | auto |
| OptiScaler.amd-dlss.ini | FSRFG | FPTVarianceFactor | auto |
| OptiScaler.amd-dlss.ini | FSRFG | FPTWaitForSingleObjectOnFence | auto |
| OptiScaler.amd-dlss.ini | FSRFG | FramePacingTuning | auto |
| OptiScaler.amd-dlss.ini | FSRFG | UseMutexForSwapchain | auto |
| OptiScaler.amd-dlss.ini | FSRFGInputs | SkipConfigForHudless | auto |
| OptiScaler.amd-dlss.ini | FSRFGInputs | SkipDispatchForHudless | auto |
| OptiScaler.amd-dlss.ini | HDR | ForceHDR | auto |
| OptiScaler.amd-dlss.ini | HDR | SkipColorSpace | auto |
| OptiScaler.amd-dlss.ini | HDR | UseHDR10 | auto |
| OptiScaler.amd-dlss.ini | Hooks | EarlyHooking | auto |
| OptiScaler.amd-dlss.ini | Hooks | HookOriginalNvngxOnly | auto |
| OptiScaler.amd-dlss.ini | Hooks | UseNtdllHooks | auto |
| OptiScaler.amd-dlss.ini | Hotfix | CheckForUpdate | auto |
| OptiScaler.amd-dlss.ini | Hotfix | ColorMaskResourceBarrier | auto |
| OptiScaler.amd-dlss.ini | Hotfix | ColorResourceBarrier | auto |
| OptiScaler.amd-dlss.ini | Hotfix | DepthResourceBarrier | auto |
| OptiScaler.amd-dlss.ini | Hotfix | DisableOverlays | auto |
| OptiScaler.amd-dlss.ini | Hotfix | DontCreateD3D12DeviceForLuma | auto |
| OptiScaler.amd-dlss.ini | Hotfix | ExposureResourceBarrier | auto |
| OptiScaler.amd-dlss.ini | Hotfix | MotionVectorResourceBarrier | auto |
| OptiScaler.amd-dlss.ini | Hotfix | OutputResourceBarrier | auto |
| OptiScaler.amd-dlss.ini | Hotfix | PreferDedicatedGpu | auto |
| OptiScaler.amd-dlss.ini | Hotfix | PreferFirstDedicatedGpu | auto |
| OptiScaler.amd-dlss.ini | Hotfix | RestoreComputeSignature | auto |
| OptiScaler.amd-dlss.ini | Hotfix | RestoreGraphicSignature | auto |
| OptiScaler.amd-dlss.ini | Hotfix | RoundInternalResolution | auto |
| OptiScaler.amd-dlss.ini | Hotfix | SkipFirstFrames | auto |
| OptiScaler.amd-dlss.ini | Hotfix | UsePrecompiledShaders | auto |
| OptiScaler.amd-dlss.ini | InitFlags | AutoExposure | auto |
| OptiScaler.amd-dlss.ini | InitFlags | DepthInverted | auto |
| OptiScaler.amd-dlss.ini | InitFlags | DisableReactiveMask | auto |
| OptiScaler.amd-dlss.ini | InitFlags | DisplayResolution | auto |
| OptiScaler.amd-dlss.ini | InitFlags | HDR | auto |
| OptiScaler.amd-dlss.ini | InitFlags | JitterCancellation | auto |
| OptiScaler.amd-dlss.ini | Inputs | EnableDlssInputs | auto |
| OptiScaler.amd-dlss.ini | Inputs | EnableFfxInputs | auto |
| OptiScaler.amd-dlss.ini | Inputs | EnableFsr2Inputs | auto |
| OptiScaler.amd-dlss.ini | Inputs | EnableFsr3Inputs | auto |
| OptiScaler.amd-dlss.ini | Inputs | EnableHotSwapping | auto |
| OptiScaler.amd-dlss.ini | Inputs | EnableXeSSInputs | auto |
| OptiScaler.amd-dlss.ini | Inputs | Fsr2Pattern | auto |
| OptiScaler.amd-dlss.ini | Inputs | Fsr3Pattern | auto |
| OptiScaler.amd-dlss.ini | Inputs | UseFfxInputs | auto |
| OptiScaler.amd-dlss.ini | Inputs | UseFsr2Dx11Inputs | auto |
| OptiScaler.amd-dlss.ini | Inputs | UseFsr2Inputs | auto |
| OptiScaler.amd-dlss.ini | Inputs | UseFsr2VulkanInputs | auto |
| OptiScaler.amd-dlss.ini | Inputs | UseFsr3Inputs | auto |
| OptiScaler.amd-dlss.ini | Log | LogAsync | auto |
| OptiScaler.amd-dlss.ini | Log | LogAsyncThreads | auto |
| OptiScaler.amd-dlss.ini | Log | LogFile | auto |
| OptiScaler.amd-dlss.ini | Log | LogLevel | auto |
| OptiScaler.amd-dlss.ini | Log | LogToConsole | auto |
| OptiScaler.amd-dlss.ini | Log | LogToDebug | auto |
| OptiScaler.amd-dlss.ini | Log | LogToFile | auto |
| OptiScaler.amd-dlss.ini | Log | LogToNGX | auto |
| OptiScaler.amd-dlss.ini | Log | OpenConsole | auto |
| OptiScaler.amd-dlss.ini | Log | SingleFile | auto |
| OptiScaler.amd-dlss.ini | Menu | DisableSplash | auto |
| OptiScaler.amd-dlss.ini | Menu | ExtendedLimits | auto |
| OptiScaler.amd-dlss.ini | Menu | FGShortcutKey | -1 |
| OptiScaler.amd-dlss.ini | Menu | FpsCycleShortcutKey | -1 |
| OptiScaler.amd-dlss.ini | Menu | FpsOverlayAlpha | auto |
| OptiScaler.amd-dlss.ini | Menu | FpsOverlayHorizontal | auto |
| OptiScaler.amd-dlss.ini | Menu | FpsOverlayPos | auto |
| OptiScaler.amd-dlss.ini | Menu | FpsOverlayType | auto |
| OptiScaler.amd-dlss.ini | Menu | FpsScale | auto |
| OptiScaler.amd-dlss.ini | Menu | FpsShortcutKey | -1 |
| OptiScaler.amd-dlss.ini | Menu | OverlayMenu | auto |
| OptiScaler.amd-dlss.ini | Menu | Scale | auto |
| OptiScaler.amd-dlss.ini | Menu | ShortcutKey | auto |
| OptiScaler.amd-dlss.ini | Menu | ShowFps | auto |
| OptiScaler.amd-dlss.ini | Menu | TTFFontPath | auto |
| OptiScaler.amd-dlss.ini | Menu | UseHQFont | auto |
| OptiScaler.amd-dlss.ini | Mipmap | MipmapBiasFixedOverride | auto |
| OptiScaler.amd-dlss.ini | Mipmap | MipmapBiasOverride | auto |
| OptiScaler.amd-dlss.ini | Mipmap | MipmapBiasOverrideAll | auto |
| OptiScaler.amd-dlss.ini | Mipmap | MipmapBiasScaleOverride | auto |
| OptiScaler.amd-dlss.ini | Nukems | MakeDepthCopy | auto |
| OptiScaler.amd-dlss.ini | NvApi | DisableFlipMetering | auto |
| OptiScaler.amd-dlss.ini | NvApi | DontUseFakenvapiForXeLLOnNvidia | auto |
| OptiScaler.amd-dlss.ini | NvApi | NvapiDllPath | auto |
| OptiScaler.amd-dlss.ini | NvApi | OverrideNvapiDll | auto |
| OptiScaler.amd-dlss.ini | OptiFG | AlwaysCaptureFSRFGSwapchain | auto |
| OptiScaler.amd-dlss.ini | OptiFG | AlwaysTrackHeaps | auto |
| OptiScaler.amd-dlss.ini | OptiFG | DepthScaleMax | auto |
| OptiScaler.amd-dlss.ini | OptiFG | DisableHUDFix | auto |
| OptiScaler.amd-dlss.ini | OptiFG | EnableDepthScale | auto |
| OptiScaler.amd-dlss.ini | OptiFG | HUDFix | auto |
| OptiScaler.amd-dlss.ini | OptiFG | HudfixDisableDI | auto |
| OptiScaler.amd-dlss.ini | OptiFG | HudfixDisableDII | auto |
| OptiScaler.amd-dlss.ini | OptiFG | HudfixDisableDispatch | auto |
| OptiScaler.amd-dlss.ini | OptiFG | HudfixDisableOM | auto |
| OptiScaler.amd-dlss.ini | OptiFG | HudfixDisableRTV | auto |
| OptiScaler.amd-dlss.ini | OptiFG | HudfixDisableSCR | auto |
| OptiScaler.amd-dlss.ini | OptiFG | HudfixDisableSGR | auto |
| OptiScaler.amd-dlss.ini | OptiFG | HudfixDisableSRV | auto |
| OptiScaler.amd-dlss.ini | OptiFG | HudfixDisableUAV | auto |
| OptiScaler.amd-dlss.ini | OptiFG | HUDFixDontUseSwapchainBuffers | auto |
| OptiScaler.amd-dlss.ini | OptiFG | HUDFixExtended | auto |
| OptiScaler.amd-dlss.ini | OptiFG | HUDFixImmediate | auto |
| OptiScaler.amd-dlss.ini | OptiFG | HUDFixRelaxedResolutionCheck | auto |
| OptiScaler.amd-dlss.ini | OptiFG | HUDLimit | auto |
| OptiScaler.amd-dlss.ini | OptiFG | MakeDepthCopy | auto |
| OptiScaler.amd-dlss.ini | OptiFG | MakeMVCopy | auto |
| OptiScaler.amd-dlss.ini | OptiFG | ResourceBlocking | auto |
| OptiScaler.amd-dlss.ini | OptiFG | ResourceFlip | auto |
| OptiScaler.amd-dlss.ini | OptiFG | ResourceFlipOffset | auto |
| OptiScaler.amd-dlss.ini | OptiFG | UseShards | auto |
| OptiScaler.amd-dlss.ini | OutputScaling | Downscaler | auto |
| OptiScaler.amd-dlss.ini | OutputScaling | Enabled | auto |
| OptiScaler.amd-dlss.ini | OutputScaling | Multiplier | auto |
| OptiScaler.amd-dlss.ini | Plugins | LoadAsiPlugins | auto |
| OptiScaler.amd-dlss.ini | Plugins | LoadReshade | auto |
| OptiScaler.amd-dlss.ini | Plugins | LoadSpecialK | auto |
| OptiScaler.amd-dlss.ini | Plugins | Path | auto |
| OptiScaler.amd-dlss.ini | ProcessFilter | ProcessExclusionList | auto |
| OptiScaler.amd-dlss.ini | ProcessFilter | TargetProcessName | auto |
| OptiScaler.amd-dlss.ini | QualityOverrides | QualityRatioBalanced | auto |
| OptiScaler.amd-dlss.ini | QualityOverrides | QualityRatioDLAA | auto |
| OptiScaler.amd-dlss.ini | QualityOverrides | QualityRatioOverrideEnabled | auto |
| OptiScaler.amd-dlss.ini | QualityOverrides | QualityRatioPerformance | auto |
| OptiScaler.amd-dlss.ini | QualityOverrides | QualityRatioQuality | auto |
| OptiScaler.amd-dlss.ini | QualityOverrides | QualityRatioUltraPerformance | auto |
| OptiScaler.amd-dlss.ini | QualityOverrides | QualityRatioUltraQuality | auto |
| OptiScaler.amd-dlss.ini | Sharpness | OverrideSharpness | auto |
| OptiScaler.amd-dlss.ini | Sharpness | Sharpness | auto |
| OptiScaler.amd-dlss.ini | Spoofing | D3DFeatureLevel | auto |
| OptiScaler.amd-dlss.ini | Spoofing | Dxgi | auto |
| OptiScaler.amd-dlss.ini | Spoofing | DxgiBlacklist | auto |
| OptiScaler.amd-dlss.ini | Spoofing | DxgiFactoryWrapping | auto |
| OptiScaler.amd-dlss.ini | Spoofing | DxgiVRAM | auto |
| OptiScaler.amd-dlss.ini | Spoofing | Registry | auto |
| OptiScaler.amd-dlss.ini | Spoofing | RegistryDriver | auto |
| OptiScaler.amd-dlss.ini | Spoofing | SpoofedDeviceId | auto |
| OptiScaler.amd-dlss.ini | Spoofing | SpoofedGPUName | auto |
| OptiScaler.amd-dlss.ini | Spoofing | SpoofedVendorId | auto |
| OptiScaler.amd-dlss.ini | Spoofing | SpoofHAGS | auto |
| OptiScaler.amd-dlss.ini | Spoofing | StreamlineSpoofing | auto |
| OptiScaler.amd-dlss.ini | Spoofing | TargetDeviceId | auto |
| OptiScaler.amd-dlss.ini | Spoofing | TargetVendorId | auto |
| OptiScaler.amd-dlss.ini | Spoofing | UEIntelAtomics | auto |
| OptiScaler.amd-dlss.ini | Spoofing | User32 | auto |
| OptiScaler.amd-dlss.ini | Spoofing | Vulkan | auto |
| OptiScaler.amd-dlss.ini | Spoofing | VulkanExtensionSpoofing | auto |
| OptiScaler.amd-dlss.ini | Spoofing | VulkanVRAM | auto |
| OptiScaler.amd-dlss.ini | UpscaleRatio | UpscaleRatioOverrideEnabled | auto |
| OptiScaler.amd-dlss.ini | UpscaleRatio | UpscaleRatioOverrideValue | auto |
| OptiScaler.amd-dlss.ini | Upscalers | Dx11Upscaler | auto |
| OptiScaler.amd-dlss.ini | Upscalers | Dx12Upscaler | auto |
| OptiScaler.amd-dlss.ini | Upscalers | VulkanUpscaler | auto |
| OptiScaler.amd-dlss.ini | V-Sync | ForceVsync | auto |
| OptiScaler.amd-dlss.ini | V-Sync | OverrideVsync | auto |
| OptiScaler.amd-dlss.ini | V-Sync | SyncInterval | auto |
| OptiScaler.amd-dlss.ini | XeFG | DebugView | auto |
| OptiScaler.amd-dlss.ini | XeFG | DepthInverted | auto |
| OptiScaler.amd-dlss.ini | XeFG | ForceBorderless | auto |
| OptiScaler.amd-dlss.ini | XeFG | HighResMV | auto |
| OptiScaler.amd-dlss.ini | XeFG | IgnoreInitChecks | auto |
| OptiScaler.amd-dlss.ini | XeFG | InterpolationCount | auto |
| OptiScaler.amd-dlss.ini | XeFG | JitteredMV | auto |
| OptiScaler.amd-dlss.ini | XeFG | UIComposition | auto |
| OptiScaler.amd-dlss.ini | XeSS | BuildPipelines | auto |
| OptiScaler.amd-dlss.ini | XeSS | CreateHeaps | auto |
| OptiScaler.amd-dlss.ini | XeSS | Dx11LibraryPath | auto |
| OptiScaler.amd-dlss.ini | XeSS | LibraryPath | auto |
| OptiScaler.amd-dlss.ini | XeSS | NetworkModel | auto |
| OptiScaler.amd-nodlss.ini | Anisotropy | AnisotropyOverride | auto |
| OptiScaler.amd-nodlss.ini | Anisotropy | ModifyComparison | auto |
| OptiScaler.amd-nodlss.ini | Anisotropy | ModifyMinMax | auto |
| OptiScaler.amd-nodlss.ini | Anisotropy | SkipPointFilter | auto |
| OptiScaler.amd-nodlss.ini | CAS | Contrast | auto |
| OptiScaler.amd-nodlss.ini | CAS | ContrastEnabled | auto |
| OptiScaler.amd-nodlss.ini | CAS | Enabled | auto |
| OptiScaler.amd-nodlss.ini | CAS | MotionScaleLimit | auto |
| OptiScaler.amd-nodlss.ini | CAS | MotionSharpness | auto |
| OptiScaler.amd-nodlss.ini | CAS | MotionSharpnessDebug | auto |
| OptiScaler.amd-nodlss.ini | CAS | MotionSharpnessEnabled | auto |
| OptiScaler.amd-nodlss.ini | CAS | MotionThreshold | auto |
| OptiScaler.amd-nodlss.ini | DLSS | Enabled | auto |
| OptiScaler.amd-nodlss.ini | DLSS | FeaturePath | auto |
| OptiScaler.amd-nodlss.ini | DLSS | LibraryPath | auto |
| OptiScaler.amd-nodlss.ini | DLSS | NVNGX_DLSS_Path | auto |
| OptiScaler.amd-nodlss.ini | DLSS | RenderPresetBalanced | auto |
| OptiScaler.amd-nodlss.ini | DLSS | RenderPresetDLAA | auto |
| OptiScaler.amd-nodlss.ini | DLSS | RenderPresetForAll | auto |
| OptiScaler.amd-nodlss.ini | DLSS | RenderPresetOverride | auto |
| OptiScaler.amd-nodlss.ini | DLSS | RenderPresetPerformance | auto |
| OptiScaler.amd-nodlss.ini | DLSS | RenderPresetQuality | auto |
| OptiScaler.amd-nodlss.ini | DLSS | RenderPresetUltraPerformance | auto |
| OptiScaler.amd-nodlss.ini | DLSS | RenderPresetUltraQuality | auto |
| OptiScaler.amd-nodlss.ini | DLSS | UseGenericAppIdWithDlss | auto |
| OptiScaler.amd-nodlss.ini | DLSSD | RenderPresetBalanced | auto |
| OptiScaler.amd-nodlss.ini | DLSSD | RenderPresetDLAA | auto |
| OptiScaler.amd-nodlss.ini | DLSSD | RenderPresetForAll | auto |
| OptiScaler.amd-nodlss.ini | DLSSD | RenderPresetOverride | auto |
| OptiScaler.amd-nodlss.ini | DLSSD | RenderPresetPerformance | auto |
| OptiScaler.amd-nodlss.ini | DLSSD | RenderPresetQuality | auto |
| OptiScaler.amd-nodlss.ini | DLSSD | RenderPresetUltraPerformance | auto |
| OptiScaler.amd-nodlss.ini | DLSSD | RenderPresetUltraQuality | auto |
| OptiScaler.amd-nodlss.ini | DRS | DrsMaxOverrideEnabled | auto |
| OptiScaler.amd-nodlss.ini | DRS | DrsMinOverrideEnabled | auto |
| OptiScaler.amd-nodlss.ini | Dx11withDx12 | DontUseNTShared | auto |
| OptiScaler.amd-nodlss.ini | Dx11withDx12 | UseDelayedInit | auto |
| OptiScaler.amd-nodlss.ini | FrameGen | AllowedFrameAhead | auto |
| OptiScaler.amd-nodlss.ini | FrameGen | DebugView | auto |
| OptiScaler.amd-nodlss.ini | FrameGen | DepthValidNow | auto |
| OptiScaler.amd-nodlss.ini | FrameGen | DisableHudless | auto |
| OptiScaler.amd-nodlss.ini | FrameGen | DisableUI | auto |
| OptiScaler.amd-nodlss.ini | FrameGen | DrawUIOverFG | auto |
| OptiScaler.amd-nodlss.ini | FrameGen | Enabled | auto |
| OptiScaler.amd-nodlss.ini | FrameGen | FGInput | auto |
| OptiScaler.amd-nodlss.ini | FrameGen | FGOutput | auto |
| OptiScaler.amd-nodlss.ini | FrameGen | FTInput | auto |
| OptiScaler.amd-nodlss.ini | FrameGen | HudlessValidNow | auto |
| OptiScaler.amd-nodlss.ini | FrameGen | ModifyBufferState | auto |
| OptiScaler.amd-nodlss.ini | FrameGen | ModifySCIndex | auto |
| OptiScaler.amd-nodlss.ini | FrameGen | OnlyAcceptFirstHudless | auto |
| OptiScaler.amd-nodlss.ini | FrameGen | PreserveSwapChain | auto |
| OptiScaler.amd-nodlss.ini | FrameGen | RectHeight | auto |
| OptiScaler.amd-nodlss.ini | FrameGen | RectLeft | auto |
| OptiScaler.amd-nodlss.ini | FrameGen | RectTop | auto |
| OptiScaler.amd-nodlss.ini | FrameGen | RectWidth | auto |
| OptiScaler.amd-nodlss.ini | FrameGen | SkipReset | auto |
| OptiScaler.amd-nodlss.ini | FrameGen | SkipResizeBuffers | auto |
| OptiScaler.amd-nodlss.ini | FrameGen | UIPremultipliedAlpha | auto |
| OptiScaler.amd-nodlss.ini | FrameGen | VelocityValidNow | auto |
| OptiScaler.amd-nodlss.ini | Framerate | FramerateLimit | auto |
| OptiScaler.amd-nodlss.ini | FSR | AccAddPerFrame | auto |
| OptiScaler.amd-nodlss.ini | FSR | CameraFar | auto |
| OptiScaler.amd-nodlss.ini | FSR | CameraNear | auto |
| OptiScaler.amd-nodlss.ini | FSR | DebugView | auto |
| OptiScaler.amd-nodlss.ini | FSR | DlssReactiveMaskBias | auto |
| OptiScaler.amd-nodlss.ini | FSR | FfxDx12Path | auto |
| OptiScaler.amd-nodlss.ini | FSR | FfxVkPath | auto |
| OptiScaler.amd-nodlss.ini | FSR | FGIndex | auto |
| OptiScaler.amd-nodlss.ini | FSR | Fsr4EnableDebugView | auto |
| OptiScaler.amd-nodlss.ini | FSR | Fsr4EnableWatermark | auto |
| OptiScaler.amd-nodlss.ini | FSR | Fsr4Model | auto |
| OptiScaler.amd-nodlss.ini | FSR | Fsr4Update | auto |
| OptiScaler.amd-nodlss.ini | FSR | FsrAgilitySDKUpgrade | auto |
| OptiScaler.amd-nodlss.ini | FSR | FsrNonLinearColorSpace | auto |
| OptiScaler.amd-nodlss.ini | FSR | FsrNonLinearPQ | auto |
| OptiScaler.amd-nodlss.ini | FSR | FsrNonLinearSRGB | auto |
| OptiScaler.amd-nodlss.ini | FSR | HorizontalFov | auto |
| OptiScaler.amd-nodlss.ini | FSR | MinDisOccAcc | auto |
| OptiScaler.amd-nodlss.ini | FSR | ReactiveScale | auto |
| OptiScaler.amd-nodlss.ini | FSR | ShadingScale | auto |
| OptiScaler.amd-nodlss.ini | FSR | UpscalerIndex | auto |
| OptiScaler.amd-nodlss.ini | FSR | UseFsrInputValues | auto |
| OptiScaler.amd-nodlss.ini | FSR | UseReactiveMaskForTransparency | auto |
| OptiScaler.amd-nodlss.ini | FSR | VelocityFactor | auto |
| OptiScaler.amd-nodlss.ini | FSR | VerticalFov | auto |
| OptiScaler.amd-nodlss.ini | FSRFG | AllowAsync | auto |
| OptiScaler.amd-nodlss.ini | FSRFG | DebugPacingLines | auto |
| OptiScaler.amd-nodlss.ini | FSRFG | DebugResetLines | auto |
| OptiScaler.amd-nodlss.ini | FSRFG | DebugTearLines | auto |
| OptiScaler.amd-nodlss.ini | FSRFG | EnableWatermark | auto |
| OptiScaler.amd-nodlss.ini | FSRFG | FPTHybridSpin | auto |
| OptiScaler.amd-nodlss.ini | FSRFG | FPTHybridSpinTime | auto |
| OptiScaler.amd-nodlss.ini | FSRFG | FPTSafetyMarginInMs | auto |
| OptiScaler.amd-nodlss.ini | FSRFG | FPTVarianceFactor | auto |
| OptiScaler.amd-nodlss.ini | FSRFG | FPTWaitForSingleObjectOnFence | auto |
| OptiScaler.amd-nodlss.ini | FSRFG | FramePacingTuning | auto |
| OptiScaler.amd-nodlss.ini | FSRFG | UseMutexForSwapchain | auto |
| OptiScaler.amd-nodlss.ini | FSRFGInputs | SkipConfigForHudless | auto |
| OptiScaler.amd-nodlss.ini | FSRFGInputs | SkipDispatchForHudless | auto |
| OptiScaler.amd-nodlss.ini | HDR | ForceHDR | auto |
| OptiScaler.amd-nodlss.ini | HDR | SkipColorSpace | auto |
| OptiScaler.amd-nodlss.ini | HDR | UseHDR10 | auto |
| OptiScaler.amd-nodlss.ini | Hooks | EarlyHooking | auto |
| OptiScaler.amd-nodlss.ini | Hooks | HookOriginalNvngxOnly | auto |
| OptiScaler.amd-nodlss.ini | Hooks | UseNtdllHooks | auto |
| OptiScaler.amd-nodlss.ini | Hotfix | CheckForUpdate | auto |
| OptiScaler.amd-nodlss.ini | Hotfix | ColorMaskResourceBarrier | auto |
| OptiScaler.amd-nodlss.ini | Hotfix | ColorResourceBarrier | auto |
| OptiScaler.amd-nodlss.ini | Hotfix | DepthResourceBarrier | auto |
| OptiScaler.amd-nodlss.ini | Hotfix | DisableOverlays | auto |
| OptiScaler.amd-nodlss.ini | Hotfix | DontCreateD3D12DeviceForLuma | auto |
| OptiScaler.amd-nodlss.ini | Hotfix | ExposureResourceBarrier | auto |
| OptiScaler.amd-nodlss.ini | Hotfix | MotionVectorResourceBarrier | auto |
| OptiScaler.amd-nodlss.ini | Hotfix | OutputResourceBarrier | auto |
| OptiScaler.amd-nodlss.ini | Hotfix | PreferDedicatedGpu | auto |
| OptiScaler.amd-nodlss.ini | Hotfix | PreferFirstDedicatedGpu | auto |
| OptiScaler.amd-nodlss.ini | Hotfix | RestoreComputeSignature | auto |
| OptiScaler.amd-nodlss.ini | Hotfix | RestoreGraphicSignature | auto |
| OptiScaler.amd-nodlss.ini | Hotfix | RoundInternalResolution | auto |
| OptiScaler.amd-nodlss.ini | Hotfix | SkipFirstFrames | auto |
| OptiScaler.amd-nodlss.ini | Hotfix | UsePrecompiledShaders | auto |
| OptiScaler.amd-nodlss.ini | InitFlags | AutoExposure | auto |
| OptiScaler.amd-nodlss.ini | InitFlags | DepthInverted | auto |
| OptiScaler.amd-nodlss.ini | InitFlags | DisableReactiveMask | auto |
| OptiScaler.amd-nodlss.ini | InitFlags | DisplayResolution | auto |
| OptiScaler.amd-nodlss.ini | InitFlags | HDR | auto |
| OptiScaler.amd-nodlss.ini | InitFlags | JitterCancellation | auto |
| OptiScaler.amd-nodlss.ini | Inputs | EnableDlssInputs | auto |
| OptiScaler.amd-nodlss.ini | Inputs | EnableFfxInputs | auto |
| OptiScaler.amd-nodlss.ini | Inputs | EnableFsr2Inputs | auto |
| OptiScaler.amd-nodlss.ini | Inputs | EnableFsr3Inputs | auto |
| OptiScaler.amd-nodlss.ini | Inputs | EnableHotSwapping | auto |
| OptiScaler.amd-nodlss.ini | Inputs | EnableXeSSInputs | auto |
| OptiScaler.amd-nodlss.ini | Inputs | Fsr2Pattern | auto |
| OptiScaler.amd-nodlss.ini | Inputs | Fsr3Pattern | auto |
| OptiScaler.amd-nodlss.ini | Inputs | UseFfxInputs | auto |
| OptiScaler.amd-nodlss.ini | Inputs | UseFsr2Dx11Inputs | auto |
| OptiScaler.amd-nodlss.ini | Inputs | UseFsr2Inputs | auto |
| OptiScaler.amd-nodlss.ini | Inputs | UseFsr2VulkanInputs | auto |
| OptiScaler.amd-nodlss.ini | Inputs | UseFsr3Inputs | auto |
| OptiScaler.amd-nodlss.ini | Log | LogAsync | auto |
| OptiScaler.amd-nodlss.ini | Log | LogAsyncThreads | auto |
| OptiScaler.amd-nodlss.ini | Log | LogFile | auto |
| OptiScaler.amd-nodlss.ini | Log | LogLevel | auto |
| OptiScaler.amd-nodlss.ini | Log | LogToConsole | auto |
| OptiScaler.amd-nodlss.ini | Log | LogToDebug | auto |
| OptiScaler.amd-nodlss.ini | Log | LogToFile | auto |
| OptiScaler.amd-nodlss.ini | Log | LogToNGX | auto |
| OptiScaler.amd-nodlss.ini | Log | OpenConsole | auto |
| OptiScaler.amd-nodlss.ini | Log | SingleFile | auto |
| OptiScaler.amd-nodlss.ini | Menu | DisableSplash | auto |
| OptiScaler.amd-nodlss.ini | Menu | ExtendedLimits | auto |
| OptiScaler.amd-nodlss.ini | Menu | FGShortcutKey | -1 |
| OptiScaler.amd-nodlss.ini | Menu | FpsCycleShortcutKey | -1 |
| OptiScaler.amd-nodlss.ini | Menu | FpsOverlayAlpha | auto |
| OptiScaler.amd-nodlss.ini | Menu | FpsOverlayHorizontal | auto |
| OptiScaler.amd-nodlss.ini | Menu | FpsOverlayPos | auto |
| OptiScaler.amd-nodlss.ini | Menu | FpsOverlayType | auto |
| OptiScaler.amd-nodlss.ini | Menu | FpsScale | auto |
| OptiScaler.amd-nodlss.ini | Menu | FpsShortcutKey | -1 |
| OptiScaler.amd-nodlss.ini | Menu | OverlayMenu | auto |
| OptiScaler.amd-nodlss.ini | Menu | Scale | auto |
| OptiScaler.amd-nodlss.ini | Menu | ShortcutKey | auto |
| OptiScaler.amd-nodlss.ini | Menu | ShowFps | auto |
| OptiScaler.amd-nodlss.ini | Menu | TTFFontPath | auto |
| OptiScaler.amd-nodlss.ini | Menu | UseHQFont | auto |
| OptiScaler.amd-nodlss.ini | Mipmap | MipmapBiasFixedOverride | auto |
| OptiScaler.amd-nodlss.ini | Mipmap | MipmapBiasOverride | auto |
| OptiScaler.amd-nodlss.ini | Mipmap | MipmapBiasOverrideAll | auto |
| OptiScaler.amd-nodlss.ini | Mipmap | MipmapBiasScaleOverride | auto |
| OptiScaler.amd-nodlss.ini | Nukems | MakeDepthCopy | auto |
| OptiScaler.amd-nodlss.ini | NvApi | DisableFlipMetering | auto |
| OptiScaler.amd-nodlss.ini | NvApi | DontUseFakenvapiForXeLLOnNvidia | auto |
| OptiScaler.amd-nodlss.ini | NvApi | NvapiDllPath | auto |
| OptiScaler.amd-nodlss.ini | NvApi | OverrideNvapiDll | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | AlwaysCaptureFSRFGSwapchain | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | AlwaysTrackHeaps | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | DepthScaleMax | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | DisableHUDFix | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | EnableDepthScale | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | HUDFix | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | HudfixDisableDI | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | HudfixDisableDII | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | HudfixDisableDispatch | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | HudfixDisableOM | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | HudfixDisableRTV | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | HudfixDisableSCR | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | HudfixDisableSGR | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | HudfixDisableSRV | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | HudfixDisableUAV | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | HUDFixDontUseSwapchainBuffers | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | HUDFixExtended | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | HUDFixImmediate | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | HUDFixRelaxedResolutionCheck | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | HUDLimit | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | MakeDepthCopy | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | MakeMVCopy | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | ResourceBlocking | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | ResourceFlip | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | ResourceFlipOffset | auto |
| OptiScaler.amd-nodlss.ini | OptiFG | UseShards | auto |
| OptiScaler.amd-nodlss.ini | OutputScaling | Downscaler | auto |
| OptiScaler.amd-nodlss.ini | OutputScaling | Enabled | auto |
| OptiScaler.amd-nodlss.ini | OutputScaling | Multiplier | auto |
| OptiScaler.amd-nodlss.ini | Plugins | LoadAsiPlugins | auto |
| OptiScaler.amd-nodlss.ini | Plugins | LoadReshade | auto |
| OptiScaler.amd-nodlss.ini | Plugins | LoadSpecialK | auto |
| OptiScaler.amd-nodlss.ini | Plugins | Path | auto |
| OptiScaler.amd-nodlss.ini | ProcessFilter | ProcessExclusionList | auto |
| OptiScaler.amd-nodlss.ini | ProcessFilter | TargetProcessName | auto |
| OptiScaler.amd-nodlss.ini | QualityOverrides | QualityRatioBalanced | auto |
| OptiScaler.amd-nodlss.ini | QualityOverrides | QualityRatioDLAA | auto |
| OptiScaler.amd-nodlss.ini | QualityOverrides | QualityRatioOverrideEnabled | auto |
| OptiScaler.amd-nodlss.ini | QualityOverrides | QualityRatioPerformance | auto |
| OptiScaler.amd-nodlss.ini | QualityOverrides | QualityRatioQuality | auto |
| OptiScaler.amd-nodlss.ini | QualityOverrides | QualityRatioUltraPerformance | auto |
| OptiScaler.amd-nodlss.ini | QualityOverrides | QualityRatioUltraQuality | auto |
| OptiScaler.amd-nodlss.ini | Sharpness | OverrideSharpness | auto |
| OptiScaler.amd-nodlss.ini | Sharpness | Sharpness | auto |
| OptiScaler.amd-nodlss.ini | Spoofing | D3DFeatureLevel | auto |
| OptiScaler.amd-nodlss.ini | Spoofing | Dxgi | false |
| OptiScaler.amd-nodlss.ini | Spoofing | DxgiBlacklist | auto |
| OptiScaler.amd-nodlss.ini | Spoofing | DxgiFactoryWrapping | auto |
| OptiScaler.amd-nodlss.ini | Spoofing | DxgiVRAM | auto |
| OptiScaler.amd-nodlss.ini | Spoofing | Registry | auto |
| OptiScaler.amd-nodlss.ini | Spoofing | RegistryDriver | auto |
| OptiScaler.amd-nodlss.ini | Spoofing | SpoofedDeviceId | auto |
| OptiScaler.amd-nodlss.ini | Spoofing | SpoofedGPUName | auto |
| OptiScaler.amd-nodlss.ini | Spoofing | SpoofedVendorId | auto |
| OptiScaler.amd-nodlss.ini | Spoofing | SpoofHAGS | auto |
| OptiScaler.amd-nodlss.ini | Spoofing | StreamlineSpoofing | auto |
| OptiScaler.amd-nodlss.ini | Spoofing | TargetDeviceId | auto |
| OptiScaler.amd-nodlss.ini | Spoofing | TargetVendorId | auto |
| OptiScaler.amd-nodlss.ini | Spoofing | UEIntelAtomics | auto |
| OptiScaler.amd-nodlss.ini | Spoofing | User32 | auto |
| OptiScaler.amd-nodlss.ini | Spoofing | Vulkan | auto |
| OptiScaler.amd-nodlss.ini | Spoofing | VulkanExtensionSpoofing | auto |
| OptiScaler.amd-nodlss.ini | Spoofing | VulkanVRAM | auto |
| OptiScaler.amd-nodlss.ini | UpscaleRatio | UpscaleRatioOverrideEnabled | auto |
| OptiScaler.amd-nodlss.ini | UpscaleRatio | UpscaleRatioOverrideValue | auto |
| OptiScaler.amd-nodlss.ini | Upscalers | Dx11Upscaler | auto |
| OptiScaler.amd-nodlss.ini | Upscalers | Dx12Upscaler | auto |
| OptiScaler.amd-nodlss.ini | Upscalers | VulkanUpscaler | auto |
| OptiScaler.amd-nodlss.ini | V-Sync | ForceVsync | auto |
| OptiScaler.amd-nodlss.ini | V-Sync | OverrideVsync | auto |
| OptiScaler.amd-nodlss.ini | V-Sync | SyncInterval | auto |
| OptiScaler.amd-nodlss.ini | XeFG | DebugView | auto |
| OptiScaler.amd-nodlss.ini | XeFG | DepthInverted | auto |
| OptiScaler.amd-nodlss.ini | XeFG | ForceBorderless | auto |
| OptiScaler.amd-nodlss.ini | XeFG | HighResMV | auto |
| OptiScaler.amd-nodlss.ini | XeFG | IgnoreInitChecks | auto |
| OptiScaler.amd-nodlss.ini | XeFG | InterpolationCount | auto |
| OptiScaler.amd-nodlss.ini | XeFG | JitteredMV | auto |
| OptiScaler.amd-nodlss.ini | XeFG | UIComposition | auto |
| OptiScaler.amd-nodlss.ini | XeSS | BuildPipelines | auto |
| OptiScaler.amd-nodlss.ini | XeSS | CreateHeaps | auto |
| OptiScaler.amd-nodlss.ini | XeSS | Dx11LibraryPath | auto |
| OptiScaler.amd-nodlss.ini | XeSS | LibraryPath | auto |
| OptiScaler.amd-nodlss.ini | XeSS | NetworkModel | auto |
| OptiScaler.nvidia.ini | Anisotropy | AnisotropyOverride | auto |
| OptiScaler.nvidia.ini | Anisotropy | ModifyComparison | auto |
| OptiScaler.nvidia.ini | Anisotropy | ModifyMinMax | auto |
| OptiScaler.nvidia.ini | Anisotropy | SkipPointFilter | auto |
| OptiScaler.nvidia.ini | CAS | Contrast | auto |
| OptiScaler.nvidia.ini | CAS | ContrastEnabled | auto |
| OptiScaler.nvidia.ini | CAS | Enabled | auto |
| OptiScaler.nvidia.ini | CAS | MotionScaleLimit | auto |
| OptiScaler.nvidia.ini | CAS | MotionSharpness | auto |
| OptiScaler.nvidia.ini | CAS | MotionSharpnessDebug | auto |
| OptiScaler.nvidia.ini | CAS | MotionSharpnessEnabled | auto |
| OptiScaler.nvidia.ini | CAS | MotionThreshold | auto |
| OptiScaler.nvidia.ini | DLSS | Enabled | auto |
| OptiScaler.nvidia.ini | DLSS | FeaturePath | auto |
| OptiScaler.nvidia.ini | DLSS | LibraryPath | auto |
| OptiScaler.nvidia.ini | DLSS | NVNGX_DLSS_Path | auto |
| OptiScaler.nvidia.ini | DLSS | RenderPresetBalanced | auto |
| OptiScaler.nvidia.ini | DLSS | RenderPresetDLAA | auto |
| OptiScaler.nvidia.ini | DLSS | RenderPresetForAll | auto |
| OptiScaler.nvidia.ini | DLSS | RenderPresetOverride | auto |
| OptiScaler.nvidia.ini | DLSS | RenderPresetPerformance | auto |
| OptiScaler.nvidia.ini | DLSS | RenderPresetQuality | auto |
| OptiScaler.nvidia.ini | DLSS | RenderPresetUltraPerformance | auto |
| OptiScaler.nvidia.ini | DLSS | RenderPresetUltraQuality | auto |
| OptiScaler.nvidia.ini | DLSS | UseGenericAppIdWithDlss | auto |
| OptiScaler.nvidia.ini | DLSSD | RenderPresetBalanced | auto |
| OptiScaler.nvidia.ini | DLSSD | RenderPresetDLAA | auto |
| OptiScaler.nvidia.ini | DLSSD | RenderPresetForAll | auto |
| OptiScaler.nvidia.ini | DLSSD | RenderPresetOverride | auto |
| OptiScaler.nvidia.ini | DLSSD | RenderPresetPerformance | auto |
| OptiScaler.nvidia.ini | DLSSD | RenderPresetQuality | auto |
| OptiScaler.nvidia.ini | DLSSD | RenderPresetUltraPerformance | auto |
| OptiScaler.nvidia.ini | DLSSD | RenderPresetUltraQuality | auto |
| OptiScaler.nvidia.ini | DRS | DrsMaxOverrideEnabled | auto |
| OptiScaler.nvidia.ini | DRS | DrsMinOverrideEnabled | auto |
| OptiScaler.nvidia.ini | Dx11withDx12 | DontUseNTShared | auto |
| OptiScaler.nvidia.ini | Dx11withDx12 | UseDelayedInit | auto |
| OptiScaler.nvidia.ini | FrameGen | AllowedFrameAhead | auto |
| OptiScaler.nvidia.ini | FrameGen | DebugView | auto |
| OptiScaler.nvidia.ini | FrameGen | DepthValidNow | auto |
| OptiScaler.nvidia.ini | FrameGen | DisableHudless | auto |
| OptiScaler.nvidia.ini | FrameGen | DisableUI | auto |
| OptiScaler.nvidia.ini | FrameGen | DrawUIOverFG | auto |
| OptiScaler.nvidia.ini | FrameGen | Enabled | auto |
| OptiScaler.nvidia.ini | FrameGen | FGInput | auto |
| OptiScaler.nvidia.ini | FrameGen | FGOutput | auto |
| OptiScaler.nvidia.ini | FrameGen | FTInput | auto |
| OptiScaler.nvidia.ini | FrameGen | HudlessValidNow | auto |
| OptiScaler.nvidia.ini | FrameGen | ModifyBufferState | auto |
| OptiScaler.nvidia.ini | FrameGen | ModifySCIndex | auto |
| OptiScaler.nvidia.ini | FrameGen | OnlyAcceptFirstHudless | auto |
| OptiScaler.nvidia.ini | FrameGen | PreserveSwapChain | auto |
| OptiScaler.nvidia.ini | FrameGen | RectHeight | auto |
| OptiScaler.nvidia.ini | FrameGen | RectLeft | auto |
| OptiScaler.nvidia.ini | FrameGen | RectTop | auto |
| OptiScaler.nvidia.ini | FrameGen | RectWidth | auto |
| OptiScaler.nvidia.ini | FrameGen | SkipReset | auto |
| OptiScaler.nvidia.ini | FrameGen | SkipResizeBuffers | auto |
| OptiScaler.nvidia.ini | FrameGen | UIPremultipliedAlpha | auto |
| OptiScaler.nvidia.ini | FrameGen | VelocityValidNow | auto |
| OptiScaler.nvidia.ini | Framerate | FramerateLimit | auto |
| OptiScaler.nvidia.ini | FSR | AccAddPerFrame | auto |
| OptiScaler.nvidia.ini | FSR | CameraFar | auto |
| OptiScaler.nvidia.ini | FSR | CameraNear | auto |
| OptiScaler.nvidia.ini | FSR | DebugView | auto |
| OptiScaler.nvidia.ini | FSR | DlssReactiveMaskBias | auto |
| OptiScaler.nvidia.ini | FSR | FfxDx12Path | auto |
| OptiScaler.nvidia.ini | FSR | FfxVkPath | auto |
| OptiScaler.nvidia.ini | FSR | FGIndex | auto |
| OptiScaler.nvidia.ini | FSR | Fsr4EnableDebugView | auto |
| OptiScaler.nvidia.ini | FSR | Fsr4EnableWatermark | auto |
| OptiScaler.nvidia.ini | FSR | Fsr4Model | auto |
| OptiScaler.nvidia.ini | FSR | Fsr4Update | auto |
| OptiScaler.nvidia.ini | FSR | FsrAgilitySDKUpgrade | auto |
| OptiScaler.nvidia.ini | FSR | FsrNonLinearColorSpace | auto |
| OptiScaler.nvidia.ini | FSR | FsrNonLinearPQ | auto |
| OptiScaler.nvidia.ini | FSR | FsrNonLinearSRGB | auto |
| OptiScaler.nvidia.ini | FSR | HorizontalFov | auto |
| OptiScaler.nvidia.ini | FSR | MinDisOccAcc | auto |
| OptiScaler.nvidia.ini | FSR | ReactiveScale | auto |
| OptiScaler.nvidia.ini | FSR | ShadingScale | auto |
| OptiScaler.nvidia.ini | FSR | UpscalerIndex | auto |
| OptiScaler.nvidia.ini | FSR | UseFsrInputValues | auto |
| OptiScaler.nvidia.ini | FSR | UseReactiveMaskForTransparency | auto |
| OptiScaler.nvidia.ini | FSR | VelocityFactor | auto |
| OptiScaler.nvidia.ini | FSR | VerticalFov | auto |
| OptiScaler.nvidia.ini | FSRFG | AllowAsync | auto |
| OptiScaler.nvidia.ini | FSRFG | DebugPacingLines | auto |
| OptiScaler.nvidia.ini | FSRFG | DebugResetLines | auto |
| OptiScaler.nvidia.ini | FSRFG | DebugTearLines | auto |
| OptiScaler.nvidia.ini | FSRFG | EnableWatermark | auto |
| OptiScaler.nvidia.ini | FSRFG | FPTHybridSpin | auto |
| OptiScaler.nvidia.ini | FSRFG | FPTHybridSpinTime | auto |
| OptiScaler.nvidia.ini | FSRFG | FPTSafetyMarginInMs | auto |
| OptiScaler.nvidia.ini | FSRFG | FPTVarianceFactor | auto |
| OptiScaler.nvidia.ini | FSRFG | FPTWaitForSingleObjectOnFence | auto |
| OptiScaler.nvidia.ini | FSRFG | FramePacingTuning | auto |
| OptiScaler.nvidia.ini | FSRFG | UseMutexForSwapchain | auto |
| OptiScaler.nvidia.ini | FSRFGInputs | SkipConfigForHudless | auto |
| OptiScaler.nvidia.ini | FSRFGInputs | SkipDispatchForHudless | auto |
| OptiScaler.nvidia.ini | HDR | ForceHDR | auto |
| OptiScaler.nvidia.ini | HDR | SkipColorSpace | auto |
| OptiScaler.nvidia.ini | HDR | UseHDR10 | auto |
| OptiScaler.nvidia.ini | Hooks | EarlyHooking | auto |
| OptiScaler.nvidia.ini | Hooks | HookOriginalNvngxOnly | auto |
| OptiScaler.nvidia.ini | Hooks | UseNtdllHooks | auto |
| OptiScaler.nvidia.ini | Hotfix | CheckForUpdate | auto |
| OptiScaler.nvidia.ini | Hotfix | ColorMaskResourceBarrier | auto |
| OptiScaler.nvidia.ini | Hotfix | ColorResourceBarrier | auto |
| OptiScaler.nvidia.ini | Hotfix | DepthResourceBarrier | auto |
| OptiScaler.nvidia.ini | Hotfix | DisableOverlays | auto |
| OptiScaler.nvidia.ini | Hotfix | DontCreateD3D12DeviceForLuma | auto |
| OptiScaler.nvidia.ini | Hotfix | ExposureResourceBarrier | auto |
| OptiScaler.nvidia.ini | Hotfix | MotionVectorResourceBarrier | auto |
| OptiScaler.nvidia.ini | Hotfix | OutputResourceBarrier | auto |
| OptiScaler.nvidia.ini | Hotfix | PreferDedicatedGpu | auto |
| OptiScaler.nvidia.ini | Hotfix | PreferFirstDedicatedGpu | auto |
| OptiScaler.nvidia.ini | Hotfix | RestoreComputeSignature | auto |
| OptiScaler.nvidia.ini | Hotfix | RestoreGraphicSignature | auto |
| OptiScaler.nvidia.ini | Hotfix | RoundInternalResolution | auto |
| OptiScaler.nvidia.ini | Hotfix | SkipFirstFrames | auto |
| OptiScaler.nvidia.ini | Hotfix | UsePrecompiledShaders | auto |
| OptiScaler.nvidia.ini | InitFlags | AutoExposure | auto |
| OptiScaler.nvidia.ini | InitFlags | DepthInverted | auto |
| OptiScaler.nvidia.ini | InitFlags | DisableReactiveMask | auto |
| OptiScaler.nvidia.ini | InitFlags | DisplayResolution | auto |
| OptiScaler.nvidia.ini | InitFlags | HDR | auto |
| OptiScaler.nvidia.ini | InitFlags | JitterCancellation | auto |
| OptiScaler.nvidia.ini | Inputs | EnableDlssInputs | auto |
| OptiScaler.nvidia.ini | Inputs | EnableFfxInputs | auto |
| OptiScaler.nvidia.ini | Inputs | EnableFsr2Inputs | auto |
| OptiScaler.nvidia.ini | Inputs | EnableFsr3Inputs | auto |
| OptiScaler.nvidia.ini | Inputs | EnableHotSwapping | auto |
| OptiScaler.nvidia.ini | Inputs | EnableXeSSInputs | auto |
| OptiScaler.nvidia.ini | Inputs | Fsr2Pattern | auto |
| OptiScaler.nvidia.ini | Inputs | Fsr3Pattern | auto |
| OptiScaler.nvidia.ini | Inputs | UseFfxInputs | auto |
| OptiScaler.nvidia.ini | Inputs | UseFsr2Dx11Inputs | auto |
| OptiScaler.nvidia.ini | Inputs | UseFsr2Inputs | auto |
| OptiScaler.nvidia.ini | Inputs | UseFsr2VulkanInputs | auto |
| OptiScaler.nvidia.ini | Inputs | UseFsr3Inputs | auto |
| OptiScaler.nvidia.ini | Log | LogAsync | auto |
| OptiScaler.nvidia.ini | Log | LogAsyncThreads | auto |
| OptiScaler.nvidia.ini | Log | LogFile | auto |
| OptiScaler.nvidia.ini | Log | LogLevel | auto |
| OptiScaler.nvidia.ini | Log | LogToConsole | auto |
| OptiScaler.nvidia.ini | Log | LogToDebug | auto |
| OptiScaler.nvidia.ini | Log | LogToFile | auto |
| OptiScaler.nvidia.ini | Log | LogToNGX | auto |
| OptiScaler.nvidia.ini | Log | OpenConsole | auto |
| OptiScaler.nvidia.ini | Log | SingleFile | auto |
| OptiScaler.nvidia.ini | Menu | DisableSplash | auto |
| OptiScaler.nvidia.ini | Menu | ExtendedLimits | auto |
| OptiScaler.nvidia.ini | Menu | FGShortcutKey | -1 |
| OptiScaler.nvidia.ini | Menu | FpsCycleShortcutKey | -1 |
| OptiScaler.nvidia.ini | Menu | FpsOverlayAlpha | auto |
| OptiScaler.nvidia.ini | Menu | FpsOverlayHorizontal | auto |
| OptiScaler.nvidia.ini | Menu | FpsOverlayPos | auto |
| OptiScaler.nvidia.ini | Menu | FpsOverlayType | auto |
| OptiScaler.nvidia.ini | Menu | FpsScale | auto |
| OptiScaler.nvidia.ini | Menu | FpsShortcutKey | -1 |
| OptiScaler.nvidia.ini | Menu | OverlayMenu | auto |
| OptiScaler.nvidia.ini | Menu | Scale | auto |
| OptiScaler.nvidia.ini | Menu | ShortcutKey | auto |
| OptiScaler.nvidia.ini | Menu | ShowFps | auto |
| OptiScaler.nvidia.ini | Menu | TTFFontPath | auto |
| OptiScaler.nvidia.ini | Menu | UseHQFont | auto |
| OptiScaler.nvidia.ini | Mipmap | MipmapBiasFixedOverride | auto |
| OptiScaler.nvidia.ini | Mipmap | MipmapBiasOverride | auto |
| OptiScaler.nvidia.ini | Mipmap | MipmapBiasOverrideAll | auto |
| OptiScaler.nvidia.ini | Mipmap | MipmapBiasScaleOverride | auto |
| OptiScaler.nvidia.ini | Nukems | MakeDepthCopy | auto |
| OptiScaler.nvidia.ini | NvApi | DisableFlipMetering | auto |
| OptiScaler.nvidia.ini | NvApi | DontUseFakenvapiForXeLLOnNvidia | auto |
| OptiScaler.nvidia.ini | NvApi | NvapiDllPath | auto |
| OptiScaler.nvidia.ini | NvApi | OverrideNvapiDll | auto |
| OptiScaler.nvidia.ini | OptiFG | AlwaysCaptureFSRFGSwapchain | auto |
| OptiScaler.nvidia.ini | OptiFG | AlwaysTrackHeaps | auto |
| OptiScaler.nvidia.ini | OptiFG | DepthScaleMax | auto |
| OptiScaler.nvidia.ini | OptiFG | DisableHUDFix | auto |
| OptiScaler.nvidia.ini | OptiFG | EnableDepthScale | auto |
| OptiScaler.nvidia.ini | OptiFG | HUDFix | auto |
| OptiScaler.nvidia.ini | OptiFG | HudfixDisableDI | auto |
| OptiScaler.nvidia.ini | OptiFG | HudfixDisableDII | auto |
| OptiScaler.nvidia.ini | OptiFG | HudfixDisableDispatch | auto |
| OptiScaler.nvidia.ini | OptiFG | HudfixDisableOM | auto |
| OptiScaler.nvidia.ini | OptiFG | HudfixDisableRTV | auto |
| OptiScaler.nvidia.ini | OptiFG | HudfixDisableSCR | auto |
| OptiScaler.nvidia.ini | OptiFG | HudfixDisableSGR | auto |
| OptiScaler.nvidia.ini | OptiFG | HudfixDisableSRV | auto |
| OptiScaler.nvidia.ini | OptiFG | HudfixDisableUAV | auto |
| OptiScaler.nvidia.ini | OptiFG | HUDFixDontUseSwapchainBuffers | auto |
| OptiScaler.nvidia.ini | OptiFG | HUDFixExtended | auto |
| OptiScaler.nvidia.ini | OptiFG | HUDFixImmediate | auto |
| OptiScaler.nvidia.ini | OptiFG | HUDFixRelaxedResolutionCheck | auto |
| OptiScaler.nvidia.ini | OptiFG | HUDLimit | auto |
| OptiScaler.nvidia.ini | OptiFG | MakeDepthCopy | auto |
| OptiScaler.nvidia.ini | OptiFG | MakeMVCopy | auto |
| OptiScaler.nvidia.ini | OptiFG | ResourceBlocking | auto |
| OptiScaler.nvidia.ini | OptiFG | ResourceFlip | auto |
| OptiScaler.nvidia.ini | OptiFG | ResourceFlipOffset | auto |
| OptiScaler.nvidia.ini | OptiFG | UseShards | auto |
| OptiScaler.nvidia.ini | OutputScaling | Downscaler | auto |
| OptiScaler.nvidia.ini | OutputScaling | Enabled | auto |
| OptiScaler.nvidia.ini | OutputScaling | Multiplier | auto |
| OptiScaler.nvidia.ini | Plugins | LoadAsiPlugins | auto |
| OptiScaler.nvidia.ini | Plugins | LoadReshade | auto |
| OptiScaler.nvidia.ini | Plugins | LoadSpecialK | auto |
| OptiScaler.nvidia.ini | Plugins | Path | auto |
| OptiScaler.nvidia.ini | ProcessFilter | ProcessExclusionList | auto |
| OptiScaler.nvidia.ini | ProcessFilter | TargetProcessName | auto |
| OptiScaler.nvidia.ini | QualityOverrides | QualityRatioBalanced | auto |
| OptiScaler.nvidia.ini | QualityOverrides | QualityRatioDLAA | auto |
| OptiScaler.nvidia.ini | QualityOverrides | QualityRatioOverrideEnabled | auto |
| OptiScaler.nvidia.ini | QualityOverrides | QualityRatioPerformance | auto |
| OptiScaler.nvidia.ini | QualityOverrides | QualityRatioQuality | auto |
| OptiScaler.nvidia.ini | QualityOverrides | QualityRatioUltraPerformance | auto |
| OptiScaler.nvidia.ini | QualityOverrides | QualityRatioUltraQuality | auto |
| OptiScaler.nvidia.ini | Sharpness | OverrideSharpness | auto |
| OptiScaler.nvidia.ini | Sharpness | Sharpness | auto |
| OptiScaler.nvidia.ini | Spoofing | D3DFeatureLevel | auto |
| OptiScaler.nvidia.ini | Spoofing | Dxgi | auto |
| OptiScaler.nvidia.ini | Spoofing | DxgiBlacklist | auto |
| OptiScaler.nvidia.ini | Spoofing | DxgiFactoryWrapping | auto |
| OptiScaler.nvidia.ini | Spoofing | DxgiVRAM | auto |
| OptiScaler.nvidia.ini | Spoofing | Registry | auto |
| OptiScaler.nvidia.ini | Spoofing | RegistryDriver | auto |
| OptiScaler.nvidia.ini | Spoofing | SpoofedDeviceId | auto |
| OptiScaler.nvidia.ini | Spoofing | SpoofedGPUName | auto |
| OptiScaler.nvidia.ini | Spoofing | SpoofedVendorId | auto |
| OptiScaler.nvidia.ini | Spoofing | SpoofHAGS | auto |
| OptiScaler.nvidia.ini | Spoofing | StreamlineSpoofing | auto |
| OptiScaler.nvidia.ini | Spoofing | TargetDeviceId | auto |
| OptiScaler.nvidia.ini | Spoofing | TargetVendorId | auto |
| OptiScaler.nvidia.ini | Spoofing | UEIntelAtomics | auto |
| OptiScaler.nvidia.ini | Spoofing | User32 | auto |
| OptiScaler.nvidia.ini | Spoofing | Vulkan | auto |
| OptiScaler.nvidia.ini | Spoofing | VulkanExtensionSpoofing | auto |
| OptiScaler.nvidia.ini | Spoofing | VulkanVRAM | auto |
| OptiScaler.nvidia.ini | UpscaleRatio | UpscaleRatioOverrideEnabled | auto |
| OptiScaler.nvidia.ini | UpscaleRatio | UpscaleRatioOverrideValue | auto |
| OptiScaler.nvidia.ini | Upscalers | Dx11Upscaler | auto |
| OptiScaler.nvidia.ini | Upscalers | Dx12Upscaler | auto |
| OptiScaler.nvidia.ini | Upscalers | VulkanUpscaler | auto |
| OptiScaler.nvidia.ini | V-Sync | ForceVsync | auto |
| OptiScaler.nvidia.ini | V-Sync | OverrideVsync | auto |
| OptiScaler.nvidia.ini | V-Sync | SyncInterval | auto |
| OptiScaler.nvidia.ini | XeFG | DebugView | auto |
| OptiScaler.nvidia.ini | XeFG | DepthInverted | auto |
| OptiScaler.nvidia.ini | XeFG | ForceBorderless | auto |
| OptiScaler.nvidia.ini | XeFG | HighResMV | auto |
| OptiScaler.nvidia.ini | XeFG | IgnoreInitChecks | auto |
| OptiScaler.nvidia.ini | XeFG | InterpolationCount | auto |
| OptiScaler.nvidia.ini | XeFG | JitteredMV | auto |
| OptiScaler.nvidia.ini | XeFG | UIComposition | auto |
| OptiScaler.nvidia.ini | XeSS | BuildPipelines | auto |
| OptiScaler.nvidia.ini | XeSS | CreateHeaps | auto |
| OptiScaler.nvidia.ini | XeSS | Dx11LibraryPath | auto |
| OptiScaler.nvidia.ini | XeSS | LibraryPath | auto |
| OptiScaler.nvidia.ini | XeSS | NetworkModel | auto |
| ReShade.ini | ADDON | DisabledAddons | Generic Depth,Effect Runtime Sync |
| ReShade.ini | GENERAL | EffectSearchPaths | .\reshade-shaders\Shaders\** |
| ReShade.ini | GENERAL | TextureSearchPaths | .\reshade-shaders\Textures\** |
| ReShade.ini | OVERLAY | TutorialProgress | 4 |
| ReShade.ini | OVERLAY | VariableListUseTabs | 1 |
| reshade.rdr2.ini | ADDON | DisabledAddons | Generic Depth,Effect Runtime Sync |
| reshade.rdr2.ini | GENERAL | EffectSearchPaths | .\reshade-shaders\Shaders\** |
| reshade.rdr2.ini | GENERAL | PreprocessorDefinitions | RESHADE_DEPTH_LINEARIZATION_FAR_PLANE=1000.0,RESHADE_DEPTH_I... |
| reshade.rdr2.ini | GENERAL | TextureSearchPaths | .\reshade-shaders\Textures\** |
| reshade.rdr2.ini | INPUT | KeyOverlay | 35,0,0,0 |
| reshade.rdr2.ini | OVERLAY | TutorialProgress | 4 |
| ReShade.Vulkan.ini | ADDON | DisabledAddons | Generic Depth,Effect Runtime Sync |
| ReShade.Vulkan.ini | GENERAL | EffectSearchPaths | .\reshade-shaders\Shaders\** |
| ReShade.Vulkan.ini | GENERAL | PreprocessorDefinitions | RESHADE_DEPTH_LINEARIZATION_FAR_PLANE=1000.0,RESHADE_DEPTH_I... |
| ReShade.Vulkan.ini | GENERAL | TextureSearchPaths | .\reshade-shaders\Textures\** |

## value vocabularies (enum)

| enum | values | file |
|---|---|---|
| AchievementStore | Steam, Xbox | Achievements.cs:7 |
| AdaXessState | Ready, Missing, WrongVersion | Rtx40MfgService.cs:477 |
| AddonType | REFramework, ReShade, RenoDX, ReLimiter, DisplayCommander, OptiScaler, Luma | AddonType.cs:3 |
| AppPage | /// <summary> 
    /// The main game list sidebar and detail panel view. 
    /// </summary> 
    GameView, /// <summary> 
    /// The global settings page. 
    /// </summary> 
    Settings, /// <summary> 
    /// The About page with app info, credits, and links. 
    /// </summary> 
    About, /// <summary> 
    /// The FAQ/Quick Start guide page. 
    /// </summary> 
    Faq | AppPage.cs:6 |
| CogButtonKind | /// <summary>Main action, e.g. "Deploy X.ini". Blue.</summary> 
    Primary, /// <summary>Secondary action, e.g. "Open X.ini", "Open log". Grey.</summary> 
    Secondary, /// <summary>Destructive action, e.g. "Delete folder". Red.</summary> 
    Danger | CogDialog.cs:19 |
| DisplayModeValueKind | Number, Decimal, Choice, Text, ReShadeKey, ReLimiterKey | DisplayModeItems.cs:42 |
| DmKind | Normal, Primary, Danger | SettingsHandler.DisplayModes.cs:234 |
| DxgiFileType | Unknown, ReShade, OptiScaler, Dxvk | AuxInstallService.DllIdentification.cs:9 |
| DxvkVariant | Development, // nightly.link master builds — default, latest fixes 
    Stable, // doitsujin/dxvk tagged releases 
    LiliumHdr, // EndlesslyFlowering/dxvk HDR-mod releases | DxvkVariant.cs:3 |
| EngineType | Unknown, Unreal, UnrealLegacy, Unity, REEngine | EngineType.cs:3 |
| FOS | FOS_PICKFOLDERS, FOS_FORCEFILESYSTEM | NativeInterop.cs:20 |
| GameStatus | NotInstalled, Available, Installed, UpdateAvailable | GameStatus.cs:3 |
| GraphicsApiType | Unknown, DirectX8, DirectX9, DirectX10, DirectX11, DirectX12, Vulkan, OpenGL | GraphicsApiType.cs:3 |
| InfoSourceType | None, Manifest, Wiki, Fallback | AddonType.cs:5 |
| Kind | Hero, Cover | SteamArtworkService.cs:17 |
| LoadStatus | /// <summary>File read and parsed.</summary> 
        Ok, /// <summary>No file and no backup.</summary> 
        Missing, /// <summary>File was corrupt or missing; restored from .bak.</summary> 
        Recovered, /// <summary>File was corrupt and no usable backup exists. The bad file was kept as .corrupt-*.</summary> 
        Corrupt, /// <summary>File exists but couldn't be read (locked or access denied).</summary> 
        Unreadable | SafeJsonFile.cs:20 |
| MachineType | Native, I386, Itanium, x64 | PeHeaderService.cs:6 |
| MainLayout | Classic, Tabs, /// <summary>Mockup D: artwork hero, cover shelf, tiles that open sections in a slide-in panel. Uses the Tabs look and arrangers.</summary> 
    Spotlight | DetailLayoutHost.cs:8 |
| MiniDumpType | MiniDumpNormal, MiniDumpWithFullMemory, MiniDumpWithHandleData, MiniDumpWithThreadInfo | NativeInterop.cs:730 |
| OsIniKind | Choice, Bool, Text | OsIniEditor.cs:18 |
| PackCategory | Essential, Recommended, Custom, Extra | ShaderPackService.cs:85 |
| PadAction | Up, Down, Left, Right, Select, // A 
    Back, // B 
    Launch, // Y 
    GameMenu, // X 
    PrevGame, // LB 
    NextGame, // RB 
    Settings, // Menu (Start) 
    Filters, // View (Back) 
    PageBack, // LT: 10 games back 
    PageForward, // RT: 10 games on 
    CycleFilter, // left stick click: All / Installed / Favourites 
    Home, // right stick click: next collection, or back to the selected game's cover | ControllerInput.cs:10 |
| PadSource | XInput, PlayStation, Steam | MainWindow.Controller.cs:44 |
| PlayStationModel | DualShock4, DualSense | PlayStationPad.cs:12 |
| PopupContext | Global, PerGame | AddonPopupHelper.cs:17 |
| PopupContext | Global, PerGame | ShaderPopupHelper.cs:16 |
| PssCaptureFlags | PSS_CAPTURE_NONE, PSS_CAPTURE_VA_CLONE, PSS_CAPTURE_HANDLES, PSS_CAPTURE_HANDLE_NAME_INFORMATION, PSS_CAPTURE_HANDLE_BASIC_INFORMATION, PSS_CAPTURE_HANDLE_TYPE_SPECIFIC_INFORMATION, PSS_CAPTURE_HANDLE_TRACE, PSS_CAPTURE_THREADS, PSS_CAPTURE_THREAD_CONTEXT, PSS_CREATE_BREAKAWAY_OPTIONAL, PSS_CREATE_USE_VM_ALLOCATIONS | NativeInterop.cs:697 |
| SIGDN | SIGDN_FILESYSPATH | NativeInterop.cs:26 |
| SourceKind | GhRelease, DirectUrl | ShaderPackService.cs:76 |
| SteamControllerModel | Original, Triton | SteamControllerPad.cs:17 |
| Tone | Green, Purple, Amber, Red, Blue, Grey | TabsTheme.cs:70 |
| ViewLayout | Detail, Compact | ViewLayout.cs:3 |

## value vocabularies (string sets, 3-40 items)

| method | count | values | file |
|---|---|---|---|
| Find7Zip | 4 | 7-Zip, 7z.exe, 7-Zip, 7z.exe | AppPaths.cs:32 |
| ApplyRenodxKeyPlaceholders | 20 | Blit_Copy_Hack, DumpUberShaders, ForceBorderless, Force_Pipeline_Cloning, PreventFullscreen, Scaling_Offset, SettingsMode, Swapchain_Encoding, Tonemap_Offset, Upgrade_CopyDestinations, Upgrade_R10G10B10A2_TYPELESS, Upgrade_R10G10B10A2_UNORM, Upgrade_R11G11B10_FLOAT, Upgrade_R16G16B16A16_TYPELESS, Up... | AuxInstallService.Ini.cs:481 |
| ApplyRenodxKeyPlaceholders | 19 | DumpLUTShaders, ForceBorderless, PreventFullscreen, SettingsMode, Upgrade_B10G10R10A2_UNORM, Upgrade_B8G8R8A8_TYPELESS, Upgrade_B8G8R8A8_UNORM, Upgrade_B8G8R8A8_UNORM_SRGB, Upgrade_CopyDestinations, Upgrade_R10G10B10A2_TYPELESS, Upgrade_R10G10B10A2_UNORM, Upgrade_R11G11B10_FLOAT, Upgrade_R16G16B16A1... | AuxInstallService.Ini.cs:493 |
| Uninstall | 3 | reshade.ini, ReShade2.ini, ReShadePreset.ini | AuxInstallService.Install.cs:417 |
| ApplyDialogLook | 11 | CheckBoxCheckBackgroundFillChecked, , 
            , , , , 
            , , , , 
            , , , , 
            , , , , 
            , ,  | CogDialog.cs:599 |
| ShowDlssg2030SettingsAsync | 4 | On, 1, Off, 0 | DetailPanelBuilder.Extras.FrameGen.Dlssg2030Settings.cs:58 |
| ShowDlssg2030SettingsAsync | 4 | DLSS (default), DLSS, XeSS, XeSS | DetailPanelBuilder.Extras.FrameGen.Dlssg2030Settings.cs:65 |
| ShowDlssg2030SettingsAsync | 12 | 2X, 1, 3X, 2, 4X (default), 3, 5X, 4, 6X, 5, Game decides, 0 | DetailPanelBuilder.Extras.FrameGen.Dlssg2030Settings.cs:78 |
| ShowDlssg2030SettingsAsync | 6 | A (default), A, B: HUD recomposition, B, C: FG Lite, C | DetailPanelBuilder.Extras.FrameGen.Dlssg2030Settings.cs:92 |
| ShowDlssg2030SettingsAsync | 8 | Exact, 0, Exact, optimised (default), 1, Fast, 2, Fastest, 3 | DetailPanelBuilder.Extras.FrameGen.Dlssg2030Settings.cs:97 |
| ShowDlssg2030SettingsAsync | 10 | Off (default), Off, M-INT8, M-INT8, L-INT8, L-INT8, M-FP16, M-FP16, L-FP16, L-FP16 | DetailPanelBuilder.Extras.FrameGen.Dlssg2030Settings.cs:102 |
| ShowRtxEncoreSettingsAsync | 3 | game, dlss-fg, xefg | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:84 |
| ShowRtxEncoreSettingsAsync | 3 | Game, DLSS-FG, XeFG | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:84 |
| ShowRtxEncoreSettingsAsync | 3 | game, fixed, dynamic | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:90 |
| ShowRtxEncoreSettingsAsync | 3 | Game decides, Fixed, Dynamic | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:90 |
| ShowRtxEncoreSettingsAsync | 5 | 2x, 3x, 4x, 5x, 6x | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:99 |
| ShowRtxEncoreSettingsAsync | 5 | 2, 3, 4, 5, 6 | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:99 |
| ShowRtxEncoreSettingsAsync | 4 | Game, A, B, C | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:106 |
| ShowRtxEncoreSettingsAsync | 4 | game, a, b, c | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:106 |
| ShowRtxEncoreSettingsAsync | 3 | Unknown, Straight, Premultiplied | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:138 |
| ShowRtxEncoreSettingsAsync | 3 | unknown, straight, premultiplied | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:138 |
| ShowRtxEncoreSettingsAsync | 7 | Game, DLAA, Quality, Balanced, Performance, Ultra Performance, Custom | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:182 |
| ShowRtxEncoreSettingsAsync | 7 | game, dlaa, quality, balanced, performance, ultra-performance, custom | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:183 |
| ShowRtxEncoreSettingsAsync | 12 | Game, Default, A, B, C, D, E, F, J, K, L, M | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:207 |
| ShowRtxEncoreSettingsAsync | 12 | game, default, a, b, c, d, e, f, j, k, l, m | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:208 |
| ShowRtxEncoreSettingsAsync | 8 | Game, Default, A, B, C, D, E, F | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:212 |
| ShowRtxEncoreSettingsAsync | 8 | game, default, a, b, c, d, e, f | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:213 |
| ShowRtxEncoreSettingsAsync | 3 | Game, On, Off | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:216 |
| ShowRtxEncoreSettingsAsync | 3 | game, on, off | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:216 |
| ShowRtxEncoreSettingsAsync | 3 | Game, On, Off | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:219 |
| ShowRtxEncoreSettingsAsync | 3 | game, on, off | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:219 |
| ShowRtxEncoreSettingsAsync | 3 | DirectX 12, DirectX 11, Vulkan | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:229 |
| ShowRtxEncoreSettingsAsync | 3 | d3d12, d3d11, vulkan | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:230 |
| ShowRtxEncoreSettingsAsync | 3 | b, a, auto | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:255 |
| ShowRtxEncoreSettingsAsync | 3 | B, A, Auto | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:255 |
| ShowRtxEncoreSettingsAsync | 4 | Auto, RTX 40, RTX 30, RTX 20 | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:268 |
| ShowRtxEncoreSettingsAsync | 4 | auto, rtx40, rtx30, rtx20 | DetailPanelBuilder.Extras.RtxEncoreSettings.cs:268 |
| BuildOverridesPanel | 4 | Global, Custom, Select, Off | DetailPanelBuilder.Overrides.cs:813 |
| BuildOverridesPanel | 3 | Auto, 32-bit, 64-bit | DetailPanelBuilder.Overrides.cs:993 |
| BuildOverridesPanel | 8 | Auto, DirectX8, DirectX9, DirectX10, DirectX11, DirectX12, Vulkan, OpenGL | DetailPanelBuilder.Overrides.cs:1103 |
| BuildDriverProfileSectionWithData | 3 | Auto (Default), Off, On | DetailPanelBuilder.Overrides.DriverSettings.cs:905 |
| BuildRsChannelSection | 5 | Stable, Nightly, Custom, No Addons, Legacy... | DetailPanelBuilder.Overrides.RsChannel.cs:35 |
| BuildShadersAddonsSection | 3 | Global, Select, Off | DetailPanelBuilder.Overrides.ShadersAddons.cs:57 |
| ShowPatchNotesDialogAsync | 4 | ButtonBackgroundPointerOver, ButtonBackgroundPressed, ButtonBorderBrushPointerOver, ButtonBorderBrushPressed | DialogService.Update.cs:408 |
| ReLimiterExtraInis | 4 | Off, Off, On, On | DisplayModeItems.cs:497 |
| ReLimiterExtraInis | 4 | Off, 0, On, 1 | DisplayModeItems.cs:498 |
| ReLimiterExtraInis | 14 | Off, off, 2x, fixed:1, 3x, fixed:2, 4x, fixed:3, 5x, fixed:4, 6x, fixed:5, Dynamic, dynamic | DisplayModeItems.cs:500 |
| Uninstall | 6 | 0, 1, 2, 3, 4, 5 | Dlssg20_30Service.cs:468 |
| Uninstall | 3 | A, B, C | Dlssg20_30Service.cs:469 |
| Uninstall | 4 | 0, 1, 2, 3 | Dlssg20_30Service.cs:470 |
| Uninstall | 5 | Off, M-INT8, L-INT8, M-FP16, L-FP16 | Dlssg20_30Service.cs:471 |
| RunOnce | 5 | shaders, renodx, framelimiter, misc, luma | DownloadsMigrationService.cs:33 |
| IsValidDeploymentPath | 3 | System32, SysWOW64, WindowsApps | DxvkService.cs:518 |
| FindEaGames | 16 | SOFTWARE\EA Games, SOFTWARE\Wow6432Node\EA Games, SOFTWARE\Electronic Arts, SOFTWARE\Wow6432Node\Electronic Arts, SOFTWARE\Criterion Games, SOFTWARE\Wow6432Node\Criterion Games, SOFTWARE\Respawn, SOFTWARE\Wow6432Node\Respawn, SOFTWARE\BioWare, SOFTWARE\Wow6432Node\BioWare, SOFTWARE\DICE, SOFTWARE\Wo... | GameDetectionService.Platform.cs:99 |
| FindUbisoftGames | 6 | Ubisoft, Ubisoft Game Launcher, games, Ubisoft, Ubisoft Game Launcher, games | GameDetectionService.Platform.cs:364 |
| FindBattleNetGames | 4 | Battle.net, Battle.net, Blizzard Entertainment, Blizzard Entertainment | GameDetectionService.Platform.cs:482 |
| DetectAllGamesDedupedAsync | 8 | Steam, GOG, Epic, EA, Xbox, Ubisoft, Battle.net, Rockstar | GameInitializationService.cs:47 |
| UninstallOptiScaler | 3 | r.FidelityFX.FSR2.UseNativeDX12, r.FidelityFX.FSR3.UseNativeDX12, r.FidelityFX.FSR3.UseRHI | InstallEventHandler.cs:404 |
|  | 4 | relimiter.addon64, relimiter.addon32, ultra_limiter.addon64, ultra_limiter.addon32 | InstallFolderMigrationService.Inventory.cs:128 |
| AddReShadeConfig | 3 | reshade.ini, ReShade2.ini, ReShadePreset.ini | InstallFolderMigrationService.Inventory.cs:327 |
| ScanForInstalledAddon | 5 | Binaries\\Win64, Binaries\\Win32, Binaries\\x86, x64, x86 | MainViewModel.CacheLoad.cs:136 |
| ScanForInstalledAddonQuick | 5 | Binaries\\Win64, Binaries\\Win32, Binaries\\x86, x64, x86 | MainViewModel.CacheLoad.cs:236 |
| ToggleLumaMode | 4 | dxgi.dll, d3d11.dll, Luma*.dll, Luma*.addon* | MainViewModel.Install.Luma.cs:822 |
| EnsureUniversalMfgForPresetAsync | 3 | version.dll, winmm.dll, dinput8.dll | MainViewModel.MfgPlacement.cs:82 |
| CycleMainFilter | 3 | Detected, Installed, Favourites | MainWindow.Controller.cs:644 |
| DcCogButton_Click | 4 | No changes, Borderless fullscreen, Borderless windowed, No exclusive fullscreen | MainWindow.Events.Components.DisplayCommander.cs:91 |
| DcCogButton_Click | 6 | Off, 1 second, 2 seconds, 5 seconds, 10 seconds, Always | MainWindow.Events.Components.DisplayCommander.cs:118 |
| RtxHdrConfigButton_Click | 3 | Gamma 2.0, Gamma 2.2, Gamma 2.4 | MainWindow.Events.Components.Hdr.cs:117 |
| AutoHdrConfigButton_Click | 4 | Low (25), Default (50), High (75), Max (100) | MainWindow.Events.Components.Hdr.cs:413 |
| OsGetUpscalerOptions | 6 | Auto (Default), DLSS, XeSS, FSR 2.1, FSR 2.2, FSR 3.x / FFX | MainWindow.Events.Components.OptiScaler.cs:120 |
| OsGetUpscalerOptions | 8 | Auto (Default), DLSS, FSR 2.1, FSR 2.2, FSR 3.x / FFX, XeSS, FSR2.1 on DX12, FSR3 on DX12 | MainWindow.Events.Components.OptiScaler.cs:121 |
| OsGetUpscalerOptions | 9 | Auto (Default), DLSS, FSR 2.2, FSR 3.1, XeSS (Arc only), XeSS on DX12, FSR2.1 on DX12, FSR2.2 on DX12, FSR3 on DX12 | MainWindow.Events.Components.OptiScaler.cs:122 |
| OsCogButton_Click | 3 | Auto (Default), True, False | MainWindow.Events.Components.OptiScaler.cs:422 |
| OsCogButton_Click | 3 | DX11, DX12, Vulkan | MainWindow.Events.Components.OptiScaler.cs:443 |
| BuildOsFrameGenSection | 7 | Auto (Default), OptiFG (Upscaler), DLSSG via Streamline, DLSSG via Nvngx, FSR 3.1 FG, FSR 3.0 FG, XeFG | MainWindow.Events.Components.OptiScaler.Sections.cs:111 |
| BuildOsFrameGenSection | 4 | Auto (Default), FSR FG, DLSSG, XeFG | MainWindow.Events.Components.OptiScaler.Sections.cs:133 |
| BuildOsFrameGenSection | 5 | None (Real DLSSG), Nukem's, Enabler, FSR 3/4 FG, Combo | MainWindow.Events.Components.OptiScaler.Sections.cs:147 |
| BuildOsFrameGenSection | 3 | Default, On, Off | MainWindow.Events.Components.OptiScaler.Sections.cs:165 |
| BuildOsFrameGenSection | 3 | Auto (0), Force Disable (1), Force Enable (2) | MainWindow.Events.Components.OptiScaler.Sections.cs:180 |
| BuildOsEngineIniSection | 4 | None, FSR2, FSR3, FSR3.1 | MainWindow.Events.Components.OptiScaler.Sections.cs:348 |
| BuildOsEngineIniSection | 3 | r.FidelityFX.FSR2.UseNativeDX12, r.FidelityFX.FSR3.UseNativeDX12, r.FidelityFX.FSR3.UseRHI | MainWindow.Events.Components.OptiScaler.Sections.cs:355 |
| BuildOsNeuralRenderingSection | 3 | Default, On, Off | MainWindow.Events.Components.OptiScaler.Sections.cs:427 |
| BuildOsNeuralRenderingSection | 4 | Default, 1, 2, 3 | MainWindow.Events.Components.OptiScaler.Sections.cs:499 |
| LumaCogButton_Click | 3 | Default, Off, On | MainWindow.Events.cs:186 |
| ClearNvidiaShaderCache_Click | 4 | NVIDIA, DXCache, NVIDIA, GLCache | MainWindow.Events.Settings.cs:1174 |
| BuildFaqContent | 4 | , 
                , , 
                , , 
                , , 
                 | MainWindow.FaqBuilder.cs:100 |
| BuildFaqContent | 7 | , 
                , , 
                , , 
                , , 
                , , 
                , , 
                , , 
                 | MainWindow.FaqBuilder.cs:113 |
| GameProcessSearchRoots | 3 | \steamapps\common\, \Epic Games\, \GOG Games\ | MainWindow.GameProcess.cs:51 |
| EnsureShelf | 5 | ListViewItemBackgroundSelected, , 
                                    , , , , 
                                    , ,  | MainWindow.SpotlightChrome.cs:339 |
| MakeNavItem | 4 | ButtonBackgroundPointerOver, ButtonBackgroundPressed, ButtonBorderBrushPointerOver, ButtonBorderBrushPressed | MainWindow.TabsChrome.cs:323 |
| ShowConfigDialogAsync | 3 | Default, Fixed, Dynamic | MfgDialog.cs:113 |
| Uninstall | 3 | OptiScaler, plugins, plugins | OptiScalerService.Install.cs:976 |
| SeedUserInis | 24 | NVIDIA, Stable, AMD, Stable, AMD, Stable, NVIDIA, Nightly, AMD, Nightly, AMD, Nightly, NVIDIA, DlssNr, AMD, DlssNr, AMD, DlssNr, NVIDIA, F5, AMD, F5, AMD, F5 | OptiScalerService.Install.cs:1636 |
| SuggestName | 4 | .zip, .7z, .rar, .dll | OsCustomBuildService.cs:139 |
|  | 4 | Stable, Nightly, DLSS NR, F5 DLSSNR | OsVariants.cs:20 |
| DeploySfDllsAsync | 4 | nvngx_dlss.dll, nvngx_dlssd.dll, nvngx_dlssg.dll, nvngx_dlssnr.dll | Renodx5AddonService.cs:349 |
| RestoreSfDlls | 4 | nvngx_dlss.dll, nvngx_dlssd.dll, nvngx_dlssg.dll, nvngx_dlssnr.dll | Renodx5AddonService.cs:404 |
| MoveOutOfOptiScalerPlugin | 3 | version.dll, winmm.dll, dinput8.dll | Rtx40MfgService.cs:187 |
| PopulateNvApiCombosFromSnapshot | 3 | Auto (Default), Off, On | SettingsHandler.cs:347 |
| PurgeCachedFiles_Click | 4 | DLSS, DLSS-D, DLSS-G, Streamline | SettingsHandler.DataTools.cs:254 |
|  | 7 | Components, GameOverrides, NeuralRendering, NvidiaProfileDlss, NvidiaProfileDriver, Management, Extras | SettingsViewModel.cs:166 |
| TryDownloadMsStoreIconAsync | 3 | BoxArt, Poster, hero | ShortcutService.cs:201 |
| ShareShaderPaths | 4 | EffectSearchPaths, Shaders, TextureSearchPaths, Textures | SourceEngineSetup.cs:239 |
| TryDownloadAsync | 3 | library_capsule, library_hero, header | SteamArtworkService.cs:218 |
| WrapTile | 6 | ButtonBackgroundPointerOver, ButtonBackgroundPressed, ButtonBackgroundDisabled, ButtonBorderBrushPointerOver, ButtonBorderBrushPressed, ButtonBorderBrushDisabled | TabsTheme.cs:212 |
| MakeSegmented | 4 | ButtonBackgroundPointerOver, ButtonBackgroundPressed, ButtonBorderBrushPointerOver, ButtonBorderBrushPressed | TabsTheme.cs:630 |
|  | 26 | d3d8.dll, d3d9.dll, d3d10.dll, d3d11.dll, d3d12.dll, dxgi.dll, ddraw.dll, dinput.dll, dinput8.dll, dsound.dll, msacm32.dll, msvfw32.dll, version.dll, wininet.dll, winmm.dll, winhttp.dll, xlive.dll, binkw32.dll, bink2w32.dll, vorbisFile.dll, xinput1_1.dll, xinput1_2.dll, xinput1_3.dll, xinput1_4.dll,... | UltimateAsiLoaderService.cs:30 |
|  | 19 | d3d9.dll, d3d10.dll, d3d11.dll, d3d12.dll, dxgi.dll, dinput8.dll, dsound.dll, version.dll, wininet.dll, winmm.dll, winhttp.dll, binkw64.dll, bink2w64.dll, xinput1_1.dll, xinput1_2.dll, xinput1_3.dll, xinput1_4.dll, xinput9_1_0.dll, xinputuap.dll | UltimateAsiLoaderService.cs:41 |
|  | 3 | version.dll, winmm.dll, winhttp.dll | UltimateAsiLoaderService.cs:52 |
|  | 4 | dxgi.dll, d3d11.dll, d3d12.dll, d3d9.dll | UltimateAsiLoaderService.cs:56 |

## loop-generated UI (rows produced by data, not literals)

| file | AddCombo | AddRow | AddSection | bytes |
|---|---|---|---|---|
| DetailPanelBuilder.NeuralRendering.SectionUi.cs | 1 | 0 | 0 | 118355 |
| MainWindow.Events.cs | 2 | 0 | 1 | 61605 |
| DetailPanelBuilder.Extras.Unlocks.cs | 2 | 0 | 1 | 38275 |
| CogDialog.cs | 3 | 2 | 1 | 34987 |
| MainWindow.Events.Components.Rdx.cs | 4 | 2 | 4 | 34969 |
| DetailPanelBuilder.Extras.RtxEncoreSettings.cs | 1 | 0 | 8 | 30024 |
| MainWindow.DisplayModes.cs | 3 | 0 | 3 | 29309 |
| MainWindow.Events.Components.Hdr.cs | 0 | 6 | 2 | 26129 |
| SettingsHandler.DisplayModes.cs | 0 | 1 | 2 | 23904 |
| MainWindow.Events.Components.cs | 2 | 2 | 5 | 20771 |
| MainWindow.Events.Components.FrameLimiters.cs | 1 | 2 | 3 | 14518 |
