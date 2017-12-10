using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TIK.Domain.Membership;
using TIK.Integration.Online;

namespace TIK.Integration.WebApi.Online
{
    public class MemberPublisher : IMemberPublisher
    {

        private readonly string _getActiveMember;

        private readonly IEndpointDiscovery _dns;
        private readonly string _serviceName;

        public MemberPublisher(string serviceName, IEndpointDiscovery dns)
        {
            _dns = dns ?? throw new ArgumentNullException(nameof(dns));
            _serviceName = serviceName;

            _getActiveMember = "CommonStock/GetList/{0}/{1}";
        }

        public Task<Member> Active(string token)
        {

            var client = new RestClient(_dns.Resolve(_serviceName).Result);

         
            var request = new RestRequest(_getActiveMember, Method.GET);

            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();

            var response = client.Execute(request);
            var content = response.Content; // raw content as string      

            var member = JsonConvert.DeserializeObject<Member>(content);

            return Task.FromResult<Member>(member);
        }


    }
}
