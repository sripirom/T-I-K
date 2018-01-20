using System;
using TIK.Core.Application.Messaging;
using TIK.Domain.Membership;

namespace TIK.Applications.Online.Members
{
    public partial class MemberActor
    {
        public class ActiveMember : MessageWithMemberId
        {
            public ActiveMember(int memberId)
                : base(memberId)
            {

            }

        }

        public class RefreshMemberStatusMessage : MessageWithMemberId
        {
            public RefreshMemberStatusMessage(int memberId = 0) 
                : base(memberId) 
            {
            }
        }
    }
}
