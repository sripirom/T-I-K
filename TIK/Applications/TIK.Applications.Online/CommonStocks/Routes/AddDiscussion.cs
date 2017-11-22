using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.Extensions.Logging;
using TIK.Domain.TheSet;
using TIK.Applications.Online.CommonStocks;
using Microsoft.AspNetCore.Mvc;

namespace TIK.Applications.Online.CommonStocks.Routes
{
    public class AddDiscussion
    {

        private ILogger<GetCommonStocks> Logger { get; set; }
        private IActorRef CommonStockRouteActor { get; set; }

        public AddDiscussion(CommonStockRouteProvider provider, ILogger<GetCommonStocks> logger)
        {
            this.Logger = logger;
            this.CommonStockRouteActor = provider.Get();
        }

        public async Task<DiscussionItem> Execute(int memberId, int stockId, DiscussionItem discussionItem)
        {

            Logger.LogInformation($"Adding {discussionItem.Comment} of stock '{stockId}'");
            var result = await this.CommonStockRouteActor.Ask<CommonStockActor.CommonStockEvent>(new CommonStockActor.AddDiscussion(
                memberId,
                stockId,
                discussionItem.UserName,
                discussionItem.Comment,
                discussionItem.EnteredOn
            ));

            if (result is CommonStockActor.DiscussionAdded)
            {
                var e = result as CommonStockActor.DiscussionAdded;
                discussionItem.Id = e.DiscussionId;
                return await Task.FromResult(discussionItem);
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}
