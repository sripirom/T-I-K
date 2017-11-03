using Akka.Actor;
using TIK.Integration.Batch;
using TIK.Applications.Membership.Mocks;

namespace TIK.Applications.Membership.Jobs
{
    public class JobActorProvider
    {
        private IActorRef JobsActor { get; set; }

        public JobActorProvider(ActorSystem actorSystem, IBatchPublisher batchPublisher)
        {
            this.JobsActor = actorSystem.ActorOf(Props.Create<JobActor>(batchPublisher), "jobs");
        }

        public IActorRef Get()
        {
            return JobsActor;
        }
    }
}
