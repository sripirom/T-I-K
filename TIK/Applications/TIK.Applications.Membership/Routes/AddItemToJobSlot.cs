using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Akka.Actor;
using System;
using System.Threading.Tasks;
using TIK.Applications.Membership.JobSlots;
using TIK.Applications.Membership.Members;

namespace TIK.Applications.Membership.Routes
{
    public class AddItemToJobSlot
    {
        //private ILogger<JobSlotActor.GetJobSlot> Logger { get; set; }
        private IActorRef MemberControllerActor { get; set; }

        public AddItemToJobSlot(MemberActorProvider provider)
        {
            //this.Logger = logger;
            this.MemberControllerActor = provider.Get();
        }

        public async Task<IActionResult> Execute(int memberId, string jobId, string application)
        {
            //Logger.LogInformation($"Adding {application} of job '{jobId}' to basket for member '{memberId}'");
            var result = await this.MemberControllerActor.Ask<JobSlotActor.JobSlotEvent>(new JobSlotActor.AddItemToJobSlot(
                memberId,
                jobId,
                application
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
