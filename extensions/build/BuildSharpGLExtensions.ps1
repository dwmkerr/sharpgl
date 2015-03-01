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

# Build the extensions solution.
Write-Host "`nBuild the extensions solution..."
$solutionExtensions = Join-Path $extensionsRoot "\SharpGL\Extensions.sln"
. $msbuild $solutionExtensions /p:Configuration=Release /verbosity:quiet /p:VisualStudioVersion=14.0

# Put the built VSIX package in the build root.
Write-Host "`nPut the built extension package (SharpGL.vsix) into the build root..."
CopyItems (Join-Path $extensionsRoot "SharpGL\SharpGL\bin\Release\SharpGL.vsix") $buildPath

# Now use vsix tools to tweak the extensions.
Write-Host "`nFixing vsix file for compatibility with the Visual Studio Gallery..."
Vsix-FixInvalidMultipleFiles -VsixPath (Join-Path $buildPath "SharpGL.vsix") 

# We're done!
Write-Host "Successfully built 'SharpGL.vsix'. Note that the VSIX version has NOT been updated."