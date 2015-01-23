Extensions
==========

This folder contains all of the code and scripts required to build the
Visual Studio Extensions for SharpGL.

`Seeds`: Actual SharpGL projects which are the seeds of the project templates.
`SharpGL.2010`: Code for `SharpGL.2010.vsix`.
`SharpGL`: Code for `SharpGL.vsix`.
`build`: Scripts and code to build the vsix packages.

Building the VS2012 & VS2013 Extensions
---------------------------------------

To open and build the 2012/2013 extensions, you will need Visual Studio 2013
installed as well as the [Visual Studio 2013 SDK](http://www.microsoft.com/en-us/download/details.aspx?id=40758).

To build the VS 2012/2013 extensions, run the script below:

```
.\extensions\build\BuildSharpGL2010Extensions.ps1
```

The VSIX is built to `.\extensions\build\SharpGL.vsix`. This vsix targets both
editions of Visaul Studio.

Building the VS2010 Extensions
------------------------------

To open the VS 2010 extensions solution, you will need to install the 
[Visual Studio 2010 SDK](http://www.microsoft.com/en-us/download/details.aspx?id=2680).

Please be aware that the VS 2010 extensions are no longer being maintained.
To build these extensions, run the script below:

```
.\extensions\build\BuildSharpGL2010Extensions.ps1
```

The VSIX is built to `.\extensions\build\SharpGL.2010.vsix`.