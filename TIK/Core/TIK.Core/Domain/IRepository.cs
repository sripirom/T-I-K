using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TIK.Core.Domain
{
    public interface IRepository<T, TId>
    {
        TId Save(T entry);

        T Get(TId id);

        bool Delete(TId id);

        IEnumerable<T> List();

        IEnumerable<T> Search(IEnumerable<Tuple<Expression<Func<T, object>>, object>> paramValue);
    }
}
