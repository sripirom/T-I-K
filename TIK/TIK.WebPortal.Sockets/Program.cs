using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TIK.WebPortal.Sockets
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                               .AddCommandLine(args)
                               .Build();
            try
            {
                BuildWebHost(config, args).Run();
            }
            catch (Exception ex)
            {
                throw ex;
            }
         
        }

        public static IWebHost BuildWebHost(IConfigurationRoot config, string[] args)
        {

            return WebHost.CreateDefaultBuilder(args)
                            .UseConfiguration(config)
                            .UseSetting(WebHostDefaults.PreventHostingStartupKey, "true")
                            .ConfigureLogging(factory =>
                            {
                             factory.AddConsole();
                            })
                            .UseKestrel()
                            .UseContentRoot(Directory.GetCurrentDirectory())
                            .UseIISIntegration()
                            .UseStartup<Startup>()
                            .Build();
        }
    }
}
