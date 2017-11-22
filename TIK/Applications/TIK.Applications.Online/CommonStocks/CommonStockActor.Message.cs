using System;
using TIK.Applications.Messaging;

namespace TIK.Applications.Online.CommonStocks
{
    public partial class CommonStockActor
    {
        public class GetStockInfo : MessageWithStockId
        {
            public GetStockInfo(int memberId, int stockId)
                :base(memberId, stockId)
            {

            }

        }

        public class RetriveEods: MessageWithStockId
        {
            public RetriveEods(int memberId, int stockId)
                : base(memberId, stockId)
            {

            }
        }
    }
}
