using System;
using Akka.Actor;
using TIK.Domain.TheSet;

namespace TIK.Applications.Online.CommonStocks
{
    public class CommonStocksProvider
    {
        private IActorRef CommonStocksActorInstance { get; set; }
         
        public CommonStocksProvider(ActorSystem actorSystem, string host)
        {

            this.CommonStocksActorInstance = actorSystem.ActorSelection(string.Format("akka.tcp://OnlineSystem@{0}/user/commonStrocks", host))
                .ResolveOne(TimeSpan.FromSeconds(3))
                .Result;
        }

        public IActorRef Get()
        {
            return this.CommonStocksActorInstance;
        }

        public static IActorRef CreateInstance(ActorSystem actorSystem, ICommonStockRepository commonStockRepository, 
                                               ICommonStockInfoRepository commonStockInfoRepository)
        {
            return actorSystem.ActorOf(CommonStocksActor.Props(commonStockRepository, commonStockInfoRepository), "commonStrocks");
        }
    }
} 