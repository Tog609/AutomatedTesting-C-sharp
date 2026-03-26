using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using Xunit;

namespace EHU.AutomationTests
{
    public class SearchTests : BaseTest
    {
        [Fact]
        public void Verify_Search_Functionality()
        {
            driver.Navigate().GoToUrl(BaseUrl);

            var searcherbox = driver.FindElement(By.CssSelector("div.header-search"));

            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.MoveToElement(searcherbox).Perform();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var input = wait.Until(ExpectedConditions.ElementIsVisible(
                By.CssSelector("div.header-search form input")
            ));

            string text = "study programs";
            input.SendKeys(text + Keys.Enter);

            Assert.Equal("https://en.ehuniversity.lt/?s=study+programs", driver.Url);
        }
    }
}
