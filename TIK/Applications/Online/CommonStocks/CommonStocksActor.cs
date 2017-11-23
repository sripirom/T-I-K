using System;
using System.Collections.ObjectModel;
using System.Linq;
using Akka.Actor;
using TIK.Domain.TheSet;

namespace TIK.Applications.Online.CommonStocks
{
    public partial class CommonStocksActor : ReceiveActor
    {
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


    }
}
