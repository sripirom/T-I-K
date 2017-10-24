using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using TIK.Domain.UserAccounts;

namespace TIK.ProcessService.Authentication
{
    public static class UserClaimsExtension
    {
        public static UserAccount GetUserInfo(this HttpContext httpContext)
        {
            UserAccount user = new UserAccount();
            var dict = new Dictionary<string, string>();

            httpContext.User.Claims.ToList()
               .ForEach(item => dict.Add(item.Type, item.Value));

            user.Id = Convert.ToInt32(dict[ServiceSettings.Instance.AuthenClaimMembershipId]);
            user.UserName = dict[ServiceSettings.Instance.AuthenClaimUserName];
            user.TokenId = dict[ServiceSettings.Instance.AuthenClaimTokenId];
            user.Expire = new TimeSpan(Convert.ToInt64(dict[ServiceSettings.Instance.AuthenClaimExpire]));

            return user;
        }
    }
}
