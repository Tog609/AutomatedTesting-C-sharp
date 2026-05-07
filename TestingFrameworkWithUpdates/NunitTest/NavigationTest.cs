using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using Shouldly;


namespace NUnit.AutomationTests
{
    [Category("FunctionalUI")]
    public class NavigationTest : BaseTest
    {
        [TestCase("About", "/about", "About")]
        [TestCase("Admissions", "/admissions", "Admissions")]
        public void Verify_Navigation(string linkText, string urlPart, string titlePart)
        {
            var home = new HomePage(Driver);
            home.Open(BaseUrl);

            home.Navigation.ClickLink(linkText);

            Driver.Url.ShouldContain(urlPart);
            Driver.Title.ShouldContain(titlePart);

            var header = Driver.FindElement(By.TagName("h1")).Text;
            header.ShouldContain(titlePart);
        }
    }
}