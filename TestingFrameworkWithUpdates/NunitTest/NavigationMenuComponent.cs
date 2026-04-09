using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace NUnit.AutomationTests
{
    public class NavigationMenuComponent
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public NavigationMenuComponent(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        public void ClickLink(string linkText)
        {
            var link = _wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText(linkText)));
            link.Click();
        }
    }
}
