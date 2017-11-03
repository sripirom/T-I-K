using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TIK.ProcessService.Authentication;
using TIK.Applications.Membership;
using TIK.ProcessService.Membership.ActorSystems;
using System.IO;

namespace TIK.ProcessService.Membership
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
            
            var huconConfig = Path.Combine(Directory.GetCurrentDirectory(), "Hucon.txt");
            var config = HoconLoader.FromFile(huconConfig); 
            var actorSystem = ActorSystem.Create("MembershipSystem", config);
 
            var memberController =
                actorSystem.ActorSelection("akka.tcp://MembershipSystem@127.0.0.1:5301/user/MemberController")
                    .ResolveOne(TimeSpan.FromSeconds(3))
                    .Result;




            services.AddSingleton<ActorSystem>(_ => actorSystem);


            services.AddMembershipServices();
            services.AddRepositories();

            services.JwtBearerAuthentication(Configuration);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
