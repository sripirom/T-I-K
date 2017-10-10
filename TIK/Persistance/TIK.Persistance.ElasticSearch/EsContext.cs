using System;
using Nest;

namespace TIK.Persistance.ElasticSearch
{
    public class EsContext
    {
        private readonly Uri _elastiSearchServerUrl;
        private readonly string _index;
        public EsContext(string index, Uri elastiSearchServerUrl = null)
        {
            _index = index;
            _elastiSearchServerUrl = elastiSearchServerUrl;
  
        }

        public IElasticClient CreateClient()
        {
            return _elastiSearchServerUrl != null ?
                new ElasticClient(new ConnectionSettings(_elastiSearchServerUrl)
                                  .DefaultIndex(_index))

                  : new ElasticClient();

        }
        public TRepository Get<TRepository>()
        {
            return default(TRepository);
        }
    }
}
