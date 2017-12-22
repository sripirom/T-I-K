using System;
using System.Collections.Generic;
using System.Linq;
using TIK.Domain.TheSet;
using TIK.Persistance.ElasticSearch;
using TIK.Persistance.ElasticSearch.Repositories;
using Xunit;

namespace TIK.UnitTests.Persistance
{
    public class CommonStockRepositoryTest
    {
        private IList<CommonStock> _collection;
        public CommonStockRepositoryTest()
        {
            _collection = new List<CommonStock> {
                new CommonStock { Id = 1, Symbol = "M1", Market="mai", SecurityName = "FIRST PUBLIC COMPANY LIMITED" },
                new CommonStock { Id = 2, Symbol = "S1", Market="SET", SecurityName = "SECOND PUBLIC COMPANY LIMITED" },
                new CommonStock { Id = 3, Symbol = "S2", Market="SET", SecurityName = "THIRD PUBLIC COMPANY LIMITED" },
                new CommonStock { Id = 4, Symbol = "M2", Market="mai", SecurityName = "FOURTH PUBLIC COMPANY LIMITED" }

            };
        }


        [Fact]
        public void MatchAllCommonStock()
        {
            try
            {
                var context = new EsContext(new Uri("http://localhost:9200"), "theset");
                var repo = new CommonStockRepository(context);

                var res = repo.List();
                Assert.NotNull(res);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }



        }

        [Fact]
        public void AddCommonStock()
        {
            var context = new EsContext(new Uri("http://localhost:9200"), "theset");
            var repo = new CommonStockRepository(context);

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
    }
}
