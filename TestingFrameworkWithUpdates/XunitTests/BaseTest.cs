using OpenQA.Selenium;
using Xunit;

namespace Xunit.AutomationTests
{
    public class BaseTest : IDisposable
    {
        protected IWebDriver driver;

        protected const string BaseUrl = "https://en.ehu.lt/";
        public BaseTest()
        {
            driver = DriverSingleton.GetDriver();
        }

        public void Dispose()
        {
            DriverSingleton.Quit();
        }
    }
}

