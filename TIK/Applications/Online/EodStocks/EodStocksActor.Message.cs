using System;
namespace TIK.Applications.Online.EodStocks
{
    public partial class EodStocksActor
    {
        public class RetriveBetween                     
        {
            public string Symbol
            {
                get;
                set;
            }
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
