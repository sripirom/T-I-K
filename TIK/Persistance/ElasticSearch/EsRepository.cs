﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nest;
using TIK.Core.Domain;

namespace TIK.Persistance.ElasticSearch
{
 
    public abstract class EsRepository<T, TId> : BaseRepository, IRepository<T, TId>
        where T : BaseModel<TId>, new()
    {
        public EsRepository(EsContext context)
            :base(context.CreateClient<T>())
        {
            IndexName = Client.ConnectionSettings.DefaultIndex;
        }

        public string IndexName { get; }

        public TId Add(T entry)
        {
            var result = Client.Index<T>(entry, c=> c.Index(IndexName));
            if(!result.IsValid){
                throw result.OriginalException;
            }
           
            return (TId)Convert.ChangeType(result.Id, typeof(TId));
        }

        public TId Save(T entry)
        {
            var result = Client.Update<T>(entry, c => c.Index(IndexName));
            var id= (TId)Convert.ChangeType(result.Id, typeof(TId));
            return id;
        }


        public bool Delete(TId id)
        {
            var result = Client.Delete<T>(id.ToString(),
                                          x => x.Index(IndexName));
            return result.Found;
        }

        public T Get(TId id)
        {
            var result = Client.Get<T>(id.ToString());
            return result.Source;
        }

        public IEnumerable<T> List(int skip = 0, int size = 20)
        {
            var result = Client.Search<T>(search =>
                                          search.Skip(skip).Size(size).MatchAll());

            return result.Documents;
        }

        public IEnumerable<T> Search(IEnumerable<Tuple<Expression<Func<T, object>>, object>> paramValue)
        {
            SearchRequest req = new SearchRequest();
            var q = new QueryContainerDescriptor<T>();
            foreach (var item in paramValue)
            {
                q.Match(m=>m.Field(item.Item1).Query(item.Item2.ToString()));
            }
            req.Query = q;
       
            var result = Client.Search<T>(req);
            return result.Documents;
        }

    }
}

