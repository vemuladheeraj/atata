﻿using NUnit.Framework;
using OpenQA.Selenium;

namespace Atata.Tests
{
    [TestFixture]
    public class CheckBoxListTest : TestBase
    {
        private CheckBoxListPage page;

        protected override void OnSetUp()
        {
            page = GoTo<CheckBoxListPage>();
        }

        [Test]
        public void CheckBoxList_Enum()
        {
            page.ByIdAndLabel.VerifyEquals(CheckBoxListPage.Options.None);

            TestCheckBoxList(
                page.ByIdAndLabel,
                CheckBoxListPage.Options.OptionC | CheckBoxListPage.Options.OptionD,
                CheckBoxListPage.Options.OptionB);

            TestCheckBoxList(
                page.ByXPathAndValue,
                CheckBoxListPage.Options.OptionA,
                CheckBoxListPage.Options.OptionsDF);

            page.ByIdAndLabel.VerifyUnchecked(CheckBoxListPage.Options.OptionA);
            page.ByIdAndLabel.VerifyChecked(CheckBoxListPage.Options.OptionD | CheckBoxListPage.Options.OptionF);

            TestCheckBoxList(
                page.ByXPathAndValue,
                CheckBoxListPage.Options.None,
                CheckBoxListPage.Options.OptionA);

            page.ByIdAndLabel.Check(CheckBoxListPage.Options.OptionD);
            page.ByXPathAndValue.VerifyEquals(CheckBoxListPage.Options.OptionA | CheckBoxListPage.Options.OptionD);

            page.ByXPathAndValue.Uncheck(CheckBoxListPage.Options.OptionA);
            page.ByIdAndLabel.VerifyChecked(CheckBoxListPage.Options.OptionD);

            Assert.Throws<AssertionException>(() =>
                page.ByIdAndLabel.VerifyUnchecked(CheckBoxListPage.Options.OptionD));

            Assert.Throws<AssertionException>(() =>
                page.ByIdAndLabel.VerifyChecked(CheckBoxListPage.Options.OptionA));

            Assert.Throws<NoSuchElementException>(() =>
                page.ByIdAndLabel.Set(CheckBoxListPage.Options.MissingValue));
        }

        private void TestCheckBoxList<T>(CheckBoxList<T, CheckBoxListPage> list, T value1, T value2)
        {
            list.VerifyExists();
            list.Set(value1);
            list.VerifyEquals(value1);
            Assert.That(list.Get(), Is.EqualTo(value1));

            list.Set(value2);
            list.VerifyNotEqual(value1);
            list.VerifyEquals(value2);
            Assert.That(list.Get(), Is.EqualTo(value2));
        }
    }
}