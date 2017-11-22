using System;
namespace TIK.Applications.Online.EodStocks
{
    public partial class EodStocksActor
    {
        public class RetriveBetween                     
        {
            public DateTime StartDate
            {
                get;
                set;
            }

            public DateTime EndDate
            {
                get;
                set;
            }
        }
    }
}
