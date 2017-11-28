using Microsoft.Extensions.DependencyInjection;

namespace TIK.Applications.Online.Jobs
{
    public static class Services
    {
        public static void AddJobServices(this IServiceCollection services)
        {
            //services.AddSingleton<JobsActorProvider>();
            services.AddSingleton<Routes.GetAllJobs>();
        }
    }
}
