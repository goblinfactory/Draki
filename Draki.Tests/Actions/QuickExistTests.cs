using System.Linq;
using Draki.Exceptions;
using NUnit.Framework;

namespace Draki.Tests.Actions
{
    public class QuickExistsTests: BaseTest
    {

        // TODO add timing to the this test to assert it is quick
        [Test]
        public void WhenElementExists_QuickExists_ReturnsTrueQuickly()
        {
            InputsPage.Go();
            bool exists = I.QuickExists(InputsPage.TextControlSelector);
            Assert.True(exists);
        }

        [Test]
        public void When_NotExist_QuickExists_ReturnsFalseQuickly()
        {
            InputsPage.Go();
            bool exists = I.QuickExists("this-css-selector-does-not-exist");
            Assert.False(exists);
        }

    }
}
