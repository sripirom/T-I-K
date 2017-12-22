using System;
using Nest;
using TIK.Core.Domain;
using TIK.Domain.TheSet;

namespace TIK.Persistance.ElasticSearch.Repositories
{
    public class EodRepository : EsRepository<Eod, String>, IEodRepository
    {
        public EodRepository(EsContext context) : base(context)
        {
        } 
    }
}
