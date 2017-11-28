using Microsoft.Extensions.DependencyInjection;

namespace TIK.Applications.Online.Members
{
    public static class Services
    {
        public static void AddMemberServices(this IServiceCollection services)
        {
            services.AddSingleton<Routes.ActiveMember>();
        }
    }
}
