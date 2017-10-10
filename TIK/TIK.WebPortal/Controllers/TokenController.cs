using System;
using System.Linq;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Principal;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TIK.WebPortal.Helpers;
using TIK.WebPortal.Models;
using System.Collections.Generic;
using TIK.Applications.Membership.Queries;

namespace TIK.WebPortal.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly IUserAccountQuery _userAccountQuery;
        public TokenController(IUserAccountQuery userAccountQuery)
        {
            _userAccountQuery = userAccountQuery;
        }

        [HttpPost]
        public IActionResult Create([FromBody]LoginInputModel inputModel)
        {
            if (_userAccountQuery.GetUser(inputModel.Username, inputModel.Password) == null)
                return Unauthorized();

            var token = new JwtTokenBuilder()
                .AddSecurityKey(JwtSecurityKey.Create("fiver-secret-key"))
                                .AddSubject("james bond")
                                .AddIssuer("Fiver.Security.Bearer")
                                .AddAudience("Fiver.Security.Bearer")
                                .AddClaim("MembershipId", "111")
                                .AddExpiry(1)
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
