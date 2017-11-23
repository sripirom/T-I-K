using System;
using Akka.Actor;
using TIK.Applications.Messaging;
using TIK.Domain.TheSet;
using TIK.Applications.Online.EodStocks;

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
                if (m is MessageWithStockId)
                {
                    
                    var envelope = m as MessageWithStockId;
                    IActorRef backLogActor;
                    if(Context.Child(envelope.StockId.ToString()) is Nobody)
                    {
                        var task = this.CommonStocksActor.Ask<CommonStockInfo>(new CommonStocksActor.RetriveCommonStock() { StockId = envelope.StockId });
                        var commonStock = task.Result;
                        backLogActor = Context.ActorOf(CommonStockActor.Props(commonStock, this.EodStocksActor), envelope.StockId.ToString());
                       
                    }else{
                        backLogActor = Context.Child(envelope.StockId.ToString());  
                    }
                                         
                    backLogActor.Forward(m);
                }
            });

        }

        public static Props Props(IActorRef commonStocksActor, IActorRef eodStocksActor)
        {
     
            return Akka.Actor.Props.Create(() => new CommonStockRouteActor(commonStocksActor, eodStocksActor));
        }
    }


}
