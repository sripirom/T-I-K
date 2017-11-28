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

        private readonly Uri _uri;

        public MemberPublisher(Uri uri)
        {
            _uri = uri;

            _getActiveMember = "CommonStock/GetList/{0}/{1}";
        }

        public Task<Member> Active(string token)
        {

            var client = new RestClient(_uri);

         
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
