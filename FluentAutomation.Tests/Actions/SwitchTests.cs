using FluentAutomation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FluentAutomation.Tests.Actions
{
    public class SwitchTests : BaseTest
    {
        public SwitchTests()
        {
            SwitchPage.Go();
        }

        [Test]
        public void FrameSwitchTest()
        {
            I.Switch.Frame(SwitchPage.IFrameSelector)
             .Assert.Text("Alerts Testbed").In("h2");

            I.Switch.Frame()
             .Assert.Text("Switch Testbed").In("h2");

            I.Switch.Frame(I.Find(SwitchPage.IFrameSelector))
             .Assert.Text("Alerts Testbed").In("h2");
        }

        //TODO: Need to mark this as a slow test!
        [Test]
        public void FrameSwitchToNonexistantFrameThrowsTest()
        {
            I.Switch.Frame(SwitchPage.IFrameSelector)
             .Assert.Text("Alerts Testbed").In("h2");

            var ex = Assert.Throws<FluentException>(() => I.Switch.Frame("nonExistentFrame"));
            // actual message is "No frame element found with name or id non existent frame" 
            // but not using that in case the format changes.
            StringAssert.Contains("nonExistentFrame", ex.InnerException.Message);
        }


        [Test]
        public void WindowSwitchTest()
        {
            I.Click(SwitchPage.NewWindowSelector);

            I.Switch.Window("Inputs - FluentAutomation Testbed")
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
