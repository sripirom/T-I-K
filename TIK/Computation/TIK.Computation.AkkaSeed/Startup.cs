using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;

namespace TIK.Computation.AkkaSeed
{
    public class Startup
    {
        private static AkkaStateService ActorSystemInstance;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                  .MinimumLevel.Verbose()
                  .WriteTo.LiterateConsole()
                  .WriteTo.RollingFile("logs\\log-{Date}.txt")
                  .CreateLogger();
            
            ActorSystemInstance = new AkkaStateService();
            services.AddSingleton(typeof(AkkaStateService), ActorSystemInstance);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();


            ActorSystemInstance.Start();
        }
    }
}
