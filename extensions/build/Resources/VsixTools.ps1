[Reflection.Assembly]::LoadWithPartialName( "System.IO.Compression.FileSystem" ) | Out-Null

# Unzips a zip file at $path to the folder $destination.
function Unzip($path, $destination)
{
    [System.IO.Compression.ZipFile]::ExtractToDirectory($path, $destination) | Out-Null
}

# Given a path such as 'c:\test.vsix' this function 
# extracts the contents to c:\test.
function ExtractVsixToWorkingFolder($vsixPath) {
    
    # Create the destination directory.
    $extractFolderName = [System.Io.Path]::GetFileNameWithoutExtension($vsixPath)
    $extractFolderPath = (Join-Path (Split-Path $vsixPath) $extractFolderName)

    # Throw if it already exists.
    if(Test-Path $extractFolderPath) {
        throw "Cannot extract the vsix to folder '$extractFolderPath' as it already exists and might cause data loss."
    }

    # Extract the zip to the folder.
    Unzip $vsixPath $extractFolderPath

    # Return the extract folder path, which is essentially our working directory.
    return $extractFolderPath
}

# Given a path to a vsix, overwrites it with the contents of the s
# associated working folder.
function ZipWorkingFolderToVsix($workingFolder, $vsixPath) {

    # Delete the vsix (as we will overwrite it).
    Copy-Item $vsixPath -Destination ($vsixPath + ".backup")
    Remove-Item $vsixPath -Force

    # Note we don't use the .NET method below - for some reason the package
    # seems to not have the templates extracted.
    # Zip the working folder up and save it at the vsix path
    #[System.IO.Compression.ZipFile]::CreateFromDirectory($workingFolder, $vsixPath)

    # Remove the working folder.
    #Remove-Item $workingFolder -Force -Recurse

    $vsixZip = [System.IO.Path]::ChangeExtension($vsixPath, "zip")
    if(!(test-path($vsixZip)))
    {
        set-content $vsixZip ("PK" + [char]5 + [char]6 + ("$([char]0)" * 18))
        (dir $vsixZip).IsReadOnly = $false    
    }
    $shellApplication = new-object -com shell.application
    $zipPackage = $shellApplication.NameSpace($vsixZip)
    $items = Get-ChildItem $workingFolder
    foreach($file in $items) 
    { 
        $zipPackage.CopyHere($file.FullName)
        do {
            Start-sleep 2
        } until ( $zipPackage.Items() | select {$_.Name -eq $file.Name} )
    }
    Move-Item $vsixZip -Destination $vsixPath -Force
    Remove-Item $workingFolder -Force -Recurse
}

# Gets the vsix manifest version. Could be:
# 1: Visual Studio 2010
# 2: Visual Studio 2012 onwards
function GetManifestVersion($manifestXml) {

    # Version 1 if we have a Vsix node with Version attribute = 1.
    if($manifestXml.DocumentElement.Name -eq "Vsix" -and $manifestXml.Vsix.Version -eq "1.0.0") {
        return 1;
    }

    # Version 2 if we have a Package manifest node with Version attribute = 2.
    if($manifestXml.DocumentElement.Name -eq "PackageManifest" -and $manifestXml.PackageManifest.Version -eq "2.0.0") {
        return 2;
    }
    
    throw "Unable to determine the version of the Vsix manifest."
}

function GetManifestNamespaceManager($manifestXml) {
    $ns = New-Object System.Xml.XmlNamespaceManager($manifestXml.NameTable)
    $ns.AddNamespace("ns", $manifestXml.DocumentElement.NamespaceURI)
    return ,$ns
}

# Sets the version of the vsix.
# Version should be a string in the format "a.b" "a.b.c" or "a.b.c.d"
function Vsix-SetVersion {
    param(
       [Parameter(Mandatory=$true)]
       [string]$VsixPath,
       [Parameter(Mandatory=$true)]
       [string]$Version
    )
    
    # First, create the working directory.
    $workingFolder = ExtractVsixToWorkingFolder $VsixPath

    # Now load the manifest.
    $manifestPath = Join-Path $workingFolder "extension.vsixmanifest"
    $manifestXml = New-Object XML
    $manifestXml.Load($manifestPath)

    # Set the package version. The xml structure depends on the manifest version.
    $manifestVersion = GetManifestVersion($manifestXml)
    if($manifestVersion -eq 1) {
        $manifestXml.Vsix.Identifier.Version = $Version
    } elseif($manifestVersion -eq 2) {
        $manifestXml.PackageManifest.Metadata.Identity.Version = $Version
    } else {
        throw "Unsupported manifest version"
    }

    # Save the manifest.
    $manifestXml.save($manifestPath)
    
    # Finally, save the updated working folder as the vsix.
    ZipWorkingFolderToVsix $workingFolder $vsixPath
}

