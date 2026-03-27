using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace Xunit.AutomationTests
{
    public class BaseTest : IDisposable
    {
        protected IWebDriver driver;

        protected const string BaseUrl = "https://en.ehu.lt/";

        public BaseTest()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}