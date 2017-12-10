using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.Extensions.Logging;
using TIK.Domain.TheSet;
using TIK.Applications.Online.CommonStocks;
using Microsoft.AspNetCore.Mvc;
using TIK.Integration.Online;

namespace TIK.Applications.Online.CommonStocks.Routes
{
    public class AddDiscussion
    {

        private ILogger<GetCommonStocks> Logger { get; set; }
        private IActorRef CommonStockRouteActor { get; set; }
        private IStockDiscussionPublisher Pubisher { get; set; }

        public AddDiscussion(CommonStockRouteProvider provider, IStockDiscussionPublisher publisher, ILogger<GetCommonStocks> logger)
        {
            this.Logger = logger;
            this.CommonStockRouteActor = provider.Get();
            this.Pubisher = publisher;
        }

        public async Task<Boolean> Execute(int memberId, int stockId, DiscussionItem discussionItem)
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

                var res = await Pubisher.AddStockDiscussion(stockId, discussionItem);

                return await Task.FromResult(res);
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}
