using Microsoft.Extensions.Logging;
using Akka.Actor;
using System.Threading.Tasks;

namespace TIK.Applications.Online.BackLogs.Routes
{
    public class GetBackLog
    {
        private ILogger<GetBackLog> Logger { get; set; }
        private IActorRef BackLogsActor { get; set; }

        public GetBackLog(BackLogsActorProvider provider, ILogger<GetBackLog> logger)
        {
            this.Logger = logger;
            this.BackLogsActor = provider.Get();
        }

        public async Task<BackLog> Execute(int memberId) {
            Logger.LogInformation($"Requesting backLog of member '{memberId}'");
            return await this.BackLogsActor.Ask<BackLog>(new BackLogActor.GetBackLog(memberId));
        }
    }
}
