using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Linq;

namespace TIK.Core.Governance.ServiceDiscovery
{
    public class ServiceDiscoveryEnvFactory
    {
        public static ServiceDiscoveryEnvs Get(ServiceDiscoveryOptions options)
        {
            var enpEptions = new ServiceDiscoveryEnvs();
            var endpoints = GetEnv(options.Endpoints);
            enpEptions.Endpoints = endpoints !=null? endpoints.Split(','):null;
            enpEptions.HealthCheckTemplate = GetEnv(options.HealthCheckTemplate);
            enpEptions.ServiceName = GetEnv(options.ServiceName);
            enpEptions.Consul = new ConsulOptions();
            enpEptions.Consul.HttpEndpoint = GetEnv(options.HttpEndpoint);
            enpEptions.Consul.DnsEndpoint = new DnsEndpoint();
            enpEptions.Consul.DnsEndpoint.Address = GetEnv(options.DnsEndpointAddress);
            enpEptions.Consul.DnsEndpoint.Port = Convert.ToInt32(GetEnv(options.DnsEndpointPort));
            return enpEptions;
        }

        private static string GetEnv(string name)
        {
            return Environment.GetEnvironmentVariable(name);
        }

    }
}
