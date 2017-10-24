using System;

namespace TIK.Applications.Membership.JobSlots
{
    public partial class JobSlotActor
    {
        public abstract class JobSlotEvent {}

        public class ItemAdded : JobSlotEvent
        {
            public readonly Guid JobSlotItemId;

            public ItemAdded(Guid jobSlotItemId = new Guid()) 
            {
                this.JobSlotItemId = jobSlotItemId;
            }
        }

        public class NotInStock : JobSlotEvent {}
        public class JobNotFound : JobSlotEvent {}

        public class ItemRemoved : JobSlotEvent {}

        public class ItemNotFound : JobSlotEvent {}
    }
}
