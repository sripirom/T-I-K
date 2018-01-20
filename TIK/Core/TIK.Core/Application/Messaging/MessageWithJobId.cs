using System;
namespace TIK.Core.Application.Messaging
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
