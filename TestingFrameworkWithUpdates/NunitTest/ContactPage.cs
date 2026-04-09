using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Linq;
using System;

namespace NUnit.AutomationTests
{
    public class ContactPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private readonly By _list = By.CssSelector("ul.wp-block-list");
        private readonly By _items = By.CssSelector("ul.wp-block-list li");
        private readonly By _email = By.CssSelector("a[href^='mailto:']");

        public ContactPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        public void Open()
        {
            _driver.Navigate().GoToUrl("https://en.ehu.lt/contact/");
            _wait.Until(ExpectedConditions.ElementIsVisible(_list));
        }

        public int GetItemsCount()
        {
            return _driver.FindElements(_items).Count;
        }

        public string GetEmail()
        {
            return _driver.FindElement(_email).Text;
        }

        public string GetPhoneByCountry(string countryCode)
        {
            var items = _driver.FindElements(_items);
            return items.First(i => i.Text.Contains(countryCode)).Text;
        }

        public string GetSocialBlock()
        {
            var items = _driver.FindElements(_items);
            return items.First(i => i.Text.Contains("Join us")).Text;
        }
    }
}