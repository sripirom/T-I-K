using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TIK.Domain.TheSet;
using TIK.Persistance.ElasticSearch;
using TIK.Persistance.ElasticSearch.Repositories;
using Xunit;

namespace TIK.Persistance.ElasticSearch.Tests.Repositories
{
    public class EodRepositoryTest
    {
        private IList<Eod> _collection;
        public EodRepositoryTest()
        {
            _collection = new List<Eod>();
        }


        [Fact]
        public void AddEod()
        {
            var context = new EsContext(new Uri("http://localhost:9200"), "set");
            var repo = new EodRepository(context);

            try
            {
                foreach (var item in _collection)
                {
                    repo.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

        }


        [Fact]
        public void MatchAllEod()
        {
            try
            {
                var context = new EsContext(new Uri("http://localhost:9200"), "set");
                var repo = new EodRepository(context);

                var res = repo.List();
                Assert.NotNull(res);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }



        }

        [Fact]
        public void SearchEod()
        {
            var context = new EsContext(new Uri("http://localhost:9200"), "set");
            var repo = new EodRepository(context);
            var symbol = "AFC";
            var startDate = DateTime.Now.AddMonths(-34);
            var endDate = DateTime.Now;
            var maxSize = 1000;

            var commonStocks = repo.SearchDateRange(symbol, startDate, endDate, maxSize).ToList();
            //var result = commonStocks.Where(a => a.EodDate > startDate && a.EodDate < endDate);
            Assert.NotEmpty(commonStocks);




       

        }
    }
}
