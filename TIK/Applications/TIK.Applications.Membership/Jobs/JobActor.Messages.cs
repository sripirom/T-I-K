namespace TIK.Applications.Membership.Jobs
{
    public partial class JobActor
    {
        public class GetAllJobs {}

        public class StatusJob {}

        public class RequestJob
        {
            public readonly string JobId;
            public readonly string Application;
            public readonly string Procedure;

            public RequestJob(string jobId = "", string application = "", 
                            string procedure = "")
            {
                this.JobId = jobId;
                this.Application = application;
                this.Procedure = procedure;
            }
        }

    }
}
