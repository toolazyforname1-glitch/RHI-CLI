param(
  [string]$Repo = '',
  [string]$Docs = ''
)
$ErrorActionPreference = 'Stop'

if (-not $Repo) {
  # Works in both layouts: released repo (tools\.. is the app root) and a dev checkout
  # where the app lives one level above the CLI project (..\..\RHI).
  $root = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
  if (Test-Path (Join-Path $root 'RenoDXCommander')) { $Repo = $root }
  else { $Repo = (Resolve-Path (Join-Path $PSScriptRoot '..\..\RHI')).Path }
}
if (-not $Docs) { $Docs = (Resolve-Path (Join-Path $PSScriptRoot '..\docs')).Path }

$inventoryPath = Join-Path $Docs 'gui-inventory.json'
if (-not (Test-Path -LiteralPath $inventoryPath)) { throw "run New-GuiInventory.ps1 first - missing $inventoryPath" }
$inventory = Get-Content -LiteralPath $inventoryPath -Raw | ConvertFrom-Json

# Components that currently have a terminal command. Keep in step with Cli\Cli*.cs.
$wiredMap = @{
  ReShade    = 'install reshade'
  RenoDX     = 'install renodx'
  Luma       = 'install luma'
  OptiScaler = 'install optiscaler'
  DXVK       = 'install dxvk'
  REFramework = 'install reframework'
  DofFix     = 'install dof-fix'
  AsiLoader  = 'install asi'
  DisplayCommander = 'toggle displaycommander'
  ReLimiter  = 'toggle relimiter'
  UltraPlus  = 'link ultra-plus'
  UltrawideFix = 'link ultrawide'
}
$knownComponents = @('ReShade', 'RenoDX', 'Luma', 'OptiScaler', 'DXVK', 'REFramework', 'DofFix', 'DisplayCommander', 'ReLimiter', 'UltraPlus', 'UltrawideFix', 'AsiLoader')

# Read the live command list if a built RHI.exe is reachable, so the report can never drift.
$commandCount = 0
$commandNames = @()
$exe = Get-ChildItem -LiteralPath (Join-Path $Repo 'RenoDXCommander\bin') -Recurse -Filter RHI.exe -ErrorAction SilentlyContinue | Select-Object -First 1
if ($exe) {
  $out = Join-Path $env:TEMP 'rhi-commands.json'
  try {
    & $exe.FullName --cli commands --json | Set-Content -LiteralPath $out -Encoding UTF8
    $parsed = Get-Content -LiteralPath $out -Raw | ConvertFrom-Json
    $commandNames = @($parsed.data.commands | ForEach-Object { $_.name })
    $commandCount = $commandNames.Count
  } catch {
    Write-Warning "could not read the command list from $($exe.FullName): $($_.Exception.Message)"
  }
}

$settingsCount = @($inventory.settings | Where-Object { $_.key }).Count
$iniCount = @($inventory.iniKeys).Count
$handlerCount = @($inventory.handlers).Count
$namedControls = $inventory.counts.namedControls

$wiredComponents = @($knownComponents | Where-Object { $wiredMap.ContainsKey($_) })
$missingComponents = @($knownComponents | Where-Object { -not $wiredMap.ContainsKey($_) })

$md = New-Object System.Collections.Generic.List[string]
$md.Add('# Terminal parity report')
$md.Add('')
$md.Add("Generated $(Get-Date -Format 'yyyy-MM-dd HH:mm') from gui-inventory.json and the built RHI.exe.")
$md.Add('')
$md.Add('| surface | total | reachable from the terminal |')
$md.Add('|---|---|---|')
$md.Add("| commands registered | $commandCount | $commandCount |")
$md.Add("| persisted settings | $settingsCount | $settingsCount (setting get/set) |")
$md.Add("| ini keys shipped | $iniCount | any key in a game's ini (ini list/get/set); templates readable |")
$md.Add("| components | $($knownComponents.Count) | $($wiredComponents.Count) ($($wiredComponents -join ', ')) |")
$md.Add("| XAML event handlers | $handlerCount | manual review pending |")
$md.Add("| named XAML controls | $namedControls | manual review pending |")
$md.Add('')
$md.Add('## Components without a command')
$md.Add('')
foreach ($c in $missingComponents) { $md.Add("- $c") }
$md.Add('')
$md.Add('## Commands registered')
$md.Add('')
foreach ($name in ($commandNames | Sort-Object)) { $md.Add("- ``$name``") }

$outPath = Join-Path $Docs 'parity.md'
$md | Set-Content -LiteralPath $outPath -Encoding UTF8

Write-Host "wrote $outPath"
Write-Host "commands: $commandCount  settings: $settingsCount  ini: $iniCount  components wired: $($wiredComponents.Count)/$($knownComponents.Count)"

