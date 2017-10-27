using System;
using Akka.Actor;
using TIK.Applications.Membership.JobSlots;
using TIK.Domain.Membership;

namespace TIK.Applications.Membership.Members
{
    public class MemberActorProvider
    {

        private IActorRef MemberControllerActorInstance { get; set; }

        public MemberActorProvider(ActorSystem actorSystem, JobSlotsActorProvider provider)
        {
            this.MemberControllerActorInstance = actorSystem.ActorOf(MemberControllerActor.SelfProps(provider), "MemberController");
        }

        public IActorRef Get()
        {
            return this.MemberControllerActorInstance;
        }
    }
}
