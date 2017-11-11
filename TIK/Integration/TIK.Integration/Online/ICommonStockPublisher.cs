using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TIK.Domain.TheSet;

namespace TIK.Integration.Online
{
    public interface ICommonStockPublisher
    {
        Task<IEnumerable<CommonStock>> GetList(int startIndex, int pageSize);
    }
}
 