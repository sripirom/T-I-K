using System;
using Nest;
using TIK.Domain.Member;
using TIK.Persistance.ElasticSearch;

namespace TIK.Persistance.ElasticSearch.Repositories
{
    public class UserAccountRepository : EsRepository<UserAccount, Guid>, IUserAccountRepository
    {
        public UserAccountRepository(IElasticClient elasticClient)
            :base(elasticClient)
        {
        }
    }
}
