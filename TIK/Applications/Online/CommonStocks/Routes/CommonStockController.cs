using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TIK.Domain.TheSet;

namespace TIK.Applications.Online.CommonStocks.Routes
{
    [Route("/CommonStock")]
    public class CommonStockController : Controller
    {
        private GetCommonStocks GetCommonStocks { get; }
        private GetStockInfo GetStockInfo { get; }
        private GetDiscussion GetDiscussion { get; }
        private AddDiscussion AddDiscussion { get; }

        public CommonStockController(GetCommonStocks getCommonStocks, GetStockInfo getStockInfo,
                                    GetDiscussion getDiscussion, AddDiscussion addDiscussion)
        {
            GetCommonStocks = getCommonStocks;
            GetStockInfo = getStockInfo;
            GetDiscussion = getDiscussion;
            AddDiscussion = addDiscussion;
        }


        [Route("GetList/{startIndex}/{pageSize}")]
        [HttpGet()] public async Task<IEnumerable<CommonStock>> GetList(int startIndex, int pageSize)
        {
            var result = await this.GetCommonStocks.Execute(startIndex, pageSize);
            return result;
        }

        [Route("GetInfo/{stockId}")]
        [HttpGet()] public async Task<CommonStockInfo> GetInfo(int stockId)
        {
            var result = await this.GetStockInfo.Execute(0, stockId);
            return result;
        }

        [Route("{stockId}/discussion")]
        [HttpGet()]public async Task<IEnumerable<DiscussionItem>> GetDiscussionList(int stockId)
        {
            var result = await this.GetDiscussion.Execute(0, stockId);
            return result;
        }

        [Route("{stockId}/discussion")]
        [HttpPost()] public async Task<Boolean> AddDiscussionItem(Int32 stockId, [FromBody] DiscussionItem discussionItem)
        {
            var result = await this.AddDiscussion.Execute(0, stockId, discussionItem);
            return result;
        }
    }
}
