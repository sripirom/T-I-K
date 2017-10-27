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
using TIK.ProcessService.Authentication;
using Swashbuckle.Swagger.Model;

namespace TIK.ProcessService.Batch
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

            services.JwtBearerAuthentication(Configuration);
            services.AddMvc();
        }

        private string GetXmlCommentsPath()
        {
            var app = PlatformServices.Default.Application;
            return Path.Combine(app.ApplicationBasePath, "TIK.ProcessService.Batch.xml");
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

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
