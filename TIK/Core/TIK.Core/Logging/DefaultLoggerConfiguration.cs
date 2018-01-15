using System;
using Serilog;

namespace TIK.Core.Logging
{
    public class DefaultLoggerConfiguration
    {
        public DefaultLoggerConfiguration()
        {
        }

        public static Serilog.ILogger Serilog()
        {
            var logTemplate = Environment.GetEnvironmentVariable("LOG_TEMPLATE");

            return  new Serilog.LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.LiterateConsole()
                .WriteTo.RollingFile(logTemplate)
                .CreateLogger();

        }
    }
}
