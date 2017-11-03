using TIK.Domain.Membership;

namespace TIK.Applications.Membership.Jobs
{
    public partial class JobActor
    {
        public abstract class JobEvent {}
        public class JobFound : JobEvent
        {
            public readonly Job Job;

            public JobFound(Job job) 
            {
                this.Job = job;
            }
        }

        public class JobNotFound : JobEvent {}
        public class JobUpdated : JobEvent
        {
            public readonly Job Job;

            public JobUpdated(Job job)
            {
                this.Job = job;
            }
        }
        public class InsuffientStock : JobEvent {}
    }
}
