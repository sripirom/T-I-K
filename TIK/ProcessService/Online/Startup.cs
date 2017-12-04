using System;
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
using TIK.Applications.Online.Members.Routes;
using TIK.Applications.Security;
using TIK.ProcessService.Activities;

namespace TIK.ProcessService.Online
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
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
                    .AddControllersAsServices();
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
        }
    }
}
