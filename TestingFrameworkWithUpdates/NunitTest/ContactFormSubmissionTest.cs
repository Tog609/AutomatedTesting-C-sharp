using NUnit.Framework;
using Shouldly;


namespace NUnit.AutomationTests
{
    [Category("StaticUI")]
    public class ContactFormSubmissionTest : BaseTest
    {
        [Test]
        public void ContactInfo_IsDisplayed()
        {
            var contact = new ContactPage(Driver);
            contact.Open();

            contact.GetItemsCount().ShouldBeGreaterThanOrEqualTo(4);
            contact.GetEmail().ShouldContain("franciskscarynacr@gmail.com");
            contact.GetPhoneByCountry("LT").ShouldContain("+370 68 771365");
            contact.GetPhoneByCountry("BY").ShouldContain("+375 29 5781488");

            var social = contact.GetSocialBlock();
            social.ShouldContain("Facebook");
            social.ShouldContain("Telegram");
            social.ShouldContain("VK");
        }
    }
}
