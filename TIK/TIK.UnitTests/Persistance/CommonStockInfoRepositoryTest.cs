using System;
using System.Collections.Generic;
using System.Linq;
using TIK.Domain.TheSet;
using TIK.Persistance.ElasticSearch;
using TIK.Persistance.ElasticSearch.Repositories;
using Xunit;

namespace TIK.UnitTests.Persistance
{
    public class CommonStockInfoRepositoryTest
    {
        IList<CommonStockInfo> _collection;
        public CommonStockInfoRepositoryTest()
        {
            _collection = new List<CommonStockInfo> {
                new CommonStockInfo { Id = 1, Symbol = "M1", Market="mai", SecurityName = "FIRST COMPANY LIMITED" ,
                    Address = "FIRST COMPLEX 2, 67/4 LADPRAO 71, WANG THONGLANG Bangkok",
                    Telephone = "0-0000-0000, 0-0000-0000",
                    Fax = "0-0000-0000",
                    WebSite = "http://www.first.co.th",
                    Industry = "Property & Construction",
                    Sector = "",
                    FirstTradeDate = new DateTime(2004, 4, 1),
                    ParValue = 1.00m,
                    AuthorizedCapital = 1200000000.00m,
                    PaidUpCapital = 980000000.00m,
                },
                new CommonStockInfo { Id = 2, Symbol = "A", Market="SET", SecurityName = "SECOND PUBLIC COMPANY LIMITED" ,
                    Address = "SECOND PROPERTY COMPLEX 2, 55/4  88, BANGPLI THONGLANG Samutprakan",
                    Telephone = "0-0000-0000, 0-0000-0000",
                    Fax = "0-0000-0000",
                    WebSite = "http://www.second.co.th",
                    Industry = "Property & Construction",
                    Sector = "",
                    FirstTradeDate = new DateTime(2004, 4, 1),
                    ParValue = 1.00m,
                    AuthorizedCapital = 1200000000.00m,
                    PaidUpCapital = 980000000.00m},
                new CommonStockInfo { Id = 3, Symbol = "AAV", Market="SET", SecurityName = "THIRD PUBLIC COMPANY LIMITED" ,
                    Address = "THIRD COMPLEX 8, 10/9 RRR , BANGNA Bangkok",
                    Telephone = "0-0000-0000, 0-0000-0000",
                    Fax = "0-0000-0000",
                    WebSite = "http://www.third.co.th",
                    Industry = "Property & Construction",
                    Sector = "",
                    FirstTradeDate = new DateTime(2004, 4, 1),
                    ParValue = 1.00m,
                    AuthorizedCapital = 1200000000.00m,
                    PaidUpCapital = 980000000.00m },
                new CommonStockInfo { Id = 4, Symbol = "ABICO", Market="mai", SecurityName = "FOURTH PUBLIC COMPANY LIMITED" ,
                    Address = "FOURTH COMPLEX 2, 44/4 RRRoad 71, SUKHUMVIT 10 Bangkok",
                    Telephone = "0-0000-0000, 0-0000-0000",
                    Fax = "0-0000-0000",
                    WebSite = "http://www.fourth.co.th",
                    Industry = "Property & Construction",
                    Sector = "",
                    FirstTradeDate = new DateTime(2004, 4, 1),
                    ParValue = 1.00m,
                    AuthorizedCapital = 1200000000.00m,
                    PaidUpCapital = 980000000.00m}

            };
        }

        [Fact]
        public void IndexCommonStockInfo()
        {
            var context = new EsContext(new Uri("http://localhost:9200"));
            var repo = new IndexRepository(context.CreateClient());

            try
            {
                repo.IndexData<CommonStockInfo>(_collection.FirstOrDefault(), "theset", "commonstockinfo");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

        }

        [Fact] 
        public void AddCommonStock()
        {
            var context = new EsContext(new Uri("http://localhost:9200"));
            var repo = new CommonStockInfoRepository(context.CreateClient(), "theset");

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
