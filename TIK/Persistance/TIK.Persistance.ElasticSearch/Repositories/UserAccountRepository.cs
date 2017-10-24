using System;
using Nest;
using TIK.Domain.UserAccounts;
using TIK.Persistance.ElasticSearch;

namespace TIK.Persistance.ElasticSearch.Repositories
{
    public class UserAccountRepository : EsRepository<UserAccount, Int32>, IUserAccountRepository
    {
        public UserAccountRepository(IElasticClient elasticClient)
            :base(elasticClient)
        {
        }
    }
}
