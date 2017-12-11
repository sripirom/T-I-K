using System;
using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace TIK.ProcessService.Identity
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
