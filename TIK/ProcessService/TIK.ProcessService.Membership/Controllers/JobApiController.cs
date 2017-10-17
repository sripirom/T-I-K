using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using TIK.Applications.Membership.Jobs.Routes;
using TIK.Applications.Membership.Jobs;

namespace TIK.ProcessService.Membership
{
   [Route("/api/jobs")]
   public class JobApiController
    {
        private GetAllJobs GetAllJobs { get; }
        public JobApiController(GetAllJobs getAllJobs)
        {
            this.GetAllJobs = getAllJobs;
        }

        [HttpGet()] public async Task<IEnumerable<Job>> Get()
        {
            var result = await this.GetAllJobs.Execute();
            return result;
        }
    }
}
