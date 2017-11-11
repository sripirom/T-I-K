using System;
namespace TIK.Applications.Messaging
{
    public abstract class MessageWithStockSymbol : MessageWithMemberId
    {
        public readonly string Symbol; 

        public MessageWithStockSymbol(int memberId = 0, string symbol = "")
            :base(memberId)
        {
            Symbol = symbol;   
        }


    }
}
