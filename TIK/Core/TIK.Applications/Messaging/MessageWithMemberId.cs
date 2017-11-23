using System;
namespace TIK.Applications.Messaging
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
