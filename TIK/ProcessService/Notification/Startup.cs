using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Hangfire;
using Hangfire.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using TIK.Applications.Notification.Commands.Socket;
using TIK.Core.Container;
using TIK.ProcessService.Authentication;

namespace TIK.ProcessService.Notification
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
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvcCore()
                   .AddJsonFormatters();

            var connectionString = Configuration.GetConnectionString("hangfire.redis");
            //GlobalConfiguration.Configuration.UseRedisStorage(connectionString);
            services.AddHangfire(configuration => configuration.UseRedisStorage(connectionString));

    
            
            services.JwtBearerAuthentication(Configuration);
            services.AddMvc();

        

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Batch API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


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

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            app.UseMvc();

            //app.UseHangfireServer();
            UseHangfireServer(app);
          
        }

        private void UseHangfireServer(IApplicationBuilder app)
        {
            var options = new BackgroundJobServerOptions
            {
                ServerName = "ProcessServiceNotification",
                ShutdownTimeout = TimeSpan.FromMinutes(30),
                WorkerCount = Math.Max(Environment.ProcessorCount, 20),
                Queues = new string[] {"default", "signalrnoti", "smsnoti", "emailnoti" }
            };

            UseAutofac();
       
            app.UseHangfireDashboard();
            app.UseHangfireServer(options);


            //it seems a bug that progress bar in Hangfire.Console cannot dispaly for a long time when using redis storage.


 
        }

        private void UseAutofac()
        {
            var builder = new ContainerBuilder();

            //builder.RegisterType<DataTransformPublisher>()
                   //.As<IDataTransformPublisher>();
            /*
            builder.Register(icomponentcontext =>  
                             new BackgroundJobClient(new Hangfire.Redis.RedisStorage(Configuration.GetConnectionString("hangfire.redis"))))
                   .As<IBackgroundJobClient>().InstancePerLifetimeScope();
*/
            //builder.RegisterType<SignalRNotificationCommand>().As<ISignalRNotificationCommand>().InstancePerLifetimeScope();
                

            builder.RegisterModule(new DelegateModule(() => new Assembly[]
            {
                typeof(ISignalRNotificationCommand).GetTypeInfo().Assembly
            }));

            var container = builder.Build();

            GlobalConfiguration.Configuration.UseAutofacActivator(container);
        }
    }
}
