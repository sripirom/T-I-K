using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Akka.Actor;
using System;
using System.Threading.Tasks;

namespace TIK.Applications.Online.BackLogs.Routes
{
    public class RemoveItemFromBackLog
    {
        private ILogger<GetBackLog> Logger { get; set; }
        private IActorRef BackLogsActor { get; set; }

        public RemoveItemFromBackLog(BackLogsActorProvider provider, ILogger<GetBackLog> logger)
        {
            this.Logger = logger;
            this.BackLogsActor = provider.Get();
        }

        public async Task<IActionResult> Execute(int memberId, Guid backLogItemId)
        {
            Logger.LogInformation($"Removing item {backLogItemId} from backLog of member {memberId}");
            var result = await this.BackLogsActor.Ask<BackLogActor.BackLogEvent>(new BackLogActor.RemoveItemFromBackLog(
                memberId,
                backLogItemId
            ));

            if (result is BackLogActor.ItemRemoved)
            {
                return new OkResult();
            }
            else if (result is BackLogActor.ItemNotFound)
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
