using System;
using System.Collections.Generic;
using Nest;

namespace TIK.Persistance.ElasticSearch.Repository
{
 
    public abstract class EsRepository<T> : BaseRepository
        where T : class, new()
    {
        private readonly string _typeName;
        public EsRepository(IElasticClient elasticClient)
            :base(elasticClient)
        {
            _typeName = this.GetType().Name.Replace("Repository", "");
        }

        public string Save(T car)
        {
            var result = Client.Index(car);
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
                x => x.Type(_typeName));
            return result.Found;
        }

        public IEnumerable<T> List()
        {
            var result = Client.Search<T>(search =>
                search.MatchAll());

            return result.Documents;
        }

        public IEnumerable<T> Search(string query)
        {
            var result = Client.Search<T>(search =>
                search.Query(q=> q.QueryString(qstr=> qstr.Query(query))));

            return result.Documents;
        }
    }
}

