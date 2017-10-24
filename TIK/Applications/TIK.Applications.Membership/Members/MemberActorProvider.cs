using System;
using Akka.Actor;
using TIK.Domain.Member;

namespace TIK.Applications.Membership.Members
{
    public class MemberActorProvider
    {

        private IActorRef MemberControllerActorInstance { get; set; }

        public MemberActorProvider(ActorSystem actorSystem, IMemberRepository repository)
        {
            this.MemberControllerActorInstance = actorSystem.ActorOf(MemberControllerActor.SelfProps(repository), "MemberController");
        }

        public IActorRef Get()
        {
            return this.MemberControllerActorInstance;
        }
    }
}
