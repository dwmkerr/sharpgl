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