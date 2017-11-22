using System;
using Akka.Actor;
using TIK.Domain.TheSet;

namespace TIK.Applications.Online.CommonStocks
{
    public partial class CommonStockActor : ReceiveActor
    {

        public CommonStockInfo CommonStorkData { get; }
        
        public CommonStockActor(CommonStockInfo commonStork, IActorRef eodStocksActor)
        {
            CommonStorkData = commonStork;


            Receive<GetStockInfo>(m =>
            {
                Sender.Tell(CommonStorkData);
            });
        }

        public static Props Props(CommonStockInfo commonStork, IActorRef eodStocksActor)
        {
            return Akka.Actor.Props.Create(() => new CommonStockActor(commonStork, eodStocksActor));
        }
    }
}
