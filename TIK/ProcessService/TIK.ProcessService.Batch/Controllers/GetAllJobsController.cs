using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TIK.Domain.Membership;

namespace TIK.ProcessService.Batch.Controllers
{
    public class GetAllJobsController : Controller
    { 
        public GetAllJobsController()
        {
        }

        [HttpGet]
        [Route("GetAllJobs")]
        public IActionResult GetAllJobs()
        {
            IEnumerable<Job> jobs = new List<Job> { 
                new Job{ Id = Guid.NewGuid().ToString()
                , Application = "Batch"
                , Procedure = "SearchNews"
                , Status = "Success"
                , Created = DateTime.Now }
            };

            return Ok(jobs);
        }
    }
}
