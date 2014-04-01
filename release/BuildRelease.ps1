# IMPORTANT: Make sure that the path to msbuild is correct!  
$msbuild = "C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe"
if ((Test-Path $msbuild) -eq $false) {
    Write-Host "Cannot find msbuild at '$msbuild'."
    Break
}

# Load useful functions.
. .\Resources\PowershellFunctions.ps1
. .\Resources\VsixTools.ps1

# Keep track of the 'release' folder location - it's the root of everything else.
# We can also build paths to the key locations we'll use.
$scriptParentPath = Split-Path -parent $MyInvocation.MyCommand.Definition
$folderReleaseRoot = $scriptParentPath
$folderSourceRoot = Split-Path -parent $folderReleaseRoot
$folderSharpGLRoot = Join-Path $folderSourceRoot "source\SharpGL"
$folderNuspecRoot = Join-Path $folderSourceRoot "build\nuspec"

# Part 1 - Build the core libraries.
Write-Host "Preparing to build the core libraries..."
$solutionCoreLibraries = Join-Path $folderSharpGLRoot "SharpGL.sln"
. $msbuild $solutionCoreLibraries /p:Configuration=Release /verbosity:minimal

# Part 2 - Get the version number of the SharpGL core library, use this to build the destination release folder.
$folderBinariesSharpGL = Join-Path $folderSharpGLRoot "Core\SharpGL\bin\Release"
$folderBinariesSharpGLSceneGraph = Join-Path $folderSharpGLRoot "Core\SharpGL.SceneGraph\bin\Release"
$folderBinariesSharpGLSerialization = Join-Path $folderSharpGLRoot "Core\SharpGL.Serialization\bin\Release"
$folderBinariesSharpGLWinForms = Join-Path $folderSharpGLRoot "Core\SharpGL.WinForms\bin\Release"
$folderBinariesSharpGLWPF = Join-Path $folderSharpGLRoot "Core\SharpGL.WPF\bin\Release"
$releaseVersion = [Reflection.Assembly]::LoadFile((Join-Path $folderBinariesSharpGL "SharpGL.dll")).GetName().Version
Write-Host "Built Core Libraries. Release Version: $releaseVersion"

# Part 3 - Copy the core libraries to the release.
$folderRelease = Join-Path $folderReleaseRoot $releaseVersion
$folderReleaseCore = Join-Path $folderRelease "Core"
EnsureEmptyFolderExists($folderReleaseCore)
CopyItems (Join-Path $folderBinariesSharpGL "*.*") (Join-Path $folderReleaseCore "SharpGL")
CopyItems (Join-Path $folderBinariesSharpGLSceneGraph "*.*") (Join-Path $folderReleaseCore "SharpGL.SceneGraph")
CopyItems (Join-Path $folderBinariesSharpGLSerialization "*.*") (Join-Path $folderReleaseCore "SharpGL.Serialization")
CopyItems (Join-Path $folderBinariesSharpGLWinForms "*.*") (Join-Path $folderReleaseCore "SharpGL.WinForms")
CopyItems (Join-Path $folderBinariesSharpGLWPF "*.*") (Join-Path $folderReleaseCore "SharpGL.WPF")

# Part 4 - Build the Samples
Write-Host "Preparing to build the samples..."
$solutionSamples = Join-Path $folderSharpGLRoot "Samples.sln"
. $msbuild $solutionSamples /p:Configuration=Release /verbosity:quiet

# Part 5 - Copy the samples to the release.
$folderReleaseSamples = Join-Path $folderRelease "Samples"
EnsureEmptyFolderExists($folderReleaseSamples)
$releaseFolders = gci (Join-Path $folderSharpGLRoot "Samples") -Recurse -Directory -filter "Release" | select FullName
$releaseFolders | ForEach {
    $releaseFolder = $_.FullName
    $sampleName = (Get-Item (Split-Path -parent (Split-Path -parent $releaseFolder))).Name
    Write-Host "Built Sample: $sampleName"
    CopyItems (Join-Path $releaseFolder "*.*") (Join-Path $folderReleaseSamples "$sampleName")
}
Write-Host "Built samples."

# Part 6 - Build the Tools
Write-Host "Preparing to build the tools..."
$solutionTools = Join-Path $folderSharpGLRoot "Tools.sln"
. $msbuild $solutionTools /p:Configuration=Release /verbosity:quiet

# Part 7 - Copy the tools to the release.
$folderReleaseTools = Join-Path $folderRelease "Tools"
EnsureEmptyFolderExists($folderReleaseTools)
$releaseFolders = gci (Join-Path $folderSharpGLRoot "Tools") -Recurse -Directory -filter "Release" | select FullName
$releaseFolders | ForEach {
    $releaseFolder = $_.FullName
    $toolName = (Get-Item (Split-Path -parent (Split-Path -parent $releaseFolder))).Name
    Write-Host "Built Tool: $toolName"
    CopyItems (Join-Path $releaseFolder "*.*") (Join-Path $folderReleaseTools "$toolName")
}
Write-Host "Built tools."

# Part 8 - Move the core libraries to the dependencies folders of the extensions, then build the extensions.
Write-Host "Preparing to build the extensions..."
$folderExtensionsRoot = Join-Path $folderSourceRoot "source\Extensions"
$dllSharpGL = Join-Path $folderReleaseCore "SharpGL\SharpGL.dll"
$dllSharpGLSceneGraph = Join-Path $folderReleaseCore "SharpGL.SceneGraph\SharpGL.SceneGraph.dll"
$dllSharpGLWPF = Join-Path $folderReleaseCore "SharpGL.WPF\SharpGL.WPF.dll"
$dllSharpGLWinForms = Join-Path $folderReleaseCore "SharpGL.WinForms\SharpGL.WinForms.dll"

