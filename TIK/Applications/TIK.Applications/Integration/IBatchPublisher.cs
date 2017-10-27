using System;
using TIK.Domain.Membership;
using TIK.Domain.SearchNews;
using System.Collections.Generic;

namespace TIK.Applications.Integration
{
    public interface IBatchPublisher
    {
        Job SearchNews(CriteriaSearchNews criteria);

        IEnumerable<Job> GetAllJobs();
    }
}
