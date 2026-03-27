using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Linq;

namespace NUnit.AutomationTests
{
    [Category("StaticUI")]
    public class ContactFormSubmissionTest : BaseTest
    {
        [Test] // Not sure what additional TestCase parameters can be added here since all required data on this page is static and does not vary.
        public void ContactInfo_IsDisplayed()
        {
            string contactUrl = "https://en.ehu.lt/contact/";
            driver.Navigate().GoToUrl(contactUrl);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("ul.wp-block-list")));

            var items = driver.FindElements(By.CssSelector("ul.wp-block-list li"));
            Assert.That(items.Count, Is.GreaterThanOrEqualTo(4));

            var email = driver.FindElement(By.CssSelector("a[href^='mailto:']"));
            Assert.That(email.Displayed, Is.True);
            Assert.That(email.Text, Does.Contain("franciskscarynacr@gmail.com"));

            var phoneLT = items.First(i => i.Text.Contains("LT"));
            Assert.That(phoneLT.Text, Does.Contain("+370 68 771365"));

            var phoneBY = items.First(i => i.Text.Contains("BY"));
            Assert.That(phoneBY.Text, Does.Contain("+375 29 5781488"));

            var social = items.First(i => i.Text.Contains("Join us"));
            Assert.That(social.Text, Does.Contain("Facebook"));
            Assert.That(social.Text, Does.Contain("Telegram"));
            Assert.That(social.Text, Does.Contain("VK"));
        }
    }
}