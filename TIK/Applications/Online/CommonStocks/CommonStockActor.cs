using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Akka.Actor;
using Akka.Event;
using TIK.Domain.TheSet;

namespace TIK.Applications.Online.CommonStocks
{
    public partial class CommonStockActor : ReceiveActor
    { 
        private readonly ILoggingAdapter _logger = Context.GetLogger();

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


        #region Lifecycle hooks

        protected override void PreStart()
        {
            _logger.Debug("CommonStockActor PreStart");
        }

        protected override void PostStop()
        {
            _logger.Debug("CommonStockActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            _logger.Debug("CommonStockActor PreRestart because {Reason}", reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            _logger.Debug("CommonStockActor PostRestart because {Reason}", reason);

            base.PostRestart(reason);
        }
        #endregion
    }
}
