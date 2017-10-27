using Microsoft.Extensions.Logging;
using Akka.Actor;
using System.Threading.Tasks;
using TIK.Applications.Membership.JobSlots;
using TIK.Applications.Membership.Members;

namespace TIK.Applications.Membership.Routes
{
    public class GetJobSlot
    {
        private ILogger<GetJobSlot> Logger { get; set; }
        private IActorRef MemberControllerActor { get; set; }

        public GetJobSlot(MemberActorProvider provider, ILogger<GetJobSlot> logger)
        {
            this.Logger = logger;
            this.MemberControllerActor = provider.Get();
        }

        public async Task<JobSlot> Execute(int memberId) {
            Logger.LogInformation($"Requesting jobSlot of member '{memberId}'");
            return await this.MemberControllerActor.Ask<JobSlot>(new JobSlotActor.GetJobSlot(memberId));
        }
    }
}
