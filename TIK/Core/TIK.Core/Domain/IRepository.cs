using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TIK.Core.Domain
{
    public interface IRepository<T, TId>
    {
        TId Add(T entry);

        TId Save(T entry);

        T Get(TId id);

        bool Delete(TId id);

        IEnumerable<T> List(int skip = 0, int size = 20);

        IEnumerable<T> Search(IEnumerable<Tuple<Expression<Func<T, object>>, object>> paramValue);
    }
}
