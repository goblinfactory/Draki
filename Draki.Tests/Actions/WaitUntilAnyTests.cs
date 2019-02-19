using Draki.Interfaces;
using Draki.Tests.Pages;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;

namespace Draki.Tests.Actions
{
    public class WaitUntilAnyTests : BaseTest
    {
        [Test]
        [Category(Category.SLOW)]
        public void WaitUntilAny()
        {
            HomePage.Go();

            //I.WaitUntil(()=> I.Expect.Text("foo").In("h2"));

            var page = I.WaitUntilAny(TimeSpan.FromSeconds(10),

                new FindText("1234", selector: "h1", containsText: "foo"),                              // won't match
                new FindText("home", selector: "h2", containsText: "FluentAutomation Testbed Index")    // should match!
            
            );

            Assert.AreEqual("home", page);
        }
    }
}
