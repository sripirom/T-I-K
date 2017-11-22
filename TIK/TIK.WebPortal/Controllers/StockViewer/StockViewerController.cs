using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TIK.Domain.TheSet;
using TIK.Integration.Online;
using TIK.WebPortal.Models.StockViewerViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TIK.WebPortal.Controllers.StockViewer
{
    [Route("api/stockViewer")]
    public class StockViewerController : Controller
    {
        public ICommonStockPublisher StockPublisher { get; }
        public StockViewerController(ICommonStockPublisher stockPublisher)
        {
            StockPublisher = stockPublisher;
        }
        [Route("stocks")]
        [HttpGet]
        public IActionResult Stocks()
        {
            var taskResult = StockPublisher.GetList(0, 20);

            return Ok(taskResult.Result.Select(s => 
                            new StockViewModel{
                            StockId = s.Id,
                            Symbol = s.Symbol,
                            SecurityName = s.SecurityName,
                            Market  = s.Market
                        })
                     );
        }
        [Route("stocks/resert")]
        [HttpGet]
        public IActionResult Stocksb()
        {
            var taskResult = StockPublisher.GetList(0, 20);

            return Ok(taskResult.Result);
        }

    
        [HttpGet("stock/{id}/info")]
        public IActionResult StockInfo(Int32 id)
        {
            var task = StockPublisher.GetInfo(0, id);
            /*
            var stock = new CommonStockInfo();
            stock.Id = id;
            stock.Symbol = "A";
            stock.SecurityName = "AREEYA PROPERTY PUBLIC COMPANY LIMITED";
            stock.Address = "DYNASTY COMPLEX 2, 67/4 LADPRAO 71, WANG THONGLANG Bangkok";
            stock.Telephone = "0-2933-0333, 0-2539-4000";
            stock.Fax = "0-2955-9766";
            stock.WebSite = "http://www.areeya.co.th";
            stock.Market = "SET";
            stock.Industry = "Property & Construction";
            stock.Sector = "";
            stock.FirstTradeDate = new DateTime(2004, 4, 1);
            stock.ParValue = 1.00m;
            stock.AuthorizedCapital = 1200000000.00m;
            stock.PaidUpCapital = 980000000.00m;
            */
            var stockInfo = task.Result;
            return Ok(stockInfo);
        }

     
        [HttpGet("stock/{symbol}/discussion")]
        public string Discussion(string symbol)
        {
            return "value";
        }


    }
}
