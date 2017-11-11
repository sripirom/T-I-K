using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.Extensions.Logging;
using TIK.Domain.TheSet;

namespace TIK.Applications.Online.CommonStocks.Routes
{
    public class GetCommonStocks
    {

        private ILogger<GetCommonStocks> Logger { get; set; }
        private IActorRef CommonStocksActor { get; set; }

        public GetCommonStocks(CommonStocksProvider provider, ILogger<GetCommonStocks> logger)
        {
            this.Logger = logger;
            this.CommonStocksActor = provider.Get();
        }

        public async Task<IEnumerable<CommonStock>> Execute(int startIndex, int pageSize)
        {
            Logger.LogInformation("Retrieve commonstocks");
            return await this.CommonStocksActor.Ask<IEnumerable<CommonStock>>(
                new CommonStocksActor.GetCommonStocks{ StartIndex = startIndex, PageSize = pageSize });
        }
    }
}
