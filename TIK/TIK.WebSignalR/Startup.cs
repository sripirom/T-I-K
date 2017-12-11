using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TIK.Core.ServiceDiscovery;
using TIK.WebSignalR.Controllers;
using TIK.WebSignalR.EndPoints;
using TIK.WebSignalR.Hubs;
using TIK.WebSignalR.Users;

namespace TIK.WebSignalR
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            

            if (env.IsDevelopment())
            {
                try
                {
                    // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                    builder.AddUserSecrets<Startup>();
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
           
            }

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            //services.AddDbContext<ApplicationDbContext>(options =>
                //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddIdentity<ApplicationUser, IdentityRole>()
                //.AddEntityFrameworkStores<ApplicationDbContext>()
                //.AddDefaultTokenProviders();

            // Add Sockets Endpoint
            services.AddSockets();
            services.AddEndPoint<MessagesEndPoint>();
            services.AddOptions();

            services.AddMvc()
                    .AddApplicationPart(typeof(HealthCheckController).GetTypeInfo().Assembly)
                    .AddControllersAsServices();

            // To use Redis scaleout uncomment .AddRedis and uncomment Redis related lines below for presence
            services.AddSignalR()
                //.AddRedis()
                ;
            services.AddAuthentication().AddCookie();

            services.AddSingleton(typeof(DefaultHubLifetimeManager<>), typeof(DefaultHubLifetimeManager<>));
            services.AddSingleton(typeof(HubLifetimeManager<>), typeof(DefaultPresenceHublifetimeManager<>));
            services.AddSingleton(typeof(IUserTracker<>), typeof(InMemoryUserTracker<>));

            //services.AddSingleton(typeof(RedisHubLifetimeManager<>), typeof(RedisHubLifetimeManager<>));
            //services.AddSingleton(typeof(HubLifetimeManager<>), typeof(RedisPresenceHublifetimeManager<>));
            //services.AddSingleton(typeof(IUserTracker<>), typeof(RedisUserTracker<>));

            // Allow for Sockets
            services.AddCors(o =>
            {
                o.AddPolicy("Everything", p =>
                {
                    p.AllowAnyHeader()
                     .AllowAnyMethod()
                     .AllowAnyOrigin();
                });
            });

     
            services.AddServiceDiscovery(Configuration.GetSection("ServiceDiscovery"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            // Sockets
            //app.UseCors("Everything");
            app.UseCors(builder =>
                        builder.WithOrigins(EnvSettings.Instance().WebPortalUrl)
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("chat");
                routes.MapHub<StockDiscussionHub>("stockDiscussion");
            });

            app.UseSockets(routes =>
            {
                routes.MapEndPoint<MessagesEndPoint>("chat/ws");
            });

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
}
