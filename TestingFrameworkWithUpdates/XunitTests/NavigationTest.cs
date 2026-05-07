using Shouldly;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

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

            driver.Url.ShouldContain(urlPart);
            driver.Title.ShouldContain(titlePart);

            var header = wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("h1"))).Text;
            header.ShouldContain(linkText);
        }
    }
}
