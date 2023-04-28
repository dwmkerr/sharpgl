# Changelog

## [3.1.2](https://github.com/dwmkerr/sharpgl/compare/v3.1.1...v3.1.2) (2023-04-28)


### Bug Fixes

* annotate version file for release please ([4ef4a48](https://github.com/dwmkerr/sharpgl/commit/4ef4a4852e0944adce90c373e8593c67a0a35848))
* correct release-please type ([9a4fc62](https://github.com/dwmkerr/sharpgl/commit/9a4fc62ab9a03d5bdd946754827a59a276125b63))

#Version 2.4

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
