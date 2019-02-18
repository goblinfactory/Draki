using Draki.Interfaces;
using Draki.Tests.Pages;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;

namespace Draki.Tests.Actions
{
    public class WaitForAnyTests : BaseTest
    {
        [Test]
        [Category(Category.SLOW)]
        public void WaitForAny()
        {
            HomePage.Go();

            //I.WaitUntil(()=> I.Expect.Text("foo").In("h2"));

            var page = I.WaitUntilAny(
                ("1234", () => I.Expect.Text("foo").In("h1")), // won't match
                ("home", () => I.Expect.Text("foo").In("h2"))  // should match
            );

            Assert.AreEqual("home", page);
        }
    }
}
