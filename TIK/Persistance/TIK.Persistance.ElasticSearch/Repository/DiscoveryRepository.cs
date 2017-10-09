using System;
using System.Collections.Generic;
using System.Linq;
using Nest;

namespace TIK.Persistance.ElasticSearch.Repository
{
    public class DiscoveryRepository : BaseRepository
    {
        public DiscoveryRepository(IElasticClient elasticClient)
            : base(elasticClient)
        {
        }

        /// <summary>
        /// Searches all indexes and all fields.
        /// </summary>
        /// <param name="queryTerm">The query term.</param>
        /// <returns>A search results.</returns>
        public List<Tuple<string, string>> SearchAll(string queryTerm)
        {
            var queryResult = Client.Search<dynamic>(d =>
                d.AllIndices()
                .AllTypes()
                 .Query(q => q.QueryString(qstr => qstr.Query(queryTerm))));
                 //.QueryString(queryTerm));
          
            return queryResult
                .Hits
                .Select(c => new Tuple<string, string>(c.Index, c.Source.name.Value))
                .Distinct()
                .ToList();
        }

        /// <summary>
        /// A fuzzye search.
        /// </summary>
        /// <param name="queryTerm">The query term.</param>
        /// <returns>A search results.</returns>
        public dynamic FuzzySearch(string queryTerm)
        {
            return Client.Search<dynamic>(d =>
                   d.AllIndices()
                   .AllTypes()
                   .Query(q => q.Fuzzy(f =>
                                       f.Value(queryTerm))));
        }
    }
}
