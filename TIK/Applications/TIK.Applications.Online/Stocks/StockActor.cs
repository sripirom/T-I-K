using System;
using Akka.Actor;
using TIK.Domain.TheSet;

namespace TIK.Applications.Online.Stocks
{
    public partial class StockActor : ReceiveActor
    {
        private readonly IEodRepository _eodRepository;
        private readonly String _symbol;
        public StockActor(string symbol, IEodRepository eodRepository)
        {
            _symbol = symbol;
            _eodRepository = eodRepository;

            Receive<GetStockInfo>(m =>
            {
                Sender.Tell(new Eod(){ Id = ""});
            });
        }
    }
}
