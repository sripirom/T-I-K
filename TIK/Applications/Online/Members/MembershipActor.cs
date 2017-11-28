using System;
using System.Collections.Generic;
using Akka.Actor;
using TIK.Applications.Messaging;
using TIK.Domain.Membership;

namespace TIK.Applications.Online.Members
{
    public class MembershipActor : ReceiveActor
    {
        private readonly IMemberRepository _memberRepository;
        public MembershipActor(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;


            ReceiveAny(m => 
            {
                if (m is MessageWithMemberId)
                {
                    if(m is MemberActor.ActiveMember)
                    {
                        var envelope = m as MemberActor.ActiveMember;
                        ActiveMember(envelope);
                    }else
                    {
                        var envelope = m as MessageWithMemberId;
                        var memberActor = Context.Child(envelope.MemberId.ToString());

                        memberActor.Forward(m);
                    }
                }
            });
        }
       
        private void ActiveMember(MemberActor.ActiveMember message)
        {
            var member = _memberRepository.Get(message.MemberId);
            if (member != null)
            {
               

                var memberActor = Context.Child(message.MemberId.ToString()) is Nobody ?
                                         Context.ActorOf(MemberActor.Props(member), message.MemberId.ToString()) :
                                         Context.Child(message.MemberId.ToString());

                memberActor.Forward(message);

                foreach (var memberRef in Context.GetChildren())
                {
                    memberRef.Tell(new MemberActor.RefreshMemberStatusMessage(message.MemberId), Sender);
                }
            }


        }

        public static Props SelfProps()
        {
            return Akka.Actor.Props.Create(typeof(MembershipActor));
        }
    }
}
