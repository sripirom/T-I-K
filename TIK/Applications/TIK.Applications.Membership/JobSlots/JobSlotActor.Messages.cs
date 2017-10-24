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
            public readonly int JobId;
            public readonly int Amount;

            public AddItemToJobSlot(int memberId = 0, int jobId = 0, int amount = 0) : base(memberId)
            {
                this.JobId = jobId;
                this.Amount = amount;
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
