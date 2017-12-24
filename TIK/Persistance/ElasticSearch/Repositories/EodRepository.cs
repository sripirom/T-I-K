using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
            IEnumerable<Tuple<Expression<Func<Eod, object>>, object>> paramValue,
            DateTime startDate, DateTime endDate)
        {
            SearchRequest req = new SearchRequest();
            var q = new QueryContainerDescriptor<Eod>();
            foreach (var item in paramValue)
            {
                q.Match(m => m.Field(item.Item1).Query(item.Item2.ToString()));
            }
            q.DateRange(r => r
            .Field(f => f.EodDate)
                        .GreaterThanOrEquals(startDate)
                        .LessThan(endDate));
        
            req.Query = q;

            var result = Client.Search<Eod>(req);
            return result.Documents;
        }
    }
}
