using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TIK.Integration.Online;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TIK.WebPortal.Controllers.StockViewer
{
    [Route("api/stockViewer")]
    public class StockController : Controller
    {
        public ICommonStockPublisher StockPublisher { get; }
        public StockController(ICommonStockPublisher stockPublisher)
        {
            StockPublisher = stockPublisher;
        }
        [Route("stocks")]
        [HttpGet]
        public IActionResult Stocks()
        {
            var taskResult = StockPublisher.GetList(0, 20);

            return Ok(taskResult.Result);
        }

    
        [HttpGet("stock/{symbol}/info")]
        public string StockInfo(string symbol)
        {
            return "value";
        }

     
        [HttpGet("stock/{symbol}/discussion")]
        public string Discussion(string symbol)
        {
            return "value";
        }


    }
}
