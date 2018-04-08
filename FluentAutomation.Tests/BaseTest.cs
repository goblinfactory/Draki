using OpenQA.Selenium;
using System;

namespace FluentAutomation.Tests
{
    /// <summary>
    /// Base Test that opens the test to the AUT
    /// </summary>
    public class BaseTest : FluentTest<IWebDriver>
    {
        public string SiteUrl { get { return "http://localhost:38043/"; } }
        
        public BaseTest()
        {
            FluentSession.EnableStickySession();
            Config.WaitUntilTimeout(TimeSpan.FromMilliseconds(1000));

            // Create Page Objects
            InputsPage = new Pages.InputsPage(this);
            AlertsPage = new Pages.AlertsPage(this);
            ScrollingPage = new Pages.ScrollingPage(this);
            TextPage = new Pages.TextPage(this);
            DragPage = new Pages.DragPage(this);
            SwitchPage = new Pages.SwitchPage(this);
            
            // Default tests use chrome and load the site
            SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome);//, SeleniumWebDriver.Browser.InternetExplorer, SeleniumWebDriver.Browser.Firefox);
            I.Open(SiteUrl);
        }

        public Pages.InputsPage InputsPage = null;
        public Pages.AlertsPage AlertsPage = null;
        public Pages.ScrollingPage ScrollingPage = null;
        public Pages.TextPage TextPage = null;
        public Pages.DragPage DragPage = null;
        public Pages.SwitchPage SwitchPage = null;
    }

    public class AssertBaseTest : BaseTest
    {
        public AssertBaseTest()
            : base()
        {
            Config.OnExpectFailed((ex, state) =>
            {
                // For the purpose of these tests, allow expects to throw (break test)
                throw ex;
            });
        }
    }
}
