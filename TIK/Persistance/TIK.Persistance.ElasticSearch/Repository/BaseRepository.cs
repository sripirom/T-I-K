using System;
using System.Collections.Generic;
using Nest;

namespace TIK.Persistance.ElasticSearch.Repository
{
 
    public abstract class BaseRepository
    {
        public BaseRepository(IElasticClient elasticClient)
        {
            Client = elasticClient;
        }

        public IElasticClient Client { get; private set; }

    }
}

