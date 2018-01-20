using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TIK.Applications.Identity.Authentication;
using TIK.Applications.Identity.JwtSecurity;
using TIK.Domain.UserAccounts;
using Application = TIK.Core.Application;

namespace TIK.Applications.Identity.Authentication.Routes
{
    public class GetToken
    {
        private ILogger<GetToken> Logger { get; set; }
        private IUserAccountQuery UserAccountQuery { get; set; }

        public GetToken(IUserAccountQuery userAccountQuery, ILogger<GetToken> logger)
        {
            this.Logger = logger;
            this.UserAccountQuery = userAccountQuery;
        }

        public async Task<String> Execute(string userName, string password)
        {
            Logger.LogInformation($"GetToken of userName '{userName}'");
            var user = UserAccountQuery.GetUser(userName, password);
            if (user == null)
                return await Task.FromResult("");

            var memberIdKey = Application.ServiceSettings.Instance.AuthenClaimMembershipId;
             
            var token = new JwtTokenBuilder()
                .AddConfiguration(userName)
                .AddClaim(memberIdKey, user.Id.ToString())
                .Build();
            
            return await Task.FromResult(token.Value);
        }
    }
}
