using System;
using Akka.Actor;
using TIK.Domain.TheSet;

namespace TIK.Applications.Online.Stocks
{
    public partial class StockActor : ReceiveActor
    {
        private readonly IEodRepository _eodRepository;
        private readonly string _symbol;
        private decimal _Open;
        private decimal _Close;
        private decimal _High;
        private decimal _Low;
        private long _volumn;

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
