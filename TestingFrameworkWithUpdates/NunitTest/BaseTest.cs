using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace NUnit.AutomationTests
{
    public class BaseTest
    {
        protected IWebDriver Driver { get; private set; }

        protected const string BaseUrl = "https://en.ehu.lt/";

        [SetUp]
        public void Initialize()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            Driver?.Dispose();
        }
    }
}