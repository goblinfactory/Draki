using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FluentAutomation.Tests.Actions
{
    public class PressTypeTests : BaseTest
    {

        [Test]
        public void PressType()
        {
            InputsPage.Go();

            I.Focus(InputsPage.TextControlSelector)
             .Press("{TAB}")
             .Type("wat")
             .Assert.Text("wat").In(InputsPage.TextareaControlSelector);
        }

    }
}
