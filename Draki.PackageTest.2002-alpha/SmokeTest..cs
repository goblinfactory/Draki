using Draki;
using NUnit.Framework;
using System;

namespace Tests
{
    [SetUpFixture]
    public class Bootstrapper : FluentTest
    {
        [SetUp]
        public void Setup()
        {
            FluentSession.EnableStickySession();
            SeleniumWebDriver.Bootstrap(Browser.Chrome);
            Config.WaitUntilTimeout(TimeSpan.FromMilliseconds(1000));
        }

        public void Teardown()
        {

        }
    }

    public class SmokeTest : FluentTest
    {

        [Test]
        public void RunSmokeTest()
        {
            Config.WaitUntilTimeout(TimeSpan.FromMilliseconds(1000));
            I.Open("http://www.catipsum.com/").Expect.Text("Litter your copy with more kitty.").In("span.cat_quote_text");
        }
    }
}