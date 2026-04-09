using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace Xunit.AutomationTests
{
    [Trait("Category", "FunctionalUI")]
    public class SearchTests : BaseTest
    {
        [Theory]
        [InlineData("study programs", "https://en.ehuniversity.lt/?s=study+programs")]
        [InlineData("Samal", "https://en.ehuniversity.lt/?s=Samal")]
        [InlineData("Rector", "https://en.ehuniversity.lt/?s=Rector")]
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

            Assert.Equal(expectedUrl, driver.Url);
        }
    }
}