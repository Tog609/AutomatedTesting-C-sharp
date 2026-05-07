using Shouldly;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace Xunit.AutomationTests
{
    [Trait("Category", "FunctionalUI")]
    public class LanguageTests : BaseTest
    {
        [Theory]
        [InlineData("li[onclick] ul li:nth-child(3)", "https://lt.ehuniversity.lt/")]
        [InlineData("li[onclick] ul li:nth-child(2)", "https://be.ehuniversity.lt/")]
        [InlineData("li[onclick] ul li:nth-child(1)", "https://ru.ehuniversity.lt/")]
        public void TestLanguageSwitcher(string selector, string resultUrl)
        {
            driver.Navigate().GoToUrl(BaseUrl);

            var languageSwitcher = driver.FindElement(By.CssSelector("li[onclick]"));

            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.MoveToElement(languageSwitcher).Perform();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var input = wait.Until(ExpectedConditions.ElementIsVisible(
                By.CssSelector(selector)
            ));

            input.Click();

            driver.Url.ShouldBe(resultUrl);
        }
    }
}
