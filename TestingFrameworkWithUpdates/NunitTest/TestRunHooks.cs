using NUnit.Framework;
using Serilog;
using AventStack.ExtentReports;
using NUnit.AutomationTests.Infrastructure.Logging;
using NUnit.AutomationTests.Infrastructure.Reporting;

namespace NUnit.AutomationTests
{
    [SetUpFixture]
    public class TestRunHooks
    {
        public static ExtentReports? Extent;

        [OneTimeSetUp]
        public void GlobalSetUp()
        {
            LoggingConfig.Configure();
            Log.Information("Test run started");

            Extent = Reporter.GetReporter();
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            Log.Information(" Test run finished ");
            Extent?.Flush();
            Log.CloseAndFlush();
        }
    }
}
