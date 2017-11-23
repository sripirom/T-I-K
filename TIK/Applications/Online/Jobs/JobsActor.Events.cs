using TIK.Domain.Jobs;

namespace TIK.Applications.Online.Jobs
{
    public partial class JobsActor
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
        public class JobAdded : JobEvent
        {
            public readonly Job Job;

            public JobAdded(Job job)
            {
                this.Job = job;
            }
        }
        public class InsuffientJob : JobEvent {}
    }
}
