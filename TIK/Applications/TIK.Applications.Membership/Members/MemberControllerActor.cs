using System;
using System.Collections.Generic;
using Akka.Actor;
using TIK.Applications.Membership.JobSlots;
using TIK.Applications.Messaging;
using TIK.Domain.Membership;

namespace TIK.Applications.Membership.Members
{
    public class MemberControllerActor : ReceiveActor
    {
        //private readonly Dictionary<int, IActorRef> _memberActives;
        private JobSlotsActorProvider JobSlotsActorProvider { get; }
        private readonly Dictionary<string, IActorRef> _members;
        private IActorRef JobSlotsActor { get; set; }
        public MemberControllerActor(JobSlotsActorProvider provider)
        {
            JobSlotsActorProvider = provider;
            _members = new Dictionary<string, IActorRef>();
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
                if (m is MessageWithMemberId)
                {
                    if(m is MemberActor.ActiveMember){
                        var envelope = m as MemberActor.ActiveMember;
                        var memberActor = Context.Child(envelope.MemberId.ToString()) is Nobody ?
                            Context.ActorOf(MemberActor.Props(envelope, JobSlotsActorProvider.Get()), envelope.MemberId.ToString()) :
                            Context.Child(envelope.MemberId.ToString()); 
                        _members.Add(envelope.MemberId.ToString(), memberActor);
                        memberActor.Forward(m);
                    }else
                    {
                        var envelope = m as MessageWithMemberId;
                        //var memberActor = Context.Child(envelope.MemberId.ToString());
                        IActorRef memberActor;
                        _members.TryGetValue(envelope.MemberId.ToString(), out memberActor);
                        memberActor.Forward(m);
                    }
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
        public static Props SelfProps(JobSlotsActorProvider provider)
        {
            return Akka.Actor.Props.Create(typeof(MemberControllerActor), provider);
        }
    }
}
