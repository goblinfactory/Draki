using FluentAutomation.Exceptions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FluentAutomation.Tests.Actions
{
    public class SelectTests : BaseTest
    {
        public SelectTests()
            : base()
        {
            InputsPage.Go();
        }

        [Test]
        public void SelectValue()
        {
            I.Select(Option.Value, "QC").From(InputsPage.SelectControlSelector)
             .Assert.Text("Québec").In(InputsPage.SelectControlSelector);
        }

        [Test]
        [Category(Category.SLOW)]
        public void SelectIndex()
        {
            I.Select(3).From(InputsPage.SelectControlSelector)
             .Assert
                .Value("MB").In(InputsPage.SelectControlSelector)
                .Text("Manitoba").In(InputsPage.SelectControlSelector);
        }

        [Test]
        public void SelectText()
        {
            I.Select("Québec").From(InputsPage.SelectControlSelector)
             .Assert.Value("QC").In(InputsPage.SelectControlSelector);
        }

        [Test]
        [Category(Category.SLOW)]
        public void SelectClearsOptionBetweenSelections()
        {
            I.Select("Québec").From(InputsPage.SelectControlSelector)
             .Assert.Value("QC").In(InputsPage.SelectControlSelector);

            I.Select("Manitoba").From(InputsPage.SelectControlSelector)
             .Assert
                .Value("MB").In(InputsPage.SelectControlSelector)
                .Value("QC").Not.In(InputsPage.SelectControlSelector);
        }

        [Test]
        [Category(Category.VERYSLOW)]
        public void SelectTextFailed()
        {
            var exception = Assert.Throws<FluentException>(() => I.Select("NonExistentText").From(InputsPage.SelectControlSelector));
            Assert.True(exception.InnerException.Message.Contains("NonExistentText"));
        }

        [Test]
        [Category(Category.VERYSLOW)]
        public void SelectValueFailed()
        {
            var exception = Assert.Throws<FluentException>(() => I.Select(Option.Value, "NonExistentValue").From(InputsPage.SelectControlSelector));
            Assert.True(exception.InnerException.Message.Contains("NonExistentValue"));
        }

        [Test]
        public void SelectIndexFailed()
        {
            var exception = Assert.Throws<FluentException>(() => I.Select(1000).From(InputsPage.SelectControlSelector));
            Assert.True(exception.InnerException.Message.Contains("1000"));
        }

        [Test]
        [Category(Category.SLOW)]
        public void MultiSelectValue()
        {
            I.Select(Option.Value, "QC", "MB").From(InputsPage.MultiSelectControlSelector)
             .Assert
                .Text("Québec").In(InputsPage.MultiSelectControlSelector)
                .Text("Manitoba").In(InputsPage.MultiSelectControlSelector)
                .Text("Alberta").Not.In(InputsPage.MultiSelectControlSelector);
        }

        [Test]
        [Category(Category.SLOW)]
        public void MultiSelectIndex()
        {
            I.Select(2).From(InputsPage.MultiSelectControlSelector)
             .Assert
                .Text("Manitoba").In(InputsPage.MultiSelectControlSelector);

            I.Select(2, 3, 4).From(InputsPage.MultiSelectControlSelector)
             .Assert
                .Text("Manitoba").In(InputsPage.MultiSelectControlSelector)
                .Text("Nouveau-Brunswick").In(InputsPage.MultiSelectControlSelector)
                .Text("Terre-Neuve").In(InputsPage.MultiSelectControlSelector);
        }

        [Test]
        [Category(Category.VERYSLOW)]
        public void MultiSelectText()
        {
            I.Select("Manitoba").From(InputsPage.MultiSelectControlSelector)
             .Assert
                .Value("MB").In(InputsPage.MultiSelectControlSelector);

            I.Select(Option.Text, "Nouveau-Brunswick").From(InputsPage.MultiSelectControlSelector)
             .Assert
                .Value("NB").In(InputsPage.MultiSelectControlSelector);

            I.Select("Manitoba", "Nouveau-Brunswick", "Terre-Neuve").From(InputsPage.MultiSelectControlSelector)
             .Assert
                .Value("MB").In(InputsPage.MultiSelectControlSelector)
                .Value("NB").In(InputsPage.MultiSelectControlSelector)
                .Value("NL").In(InputsPage.MultiSelectControlSelector);

            I.Select(Option.Text, "Manitoba", "Nouveau-Brunswick", "Terre-Neuve").From(InputsPage.MultiSelectControlSelector)
             .Assert
                .Value("MB").In(InputsPage.MultiSelectControlSelector)
                .Value("NB").In(InputsPage.MultiSelectControlSelector)
                .Value("NL").In(InputsPage.MultiSelectControlSelector);
        }

        [Test]
        [Category(Category.SLOW)]
        public void MultiSelectClearOptionsBetweenSelections()
        {
            I.Select(Option.Value, "QC", "MB").From(InputsPage.MultiSelectControlSelector)
             .Assert.Text("Québec").In(InputsPage.MultiSelectControlSelector);

            I.Select(Option.Value, "AB").From(InputsPage.MultiSelectControlSelector)
             .Assert
                .Text("Alberta").In(InputsPage.MultiSelectControlSelector)
                .Text("Québec").Not.In(InputsPage.MultiSelectControlSelector);
        }
    }
}
