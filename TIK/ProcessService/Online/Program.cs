using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TIK.Core.Governance;
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
                BuildWebHost(args).Start();

                var client = new ConsulProvider(EnvSettings.Instance().IP.ToString(),
                                    EnvSettings.Instance().Port);
                client.Start();

                Console.WriteLine("DataService started...");
                Console.WriteLine("Press ESC to exit");

                while (Console.Read() != (int)ConsoleKey.Escape)
                {
                }

                client.Stop();
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseKestrel(options =>
                      {
                          options.Listen(EnvSettings.Instance().IP, EnvSettings.Instance().Port);
                      })
                .UseStartup<Startup>()
                .Build();
    }
}
