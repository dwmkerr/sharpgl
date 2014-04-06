# Copy items to a destination folder, creating the folder if needed.
function CopyItems($source, $destinationFolder) {
    
    # Create the any folders or subfolders up to the destination that don't exist.
    EnsureFolderExists($destinationFolder)

    # Now copy the items.
    Copy-Item $source -Destination $destinationFolder
}

# Ensures that a folder exists.
function EnsureFolderExists($folder) {

    # Create the any folders or subfolders up to the destination that don't exist.
    if (!(Test-Path -path $folder)) {
        New-Item $folder -Type Directory | Out-Null
    }
}

# Ensures that a folder exists and deletes anything in it.
function EnsureEmptyFolderExists($folder) {
    EnsureFolderExists($folder)
    Remove-Item -Recurse -Force $folder
    EnsureFolderExists($folder)
}

# Creates a temporary directory, returning the path.
function CreateTemporaryDirectory {   
    
    $folderPath = (Join-Path $env:temp ([System.Guid]::NewGuid().ToString()))
    [Void](New-Item -Type Directory $folderPath)
    return $folderPath
}

# Sets the version of a dependency in a nuspec.
# e.g:
# SetNuspecDependencyVersion "SharpGLforWinForms.nuspec" "SharpGLCore" "2.3.0.1"
function SetNuspecDependencyVersion($nuspecPath, $dependencyId, $version) {

    $nuspecXml = New-Object XML
    $nuspecXml.Load($nuspecPath)

    # Loop through the dependencies, looking for the one with the specfied id.
    foreach($dependency in $nuspecXml.package.metadata.dependencies.ChildNodes) {
        if($dependency.id -eq $dependencyId) {
            $dependency.version = $version
        }
    }

    $nuspecXml.Save($nuspecPath)
}

# Creates a Nuget package from a spec and a set of items. The items will be
# copied into the ,p
function CreateNugetPackage($nuget, $nuspecPath, $version, $dependencyVersions, $libNet4Items, $outputPath) {

    # Create a temporary directory, set the temp spec path.
    $tempFolder = CreateTemporaryDirectory
    $tempNuspecPath = Join-Path $tempFolder (Split-Path $nuspecPath -leaf)

    # Copy the source items into the lib/net4 folder.
    Copy-Item $nuspecPath -Destination $tempNuspecPath
    CopyItems $libNet4Items (Join-Path $tempFolder "lib/net40")

    # Set the dependency versions.
    foreach ($dependencyVersion in $dependencyVersions.GetEnumerator()) {
        SetNuspecDependencyVersion $tempNuspecPath $dependencyVersion.Name $dependencyVersion.Value
    }

    # Create the package.
    . $nuget pack $tempNuspecPath -Version $version -OutputDirectory $outputPath

    # Clean up the temporary directory.
    Remove-Item $tempFolder -Force -Recurse
}