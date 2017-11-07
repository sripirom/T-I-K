using System;
using System.Linq;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Principal;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using TIK.ProcessService.Authentication.Helpers;
using TIK.Domain.Membership;
using TIK.ProcessService.Authentication.Models;
using TIK.Applications.Authentication.Queries;
namespace TIK.ProcessService.Authentication.Controllers
{

    public class TokenController : Controller
    {
        private readonly IUserAccountQuery _userAccountQuery;
        public TokenController(IUserAccountQuery userAccountQuery)
        {
            _userAccountQuery = userAccountQuery;
        }

        [HttpPost]
        [Route("Token")]
        public IActionResult Create([FromBody]LoginInputModel inputModel)
        {
            var user = _userAccountQuery.GetUser(inputModel.Username, inputModel.Password);
            if (user == null)
                return Unauthorized();

            var memberIdKey = ServiceSettings.Instance.AuthenClaimMembershipId;

            var token = new JwtTokenBuilder()
                .AddConfiguration(inputModel.Username)
                .AddClaim(memberIdKey, user.Id.ToString())
                .Build();

            return Ok(token.Value);

        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var dict = new Dictionary<string, string>();

            HttpContext.User.Claims.ToList()
               .ForEach(item => dict.Add(item.Type, item.Value));

            return Ok(dict);
        }
      
      
    }
}
