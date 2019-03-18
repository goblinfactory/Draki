using NUnit.Framework;

namespace Draki.Tests.Actions
{
    public class PressTypeTests : BaseTest
    {

        [Test]
        public void PressType()
        {
            InputsPage.Go();

            I.Focus(InputsPage.TextControlSelector)
             .Press(Key.TAB)
             .Type("wat")
             .Assert.Text("wat").In(InputsPage.TextareaControlSelector);
        }

    }
}
