using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;

[Binding]
public class ReqnrollHooks
{
    public IWebDriver Driver { get; private set; }

    [BeforeScenario]
    public void BeforeScenario()
    {
        Driver = new ChromeDriver();
        Driver.Manage().Window.Maximize();
    }

    [AfterScenario]
    public void AfterScenario()
    {
        Driver?.Quit();
    }
}