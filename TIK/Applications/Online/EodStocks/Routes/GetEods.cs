using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.Extensions.Logging;
using TIK.Domain.TheSet;

namespace TIK.Applications.Online.EodStocks.Routes
{
    public class GetEods
    {
        private ILogger<GetEods> Logger { get; set; }
        private IActorRef EodStocksActor { get; set; }

        public GetEods(EodStocksProvider provider, ILogger<GetEods> logger)
        {
            Logger = logger;
            EodStocksActor = provider.Get();
        }

        public async Task<IEnumerable<Eod>> Execute(string symbol, DateTime  startDate, DateTime endDate)
        {  
            Logger.LogInformation("Retrieve stock eods.");
            return await this.EodStocksActor.Ask<IEnumerable<Eod>>(
                new EodStocksActor.RetriveBetween() { Symbol = symbol, StartDate = startDate, EndDate = endDate });
        }
    }
}