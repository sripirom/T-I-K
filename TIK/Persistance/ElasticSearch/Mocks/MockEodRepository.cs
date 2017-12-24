using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TIK.Domain.TheSet;

namespace TIK.Persistance.ElasticSearch.Mocks
{
    public class MockEodRepository: MockEsRepository<Eod, string>, IEodRepository
    {
        public MockEodRepository()
        {
            _collection = new List<Eod> {
                new Eod{ Id="1", Symbol = "M1", EodDate = new DateTime(2017, 11, 1), Open = 1, High = 220, Low = 200, Close = 100, Volume = 128282828 },
                new Eod{ Id="2", Symbol = "M1", EodDate = new DateTime(2017, 11, 2), Open = 1, High = 220, Low = 200, Close = 100, Volume = 128282828 },
                new Eod{ Id="3", Symbol = "S1", EodDate = new DateTime(2017, 11, 3), Open = 1, High = 220, Low = 200, Close = 100, Volume = 128282828 },
                new Eod{ Id="4", Symbol = "S1", EodDate = new DateTime(2017, 11, 4), Open = 1, High = 220, Low = 200, Close = 100, Volume = 128282828 },
                new Eod{ Id="5", Symbol = "S2", EodDate = new DateTime(2017, 11, 5), Open = 1, High = 220, Low = 200, Close = 100, Volume = 128282828 },
                new Eod{ Id="6", Symbol = "S2", EodDate = new DateTime(2017, 11, 6), Open = 1, High = 220, Low = 200, Close = 100, Volume = 128282828 },
                new Eod{ Id="7", Symbol = "M2", EodDate = new DateTime(2017, 11, 7), Open = 1, High = 220, Low = 200, Close = 100, Volume = 128282828 },
                new Eod{ Id="8", Symbol = "M2", EodDate = new DateTime(2017, 11, 8), Open = 1, High = 220, Low = 200, Close = 100, Volume = 128282828 }
            };
        }

        public IEnumerable<Eod> SearchDateRange(IEnumerable<Tuple<Expression<Func<Eod, object>>, object>> paramValue, DateTime startDate, DateTime endDate)
        {
            IEnumerable<Eod> results = _collection;
            foreach (var predicate in paramValue)
            {
                results = results.Where(a => predicate.Item1.Compile().Invoke(a).Equals(predicate.Item2)).ToList();
            }

            return results.Where(a=>a.EodDate >= startDate && a.EodDate<=endDate);
        }
    }
}
