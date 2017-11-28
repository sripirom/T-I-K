using System;
using System.Collections.Generic;
using TIK.Domain.TheSet;

namespace TIK.Persistance.ElasticSearch.Mocks
{
    public class MockEodRepository: MockEsRepository<Eod, string>, IEodRepository
    {
        public MockEodRepository()
        {
            _collection = new List<Eod>();
        }
   
    }
}
