using System;
namespace TIK.Core.ServiceDiscovery
{
    public class ServiceDiscoveryOptions
    {
        public string ServiceName { get; set; }
        public string HealthCheckTemplate { get; set; }
        public string Endpoints { get; set; }
        public string HttpEndpoint { get; set; }
        public string DnsEndpointAddress { get; set; }
        public string DnsEndpointPort { get; set; }
    }
}
