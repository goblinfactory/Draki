# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [2.0]

### Busy

- remove all references to SeleniumQA! Should not need to reference that  to bootstrap driver.
- Single package, Draki. Get up and running with just one package.
- start and stop the test website necessary for running the tests.
- migrate sample website from asp.net MVC to ASP.NET Core MVC so that we can run the website and test it using kestrel via dotnet run. 
  - notes here : https://docs.microsoft.com/en-us/aspnet/core/migration/mvc?view=aspnetcore-2.2
- confirm that I.Expect actually throws assert inconclusive and can be used for precondition checks in Nunit. make a note to test with Xunit, don't think the concept exists in xunit?

### changed

- now supporting latest Chrome drivers `ChromeDriver 73.0.3683.68`
- move to dotnetcore
- removed Winforms sendkeys, replaced with Selenium.Sendkeys.
- add an assertInconclusive placeholder test for HTML5 drag and drop showing the issues with ChromeDriver and drag and drop.

### added

### deleted
- removed phantomJs : See https://stackoverflow.com/questions/52442100/selenium-phantomjs-is-invalid-namespace
- removed UploadFile. Simply use `EnterText` and `Click` to upload file instead, see `UploadTests.cs`.

## [1.1.0] - 2019-02-19

### Added

- `QuickExists` : Returns whether a css item exists without the long wait typical of Selenium tests if it does not exist.

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