using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Akka.Actor;
using System;
using System.Threading.Tasks;

namespace TIK.Applications.Membership.JobSlots.Routes
{
    public class AddItemToJobSlot
    {
        private ILogger<JobSlotActor.GetJobSlot> Logger { get; set; }
        private IActorRef JobSlotsActor { get; set; }

        public AddItemToJobSlot(JobSlotsActorProvider provider, ILogger<JobSlotActor.GetJobSlot> logger)
        {
            this.Logger = logger;
            this.JobSlotsActor = provider.Get();
        }

        public async Task<IActionResult> Execute(int memberId, int jobId, int amount)
        {
            Logger.LogInformation($"Adding {amount} of job '{jobId}' to basket for member '{memberId}'");
            var result = await this.JobSlotsActor.Ask<JobSlotActor.JobSlotEvent>(new JobSlotActor.AddItemToJobSlot(
                memberId,
                jobId,
                amount
            ));

            if (result is JobSlotActor.ItemAdded)
            {
                var e = result as JobSlotActor.ItemAdded;
                return new CreatedResult($"/api/baskets/{memberId}/", e.JobSlotItemId);
            }
            else if (result is JobSlotActor.JobNotFound || result is JobSlotActor.NotInStock)
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
