using System;
using Microsoft.Extensions.DependencyInjection;
using TIK.Domain.Membership;
using TIK.Integration.Batch;
using TIK.Integration.WebApi.Batch;
using System.IO;
using TIK.Integration.Identity;
using TIK.Integration.WebApi.Identity;
using TIK.Integration.Online;
using TIK.Integration.WebApi.Online;

namespace TIK.WebPortal
{
    public static class Services
    {
        public static void AddServiceCollection(this IServiceCollection services)
        {
            services.AddTransient<IIdentityTokenPublisher>(_ => new IdentityTokenPublisher(new Uri("http://localhost:5100")));
            services.AddTransient<ICommonStockPublisher>(_ => new CommonStockPublisher(new Uri("http://localhost:5101"))); 
        }
         

    }
}
