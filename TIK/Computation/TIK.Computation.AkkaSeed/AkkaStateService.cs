using System;
using System.IO;
using Akka.Actor;
using TIK.Applications.Online;
using TIK.Applications.Online.BackLogs;
using TIK.Applications.Online.CommonStocks;
using TIK.Applications.Online.EodStocks;
using TIK.Applications.Online.Jobs;
using TIK.Applications.Online.Members;
using TIK.Domain.Membership;
using TIK.Domain.TheSet;
using TIK.Integration.Batch;
using TIK.Integration.WebApi.Batch;
using TIK.Persistance.ElasticSearch.Mocks;

namespace TIK.Computation.AkkaSeed
{
    public class AkkaStateService
    {
        private ActorSystem ActorSystemInstance;

        public void Start()
        {
            try
            {

                var huconConfig = Path.Combine(Directory.GetCurrentDirectory(), EnvSettings.Instance().HuconFileName);
                var config = HoconLoader.FromFile(huconConfig);
                ActorSystemInstance = ActorSystem.Create(EnvSettings.Instance().ActorSystem, config);

                IMemberRepository memberRepository = new MockMemberRepository();
                ICommonStockRepository commonStockRepository = new MockCommonStockRepository();
                ICommonStockInfoRepository commonStockInfoRepository = new MockCommonStockInfoRepository();
                IEodRepository eodRepository = new MockEodRepository();

                IBatchPublisher batchPublisher = new BatchPublisher(new Uri(EnvSettings.Instance().BatchUrl));

                var memberController = MemberActorProvider.CreateInstance(ActorSystemInstance, memberRepository);
                //var jobsActorProvider = JobsActorProvider.CreateInstance(ActorSystemInstance, batchPublisher);
                //var backLogsActor = BackLogsActorProvider.CreateInstance(ActorSystemInstance, new JobsActorProvider(ActorSystemInstance, host));
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
