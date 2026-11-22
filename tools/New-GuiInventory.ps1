param(
  [string]$Repo = '',
  [string]$OutDir = ''
)
$ErrorActionPreference = 'Stop'

if (-not $Repo) {
  # Works in both layouts: released repo (tools\.. is the app root) and a dev checkout
  # where the app lives one level above the CLI project (..\..\RHI).
  $root = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
  if (Test-Path (Join-Path $root 'RenoDXCommander')) { $Repo = $root }
  else { $Repo = (Resolve-Path (Join-Path $PSScriptRoot '..\..\RHI')).Path }
}
$app = Join-Path $Repo 'RenoDXCommander'
if (-not (Test-Path -LiteralPath $app)) { throw "App folder not found: $app" }
if (-not $OutDir) { $OutDir = (Resolve-Path (Join-Path $PSScriptRoot '..\docs')).Path }

$XamlNs = 'http://schemas.microsoft.com/winfx/2006/xaml'
$EventNames = @('Click','Checked','Unchecked','SelectionChanged','TextChanged','Toggled','ValueChanged',
  'Loaded','Unloaded','PointerPressed','PointerReleased','PointerEntered','PointerExited','DoubleTapped',
  'RightTapped','ItemClick','Drop','DragOver','DragLeave','DropDownClosed','KeyDown','KeyUp','Expanding',
  'Collapsed','Tapped','Holding','GotFocus','LostFocus','SizeChanged')
$LabelAttrs = @('Text','Header','Content','PlaceholderText','ToolTipService.ToolTip','AutomationProperties.Name')

Write-Host "repo    : $Repo"
Write-Host "app     : $app"

# ---------------------------------------------------------------- XAML pass
$xamlFiles   = Get-ChildItem -LiteralPath $app -Recurse -Filter *.xaml -File
$xamlControls = New-Object System.Collections.Generic.List[object]
$handlerRefs  = New-Object System.Collections.Generic.List[object]

foreach ($file in $xamlFiles) {
  $doc = New-Object System.Xml.XmlDocument
  $doc.Load($file.FullName)
  foreach ($node in $doc.SelectNodes('//*')) {
    $name = $null
    $labels = [ordered]@{}
    $events = [ordered]@{}
    if ($node.Attributes) {
      foreach ($a in $node.Attributes) {
        if ($a.LocalName -eq 'Name' -and $a.NamespaceURI -eq $XamlNs) { $name = $a.Value }
        elseif ($LabelAttrs -contains $a.LocalName) { $labels[$a.LocalName] = $a.Value }
        if ($EventNames -contains $a.LocalName) {
          $events[$a.LocalName] = $a.Value
          $handlerRefs.Add([pscustomobject]@{
            handler = $a.Value; eventName = $a.LocalName; control = $name
            controlType = $node.LocalName; xaml = $file.Name
          })
        }
      }
    }
    if (-not $name -and $events.Count -eq 0) { continue }
    $chain = @()
    $p = $node.ParentNode
    while ($p -and $p.NodeType -eq 'Element') { $chain += $p.LocalName; if ($chain.Count -ge 4) { break }; $p = $p.ParentNode }
    $xamlControls.Add([pscustomobject]@{
      name = $name; type = $node.LocalName; xaml = $file.Name
      parents = ($chain -join ' < '); labels = $labels; events = $events
    })
  }
}
Write-Host ("xaml    : {0} files, {1} named/event nodes, {2} event refs" -f $xamlFiles.Count, $xamlControls.Count, $handlerRefs.Count)

# ------------------------------------------------------- handler resolution
$csFiles = Get-ChildItem -LiteralPath $app -Recurse -Filter *.cs -File
$textCache = @{}
$methodRx = [regex]'(?m)^[ \t]*(?:\[[^\]]*\][ \t]*)*(?:private|public|internal|protected)[ \t]+(?:static[ \t]+)?(?:async[ \t]+)?[\w<>\[\],\.\?]+[ \t]+(\w+)[ \t]*\('

function Get-LineOf([string]$text, [int]$pos) {
  if ($pos -le 0) { return 1 }
  return 1 + ($text.Substring(0, $pos).Split([char]10).Count - 1)
}

