using System;
using Akka.Actor;
using TIK.Domain.TheSet;

namespace TIK.Applications.Online.EodStocks 
{
    public class EodStocksProvider
    {
        private IActorRef EodStockActorInstance { get; set; }
         
        public EodStocksProvider(ActorSystem actorSystem, string host)
        {

            this.EodStockActorInstance = actorSystem.ActorSelection(string.Format("akka.tcp://OnlineSystem@{0}/user/eodStocks", host))
                .ResolveOne(TimeSpan.FromSeconds(3))
                .Result;
        }

        public IActorRef Get()
        {
            return this.EodStockActorInstance;
        }

        public static IActorRef CreateInstance(ActorSystem actorSystem, IEodRepository eodRepository)
        {
            return actorSystem.ActorOf(EodStocksActor.Props(eodRepository), "eodStocks");
        }
    }
} 