using System;
namespace TIK.Core.Application.Messaging
{
    public abstract class MessageWithMemberId
    {
        public readonly int MemberId;

        public MessageWithMemberId(int memberId = 0)
        {
            this.MemberId = memberId;
        }
    }
}
