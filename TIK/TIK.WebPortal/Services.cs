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
using DnsClient;
using System.Net;

namespace TIK.WebPortal
{
    public static class Services
    {
        public static void AddServiceCollection(this IServiceCollection services)
        {
            var dns = new LookupClient(IPAddress.Parse("127.0.0.1"), 8600);
            services.AddSingleton<IDnsQuery>(dns);

            services.AddTransient<IIdentityTokenPublisher>(_ => new IdentityTokenPublisher(dns));
            //services.AddSingleton<IdentityTokenPublisher>();
            services.AddTransient<ICommonStockPublisher>(_ => new CommonStockPublisher(new Uri(EnvSettings.Instance().OnlineUrl))); 
        }
         

    }
}
