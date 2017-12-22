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
using TIK.Applications.Online.EodStocks;
using TIK.Integration.Online;
using TIK.Integration.SignalR.Online;
using TIK.Integration;
using System.Text.RegularExpressions;
using DnsClient;
using System.Net;
using System.Linq;

namespace TIK.ProcessService.Online
{
    public static class Services
    {
        public static void AddServiceCollection(this IServiceCollection services)
        {
            var dnsPublishAddress = EnvSettings.Instance().ConsulDnsPublishAddress;
            var baseDomain = EnvSettings.Instance().ConsulDnsBaseDomain;
            var websignalServiceName = EnvSettings.Instance().WebSignalRServiceName;

            var baseDns = new BaseDnsDiscovery(baseDomain);

            if (!Regex.IsMatch(dnsPublishAddress, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b"))
            {
                var address = Dns.GetHostAddresses(dnsPublishAddress).LastOrDefault();
                if (address != null) 
                {
                    dnsPublishAddress = address.ToString();
                }
            }
            var endpointDiscovery = new EndpointDiscovery
                                (new LookupClient(IPAddress.Parse(dnsPublishAddress),
                                 EnvSettings.Instance().ConsulDnsPort)
                                { UseTcpOnly = true }, baseDns);

            services.AddSingleton<EndpointDiscovery>(_ => endpointDiscovery);


            services.AddTransient<IMemberRepository, MockMemberRepository>();
             
            services.AddSingleton<IBatchPublisher>(_=> new BatchPublisher(new Uri(EnvSettings.Instance().BatchUrl)));

            services.AddSingleton<IStockDiscussionPublisher>(_=> new StockDiscussionPublisher(websignalServiceName, endpointDiscovery));
        }

        public static void AddActorSystem(this IServiceCollection services)
        {
            string host = EnvSettings.Instance().AkkaAddress;

            var huconConfig = Path.Combine(Directory.GetCurrentDirectory(), EnvSettings.Instance().HuconFileName);
            var config = HoconLoader.FromFile(huconConfig);

            var actorSystem = ActorSystem.Create(EnvSettings.Instance().ActorSystem, config);
            var memberActorProvider = new MemberActorProvider(actorSystem, host);
            //var backLogsActorProvider = new BackLogsActorProvider(actorSystem, host);
            //var jobsActorProvider = new JobsActorProvider(actorSystem, host);
            var commonStocksProvider = new CommonStocksProvider(actorSystem, host);
            var eodStocksProvider = new EodStocksProvider(actorSystem, host);
            var commonStockRouteProvider = new CommonStockRouteProvider(actorSystem, host);

            services.AddSingleton(typeof(ActorSystem), actorSystem);


            services.AddSingleton<MemberActorProvider>(_ => memberActorProvider);
            //services.AddSingleton<BackLogsActorProvider>(_ => backLogsActorProvider);
            //services.AddSingleton<JobsActorProvider>(_ => jobsActorProvider);
            services.AddSingleton<CommonStocksProvider>(_ => commonStocksProvider);
            services.AddSingleton<EodStocksProvider>(_ => eodStocksProvider);
            services.AddSingleton<CommonStockRouteProvider>(_ => commonStockRouteProvider);

            services.AddMemberServices();
            //services.AddJobServices();
            //services.AddBackLogServices();
            services.AddCommonStockServices();
            services.AddEodServices();
        }
    }
}
