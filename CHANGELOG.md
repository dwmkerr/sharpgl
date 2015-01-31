### Version 2.5

* Added support and documentation for all OpenGL 4.3 features.

### Version 2.4

* All imported APIs now use 'SetLastError = true' to aid in analysing issues (thanks [robinsedlaczek](https://github.com/robinsedlaczek).
* Improvments to the robustness of bitmap management in render contexts (thanks [robinsedlaczek](https://github.com/robinsedlaczek).
* Extensions have been moved into their own location in the repository, isolate from the main code.
* Extension projects reference SharpGL via Nuget.
* Fixed issues with `glGetActiveAttrib` and `glGetActiveUniform`.
* Full OpenGL 4.3 support.
* Visual Studio 2013 support for extensions.
* Nuget packages renamed to match standard conventions:
  - SharpGLCore -> SharpGL
  - SharpGLforWinForms -> SharpGL.WinForms
  - SharpGLforWPF -> SharpGL.WPF
* No dynamic calls to OpenGL extension delegates, leading to a big performance gain. 