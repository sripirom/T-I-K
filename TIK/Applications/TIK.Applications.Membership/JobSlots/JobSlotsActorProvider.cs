using Akka.Actor;
using TIK.Applications.Membership.Jobs;

namespace TIK.Applications.Membership.JobSlots
{
    public class JobSlotsActorProvider
    {
        private IActorRef JobSlotsActorInstance { get; set; }

        public JobSlotsActorProvider(ActorSystem actorSystem, JobActorProvider provider)
        {
            var jobsActor = provider.Get();
            this.JobSlotsActorInstance = actorSystem.ActorOf(JobSlotsActor.Props(jobsActor), "jobSlots");
        }

        public IActorRef Get()
        {
            return this.JobSlotsActorInstance;
        }
    }
}
