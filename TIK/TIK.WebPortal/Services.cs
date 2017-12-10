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
using TIK.Integration;
using System.Text.RegularExpressions;
using System.Linq;

namespace TIK.WebPortal
{
    public static class Services
    {
        public static void AddServiceCollection(this IServiceCollection services)
        {
            var dnsAddress = EnvSettings.Instance().ConsulDnsAddress;
            var baseDomain = Environment.GetEnvironmentVariable("CONSUL_DNS_BASEDOMAIN");
            var onlineServiceName = Environment.GetEnvironmentVariable("TIK_ONLINE_SERVICENAME"); 
            var identityServiceName = Environment.GetEnvironmentVariable("TIK_IDENTITY_SERVICENAME");

            var baseDns = new BaseDnsDiscovery(baseDomain);

            if (!Regex.IsMatch(dnsAddress, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b"))
            {
                var address = Dns.GetHostAddresses(dnsAddress).LastOrDefault();
                if (address != null)
                {
                    dnsAddress = address.ToString();
                }
            }
            var endpointDiscovery = new EndpointDiscovery
                                (new LookupClient(IPAddress.Parse(dnsAddress),
                                 EnvSettings.Instance().ConsulDnsPort)
                                { UseTcpOnly = true }, baseDns);

            services.AddSingleton<EndpointDiscovery>(_ => endpointDiscovery);

            services.AddTransient<IIdentityTokenPublisher>(_ => new IdentityTokenPublisher(identityServiceName, endpointDiscovery));



            services.AddTransient<ICommonStockPublisher>(_ => new CommonStockPublisher(onlineServiceName, endpointDiscovery)); 
        }
         

    }
}
