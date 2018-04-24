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
        [Category(Category.SLOW)]
        public void WindowSwitchTest()
        {
            SwitchPage.Go();

            I.Click(SwitchPage.NewWindowSelector);

            I.Switch.Window("Inputs - FluentAutomation Testbed");

            I.Assert.Text("Input Controls Testbed").In("h2");

            I.Switch.Window()
             .Assert.Text("Switch Testbed").In("h2");

            I.Switch.Window("/Inputs")
             .Assert.Text("Input Controls Testbed").In("h2");

            I.Switch.Window()
             .Assert.Text("Switch Testbed").In("h2");
        }

        [Test]
        [Category(Category.SLOW)]
        public void WindowSwitchToTitleContainingTest()
        {
            SwitchPage.Go();

            I.Click(SwitchPage.NewWindowSelector);

            I.Switch.Window("Inputs - Flue");
            I.Assert.Text("Input Controls Testbed").In("h2");
        }

        [Test]
        [Category(Category.SLOW)]
        public void WindowSwitchToUrlContainingTest()
        {
            SwitchPage.Go();

            I.Click(SwitchPage.NewWindowSelector);

            I.Switch.Window("/Inp");
            I.Assert.Text("Input Controls Testbed").In("h2");
        }

        [Test]
        [Category(Category.VERYSLOW)]
        public void WindowSwitchToNonExistentThrowsTest()
        {
            SwitchPage.Go();
            Assert.Throws<FluentException>(() => I.Switch.Window("non existent window"));
        }

        [Test]
        public void SwitchToPopupTest()
        {
            SwitchPage.Go();

            I.Expect.Text("Switch Testbed").In("h2");
            // open pop up
            I.Click(SwitchPage.new_popup);

            I.Switch.Window("Inputs").Expect.Text("Inputs - FluentAutomation Testbed").In("head > title");
            I.Enter("can you see me").In(InputsPage.TextControlSelector);

            // switch back to primary window
            I.Switch.Window().Expect.Text("Switch Testbed").In("h2");
        }
    }
}
