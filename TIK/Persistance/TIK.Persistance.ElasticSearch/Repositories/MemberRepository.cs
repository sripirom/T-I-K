using System;
using Nest;
using TIK.Domain.Member;

namespace TIK.Persistance.ElasticSearch.Repositories
{
    public class MemberRepository: EsRepository<Member, Int32>, IMemberRepository
    {
        public MemberRepository(IElasticClient elasticClient)
            :base(elasticClient)
        {
        }
    }
}
