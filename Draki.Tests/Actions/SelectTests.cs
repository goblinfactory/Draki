﻿using Draki.Exceptions;
using NUnit.Framework;

namespace Draki.Tests.Actions
{
    public class SelectTests : BaseTest
    {

        [Test]
        public void SelectValue()
        {
            InputsPage.Go();

            I.Select(Option.Value, "QC").From(InputsPage.SelectControlSelector)
             .Assert.Text("Québec").In(InputsPage.SelectControlSelector);
        }

        [Test]
        [Category(Category.SLOW)]
        public void SelectIndex()
        {
            InputsPage.Go();

            I.Select(3).From(InputsPage.SelectControlSelector)
             .Assert
                .Value("MB").In(InputsPage.SelectControlSelector)
                .Text("Manitoba").In(InputsPage.SelectControlSelector);
        }

        [Test]
        public void SelectText()
        {
            InputsPage.Go();

            I.Select("Québec").From(InputsPage.SelectControlSelector)
             .Assert.Value("QC").In(InputsPage.SelectControlSelector);
        }

        [Test]
        [Category(Category.SLOW)]
        public void SelectClearsOptionBetweenSelections()
        {
            InputsPage.Go();

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
            InputsPage.Go();

            var exception = Assert.Throws<FluentException>(() => I.Select("NonExistentText").From(InputsPage.SelectControlSelector));
            Assert.True(exception.InnerException.Message.Contains("NonExistentText"));
        }

        [Test]
        [Category(Category.VERYSLOW)]
        public void SelectValueFailed()
        {
            InputsPage.Go();

            var exception = Assert.Throws<FluentException>(() => I.Select(Option.Value, "NonExistentValue").From(InputsPage.SelectControlSelector));
            Assert.True(exception.InnerException.Message.Contains("NonExistentValue"));
        }

        [Test]
        public void SelectIndexFailed()
        {
            InputsPage.Go();

            var exception = Assert.Throws<FluentException>(() => I.Select(1000).From(InputsPage.SelectControlSelector));
            Assert.True(exception.InnerException.Message.Contains("1000"));
        }

        [Test]
        [Category(Category.SLOW)]
        public void MultiSelectValue()
        {
            InputsPage.Go();

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
            InputsPage.Go();

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
            InputsPage.Go();

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
            InputsPage.Go();

            I.Select(Option.Value, "QC", "MB").From(InputsPage.MultiSelectControlSelector)
             .Assert.Text("Québec").In(InputsPage.MultiSelectControlSelector);

            I.Select(Option.Value, "AB").From(InputsPage.MultiSelectControlSelector)
             .Assert
                .Text("Alberta").In(InputsPage.MultiSelectControlSelector)
                .Text("Québec").Not.In(InputsPage.MultiSelectControlSelector);
        }
    }
}
