using System;
using System.Collections.ObjectModel;
using System.Linq;
using Akka.Actor;
using Akka.Event;
using TIK.Domain.TheSet;

namespace TIK.Applications.Online.CommonStocks
{
    public partial class CommonStocksActor : ReceiveActor
    {
        private readonly ILoggingAdapter _logger = Context.GetLogger();

        private ICommonStockRepository _commonStrockRepository;
        private ICommonStockInfoRepository _commonStockInfoRepository;
         
        public CommonStocksActor(ICommonStockRepository commonStrockRepository,
                                 ICommonStockInfoRepository commonStockInfoRepository)
        {
            _commonStrockRepository = commonStrockRepository;
            _commonStockInfoRepository = commonStockInfoRepository;

            Receive<GetCommonStocks>(m =>
            {
                var commonStocks = _commonStrockRepository.List().Skip(m.StartIndex).Take(m.PageSize);

                Sender.Tell(new ReadOnlyCollection<CommonStock>(commonStocks.ToList()));
            });

            Receive<RetriveCommonStock>(m => 
            {
                var stockInfo = _commonStockInfoRepository.Get(m.StockId);
                Sender.Tell(stockInfo);
            });

        }

        public static Props Props(ICommonStockRepository commonStrockRepository, ICommonStockInfoRepository commonStockInfoRepository)
        {
            return Akka.Actor.Props.Create(() => new CommonStocksActor(commonStrockRepository, commonStockInfoRepository));
        }


        #region Lifecycle hooks

        protected override void PreStart()
        {
            _logger.Debug("CommonStocksActor PreStart");
        }

        protected override void PostStop()
        {
            _logger.Debug("CommonStocksActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            _logger.Debug("CommonStocksActor PreRestart because {Reason}", reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            _logger.Debug("CommonStocksActor PostRestart because {Reason}", reason);

            base.PostRestart(reason);
        }
        #endregion
    }
}
