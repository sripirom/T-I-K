using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TIK.Core.Domain;

namespace TIK.Domain.TheSet
{
    public interface IEodRepository : IRepository<Eod, String>
    {
        IEnumerable<Eod> SearchDateRange(string symbol,
                                         DateTime startDate, DateTime endDate, int maxSize);
    } 
}
