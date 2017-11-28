using System;

namespace TIK.Applications.Online.CommonStocks
{
    public partial class CommonStockActor
    {
        public abstract class CommonStockEvent {}

        public class DiscussionAdded : CommonStockEvent
        {
            public readonly Int32 DiscussionId;

            public DiscussionAdded(Int32 discussionId = 0 )
            {
                this.DiscussionId = discussionId;
            }
        }

    }
}
