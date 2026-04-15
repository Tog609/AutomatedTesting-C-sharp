using OpenQA.Selenium;
namespace NUnit.AutomationTests
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        public LanguageMenuComponent LanguageMenu { get; }
        public NavigationMenuComponent Navigation { get; }
        public SearchComponent Search { get; }

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
            LanguageMenu = new LanguageMenuComponent(driver);
            Navigation = new NavigationMenuComponent(driver);
            Search = new SearchComponent(driver);
        }

        public void Open(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }
    }
}
