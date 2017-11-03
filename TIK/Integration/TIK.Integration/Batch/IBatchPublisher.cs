using System;
using TIK.Domain.Membership;
using TIK.Domain.SearchNews;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TIK.Integration.Batch
{
    public interface IBatchPublisher
    {
        Task<Job> SearchNews(CriteriaSearchNews criteria);
         
        Task<IEnumerable<Job>> GetAllJobs();
    }
}
