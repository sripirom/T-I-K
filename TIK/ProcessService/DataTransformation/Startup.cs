using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using TIK.ProcessService.DataTransformation.Hubs;

namespace TIK.ProcessService.DataTransformation
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
            //services.AddMvc();

            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc();

            services.AddSignalR();

            var connectionString = Configuration.GetConnectionString("hangfire.redis");
            GlobalConfiguration.Configuration.UseRedisStorage(connectionString);

   

            // Swagger
            // https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?tabs=visual-studio
           // var xmlPath = GetXmlCommentsPath();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Hangfire Samples APIs",
                    Description = "The unified entry for hangfire invocation to add background job to queues.",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "TIK", Url = "https://github.com/sripirom/TIK" }
                });
                //options.IncludeXmlComments(xmlPath);

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
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DataTransformation API V1");
            });

            //app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseSignalR(routes =>
            {
                routes.MapHub<DataTransformationHUB>("DataTransformation");
            });

            app.UseMvc();

 
        }
    }
}
