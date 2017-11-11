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
        public CommonStockController(GetCommonStocks getCommonStocks)
        {
            GetCommonStocks = getCommonStocks;
        }


        [Route("GetList/{startIndex}/{pageSize}")]
        [HttpGet()] public async Task<IEnumerable<CommonStock>> GetList(int startIndex, int pageSize)
        {
            var result = await this.GetCommonStocks.Execute(startIndex, pageSize);
            return result;
        }

    }
}
