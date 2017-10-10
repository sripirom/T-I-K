using System;
using System.Collections.Generic;
using Nest;

namespace TIK.Persistance.ElasticSearch.Repository
{
 
    public abstract class EsRepository<T> : BaseRepository
        where T : class, new()
    {
        private readonly string _mappingType;
        private readonly string _indexName = "member";
        public EsRepository(IElasticClient elasticClient)
            :base(elasticClient)
        {
            _mappingType = this.GetType().Name.Replace("Repository", "");
        }

        public string Save(T entry)
        {
            var result = Client.Index<T>(entry, c => c.Index(_indexName).Type(_mappingType));
            return result.Id;
        }

        public T Get(Guid id)
        {
            var result = Client.Get<T>(id.ToString());
            return result.Source;
        }

        public bool Delete(Guid id)
        {
            var result = Client.Delete<T>(id.ToString(),
                                          x => x.Type(_mappingType));
            return result.Found;
        }

        public IEnumerable<T> List()
        {
            var result = Client.Search<T>(search =>
                search.MatchAll());

            return result.Documents;
        }

        public IEnumerable<T> Search(Func<QueryStringQueryDescriptor<T>, IQueryStringQuery> query)
        {
            var result = Client.Search<T>(search =>
                                          search.Query(q=> q.QueryString(qstr=> query.Invoke(qstr)) ));

            return result.Documents;
        }
    }
}

