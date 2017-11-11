using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TIK.Domain.TheSet;
using TIK.Integration.Online;

namespace TIK.Integration.WebApi.Online
{
    public class CommonStockPublisher : ICommonStockPublisher
    {

        private readonly string _getAllJobs;
        private readonly string _getCommonStocks;

        private readonly Uri _uri;

        public CommonStockPublisher(Uri uri)
        {
            _uri = uri;

            _getCommonStocks = "CommonStock/GetList/{0}/{1}";
        }

        public Task<IEnumerable<CommonStock>> GetList(int startIndex, int pageSize)
        {
            
            var client = new RestClient(_uri);

            string template = string.Format(_getCommonStocks, startIndex, pageSize);

            var request = new RestRequest(template, Method.GET);

            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();

            var response = client.Execute(request);
            var content = response.Content; // raw content as string      
  
            var stocks = JsonConvert.DeserializeObject<IEnumerable<CommonStock>>(content);
             
            return Task.FromResult<IEnumerable<CommonStock>>(stocks);
        }
    }
}
