using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Xunit.AutomationTests 
{
    public sealed class DriverSingleton
    {
        private static IWebDriver _driver;

        private DriverSingleton() { }

        public static IWebDriver GetDriver()
        {
            if (_driver == null)
            {
                _driver = new ChromeDriver();
                _driver.Manage().Window.Maximize();
            }

            return _driver;
        }

        public static void Quit()
        {
            _driver?.Quit();
            _driver = null;
        }
    }

}
