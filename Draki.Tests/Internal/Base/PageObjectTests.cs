using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Draki.Exceptions;
using Draki.Tests.Pages;
using NUnit.Framework;

namespace Draki.Tests.Base
{
    public class PageObjectTests : BaseTest
    {
        [Test]
        [Category(Category.SLOW)]
        public void SwitchPageObject()
        {
            SwitchPage.Go();

            var switchPage = InputsPage.Switch<SwitchPage>();
            Assert.True(switchPage.Url.EndsWith("Switch"));

            InputsPage.Go();

            // throw because we aren't on the SwitchPage and nothing is navigating us there
            Assert.Throws<FluentException>(() => InputsPage.Switch<SwitchPage>());
        }
    }
}
