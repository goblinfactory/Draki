using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Draki.Tests.Pages;
using NUnit.Framework;

namespace Draki.Tests.Actions
{
    public class FocusTests : BaseTest
    {
        /// <summary>
        /// Attempt to focus a simple block element, in this case an H1
        /// </summary>
        [Test]
        public void FocusBlockElement()
        {
            TextPage.Go();
            I.Assert.Css("color", Colors.ALICE_BLUE).Not.On(TextPage.TitleSelector);
            // This shouldn't actually do anything. Focusing a block element makes no sense.
            I.Focus(TextPage.TitleSelector)
             .Assert.Css("color", Colors.ALICE_BLUE).Not.On(TextPage.TitleSelector);
        }

        [Test]
        public void FocusLink()
        {
            TextPage.Go();
            I.Assert.Css("text-decoration", TextPage.LinkTextDecorationHover).Not.On(TextPage.Link1Selector);
            I.Assert.Css("text-decoration", TextPage.LinkTextDecorationHover).Not.On(TextPage.Link1Selector);
            I.Focus(TextPage.Link1Selector);
            I.Assert.Css("text-decoration", TextPage.LinkTextDecorationHover).On(TextPage.Link1Selector);
            I.Focus(TextPage.Link2Selector);
            I.Assert.Css("text-decoration", TextPage.LinkTextDecorationHover).On(TextPage.Link2Selector);
        }

        [Test]
        [Category(Category.SLOW)]
        public void FocusInput()
        {
            InputsPage.Go();
            I.Assert.Css("background-color", Colors.ALICE_BLUE).Not.On(InputsPage.TextControlSelector);
            I.Focus(InputsPage.TextControlSelector)
             .Assert.Css("background-color", Colors.ALICE_BLUE).On(InputsPage.TextControlSelector);

            I.Assert.Css("background-color", Colors.ALICE_BLUE).Not.On(InputsPage.TextareaControlSelector);
            I.Focus(InputsPage.TextareaControlSelector)
             .Assert.Css("background-color", Colors.ALICE_BLUE).On(InputsPage.TextareaControlSelector);
        }

        [Test]
        public void FocusInputButton()
        {
            InputsPage.Go();
            I.Assert.Css("background-color", Colors.DARK_BLUE).Not.On(InputsPage.InputButtonControlSelector);
            I.Focus(InputsPage.InputButtonControlSelector)
                .Assert.Css("background-color", Colors.DARK_BLUE).On(InputsPage.InputButtonControlSelector);
        }

        [Test]
        public void FocusButton()
        {
            InputsPage.Go();
            I.Focus(InputsPage.InputButtonControlSelector);
            I.Assert.Css("background-color", InputsPage.ButtonFocusColor).Not.On(InputsPage.ButtonControlSelector);
            I.Focus(InputsPage.ButtonControlSelector);
            I.Assert.Css("background-color", InputsPage.ButtonFocusColor).On(InputsPage.ButtonControlSelector);
        }

        /// <summary>
        /// Test that we can still focus elements outside of the viewport
        /// </summary>
        [Test]
        [Category(Category.SLOW)]
        public void FocusScroll()
        {
            ScrollingPage.Go();

            I.Assert.Css("color", ScrollingPage.FocusColor).Not.On(ScrollingPage.TopRightSelector);
            I.Focus(ScrollingPage.TopRightSelector)
             .Assert.Css("color", ScrollingPage.FocusColor).On(ScrollingPage.TopRightSelector);

            I.Assert.Css("color", ScrollingPage.FocusColor).Not.On(ScrollingPage.BottomRightSelector);
            I.Focus(ScrollingPage.BottomRightSelector)
             .Assert.Css("color", ScrollingPage.FocusColor).On(ScrollingPage.BottomRightSelector);

            I.Assert.Css("color", ScrollingPage.FocusColor).Not.On(ScrollingPage.BottomLeftSelector);
            I.Focus(ScrollingPage.BottomLeftSelector)
             .Assert.Css("color", ScrollingPage.FocusColor).On(ScrollingPage.BottomLeftSelector);

            I.Assert.Css("color", ScrollingPage.FocusColor).Not.On(ScrollingPage.TopLeftSelector);
            I.Focus(ScrollingPage.TopLeftSelector)
             .Assert.Css("color", ScrollingPage.FocusColor).On(ScrollingPage.TopLeftSelector);
        }
    }
}
