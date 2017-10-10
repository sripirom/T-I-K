using System;
using Nest;

namespace TIK.Persistance.ElasticSearch
{
    public class EsContext
    {
        private readonly Uri _elastiSearchServerUrl;
        public EsContext(Uri elastiSearchServerUrl = null)
        {
            _elastiSearchServerUrl = elastiSearchServerUrl;
  
        }

        public IElasticClient CreateClient()
        {
            return _elastiSearchServerUrl != null ?
                new ElasticClient(new ConnectionSettings(_elastiSearchServerUrl))
                  : new ElasticClient();

        }
        public TRepository Get<TRepository>()
        {
            return default(TRepository);
        }
    }
}
