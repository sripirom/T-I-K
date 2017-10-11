namespace TIK.Applications.Membership.Jobs
{
    public partial class JobsActor
    {
        public class GetAllJobs {}

        public class UpdateStock
        {
            public readonly int JobId;
            public readonly int AmountChanged;

            public UpdateStock(int jobId = 0, int amountChanged = 0)
            {
                this.JobId = jobId;
                this.AmountChanged = amountChanged;
            }
        }
    }
}
