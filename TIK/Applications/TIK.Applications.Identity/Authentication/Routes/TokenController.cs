using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace TIK.Applications.Identity.Authentication.Routes
{

    [Route("IdentityToken")]
    public class TokenController : Controller
    {
        private GetToken GetToken { get; }
        private DescribeToken DescribeToken {get;}
        public TokenController(GetToken getToken, DescribeToken describeToken)
        {
            GetToken = getToken;
            DescribeToken = describeToken;
        }


        [Route("Authen")]
        [HttpPost] public IActionResult Create(string username, string password, string grant_type)
        {
            var taskAuthen = GetToken.Execute(username, password);

            if (taskAuthen.IsCompletedSuccessfully){
                var token = taskAuthen.Result;
                if(!string.IsNullOrEmpty(token))
                {
                    return Ok(token);
                }

            }
                
            return Unauthorized(); 

        }


        [Authorize]
        [Route("Describe")]
        [HttpGet] public IActionResult Describe()
        {
            var taskDict = DescribeToken.Execute(HttpContext.User);
  
            return Ok(taskDict.Result);
        }
      
      
    }
}
