using System;
using Nest;


namespace TIK.Persistance.ElasticSearch.Repository
{
    public class IndexRepository : BaseRepository
    {
        public IndexRepository(IElasticClient elasticClient)
            :base(elasticClient)
        {
        }



        public bool IndexData<T>(T data, string indexName = null, string mappingType = null)
            where T : class, new()
        {
            if (Client == null)
            {
                throw new ArgumentNullException("data");
            }

            var result = Client.Index<T>(data, c => c.Index(indexName).Type(mappingType));
            return result.IsValid;
        }
    }
}
