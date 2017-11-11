using System;
using Microsoft.Extensions.DependencyInjection;
using TIK.Domain.Membership;
using TIK.Integration.Batch;
using TIK.Persistance.ElasticSearch.Repositories;
using TIK.Integration.WebApi.Batch;
using TIK.Applications.Online;
using System.IO;
using Akka.Actor;
using TIK.Persistance.ElasticSearch.Mocks;
using TIK.Applications.Online.Members;
using TIK.Applications.Online.BackLogs;
using TIK.Applications.Online.Jobs;
using TIK.Applications.Online.Members.Routes;
using TIK.Applications.Online.CommonStocks;

namespace TIK.ProcessService.Online
{
    public static class Services
    {
        public static void AddServiceCollection(this IServiceCollection services)
        {
            services.AddTransient<IMemberRepository, MockMemberRepository>();
             
            services.AddSingleton<IBatchPublisher>(_=> new BatchPublisher(new Uri("http://localhost:5102")));
        }

        public static void AddActorSystem(this IServiceCollection services)
        {
            string host = @"127.0.0.1:5301";
            var huconConfig = Path.Combine(Directory.GetCurrentDirectory(), "Hucon.txt");
            var config = HoconLoader.FromFile(huconConfig);

            var actorSystem = ActorSystem.Create("OnlineSystem", config);
            var memberActorProvider = new MemberActorProvider(actorSystem, host);
            var backLogsActorProvider = new BackLogsActorProvider(actorSystem, host);
            var jobsActorProvider = new JobsActorProvider(actorSystem, host);
            var commonStocksProvider = new CommonStocksProvider(actorSystem, host);

            services.AddSingleton(typeof(ActorSystem), actorSystem);


            services.AddSingleton<MemberActorProvider>(_ => memberActorProvider);
            services.AddSingleton<BackLogsActorProvider>(_ => backLogsActorProvider);
            services.AddSingleton<JobsActorProvider>(_ => jobsActorProvider);
            services.AddSingleton<CommonStocksProvider>(_ => commonStocksProvider);

            services.AddMemberServices();
            services.AddJobServices();
            services.AddBackLogServices();
            services.AddCommonStockServices();
        }
    }
}
