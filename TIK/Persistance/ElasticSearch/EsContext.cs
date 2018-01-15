using System;
using Elasticsearch.Net;
using Nest;

namespace TIK.Persistance.ElasticSearch
{
    public class EsContext
    {
        private readonly Uri _elastiSearchServerUrl;
        private readonly string _rootIndex;
        public EsContext(Uri elastiSearchServerUrl = null, string rootIndex = "")
        {
            _rootIndex = rootIndex;
            _elastiSearchServerUrl = elastiSearchServerUrl;
  
        }

        public string RootIndex { get { return _rootIndex; }}

        public IElasticClient CreateClient<T>()
        {
            var typeIndex = $"{RootIndex}_{typeof(T).Name.ToLower()}"; 
            var connectionPool = new SingleNodeConnectionPool(_elastiSearchServerUrl);

            var settings = new ConnectionSettings(connectionPool)
                                    .DefaultIndex(typeIndex)
                                    .DisableDirectStreaming(); 
            var client = _elastiSearchServerUrl != null ?
                        new ElasticClient(settings)
                        : new ElasticClient();
            
            return client;

        }
        public TRepository Get<TRepository>()
        {
            return default(TRepository);
        }
    }
}
