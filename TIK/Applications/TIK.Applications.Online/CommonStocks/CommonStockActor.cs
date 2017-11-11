using System;
using Akka.Actor;
using TIK.Domain.TheSet;

namespace TIK.Applications.Online.CommonStocks
{
    public partial class CommonStockActor : ReceiveActor
    {

        public CommonStock CommonStork { get; }
        /*
        private decimal _Open;
        private decimal _Close;
        private decimal _High;
        private decimal _Low;
        private long _volumn;
*/

        public CommonStockActor(CommonStock commonStork, IActorRef eodStocksActor)
        {
            CommonStork = commonStork;


            Receive<GetStockInfo>(m =>
            {
                //Sender.Tell(new CommonStockInfo(){ Id = ""});
            });
        }

        public static Props Props(CommonStock commonStork, IActorRef eodStocksActor)
        {
            return Akka.Actor.Props.Create(() => new CommonStockActor(commonStork, eodStocksActor));
        }
    }
}
