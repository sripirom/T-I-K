using System;
using TIK.Domain.TheSet;
using Akka;
using Akka.Actor;
using Akka.Event;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Collections.ObjectModel;

namespace TIK.Applications.Online.EodStocks
{
    public partial class EodStocksActor : ReceiveActor 
    {
        private readonly ILoggingAdapter _logger = Context.GetLogger();

        private readonly IEodRepository _eodRepository;

        public EodStocksActor(IEodRepository eodRepository)
        {
            _eodRepository = eodRepository;

            Receive<RetriveBetween>(m =>
            {
                var commonStocks = _eodRepository.SearchDateRange(m.Symbol, m.StartDate, m.EndDate, 1000).ToList();

                Sender.Tell(new ReadOnlyCollection<Eod>(commonStocks));

            });
        }

        public static Props Props(IEodRepository eodRepository)
        {
            return Akka.Actor.Props.Create(() => new EodStocksActor(eodRepository));
        }

        #region Lifecycle hooks

        protected override void PreStart()
        {
            _logger.Debug("EodStocksActor PreStart");
        }

        protected override void PostStop()
        {
            _logger.Debug("EodStocksActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            _logger.Debug("EodStocksActor PreRestart because {Reason}", reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            _logger.Debug("EodStocksActor PostRestart because {Reason}", reason);

            base.PostRestart(reason);
        }
        #endregion
    }
}
