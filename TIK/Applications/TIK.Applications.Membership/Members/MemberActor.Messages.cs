using System;
using TIK.Applications.Messaging;
using TIK.Domain.Membership;

namespace TIK.Applications.Membership.Members
{
    public partial class MemberActor
    {
        public class ActiveMember : MessageWithMemberId
        {
            public ActiveMember(Member member)
                : base(member.Id)
            {
                FirstName = member.Name.FirstName;
                LastName = member.Name.LastName;
                Email = member.ContactInfo.Email;
                Phone = member.ContactInfo.Phone;
            }

            public string FirstName { get; private set; }
            public string LastName { get; private set; }
            public string Email { get; private set; }
            public string Phone { get; private set; }
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
