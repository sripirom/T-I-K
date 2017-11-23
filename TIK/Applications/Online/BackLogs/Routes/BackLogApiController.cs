using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TIK.Applications.Online.BackLogs.Routes
{
   [Route("/api/backLog")]
   public class BackLogApiController
    {
        private GetBackLog GetBackLog { get; }
        private AddItemToBackLog AddItemToBackLog { get; }
        private RemoveItemFromBackLog RemoveItemFromBackLog { get; }

        public BackLogApiController(GetBackLog getBackLog, AddItemToBackLog addItemToBackLog, RemoveItemFromBackLog removeItemFromBackLog)
        {
            this.GetBackLog = getBackLog;
            this.AddItemToBackLog = addItemToBackLog;
            this.RemoveItemFromBackLog = removeItemFromBackLog;
        }
        [HttpGet("{memberId}")] public async Task<BackLog> Get(int memberId)
        {
            var result = await this.GetBackLog.Execute(memberId);
            return result;
        }

        [HttpPut("{memberId}/items")] public async Task<IActionResult> PutItem(int memberId, [FromBody] ApiDsl.AddItem item)
        {
            var result = await this.AddItemToBackLog.Execute(memberId, item.JobId, item.Command);
            return result;
        }

        [HttpDelete("{memberId}/items/{backLogItemId}")] public async Task<IActionResult> DeleteItem(int memberId, Guid backLogItemId)
        {
            var result = await this.RemoveItemFromBackLog.Execute(memberId, backLogItemId);
            return result;
        }
    }
}
