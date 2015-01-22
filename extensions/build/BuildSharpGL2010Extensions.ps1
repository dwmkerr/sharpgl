# IMPORTANT: Make sure that the path to msbuild is correct!
$ErrorActionPreference = "Stop"
$msbuild = "C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe"
if ((Test-Path $msbuild) -eq $false) {
    Write-Host "Cannot find msbuild at '$msbuild'."
    Break
}

# Keep track of important locations.
$buildPath = Split-Path -parent $MyInvocation.MyCommand.Definition
$extensionsRoot = Split-Path -parent $buildPath

# Load useful functions.
. "$buildPath\Resources\PowershellFunctions.ps1"
. "$buildPath\Resources\VsixTools.ps1"

# Build the VS2010 extensions solution.
Write-Host "Building extensions..."
$solutionExtensions2010 = Join-Path $extensionsRoot "\SharpGL.2010\Extensions.2010.sln"
. $msbuild $solutionExtensions2010 /p:Configuration=Release /verbosity:quiet

# Put the built VSIX package in the build root.
CopyItems (Join-Path $extensionsRoot "SharpGL.2010\SharpGL.2010\bin\Release\SharpGL.2010.vsix") $buildPath

# Now use vsix tools to tweak the extensions.
Write-Host "Cleaning up vsix file..."
Vsix-FixInvalidMultipleFiles -VsixPath (Join-Path $buildPath "SharpGL.2010.vsix") 

# We're done!
Write-Host "Successfully built 'SharpGL.2010.vsix'. Note that the VSIX version has NOT been updated."