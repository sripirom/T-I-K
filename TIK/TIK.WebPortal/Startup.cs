using System;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using TIK.Persistance.ElasticSearch.Repositories;
using Nest;
using DnsClient;
using System.Net;
using TIK.Core.ServiceDiscovery;
using TIK.Core.Hosting;
using Serilog;
using TIK.Applications.Security;
using TIK.Core.Logging;

namespace TIK.WebPortal
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
            Log.Information("I:ConfigureServices");
            services.JwtBearerAuthentication(Configuration);

            services.AddServiceCollection();
        

            services.AddMvc();

            services.AddServiceDiscovery(Configuration.GetSection("ServiceDiscovery"));

            Log.Information("O:ConfigureServices");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Log.Information("I:Configure ApplicationBuilder");
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

            app.RegisterApplicationStopping(OnShutdown);

             
            app.UseAuthentication();
           
            app.UseStaticFiles();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Autoregister using server.Features (does not work in reverse proxy mode)
            app.UseConsulRegisterService();
            Log.Information("O:Configure ApplicationBuilder");
        }

        public void OnShutdown()
        {
            Log.Information("OnShutdown");
        } 
    }

}
