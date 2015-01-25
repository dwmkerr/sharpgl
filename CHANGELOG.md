#Version 2.4 (In Progress)

* All imported APIs now use 'SetLastError = true' to aid in analysing issues (thanks [robinsedlaczek](https://github.com/robinsedlaczek).
* Improvments to the robustness of bitmap management in render contexts (thanks [robinsedlaczek](https://github.com/robinsedlaczek).
* Extensions have been moved into their own location in the repository, isolate from the main code.
* Extension projects reference SharpGL via Nuget.
* Fixed issues with `glGetActiveAttrib` and `glGetActiveUniform`.