using NUnit.Framework;

namespace Draki.Tests.Actions
{
    public class DragTests : BaseTest
    {
        [Test]
        public void JqueryDragAndDrop_using_draggable_and_droppable()
        {
            // this tests passes because the HTML is using JQUERY draggable
            I.Open("http://jqueryui.com/resources/demos/droppable/default.html");
            I.Drag("#draggable").To("#droppable");
        }

        [Test]
        [Category(Category.SLOW)]
        public void HTML5DragAndDropUsing_ondragstart_draggable_ondragover_and_ondrop()
        {
            Assert.Inconclusive("While Draki works, there is currently a bug with ChromeDriver and HTML5 drag and drop. Draki merely delegates the call to Selenium which has a known issue with the Chromedriver");

            // this should probably be added to Draki as a plugin, simply include an additional package.
            // this test currently fails because the HTML uses HTML5 drag events, that currently cannot be exercised via the Chrome driver.
            // this link suggests a workaround that draki could possibly implement, and would require injecting more than just sizzle into the page.
            // need to decide when wiring up the driver whether we need this, and if we do, then we do the injection, otherwise we don't have to.
            // http://elementalselenium.com/tips/39-drag-and-drop  
            // https://gist.github.com/rcorreia/2362544 6 years old?
            // here's a javascript drag drop simulator that's available under MIT
            // https://github.com/jquery/jquery-simulate/blob/master/jquery.simulate.js
            // typescript solution : https://github.com/html-dnd/html-dnd  2 years old.

            // Collin's selenium book could be helpful?
            // https://books.google.co.uk/books?id=PMNiDwAAQBAJ&printsec=frontcover#v=onepage&q=drag&f=false

            DragPage.Go();

            var boarder1 = I.Find("#boarder1");
            var boarder2 = I.Find("#boarder2");
            var boarder3 = I.Find("#boarder3");
            var conference1 = I.Find("#conference1");
            var conference2 = I.Find("#conference2");
            var conference3 = I.Find("#conference3");

            // selector to selector
            NotOnTop(boarder1, conference1);
            NotOnTop(boarder2, conference2);
            NotOnTop(boarder3, conference3);

            I.Drag("#boarder1").To("#conference1");
            IsOnTop(boarder1, conference1);

            I.Drag(boarder2).To(conference2);
            IsOnTop(boarder2, conference2);

            I.Drag(boarder3).To(conference3);
            IsOnTop(boarder3, conference3);

        }

        private void NotOnTop(ElementProxy ep1, ElementProxy ep2)
        {
            return;
            var e1 = ep1.Element;
            var e2 = ep2.Element;
            I.Assert.False(() => e1.PosX == e2.PosX && e1.PosY == e2.PosY);
        }

        private void IsOnTop(ElementProxy ep1, ElementProxy ep2)
        {
            return;
            var e1 = ep1.Element;
            var e2 = ep2.Element;
            I.Assert.True(() => e1.PosX == e2.PosX && e1.PosY == e2.PosY);
        }
    }
}
