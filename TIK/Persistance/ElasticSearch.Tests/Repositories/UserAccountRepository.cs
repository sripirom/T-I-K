using System;
using Nest;
using TIK.Domain.UserAccounts;
using TIK.Persistance.ElasticSearch;

namespace TIK.Persistance.ElasticSearch.Tests.Repositories
{
    public class UserAccountRepository : EsRepository<UserAccount, Int32>
    {
        public UserAccountRepository(IElasticClient elasticClient)
            :base(elasticClient, "userAccount")
        {
        }
    }
}
