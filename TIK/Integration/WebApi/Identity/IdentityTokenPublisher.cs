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

        private readonly string _getAllJobs;
        private readonly string _postSearchNews;

        private readonly IDnsQuery _dns;

        public IdentityTokenPublisher(IDnsQuery dns)
        {
            _dns = dns ?? throw new ArgumentNullException(nameof(dns));

            _postSearchNews = "IdentityToken/Authen";
        }

        public Task<String> Authen(string username, string password)
        {
            var uri = Route().Result;
            if(uri != null){
                var client = new RestClient(uri);

                var requestModel = new Login() { Username = username, Password = password };

                var request = new RestRequest(_postSearchNews, Method.POST);

                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", JsonConvert.SerializeObject(requestModel), ParameterType.RequestBody);

                var response = client.Execute(request);
                var content = response.Content; // raw content as string      

                return Task.FromResult(content); 
            }else{
                throw new Exception("Cannot route to enpoint.");
            }
  
        }

        public async Task<Uri> Route()
        {

            var dnsResult = await _dns.ResolveServiceAsync("service.consul", "identity");


            if (dnsResult.Length > 0)
            {
                var firstAddress = dnsResult.First().AddressList.FirstOrDefault();
                var port = dnsResult.First().Port;

                return new Uri($"http://{firstAddress}:{port}");
            }else{
                return null;
            }
        }

    }
}
    