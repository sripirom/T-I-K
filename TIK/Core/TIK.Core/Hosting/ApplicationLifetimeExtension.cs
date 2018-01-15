using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace TIK.Core.Hosting
{
    public static class ApplicationLifetimeExtension
    {
        public  static void  RegisterApplicationStopping(this IApplicationBuilder app, Action onShutdown)
        {
            var applicationLifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();
            applicationLifetime.ApplicationStopping.Register(onShutdown);
        }
    }
}
