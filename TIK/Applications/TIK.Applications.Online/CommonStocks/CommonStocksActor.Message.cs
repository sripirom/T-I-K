using System;
namespace TIK.Applications.Online.CommonStocks
{
    public partial class CommonStocksActor
    {
        public class GetCommonStocks
        {
            public int StartIndex
            {
                get;
                set;
            }
            public int PageSize
            {
                get;
                set;
            }

        }

        public class RetriveCommonStock
        {
            public string Symbol
            {
                get;
                set;
            }
        }
    }
}
