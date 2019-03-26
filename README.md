# Draki - Fluent web automation for C#

[![Join the chat at https://gitter.im/Draki-web-testing/Lobby](https://badges.gitter.im/Draki-web-testing/Lobby.svg)](https://gitter.im/Draki-web-testing/Lobby?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

### write robust web automation scripts in C#. Use for requirement verification, smoke tests, acceptance tests, you name it, Draki does it, fluently, quickly, easily.

*Make it so easy to write a functional smoke test for your web application that there's no excuse not to!*

*Smoke testing library with a deliberately simple API that does not allow you to test functionality that you should only be testing by using a javascript testing framework. (e.g. complex mouse movements)*

*This is the test framework that every manager should insist their dev team use. Rapidly get broad (critical paths) tested and baked into your CICD, and don't get bogged down in detail, leave that to the Angular devs and to Jasmin [and other test tools](https://medium.com/powtoon-engineering/a-complete-guide-to-testing-javascript-in-2017-a217b4cd5a2a).*

## Getting started

#### 1) have a discussion with client and collect some written requirements  of the most critical workflows of your system you need to urgently test

Get this down in writing in any written form that allows you to include the requirements as part of your source code. 
Example below is one common style of writing requirements. It is just a plain markdown text file with our requirments written loosly in [Gherkin syntax](http://docs.behat.org/en/v2.5/guides/1.gherkin.html). You can use plain english, it really does not matter.

> :: `outstanding-balance.requirement.md`

Scenario outline : customer with outstanding balance cannot close their account

> Given a customer with an `@outstandingBalance` has just logged in with valid credentials<br/>
> then the customer should be on the 'my-account' page<br/>
> and the close account button should be styled with the `@appropriate` style. <br/>
> When the customers tries to close his account by clicking the close account button<br/>
> Then the customer `@shouldSeeWarning` message his account cannot be closed <br/>
> And the customer `@shouldSeeReason` why the account cannot be closed<br/>

Examples:

outstanding balance | appropriate style | should see warning | should see reason
--- | --- | --- | ---
100 | close-denied | yes | yes
0 | close-ok | no | no



#### 2) install draki test package in your test project (you need to run both commands below)

```powershell
 > install package draki.core
 > install-package draki.seleniumwebdriver
```

#### 3) Initialising the test engine

```csharp

using Draki;
using NUnit.Framework;

[SetUpFixture]
public class RunOnceBeforeAllTests : FluentTest
{
    [OneTimeSetUp]
    public void Setup(){
		FluentSession.EnableStickySession();
        FluentConfig.WaitUntilTimeout(TimeSpan.FromMilliseconds(1000));
        SeleniumWebDriver.Bootstrap(Browser.Chrome);
    }
}
```

#### 4) Write some test classes

```csharp

public class RootPage {
    public cost string Url = "http://localhost:12345";
}

public class LoginPage {
    
    public const string Login = RootPage.Url + "/Login";
    public const string UserNameSelector = "#name-control";
    public const string UserPasswordSelector = "#password-control";
    public const string LoginButtonSelector = "#login-button";
}

public class MyTestBase : FluentTest
{
    public void GivenIamLoggedIn(TestUser user){
        I.Open(Pages.Login);
        I.Expect.Text("Welcome to the login page").In("h1.page_title");
        I.Enter(user.Name).In(LoginPage.UserNameSelector);
        I.Enter(user.Password).In(LoginPage.UserPasswordSelector);
        I.Click(LoginPage.LoginButtonSelector);

        // for our demo purposes we are pretending all pages have a hidden loginstatus `div`
        // also using I.Expect in the inside lambda, so that if the login fails, 
        // and test that has a precondition for the user to be logged in will not fail, instead
        // will Assert.Inconclusive() which is what we want. 

        I.WaitUntil(()=> I.Expect.Text("logged-in").In("div.loginstatus"), TimeSpan.FromSeconds(3));
    }
}
```

#### 5) Write some tests that test the system as described step by step in your requirements

If you have any questions, please contact me on twitter [@drakiSelenium](https://twitter.com/drakiSelenium)

```csharp

public class AccountTests : MyTestBase
{
    [Test]
    [TestCase(100.00M, "close-denied", true, true)]
    [TestCase(0M, "close-ok", false, false)]
    public void customer_with_outstanding_balance_cannot_close_their_account(decimal outstandingbalance, string appropriateStyle, bool shouldSeeWarning, bool shouldSeeReason)
    {
        // Given a customer with an `@outstandingBalance` has just logged in with valid credentials
        var user = TestData.LoadTemporaryTestUser(outstandingBalance);
        this.GivenIAmLoggedIn(user);

        // then the customer should be on the 'my-account' page
        I.Assert.Text("MY ACCOUNT").In(AccountsPage.AccountHeadingSelector);

        // and the close account button should be styled with the `@appropriate` style.
         I.Assert.Class(appropriateStyle).On(AccountsPage.CloseAccountButton);
        
        // When the customers tries to close his account by clicking the close account button
        I.Click(AccountsPage.CloseAccountButton);

        I.WaitUntil(()=> {

            // Then the customer `@shouldSeeWarning` message his account cannot be closed
            I.Assert.Css("visibility",shouldSeeWarning ? "visible" : "hidden").On(AccountsPage.WarningMessage);

            // And the customer `@shouldSeeReason` why the account cannot be closed
            I.Assert.Css("visibility",shouldSeeReason ? "visible" : "hidden").On(AccountsPage.ReasonMessage);

        }, TimeSpan.FromSeconds(10))
    }
}
```

#### 6) If you want you can read the temporary docs

[temporary API documentation for Draki (FluentAutomation)](/Docs/)

#### WOOT! your're done early! Now go home early and fire up the barbi!

some good housekeeping below...

#### Test code notes

1. In the demo above I've highlighted a common difficulty with automated testing, how do you repeatedly run a test against a live system without having to constantly reload your entire repository, e.g. your sql database?
1. The solution is not to hard code specific test users, but rather create new random test users specifically for each test, otherwise there is a risk that some badly written test can interfere with this test, e.g. `add balance, and close account` test would kill this test if the db changes were not rolled back.
1. If your `TestData` helper class can access an in memory test database proxy, that's also an excellent testing parttern.
1. Running tests against uniquely created users means you don't have to worry about parallel tests interfering with each other.
1. It's ok to open a page that may have data that's changing as a result of other tests, as long as you consider this and what you're actually testing, the 'xpath selectors' your're executing are working on DOM elements that you know will be consistent.

#### 7) Bake in a good habit of always checking your preconditions using `I.Expect`

Remember to always use positive confirmation to check your precondiions using the `I.Expect` fluid syntax

- any precoditions that fail will throw an `Assert.Inconclusive` and not a `Fail`, so that you can quickly hone in on the actual code changes to your system that has caused the tests to fail, instead of suddenly seeing hundreds of tests fail.

#### 8) Now that you're familiar with the basics it's time to kick this party up a notch!

Update : **alpha draft convertion to dotnet core does not currently support multiple browsers. After the latest port to dotnet core is stable will finish the tests and migration for multibrowser.**
Run your tests simultaneously in both Firefox and Chrome and IE and record screenshots of any errors along the way:

```csharp
// Multi-provider sample coming soon...
```

### How is Draki different to simply using Selenium directly?

- busy with the docs for this now.

### Building the solution and running the unit tests

1. build the solution
1. right click on the test project `FluentAutomation.TestApplication` and select `View -> in browser`. This will start IIS Express. (I am busy migrating the demo site to ASP.NET core MVC and will switch to `dotnet run` shortly.)
1. Run all the unit tests in `Draki.Tests`. The tests do not run in headless mode, makes it easier to stop and debug when extending the test website.

### Known issues

- See https://github.com/goblinfactory/Draki/issues for list of known issues. I will update this readme with the most critical issues that may impact your ability to use the project. (of course ymmv)
- Currently only Chrome driver has tests.
- Multibrowser, i.e. ability to run parallel tests, e.g. five chrome, and three firefox, all in the same test is disabled, some features not implemented or migrated to dotnet core yet. Busy with this.
- using Draki from a .NET core project will mean you must stop chromedriver with a OneTimeTeardown. This is not needed with .NET Framework projects. (busy investigating. My Guess is that it could be a TinyIOC lifetime issue.)

### Using Draki in any sensitive project

1. We strongly recommend you fork the project and build and create your own package due the (currently unavoidable) requirement to embed the actual web driver executables (chromedriver.exe, IEDriverServer32.exe and IEDriverServer64.exe.) See `_update-chromedriver.ps1`
2. make sure that you are able to code review and regularly merge in updates to your fork. i.e. recommendation is not to refactor the project. Submit requests for improvements as pull requests to the open source project and merge them back in to your fork.
3. When creating your own package, download the chromedriver.exe yourself, verify it's hashcode and scan for malware before adding it to your internal package server. Sign if necessary.
4. Make sure you are able to have a 'fixed' (static) version of chrome on your build agents in the event that a chrome update comes out and impacts you before the open source project can be updated with a patch.

### How is this different to just using SeleniumDriver directly in C#?

- TBD 

### how is this different from FluentAutomation?

* Draki is a derived clone of [FluentAutomation](https://github.com/stirno/FluentAutomation) with some updated drivers and simplified tests so that it can be more easily kept up to date with Selenium changes.
* Mostly what this boiled down to is removing all the API calls that worked with the mouse X and Y position or used relative X and Y physical pixel offsets anywhere. These are the parts that result in brittle tests that break if a div changes size or position by a few pixels, updating drivers, switching from xunit to nunit and writing some documentation.
* I'm hoping FluentAutomation will become more actively developed again and I can eventually replace my reliance on this project with FluentAutomation again, however I urgently need the testing features for a project that relies on the FluentAutomation syntax, and FluentAutomation is currently broken and not actively being developed. (last commit > 2 years ago.)
* result is much smaller code base
* The added benefit of the reduced API is a longer 'shelf life' between each package release before any update of Chrome or Selenium causes one of the API's on the test library to fail and require a new nuget release. 
* The aim is to be able to automate the testing of new Chrome packages, and immediately (automatically) bring out a new nuget package if an update to Selenium passes all tests, without human intervention.

### useful references

* https://www.automatetheplanet.com/selenium-webdriver-csharp-cheat-sheet/