$methodsByFile = @{}
foreach ($f in $csFiles) {
  $t = [System.IO.File]::ReadAllText($f.FullName)
  $textCache[$f.FullName] = $t
  $list = @()
  foreach ($m in $methodRx.Matches($t)) { $list += [pscustomobject]@{ name = $m.Groups[1].Value; index = $m.Index } }
  $methodsByFile[$f.FullName] = $list
}

$methodIndex = @{}
foreach ($kv in $methodsByFile.GetEnumerator()) {
  foreach ($m in $kv.Value) {
    if (-not $methodIndex.ContainsKey($m.name)) { $methodIndex[$m.name] = @{ file = $kv.Key; index = $m.index } }
  }
}

$handlers = New-Object System.Collections.Generic.List[object]
$seen = @{}
foreach ($ref in $handlerRefs) {
  $key = $ref.handler + '|' + $ref.xaml + '|' + $ref.eventName
  if ($seen.ContainsKey($key)) { continue }
  $seen[$key] = 1

  $def = $null
  if ($methodIndex.ContainsKey($ref.handler)) { $def = $methodIndex[$ref.handler] }

  $services = @(); $calls = @(); $vm = @(); $fileShort = ''; $line = 0
  if ($def) {
    $t = $textCache[$def.file]
    $line = Get-LineOf $t $def.index
    $body = $t.Substring($def.index, [Math]::Min(4000, $t.Length - $def.index))
    foreach ($m in [regex]::Matches($body, 'GetRequiredService<I(\w+)>')) { $services += $m.Groups[1].Value }
    foreach ($m in [regex]::Matches($body, '\b_?([A-Za-z]\w*Service)\.(\w+)\s*\(')) { $calls += ($m.Groups[1].Value + '.' + $m.Groups[2].Value) }
    foreach ($m in [regex]::Matches($body, '\b(?:ViewModel|_viewModel|_vm)\.(\w+)')) { $vm += $m.Groups[1].Value }
    $fileShort = Split-Path $def.file -Leaf
  }

  $handlers.Add([pscustomobject]@{
    handler     = $ref.handler
    eventName   = $ref.eventName
    control     = $ref.control
    controlType = $ref.controlType
    xaml        = $ref.xaml
    definedIn   = $fileShort
    line        = $line
    resolved    = [bool]$def
    services    = @($services | Sort-Object -Unique)
    calls       = @($calls | Sort-Object -Unique | Select-Object -First 12)
    vmMembers   = @($vm | Sort-Object -Unique | Select-Object -First 12)
  })
}
$unresolved = @($handlers | Where-Object { -not $_.resolved })
Write-Host ("handlers: {0} unique, {1} unresolved" -f $handlers.Count, $unresolved.Count)

# --------------------------------------------------- controls built in code
$ctrlTypes = @('Button','ToggleSwitch','CheckBox','ComboBox','Slider','NumberBox','TextBox','RadioButton',
  'HyperlinkButton','DropDownButton','ToggleButton','MenuFlyoutItem','Expander','PasswordBox','AutoSuggestBox')
$ctrlRx  = [regex]('new\s+(' + ($ctrlTypes -join '|') + ')\b')
$labelRx = [regex]'(?:Content|Header|Text|PlaceholderText|AutomationProperties\.Name)\s*=\s*"([^"]{1,80})"'

$codeControls = New-Object System.Collections.Generic.List[object]
foreach ($f in $csFiles) {
  $t = $textCache[$f.FullName]
  $methods = $methodsByFile[$f.FullName]
  foreach ($m in $ctrlRx.Matches($t)) {
    $slice = $t.Substring($m.Index, [Math]::Min(700, $t.Length - $m.Index))
    $label = ''
    $lm = $labelRx.Match($slice)
    if ($lm.Success) { $label = $lm.Groups[1].Value }
    $owner = ''
    foreach ($mm in $methods) { if ($mm.index -lt $m.Index) { $owner = $mm.name } else { break } }
    $codeControls.Add([pscustomobject]@{
      type = $m.Groups[1].Value; label = $label; method = $owner
      file = (Split-Path $f.FullName -Leaf); line = (Get-LineOf $t $m.Index)
    })
  }
}
Write-Host ("code ui : {0} controls" -f $codeControls.Count)

