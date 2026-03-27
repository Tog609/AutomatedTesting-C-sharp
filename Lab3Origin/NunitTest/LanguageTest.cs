using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace NUnit.AutomationTests
{
    [Category("FunctionalUI")]
    public class LanguageTests : BaseTest
    {
        [TestCase("li[onclick] ul li:nth-child(3)", "https://lt.ehuniversity.lt/")]
        [TestCase("li[onclick] ul li:nth-child(2)", "https://be.ehuniversity.lt/")]
        [TestCase("li[onclick] ul li:nth-child(1)", "https://ru.ehuniversity.lt/")]
        public void TestLanguageSwitcher(string selector, string expectedUrl)
        {
            driver.Navigate().GoToUrl(BaseUrl);

            var languageSwitcher = driver.FindElement(By.CssSelector("li[onclick]"));

            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.MoveToElement(languageSwitcher).Perform();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var input = wait.Until(
                ExpectedConditions.ElementIsVisible(
                    By.CssSelector(selector)
                )
            );

            input.Click();

            Assert.That(driver.Url, Is.EqualTo(expectedUrl));
        }
    }
}