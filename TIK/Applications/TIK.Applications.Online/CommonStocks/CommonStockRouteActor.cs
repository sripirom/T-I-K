using System;
using Akka.Actor;
using TIK.Applications.Messaging;
using TIK.Domain.TheSet;

namespace TIK.Applications.Online.CommonStocks
{
    public partial class CommonStockRouteActor : ReceiveActor
    {
        private IActorRef CommonStocksActor { get; set; }
        private IActorRef EodStocksActor { get; set; }

        public CommonStockRouteActor(IActorRef commonStocksActor, IActorRef eodStocksActor)
        {
            CommonStocksActor = commonStocksActor;
            EodStocksActor = eodStocksActor;

            ReceiveAny(m => {
                if (m is MessageWithStockSymbol)
                {
                    
                    var envelope = m as MessageWithStockSymbol;
                    IActorRef backLogActor;
                    if(Context.Child(envelope.Symbol.ToString()) is Nobody)
                    {
                        var task = this.CommonStocksActor.Ask<CommonStock>(new CommonStocksActor.RetriveCommonStock() { Symbol = envelope.Symbol });
                        var commonStock = task.Result;
                        backLogActor = Context.ActorOf(CommonStockActor.Props(commonStock, this.EodStocksActor), envelope.MemberId.ToString());
                       
                    }else{
                        backLogActor = Context.Child(envelope.MemberId.ToString());  
                    }
                                         
                    backLogActor.Forward(m);
                }
            });

        }
    }


}
