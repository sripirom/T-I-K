using System;
using System.Net;

namespace TIK.WebPortal
{
    public class EnvSettings
    {
        public static EnvSettings Instance()
        {
            return new EnvSettings();
        }
        public IPAddress IP
        {
            get { return IPAddress.Parse(Environment.GetEnvironmentVariable("TIK_WEBPORTAL_IP")); }
        }

        public Int32 Port
        {
            get { return Int32.Parse(Environment.GetEnvironmentVariable("TIK_WEBPORTAL_PORT")); }
        }

        public string IdentityUrl
        {
            get { return Environment.GetEnvironmentVariable("TIK_IDENTITY_URL"); }
        }
        public string OnlineUrl
        {
            get { return Environment.GetEnvironmentVariable("TIK_ONLINE_URL"); }
        }

        public int ConsulDnsPort
        {
            get { return Convert.ToInt32(Environment.GetEnvironmentVariable("CONSUL_DNS_PORT")); }
        }

        public string ConsulDnsPublishAddress
        {
            get { return Environment.GetEnvironmentVariable("CONSUL_DNS_PUBLISH_ADDRESS"); }
        }

    } 
}
