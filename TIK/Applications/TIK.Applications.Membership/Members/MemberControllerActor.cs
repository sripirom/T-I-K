using System;
using System.Collections.Generic;
using Akka.Actor;
using TIK.Applications.Membership.JobSlots;
using TIK.Applications.Messaging;
using TIK.Domain.Member;

namespace TIK.Applications.Membership.Members
{
    public class MemberControllerActor : ReceiveActor
    {
        //private readonly Dictionary<int, IActorRef> _memberActives;
        private readonly IMemberRepository _memberRepository;
        public MemberControllerActor(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
            /*
            _memberActives = new Dictionary<int, IActorRef>();

            Receive<MemberActor.ActiveMember>(message => ActiveMember(message));

            Receive<JobSlotActor.AddItemToJobSlot>(
                message =>
                { 
                    _memberActives[message.MemberId].Forward(message);
                });
*/
            ReceiveAny(m => 
            {
                if (m is MemberActor.ActiveMember)
                {
                    var envelope = m as MemberActor.ActiveMember;
                    var memberActor = Context.Child(envelope.MemberId.ToString()) is Nobody ?
                        Context.ActorOf(MemberActor.Props(envelope), envelope.MemberId.ToString()) :
                        Context.Child(envelope.MemberId.ToString());
                    
                    memberActor.Forward(m);
                }
            });
        }
        /*
        private void ActiveMember(MemberActor.ActiveMember message)
        {
            var memberNeedsCreating = !_memberActives.ContainsKey(message.MemberId);

            if (memberNeedsCreating)
            {
                IActorRef newMemberActor =
                    Context.ActorOf(
                        Props.Create(() => new MemberActor(message)), message.MemberId.ToString()
                        );

                _memberActives.Add(message.MemberId, newMemberActor);

                foreach (var member in _memberActives.Values)
                {
                    member.Tell(new MemberActor.RefreshMemberStatusMessage(message.MemberId), Sender);
                }
            }


        }
*/
        public static Props SelfProps(IMemberRepository repository)
        {
            return Akka.Actor.Props.Create(() => new MemberControllerActor(repository));
        }
    }
}
