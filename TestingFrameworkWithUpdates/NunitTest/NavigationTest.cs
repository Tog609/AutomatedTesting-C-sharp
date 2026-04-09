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
        [TestCase("Admissions", "/admissions", "Admissions")]
        public void Verify_Navigation(string linkText, string urlPart, string titlePart)
        {
            var home = new HomePage(driver);
            home.Open(BaseUrl);

            home.Navigation.ClickLink(linkText);

            Assert.That(driver.Url, Does.Contain(urlPart));
            Assert.That(driver.Title, Does.Contain(titlePart));

            var header = driver.FindElement(By.TagName("h1")).Text;
            Assert.That(header, Does.Contain(titlePart));
        }
    }
}