using Shouldly;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Linq;

namespace Xunit.AutomationTests
{
    [Trait("Category", "StaticUI")]
    public class ContactFormSubmissionTest : BaseTest
    {
        [Fact]
        public void ContactInfo_IsDisplayed()
        {
            string contactUrl = "https://en.ehuniversity.lt/contact/";
            driver.Navigate().GoToUrl(contactUrl);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            var list = wait.Until(
                ExpectedConditions.ElementIsVisible(By.CssSelector("ul.wp-block-list"))
            );

            var items = driver.FindElements(By.CssSelector("ul.wp-block-list li"));
            items.Count.ShouldBeGreaterThanOrEqualTo(4);

            var email = driver.FindElement(By.CssSelector("a[href^='mailto:']"));
            email.Displayed.ShouldBeTrue();
            email.Text.ShouldContain("franciskscarynacr@gmail.com");

            var phoneLT = items.First(i => i.Text.Contains("LT"));
            phoneLT.Text.ShouldContain("+370 68 771365");

            var phoneBY = items.First(i => i.Text.Contains("BY"));
            phoneBY.Text.ShouldContain("+375 29 5781488");

            var social = items.First(i => i.Text.Contains("Join us"));
            social.Text.ShouldContain("Facebook");
            social.Text.ShouldContain("Telegram");
            social.Text.ShouldContain("VK");
        }
    }
}
