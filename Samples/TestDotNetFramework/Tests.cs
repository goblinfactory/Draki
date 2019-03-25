using Draki;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDotNetFramework
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
        public void TestNETFramework()
        {
            I.Open("http://www.catipsum.com/").Expect.Text("Litter your copy with more kitty.").In("span.cat_quote_text");
        }

    }

}
