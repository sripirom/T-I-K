using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TIK.Domain.TheSet;

namespace TIK.Applications.Online.EodStocks.Routes
{
    [Route("/Eod")]
    public class EodController : Controller
    {
        private GetEods GetEods { get; }

        public EodController(GetEods getEods)
        {
            GetEods = getEods;
        }

        [Route("GetList/{symbol}/{startDate}/{endDate}")]
        [HttpGet()]
        public async Task<IEnumerable<Eod>> GetList(string symbol, DateTime startDate, DateTime endDate)
        { 
            var result = await this.GetEods.Execute(symbol, startDate, endDate);
            return result;
        }
    }
}
