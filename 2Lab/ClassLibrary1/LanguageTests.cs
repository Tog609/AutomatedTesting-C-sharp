using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace EHU.AutomationTests
{
    public class LanguageTests : BaseTest
    {
        [Fact]
        public void TestLanguageSwticher()
        {
            driver.Navigate().GoToUrl(BaseUrl);

            var languageSwitcher = driver.FindElement(By.CssSelector("li[onclick]"));

            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.MoveToElement(languageSwitcher).Perform();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var input = wait.Until(ExpectedConditions.ElementIsVisible(
                By.CssSelector("li[onclick] ul li:nth-child(3)")
            ));

            input.Click();

            Assert.Equal("https://lt.ehuniversity.lt/", driver.Url);
        }
    }
}