# ------------------------------------------------------------ settings keys
$svPath = Join-Path $app 'ViewModels\SettingsViewModel.cs'
$svText = [System.IO.File]::ReadAllText($svPath)
$propRx = [regex]'\[ObservableProperty\]\s*private\s+([\w\?<>\.]+)\s+_(\w+)\s*=\s*([^;]+);'
$saveRx = [regex]'s\["([^"]+)"\]\s*=\s*([^;]+);'
$loadRx = [regex]'s\.TryGetValue\("([^"]+)"'

$props = @{}
foreach ($m in $propRx.Matches($svText)) {
  $props[$m.Groups[2].Value] = [pscustomobject]@{ type = $m.Groups[1].Value; default = $m.Groups[3].Value.Trim() }
}
$saves = @{}
foreach ($m in $saveRx.Matches($svText)) { $saves[$m.Groups[1].Value] = $m.Groups[2].Value.Trim() }
$loads = @{}
foreach ($m in $loadRx.Matches($svText)) { $loads[$m.Groups[1].Value] = 1 }

$settings = New-Object System.Collections.Generic.List[object]
foreach ($k in ($saves.Keys | Sort-Object)) {
  $propName = ''
  foreach ($p in $props.Keys) { if ($p -ieq $k) { $propName = $p } }
  if ($propName) { $settings.Add([pscustomobject]@{ key = $k; property = $propName; type = $props[$propName].type; default = $props[$propName].default; persisted = $true; loaded = $loads.ContainsKey($k); saveExpression = $saves[$k] }) }
  else { $settings.Add([pscustomobject]@{ key = $k; property = ''; type = ''; default = ''; persisted = $true; loaded = $loads.ContainsKey($k); saveExpression = $saves[$k] }) }
}
foreach ($p in ($props.Keys | Sort-Object)) {
  $hit = $settings | Where-Object { $_.property -eq $p }
  if (-not $hit) {
    $settings.Add([pscustomobject]@{ key = ''; property = $p; type = $props[$p].type; default = $props[$p].default; persisted = $false; loaded = $false; saveExpression = '' })
  }
}
Write-Host ("settings: {0} entries, {1} persisted keys" -f $settings.Count, $saves.Count)

# ------------------------------------------------------------ ini templates
$iniFiles = @()
$iniFiles += Get-ChildItem -LiteralPath $app -Filter *.ini -File
$conf = Join-Path $app 'dxvk.conf'
if (Test-Path -LiteralPath $conf) { $iniFiles += Get-Item -LiteralPath $conf }

$iniKeys = New-Object System.Collections.Generic.List[object]
foreach ($f in $iniFiles) {
  $section = ''
  foreach ($line in [System.IO.File]::ReadAllLines($f.FullName)) {
    $trim = $line.Trim()
    if (-not $trim) { continue }
    if ($trim.StartsWith(';') -or $trim.StartsWith('#')) { continue }
    if ($trim.StartsWith('[') -and $trim.EndsWith(']')) { $section = $trim.Substring(1, $trim.Length - 2); continue }
    $eq = $trim.IndexOf('=')
    if ($eq -lt 1) { continue }
    $k = $trim.Substring(0, $eq).Trim()
    $v = $trim.Substring($eq + 1).Trim()
    if ($v.Length -gt 60) { $v = $v.Substring(0, 60) + '...' }
    $iniKeys.Add([pscustomobject]@{ file = $f.Name; section = $section; key = $k; value = $v })
  }
}
Write-Host ("ini     : {0} files, {1} keys" -f $iniFiles.Count, $iniKeys.Count)

# ------------------------------------------------------------ vocabularies
$enumRx  = [regex]'(?m)(?:public|internal|private)\s+enum\s+(\w+)\s*(?::\s*\w+\s*)?\{([^}]*)\}'
$setRx   = [regex]'new\s*(?:\[\])?\s*\{([^}]{8,900}?)\}'
$strRx   = [regex]'"([^"]{1,40})"'

$enums = New-Object System.Collections.Generic.List[object]
$stringSets = New-Object System.Collections.Generic.List[object]
$loopGenerated = New-Object System.Collections.Generic.List[object]

