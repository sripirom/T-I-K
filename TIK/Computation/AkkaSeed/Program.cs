using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TIK.Core.Governance;

namespace TIK.Computation.AkkaSeed
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
                   .UseKestrel(options=>{
                        options.Listen(EnvSettings.Instance().IP,EnvSettings.Instance().Port);
                    })
                .UseStartup<Startup>()
                .Build();
    }
}
