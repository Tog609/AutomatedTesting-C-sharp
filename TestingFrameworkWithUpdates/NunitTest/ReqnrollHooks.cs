using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;
using Serilog;

[Binding]
public class ReqnrollHooks
{
    public IWebDriver Driver { get; private set; }

    [BeforeScenario]
    public void BeforeScenario()
    {
        Driver = new ChromeDriver();
        Driver.Manage().Window.Maximize();

        Log.Information("BDD Scenario started");
    }

    [AfterScenario]
    public void AfterScenario()
    {
        Log.Information("BDD Scenario finished");
        Driver?.Quit();
    }
}
