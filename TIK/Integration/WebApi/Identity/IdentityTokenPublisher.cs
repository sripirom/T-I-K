using System;
using System.Net.Http;
using System.Threading.Tasks;
using DnsClient;
using Newtonsoft.Json;
using RestSharp;
using TIK.Domain.Identity;
using TIK.Integration.Identity;
using System.Linq;


namespace TIK.Integration.WebApi.Identity
{
    public class IdentityTokenPublisher : IIdentityTokenPublisher
    { 

        private readonly string _postIdentityToken;

        private readonly IEndpointDiscovery _dns;
        private readonly string _serviceName;

        public IdentityTokenPublisher(string serviceName, IEndpointDiscovery dns)
        {
            _serviceName = serviceName;

            _dns = dns ?? throw new ArgumentNullException(nameof(dns));

            _postIdentityToken = "IdentityToken/Authen";
        }

        public Task<String> Authen(string username, string password)
        {

            var client = new RestClient(_dns.Resolve(_serviceName).Result);

            var request = new RestRequest(_postIdentityToken, Method.POST);

            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();

            var requestModel = new Login() { Username = username, Password = password };
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestModel), ParameterType.RequestBody);

            var response = client.Execute(request);
            var content = response.Content; // raw content as string      

            return Task.FromResult(content); 
   
  
        }


    }
}
    