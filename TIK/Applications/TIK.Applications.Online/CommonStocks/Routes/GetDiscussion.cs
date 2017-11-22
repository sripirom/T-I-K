using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.Extensions.Logging;
using TIK.Domain.TheSet;

namespace TIK.Applications.Online.CommonStocks.Routes
{
    public class GetDiscussion
    {

        private ILogger<GetDiscussion> Logger { get; set; }
        private IActorRef CommonStockRouteActor { get; set; }

        public GetDiscussion(CommonStockRouteProvider provider, ILogger<GetDiscussion> logger)
        {
            this.Logger = logger;
            this.CommonStockRouteActor = provider.Get();
        }

        public async Task<IEnumerable<DiscussionItem>> Execute(int memberId, int stockId)
        {
            Logger.LogInformation("Get Stock Discussions");
            return await this.CommonStockRouteActor.Ask<IEnumerable<DiscussionItem>>(
                new CommonStockActor.GetDiscussion(memberId, stockId));
        }
    }
}
