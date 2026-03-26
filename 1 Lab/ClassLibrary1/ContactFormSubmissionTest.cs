
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace EHU.AutomationTests
{
    public class ContactFormSubmissionTest : BaseTest
    {
        [Fact]
        public void ContactInfo_IsDisplayed()
        {
            string contactUrl = "https://en.ehu.lt/contact/";
            driver.Navigate().GoToUrl(contactUrl);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            var list = wait.Until(
                ExpectedConditions.ElementIsVisible(By.CssSelector("ul.wp-block-list"))
            );

            var items = driver.FindElements(By.CssSelector("ul.wp-block-list li"));
            Assert.True(items.Count >= 4);

            var email = driver.FindElement(By.CssSelector("a[href^='mailto:']"));
            Assert.True(email.Displayed);
            Assert.Contains("franciskscarynacr@gmail.com", email.Text);

            var phoneLT = items.First(i => i.Text.Contains("LT"));
            Assert.Contains("+370 68 771365", phoneLT.Text);

            var phoneBY = items.First(i => i.Text.Contains("BY"));
            Assert.Contains("+375 29 5781488", phoneBY.Text);

            var social = items.First(i => i.Text.Contains("Join us"));
            Assert.Contains("Facebook", social.Text);
            Assert.Contains("Telegram", social.Text);
            Assert.Contains("VK", social.Text);
        }
    }
}