using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace NUnit.AutomationTests
{
    [Category("FunctionalUI")]
    public class NavigationTest : BaseTest
    {
        [TestCase("About", "/about", "About")]
        [TestCase("Admissions","/admissions","Admissions")]
        public void Verify_About_Page_Navigation(string linkText, string urlPart, string titlePart)
        {
            driver.Navigate().GoToUrl(BaseUrl);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            var aboutLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText(linkText)));
            aboutLink.Click();

            wait.Until(d => d.Url.Contains(urlPart));

            Assert.That(driver.Url, Does.Contain(urlPart));
            Assert.That(driver.Title, Does.Contain(titlePart));

            var header = wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("h1"))).Text;
            Assert.That(header, Does.Contain(titlePart));
        }
    }
}