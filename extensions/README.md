Extensions
==========

This folder contains all of the code and scripts required to build the
Visual Studio Extensions for SharpGL.

 * `Seeds`: Actual SharpGL projects which are the seeds of the project templates.
 * `SharpGL.2010`: Code for `SharpGL.2010.vsix`.
 * `SharpGL`: Code for `SharpGL.vsix`.
 * `build`: Scripts and code to build the vsix packages.

Building the VS2012, VS2013 and VS2015 Extensions
-------------------------------------------------

To open and build the 2012/2013/2015 extensions, you will need Visual Studio 2015
installed as well as the [Visual Studio 2015 SDK](http://go.microsoft.com/?linkid=9875738).

To build the VS 2012/2013/2015 extensions, run the script below:

```
.\extensions\build\BuildSharpGLExtensions.ps1
```

The VSIX is built to `.\extensions\build\SharpGL.vsix`. This vsix targets the 2012, 
2013 and 2015 editions of Visaul Studio.

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