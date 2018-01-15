using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TIK.Domain.TheSet;

namespace TIK.Integration.Online
{
    public interface IEodPublisher
    {
        Task<IEnumerable<Eod>> GetList(string symbol, DateTime startDate, DateTime endDate);
    }  
}
