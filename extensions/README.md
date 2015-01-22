Extensions
==========

This folder contains all of the code and scripts required to build the
Visual Studio Extensions for SharpGL.

`SharpGL.2010`: Code for `SharpGL.2010.vsix`.
`SharpGL`: Code for `SharpGL.vsix`.
`build`: Scripts and code to build the vsix packages.

Building the VS2012 Extensions
------------------------------

To build the VS 2012 extensions, run the script below:

```
.\extensions\build\BuildSharpGL2010Extensions.ps1
```

The VSIX is built to `.\extensions\build\SharpGL.vsix`.

Building the VS2010 Extensions
------------------------------

Please be aware that the VS 2010 extensions are no longer being maintained.
To build these extensions, run the script below:

```
.\extensions\build\BuildSharpGL2010Extensions.ps1
```

The VSIX is built to `.\extensions\build\SharpGL.2010.vsix`.