foreach ($f in $csFiles) {
  $t = $textCache[$f.FullName]
  $short = Split-Path $f.FullName -Leaf

  foreach ($m in $enumRx.Matches($t)) {
    $vals = @()
    foreach ($part in $m.Groups[2].Value.Split(',')) {
      $v = $part.Trim()
      if (-not $v) { continue }
      $v = ($v -split '=')[0].Trim()
      if ($v) { $vals += $v }
    }
    if ($vals.Count -eq 0) { continue }
    $enums.Add([pscustomobject]@{ name = $m.Groups[1].Value; values = $vals; file = $short; line = (Get-LineOf $t $m.Index) })
  }

  foreach ($m in $setRx.Matches($t)) {
    $vals = @()
    foreach ($sm in $strRx.Matches($m.Groups[1].Value)) { $vals += $sm.Groups[1].Value }
    if ($vals.Count -lt 3 -or $vals.Count -gt 40) { continue }
    $owner = ''
    $methods = $methodsByFile[$f.FullName]
    foreach ($mm in $methods) { if ($mm.index -lt $m.Index) { $owner = $mm.name } else { break } }
    $stringSets.Add([pscustomobject]@{ method = $owner; values = $vals; count = $vals.Count; file = $short; line = (Get-LineOf $t $m.Index) })
  }

  if ($t.Contains('foreach') -and ($t.Contains('AddCombo(') -or $t.Contains('AddRow(') -or $t.Contains('AddCompactGrid('))) {
    $loopGenerated.Add([pscustomobject]@{
      file = $short
      addCombo  = ([regex]::Matches($t, 'AddCombo\(')).Count
      addRow    = ([regex]::Matches($t, 'AddRow\(')).Count
      addSection = ([regex]::Matches($t, 'AddSection\(')).Count
      size = $f.Length
    })
  }
}
Write-Host ("vocab   : {0} enums, {1} string sets, {2} loop-generated ui files" -f $enums.Count, $stringSets.Count, $loopGenerated.Count)

# --------------------------------------------------------------------- emit
$gitHead = ''
try { $gitHead = (& git -C $Repo rev-parse --short HEAD 2>$null) } catch { }
$stamp = (Get-Date).ToString('yyyy-MM-ddTHH:mm:ss')

$result = [pscustomobject]@{
  generated           = $stamp
  sourceRepo          = $Repo
  sourceCommit        = $gitHead
  counts              = [pscustomobject]@{
    xamlFiles          = $xamlFiles.Count
    namedControls      = @($xamlControls | Where-Object { $_.name }).Count
    xamlNodes          = $xamlControls.Count
    eventRefs          = $handlerRefs.Count
    uniqueHandlers     = $handlers.Count
    unresolvedHandlers = $unresolved.Count
    codeBuiltControls  = $codeControls.Count
    settingsEntries    = $settings.Count
    persistedSettings  = $saves.Count
    iniFiles           = $iniFiles.Count
    iniKeys            = $iniKeys.Count
  }
  xamlControls      = $xamlControls
  handlers          = $handlers
  codeBuiltControls = $codeControls
  settings          = $settings
  iniKeys           = $iniKeys
  enums             = $enums
  stringSets        = $stringSets
  loopGeneratedUi   = $loopGenerated
  commands          = @()
}

$jsonPath = Join-Path $OutDir 'gui-inventory.json'
$result | ConvertTo-Json -Depth 8 | Set-Content -LiteralPath $jsonPath -Encoding UTF8

function Esc([string]$s) { if ($null -eq $s) { return '' }; return $s.Replace('|', '\|').Replace("`r", ' ') }

