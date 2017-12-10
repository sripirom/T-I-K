using System;
using System.Net;
using System.Text.RegularExpressions;
using Xunit;
using System.Linq;
using DnsClient;
using TIK.Integration;

namespace TIK.UnitTests
{
    public class TestDns
    {
        public TestDns()
        {
        } 

        [Fact]
        public void GetIpFromDns()
        {
            string HostName = "localhost";  
            IPAddress[] ipaddress = Dns.GetHostAddresses(HostName); 
            foreach (var a in ipaddress)
            {
                Console.WriteLine(a.ToString());
            }
        }

        [Fact]
        public void DnsDiscovery()
        {

            var dnsAddress = "http://127.0.0.1:80";
            if (!Regex.IsMatch(dnsAddress, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b"))
            {
                var address = Dns.GetHostAddresses(dnsAddress).LastOrDefault();
                if (address != null)
                {
                    dnsAddress = address.ToString();
                }
            }

            dnsAddress = "192.168.1.36";
            var endpointDiscovery = new EndpointDiscovery(new LookupClient(IPAddress.Parse(dnsAddress),
                                                                           8600), new BaseDnsDiscovery("service.consul"));

            var uri = endpointDiscovery.Resolve("tik-online").Result;

            Assert.NotNull(uri);
        }
    }
}
