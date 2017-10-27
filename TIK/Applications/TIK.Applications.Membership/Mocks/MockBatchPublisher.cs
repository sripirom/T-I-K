using System;
using System.Collections.Generic;
using TIK.Applications.Integration;
using TIK.Domain.Membership;
using TIK.Domain.SearchNews;
using System.Linq;

namespace TIK.Applications.Membership.Mocks
{
    public class MockBatchPublisher : IBatchPublisher
    {
        private IList<Job> _jobs;

        public MockBatchPublisher()
        {
            _jobs = SampleData.Get(); // set sample jobs

        } 

        public IEnumerable<Job> GetAllJobs()
        {
            return _jobs;
        }

        public Job SearchNews(CriteriaSearchNews criteria)
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

            return job;
        }
    }
}
