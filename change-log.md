# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [1.0.1.0] - 2019-02-19

### Added

- `GetTitle` : `I.GetTitle()` returns the page title of the current page.

## [0.2.1.0] - 2018-04-26

### Fixed

- CRITICAL FIX : Upload file `I.Upload(upload, file);` would crash selenium. (was calling recursively) fixed.

### Added

- `UploadTests.UploadFileTest()`

## [0.2.0.0] - 2018-04-24

### Added
- marked `LeftClick()` and `LinkClick()` tests as `[Category(Category.FLAKEY)]` : Have not worked out yet why these tests are failing. Re-running these tests and they pass. It's on the backlog to find out why.
- new test `WindowSwitchToTitleContainingTest()`
- new test `WindowSwitchToNonExistentThrowsTest()` : move the slow part of the test into a seperate test and mark as `VerySlow`.
- new test `SwitchToPopupTest()` : test popup windows.
- two link buttons to `TestApplication/Views/Home/Index.cshtml` to launch pop up windows for testing.

### Changed
- `I.Switch.Window(x)` now switches to windows where url contains `x` or window title contains `x`. Previously it was url endswith, which would fail if your url contained query params.
- added counting elements to `CountTests.cs`.

### Fixed
- fixed bug `No target with given id` that sometimes occurrs when switching windows with alerts and where size of window changes.
- home button on `TestApplication/Views/Shared/Layout.cshtml` did not take you back to home. Useful to have this link working when manually debugging. 
