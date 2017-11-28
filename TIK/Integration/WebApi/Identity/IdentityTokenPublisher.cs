using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions;
using TIK.Domain.Identity;
using TIK.Domain.Jobs;
using TIK.Domain.SearchNews;
using TIK.Integration.Batch;
using TIK.Integration.Identity;

namespace TIK.Integration.WebApi.Identity
{
    public class IdentityTokenPublisher : IIdentityTokenPublisher
    { 
        private readonly HttpClient _client;
        private readonly string _getAllJobs;
        private readonly string _postSearchNews;

        private readonly Uri _uri;

        public IdentityTokenPublisher(Uri uri)
        {
            _uri = uri;
            _client = new HttpClient();
            _client.BaseAddress = uri;
            _postSearchNews = "IdentityToken/Authen";
        }

        public Task<String> Authen(string username, string password)
        {

            var client = new RestClient(_uri);

            var requestModel = new Login() { Username = username, Password = password };

            var request = new RestRequest(_postSearchNews, Method.POST);

            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestModel), ParameterType.RequestBody);

            var response = client.Execute(request);
            var content = response.Content; // raw content as string      

            return Task.FromResult(content);
        }

    }
}
    