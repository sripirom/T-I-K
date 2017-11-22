using System;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.Extensions.Logging;
using TIK.Domain.TheSet;

namespace TIK.Applications.Online.CommonStocks.Routes
{
    public class GetStockInfo
    {
        private ILogger<GetCommonStocks> Logger { get; set; }
        private IActorRef CommonStockRouteActor { get; set; }

        public GetStockInfo(CommonStockRouteProvider provider, ILogger<GetCommonStocks> logger)
        {
            this.Logger = logger;
            this.CommonStockRouteActor = provider.Get();
        }

        public async Task<CommonStockInfo> Execute(int memberId, int stockId)
        {
            Logger.LogInformation("Retrieve commonstocks");
            return await this.CommonStockRouteActor.Ask<CommonStockInfo>(
                new CommonStockActor.GetStockInfo(memberId, stockId));
        }
    }
}
