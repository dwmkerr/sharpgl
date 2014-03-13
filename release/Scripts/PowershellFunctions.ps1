# Copy items to a destination folder, creating the folder if needed.
function CopyItems($source, $destinationFolder) {
    
    # Create the any folders or subfolders up to the destination that don't exist.
    if (!(Test-Path -path $destinationFolder)) {
        New-Item $destinationFolder -Type Directory
    }

    # Now copy the items.
    Copy-Item $source -Destination $destinationFolder
}