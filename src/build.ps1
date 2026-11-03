param([string]$Output = (Join-Path $PSScriptRoot 'dist'))
$ErrorActionPreference = 'Stop'
$compiler = Join-Path $env:WINDIR 'Microsoft.NET\Framework\v4.0.30319\csc.exe'
if (!(Test-Path -LiteralPath $compiler)) { throw 'Requires the Windows .NET Framework C# compiler (csc.exe).' }
New-Item -ItemType Directory -Force -Path $Output | Out-Null
$app = Join-Path $Output 'JingFamily.exe'
$iconBuilder = Join-Path $Output 'MakeIcon.exe'
$icon = Join-Path $Output 'JingFamily.ico'
& $compiler /nologo /target:exe /codepage:65001 /reference:System.Drawing.dll "/out:$iconBuilder" (Join-Path $PSScriptRoot 'MakeIcon.cs') (Join-Path $PSScriptRoot 'BrandMark.cs')
if ($LASTEXITCODE -ne 0) { throw 'Icon tool build failed' }
& $iconBuilder $icon
$logo = Join-Path $PSScriptRoot 'JingPu-icon-256.png'
$sources = 'Model.cs','Library.cs','LibraryTests.cs','PurgeConfirmation.cs','PurgeTests.cs','VisualTests.cs','BrandMark.cs','ModernControls.cs','Ui.cs','Diagram.cs','DiagramTests.cs','FamilyTree.cs','Reports.cs','App.cs','Relations.cs','Workbench.cs','WorkshopTests.cs','Templates.cs','TemplateTests.cs','Tests.cs','AssemblyInfo.cs' | ForEach-Object { Join-Path $PSScriptRoot $_ }
& $compiler /nologo /target:winexe /platform:anycpu /optimize+ /codepage:65001 "/win32icon:$icon" "/resource:$logo,JingPuLogo" "/resource:$icon,JingPuIcon" "/out:$app" /reference:System.Windows.Forms.dll /reference:System.Drawing.dll /reference:System.Web.Extensions.dll /reference:System.Xml.Linq.dll /reference:System.IO.Compression.dll /reference:System.IO.Compression.FileSystem.dll $sources
if ($LASTEXITCODE -ne 0) { throw 'Application build failed' }
$setup = Join-Path $Output 'JingFamily-Setup.exe'
& $compiler /nologo /target:winexe /platform:anycpu /optimize+ /codepage:65001 "/win32icon:$icon" "/resource:$logo,JingPuLogo" "/resource:$icon,JingPuIcon" "/out:$setup" /reference:System.Windows.Forms.dll /reference:System.Drawing.dll "/resource:$app,JingFamily.exe" (Join-Path $PSScriptRoot 'Installer.cs') (Join-Path $PSScriptRoot 'ModernControls.cs')
if ($LASTEXITCODE -ne 0) { throw 'Setup build failed' }
Write-Host "Built: $app"
Write-Host "Built: $setup"
