using System;
using System.Linq;
using System.Threading.Tasks;
using DnsClient;

namespace TIK.Integration
{
    public class EndpointDiscovery : IEndpointDiscovery
    {
        private readonly IDnsQuery _dns;
        private readonly BaseDnsDiscovery _baseDnsDiscovery;
        public EndpointDiscovery(IDnsQuery dns, BaseDnsDiscovery baseDnsDiscovery)
        {
            _dns = dns ?? throw new ArgumentNullException(nameof(dns));
            _baseDnsDiscovery = baseDnsDiscovery ?? throw new ArgumentNullException(nameof(baseDnsDiscovery));
        }

        public async Task<Uri> Resolve(string serviceName)
        {
             
            var dnsResult = await _dns.ResolveServiceAsync(_baseDnsDiscovery.BaseDomain, serviceName);


            if (dnsResult.Length > 0)
            {
                Console.WriteLine("Founded Dns");

                string address = "";
                var service = dnsResult.First();
                var firstAddress = service.AddressList.FirstOrDefault();
                if(firstAddress == null)
                {
                    address = service.HostName;
                }else{
                    address = firstAddress.ToString();
                }

                var port = dnsResult.First().Port;

                return new Uri($"http://{address}:{port}");
            }
            else
            {
                throw new EndpointDiscoveryException();
            }
        }
    }
}
