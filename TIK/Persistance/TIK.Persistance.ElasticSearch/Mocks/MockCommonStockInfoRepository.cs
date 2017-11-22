using System;
using System.Collections.Generic;
using TIK.Domain.TheSet;

namespace TIK.Persistance.ElasticSearch.Mocks
{
    public class MockCommonStockInfoRepository : MockEsRepository<CommonStockInfo, Int32>, ICommonStockInfoRepository
    {
        public MockCommonStockInfoRepository()
        {
            _collection = new List<CommonStockInfo> {
                new CommonStockInfo { Id = 1, Symbol = "2S", Market="mai", SecurityName = "2S METAL PUBLIC COMPANY LIMITED" ,
                    Address = "DYNASTY COMPLEX 2, 67/4 LADPRAO 71, WANG THONGLANG Bangkok",
                    Telephone = "0-2933-0333, 0-2539-4000",
                    Fax = "0-2955-9766",
                    WebSite = "http://www.areeya.co.th",
                    Industry = "Property & Construction",
                    Sector = "",
                    FirstTradeDate = new DateTime(2004, 4, 1),
                    ParValue = 1.00m,
                    AuthorizedCapital = 1200000000.00m,
                    PaidUpCapital = 980000000.00m,
                },
                new CommonStockInfo { Id = 2, Symbol = "A", Market="SET", SecurityName = "AREEYA PROPERTY PUBLIC COMPANY LIMITED" ,
                    Address = "AREEYA PROPERTY COMPLEX 2, 55/4  88, BANGPLI THONGLANG Samutprakan",
                    Telephone = "0-2933-0333, 0-2539-4000",
                    Fax = "0-2955-9766",
                    WebSite = "http://www.areeya.co.th",
                    Industry = "Property & Construction",
                    Sector = "",
                    FirstTradeDate = new DateTime(2004, 4, 1),
                    ParValue = 1.00m,
                    AuthorizedCapital = 1200000000.00m,
                    PaidUpCapital = 980000000.00m},
                new CommonStockInfo { Id = 3, Symbol = "AAV", Market="SET", SecurityName = "ASIA AVIATION PUBLIC COMPANY LIMITED" ,
                    Address = "ASIA AVIATION COMPLEX 8, 10/9 RRR , BANGNA Bangkok",
                    Telephone = "0-2933-0333, 0-2539-4000",
                    Fax = "0-2955-9766",
                    WebSite = "http://www.areeya.co.th",
                    Industry = "Property & Construction",
                    Sector = "",
                    FirstTradeDate = new DateTime(2004, 4, 1),
                    ParValue = 1.00m,
                    AuthorizedCapital = 1200000000.00m,
                    PaidUpCapital = 980000000.00m },
                new CommonStockInfo { Id = 4, Symbol = "ABICO", Market="mai", SecurityName = "ABICO HOLDINGS PUBLIC COMPANY LIMITED" ,
                    Address = "ABICO HOLDING COMPLEX 2, 44/4 RRRoad 71, SUKHUMVIT 10 Bangkok",
                    Telephone = "0-2933-0333, 0-2539-4000",
                    Fax = "0-2955-9766",
                    WebSite = "http://www.areeya.co.th",
                    Industry = "Property & Construction",
                    Sector = "",
                    FirstTradeDate = new DateTime(2004, 4, 1),
                    ParValue = 1.00m,
                    AuthorizedCapital = 1200000000.00m,
                    PaidUpCapital = 980000000.00m}

            };
        }
    }
}
