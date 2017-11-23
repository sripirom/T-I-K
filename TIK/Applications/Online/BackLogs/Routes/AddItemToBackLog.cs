using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Akka.Actor;
using System;
using System.Threading.Tasks;

namespace TIK.Applications.Online.BackLogs.Routes
{
    public class AddItemToBackLog
    {
        private ILogger<GetBackLog> Logger { get; set; }
        private IActorRef BackLogsActor { get; set; }

        public AddItemToBackLog(BackLogsActorProvider provider, ILogger<GetBackLog> logger)
        {
            this.Logger = logger;
            this.BackLogsActor = provider.Get();
        }

        public async Task<IActionResult> Execute(int memberId, int productId, string command)
        {
            Logger.LogInformation($"Adding {command} of product '{productId}' to backLog for member '{memberId}'");
            var result = await this.BackLogsActor.Ask<BackLogActor.BackLogEvent>(new BackLogActor.AddItemToBackLog(
                memberId,
                productId,
                command
            ));

            if (result is BackLogActor.ItemAdded)
            {
                var e = result as BackLogActor.ItemAdded;
                return new CreatedResult($"/api/backLogs/{memberId}/", e.BackLogItemId);
            }
            else if (result is BackLogActor.JobNotFound || result is BackLogActor.NotInStock)
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
