using Serilog;
using System;
using Serilog.Sinks.SystemConsole;

namespace ApiTesting;

public static class LoggerConfig
{
    public static void ConfigureLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(
                "logs/test-log-.txt",
                rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }
}