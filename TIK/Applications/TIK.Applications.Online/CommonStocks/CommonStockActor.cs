using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Akka.Actor;
using TIK.Domain.TheSet;

namespace TIK.Applications.Online.CommonStocks
{
    public partial class CommonStockActor : ReceiveActor
    {

        public CommonStockInfo CommonStorkData { get; }
        public IList<DiscussionItem> Discussions { get; }
        
        public CommonStockActor(CommonStockInfo commonStork, IActorRef eodStocksActor)
        {
            CommonStorkData = commonStork;
            Discussions = new List<DiscussionItem>();


            Receive<GetStockInfo>(m =>
            {
                Sender.Tell(CommonStorkData);
            });

            Receive<GetDiscussion>(m =>{
                Sender.Tell(new ReadOnlyCollection<DiscussionItem>(Discussions));
            });

            Receive<AddDiscussion>(m => {
                var discussionItem = new DiscussionItem { 
                    Id = Discussions.Count +1,
                    UserName = m.UserName,
                     Comment = m.Comment,
                    EnteredOn = m.EnteredOn
                };
                Discussions.Add(discussionItem);
                Sender.Tell(new DiscussionAdded(discussionItem.Id));
            });
        }

        public static Props Props(CommonStockInfo commonStork, IActorRef eodStocksActor)
        {
            return Akka.Actor.Props.Create(() => new CommonStockActor(commonStork, eodStocksActor));
        }
    }
}