Copy-Item $dllSharpGL -Destination (Join-Path $folderExtensionsRoot "WinformsTemplate\Dependencies")
Copy-Item $dllSharpGLSceneGraph -Destination (Join-Path $folderExtensionsRoot "WinformsTemplate\Dependencies")
Copy-Item $dllSharpGLWinForms -Destination (Join-Path $folderExtensionsRoot "WinformsTemplate\Dependencies")

Copy-Item $dllSharpGL -Destination (Join-Path $folderExtensionsRoot "WinformsTemplateProject.2010\Dependencies")
Copy-Item $dllSharpGLSceneGraph -Destination (Join-Path $folderExtensionsRoot "WinformsTemplateProject.2010\Dependencies")
Copy-Item $dllSharpGLWinForms -Destination (Join-Path $folderExtensionsRoot "WinformsTemplateProject.2010\Dependencies")

Copy-Item $dllSharpGL -Destination (Join-Path $folderExtensionsRoot "WinformsTemplateProject\Dependencies")
Copy-Item $dllSharpGLSceneGraph -Destination (Join-Path $folderExtensionsRoot "WinformsTemplateProject\Dependencies")
Copy-Item $dllSharpGLWinForms -Destination (Join-Path $folderExtensionsRoot "WinformsTemplateProject\Dependencies")

Copy-Item $dllSharpGL -Destination (Join-Path $folderExtensionsRoot "WpfTemplate\Dependencies")
Copy-Item $dllSharpGLSceneGraph -Destination (Join-Path $folderExtensionsRoot "WpfTemplate\Dependencies")
Copy-Item $dllSharpGLWPF -Destination (Join-Path $folderExtensionsRoot "WpfTemplate\Dependencies")

Copy-Item $dllSharpGL -Destination (Join-Path $folderExtensionsRoot "WpfTemplateProject.2010\Dependencies")
Copy-Item $dllSharpGLSceneGraph -Destination (Join-Path $folderExtensionsRoot "WpfTemplateProject.2010\Dependencies")
Copy-Item $dllSharpGLWPF -Destination (Join-Path $folderExtensionsRoot "WpfTemplateProject.2010\Dependencies")

Copy-Item $dllSharpGL -Destination (Join-Path $folderExtensionsRoot "WpfTemplateProject\Dependencies")
Copy-Item $dllSharpGLSceneGraph -Destination (Join-Path $folderExtensionsRoot "WpfTemplateProject\Dependencies")
Copy-Item $dllSharpGLWPF -Destination (Join-Path $folderExtensionsRoot "WpfTemplateProject\Dependencies")

$solutionExtensions2010 = Join-Path $folderExtensionsRoot "Extensions.2010.sln"
. $msbuild $solutionExtensions2010 /p:Configuration=Release /verbosity:quiet
$solutionExtensions = Join-Path $folderExtensionsRoot "Extensions.sln"
. $msbuild $solutionExtensions /p:Configuration=Release /verbosity:quiet

# Part 9 - Copy the extensions to the release.
$folderReleaseExtensions = Join-Path $folderRelease "Extensions"
EnsureEmptyFolderExists($folderReleaseExtensions)
CopyItems (Join-Path $folderExtensionsRoot "SharpGL.2010\bin\Release\SharpGL.2010.vsix") $folderReleaseExtensions
CopyItems (Join-Path $folderExtensionsRoot "SharpGL\bin\Release\SharpGL.vsix") $folderReleaseExtensions
Write-Host "Built extensions, updating VSIX files for the Visual Studio Gallery..."

# Now use vsix tools to tweak the extensions.
Vsix-SetVersion -VsixPath (Join-Path $folderReleaseExtensions "SharpGL.2010.vsix") -Version $releaseVersion
Vsix-SetVersion -VsixPath (Join-Path $folderReleaseExtensions "SharpGL.vsix") -Version $releaseVersion
Vsix-FixInvalidMultipleFiles -VsixPath (Join-Path $folderReleaseExtensions "SharpGL.2010.vsix") 
Vsix-FixInvalidMultipleFiles -VsixPath (Join-Path $folderReleaseExtensions "SharpGL.vsix") 

# Part 10 - Build the Nuget Packages
Write-Host "Preparing to build the Nuget Packages..."
$folderReleasePackages = Join-Path $folderRelease "Packages"
EnsureEmptyFolderExists($folderReleasePackages)
$nuget = Join-Path $scriptParentPath "Resources\nuget.exe"

CopyItems (Join-Path $folderReleaseCore "SharpGL.SceneGraph\*.*") (Join-Path $folderNuspecRoot "SharpGLCore\lib\net40")
. $nuget pack (Join-Path $folderNuspecRoot "SharpGLCore\SharpGLCore.nuspec") -Version $releaseVersion -OutputDirectory $folderReleasePackages

CopyItems (Join-Path $folderReleaseCore "SharpGL.WinForms\SharpGL.WinForms.*") (Join-Path $folderNuspecRoot "SharpGLforWinForms\lib\net40")
. $nuget pack (Join-Path $folderNuspecRoot "SharpGLforWinForms\SharpGLforWinForms.nuspec") -Version $releaseVersion -OutputDirectory $folderReleasePackages

CopyItems (Join-Path $folderReleaseCore "SharpGL.WPF\SharpGL.WPF.*") (Join-Path $folderNuspecRoot "SharpGLforWPF\lib\net40")
. $nuget pack (Join-Path $folderNuspecRoot "SharpGLforWPF\SharpGLforWPF.nuspec") -Version $releaseVersion -OutputDirectory $folderReleasePackages

# We're done!
Write-Host "Successfully built version: $releaseVersion"