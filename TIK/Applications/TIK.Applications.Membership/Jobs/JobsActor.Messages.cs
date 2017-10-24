namespace TIK.Applications.Membership.Jobs
{
    public partial class JobsActor
    {
        public class GetAllJobs {}

        public class UpdateJob
        {
            public readonly int JobId;
            public readonly int AmountChanged;

            public UpdateJob(int jobId = 0, int amountChanged = 0)
            {
                this.JobId = jobId;
                this.AmountChanged = amountChanged;
            }
        }
    }
}
