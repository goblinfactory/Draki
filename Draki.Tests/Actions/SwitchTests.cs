using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Draki.Exceptions;
using NUnit.Framework;

namespace Draki.Tests.Actions
{
    public class SwitchTests : BaseTest
    {

        [Test]
        [Category(Category.SLOW)]
        public void FrameSwitchTest()
        {
            SwitchPage.Go();

            I.Switch.Frame(SwitchPage.IFrameSelector)
             .Assert.Text("Alerts Testbed").In("h2");

            I.Switch.Frame()
             .Assert.Text("Switch Testbed").In("h2");

            I.Switch.Frame(I.Find(SwitchPage.IFrameSelector))
             .Assert.Text("Alerts Testbed").In("h2");
        }

        [Test]
        [Category(Category.VERYSLOW)]
        public void FrameSwitchToNonexistantFrameThrowsTest()
        {
            SwitchPage.Go();

            I.Switch.Frame(SwitchPage.IFrameSelector)
             .Assert.Text("Alerts Testbed").In("h2");

            var ex = Assert.Throws<FluentException>(() => I.Switch.Frame("nonExistentFrame"));
            // actual message is "No frame element found with name or id non existent frame" 
            // but not using that in case the format changes.
            StringAssert.Contains("nonExistentFrame", ex.InnerException.Message);
        }


        [Test]
        [Category(Category.VERYSLOW)]
        public void WindowSwitchTest()
        {
            SwitchPage.Go();

            I.Click(SwitchPage.NewWindowSelector);

            I.Switch.Window("Inputs - Draki Testbed")
             .Assert.Text("Input Controls Testbed").In("h2");

            I.Switch.Window()
             .Assert.Text("Switch Testbed").In("h2");

            I.Switch.Window("/Inputs")
             .Assert.Text("Input Controls Testbed").In("h2");

            I.Switch.Window()
             .Assert.Text("Switch Testbed").In("h2");

            Assert.Throws<FluentException>(() => I.Switch.Window("non existent window"));
        }
    }
}
