## Temporary FluentAutomation documentation

method | summary
--- | ---
[actions.append](/Docs/v3/actions.append.md)|Append text to the current values of inputs and textareas. Automatically calls <code>ToString()</code> on non-string values to simplify tests.
[actions.click](/Docs/v3/actions.click.md)|Click an element by selector, coordinates or cached reference. Optionally provide an offset to click relative to the item.
[actions.doubleclick](/Docs/v3/actions.doubleclick.md)|Double-click an element by selector, coordinates or cached reference. Optionally provide an offset to click relative to the item.
[actions.drag](/Docs/v3/actions.drag.md)|Drag &amp; drop works with elements, coordinates and offsets. In the next version, the offset functionality will look a bit less... stupid. Sorry about that.
[actions.enter](/Docs/v3/actions.enter.md)|Primary method of entering text into inputs and textareas. Automatically calls `ToString()` on non-string values to simplify tests.
[actions.find](/Docs/v3/actions.find.md)|Get a factory reference to an element. Returns a function that can be evaluated to return access to the underlying element. Used internally by all functions that target elements.
[actions.focus](/Docs/v3/actions.focus.md)|Set the browser's current focus to a specified element or cached reference to an element.
[actions.hover](/Docs/v3/actions.hover.md)|Cause the mouse to hover over a specified element, coordinates or position relative to an element.
[actions.intro](/Docs/v3/actions.intro.md)|We interchangeable use the terms `Action` and `Command` to refer to the same thing. This is because of terminology changes throughout the lifetime of the project.
[actions.open](/Docs/v3/actions.open.md)|Open and navigate the web browser to the specified URL or <a href="http://msdn.microsoft.com/en-us/library/system.uri(v=vs.110).aspx" target="_blank">Uri</a>. Using a Uri can be valuable to validate your URI fragment before using it in a test.
[actions.press](/Docs/v3/actions.press.md)|Triggers a single OS level keypress event. This method will send events to whatever the active window is at the time its trigger, currently **not guaranteed to be the actual browser window. Use with caution.**
[actions.rightclick](/Docs/v3/actions.rightclick.md)|Right-click an element by selector, coordinates or cached reference. Optionally provide an offset to click relative to the item.
[actions.select](/Docs/v3/actions.select.md)|
[actions.takescreenshot](/Docs/v3/actions.takescreenshot.md)|Grab a quick screenshot of the current browser window and save it to disk. The screenshot path is configurable via `Settings.ScreenshotPath`.
[actions.type](/Docs/v3/actions.type.md)|Type a string, one character at a time using OS level keypress events. This functionality will send keypress events to whatever the active window is at the time its trigger, currently **not guaranteed to be the actual browser window. Use with caution.**
[actions.upload](/Docs/v3/actions.upload.md)|Upload a file using an `<input type="file">` on the current page. The provided path must be absolute and point to the file you want to upload.
[actions.wait](/Docs/v3/actions.wait.md)|Wait for a specified period of time before continuing the test. Method accepts a number of seconds or a <a href="http://msdn.microsoft.com/en-us/library/system.timespan(v=vs.110).aspx" target="_blank">TimeSpan</a>. Not guaranteed to be exact.
[actions.waituntil](/Docs/v3/actions.waituntil.md)|Recommended method of waiting in tests. Conditional wait using anonymous functions that either return `true` / `false` or throw an `Exception` when the condition has not been met. Useful when content on the page is loaded dynamically or changes state during interactions.
[asserts.class](/Docs/v3/asserts.class.md)|Assert that an element matching selector has the specified class.
[asserts.count](/Docs/v3/asserts.count.md)|Assert that we have a certain count of items matching a selector.
[asserts.exists](/Docs/v3/asserts.exists.md)|Assert that an element is on the page.
[asserts.false](/Docs/v3/asserts.false.md)|Assert that an anonymous function should return false. Use with `I.Find` to fail tests properly if conditions are not met.
[asserts.intro](/Docs/v3/asserts.intro.md)|As of `v2.3` proper assertions were added to FluentAutomation.
[asserts.text](/Docs/v3/asserts.text.md)|Assert that an element matching selector has the specified text. Works with any DOM element that has `innerHTML` or can provides its contents/value via text.
[asserts.throws](/Docs/v3/asserts.throws.md)|Assert that an `Exception` should be thrown by the anonymous function. Useful for negative assertions such as testing that something is not present.
[asserts.true](/Docs/v3/asserts.true.md)|Assert that an anonymous function should return true. Use with `I.Find` to fail tests properly if conditions are not met.
[asserts.url](/Docs/v3/asserts.url.md)|Assert that the browser has the specified Url. If using the string or Uri overloads, the match must be exact.
[asserts.value](/Docs/v3/asserts.value.md)|Assert that an element matching selector has the specified value. Works with `<INPUT>`, `<TEXTAREA>` and `<SELECT>`
[getting-started](/Docs/v3/getting-started.md)|FluentAutomation is implemented using one of two supported automation providers - <a href="http://seleniumhq.org" target="_blank">Selenium WebDriver</a> or <a href="http://watin.org" target="_blank">WatiN</a>. Selenium is the preferred provider and the most developed. WatiN is provided as an alternative method of automation Internet Explorer. PhantomJS support has been moved into the Selenium package as a browser target.
[intro](/Docs/v3/intro.md)|FluentAutomation is a simple, powerful automated testing framework for web applications. It can be used with Selenium WebDriver or WatiN to test all sorts of browsers and devices.
[method-chaining](/Docs/v3/method-chaining.md)|As part of trying to provide the best fluent syntax we can, we now have method chaining on all "terminating methods". These are the bits that actually execute or do something, rather than just configure the next step.
[multi-browser](/Docs/v3/multi-browser.md)|**_Feature Status: Beta_**
[pageobjects](/Docs/v3/pageobjects.md)|Anyone who has done any serious amount of automated testing will tell you that the largest challenge we all face is fragile, hard to maintain tests.
[remote-webdriver](/Docs/v3/remote-webdriver.md)|We fully support connecting to remote WebDriver instances (including Selenium Grid) as of the v2.2 bits. Obviously this is only supported in the SeleniumWebDriver provider.
[scriptcs](/Docs/v3/scriptcs.md)|Access to FluentAutomation inside of <a href="http://scriptcs.net/" target="_blank">scriptcs</a> is trivially easy and very powerful. We have a custom Script Pack that provides access to the functionality you expect.
[settings](/Docs/v3/settings.md)|Most of the configurable settings are located on the static object FluentAutomation.Settings and can be set anywhere within your project.
[sticky-sessions](/Docs/v3/sticky-sessions.md)|**_Feature Status: Beta_**

Created : 09/04/2018
