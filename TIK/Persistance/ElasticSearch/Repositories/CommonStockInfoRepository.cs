using System;
using Nest;
using TIK.Domain.TheSet;

namespace TIK.Persistance.ElasticSearch.Repositories
{
    public class CommonStockInfoRepository : EsRepository<CommonStockInfo, Int32>, ICommonStockInfoRepository
    {
        public CommonStockInfoRepository(IElasticClient elasticClient, string indexName) : base(elasticClient, indexName)
        {
        }
    }
}
