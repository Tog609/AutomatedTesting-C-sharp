using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace NUnit.AutomationTests
{
    public class SearchComponent
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private readonly By _searchBox = By.CssSelector("div.header-search");
        private readonly By _input = By.CssSelector("div.header-search form input");

        public SearchComponent(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        public void Search(string text)
        {
            var box = _driver.FindElement(_searchBox);

            new OpenQA.Selenium.Interactions.Actions(_driver)
                .MoveToElement(box)
                .Perform();

            var input = _wait.Until(ExpectedConditions.ElementIsVisible(_input));
            input.SendKeys(text + Keys.Enter);
        }
    }
}
