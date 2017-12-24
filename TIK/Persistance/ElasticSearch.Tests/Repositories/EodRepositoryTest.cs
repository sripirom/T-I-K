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
            var symbol = "AMA";
            var startDate = DateTime.Now.AddYears(-1);
            var endDate = DateTime.Now;
            try
            {
                
                List<Tuple<Expression<Func<Eod, object>>, object>> paramValue =
                    new List<Tuple<Expression<Func<Eod, object>>, object>>()
                    {
                    new Tuple<Expression<Func<Eod, object>>, object>(q=>q.Symbol, symbol)
                    };

                var commonStocks = repo.Search(paramValue).ToList();
                var result = commonStocks.Where(a => a.EodDate > startDate && a.EodDate < endDate);
                Assert.NotEmpty(result);




            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

        }
    }
}
