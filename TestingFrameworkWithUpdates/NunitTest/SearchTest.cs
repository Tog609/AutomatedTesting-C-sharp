using NUnit.Framework;

namespace NUnit.AutomationTests
{
    [Category("FunctionalUI")]
    public class SearchTests : BaseTest
    {
        [TestCase("study programs", "https://en.ehuniversity.lt/?s=study+programs")]
        [TestCase("Samal", "https://en.ehuniversity.lt/?s=Samal")]
        [TestCase("Rector", "https://en.ehuniversity.lt/?s=Rector")]
        public void Verify_Search_Functionality(string text, string expectedUrl)
        {
            var home = new HomePage(driver);
            home.Open(BaseUrl);

            home.Search.Search(text);

            Assert.That(driver.Url, Is.EqualTo(expectedUrl));
        }
    }
}
