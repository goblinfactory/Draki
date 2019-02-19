using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Draki.Tests.Actions
{
    public class GetTitleTests : BaseTest
    {
        [Test]
        [Category(Category.SLOW)]
        public void GetTitleTest()
        {
            HomePage.Go();
            var actual = I.GetTitle();
            Assert.AreEqual("Index - FluentAutomation Testbed", actual);
        }
    }
}