function Vsix-FixInvalidMultipleFiles {
    param(
       [Parameter(Mandatory=$true)]
       [string]$VsixPath
    )

    # Folder names need to be more than one letter and have different starting letters and numbers.
    $folderNames = @("Alpha1","Bravo","Charlie","Delta","Echo","Foxtrot","Golf","Hotel","India","Juliet","Kilo","Lima","Mike","November","Oscar","Papa","Quebec","Romeo","Sierra","Tango","Uniform","Victor","Whiskey","Xray","Yankee","Zulu")
    # $folderNames = @("F1","F2","F3","F4")

    # The gist is this. Find every zip file in Project Templates, e.g:
    # ProjectTemplates\CSharp\1033\PlumsProject.zip
    # ProjectTemplates\CSharp\1033\ApplesProject.zip
    # Then put *each one* into a uniquely named folder by replacing
    # 'project templates' in the path with a new unique id
    # A\CSharp\1033\PlumsProject.zip
    # B\CSharp\1033\ApplesProject.zip
    # don't use numbers, as the Visual Studio Gallery site fails
    # if you use 1, 11, 111 etc.

    # First, create the working directory.
    $workingFolder = ExtractVsixToWorkingFolder $VsixPath

    # Get the zip paths. Also create an array that will store the new project template folders.
    $projectTemplateFolders = @()
    $folderNameIndex = 0
    Get-ChildItem -Path (Join-Path $workingFolder '.\ProjectTemplates') -Filter *.zip -Recurse | ForEach-Object {
        $from = $_.FullName
        $newPath = $from.Replace('\ProjectTemplates\', '\' + $folderNames[$folderNameIndex] + '\')
        $projectTemplateFolders += $folderNames[$folderNameIndex]
        $folderNameIndex++

        # Copy the file from the old location to the new one, creating a directory chain as necessary.
        New-Item -ItemType File -Path $newPath -Force
        Copy-Item $from $newPath -Force
    } | Out-Null

    # Delete the project templates folder.
    Remove-Item (Join-Path $workingFolder '.\ProjectTemplates') -Force -Recurse
    
    # Now load the manifest.
    $manifestPath = Join-Path $workingFolder "extension.vsixmanifest"
    $manifestXml = New-Object XML
    $manifestXml.Load($manifestPath)

    # Get the manifest version - this will determine what nodes we need to change to match the
    # new folder structure.
    $manifestVersion = GetManifestVersion($manifestXml)
    $ns = GetManifestNamespaceManager($manifestXml)
    if($manifestVersion -eq 1) {
        
        # Manifest v1:
        # Remove all Vsix/Content/ProjectTemplate nodes and replace with A/B/C etc, e.g.:
        # <Content>
        #   <ProjectTemplate>ProjectTemplates</ProjectTemplate>
        # </Content>
        # to 
        # <Content>
        #   <ProjectTemplate>A</ProjectTemplate>
        #   <ProjectTemplate>B</ProjectTemplate>
        #   <ProjectTemplate>C</ProjectTemplate>
        # </Content>
        $contentNode = $manifestXml.Vsix.SelectSingleNode("ns:Content", $ns)
        $projectTemplateNode = $manifestXml.Vsix.Content.SelectSingleNode("ns:ProjectTemplate", $ns)
        $manifestXml.Vsix.Content.RemoveChild($projectTemplateNode) | Out-Null

        foreach ($projectTemplateFolder in $projectTemplateFolders) {
            $newnode = $projectTemplateNode.CloneNode($true)
            $newnode.InnerText = $projectTemplateFolder
            $contentNode.AppendChild($newnode) | Out-Null
        }    
    } elseif($manifestVersion -eq 2) {
        
        # Manifest v2:
        # Remove all PackageManifest/Assets/Asset (project templat) nodes and replace with A/B/C etc, e.g.:
        # <Assets>
        #   <Asset Type="Microsoft.VisualStudio.ProjectTemplate" Path="ProjectTemplates" />
        # </Assets>
        # to 
        # <Assets>
        #   <Asset Type="Microsoft.VisualStudio.ProjectTemplate" Path="A" />
        #   <Asset Type="Microsoft.VisualStudio.ProjectTemplate" Path="B" />
        #   <Asset Type="Microsoft.VisualStudio.ProjectTemplate" Path="C" />
        # </Assets>

        # Get all the existing project template nodes.
        $assetsNode = $manifestXml.PackageManifest.SelectSingleNode("ns:Assets", $ns)
        $projectTemplateNodes = $manifestXml.PackageManifest.Assets.SelectNodes("ns:Asset[@Type='Microsoft.VisualStudio.ProjectTemplate']", $ns)
        foreach($projectTemplateNode in $projectTemplateNodes) {
            $manifestXml.PackageManifest.Assets.RemoveChild($projectTemplateNode)
        }
        foreach ($projectTemplateFolder in $projectTemplateFolders) {
            $newnode = $projectTemplateNodes[0].CloneNode($true)
            $newnode.Path = $projectTemplateFolder
            $assetsNode.AppendChild($newnode) | Out-Null
        }
    } else {
        throw "Unsupported manifest version"
    }

    # Save the manifest.
    $manifestXml.save($manifestPath)


    # Finally, save the updated working folder as the vsix.
    ZipWorkingFolderToVsix $workingFolder $vsixPath
}

function Vsix-GetManifestVersion {
    param(
       [Parameter(Mandatory=$true)]
       [string]$VsixPath
    )
    
    # First, create the working directory.
    $workingFolder = ExtractVsixToWorkingFolder $VsixPath
    
    # Now load the manifest.
    $manifestPath = Join-Path $workingFolder "extension.vsixmanifest"

    # Get the manifest version.
    $manifestXml = New-Object XML
    $manifestXml.Load($manifestPath)
    $manifestVersion = GetManifestVersion($manifestXml)

    # Finally, clean up the working folder.
    Remove-Item $workingFolder -Force -Recurse
    return $manifestVersion
}