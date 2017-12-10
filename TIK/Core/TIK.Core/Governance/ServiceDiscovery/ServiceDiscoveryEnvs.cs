using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Linq;

namespace TIK.Core.Governance.ServiceDiscovery
{
    public class ServiceDiscoveryEnvs
    {
        public string ServiceName { get; set; }

        public ConsulOptions Consul { get; set; }

        public string HealthCheckTemplate { get; set; }

        public string[] Endpoints { get; set; }
    }

    public class ConsulOptions
    {
        public string HttpEndpoint { get; set; }

        public DnsEndpoint DnsEndpoint { get; set; }
    }

    public class DnsEndpoint
    {
        public string Address { get; set; }

        public int Port { get; set; }

        public IPEndPoint ToIPEndPoint()
        {
            if(!Regex.IsMatch(Address, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b")){
                var address = Dns.GetHostAddresses(Address).LastOrDefault(); 
                if(address!=null){
                    Address = address.ToString();
                }
            }
            return new IPEndPoint(IPAddress.Parse(Address), Port);
        }
    }
}
