using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using NUnit.Framework;

namespace NUnit.AutomationTests
{
    public class BaseTest
    {
        protected IWebDriver driver;

        protected const string BaseUrl ="https://en.ehu.lt/";

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }
        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}