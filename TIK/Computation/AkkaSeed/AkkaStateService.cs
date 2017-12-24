using System;
using System.IO;
using Akka.Actor;
using Microsoft.Extensions.DependencyInjection;
using TIK.Applications.Online;
using TIK.Applications.Online.BackLogs;
using TIK.Applications.Online.CommonStocks;
using TIK.Applications.Online.EodStocks;
using TIK.Applications.Online.Jobs;
using TIK.Applications.Online.Members;
using TIK.Core.Logging;
using TIK.Domain.Membership;
using TIK.Domain.TheSet;
using TIK.Integration.Batch;
using TIK.Integration.WebApi.Batch;
using TIK.Persistance.ElasticSearch;
using TIK.Persistance.ElasticSearch.Mocks;
using TIK.Persistance.ElasticSearch.Repositories;

namespace TIK.Computation.AkkaSeed
{
    public class AkkaStateService
    {
        ILog logger = LogProvider.GetLogger(typeof(AkkaStateService));

        private ActorSystem ActorSystemInstance;
        private IServiceCollection _services;

        public AkkaStateService(IServiceCollection services)
        {
            _services = services;
        }

        public void Start()
        {
            logger.Info("Start");
            try
            {


                var huconConfig = Path.Combine(Directory.GetCurrentDirectory(), EnvSettings.Instance().HuconFileName);
                var config = HoconLoader.FromFile(huconConfig);
                ActorSystemInstance = ActorSystem.Create(EnvSettings.Instance().ActorSystem, config);

                IMemberRepository memberRepository = new MockMemberRepository();


                var elasticsearchUrl = EnvSettings.Instance().ElasticsearchUrl;
                var rootIndex = EnvSettings.Instance().ElasticsearchIndexSet;
                var context = new EsContext(new Uri(elasticsearchUrl), rootIndex);
                ICommonStockRepository commonStockRepository = new CommonStockRepository(context);
                ICommonStockInfoRepository commonStockInfoRepository = new CommonStockInfoRepository(context);
                IEodRepository eodRepository = new EodRepository(context); 

                //IBatchPublisher batchPublisher = new BatchPublisher(new Uri(EnvSettings.Instance().BatchUrl));

                var memberController = MemberActorProvider.CreateInstance(ActorSystemInstance, memberRepository);
                //var jobsActorProvider = JobsActorProvider.CreateInstance(ActorSystemInstance, batchPublisher);
                //var backLogsActor = BackLogsActorProvider.CreateInstance(ActorSystemInstance, new JobsActorProvider(ActorSystemInstance, EnvSettings.Instance().AkkaAddress));
                var commonStocksActor = CommonStocksProvider.CreateInstance(ActorSystemInstance, commonStockRepository, commonStockInfoRepository);
                var eodStocksActor = EodStocksProvider.CreateInstance(ActorSystemInstance, eodRepository);
                var commonStockRouteActor = CommonStockRouteProvider.CreateInstance(ActorSystemInstance, commonStocksActor, eodStocksActor);

            
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Stop()
        {
            ActorSystemInstance.Terminate().Wait(TimeSpan.FromSeconds(2));
        }
    }
}
