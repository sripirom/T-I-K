using Microsoft.Extensions.Logging;
using Akka.Actor;
using System.Threading.Tasks;

namespace TIK.Applications.Membership.JobSlots.Routes
{
    public class GetJobSlot
    {
        private ILogger<GetJobSlot> Logger { get; set; }
        private IActorRef JobSlotsActor { get; set; }

        public GetJobSlot(JobSlotsActorProvider provider, ILogger<GetJobSlot> logger)
        {
            this.Logger = logger;
            this.JobSlotsActor = provider.Get();
        }

        public async Task<JobSlot> Execute(int memberId) {
            Logger.LogInformation($"Requesting jobSlot of member '{memberId}'");
            return await this.JobSlotsActor.Ask<JobSlot>(new JobSlotActor.GetJobSlot(memberId));
        }
    }
}
