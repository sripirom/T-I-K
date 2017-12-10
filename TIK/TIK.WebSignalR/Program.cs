using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TIK.WebSignalR
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
                .UseSetting(WebHostDefaults.PreventHostingStartupKey, "true")
                .ConfigureLogging((context, factory) =>
                {
                    try
                    {
                        factory.AddConfiguration(context.Configuration.GetSection("Logging"));
                        factory.AddConsole();
                        factory.AddDebug();
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                    }
                 
                })
                .UseKestrel(options =>
                   {
                       options.Listen(EnvSettings.Instance().IP, EnvSettings.Instance().Port);
                   })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
    }
}
