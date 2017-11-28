using System;
using Akka.Actor;
using TIK.Integration.Batch;

namespace TIK.Applications.Online.Jobs
{
    public class JobsActorProvider
    {
        private IActorRef JobsActor { get; set; }

        public JobsActorProvider(ActorSystem actorSystem, string host)
        {
         
            this.JobsActor = actorSystem.ActorSelection(string.Format("akka.tcp://OnlineSystem@{0}/user/jobs", host))
                .ResolveOne(TimeSpan.FromSeconds(3))
                .Result;
        }

        public IActorRef Get()
        {
            return JobsActor;
        }

        public static IActorRef CreateInstance(ActorSystem actorSystem, IBatchPublisher batchPublisher)
        {
            var jobs = SampleData.Get(); // set sample jobs
            return actorSystem.ActorOf(Props.Create<JobsActor>(jobs, batchPublisher), "jobs");
        }
    }
}
