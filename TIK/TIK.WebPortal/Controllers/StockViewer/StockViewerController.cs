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


        [HttpGet("stock/{stockId}/historical")]
        public IActionResult GetHistorical(string stockId)
        {
            IList<Eod> list = new List<Eod> { 
                new Eod{ Id="1", Symbol = "A", EodDate = new DateTime(2017, 11, 1), Open = 1, High = 220, Low = 200, Close = 100, Volume = 128282828 },
                new Eod{ Id="2", Symbol = "A", EodDate = new DateTime(2017, 11, 2), Open = 1, High = 220, Low = 200, Close = 100, Volume = 128282828 },
                new Eod{ Id="3", Symbol = "A", EodDate = new DateTime(2017, 11, 3), Open = 1, High = 220, Low = 200, Close = 100, Volume = 128282828 },
                new Eod{ Id="4", Symbol = "A", EodDate = new DateTime(2017, 11, 4), Open = 1, High = 220, Low = 200, Close = 100, Volume = 128282828 },
                new Eod{ Id="5", Symbol = "A", EodDate = new DateTime(2017, 11, 5), Open = 1, High = 220, Low = 200, Close = 100, Volume = 128282828 },
                new Eod{ Id="6", Symbol = "A", EodDate = new DateTime(2017, 11, 6), Open = 1, High = 220, Low = 200, Close = 100, Volume = 128282828 },
                new Eod{ Id="7", Symbol = "A", EodDate = new DateTime(2017, 11, 7), Open = 1, High = 220, Low = 200, Close = 100, Volume = 128282828 },
                new Eod{ Id="8", Symbol = "A", EodDate = new DateTime(2017, 11, 8), Open = 1, High = 220, Low = 200, Close = 100, Volume = 128282828 }
            };
            return Ok(list);
        }

    }
}
