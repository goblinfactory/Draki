using System;

namespace Draki
{
    /// <summary>
    /// Simplest class possible to confirm both the Draki.Core and Draki.SeleniumWebDriver projects can be referenced and execute most rudimentary critical action.
    /// </summary>
    public class SmokeTest : FluentTest
    {
        private readonly string Url;
        public string CssSelector { get; }
        public string ExpectedText { get; }

        public SmokeTest(string url, string cssSelector, string expectedText)
        {
            Url = url;
            CssSelector = cssSelector;
            ExpectedText = expectedText;
        }

        public bool BootStrapChromeDriverOpenPageAndConfirmText()
        {
            SeleniumWebDriver.Bootstrap(TimeSpan.FromSeconds(1));
            I.Open(Url).Expect.Text(ExpectedText).In(CssSelector);
            return true;
        }
    }
}
