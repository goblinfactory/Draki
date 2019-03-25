using Draki;
using NUnit.Framework;
using System;

namespace Tests
{
    public class Tests : FluentTest
    {
        [SetUp]
        public void Setup()
        {
            FluentSession.EnableStickySession();
            Config.WaitUntilTimeout(TimeSpan.FromMilliseconds(1000));
            SeleniumWebDriver.Bootstrap(Browser.Chrome);
        }

        public void Teardown()
        {
            
        }

        [Test]
        public void TestNETCore()
        {
            I.Open("http://www.catipsum.com/").Expect.Text("Litter your copy with more kitty.").In("span.cat_quote_text");
        }
    }
}


//// these three are common settings, convert to BootstrapChrome(); does it make sense to lazy autobootstrap chrome on I.Open();
//FluentSession.EnableStickySession();
//            //Config.WaitUntilTimeout(TimeSpan.FromMilliseconds(1000));
//            SeleniumWebDriver.Bootstrap(Browser.Chrome);
