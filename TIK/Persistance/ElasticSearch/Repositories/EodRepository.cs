using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Nest;
using TIK.Core.Domain;
using TIK.Domain.TheSet;

namespace TIK.Persistance.ElasticSearch.Repositories
{
    public class EodRepository : EsRepository<Eod, String>, IEodRepository
    {
        public EodRepository(EsContext context) : base(context)
        {
        }

        public IEnumerable<Eod> SearchDateRange(
            string symbol,
            DateTime startDate, DateTime endDate, int maxSize)
        {
           // SearchRequest req = new SearchRequest();
            var q = new QueryContainerDescriptor<Eod>();

            string queryString = $"{symbol} AND eodDate:[{startDate.ToString("O")} TO {endDate.ToString("O")}]";
            q.QueryString(c => 
                          c.DefaultField(prop=>prop.Symbol)
                          .Query(queryString)
                         );


            var result = Client.Search<Eod>(s => s
                            .RequestConfiguration(r => r
                                    .DisableDirectStreaming()
                                )
                                            .Query(i => i = q).Size(maxSize));

   
            return result.Documents; 
        }

    }
}
