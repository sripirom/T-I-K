using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using TIK.ProcessService.Activities;

namespace TIK.ProcessService.Online
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task.Delay(15000).Wait();
            try
            {
                BuildWebHost(args).Run();

            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseKestrel(options => {
                   options.Listen(EnvSettings.Instance().IP, EnvSettings.Instance().Port);
               }).ConfigureLogging((hostingContext, logging) =>
               {
                   Log.Logger = new LoggerConfiguration()
                   .MinimumLevel.Verbose()
                   .WriteTo.LiterateConsole()
                   .WriteTo.RollingFile("logs/log-{Date}.txt")
                   .CreateLogger();
             
               })
                .UseStartup<Startup>()
                .Build();
    }
}
