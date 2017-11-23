using System;
using Akka.Actor;
using TIK.Applications.Online.Jobs;

namespace TIK.Applications.Online.BackLogs
{
    public class BackLogsActorProvider
    {
        private IActorRef BackLogsActorInstance { get; set; }

        public BackLogsActorProvider(ActorSystem actorSystem, string host)
        {
           
            this.BackLogsActorInstance = actorSystem.ActorSelection(string.Format("akka.tcp://OnlineSystem@{0}/user/backLogs", host))
                .ResolveOne(TimeSpan.FromSeconds(3))
                .Result;
        }

        public IActorRef Get()
        {
            return this.BackLogsActorInstance;
        }

        public static IActorRef CreateInstance(ActorSystem actorSystem, JobsActorProvider provider)
        {
            var jobsActor = provider.Get();
            return actorSystem.ActorOf(BackLogsActor.Props(jobsActor), "backLogs");
        }
    }
}
