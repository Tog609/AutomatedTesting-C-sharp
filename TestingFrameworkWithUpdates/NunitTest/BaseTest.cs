using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Serilog;

namespace NUnit.AutomationTests
{
    public class BaseTest
    {
        protected IWebDriver Driver { get; private set; } = null!;
        protected ExtentTest? Report;

        private DateTime _startTime;

        protected const string BaseUrl = "https://en.ehu.lt/";

        [SetUp]
        public void Initialize()
        {
            _startTime = DateTime.Now;

            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();

            var testName = TestContext.CurrentContext.Test.Name;
            Report = TestRunHooks.Extent?.CreateTest(testName);

            Log.Information("=== Test {TestName} started ===", testName);
            Report?.Info("Test started");
        }

        [TearDown]
        public void TearDown()
        {
            var result = TestContext.CurrentContext.Result;
            var status = result.Outcome.Status;

            var durationMs = (DateTime.Now - _startTime).TotalMilliseconds;

            switch (status)
            {
                case TestStatus.Passed:
                    Report?.Pass("PASSED");
                    Log.Information("Test PASSED");
                    break;

                case TestStatus.Failed:
                    Report?.Fail(result.Message ?? "Unknown error");
                    Log.Error(result.Message ?? "Unknown error");
                    break;

                case TestStatus.Skipped:
                    Report?.Skip(result.Message ?? "Skipped");
                    Log.Warning("Test SKIPPED: {Reason}", result.Message ?? "No reason");
                    break;
            }

            Report?.Info($"Execution time: {durationMs} ms");
            Report?.Info($"Timestamp: {DateTime.Now:O}");

            Driver?.Dispose();
        }
    }
}
