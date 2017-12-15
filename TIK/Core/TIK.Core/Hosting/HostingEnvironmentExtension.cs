using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using TIK.Core.Logging;

namespace TIK.Core.Hosting
{
    public static class HostingEnvironmentExtension
    {
        public static IConfigurationBuilder InnitConfigurationHosting(this IHostingEnvironment env)
        {
            // Innitial 
            Log.Logger = DefaultLoggerConfiguration.Serilog(); 
            
            Log.Information("I:CreateConfigurationBuilder");

            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Log.Information("O:CreateConfigurationBuilder");

            return builder;
        } 


    }
}
