using Microsoft.Extensions.DependencyInjection;

namespace TIK.Applications.Online.BackLogs
{
    public static class Services
    {
        public static void AddBackLogServices(this IServiceCollection services)
        {
            //services.AddSingleton<BackLogsActorProvider>();

            services.AddSingleton<Routes.GetBackLog>();
            services.AddSingleton<Routes.AddItemToBackLog>();
            services.AddSingleton<Routes.RemoveItemFromBackLog>();
        }
    }
}
