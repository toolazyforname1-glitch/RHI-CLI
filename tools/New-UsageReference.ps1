param(
  [string]$RhiBat = '',
  [string]$Out = ''
)

# Regenerates the command reference section of USAGE.md from the shipped binary, so the reference
# can never drift from the code. Maintainer tool: point -RhiBat at a built rhi.bat.

$ErrorActionPreference = 'Stop'
if (-not $Out) { $Out = Join-Path (Split-Path $PSScriptRoot -Parent) 'USAGE.md' }
if (-not $RhiBat) { $RhiBat = Join-Path (Split-Path $PSScriptRoot -Parent) 'cli\rhi.bat' }
if (-not (Test-Path -LiteralPath $RhiBat)) { throw "rhi.bat not found: $RhiBat" }

$runner = Join-Path $env:TEMP 'rhi-usage-gen.cmd'
$json = Join-Path $env:TEMP 'rhi-usage-gen.json'
"@echo off`r`ncall `"$RhiBat`" commands --json" | Set-Content -LiteralPath $runner -Encoding ASCII
Start-Process cmd.exe -ArgumentList '/c', $runner -NoNewWindow -Wait -RedirectStandardOutput $json | Out-Null

$commands = (Get-Content -LiteralPath $json -Raw | ConvertFrom-Json).data.commands
if (-not $commands) { throw 'no commands returned' }

$sb = New-Object System.Text.StringBuilder
[void]$sb.AppendLine('## Command reference')
[void]$sb.AppendLine('')
[void]$sb.AppendLine("$($commands.Count) commands, generated from the binary by ``tools\New-UsageReference.ps1``.")
[void]$sb.AppendLine('')
[void]$sb.AppendLine('Every command accepts `--json` (one envelope: `ok`, `command`, `exitCode`, `message`, `data`) and')
[void]$sb.AppendLine('returns exit `0` ok, `1` failed, `2` bad usage. Commands marked **write** change files or')
[void]$sb.AppendLine('settings and require `--yes`; add `--dry-run` to preview without changing anything.')
[void]$sb.AppendLine('')

foreach ($group in ($commands | Group-Object group | Sort-Object Name)) {
  $title = (Get-Culture).TextInfo.ToTitleCase($group.Name)
  [void]$sb.AppendLine("### $title")
  [void]$sb.AppendLine('')

  foreach ($c in ($group.Group | Sort-Object name)) {
    [void]$sb.AppendLine("#### ``rhi $($c.name)``")
    [void]$sb.AppendLine('')
    [void]$sb.AppendLine($c.summary)
    [void]$sb.AppendLine('')
    [void]$sb.AppendLine('```')
    [void]$sb.AppendLine($c.usage)
    [void]$sb.AppendLine('```')
    [void]$sb.AppendLine('')

    if ($c.examples) {
      foreach ($e in $c.examples) {
        [void]$sb.AppendLine('```bat')
        [void]$sb.AppendLine($e)
        [void]$sb.AppendLine('```')
      }
      [void]$sb.AppendLine('')
    }

    $notes = @()
    if ($c.destructive) { $notes += '**write** - needs `--yes`' }
    if ($c.requiresGui) { $notes += 'needs RHI running (runs through the attach pipe)' }
    if ($notes.Count -gt 0) {
      [void]$sb.AppendLine('> ' + ($notes -join ' · '))
      [void]$sb.AppendLine('')
    }
  }
}

$reference = $sb.ToString().TrimEnd()

$beginMarker = '<!-- BEGIN COMMAND REFERENCE -->'
$endMarker   = '<!-- END COMMAND REFERENCE -->'
$block = $beginMarker + "`r`n" + $reference + "`r`n" + $endMarker

if (Test-Path -LiteralPath $Out) {
    # Splice: only the block between the markers is replaced, so the hand-written prose around it
    # survives. Without markers the block is appended instead of overwriting the file.
    $existing = [System.IO.File]::ReadAllText($Out)
    $pattern = '(?s)' + [regex]::Escape($beginMarker) + '.*?' + [regex]::Escape($endMarker)

    $updated = if ([regex]::IsMatch($existing, $pattern)) {
        [regex]::Replace($existing, $pattern, [System.Text.RegularExpressions.MatchEvaluator] { param($m) $block })
    } else {
        $existing.TrimEnd() + "`r`n`r`n" + $block + "`r`n"
    }
} else {
    $updated = $block + "`r`n"
}

Set-Content -LiteralPath $Out -Value $updated -Encoding UTF8
Write-Host "wrote $Out ($($commands.Count) commands, spliced between markers)"
