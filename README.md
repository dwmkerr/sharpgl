SharpGL
=======

[![Build status](https://ci.appveyor.com/api/projects/status/thfa4defh5f4itga?svg=true)](https://ci.appveyor.com/project/dwmkerr/sharpgl)

Unlock the power of OpenGL in any .NET application. SharpGL wraps all modern OpenGL features, provides helpful wrappers for advanced objects like Vertex Buffer Arrays and Shader, as well as offering a powerful Scene Graph and utility library to help you build your projects.

![Example of SharpGL](https://github.com/dwmkerr/sharpgl/blob/master/assets/frontscreen.png?raw=true)

Check out the [Samples](https://github.com/dwmkerr/sharpgl/wiki/Samples), they're a great place to start learning how to use SharpGL.

Contents
--------

1. [Gettings Started](#getting-started)
2. [Building the Code](#building-the-code)
3. [Sample Applications](#sample-applications)
4. [Documentation](https://github.com/dwmkerr/sharpgl/wiki)
5. [Credits, Sponsorship & Thanks](#credits-sponsorship--thanks)

Getting Started
---------------

Installing SharpGL is easy, just use Nuget:

````
PM> Install-Package SharpGL
````

The Core is simply the full set of OpenGL functions and extensions wrapped and available to use.

````
PM> Install-Package SharpGL.WinForms
````

SharpGL for WinForms includes the Core as well as OpenGL controls to drop into your WinForms app.

````
PM> Install-Package SharpGL.WPF
````

SharpGL for WPF includes the Core as well as OpenGL controls to drop into your WPF app.

There are project templates available for SharpGL WinForms and WPF projects - just search for SharpGL on the Visual Studio Extensions gallery, or get the extensions directly:

* [SharpGL for Visual Studio 2010](http://visualstudiogallery.msdn.microsoft.com/ba57efa3-4061-4cdf-97f5-51715c4f120a)
* [SharpGL for Visual Studio 2012/2013](http://visualstudiogallery.msdn.microsoft.com/b61cc443-4790-42b7-b7ab-2691119667d2)

Building the Code
-----------------

To build the code, clone the repo and open the SharpGL, Samples or Tools solution. The Extensions solution is used for the Visual Studio Project Templates and requires additional components - you can find out more on the Wiki on the '[Developing SharpGL](https://github.com/dwmkerr/sharpgl/wiki/Developing-SharpGL)' page.

Sample Applications
-------------------

There are a large number of sample applications that show how to use SharpGL. Check out the 'Samples' solution to see the samples that are available - they'll be documented soon.

Documentation
-------------

All documentation is available on [the Wiki](https://github.com/dwmkerr/sharpgl/wiki).

Credits, Sponsorship & Thanks
-----------------------------

SharpGL is written and maintained by me. Special thanks go to the following contributors:

 * [robinsedlaczek](https://github.com/robinsedlaczek) - Code and documentation updates, tireless patience 
   while I get through a backlog of work!

### NDepend ###

![NDepend](https://github.com/dwmkerr/sharpgl/blob/master/assets/sponsors/ndepend.png?raw=true "NDepend")

SharpGL is proudly sponsored by NDepend! Find out more at [www.NDepend.com](http://www.NDepend.com).

### Red Gate ###

![Red Gate](https://github.com/dwmkerr/sharpgl/blob/master/assets/sponsors/redgate.png?raw=true "Red Gate")

Many thanks to [Red Gate](http://www.red-gate.com/) who have kindly provided SharpGL with a copy of their superb [.NET Developer Bundle](http://www.red-gate.com/products/dotnet-development/dotnet-developer-bundle/)

### JetBrains ###

![JetBrains](https://github.com/dwmkerr/sharpgl/blob/master/assets/sponsors/jetbrains.png?raw=true "JetBrains")

Thanks for [JetBrains](http://www.jetbrains.com/) for sponsoring SharpGL with [Resharper](http://www.jetbrains.com/resharper/)!
