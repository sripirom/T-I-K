using System;
using TIK.Applications.Messaging;

namespace TIK.Applications.Online.BackLogs
{
    public partial class BackLogActor
    {
        public class GetBackLog : MessageWithMemberId 
        {
            public GetBackLog(int memberId = 0) : base(memberId) { }
        }

        public class AddItemToBackLog : MessageWithMemberId {
            public readonly int JobId;
            public readonly string Command;

            public AddItemToBackLog(int memberId = 0, int jobId = 0, string command = "") : base(memberId)
            {
                this.JobId = jobId;
                this.Command = command;
            }
        }

        public class RemoveItemFromBackLog : MessageWithMemberId {
            public readonly Guid BackLogItemId;

            public RemoveItemFromBackLog(int memberId = 0, Guid backLogItemId = new Guid()) : base(memberId)
            {
                this.BackLogItemId = backLogItemId;
            }
        }
    }
}
