namespace TIK.Applications.Online.Jobs
{
    public partial class JobsActor
    {
        public class GetAllJobs {}

        public class AddJob
        {
            public readonly int JobId;
            public readonly string Command;

            public AddJob(int jobId = 0, string command = "")
            {
                this.JobId = jobId;
                this.Command = command;
            }
        }
    }
}
