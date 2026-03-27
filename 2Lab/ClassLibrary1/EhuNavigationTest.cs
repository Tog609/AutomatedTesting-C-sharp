using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace EHU.AutomationTests
{
    public class NavigationTests : BaseTest
    {
        [Fact]
        public void Verify_About_Page_Navigation()
        {
            driver.Navigate().GoToUrl(BaseUrl);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            var aboutLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("About")));
            aboutLink.Click();

            wait.Until(d => d.Url.Contains("/about"));

            Assert.Contains("/about", driver.Url);
            Assert.Contains("About", driver.Title);

            var header = wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("h1"))).Text;
            Assert.Contains("About", header);
        }
    }
}