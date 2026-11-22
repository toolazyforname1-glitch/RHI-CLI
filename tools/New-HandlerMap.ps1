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

$commands = @()
$exe = Get-ChildItem -LiteralPath (Join-Path $Repo 'RenoDXCommander\bin') -Recurse -Filter RHI.exe -ErrorAction SilentlyContinue | Select-Object -First 1
if ($exe) {
  try {
    $tmp = Join-Path $env:TEMP 'rhi-handler-map-commands.json'
    Start-Process -FilePath $exe.FullName -ArgumentList '--cli','commands','--json' -NoNewWindow -Wait -RedirectStandardOutput $tmp | Out-Null
    $parsed = Get-Content -LiteralPath $tmp -Raw | ConvertFrom-Json
    $commands = @($parsed.data.commands | ForEach-Object { $_.name } | Where-Object { $_ })
  } catch { Write-Warning "command list unavailable: $($_.Exception.Message)" }
}
$verbs = @('install','uninstall','update','check','list','get','set','show','toggle','template',
  'ui','ini','game','shot','setting','overlay','commands','help','version','doctor','status',
  'button','handler','click','row','item','text','box','panel','card','combo','slider','async','on')

$aliases = @{
  'rs' = 'reshade'; 'reshade' = 'reshade'
  'rdx' = 'renodx'; 'renodx' = 'renodx'
  'luma' = 'luma'
  'dxvk' = 'dxvk'
  'os' = 'optiscaler'; 'optiscaler' = 'optiscaler'
  'ref' = 'reframework'; 'reframework' = 'reframework'
  'dc' = 'displaycommander'; 'displaycommander' = 'displaycommander'
  'ul' = 'relimiter'; 'relimiter' = 'relimiter'
  'asi' = 'asi'; 'asa' = 'asi'
  'screenshot' = 'shot'; 'shots' = 'shot'
}

$commandNouns = @{}
foreach ($c in $commands) {
  $words = ($c -split '[^a-zA-Z]+') | ForEach-Object { $_.ToLower() } | Where-Object { $_ }
  $commandNouns[$c] = @($words | Where-Object { $verbs -notcontains $_ })
}

function Suggest([string]$handler) {
  $clean = $handler -replace '(_Click|_OnClick|_Checked|_Unchecked|_Changed|_Toggled|Async|_Loaded|_Tapped)$',''
  $words = [regex]::Matches($clean, '[A-Z][a-z0-9]*|[a-z0-9]+') | ForEach-Object { $_.Value.ToLower() }

  $nouns = @($words | ForEach-Object {
      if ($aliases.ContainsKey($_)) { $aliases[$_] } else { $_ }
    } | Where-Object { $verbs -notcontains $_ -and $_.Length -gt 1 } | Select-Object -Unique)

  if ($nouns.Count -eq 0) { return '' }

  $best = ''
  $bestScore = 0
  foreach ($c in $commands) {
    $score = @($commandNouns[$c] | Where-Object { $nouns -contains $_ }).Count
    if ($score -gt $bestScore) { $bestScore = $score; $best = $c }
  }
  return $best
}

$rows = @()
foreach ($h in $inventory.handlers) {
  $rows += [pscustomobject]@{
    Handler   = $h.handler
    Event     = $h.eventName
    Control   = $h.control
    Where     = "$($h.definedIn):$($h.line)"
    Services  = ($h.services -join ',')
    Suggestion = (Suggest $h.handler)
  }
}

$byFile = $rows | Group-Object { ($_.Where -split ':')[0] } | Sort-Object Count -Descending

$md = New-Object System.Collections.Generic.List[string]
$md.Add('# XAML handler map')
$md.Add('')
$md.Add("Generated $(Get-Date -Format 'yyyy-MM-dd HH:mm') from gui-inventory.json and the $($commands.Count) registered commands.")
$md.Add('')
$md.Add('Every control in the XAML files maps to one of these handlers. `Suggestion` is a keyword')
$md.Add('heuristic against the command names - it is a starting point for review, not proof of coverage.')
$md.Add('A handler is covered when the action it performs is reachable by a command, or explicitly')
$md.Add('accepted as ui-only (dialog internals, purely visual state).')
$md.Add('')
$md.Add('## Handlers per file')
$md.Add('')
$md.Add('| file | handlers | with a suggestion |')
$md.Add('|---|---|---|')
foreach ($g in $byFile) {
  $suggested = @($g.Group | Where-Object { $_.Suggestion -ne '' }).Count
  $md.Add("| $($g.Name) | $($g.Count) | $suggested |")
}
$md.Add('')
$md.Add('## Handlers')
$md.Add('')
$md.Add('| handler | event | control | where | services | suggestion |')
$md.Add('|---|---|---|---|---|---|')
foreach ($r in ($rows | Sort-Object { ($_.Where -split ':')[0] }, Handler)) {
  $md.Add("| $($r.Handler) | $($r.Event) | $($r.Control) | $($r.Where) | $($r.Services) | $($r.Suggestion) |")
}

$outPath = Join-Path $Docs 'handler-map.md'
$md | Set-Content -LiteralPath $outPath -Encoding UTF8

$covered = @($rows | Where-Object { $_.Suggestion -ne '' }).Count
Write-Host "wrote $outPath"
Write-Host "handlers: $($rows.Count)  with a suggestion: $covered  commands: $($commands.Count)"

