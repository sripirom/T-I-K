using System;
using Akka.Actor;
using TIK.Domain.Membership;

namespace TIK.Applications.Online.Members
{
    public class MemberActorProvider
    {

        private IActorRef MemberControllerActorInstance { get; set; }

        public MemberActorProvider(ActorSystem actorSystem, string host)
        {
            this.MemberControllerActorInstance = actorSystem.ActorSelection(string.Format("akka.tcp://OnlineSystem@{0}/user/Membership", host))
                .ResolveOne(TimeSpan.FromSeconds(3))
                .Result;

        }

        public IActorRef Get()
        {
            return this.MemberControllerActorInstance;
        }

        public static IActorRef CreateInstance(ActorSystem actorSystem, IMemberRepository memberRepository)
        {
            return actorSystem.ActorOf(Props.Create(typeof(MembershipActor), memberRepository), "Membership"); 
        }
    }
}
