using System.Linq;
using Draki.Exceptions;
using NUnit.Framework;

namespace Draki.Tests.Actions
{
    public class FindTests : BaseTest
    {

        [Test]
        public void FindElement()
        {
            InputsPage.Go();
            var element = I.Find(InputsPage.TextControlSelector).Element;
            Assert.True(element.IsText);
        }

        public void FindNonExistentElementThrowsTest()
        {
            Assert.Throws<FluentElementNotFoundException>(() => I.Find("doesntexist").Element.ToString()); // accessing Element executes the Find
        }

        [Test]
        public void FindSpecificElementTest()
        {
            InputsPage.Go();
            Assert.True(I.Find(InputsPage.ButtonControlSelector).Element.Text == "Button");
        }


        [Test]
        public void FindMultipleElements()
        {
            InputsPage.Go();
            var proxy = I.FindMultiple("div");

            Assert.True(proxy.Elements.Count > 1);
            Assert.False(proxy.Element.IsText);
            Assert.True(proxy.Element.Text == proxy.Element.Value);
        }

        [Test]
        public void AttemptToFindFakeElement()
        {
            InputsPage.Go();
            var exception = Assert.Throws<FluentElementNotFoundException>(() => I.Find("#very-fake-control").Element.ToString()); // accessing Element executes the Find
            Assert.True(exception.Message.Contains("Unable to find"));
            Assert.Throws<FluentElementNotFoundException>(() => I.FindMultiple("doesntexist").Element.ToString());
        }
    }
}
