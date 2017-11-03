using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Autofac;
using Hangfire;
using TIK.Applications.DataTransformation.Commands.FixedLength;
using Autofac.Extensions.DependencyInjection;
using TIK.Core.Container;
using TIK.Applications.Batch.Commands.SearchNews;

namespace TIK.Governance
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
            // Add framework services.
            //services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc();

            services.AddHangfire(x =>
            {
                var connectionString = Configuration.GetConnectionString("hangfire.redis");
                x.UseRedisStorage(connectionString);
            });

            //return RegisterAutofac(services);
        }

        private IServiceProvider RegisterAutofac(IServiceCollection services)
        {
          
            var builder = new ContainerBuilder();

            //builder.Populate(services);

            builder.RegisterModule(new DelegateModule(() => new Assembly[]
            {
                typeof(IFixedLengthCommand).GetTypeInfo().Assembly,
                typeof(ISearchNewsCommand).GetTypeInfo().Assembly

            }));

            var container = builder.Build();

            GlobalConfiguration.Configuration.UseAutofacActivator(container);

            return new AutofacServiceProvider(container);
        }
     
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging")); 
            //loggerFactory.AddDebug(); 

            //app.UseApplicationInsightsRequestTelemetry();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseHangfireDashboard();
        }
           
    }
}
