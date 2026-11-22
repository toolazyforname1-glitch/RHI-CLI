# XAML handler map

Generated 2026-11-11 20:07 from gui-inventory.json and the 42 registered commands.

Every control in the XAML files maps to one of these handlers. `Suggestion` is a keyword
heuristic against the command names - it is a starting point for review, not proof of coverage.
A handler is covered when the action it performs is reachable by a command, or explicitly
accepted as ui-only (dialog internals, purely visual state).

## Handlers per file

| file | handlers | with a suggestion |
|---|---|---|
| MainWindow.Events.Settings.cs | 80 | 12 |
| MainWindow.Events.cs | 25 | 5 |
| MainWindow.Events.Install.cs | 22 | 10 |
| InstallEventHandler.cs | 14 | 7 |
| SettingsHandler.Accounts.cs | 6 | 0 |
| MainWindow.Events.SettingsInputs.cs | 5 | 2 |
| MainWindow.Events.Components.cs | 4 | 2 |
| MassDeployHandler.cs | 4 | 2 |
| SettingsHandler.Hotkeys.cs | 4 | 0 |
| SettingsHandler.DataTools.cs | 3 | 0 |
| MainWindow.UISync.cs | 2 | 0 |
| SettingsHandler.Channels.cs | 2 | 1 |
| MainWindow.Events.Components.DisplayCommander.cs | 1 | 0 |
| MainWindow.Events.Components.FrameLimiters.cs | 1 | 0 |
| MainWindow.Events.Components.Rdx.cs | 1 | 1 |
| MainWindow.xaml.cs | 1 | 0 |

## Handlers

