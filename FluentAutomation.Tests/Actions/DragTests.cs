using FluentAutomation.Interfaces;
using Xunit;

namespace FluentAutomation.Tests.Actions
{
    public class DragTests : BaseTest
    {
        public DragTests()
            : base()
        {
            DragPage.Go();
        }

        [Fact]
        public void DragAndDropBySelector()
        {
            var peg = I.Find(DragPage.Peg1).Element;
            var hole1 = I.Find(DragPage.Hole1).Element;
            var hole2 = I.Find(DragPage.Hole2).Element;

            // selector to selector
            NotOnTop(peg, hole2);
            I.Drag(DragPage.Peg1).To(DragPage.Hole2);
            OnTop(peg, hole2);
        }

        private void NotOnTop(IElement e1, IElement e2)
        {
            I.Assert.False(() => e1.PosX == e2.PosX && e1.PosY == e2.PosY);
        }

        private void OnTop(IElement e1, IElement e2)
        {
            I.Assert.True(() => e1.PosX == e2.PosX && e1.PosY == e2.PosY);
        }
    }
}
