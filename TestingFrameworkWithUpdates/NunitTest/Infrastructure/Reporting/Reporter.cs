using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace NUnit.AutomationTests.Infrastructure.Reporting;

public static class Reporter
{
    private static ExtentReports? _extent;

    public static ExtentReports GetReporter()
    {
        if (_extent != null)
            return _extent;

        var reportDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
        Directory.CreateDirectory(reportDir);

        var reportPath = Path.Combine(reportDir, $"TestReport_{DateTime.Now:yyyyMMdd_HHmmss}.html");
        var html = new ExtentHtmlReporter(reportPath);

        _extent = new ExtentReports();
        _extent.AttachReporter(html);

        return _extent;
    }
}