| handler | event | control | where | services | suggestion |
|---|---|---|---|---|---|
| ChooseShadersButton_Click | Click | ChooseShadersItem | InstallEventHandler.cs:491 |  |  |
| InstallButton_Click | Click | DetailRdxInstallBtn | InstallEventHandler.cs:36 |  |  |
| InstallDcButton_Click | Click | DetailDcInstallBtn | InstallEventHandler.cs:183 |  |  |
| InstallLumaButton_Click | Click | DetailLumaInstallBtn | InstallEventHandler.cs:464 |  | install luma |
| InstallRefButton_Click | Click | DetailRefInstallBtn | InstallEventHandler.cs:443 |  | install reframework |
| InstallRsButton_Click | Click | DetailRsInstallBtn | InstallEventHandler.cs:136 |  | install reshade |
| InstallUlButton_Click | Click | DetailUlInstallBtn | InstallEventHandler.cs:162 |  |  |
| LumaToggle_Click | Click | DetailLumaToggle | InstallEventHandler.cs:514 |  | install luma |
| UninstallButton_Click | Click | DetailRdxDeleteBtn | InstallEventHandler.cs:130 |  |  |
| UninstallDcButton_Click | Click | DetailDcDeleteBtn | InstallEventHandler.cs:198 |  |  |
| UninstallLumaButton_Click | Click | DetailLumaDeleteBtn | InstallEventHandler.cs:485 |  | install luma |
| UninstallRefButton_Click | Click | DetailRefDeleteBtn | InstallEventHandler.cs:458 |  | install reframework |
| UninstallRsButton_Click | Click | DetailRsDeleteBtn | InstallEventHandler.cs:151 |  | install reshade |
| UninstallUlButton_Click | Click | DetailUlDeleteBtn | InstallEventHandler.cs:177 |  |  |
| RefCogButton_Click | Click | DetailRefCogBtn | MainWindow.Events.Components.cs:284 |  | install reframework |
| RsCogButton_Click | Click | DetailRsIniBtn | MainWindow.Events.Components.cs:22 |  | install reshade |
| SupportGuide_Click | Click |  | MainWindow.Events.Components.cs:354 |  |  |
| SupportKofi_Click | Click |  | MainWindow.Events.Components.cs:360 |  |  |
| DcCogButton_Click | Click | DetailDcIniBtn | MainWindow.Events.Components.DisplayCommander.cs:24 |  |  |
| UlCogButton_Click | Click | DetailUlIniBtn | MainWindow.Events.Components.FrameLimiters.cs:39 |  |  |
| RdxCogButton_Click | Click | DetailUeExtendedBtn | MainWindow.Events.Components.Rdx.cs:19 |  | install renodx |
| AddGameButton_Click | Click | AddGameBtn | MainWindow.Events.cs:1178 |  |  |
| BrowseAddonWatchFolder_Click | Click |  | MainWindow.Events.cs:131 |  | shot folder |
| CheckForUpdatesButton_Click | Click | CheckForUpdatesBtn | MainWindow.Events.cs:61 |  |  |
| DetailScrollViewer_SizeChanged | SizeChanged | DetailScrollViewer | MainWindow.Events.cs:970 |  |  |
| DonateButton_Click | Click | DonateBtn | MainWindow.Events.cs:287 |  |  |
| FavouriteButton_Click | Click | DetailFavBtn | MainWindow.Events.cs:1272 |  |  |
| Filter_Click | Click | FilterDetected | MainWindow.Events.cs:1265 |  |  |
| FullRefreshButton_Click | Click | FullRefreshBtn | MainWindow.Events.cs:25 |  |  |
| GameRow_RightTapped | RightTapped |  | MainWindow.Events.cs:657 |  |  |
| InfoButton_Click | Click | DetailRefInfoBtn | MainWindow.Events.cs:787 |  |  |
| LinkLumaWiki_Click | Click |  | MainWindow.Events.cs:261 |  | install luma |
| LinkReLimiterGithub_Click | Click |  | MainWindow.Events.cs:273 |  |  |
| LinkRenoDxWiki_Click | Click |  | MainWindow.Events.cs:255 |  |  |
| LinkRhiGithub_Click | Click |  | MainWindow.Events.cs:267 |  |  |
| LumaCogButton_Click | Click | DetailLumaIniBtn | MainWindow.Events.cs:158 |  | install luma |
| NexusModsLink_Click | Click | DetailNexusModsBtn | MainWindow.Events.cs:926 |  |  |
| PcgwLink_Click | Click | DetailPcgwBtn | MainWindow.Events.cs:889 |  |  |
| RefreshButton_Click | Click | RefreshBtn | MainWindow.Events.cs:19 |  |  |
| ResetAddonWatchFolder_Click | Click |  | MainWindow.Events.cs:148 |  | shot folder |
| ReShadeAddonsButton_Click | Click | ReShadeAddonsItem | MainWindow.Events.cs:1298 |  |  |
| SaveFilterButton_Click | Click | SaveFilterButton | MainWindow.Events.cs:1003 |  |  |
| SearchBox_TextChanged | TextChanged | SearchBox | MainWindow.Events.cs:988 |  |  |
| SupportDiscord_Click | Click |  | MainWindow.Events.cs:247 |  |  |
| UltraPlusLink_Click | Click | DetailUltraPlusBtn | MainWindow.Events.cs:954 |  |  |
| UwFixLink_Click | Click | DetailUwFixBtn | MainWindow.Events.cs:940 |  | install dof-fix |
| DetailDcStatus_PointerPressed | PointerPressed | DetailDcStatus | MainWindow.Events.Install.cs:233 |  |  |
| DetailDofFixStatus_PointerPressed | PointerPressed | DetailDofFixStatus | MainWindow.Events.Install.cs:409 |  | install dof-fix |
| DetailLumaStatus_PointerPressed | PointerPressed | DetailLumaStatus | MainWindow.Events.Install.cs:250 |  | install luma |
| DetailRdxStatus_PointerPressed | PointerPressed | DetailRdxStatus | MainWindow.Events.Install.cs:456 |  | install renodx |
| DetailRefStatus_PointerPressed | PointerPressed | DetailRefStatus | MainWindow.Events.Install.cs:443 |  | install reframework |
| DetailRsStatus_PointerPressed | PointerPressed | DetailRsStatus | MainWindow.Events.Install.cs:429 |  | install reshade |
| DetailUlStatus_PointerPressed | PointerPressed | DetailUlStatus | MainWindow.Events.Install.cs:214 |  |  |
| DofFixCogButton_Click | Click | DetailDofFixCogBtn | MainWindow.Events.Install.cs:395 |  | install dof-fix |
| DofFixInfoButton_Click | Click | DetailDofFixInfoBtn | MainWindow.Events.Install.cs:357 |  | install dof-fix |
| EngineBadge_PointerEntered | PointerEntered | DetailEngineBadge | MainWindow.Events.Install.cs:602 |  |  |
| EngineBadge_PointerExited | PointerExited | DetailEngineBadge | MainWindow.Events.Install.cs:611 |  |  |
| EngineBadge_PointerPressed | PointerPressed | DetailEngineBadge | MainWindow.Events.Install.cs:510 |  |  |
| GameList_DoubleTapped | DoubleTapped | GameList | MainWindow.Events.Install.cs:646 |  |  |
| HideButton_Click | Click | DetailHideBtn | MainWindow.Events.Install.cs:626 |  |  |
| InstallDofFixButton_Click | Click | DetailDofFixInstallBtn | MainWindow.Events.Install.cs:291 |  | install dof-fix |
| LaunchGame_Click | Click | DetailLaunchBtn | MainWindow.Events.Install.cs:632 |  |  |
| LinkText_PointerEntered | PointerEntered | DetailRefStatus | MainWindow.Events.Install.cs:490 |  |  |
| LinkText_PointerExited | PointerExited | DetailRefStatus | MainWindow.Events.Install.cs:499 |  |  |
| OpenAppData_Click | Click | DetailAppDataBtn | MainWindow.Events.Install.cs:941 |  |  |
| OpenFolder_Click | Click | DetailFolderBtn | MainWindow.Events.Install.cs:933 |  | shot folder |
| UninstallDofFixButton_Click | Click | DetailDofFixDeleteBtn | MainWindow.Events.Install.cs:337 |  | install dof-fix |
| UpdateAllButton_Click | Click | UpdateBtn | MainWindow.Events.Install.cs:19 |  |  |
| AboutBack_Click | Click |  | MainWindow.Events.Settings.cs:2247 |  |  |
| AboutButton_Click | Click |  | MainWindow.Events.Settings.cs:2234 |  |  |
| AdminModeCombo_SelectionChanged | SelectionChanged | AdminModeCombo | MainWindow.Events.Settings.cs:109 |  |  |
| ApplyOsHotkey_Click | Click | ApplyOsHotkeyBtn | MainWindow.Events.Settings.cs:178 | DlssStreamlineService | install optiscaler |
| ApplyPeakNitsToAll_Click | Click | ApplyScreenshotPathBtn | MainWindow.Events.Settings.cs:127 | DlssStreamlineService |  |
| ApplyScreenshotPath_Click | Click | ApplyHotkeyBtn | MainWindow.Events.Settings.cs:124 |  |  |
| AutoUpdateComponentsCombo_SelectionChanged | SelectionChanged | AutoUpdateComponentsCombo | MainWindow.Events.Settings.cs:1291 |  |  |
| AutoUpdateDlssCombo_SelectionChanged | SelectionChanged | AutoUpdateDlssCombo | MainWindow.Events.Settings.cs:1277 |  |  |
| AutoUpdateStreamlineCombo_SelectionChanged | SelectionChanged | AutoUpdateStreamlineCombo | MainWindow.Events.Settings.cs:1284 |  |  |
| BackgroundUpdateChecksCombo_SelectionChanged | SelectionChanged | BackgroundUpdateChecksCombo | MainWindow.Events.Settings.cs:1298 |  |  |
| BrowseScreenshotPath_Click | Click |  | MainWindow.Events.Settings.cs:2064 |  |  |
| CacheAllShadersCombo_SelectionChanged | SelectionChanged | CacheAllShadersCombo | MainWindow.Events.Settings.cs:175 | DlssStreamlineService |  |
| ClearNvidiaShaderCache_Click | Click |  | MainWindow.Events.Settings.cs:1153 |  |  |
| CloseToTrayCombo_SelectionChanged | SelectionChanged | CloseToTrayCombo | MainWindow.Events.Settings.cs:1394 |  |  |
| ColorApplyBtn_Click | Click | ColorApplyBtn | MainWindow.Events.Settings.cs:1550 |  |  |
| ColorDisplayCombo_SelectionChanged | SelectionChanged | ColorDisplayCombo | MainWindow.Events.Settings.cs:1514 |  |  |
| CreateMissingNvidiaProfiles_Click | Click |  | MainWindow.Events.Settings.cs:645 |  |  |
| CustomShadersCombo_SelectionChanged | SelectionChanged | CustomShadersCombo | MainWindow.Events.Settings.cs:121 |  |  |
| DigitalVibrance_Click | Click |  | MainWindow.Events.Settings.cs:856 |  |  |
| DldsrApplyBtn_Click | Click | DldsrApplyBtn | MainWindow.Events.Settings.cs:1801 | DldsrService |  |
| DldsrCaptureBtn_Click | Click | DldsrCaptureBtn | MainWindow.Events.Settings.cs:1884 | DldsrService |  |
| DldsrDeleteBtn_Click | Click | DldsrDeleteBtn | MainWindow.Events.Settings.cs:1962 | DldsrService |  |
| DldsrInfoBtn_Click | Click | DldsrInfoBtn | MainWindow.Events.Settings.cs:1690 |  |  |
| DldsrRefreshBtn_Click | Click | DldsrRefreshBtn | MainWindow.Events.Settings.cs:2000 | DldsrService |  |
| DldsrSmoothnessApplyBtn_Click | Click | DldsrSmoothnessApplyBtn | MainWindow.Events.Settings.cs:2006 | DldsrService |  |
| DlssDefaults_Click | Click |  | MainWindow.Events.Settings.cs:227 | DlssStreamlineService |  |
| DlssIndicatorCombo_SelectionChanged | SelectionChanged | DlssIndicatorCombo | MainWindow.Events.Settings.cs:1221 |  |  |
| DmfgFrameCountCombo_SelectionChanged | SelectionChanged | DmfgFrameCountCombo | MainWindow.Events.Settings.cs:500 |  |  |
| DmfgTargetFpsCombo_SelectionChanged | SelectionChanged | DmfgTargetFpsCombo | MainWindow.Events.Settings.cs:513 |  |  |
| DropHelperCombo_SelectionChanged | SelectionChanged | DropHelperCombo | MainWindow.Events.Settings.cs:1386 |  |  |
| ExportNvidiaProfiles_Click | Click |  | MainWindow.Events.Settings.cs:707 |  |  |
| FaqBack_Click | Click |  | MainWindow.Events.Settings.cs:2427 |  |  |
| FaqButton_Click | Click | FaqBtn | MainWindow.Events.Settings.cs:2421 |  |  |
| FpsLimitCombo_SelectionChanged | SelectionChanged | FpsLimitCombo | MainWindow.Events.Settings.cs:392 |  |  |
| GlobalPowerModeCombo_SelectionChanged | SelectionChanged | GlobalPowerModeCombo | MainWindow.Events.Settings.cs:632 |  |  |
| GlobalReBarEnableCombo_SelectionChanged | SelectionChanged | GlobalReBarEnableCombo | MainWindow.Events.Settings.cs:576 |  |  |
| GlobalReBarSizeCombo_SelectionChanged | SelectionChanged | GlobalReBarSizeCombo | MainWindow.Events.Settings.cs:596 |  |  |
| GlobalUpdateInclusion_Click | Click | GlobalUpdateInclusionBtn | MainWindow.Events.Settings.cs:163 | DlssStreamlineService |  |
| GlobalVSyncCombo_SelectionChanged | SelectionChanged | GlobalVSyncCombo | MainWindow.Events.Settings.cs:614 |  |  |
| GSyncEnableCombo_SelectionChanged | SelectionChanged | GSyncEnableCombo | MainWindow.Events.Settings.cs:379 |  |  |
| GSyncIndicatorCombo_SelectionChanged | SelectionChanged | GSyncIndicatorCombo | MainWindow.Events.Settings.cs:1268 |  |  |
| GSyncModeCombo_SelectionChanged | SelectionChanged | GSyncModeCombo | MainWindow.Events.Settings.cs:366 |  |  |
| HdrAutoToggleCombo_SelectionChanged | SelectionChanged | HdrAutoToggleCombo | MainWindow.Events.Settings.cs:1305 |  |  |
| HdrModsListBtn_Click | Click | HdrModsListBtn | MainWindow.Events.Settings.cs:2434 |  |  |
| HdrSelectMonitors_Click | Click |  | MainWindow.Events.Settings.cs:1312 |  |  |
| HdrToggle_Click | Click | DetailHdrToggleBtn | MainWindow.Events.Settings.cs:1470 |  |  |
| ImportNvidiaProfiles_Click | Click |  | MainWindow.Events.Settings.cs:763 |  |  |
| MassDlssDeploy_Click | Click |  | MainWindow.Events.Settings.cs:205 | DlssStreamlineService |  |
| MassPresetInstall_Click | Click |  | MainWindow.Events.Settings.cs:193 | DlssStreamlineService |  |
| MotdButton_Click | Click |  | MainWindow.Events.Settings.cs:62 | UpdateLogService |  |
| NewModsButton_Click | Click | NewModsBtn | MainWindow.Events.Settings.cs:2250 |  |  |
| OpenAppDataFolder_Click | Click | OpenAppDataBtn | MainWindow.Events.Settings.cs:112 |  | shot folder |
| OpenCustomFolder_Click | Click | OpenCustomBtn | MainWindow.Events.Settings.cs:115 |  | shot folder |
| OpenDownloadsFolder_Click | Click | OpenDownloadsBtn | MainWindow.Events.Settings.cs:118 |  | shot folder |
| OpenLogsFolder_Click | Click | OpenLogsBtn | MainWindow.Events.Settings.cs:97 |  | shot folder |
| OpenScreenshotFolder_Click | Click |  | MainWindow.Events.Settings.cs:2082 |  | shot folder |
| OsDlssInputsCombo_SelectionChanged | SelectionChanged | OsDlssInputsCombo | MainWindow.Events.Settings.cs:160 | DlssStreamlineService | install optiscaler |
| OsGpuCombo_SelectionChanged | SelectionChanged | OsGpuCombo | MainWindow.Events.Settings.cs:157 | DlssStreamlineService | install optiscaler |
| OsHotkeyCombo_SelectionChanged | SelectionChanged | OsHotkeyCombo | MainWindow.Events.Settings.cs:154 | DlssStreamlineService | install optiscaler |
| PatchNotesLink_Click | Click |  | MainWindow.Events.Settings.cs:50 | UpdateLogService |  |
| PeakNitsAuto_Click | Click | PeakNitsAutoBtn | MainWindow.Events.Settings.cs:2104 |  |  |
| PeakNitsBox_KeyDown | KeyDown | PeakNitsBox | MainWindow.Events.Settings.cs:2137 |  |  |
| PeakNitsCog_Click | Click |  | MainWindow.Events.Settings.cs:2150 |  |  |
| PerGameScreenshotCombo_SelectionChanged | SelectionChanged | PerGameScreenshotCombo | MainWindow.Events.Settings.cs:1371 |  |  |
| PreferredRefreshRateCombo_SelectionChanged | SelectionChanged | PreferredRefreshRateCombo | MainWindow.Events.Settings.cs:487 |  |  |
| RecentGamesCombo_SelectionChanged | SelectionChanged | RecentGamesCombo | MainWindow.Events.Settings.cs:1420 |  |  |
| ResAutoToggleCombo_SelectionChanged | SelectionChanged | ResAutoToggleCombo | MainWindow.Events.Settings.cs:1505 |  |  |
| ResetAllNvidiaProfiles_Click | Click |  | MainWindow.Events.Settings.cs:1037 |  |  |
| ResolutionTargetCombo_SelectionChanged | SelectionChanged | ResolutionTargetCombo | MainWindow.Events.Settings.cs:1646 |  |  |
| ResSelectMonitors_Click | Click |  | MainWindow.Events.Settings.cs:1588 |  |  |
| ResToggle_Click | Click | DetailResToggleBtn | MainWindow.Events.Settings.cs:1656 |  |  |
| RsVariableListUseTabsCombo_SelectionChanged | SelectionChanged | RsVariableListUseTabsCombo | MainWindow.Events.Settings.cs:1378 |  | install reshade |
| SettingsBack_Click | Click | SettingsBackBtn | MainWindow.Events.Settings.cs:2231 |  | ui settings |
| SettingsButton_Click | Click | SettingsBtn | MainWindow.Events.Settings.cs:19 | UpdateLogService | ui settings |
| ShaderCacheSizeCombo_SelectionChanged | SelectionChanged | ShaderCacheSizeCombo | MainWindow.Events.Settings.cs:340 |  |  |
| ShaderPrecompileCombo_SelectionChanged | SelectionChanged | ShaderPrecompileCombo | MainWindow.Events.Settings.cs:353 |  |  |
| StartWithWindowsCombo_SelectionChanged | SelectionChanged | StartWithWindowsCombo | MainWindow.Events.Settings.cs:1434 |  |  |
| StatusBarVersion_Click | Click | StatusBarVersionText | MainWindow.Events.Settings.cs:22 | UpdateLogService |  |
| UlTargetFpsCombo_SelectionChanged | SelectionChanged | UlTargetFpsCombo | MainWindow.Events.Settings.cs:151 | DlssStreamlineService |  |
| UpdateLogButton_Click | Click | UpdateLogBtn | MainWindow.Events.Settings.cs:74 | UpdateLogService |  |
| AddonWatchFolderBox_KeyDown | KeyDown | AddonWatchFolderBox | MainWindow.Events.SettingsInputs.cs:42 |  | shot folder |
| AddonWatchFolderBox_LostFocus | LostFocus | AddonWatchFolderBox | MainWindow.Events.SettingsInputs.cs:56 |  | shot folder |
| PeakNitsBox_LostFocus | LostFocus | PeakNitsBox | MainWindow.Events.SettingsInputs.cs:128 |  |  |
| ScreenshotPathBox_KeyDown | KeyDown | ScreenshotPathBox | MainWindow.Events.SettingsInputs.cs:14 |  |  |
| ScreenshotPathBox_LostFocus | LostFocus | ScreenshotPathBox | MainWindow.Events.SettingsInputs.cs:21 |  |  |
| Grid_DragOver | DragOver |  | MainWindow.UISync.cs:320 |  |  |
| Grid_Drop | Drop |  | MainWindow.UISync.cs:323 |  |  |
| GameList_SelectionChanged | SelectionChanged | GameList | MainWindow.xaml.cs:696 |  |  |
| MassDeployDcIni_Click | Click |  | MassDeployHandler.cs:117 |  |  |
| MassDeployOsIni_Click | Click |  | MassDeployHandler.cs:158 |  | install optiscaler |
| MassDeployRsIni_Click | Click |  | MassDeployHandler.cs:25 |  | install reshade |
| MassDeployUlIni_Click | Click |  | MassDeployHandler.cs:76 |  |  |
| GitHubDisconnectBtn_Click | Click |  | SettingsHandler.Accounts.cs:246 |  |  |
| GitHubPatClearBtn_Click | Click | GitHubPatClearBtn | SettingsHandler.Accounts.cs:362 |  |  |
| GitHubPatSaveBtn_Click | Click | GitHubPatSaveBtn | SettingsHandler.Accounts.cs:264 |  |  |
| NexusConnectBtn_Click | Click | NexusConnectBtn | SettingsHandler.Accounts.cs:51 |  |  |
| NexusDisconnectBtn_Click | Click |  | SettingsHandler.Accounts.cs:125 |  |  |
| NxmRegisterBtn_Click | Click | NxmRegisterBtn | SettingsHandler.Accounts.cs:135 |  |  |
| DxvkVariantCombo_SelectionChanged | SelectionChanged | DxvkVariantCombo | SettingsHandler.Channels.cs:18 |  | install dxvk |
| ReShadeChannelCombo_SelectionChanged | SelectionChanged | ReShadeChannelCombo | SettingsHandler.Channels.cs:101 |  |  |
| CopyLogsArchive_Click | Click | CopyLogsArchiveBtn | SettingsHandler.DataTools.cs:177 |  |  |
| ExportGameData_Click | Click | ExportGameDataBtn | SettingsHandler.DataTools.cs:14 | GameNameService,PcgwService |  |
| PurgeCachedFiles_Click | Click | PurgeCacheBtn | SettingsHandler.DataTools.cs:230 |  |  |
| ApplyUlOsdHotkey_Click | Click | ApplyUlHotkeyBtn | SettingsHandler.Hotkeys.cs:199 |  |  |
| ResetScreenshotHotkey_Click | Click |  | SettingsHandler.Hotkeys.cs:141 |  |  |
| UlDlssHooksCombo_SelectionChanged | SelectionChanged | UlDlssHooksCombo | SettingsHandler.Hotkeys.cs:293 |  |  |
| UlSharedPresetsCombo_SelectionChanged | SelectionChanged | UlSharedPresetsCombo | SettingsHandler.Hotkeys.cs:284 |  |  |
