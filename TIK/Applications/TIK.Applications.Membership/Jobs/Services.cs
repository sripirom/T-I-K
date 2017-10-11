using Microsoft.Extensions.DependencyInjection;

namespace TIK.Applications.Membership.Jobs
{
    public static class Services
    {
        public static void AddJobServices(this IServiceCollection services)
        {
            services.AddSingleton<JobsActorProvider>();
            services.AddSingleton<Routes.GetAllJobs>();
        }
    }
}
