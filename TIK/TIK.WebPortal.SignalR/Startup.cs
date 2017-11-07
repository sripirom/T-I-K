using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TIK.WebPortal.SignalR.EndPoints;
using TIK.WebPortal.SignalR.Hubs;
using TIK.WebPortal.SignalR.Services;
using TIK.WebPortal.SignalR.Users;

namespace TIK.WebPortal.SignalR
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

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

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
            app.UseCors("Everything");

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("chat");
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


        }
    }
}
