using System;
namespace TIK.Applications.Messaging
{
    public class MessageWithJobId
    {
        public readonly string JobId;

        public MessageWithJobId(string jobId = "")
        {
            this.JobId = jobId;
        }
    }
}
