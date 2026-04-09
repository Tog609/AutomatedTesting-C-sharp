using NUnit.Framework;

namespace NUnit.AutomationTests
{
    [Category("StaticUI")]
    public class ContactFormSubmissionTest : BaseTest
    {
        [Test]
        public void ContactInfo_IsDisplayed()
        {
            var contact = new ContactPage(driver);
            contact.Open();

            Assert.That(contact.GetItemsCount(), Is.GreaterThanOrEqualTo(4));

            Assert.That(contact.GetEmail(), Does.Contain("franciskscarynacr@gmail.com"));

            Assert.That(contact.GetPhoneByCountry("LT"), Does.Contain("+370 68 771365"));
            Assert.That(contact.GetPhoneByCountry("BY"), Does.Contain("+375 29 5781488"));

            var social = contact.GetSocialBlock();
            Assert.That(social, Does.Contain("Facebook"));
            Assert.That(social, Does.Contain("Telegram"));
            Assert.That(social, Does.Contain("VK"));
        }
    }
}
