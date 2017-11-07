using System;
using Nest;

namespace TIK.Persistance.ElasticSearch
{
    public class EsContext
    {
        private readonly Uri _elastiSearchServerUrl;
        private readonly string _indexName;
        public EsContext(Uri elastiSearchServerUrl = null, string indexName = "")
        {
            _indexName = indexName;
            _elastiSearchServerUrl = elastiSearchServerUrl;
  
        }

        public string IndexName { get { return _indexName; }}

        public IElasticClient CreateClient()
        {
            return _elastiSearchServerUrl != null ?
                new ElasticClient(new ConnectionSettings(_elastiSearchServerUrl)
                                  .DefaultIndex(_indexName))

                  : new ElasticClient();

        }
        public TRepository Get<TRepository>()
        {
            return default(TRepository);
        }
    }
}
