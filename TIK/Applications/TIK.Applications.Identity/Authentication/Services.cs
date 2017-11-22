using Microsoft.Extensions.DependencyInjection;

namespace TIK.Applications.Identity.Authentication
{
    public static class Services
    {
        public static void AddAuthenticationServices(this IServiceCollection services)
        {
            services.AddSingleton<Routes.DescribeToken>();
            services.AddSingleton<Routes.GetToken>();
        }
    }
}
