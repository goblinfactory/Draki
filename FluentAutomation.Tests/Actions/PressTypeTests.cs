using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FluentAutomation.Tests.Actions
{
    public class PressTypeTests : BaseTest
    {
        public PressTypeTests()
            : base()
        {
            InputsPage.Go();
        }

        [Test]
        public void PressType()
        {
            I.Focus(InputsPage.TextControlSelector)
             .Press("{TAB}")
             .Type("wat")
             .Assert.Text("wat").In(InputsPage.TextareaControlSelector);
        }

    }
}
