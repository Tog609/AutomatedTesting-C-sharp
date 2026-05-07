using Serilog;
using Serilog.Events;

namespace NUnit.AutomationTests.Infrastructure.Logging;

public static class LoggingConfig
{
    private static bool _initialized;

    public static void Configure()
    {
        if (_initialized)
            return;

        var logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        Directory.CreateDirectory(logDir);

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File(
                Path.Combine(logDir, "test-log-.txt"),
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: LogEventLevel.Debug)
            .CreateLogger();

        _initialized = true;
    }
}
