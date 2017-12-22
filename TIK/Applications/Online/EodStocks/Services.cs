using System;
using Microsoft.Extensions.DependencyInjection;

namespace TIK.Applications.Online.EodStocks
{
    public static class Services
    {
        public static void AddEodServices(this IServiceCollection services)
        {
            services.AddSingleton<Routes.GetEods>();
        }
    }
}
