using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace TIK.Core.Application.Security
{
    public static class UserClaimsExtension
    {
        public static UserToken GetUserInfo(this HttpContext httpContext)
        {
            UserToken user = new UserToken();
            var dict = new Dictionary<string, string>();

            httpContext.User.Claims.ToList()
               .ForEach(item => dict.Add(item.Type, item.Value));

            user.Id = Convert.ToInt32(dict[ServiceSettings.Instance.AuthenClaimMembershipId]);
            user.UserName = dict[ServiceSettings.Instance.AuthenClaimUserName];
            user.TokenId = dict[ServiceSettings.Instance.AuthenClaimTokenId];
            user.Expire = new TimeSpan(Convert.ToInt64(dict[ServiceSettings.Instance.AuthenClaimExpire]));

            return user;
        }
        public static int GetMemberId(this HttpContext httpContext)
        {

            var memberIdClaim = httpContext.User.Claims.FirstOrDefault(c=> c.Type==ServiceSettings.Instance.AuthenClaimMembershipId);

            if(memberIdClaim != null){

                return Convert.ToInt32(memberIdClaim.Value);
            }else{
                return 0;
            }

        }
    }
}
