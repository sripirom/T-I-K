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
        private readonly string _getInfo;
        private readonly string _getStockDiscussion;
        private readonly string _addStockDiscussionItem;
        private readonly Uri _uri;

        public CommonStockPublisher(Uri uri)
        {
            _uri = uri;

            _getCommonStocks = "CommonStock/GetList/{0}/{1}";
            _getInfo = "CommonStock/GetInfo/{0}";

            _getStockDiscussion = "CommonStock/{0}/discussion";
            _addStockDiscussionItem = "CommonStock/{0}/discussion";
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

        public Task<CommonStockInfo> GetInfo(int memberId, int stockId)
        {

            var client = new RestClient(_uri);

            string template = string.Format(_getInfo, stockId);

            var request = new RestRequest(template, Method.GET);

            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();

            var response = client.Execute(request);
            var content = response.Content; // raw content as string      

            var stockInfo = JsonConvert.DeserializeObject<CommonStockInfo>(content);

            return Task.FromResult<CommonStockInfo>(stockInfo);
        }

        public Task<IEnumerable<DiscussionItem>> GetStockDiscussion(int memberId, int stockId)
        {
            var client = new RestClient(_uri);

            string template = string.Format(_getStockDiscussion, stockId);

            var request = new RestRequest(template, Method.GET);

            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();

            var response = client.Execute(request);
            var content = response.Content; // raw content as string      

            var stockDiscussions = JsonConvert.DeserializeObject<IEnumerable<DiscussionItem>>(content);

            return Task.FromResult<IEnumerable<DiscussionItem>>(stockDiscussions);
        }

        public Task<DiscussionItem> AddStockDiscussionItem(Int32 memberId, Int32 stockId, DiscussionItem discussionItem)
        {
            var client = new RestClient(_uri);

            string template = string.Format(_addStockDiscussionItem, stockId);

            var request = new RestRequest(template, Method.POST);

            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddJsonBody(discussionItem);


            var response = client.Execute(request);
            var content = response.Content; // raw content as string      

            var stockDiscussion = JsonConvert.DeserializeObject<DiscussionItem>(content);

            return Task.FromResult<DiscussionItem>(stockDiscussion);
        }

    }
}
