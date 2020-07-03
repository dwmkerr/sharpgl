# Create the artifacts packages folder.
$artifactsFolder = "$PSScriptRoot/artifacts/packages"

if (Test-Path $artifactsFolder) { Remove-Item $artifactsFolder -Recurse -Force }
New-Item -Path $artifactsFolder -ItemType directory

# Package each of our projects. This must be run *after* ./build.ps1.
dotnet pack --no-restore --no-build "$PSScriptRoot/Core/SharpGL/SharpGL.csproj" -c:Release
dotnet pack --no-restore --no-build "$PSScriptRoot/Core/SharpGL.SceneGraph/SharpGL.SceneGraph.csproj" -c:Release
dotnet pack --no-restore --no-build "$PSScriptRoot/Core/SharpGL.Serialization/SharpGL.Serialization.csproj" -c:Release
dotnet pack --no-restore --no-build "$PSScriptRoot/Core/SharpGL.WinForms/SharpGL.WinForms.csproj" -c:Release
dotnet pack --no-restore --no-build "$PSScriptRoot/Core/SharpGL.WPF/SharpGL.WPF.csproj" -c:Release

# Copy over the packages.
Get-ChildItem "$PSScriptRoot" -Include SharpGL*.nupkg -Recurse | Copy-Item -Destination $artifactsFolder 
