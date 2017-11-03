using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using TIK.Applications.Membership.Jobs;
using TIK.Domain.Membership;
using static TIK.Applications.Membership.Jobs.JobActor;

namespace TIK.ProcessService.Membership
{
   [Route("/api/jobs")]
   public class JobApiController
    {
        private Applications.Membership.Routes.GetAllJobs GetAllJobs { get; }
        public JobApiController(Applications.Membership.Routes.GetAllJobs getAllJobs)
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
