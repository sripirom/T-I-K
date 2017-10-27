using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Akka.Actor;
using System;
using System.Threading.Tasks;
using TIK.Applications.Membership.JobSlots;
using TIK.Applications.Membership.Members;

namespace TIK.Applications.Membership.Routes
{
    public class RemoveItemFromJobSlot
    {
        private ILogger<JobSlotActor.GetJobSlot> Logger { get; set; }
        private IActorRef MemberControllerActor { get; set; }

        public RemoveItemFromJobSlot(MemberActorProvider provider, ILogger<JobSlotActor.GetJobSlot> logger)
        {
            this.Logger = logger;
            this.MemberControllerActor = provider.Get();
        }

        public async Task<IActionResult> Execute(int memberId, Guid jobSlotItemId)
        {
            Logger.LogInformation($"Removing item {jobSlotItemId} from jobSlot of member {memberId}");
            var result = await this.MemberControllerActor.Ask<JobSlotActor.JobSlotEvent>(new JobSlotActor.RemoveItemFromJobSlot(
                memberId,
                jobSlotItemId
            ));

            if (result is JobSlotActor.ItemRemoved)
            {
                return new OkResult();
            }
            else if (result is JobSlotActor.ItemNotFound)
            {
                return new BadRequestResult();
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}
