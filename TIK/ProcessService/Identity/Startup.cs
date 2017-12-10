using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using TIK.Applications.Identity.Authentication;
using TIK.Applications.Identity.Authentication.Routes;
using TIK.Applications.Identity.JwtSecurity;
using TIK.Applications.Security;
using TIK.Core.Governance.ServiceDiscovery;
using TIK.Domain.UserAccounts;
using TIK.Persistance.ElasticSearch.Mocks;
using TIK.ProcessService.Activities;

namespace TIK.ProcessService.Identity
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.JwtBearerAuthentication(Configuration);

            services.AddTransient<IUserAccountQuery, UserAccountQuery>();
            services.AddTransient<IUserAccountRepository, MockUserAccountRepository>();

            services.AddAuthenticationServices();

            services.AddCors();

            // add controller at TIK.Applications.Identity
            services.AddMvc()
                    .AddApplicationPart(typeof(TokenController).GetTypeInfo().Assembly)
                    .AddApplicationPart(typeof(HealthCheckController).GetTypeInfo().Assembly)
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
            else
            {
                //app.UseExceptionHandler("/Home/Error");

                // Register a simple error handler to catch token expiries and change them to a 401, 
                // and return all other errors as a 500. This should almost certainly be improved for
                // a real application.
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Use(async (context, next) =>
                    {
                        var error = context.Features[typeof(IExceptionHandlerFeature)] as IExceptionHandlerFeature;
                        // This should be much more intelligent - at the moment only expired 
                        // security tokens are caught - might be worth checking other possible 
                        // exceptions such as an invalid signature.
                        if (error != null && error.Error is SecurityTokenExpiredException)
                        {
                            context.Response.StatusCode = 401;
                            // What you choose to return here is up to you, in this case a simple 
                            // bit of JSON to say you're no longer authenticated.
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsync(
                                JsonConvert.SerializeObject(
                                    new { authenticated = false, tokenExpired = true }));
                        }
                        else if (error != null && error.Error != null)
                        {
                            context.Response.StatusCode = 500;
                            context.Response.ContentType = "application/json";
                            // TODO: Shouldn't pass the exception message straight out, change this.
                            await context.Response.WriteAsync(
                                JsonConvert.SerializeObject
                                (new { success = false, error = error.Error.Message }));
                        }
                        // We're not trying to handle anything else so just let the default 
                        // handler handle.
                        else await next();
                    });
                });
            }

            //app.UseCors(builder =>
                        //builder.WithOrigins("http://localhost:5000"));

            app.UseAuthentication();

            app.UseMvc();

            // Autoregister using server.Features (does not work in reverse proxy mode)
            app.UseConsulRegisterService();
        }
    }
}
