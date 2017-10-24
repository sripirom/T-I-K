using System;
using Akka.Actor;
using TIK.Applications.Membership.JobSlots;
using TIK.Applications.Messaging;
using TIK.Domain.Member;

namespace TIK.Applications.Membership.Members
{
    public partial class MemberActor : ReceiveActor
    {
        private Member _memberInfo;
        public MemberActor(Member memberInfo)
        {
            _memberInfo = memberInfo;

            Receive<ActiveMember>(message => {

                var result = new Member
                    {
                        Id = _memberInfo.Id,
                        Name = new Name { FirstName = _memberInfo.Name.FirstName, LastName = _memberInfo.Name.FirstName },
                        ContactInfo = new ContactInfo { Email = _memberInfo.ContactInfo.Email, Phone = _memberInfo.ContactInfo.Phone },
                        IsActive = true
                    }; 


                Sender.Tell(result);
            });


            Receive<JobSlotActor.AddItemToJobSlot>(
                message =>
                {
                    //_health -= 20;

                    //Sender.Tell(new PlayerHealthChangedMessage(_playerName, _health));
                });

            Receive<RefreshMemberStatusMessage>(
                message =>
                {
                    //Sender.Tell(new PlayerStatusMessage(_playerName, _health));
                });
        }


        public static Props Props(ActiveMember member)
        {
            var memberInfo = new Member
                    {
                        Id = member.MemberId,
                        Name = new Name { FirstName = member.FirstName, LastName = member.FirstName },
                        ContactInfo = new ContactInfo { Email = member.Email, Phone = member.Phone },
                        IsActive = false
                    }; 
            return Akka.Actor.Props.Create(() => new MemberActor(memberInfo));
        }

    }
}
