﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TIK.Applications.Online;
using TIK.Applications.Online.EodStocks.Routes;
using TIK.Applications.Online.Members.Routes;
using TIK.Core.Application.Security;
using TIK.Core.Hosting;
using TIK.Core.ServiceDiscovery;
using TIK.ProcessService.Activities;

namespace TIK.ProcessService.Online
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = env.InnitConfigurationHosting();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvcCore()
                    .AddJsonFormatters();


            services.AddServiceCollection();

            services.AddActorSystem();

            services.JwtBearerAuthentication(Configuration);

            // add controller at TIK.Applications.Online
            services.AddMvc()
                    .AddApplicationPart(typeof(MemberController).GetTypeInfo().Assembly)
                    .AddApplicationPart(typeof(HealthCheckController).GetTypeInfo().Assembly) 
                    .AddApplicationPart(typeof(EodController).GetTypeInfo().Assembly)
                    .AddControllersAsServices();

            services.AddServiceDiscovery(Configuration.GetSection("ServiceDiscovery"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvc();

            // Autoregister using server.Features (does not work in reverse proxy mode)
            app.UseConsulRegisterService();
        }
    }
}
