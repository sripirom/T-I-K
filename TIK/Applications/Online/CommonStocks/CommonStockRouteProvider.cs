using System;
using Akka.Actor;
using TIK.Applications.Online.EodStocks;
using TIK.Domain.TheSet;

namespace TIK.Applications.Online.CommonStocks
{
    public class CommonStockRouteProvider
    {
        private IActorRef CommonStocksRouteActorInstance { get; set; }

        public CommonStockRouteProvider(ActorSystem actorSystem, string host)
        {

            this.CommonStocksRouteActorInstance = actorSystem.ActorSelection(string.Format("akka.tcp://OnlineSystem@{0}/user/commonStrockRoute", host))
                .ResolveOne(TimeSpan.FromSeconds(3))
                .Result;
        }

        public IActorRef Get()
        {
            return this.CommonStocksRouteActorInstance;
        }

        public static IActorRef CreateInstance(ActorSystem actorSystem, IActorRef commonStocksActor, IActorRef eodStocksActor)
        {

            return actorSystem.ActorOf(CommonStockRouteActor.Props(commonStocksActor, eodStocksActor), "commonStrockRoute");
        }
    }
}
