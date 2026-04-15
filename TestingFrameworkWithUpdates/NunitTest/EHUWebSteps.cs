using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;

namespace NUnit.AutomationTests.BDD
{
    [Binding]
    public class EhuWebSteps
    {
        private readonly IWebDriver _driver;
        public EhuWebSteps(ReqnrollHooks hooks)
        {
            _driver = hooks.Driver;
        }

        [Given("I open the home page")]
        public void GivenIOpenTheHomePage()
        {
            var home = new HomePage(_driver);
            home.Open("https://en.ehu.lt/");
        }

        [When(@"I click the navigation link ""(.*)""")]
        public void WhenIClickNavigationLink(string linkText)
        {
            var home = new HomePage(_driver);
            home.Navigation.ClickLink(linkText);
        }

        [Then(@"the page URL should contain ""(.*)""")]
        public void ThenUrlShouldContain(string urlPart)
        {
            Assert.That(_driver.Url, Does.Contain(urlPart));
        }

        [Then(@"the page title should contain ""(.*)""")]
        public void ThenTitleShouldContain(string titlePart)
        {
            Assert.That(_driver.Title, Does.Contain(titlePart));
        }

        [Then(@"the page header should contain ""(.*)""")]
        public void ThenHeaderShouldContain(string titlePart)
        {
            var header = _driver.FindElement(By.TagName("h1")).Text;
            Assert.That(header, Does.Contain(titlePart));
        }

        [When(@"I search for ""(.*)""")]
        public void WhenISearchFor(string query)
        {
            var home = new HomePage(_driver);
            home.Search.Search(query);
        }

        [Then(@"the search results URL should be ""(.*)""")]
        public void ThenSearchUrlShouldBe(string expectedUrl)
        {
            Assert.That(_driver.Url, Is.EqualTo(expectedUrl));
        }

        [Given("I open the contact page")]
        public void GivenIOpenContactPage()
        {
            var contact = new ContactPage(_driver);
            contact.Open();
        }

        [Then("the contact list should contain at least 4 items")]
        public void ThenContactListShouldContainAtLeast4()
        {
            var contact = new ContactPage(_driver);
            Assert.That(contact.GetItemsCount(), Is.GreaterThanOrEqualTo(4));
        }

        [Then(@"the email should contain ""(.*)""")]
        public void ThenEmailShouldContain(string email)
        {
            var contact = new ContactPage(_driver);
            Assert.That(contact.GetEmail(), Does.Contain(email));
        }

        [Then(@"the phone for country ""(.*)"" should contain ""(.*)""")]
        public void ThenPhoneShouldContain(string country, string phone)
        {
            var contact = new ContactPage(_driver);
            Assert.That(contact.GetPhoneByCountry(country), Does.Contain(phone));
        }

        [Then("the social block should contain:")]
        public void ThenSocialBlockShouldContain(Table table)
        {
            var contact = new ContactPage(_driver);
            var social = contact.GetSocialBlock();

            foreach (var row in table.Rows)
                Assert.That(social, Does.Contain(row[0]));
        }

        [When(@"I change language to ""(.*)""")]
        public void WhenIChangeLanguage(string lang)
        {
            var home = new HomePage(_driver);

            var language = lang switch
            {
                "Russian" => Language.Russian,
                "Belarusian" => Language.Belarusian,
                "Lithuanian" => Language.Lithuanian,
                _ => throw new ArgumentException("Unknown language")
            };

            home.LanguageMenu.Select(language);
        }

        [Then("the page should reload in Lithuanian")]
        public void ThenPageReloadsInLithuanian()
        {
            Assert.That(_driver.Url, Does.Contain("https://lt.ehuniversity.lt/"));
        }
    }
}
