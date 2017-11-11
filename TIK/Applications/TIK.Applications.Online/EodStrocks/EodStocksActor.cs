using System;
using TIK.Domain.TheSet;
using Akka;
using Akka.Actor;

namespace TIK.Applications.Online.EodStrocks
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
    }
}
