using Draki;
using Draki.Interfaces;
using NUnit.Framework;

namespace TestPackage.V0100
{
    public class TestIt : FluentTest
    {
        [Test]
        public void test_the_package()
        {
            const string root = "http://localhost:38043/";
            FluentSession.EnableStickySession();
            
            SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome);

            I.Open(root);
            I.ClickLink("Alerts");
            I.Assert.Text("Alerts Testbed").In("h2");
            I.Open(root + "Inputs");
            I.Enter("hello word").In("#textarea-control");
        }

    }

    public static class FluentTestExtensions
    {
        public static IActionSyntaxProvider ClickLink(this IActionSyntaxProvider I, string linkText)
        {
            var selector = $"a:contains('{linkText}')";
            I.Click(selector);
            return I;
        }
    }
}
