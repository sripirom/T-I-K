using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TIK.Core.Domain;

namespace TIK.Domain.TheSet
{
    public interface IEodRepository : IRepository<Eod, String>
    {
        IEnumerable<Eod> SearchDateRange(IEnumerable<Tuple<Expression<Func<Eod, object>>, object>> paramValue,
                                DateTime startDate, DateTime endDate);
    } 
}
