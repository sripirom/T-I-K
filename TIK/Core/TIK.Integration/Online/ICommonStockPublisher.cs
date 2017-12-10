using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TIK.Domain.TheSet;

namespace TIK.Integration.Online
{
    public interface ICommonStockPublisher
    {
        Task<IEnumerable<CommonStock>> GetList(int startIndex, int pageSize);
        Task<CommonStockInfo> GetInfo(int memberId, int stockId);
        Task<IEnumerable<DiscussionItem>> GetStockDiscussion(int memberId, int stockId);
        Task<Boolean> AddStockDiscussionItem(int memberId, int stockId, DiscussionItem discussionItem);
    }
}
 