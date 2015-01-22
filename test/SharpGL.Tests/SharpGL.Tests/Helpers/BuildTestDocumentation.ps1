# IMPORTANT: Make sure that the path to msbuild is correct!  
$msbuild = "C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe"
if ((Test-Path $msbuild) -eq $false) {
    Write-Host "Cannot find msbuild at '$msbuild'."
    Break
}

# Load useful functions.
. .\Resources\PowershellFunctions.ps1
. .\Resources\VsixTools.ps1

# Keep track of the 'release' folder location and the repo root.
$releaseRoot = Split-Path -parent $MyInvocation.MyCommand.Definition
$repoRoot = Split-Path -parent $releaseRoot

# Let the user know what we're doing
Write-Host "Preparing to create Test Documentation..."

# Compile test SharpGL.Tests project.
Write-Host "Building tests..."
$testSolutionPath = Join-Path $repoRoot "/test/SharpGL.Tests/SharpGL.Tests.sln"
. $msbuild $testSolutionPath /p:Configuration=Release /verbosity:quiet

# Get the built assembly path.
$testAssembly = Join-Path $repoRoot "/test/SharpGL.Tests/SharpGL.Tests/bin/Release/SharpGL.Tests.dll"

$assembly = [System.Reflection.Assembly]::LoadFile($testAssembly)
$testTypes = $assembly.GetTypes()

