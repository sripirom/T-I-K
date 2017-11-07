using System;

namespace TIK.Applications.Online.BackLogs
{
    public partial class BackLogActor
    {
        public abstract class BackLogEvent {}

        public class ItemAdded : BackLogEvent
        {
            public readonly Guid BackLogItemId;

            public ItemAdded(Guid backLogItemId = new Guid()) 
            {
                this.BackLogItemId = backLogItemId;
            }
        }

        public class NotInStock : BackLogEvent {}
        public class JobNotFound : BackLogEvent {}

        public class ItemRemoved : BackLogEvent {}

        public class ItemNotFound : BackLogEvent {}
    }
}
