using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FluentAutomation.Tests.Base
{
    public class FluentTestTests : BaseTest
    {
        public FluentTestTests()
            : base()
        {
        }

        [Test]
        public void WebDriverIsAvailable()
        {
            Assert.True(this.Provider != null);
        }
    }

    /// <summary>
    /// Need to test that the non-generic FluentTest instance can still get access
    /// to Selenium Provider
    /// </summary>
    public class MoreFluentTestTests : FluentTest
    {
        public MoreFluentTestTests()
        {
            FluentAutomation.SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome);

            Config.MinimizeAllWindowsOnTestStart(true);
        }

        [Test]
        public void ProviderIsAvailable()
        {
            I.Open("http://google.com/");
            Assert.True(this.Provider != null);
            Assert.True((this.Provider as IWebDriver) != null);

            Config.MinimizeAllWindowsOnTestStart(false);
        }
    }
}
