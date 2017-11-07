using System;
using TIK.Applications.Messaging;
using TIK.Domain.Membership;

namespace TIK.Applications.Membership.Members
{
    public partial class MemberActor
    {
        public class ActiveMember : MessageWithMemberId
        {
            public ActiveMember(int memberId, string firstName, string lastName, string email, string phone)
                : base(memberId)
            {
                FirstName = firstName;
                LastName = lastName;
                Email = email;
                Phone = phone;
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
