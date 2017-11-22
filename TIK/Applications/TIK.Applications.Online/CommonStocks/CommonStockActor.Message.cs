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

        public class GetDiscussion : MessageWithStockId
        {
            public GetDiscussion(int memberId, int stockId)
                : base(memberId, stockId)
            {

            }
        }

        public class AddDiscussion : MessageWithStockId
        {
            public AddDiscussion(int memberId, int stockId, string userName, string comment, DateTime enteredOn)
                : base(memberId, stockId)
            {
                UserName = userName;
                EnteredOn = enteredOn;
                Comment = comment;
            }

            public string UserName { get; private set; }
            public DateTime EnteredOn { get; private set; }
            public string Comment { get; private set; }
        }
    }
}
