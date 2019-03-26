using Draki;
using NUnit.Framework;
using System;

namespace TestDotNetFramework.Features
{
    [SetUpFixture]
    public class SetupTeardown : FluentTest
    {
        [OneTimeSetUp]
        public void OnetimeSetup()
        {
            FluentSession.EnableStickySession();
            Config.WaitUntilTimeout(TimeSpan.FromMilliseconds(1000));
            SeleniumWebDriver.Bootstrap(Browser.Chrome);
        }

        [OneTimeTearDown]
        public void OnetimeTeardown()
        {
        }
    }
}
