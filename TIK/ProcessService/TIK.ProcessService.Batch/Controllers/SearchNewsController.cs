using System;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TIK.Applications.Batch.Commands.SearchNews;
using TIK.Domain.Jobs;
using TIK.Domain.SearchNews;
using TIK.ProcessService.Batch.Models;

namespace TIK.ProcessService.Batch.Controllers
{
    //[Authorize]
    public class SearchNewsController : Controller
    {
        public SearchNewsController()
        {
        }
        [HttpPost]
       // [ValidateAntiForgeryToken]
        [Route("SearchNews")]
        public IActionResult AddQueue([FromBody]SearchNewsModel inputModel)
        {
             
            CriteriaSearchNews message = new CriteriaSearchNews 
            { 
                Target = inputModel.Target
            };

            var jobId = BackgroundJob.Enqueue<ISearchNewsCommand>(x => x.SearchNewsBankOfThailand(message));

            BackgroundJob.ContinueWith<ISearchNewsCommand>(jobId, x => x.AnalysisResult(message));

            BackgroundJob.ContinueWith<ISearchNewsCommand>(jobId, x => x.ReportingResult(message));

            var job = new Job() { Id = jobId, Application = "Batch", Procedure = "SearchNews", Status = "Created", Created = DateTime.Now };
            return Ok(job);

        }
  
    }
}
