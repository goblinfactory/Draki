using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FluentAutomation.Tests.Asserts
{
    public class ExistsTests : AssertBaseTest
    {
        public ExistsTests()
            : base()
        {
            InputsPage.Go();
        }

        [Test]
        public void ElementExists()
        {
            I.Assert
             .Exists("div")
             .Not.Exists("crazyElementThatDoesntExist")
             .Exists(I.Find("div"))
             .Not.Exists(I.Find("crazyElementThatDoesntExist"));

            I.Expect
             .Exists("div")
             .Not.Exists("crazyElementThatDoesntExist")
             .Exists(I.Find("div"))
             .Not.Exists(I.Find("crazyElementThatDoesntExist"));
        }
    }
}
