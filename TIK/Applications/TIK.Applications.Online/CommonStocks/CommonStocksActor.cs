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
         
        public CommonStocksActor(ICommonStockRepository commonStrockRepository)
        {
            _commonStrockRepository = commonStrockRepository;

            Receive<GetCommonStocks>(m =>
            {
                var commonStocks = _commonStrockRepository.List().Skip(m.StartIndex).Take(m.PageSize);

                Sender.Tell(new ReadOnlyCollection<CommonStock>(commonStocks.ToList()));
            });

        }

        public static Props Props(ICommonStockRepository commonStrockRepository)
        {
            return Akka.Actor.Props.Create(() => new CommonStocksActor(commonStrockRepository));
        }


    }
}
