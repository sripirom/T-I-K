using System;
using System.Net;
using System.Threading.Tasks;


namespace TIK.Integration
{
    public interface IEndpointDiscovery
    {
        Task<Uri> Resolve(string serviceName);
    }

    public class BaseDnsDiscovery
    {
        public BaseDnsDiscovery(string baseDomain, string tag = null)
        {
            BaseDomain = baseDomain;
            Tag = tag;
        }
        public string BaseDomain
        {
            get;
        }
        public string Tag
        {
            get;
        }
    
    }
}