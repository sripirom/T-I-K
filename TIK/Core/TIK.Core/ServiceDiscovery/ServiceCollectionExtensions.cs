using System;
using System.Net;
using Consul;
using DnsClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Text.RegularExpressions;

namespace TIK.Core.ServiceDiscovery
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceDiscovery(this IServiceCollection services, IConfiguration serviceOptionsConfiguration)
        {
            services.AddOptions();

            // setup options
            services.Configure<ServiceDiscoveryOptions>(serviceOptionsConfiguration);

            // register consul client
            services.AddSingleton<IConsulClient>(p => new ConsulClient(cfg =>
            {
               
                var options = p.GetRequiredService<IOptions<ServiceDiscoveryOptions>>().Value;

                var serviceConfiguration = ServiceDiscoveryEnvFactory.Get(options);
               

                if (!string.IsNullOrEmpty(serviceConfiguration.Consul.HttpEndpoint))
                {
                    // if not configured, the client will use the default value "127.0.0.1:8500"
                    cfg.Address = new Uri(serviceConfiguration.Consul.HttpEndpoint); 
                }
            }));

            // register dns lookup
            services.AddSingleton<IDnsQuery>(p =>
            {
                var options = p.GetRequiredService<IOptions<ServiceDiscoveryOptions>>().Value;

                var serviceConfiguration = ServiceDiscoveryEnvFactory.Get(options);

                var client = new LookupClient(IPAddress.Parse("127.0.0.1"), 8600);

                if (serviceConfiguration.Consul.DnsEndpoint != null)
                {
                    client = new LookupClient(serviceConfiguration.Consul.DnsEndpoint.ToIPEndPoint());
                }

                client.EnableAuditTrail = false;
                client.UseCache = true;
                client.MinimumCacheTimeout = TimeSpan.FromSeconds(1);
                return client;
            });

            return services;
        }
    }
}
