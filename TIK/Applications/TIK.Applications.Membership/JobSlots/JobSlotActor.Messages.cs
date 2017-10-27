using System;
using TIK.Applications.Messaging;

namespace TIK.Applications.Membership.JobSlots
{
    public partial class JobSlotActor
    {
        public class GetJobSlot : MessageWithMemberId {
            public GetJobSlot(int memberId = 0) : base(memberId) { }
        }

        public class AddItemToJobSlot : MessageWithMemberId {
            public readonly string JobId;
            public readonly string Application;
            public readonly string Procedure;

            public AddItemToJobSlot(int memberId = 0, string jobId = "", string application = "",
                                   string procedure = "") 
                : base(memberId)
            {
                this.JobId = jobId;
                this.Application = application;
                this.Procedure = procedure;
            }
        }

        public class RemoveItemFromJobSlot : MessageWithMemberId {
            public readonly Guid JobSlotItemId;

            public RemoveItemFromJobSlot(int memberId = 0, Guid JobSlotItemId = new Guid()) : base(memberId)
            {
                this.JobSlotItemId = JobSlotItemId;
            }
        }
    }
}
