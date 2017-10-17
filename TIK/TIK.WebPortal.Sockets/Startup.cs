using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TIK.WebPortal.Sockets.EndPoints;
using TIK.WebPortal.Sockets.Hubs;

namespace TIK.WebPortal.Sockets
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
            try
            {
                // Regis Endpoint
                services.AddSockets();
                services.AddEndPoint<MessagesEndPoint>();
                services.AddOptions();


                services.AddSignalR();
                // .AddRedis();

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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            try
            {
                app.UseFileServer();

                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                app.UseCors("Everything");

                app.UseSignalR(routes =>
                {

                    routes.MapHub<DynamicChat>("dynamic");
                    routes.MapHub<Chat>("default");
                    routes.MapHub<Streaming>("streaming");
                    routes.MapHub<HubTChat>("hubT");

                });

                app.UseSockets(routes =>
                {
                    routes.MapEndPoint<MessagesEndPoint>("chat");
                });
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
