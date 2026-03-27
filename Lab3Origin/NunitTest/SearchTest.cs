using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace NUnit.AutomationTests
{
    [Category("FunctionalUI")]
    public class SearchTests : BaseTest
    {
        [TestCase("study programs", "https://en.ehuniversity.lt/?s=study+programs")]
        [TestCase("Samal", "https://en.ehuniversity.lt/?s=Samal")]
        [TestCase("Rector", "https://en.ehuniversity.lt/?s=Rector")]
        public void Verify_Search_Functionality(string text, string expectedUrl)
        {
            driver.Navigate().GoToUrl(BaseUrl);

            var searcherbox = driver.FindElement(By.CssSelector("div.header-search"));

            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.MoveToElement(searcherbox).Perform();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var input = wait.Until(
                ExpectedConditions.ElementIsVisible(
                    By.CssSelector("div.header-search form input")
                )
            );

            input.SendKeys(text + Keys.Enter);

            Assert.That(driver.Url, Is.EqualTo(expectedUrl));
        }
    }
}