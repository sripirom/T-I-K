using System;
using Akka.Actor;
using TIK.Applications.Messaging;
using TIK.Domain.TheSet;
using TIK.Applications.Online.EodStocks;
using Akka.Event;

namespace TIK.Applications.Online.CommonStocks
{
    public partial class CommonStockRouteActor : ReceiveActor
    {
        private readonly ILoggingAdapter _logger = Context.GetLogger();

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

        #region Lifecycle hooks

        protected override void PreStart()
        {
            _logger.Debug("CommonStockRouteActor PreStart");
        }

        protected override void PostStop()
        {
            _logger.Debug("CommonStockRouteActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            _logger.Debug("CommonStockRouteActor PreRestart because {Reason}", reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            _logger.Debug("CommonStockRouteActor PostRestart because {Reason}", reason);

            base.PostRestart(reason);
        }
        #endregion
    }


}
