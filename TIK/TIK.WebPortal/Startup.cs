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

namespace TIK.WebPortal
{
    public class Startup
    {
        const string TokenAudience = "ExampleAudience";
        const string TokenIssuer = "ExampleIssuer";

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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options => {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,

                     ValidIssuer = "Fiver.Security.Bearer",
                     ValidAudience = "Fiver.Security.Bearer",
                     IssuerSigningKey = JwtSecurityKey.Create("fiver-secret-key")
                 };

                 options.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = context =>
                     {
                         Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                         return Task.CompletedTask;
                     },
                     OnTokenValidated = context =>
                     {
                         Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                         return Task.CompletedTask;
                     }
                 };
             });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Member",
                    policy => policy.RequireClaim("MembershipId"));
            });


            services.AddServiceCollection();
        

            services.AddMvc();

            services.AddServiceDiscovery(Configuration.GetSection("ServiceDiscovery"));

        

    //Now register our services with Autofac container
            /*
   
            var builder = new ContainerBuilder();
            //builder.RegisterType<UserAccountRepository>().As<IUserAccountRepository>();
            //builder.RegisterType<UserAccountQuery>().As<IUserAccountQuery>();
            builder.RegisterInstance(new ElasticClient(new ConnectionSettings(new Uri("http://192.168.99.100:32809"))
                          .DefaultIndex("member"))).As<IElasticClient>();
            
            builder.Populate(services);
            var container = builder.Build();
            //Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(container);
            */
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
        }
    }


    public static class JwtSecurityKey
    {
        public static SymmetricSecurityKey Create(string secret)
        {
            try
            {
                return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
