using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Draki.Tests.Asserts
{
    public class CountTests : AssertBaseTest
    {

        [Test]
        [Category(Category.SLOW)]
        public void CountElements()
        {
            InputsPage.Go();

            I.Assert.Count(1).Of("h2");
            I.Expect.Count(1).Of("h2");

            I.Assert.Count(10).Of("input");
            I.Expect.Count(10).Of("input");

            I.Assert
             .Count(0).Not.Of("div")
             .Count(0).Not.Of(I.Find("div"))
             .Count(0).Of("crazyElementThatDoesntExist")
             .Count(0).Of(I.Find("crazyElementThatDoesntExist"));

            I.Expect
             .Count(0).Not.Of("div")
             .Count(0).Not.Of(I.Find("div"))
             .Count(0).Of("crazyElementThatDoesntExist")
             .Count(0).Of(I.Find("crazyElementThatDoesntExist"));
        }

        [Test]
        [Category(Category.VERYSLOW)]
        public void CountFailure()
        {
            I.Assert
             .Throws(() => I.Assert.Count(0).Of("div"))
             .Throws(() => I.Assert.Count(1).Of("div"))
             .Throws(() => I.Assert.Count(1).Of("crazyElementThatDoesntExist"))
             .Throws(() => I.Assert.Count(0).Not.Of("crazyElementThatDoesntExist"));
        }
    }
}
