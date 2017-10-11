using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TIK.Applications.Membership.Jobs.Routes
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
