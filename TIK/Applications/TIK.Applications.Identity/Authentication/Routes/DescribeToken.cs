using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;


namespace TIK.Applications.Identity.Authentication.Routes
{
    public class DescribeToken
    {
        private ILogger<DescribeToken> Logger { get; set; }

        public DescribeToken(ILogger<DescribeToken> logger)
        {
            this.Logger = logger;
        }

        public async Task<IDictionary<string, string>> Execute(ClaimsPrincipal userClaims)
        {
            Logger.LogInformation($"DescribeToken");
            var dict = new Dictionary<string, string>();


                userClaims.Claims.ToList()
              .ForEach(item => dict.Add(item.Type, item.Value));

            return await Task.FromResult(dict);
        }
    }
}
