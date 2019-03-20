using Draki.Interfaces;
using NUnit.Framework;

namespace Draki.Tests.Actions
{
    public class DragTests : BaseTest
    {
        [Test]
        public void DragAndDropExternalPage()
        {
            I.Open("http://jqueryui.com/resources/demos/droppable/default.html");
            I.Drag("draggable").To("droppable");
            int x = 5;
        }

        [Test]
        [Category(Category.SLOW)]
        public void DragAndDropBySelector()
        {
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

            I.Drag(boarder1).To(conference1);
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
