using System;
using System.Collections.Generic;
using TIK.Integration.Batch;
using TIK.Domain.Membership;
using TIK.Domain.SearchNews;
using System.Linq;
using System.Threading.Tasks;

namespace TIK.Applications.Membership.Mocks
{
    public class MockBatchPublisher : IBatchPublisher
    {
        private IList<Job> _jobs;

        public MockBatchPublisher()
        {
            _jobs = SampleData.Get(); // set sample jobs

        } 

        public Task<IEnumerable<Job>> GetAllJobs()
        {
            return Task.FromResult<IEnumerable<Job>>(_jobs);
        }

        public Task<Job> SearchNews(CriteriaSearchNews criteria)
        {
            var job = new Job
            {
                Id = (_jobs.Max(a => Convert.ToInt32(a.Id)) + 1).ToString(),
                Application = criteria.Target,
                Procedure = "SearchNews",
                Status = "000",
                Created = DateTime.Now
            };
            _jobs.Add(job);

            return Task.FromResult<Job>(job);
        }
    }
}
