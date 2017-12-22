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
        public IEodPublisher EodPublisher { get; }
        public StockViewerController(ICommonStockPublisher stockPublisher, IEodPublisher eodPublisher)
        {
            StockPublisher = stockPublisher;
            EodPublisher = eodPublisher;
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

            var stockInfo = task.Result;
            return Ok(stockInfo);
        }

     
        [HttpGet("stock/{stockId}/discussion")]
        public IActionResult GetDiscussion(string stockId)
        {
            var id = Convert.ToInt32(stockId);
            var task = StockPublisher.GetStockDiscussion(0, id);

            var list = task.Result;
            return Ok(list);
        }

        [HttpPost("stock/{stockId}/discussion")]
        public IActionResult PostDiscussion(Int32 stockId, [FromBody] DiscussionItemViewModel discussionItem)
        {
            var task = StockPublisher.AddStockDiscussionItem(0, stockId, new DiscussionItem{
                 UserName = discussionItem.UserName,
                Comment = discussionItem.Comment,
                 EnteredOn = DateTime.Now
            });

            var item = task.Result;
            return Ok(item);
        }


        [HttpGet("stock/{symbol}/historical")]
        public IActionResult GetHistorical(string symbol)
        {
            var taskResult = EodPublisher.GetList(symbol, DateTime.Now.AddYears(-1), DateTime.Now);

            return Ok(taskResult.Result);
                     
        }

    }
}
