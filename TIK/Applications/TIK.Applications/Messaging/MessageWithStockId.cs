using System;
namespace TIK.Applications.Messaging
{
    public abstract class MessageWithStockId : MessageWithMemberId
    {
        public readonly int StockId; 

        public MessageWithStockId(int memberId = 0, int stockId = 0)
            :base(memberId)
        {
            StockId = stockId;   
        }


    }
}
