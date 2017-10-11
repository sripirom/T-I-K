using Akka.Actor;

namespace TIK.Applications.Membership.Jobs
{
    public class JobsActorProvider
    {
        private IActorRef JobsActor { get; set; }

        public JobsActorProvider(ActorSystem actorSystem)
        {
            var jobs = SampleData.Get(); // set sample jobs
            this.JobsActor = actorSystem.ActorOf(Props.Create<JobsActor>(jobs), "jobs");
        }

        public IActorRef Get()
        {
            return JobsActor;
        }
    }
}
