using System;
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

            var client = _elastiSearchServerUrl != null ?
                new ElasticClient(new ConnectionSettings(_elastiSearchServerUrl)
                                  .DefaultIndex(typeIndex))
             
                  : new ElasticClient();
            
            return client;

        }
        public TRepository Get<TRepository>()
        {
            return default(TRepository);
        }
    }
}
