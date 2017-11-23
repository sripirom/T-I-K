using System;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.Extensions.Logging;
using TIK.Domain.Membership;

namespace TIK.Applications.Online.Members.Routes
{
    public class ActiveMember
    {
        private ILogger<ActiveMember> Logger { get; set; }
        private IActorRef MemberControllerActor { get; set; }

        public ActiveMember(MemberActorProvider provider, ILogger<ActiveMember> logger)
        {
            this.Logger = logger;
            this.MemberControllerActor = provider.Get();
        }

        public async Task<Member> Execute(int memberId)
        {
            Logger.LogInformation($"Requesting jobSlot of member '{memberId}'");
            return await this.MemberControllerActor.Ask<Member>
               (new MemberActor.ActiveMember(memberId));
        }

    }
}
  