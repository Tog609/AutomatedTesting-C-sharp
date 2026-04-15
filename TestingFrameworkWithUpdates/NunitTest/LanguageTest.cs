using NUnit.Framework;

namespace NUnit.AutomationTests
{
    [Category("FunctionalUI")]
    public class LanguageTests : BaseTest
    {
        [TestCase(Language.Lithuanian, "https://lt.ehuniversity.lt/")]
        [TestCase(Language.Belarusian, "https://be.ehuniversity.lt/")]
        [TestCase(Language.Russian, "https://ru.ehuniversity.lt/")]
        public void TestLanguageSwitcher(Language lang, string expectedUrl)
        {
            var home = new HomePage(Driver);
            home.Open(BaseUrl);

            home.LanguageMenu.Select(lang);

            Assert.That(Driver.Url, Is.EqualTo(expectedUrl));
        }
    }
}