$c = $result.counts
$md = New-Object System.Collections.Generic.List[string]
$md.Add('# GUI inventory')
$md.Add('')
$md.Add("Generated $stamp from ``$Repo`` @ ``$gitHead``.")
$md.Add('')
$md.Add('| area | count |')
$md.Add('|---|---|')
$md.Add("| x:Name controls | $($c.namedControls) |")
$md.Add("| XAML nodes with name or events | $($c.xamlNodes) |")
$md.Add("| XAML event references | $($c.eventRefs) |")
$md.Add("| unique handlers | $($c.uniqueHandlers) |")
$md.Add("| unresolved handlers | $($c.unresolvedHandlers) |")
$md.Add("| controls built in C# | $($c.codeBuiltControls) |")
$md.Add("| settings entries | $($c.settingsEntries) |")
$md.Add("| persisted settings keys | $($c.persistedSettings) |")
$md.Add("| ini keys ($($c.iniFiles) files) | $($c.iniKeys) |")
$md.Add('')
$md.Add('## x:Name controls')
$md.Add('')
$md.Add('| name | type | file | parents | labels | events |')
$md.Add('|---|---|---|---|---|---|')
foreach ($x in ($xamlControls | Where-Object { $_.name } | Sort-Object xaml, name)) {
  $lb = ($x.labels.GetEnumerator() | ForEach-Object { $_.Key + '=' + $_.Value }) -join '; '
  $ev = ($x.events.GetEnumerator() | ForEach-Object { $_.Key + '->' + $_.Value }) -join '; '
  $md.Add("| $($x.name) | $($x.type) | $($x.xaml) | $(Esc $x.parents) | $(Esc $lb) | $(Esc $ev) |")
}
$md.Add('')
$md.Add('## event handlers')
$md.Add('')
$md.Add('| handler | event | control | defined in | services | calls |')
$md.Add('|---|---|---|---|---|---|')
foreach ($h in ($handlers | Sort-Object handler)) {
  $md.Add("| $($h.handler) | $($h.eventName) | $($h.control) | $($h.definedIn):$($h.line) | $(Esc ($h.services -join ', ')) | $(Esc ($h.calls -join ', ')) |")
}
$md.Add('')
$md.Add("## controls built in C# (showing first 500 of $($codeControls.Count))")
$md.Add('')
$md.Add('| type | label | method | file |')
$md.Add('|---|---|---|---|')
$shown = 0
foreach ($k in $codeControls) {
  if ($shown -ge 500) { break }
  $md.Add("| $($k.type) | $(Esc $k.label) | $(Esc $k.method) | $($k.file):$($k.line) |")
  $shown++
}
$md.Add('')
$md.Add('## persisted settings')
$md.Add('')
$md.Add('| key | type | default | loaded |')
$md.Add('|---|---|---|---|')
foreach ($s in ($settings | Where-Object { $_.key } | Sort-Object key)) {
  $md.Add("| $($s.key) | $($s.type) | $(Esc $s.default) | $($s.loaded) |")
}
$md.Add('')
$md.Add('## ini keys')
$md.Add('')
$md.Add('| file | section | key | value |')
$md.Add('|---|---|---|---|')
foreach ($i in ($iniKeys | Sort-Object file, section, key)) {
  $md.Add("| $($i.file) | $($i.section) | $($i.key) | $(Esc $i.value) |")
}

$md.Add('')
$md.Add('## value vocabularies (enum)')
$md.Add('')
$md.Add('| enum | values | file |')
$md.Add('|---|---|---|')
foreach ($e in ($enums | Sort-Object name)) { $md.Add("| $($e.name) | $(Esc ($e.values -join ', ')) | $($e.file):$($e.line) |") }
$md.Add('')
$md.Add('## value vocabularies (string sets, 3-40 items)')
$md.Add('')
$md.Add('| method | count | values | file |')
$md.Add('|---|---|---|---|')
foreach ($s in ($stringSets | Sort-Object file, line)) {
  $v = ($s.values -join ', ')
  if ($v.Length -gt 300) { $v = $v.Substring(0, 300) + '...' }
  $md.Add("| $(Esc $s.method) | $($s.count) | $(Esc $v) | $($s.file):$($s.line) |")
}
$md.Add('')
$md.Add('## loop-generated UI (rows produced by data, not literals)')
$md.Add('')
$md.Add('| file | AddCombo | AddRow | AddSection | bytes |')
$md.Add('|---|---|---|---|---|')
foreach ($l in ($loopGenerated | Sort-Object size -Descending)) { $md.Add("| $($l.file) | $($l.addCombo) | $($l.addRow) | $($l.addSection) | $($l.size) |") }

$mdPath = Join-Path $OutDir 'gui-inventory.md'
$md | Set-Content -LiteralPath $mdPath -Encoding UTF8

Write-Host ''
Write-Host "wrote $jsonPath"
Write-Host "wrote $mdPath"
Write-Host ''
Write-Host "named controls : $($c.namedControls)"
Write-Host "event refs     : $($c.eventRefs)  (unique handlers $($c.uniqueHandlers), unresolved $($c.unresolvedHandlers))"
Write-Host "code-built ui  : $($c.codeBuiltControls)"
Write-Host "settings       : $($c.settingsEntries) ($($c.persistedSettings) persisted)"
Write-Host "ini keys       : $($c.iniKeys)"
Write-Host "enums / sets   : $($enums.Count) / $($stringSets.Count)   loop-built ui files: $($loopGenerated.Count)"

