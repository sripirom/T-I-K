using System;
using Nest;
using TIK.Domain.Member;
using TIK.Persistance.ElasticSearch.Repository;

namespace TIK.Persistance.ElasticSearch.Tests.Repositories
{
    public class UserAccountRepository : EsRepository<UserAccount>
    {
        public UserAccountRepository(IElasticClient elasticClient)
            :base(elasticClient)
        {
        }
    }
}
