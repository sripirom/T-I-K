using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TIK.Applications.Membership.JobSlots;
using TIK.Applications.Membership.Routes;
using static TIK.Applications.Membership.JobSlots.JobSlotActor;

namespace TIK.ProcessService.Membership.Controllers
{
   [Route("/api/jobSlot")]
   public class JobSlotApiController
    { 
        private Applications.Membership.Routes.GetJobSlot GetJobSlot { get; }
        private Applications.Membership.Routes.AddItemToJobSlot AddItemToJobSlot { get; }
        private Applications.Membership.Routes.RemoveItemFromJobSlot RemoveItemFromJobSlot { get; }

        public JobSlotApiController(Applications.Membership.Routes.GetJobSlot getJobSlot, 
                                    Applications.Membership.Routes.AddItemToJobSlot addItemToJobSlot, 
                                    Applications.Membership.Routes.RemoveItemFromJobSlot removeItemFromJobSlot)
        {
            this.GetJobSlot = getJobSlot;
            this.AddItemToJobSlot = addItemToJobSlot;
            this.RemoveItemFromJobSlot = removeItemFromJobSlot;
        }
        [HttpGet("{memberId}")] public async Task<JobSlot> Get(int memberId)
        {
            var result = await this.GetJobSlot.Execute(memberId);
            return result;
        }

        [HttpPut("{memberId}/items")] public async Task<IActionResult> PutItem(int memberId, [FromBody] ApiDsl.AddItem item)
        {
            var result = await this.AddItemToJobSlot.Execute(memberId, item.JobId, item.Application);
            return result;
        }

        [HttpDelete("{memberId}/items/{jobSlotItemId}")] public async Task<IActionResult> DeleteItem(int memberId, Guid jobSlotItemId)
        {
            var result = await this.RemoveItemFromJobSlot.Execute(memberId, jobSlotItemId);
            return result;
        }
    }
}
