using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TIK.Domain.TheSet;
using TIK.Integration.Online;

namespace TIK.Integration.WebApi.Online
{
    public class EodPublisher : IEodPublisher
    {
        private readonly string _getEods;
        private readonly string _serviceName;

        private readonly IEndpointDiscovery _dns;


        public EodPublisher(string serviceName, IEndpointDiscovery dns)
        {
            _dns = dns ?? throw new ArgumentNullException(nameof(dns));
            _serviceName = serviceName;
            _getEods = "Eod/GetList/{0}/{1}/{2}";

        }

        public Task<IEnumerable<Eod>> GetList(string symbol, DateTime startDate, DateTime endDate)
        {  

            var client = new RestClient(_dns.Resolve(_serviceName).Result);

            string template = string.Format(_getEods, symbol, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));

            var request = new RestRequest(template, Method.GET);

            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();

            var response = client.Execute(request);
            var content = response.Content; // raw content as string      

            var eods = JsonConvert.DeserializeObject<IEnumerable<Eod>>(content);

            return Task.FromResult<IEnumerable<Eod>>(eods);
        }
    }
}
