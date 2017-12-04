using System;
using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Consul;
using TIK.ProcessService.Activities;
using TIK.Core.Governance;

namespace TIK.ProcessService.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
                Console.Write(ex);
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
