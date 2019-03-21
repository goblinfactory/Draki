using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Draki.Tests.Pages;
using NUnit.Framework;

namespace Draki.Tests.Actions
{
    public class HoverTests : BaseTest
    {

        /// <summary>
        /// Hover over a simple block element, in this case an H1
        /// </summary>
        [Test]
        public void HoverBlockElement()
        {
            TextPage.Go();

            I.Assert.Css("color", Colors.RED).Not.On(TextPage.TitleSelector);
            I.Hover(TextPage.TitleSelector);
            I.Assert.Css("color", Colors.RED).On(TextPage.TitleSelector);
        }

        [Test]
        public void HoverLink()
        {
            TextPage.Go();

            I.Assert.Css("color", Colors.RED).Not.On(TextPage.Link1Selector);
            I.Hover(TextPage.Link1Selector)
             .Assert.Css("color", Colors.RED).On(TextPage.Link1Selector);
        }

        [Test]
        [Category(Category.SLOW)]
        public void HoverInput()
        {
            InputsPage.Go();

            I.Assert.Css("color", Colors.RED).Not.On(InputsPage.TextControlSelector);
            I.Hover(InputsPage.TextControlSelector)
             .Assert.Css("color", Colors.RED).On(InputsPage.TextControlSelector);

            I.Assert.Css("color", Colors.RED).Not.On(InputsPage.TextareaControlSelector);
            I.Hover(InputsPage.TextareaControlSelector)
             .Assert.Css("color", Colors.RED).On(InputsPage.TextareaControlSelector);
        }

        [Test]
        public void HoverInputButton()
        {
            InputsPage.Go();

            I.Assert.Css("color", Colors.RED).Not.On(InputsPage.InputButtonControlSelector);
            I.Hover(InputsPage.InputButtonControlSelector)
             .Assert.Css("color", Colors.RED).On(InputsPage.InputButtonControlSelector);
        }

        [Test]
        public void HoverButton()
        {
            InputsPage.Go();

            I.Assert.Css("color", Colors.RED).Not.On(InputsPage.ButtonControlSelector);
            I.Hover(InputsPage.ButtonControlSelector)
             .Assert.Css("color", Colors.RED).On(InputsPage.ButtonControlSelector);
        }

        /// <summary>
        /// Test that Hover actually pulls elements into the viewport
        /// </summary>
        [Test]
        public void HoverScroll()
        {
            ScrollingPage.Go();

            I.Assert.Css("color", ScrollingPage.HoverColor).Not.On(ScrollingPage.TopRightSelector);
            I.Hover(ScrollingPage.TopRightSelector)
             .Assert.Css("color", ScrollingPage.HoverColor).On(ScrollingPage.TopRightSelector);

            I.Assert.Css("color", ScrollingPage.HoverColor).Not.On(ScrollingPage.BottomRightSelector);
            I.Hover(ScrollingPage.BottomRightSelector)
             .Assert.Css("color", ScrollingPage.HoverColor).On(ScrollingPage.BottomRightSelector);

            I.Assert.Css("color", ScrollingPage.HoverColor).Not.On(ScrollingPage.BottomLeftSelector);
            I.Hover(ScrollingPage.BottomLeftSelector)
             .Assert.Css("color", ScrollingPage.HoverColor).On(ScrollingPage.BottomLeftSelector);

            I.Assert.Css("color", ScrollingPage.HoverColor).Not.On(ScrollingPage.TopLeftSelector);
            I.Hover(ScrollingPage.TopLeftSelector)
             .Assert.Css("color", ScrollingPage.HoverColor).On(ScrollingPage.TopLeftSelector);
        }

        /// <summary>
        /// Test that Scroll is equivalent to Hover
        /// </summary>
        [Test]
        [Category(Category.SLOW)]
        public void Scroll()
        {
            InputsPage.Go();
            I.Assert.Css("color", Colors.RED).Not.On(InputsPage.ButtonControlSelector);
            I.Scroll(InputsPage.ButtonControlSelector)
             .Assert.Css("color", Colors.RED).On(InputsPage.ButtonControlSelector);

            ScrollingPage.Go();

            // Identical to this.HoverScroll()
            I.Assert.Css("color", ScrollingPage.HoverColor).Not.On(ScrollingPage.TopRightSelector);
            I.Scroll(ScrollingPage.TopRightSelector)
             .Assert.Css("color", ScrollingPage.HoverColor).On(ScrollingPage.TopRightSelector);

            I.Assert.Css("color", ScrollingPage.HoverColor).Not.On(ScrollingPage.BottomRightSelector);
            I.Scroll(ScrollingPage.BottomRightSelector)
             .Assert.Css("color", ScrollingPage.HoverColor).On(ScrollingPage.BottomRightSelector);

            I.Assert.Css("color", ScrollingPage.HoverColor).Not.On(ScrollingPage.BottomLeftSelector);
            I.Scroll(ScrollingPage.BottomLeftSelector)
             .Assert.Css("color", ScrollingPage.HoverColor).On(ScrollingPage.BottomLeftSelector);

            I.Assert.Css("color", ScrollingPage.HoverColor).Not.On(ScrollingPage.TopLeftSelector);
            I.Scroll(ScrollingPage.TopLeftSelector)
             .Assert.Css("color", ScrollingPage.HoverColor).On(ScrollingPage.TopLeftSelector);
        }
    }
}
