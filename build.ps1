# Find which VS version is installed
$VsWherePath = "${env:PROGRAMFILES(X86)}\Microsoft Visual Studio\Installer\vswhere.exe"

Write-Output "VsWherePath is: $VsWherePath"

$VsInstance = $(&$VSWherePath -latest -property displayName)

$idVS2019 = 1

$VsPath = $(&$VsWherePath -latest -property installationPath)

$msbuildPath = Join-Path -Path $VsPath -ChildPath "\MSBuild\Current\Bin\"
Write-Output "Found MSBUILD in: $msbuildPath\msbuild.exe"
Write-Output "Restore $Solution_Name /t:Restore /p:Configuration=$Configuration -m"
&"$msbuildPath\msbuild.exe" "$Solution_Name /t:Restore /p:Configuration=$Configuration -m" | Write-Output
Write-Output "Build $Solution_Name /t:Build /p:Configuration=$Configuration -m"
&"$msbuildPath\msbuild.exe" "$Solution_Name /t:Build /p:Configuration=$Configuration -m" | Write-Output
Write-Output "Build done"
