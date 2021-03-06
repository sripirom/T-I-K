﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DnsClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using TIK.Core.Hosting;
using TIK.Core.Logging;
using TIK.Core.ServiceDiscovery;

namespace TIK.Computation.AkkaSeed
{
    public class Startup
    {
        ILog logger = LogProvider.GetLogger(typeof(Startup));

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            /*
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();

            Configuration = builder.Build();
            */
            Configuration = configuration;

          
        }

        public IConfiguration Configuration { get; }

        public AkkaStateService ActorSystemInstance { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ActorSystemInstance = new AkkaStateService(services);

            services.AddSingleton(typeof(AkkaStateService), ActorSystemInstance);

            services.AddMvc();

            services.AddServiceDiscovery(Configuration.GetSection("ServiceDiscovery"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            logger.Info("I:Configure");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.RegisterApplicationStopping(OnShutdown);

            ActorSystemInstance.Start();

            app.UseMvc();



            // Autoregister using server.Features (does not work in reverse proxy mode)
            app.UseConsulRegisterService();
            logger.Info("O:Configure");
       
        }

        private void OnShutdown()
        {
            logger.Info("OnShutdown");
            ActorSystemInstance.Stop();
        }

    }
}
