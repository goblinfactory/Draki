# Draki - smoke testing

### Simple and rapid critical path smoke testing for web apps

*Make it so easy to write a functional smoke test for your web application that there's no excuse not to!*

*Smoke testing library with a deliberately simple API that does not allow you to test functionality that you should only be testing by using a javascript testing framework. (e.g. complex mouse movements)*

*This is the test framework that every manager should insist their dev team use. Rapidly get broad (critical paths) tested and baked into your CICD, and don't get bogged down in detail, leave that to the Angular devs and to Jasmin [and other test tools](https://medium.com/powtoon-engineering/a-complete-guide-to-testing-javascript-in-2017-a217b4cd5a2a).*

### Sample code

```csharp

// busy moving some sample code across, will push that branch shortly. 
// Getting started notes and basic principles will go here.

```

##### Building the solution and running the unit tests

1. run `update-chrome-driver.ps1` to download and unpack (unzip) the latest chrome driver.
1. build the solution
1. right click on the test project `FluentAutomation.TestApplication` and select `View -> in browser`. This will start iisexpress.
1. Run or debug any unit test you want to.

##### how is this different from FluentAutomation?

* It's a subset of FluentAutomation with some updated drivers and simplified tests so that it can be more easily kept up to date with Selenium changes.
* Mostly what this boiled down to is removing all the API calls that worked with the mouse X and Y position or used relative X and Y physical pixel offsets anywhere. These are the parts that result in brittle tests that break if a div changes size or position by a few pixels.
* I'm hoping FluentAutomation will become more actively developed again and I can eventually replace my reliance on this project with FluentAutomation again, however I urgently need the testing features for a project that relies on the FluentAutomation syntax, and FluentAutomation is currently broken and not actively being developed. (last commit > 2 years ago.)
* result is much smaller code base
* The added benefit of the reduced API is a longer 'shelf life' between each package release before any update of Chrome or Selenium causes one of the API's on the test library to fail and require a new nuget release. 
* The aim is to be able to automate the testing of new Chrome packages, and immediately (automatically) bring out a new nuget package if an update to Selenium passes all tests, without human intervention.