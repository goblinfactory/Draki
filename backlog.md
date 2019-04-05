# Alan's Backlog & random thoughts - notes

## TODO

- create Draki gitter. Remove old.
- create draki website.
- move drivers, chrome and IE to separate package.
- add chromedriver and IEDriver licences.
- remove the Page objects, replace with simpler static page helpers.
- speak to community about chromedriver being embedded. Possibly move that to a seperate package, so you can either have the build script download chromedriver from google directly, or more obviously reference a package that just embeds the latest driver.
  - this package should really come from google, not a random person, not me, not someone in china? https://www.nuget.org/packages/Selenium.WebDriver.ChromeDriver/
- take a look at https://github.com/jbaranda/nupkg-selenium-webdrivers
  and see if that avoids having to embed the drivers,
  or if thats just doing the same thing in a more roundabout way. 
- go to : https://github.com/stirno/FluentAutomation/issues and see if I can write tests for these, and if any issues are serious or fixed or need to be brought into this project.

## unsorted

- write up diff between draki and selenium.
- add ability to select options from <Input> based dropdowns! New in html 5 (vueJs)
	- click input, scroll select into view, click select.

## proposed [1.3.0] busy

- `T[] Parse<T>(cssSelector)` : parses the results from element into T. Each property on T becomes a selector, and will find the matching property from the selected items, or in it's children.
- `Verify<T>` : better verification, scrubb.
- VerifyStrict(cssSelector) : hard core, verify 100% - not even a single character or whitespace can change without requiring approval.

## proposed [1.2.0] - busy

- get onto appveyor or AzureDevops.
- add extra pages to test project, with cookie (forms authentication) logins to test logging in as different users.
- `void ClickLink(string linktextContains)` : click the first link with text containing x
- `string GetXml(cssSelector)` : retrieve the outterXml for an element by cssSelector. Typically used for snapshot testing verification.
- `string GetText(cssSelector)` : retrieve the text of an element by cssSelector
- `string[] GetTexts(cssSelector, bool mustBeManyAndMoreThanOne)` : used for bulk verification of repeating items.

### TODO (to investigate)

* Strip out and remove as much code as I can.
  - do we really need runtime assembly loading? see FluentSession
  - do we need Expressions?
  - do we need the whole API?
* rename Draki.Core back into Draki, so that I don't end up with a Draki.Core nuget package, instead create a Draki only package.
* change the default for Text entry to be without events, this will make the tests more robust and faster. If you want to specifically trigger the events, then the default should require you to specify WithEvents() or something like that. Rationale is to stay away from SendKeys that can send keystrokes to windows outside of the browser. This is brittle and when it breaks it's NAAASTY!
* setup uservoice for the project so that we can start seeing who is using the library.
* also setup a gitter for the project, seed gitter with some mates I can convince to try it!
* rename FluentTest to DrakiTest

### Housekeeping

* look through code for all instances where Global settings have been changed as part of a test, and add in `try {} finally` code blocks to reset settings. 

* Aaargh, the fact that I'm having problem isolating these global settings, is a bigger concern that this should be dealt with and the only way that global settings should be able to be overridden should be as a parameter to the currently running test! Otherwise we can have all sorts of testing issues.
* no point in fixing this problem by forcing the test runner to run single threaded, since we can't make that a pre-requisite for the users of the library. The same problem we have testing our library is the same problem that users will have with their own code, so this must be fixed! Mitigation is to at the very least we should use `save, try { change } finally { restore }` pattern. ***this is not a good fix, but it will be better than what we have at the moment!*** 
* I would give up the fluent syntax in preference for fixing the above issue! if it came down to a choice?

### issue : fluent syntax and chaining in a test dsl

* chaining can be difficult when something goes wrong and you need to debug into a massively chained block of code. 
  - First thing I usually have to do is undo the chaining so that I can single step through the code. 
  - need to decide if the whole fluent DSL is not actually an anti-pattern, and whether we should simply reduce it
    to just the real fluent stuff, or remote it all completely and just leave the value added stuff.

  - I'm not a huge fan of base classes for tests. See if we can replace that with an instance you reference?