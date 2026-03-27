using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Buffers.Text;
using Xunit;

namespace Xunit.AutomationTests
{
    [Trait("Category", "FunctionalUI")]
    public class NavigationTest : BaseTest
    {
        [Theory]
        [InlineData("About", "/about", "About")]
        [InlineData("Admissions", "/admissions", "Admissions")]
        public void Verify_About_Page_Navigation(string linkText, string urlPart, string titlePart)
        {
            driver.Navigate().GoToUrl(BaseUrl);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            var aboutLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText(linkText)));
            aboutLink.Click();

            wait.Until(d => d.Url.Contains(urlPart));

            Assert.Contains(urlPart, driver.Url);
            Assert.Contains(titlePart, driver.Title);

            var header = wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("h1"))).Text;
            Assert.Contains(linkText, header);
        }
    }
}
