using System;
using Microsoft.Extensions.DependencyInjection;

namespace TIK.Applications.Membership.Members
{
    public static class Services
    {
        public static void AddMemberControllerServices(this IServiceCollection services)
        {
            services.AddSingleton<MemberActorProvider>();

            services.AddSingleton<Routes.ActiveMember>();

        }
    }
}
