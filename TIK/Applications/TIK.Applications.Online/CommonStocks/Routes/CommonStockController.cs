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
        public CommonStockController(GetCommonStocks getCommonStocks, GetStockInfo getStockInfo)
        {
            GetCommonStocks = getCommonStocks;
            GetStockInfo = getStockInfo;
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
    }
}
