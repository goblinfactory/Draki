using Draki;
using System;
using TechTalk.SpecFlow;

namespace TestDotNetFramework.Features
{
    [Binding]
    public class Pagination3Steps : FluentTest
    {
        [Given(@"(.*) adverts in a list and a page size of (.*)")]
        public void GivenAdvertsInAListAndAPageSizeOf(int p0, int p1)
        {
            // for the demo not going to run linqpad scripts to modify the database 
            // will run that by hand before the tests.
        }
        
        [When(@"I view the list")]
        public void WhenIViewTheList()
        {
            I.Open(Pages.Pagination.InternetWeb_WebDesign_General);
        }

        [Then(@"only (.*) items should be visible")]
        public void ThenOnlyItemsShouldBeVisible(int p0)
        {
            I.ExpectMultiple(10, "a:contains('details')");
        }

        [Then(@"the pager should have (.*) pages")]
        public void ThenThePagerShouldHavePages(int p0)
        {
            I.Expect.Text("Page: 1 2 3 ").In("div.pager td");
        }

        [Then(@"I should be on page (.*)")]
        public void ThenIShouldBeOnPage(int page)
        {
            switch(page)
            {
                case 1: I.Expect.Text("test_1").In("td.tb>b>a"); break;
                case 2: I.Expect.Text("test_11").In("td.tb>b>a"); break;
            }
        }

        [When(@"I click (.*) items per page")]
        public void WhenIClickItemsPerPage(int p0)
        {
            I.Click("div.pager a:contains('30')");
        }

        [When(@"I click page (.*) link")]
        public void WhenIClickPageLink(int p0)
        {
            I.Click("div.pager a");
        }
                
        [Then(@"there should only be (.*) page")]
        public void ThenThereShouldOnlyBePage(int p0)
        {
            I.Expect.Text("Page: 1 ").In("div.pager td");
        }
    }
}
