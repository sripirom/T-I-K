namespace TIK.Applications.Membership.Jobs
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
        public class StockUpdated : JobEvent
        {
            public readonly Job Job;

            public StockUpdated(Job job)
            {
                this.Job = job;
            }
        }
        public class InsuffientStock : JobEvent {}
    }
}
