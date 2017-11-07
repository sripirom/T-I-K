using System;
using Akka.Actor;
using TIK.Applications.Messaging;
using TIK.Domain.Membership;

namespace TIK.Applications.Online.Members
{
    public partial class MemberActor : ReceiveActor
    {

        private Member _memberInfo;

        public MemberActor(Member memberInfo)
        {
            _memberInfo = memberInfo;

            Receive<ActiveMember>(message =>
            {
                if(_memberInfo != null)
                {
                    Sender.Tell(_memberInfo);
                }else
                {
                    Member member = new Member();
                    member.Id = message.MemberId;
                    member.IsActive = false;
                    Sender.Tell(member);
                }
            });

            Receive<RefreshMemberStatusMessage>(message =>
            {
                Sender.Tell(_memberInfo);
            });

        }


        public static Props Props(Member memberInfo)
        {
            return Akka.Actor.Props.Create(() => new MemberActor(memberInfo));
        }

    }
}
