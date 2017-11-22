using System;
using TIK.Domain.TheSet;
using Akka;
using Akka.Actor;

namespace TIK.Applications.Online.EodStocks
{
    public partial class EodStocksActor : ReceiveActor 
    {
        
        private readonly IEodRepository _eodRepository;

        public EodStocksActor(IEodRepository eodRepository)
        {
            _eodRepository = eodRepository;

            Receive<RetriveBetween>(m =>
            {
                Sender.Tell(new Eod() { Id = "" });
            });
        }

        public static Props Props(IEodRepository eodRepository)
        {
            return Akka.Actor.Props.Create(() => new EodStocksActor(eodRepository));
        }
    }
}
