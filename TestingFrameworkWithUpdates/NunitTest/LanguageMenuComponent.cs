using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace NUnit.AutomationTests
{
    public class LanguageMenuComponent
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private readonly By _menu = By.CssSelector("li[onclick]");
        private readonly By _russian = By.CssSelector("li[onclick] ul li:nth-child(1)");
        private readonly By _belarusian = By.CssSelector("li[onclick] ul li:nth-child(2)");
        private readonly By _lithuanian = By.CssSelector("li[onclick] ul li:nth-child(3)");

        public LanguageMenuComponent(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        private void OpenMenu()
        {
            var menu = _wait.Until(ExpectedConditions.ElementToBeClickable(_menu));
            new OpenQA.Selenium.Interactions.Actions(_driver)
                .MoveToElement(menu)
                .Perform();
        }

        public void Select(Language lang)
        {
            OpenMenu();

            By selector = lang switch
            {
                Language.Russian => _russian,
                Language.Belarusian => _belarusian,
                Language.Lithuanian => _lithuanian,
                _ => throw new ArgumentOutOfRangeException(nameof(lang))
            };

            _wait.Until(ExpectedConditions.ElementToBeClickable(selector)).Click();
        }
    }
}
public enum Language
{
    Russian,
    Belarusian,
    Lithuanian
}
