using Draki.Exceptions;
using NUnit.Framework;

namespace Draki.Tests.Actions
{
    public class ClickTests : BaseTest
    {
        [Test]
        [Category(Category.FLAKEY)]
        public void LeftClick()
        {
            // either flakey or has side effects or is impacted by side effects!
            InputsPage.Go();
            I.Click(InputsPage.ButtonControlSelector)
                .WaitUntil(()=> I.Expect.Text("Button Clicked").In(InputsPage.ButtonClickedTextSelector));

            I.Click(InputsPage.InputButtonControlSelector)
                .WaitUntil(()=> I.Expect.Text("Input Button Clicked").In(InputsPage.ButtonClickedTextSelector));
        }

        [Test]
        [Category(Category.SLOW)]
        [Category(Category.FLAKEY)]
        public void LinkClick()
        {
            InputsPage.Go();
            I.Click(InputsPage.LinkButtonControlSelector)
                .WaitUntil(() => I.Expect.Text("If you see me, then the click link worked").In("#seeme"));
            InputsPage.Go();
            I.Click(InputsPage.LinkButtonJavascriptControlSelector)
                .WaitUntil(() => I.Expect.Text("If you see me, then the click link worked").In("#seeme"));

        }

        [Test]
        [Category(Category.SLOW)]
        public void RightClick()
        {
            InputsPage.Go();
            // TODO: write up demo code showing how this needs to be done to cater for page settling down
            // TODO: create new overloaded method that does this automatically in 1 step

            I.RightClick(InputsPage.ButtonControlSelector)
                .WaitUntil(()=> I.Expect.Text("Button Right Clicked").In(InputsPage.ButtonClickedTextSelector));

            I.RightClick(InputsPage.InputButtonControlSelector)
                .WaitUntil(()=> I.Expect.Text("Input Button Right Clicked").In(InputsPage.ButtonClickedTextSelector));
        }

        //[Test]
        public void DoubleClick()
        {
            InputsPage.Go();
            I.DoubleClick(InputsPage.ButtonControlSelector)
                .WaitUntil(()=> I.Expect.Text("Button Double Clicked").In(InputsPage.ButtonClickedTextSelector));

            // double click is in fact 4 events, mousedown, up, down and up again, 
            // so we have to "wait" until page settles down, i.e. our condition we are looking for is met, since the text values might be cycling through various options

            I.DoubleClick(InputsPage.InputButtonControlSelector)
                .WaitUntil(()=> I.Expect.Text("Input Button Double Clicked").In(InputsPage.ButtonClickedTextSelector));
        }

        [Test]
        [Category(Category.VERYSLOW)]
        public void AlertClicks()
        {
            AlertsPage.Go();

            // No alerts present
            Assert.Throws<FluentException>(() => I.Click(Alert.OK));

            // Can't 'click' these fields
            Assert.Throws<FluentException>(() => I.Click(Alert.Input));
            Assert.Throws<FluentException>(() => I.Click(Alert.Message));

            // Alert box:
            // Alerts don't have OK/Cancel but both work, so we test as if Cancel was clicked
            I.Click(AlertsPage.TriggerAlertSelector)
             .Click(Alert.OK)
             .Assert.Text("Clicked Alert Cancel").In(AlertsPage.ResultSelector);

            I.Click(AlertsPage.TriggerAlertSelector)
             .Click(Alert.Cancel)
             .Assert.Text("Clicked Alert Cancel").In(AlertsPage.ResultSelector);

            // Confirmation box:
            I.Click(AlertsPage.TriggerConfirmSelector)
             .Click(Alert.OK)
             .Assert.Text("Clicked Confirm OK").In(AlertsPage.ResultSelector);

            I.Click(AlertsPage.TriggerConfirmSelector)
             .Click(Alert.Cancel)
             .Assert.Text("Clicked Confirm Cancel").In(AlertsPage.ResultSelector);

            // Prompt box:
            I.Click(AlertsPage.TriggerPromptSelector)
             .Enter("1").In(Alert.Input)
             .Assert.Text("Clicked Prompt OK: 1").In(AlertsPage.ResultSelector);

            I.Click(AlertsPage.TriggerPromptSelector)
             .Click(Alert.Cancel)
             .Assert.Text("Clicked Prompt Cancel").In(AlertsPage.ResultSelector);
        }
    }
}
