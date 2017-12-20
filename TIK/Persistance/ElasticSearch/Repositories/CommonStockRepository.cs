using System;
using Nest;
using TIK.Domain.TheSet;

namespace TIK.Persistance.ElasticSearch.Repositories
{
    public class CommonStockRepository : EsRepository<CommonStock, Int32>, ICommonStockRepository
    {
        public CommonStockRepository(IElasticClient elasticClient, string indexName) 
            : base(elasticClient, indexName)
        {
        }
    }
}
