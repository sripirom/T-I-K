using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TIK.Domain.Member;
using System.Linq;
using TIK.Core.Domain;

namespace TIK.Persistance.ElasticSearch.Mocks
{
    public class MockEsRepository<T>  
        where T : BaseModel<Int32>
    {
        protected List<T> _collection;

        public MockEsRepository()
        {
            _collection = new List<T>();

        }

        public T Get(Int32 id)
        {
            return (T)_collection.FirstOrDefault(a => a.Id.Equals(id));
        }
        public IEnumerable<T> List()
        {
            return _collection;
        }
        public virtual IEnumerable<T> Search(IEnumerable<Tuple<Expression<Func<T, object>>, object>> paramValue)
        {
            IEnumerable<T> results = _collection;
            foreach (var predicate in paramValue)
            {
                results = results.Where(a=>predicate.Item1.Compile().Invoke(a).Equals(predicate.Item2)).ToList();
            }

            return results;
        }
        public bool Delete(Int32 id)
        {
            var entity = _collection.FirstOrDefault(a => a.Id.Equals(id));
            if (entity != null)
            {
                return _collection.Remove(entity);
            }
            else
            {

                return false;
            }

        }
        public Int32 Save(T entry)
        {
            //if (entry.Id == 0)
            {
                entry.Id = _collection.Count() + 1;
            }
            var persistEntity = _collection.FirstOrDefault(a => a.Id.Equals(entry.Id));
            if (persistEntity == null)
            {
                _collection.Add(entry);
            }
            else
            {
                persistEntity = entry;
            }
            return entry.Id;
        }


    }
}
