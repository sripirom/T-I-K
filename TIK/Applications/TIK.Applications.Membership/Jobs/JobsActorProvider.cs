using Akka.Actor;
using TIK.Applications.Integration;
using TIK.Applications.Membership.Mocks;

namespace TIK.Applications.Membership.Jobs
{
    public class JobsActorProvider
    {
        private IActorRef JobsActor { get; set; }

        public JobsActorProvider(ActorSystem actorSystem, IBatchPublisher batchPublisher)
        {
            this.JobsActor = actorSystem.ActorOf(Props.Create<JobsActor>(batchPublisher), "jobs");
        }

        public IActorRef Get()
        {
            return JobsActor;
        }
    }
}
