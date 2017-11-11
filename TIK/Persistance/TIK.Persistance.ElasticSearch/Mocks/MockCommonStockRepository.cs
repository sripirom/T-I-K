using System;
using System.Collections.Generic;
using TIK.Domain.TheSet;

namespace TIK.Persistance.ElasticSearch.Mocks
{
    public class MockCommonStockRepository : MockEsRepository<CommonStock, String>, ICommonStockRepository
    {
        public MockCommonStockRepository()
        {
            //1,2S,mai,บริษัท 2 เอส เมทัล จำกัด(มหาชน),2S METAL PUBLIC COMPANY LIMITED,,
            //2,A,SET,บริษัท อารียา พรอพเพอร์ตี้ จำกัด(มหาชน),AREEYA PROPERTY PUBLIC COMPANY LIMITED,,
            //3,AAV,SET,บริษัท เอเชีย เอวิเอชั่น จำกัด(มหาชน),ASIA AVIATION PUBLIC COMPANY LIMITED,,
            //4,ABICO,mai,บริษัท เอบิโก้ โฮลดิ้งส์ จำกัด(มหาชน),ABICO HOLDINGS PUBLIC COMPANY LIMITED,,
            _collection = new List<CommonStock> {
                new CommonStock { Id = "2S", Market="mai", SecurityName = "2S METAL PUBLIC COMPANY LIMITED" },
                new CommonStock { Id = "A", Market="SET", SecurityName = "AREEYA PROPERTY PUBLIC COMPANY LIMITED" },
                new CommonStock { Id = "AAV", Market="SET", SecurityName = "ASIA AVIATION PUBLIC COMPANY LIMITED" },
                new CommonStock { Id = "ABICO", Market="mai", SecurityName = "ABICO HOLDINGS PUBLIC COMPANY LIMITED" }

            };
        }
    }
}
