using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TIK.Applications.Membership.JobSlots;
using TIK.Applications.Membership.JobSlots.Routes;

namespace TIK.ProcessService.Membership.Controllers
{
   [Route("/api/jobSlot")]
   public class JobSlotApiController
    {
        private GetJobSlot GetJobSlot { get; }
        private AddItemToJobSlot AddItemToJobSlot { get; }
        private RemoveItemFromJobSlot RemoveItemFromJobSlot { get; }

        public JobSlotApiController(GetJobSlot getJobSlot, AddItemToJobSlot addItemToJobSlot, RemoveItemFromJobSlot removeItemFromJobSlot)
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
            var result = await this.AddItemToJobSlot.Execute(memberId, item.JobId, item.Amount);
            return result;
        }

        [HttpDelete("{memberId}/items/{jobSlotItemId}")] public async Task<IActionResult> DeleteItem(int memberId, Guid jobSlotItemId)
        {
            var result = await this.RemoveItemFromJobSlot.Execute(memberId, jobSlotItemId);
            return result;
        }
    }
}
