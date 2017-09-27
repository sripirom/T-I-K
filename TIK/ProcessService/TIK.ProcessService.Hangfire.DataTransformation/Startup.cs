using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.Swagger.Model;
using TIK.ProcessService.Hangfire.DataTransformation.Hubs;

namespace TIK.ProcessService.Hangfire.DataTransformation
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
            services.AddMvc();

            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc();

            services.AddSignalR();

            var connectionString = Configuration.GetConnectionString("hangfire.redis");
            GlobalConfiguration.Configuration.UseRedisStorage(connectionString);

            services.AddSwaggerGen();

            var xmlPath = GetXmlCommentsPath();

            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Hangfire Samples APIs",
                    Description = "The unified entry for hangfire invocation to add background job to queues.",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "TIK", Url = "https://github.com/sripirom/TIK" }
                });

                options.IncludeXmlComments(xmlPath);

                //options.DescribeAllEnumsAsStrings();
            });
        }
        private string GetXmlCommentsPath()
        {
            var app = PlatformServices.Default.Application;
            return Path.Combine(app.ApplicationBasePath, "TIK.ProcessService.Hangfire.DataTransformation.xml");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSignalR(routes =>
            {
                routes.MapHub<DataTransformationHUB>("DataTransformation");
            });

            app.UseMvc();

 
        }
    }
}
