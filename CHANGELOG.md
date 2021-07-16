# VideoDuplicate Finder

## v2.0.8.9xxx

### Misc

Update build script

### NEW: Add webserver version.

- Pesently, backend implemented, client will be written in Vue

### Console

#### Features

- Added `json` output option to console app

#### Fixes

- Fix folders designated as "Exclude" being erroneously added to "Include" [#8](https://github.com/matthewstrasiotto/videoduplicatefinder/pull/8), @obs1dian [0x90d/videoduplicatefinder#116](https://github.com/0x90d/videoduplicatefinder/pull/116) 

### Output Changes

- Write scanning logs to `stderr` instead of `stdout` 

#### Refactors / Internals

- Extract output string building into separate function from writing to file
### Windows

#### Fixes

- Fix log outputs not displaying [#9](https://github.com/matthewstrasiotto/videoduplicatefinder/pull/9), [0x90d/videoduplicatefinder#17](https://github.com/0x90d/videoduplicatefinder/pull/117) (@obs1dium)

### Linux

#### Fixes

- Fix linux build by upgrading Avalonia dependency. [#10](https://github.com/matthewstrasiotto/videoduplicatefinder/pull/10), [0x90d/videoduplicatefinder#96](https://github.com/0x90d/videoduplicatefinder/pull/96) (@mimran1980)

