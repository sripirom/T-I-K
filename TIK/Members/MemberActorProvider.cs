using System;
using Akka.Actor;
using TIK.Applications.Membership.JobSlots;
using TIK.Domain.Membership;

namespace TIK.Applications.Membership.Members
{
    public class MemberActorProvider
    {

        private IActorRef MemberControllerActorInstance { get; set; }

        public MemberActorProvider(ActorSystem actorSystem)
        {
            //this.MemberControllerActorInstance = actorSystem.ActorOf(MemberControllerActor.SelfProps(provider), "MemberController");
            this.MemberControllerActorInstance = actorSystem.ActorSelection("akka.tcp://MembershipSystem@127.0.0.1:5301/user/MemberController")
            .ResolveOne(TimeSpan.FromSeconds(3))
            .Result;
        }

        public IActorRef Get()
        {
            return this.MemberControllerActorInstance;
        }
    }
}
