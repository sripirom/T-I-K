using System;
using System.Threading.Tasks;
using TIK.Domain.TheSet;

namespace TIK.Integration.Online
{
    public interface IStockDiscussionPublisher
    {
        Task<Boolean> AddStockDiscussion(Int32 stockId, DiscussionItem discussionItem);
    }
}
