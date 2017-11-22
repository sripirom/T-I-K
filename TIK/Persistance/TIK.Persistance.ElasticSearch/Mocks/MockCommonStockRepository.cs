using System;
using System.Collections.Generic;
using TIK.Domain.TheSet;

namespace TIK.Persistance.ElasticSearch.Mocks
{
    public class MockCommonStockRepository : MockEsRepository<CommonStock, Int32>, ICommonStockRepository
    {
        public MockCommonStockRepository()
        {
            //1,2S,mai,บริษัท 2 เอส เมทัล จำกัด(มหาชน),2S METAL PUBLIC COMPANY LIMITED,,
            //2,A,SET,บริษัท อารียา พรอพเพอร์ตี้ จำกัด(มหาชน),AREEYA PROPERTY PUBLIC COMPANY LIMITED,,
            //3,AAV,SET,บริษัท เอเชีย เอวิเอชั่น จำกัด(มหาชน),ASIA AVIATION PUBLIC COMPANY LIMITED,,
            //4,ABICO,mai,บริษัท เอบิโก้ โฮลดิ้งส์ จำกัด(มหาชน),ABICO HOLDINGS PUBLIC COMPANY LIMITED,,
            _collection = new List<CommonStock> {
                new CommonStock { Id = 1, Symbol = "2S", Market="mai", SecurityName = "2S METAL PUBLIC COMPANY LIMITED" },
                new CommonStock { Id = 2, Symbol = "A", Market="SET", SecurityName = "AREEYA PROPERTY PUBLIC COMPANY LIMITED" },
                new CommonStock { Id = 3, Symbol = "AAV", Market="SET", SecurityName = "ASIA AVIATION PUBLIC COMPANY LIMITED" },
                new CommonStock { Id = 4, Symbol = "ABICO", Market="mai", SecurityName = "ABICO HOLDINGS PUBLIC COMPANY LIMITED" }

            };
        }
    }
}
