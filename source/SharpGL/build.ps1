# Run msbuild on the solution, in release mode.
# Note that we disable these warnings:
#  - warning CS1591: Missing XML comment for publicly visible type or member
#  - warning CS1573: Parameter 'XXX' has no matching param tag in the XML comment for
# There are thousands of these warnings as lots of the OpenGL functions still
# need XML documentation. We'll still see the warnings in Visal Studio and can
# incrementally fix them, but for the CI build they add too much noise (and slow
# down the build considerably).
$buildCommand ="dotnet msbuild $PSScriptRoot\SharpGL.sln -noWarn:CS1591 -noWarn:CS1573 -t:Rebuild -p:Configuration=Release"

# Run the command.
Write-Host "Running: ""$buildCommand"""
Invoke-Expression $buildCommand
