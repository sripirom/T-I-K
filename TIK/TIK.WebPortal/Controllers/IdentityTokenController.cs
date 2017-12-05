using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DnsClient;
using Microsoft.AspNetCore.Mvc;
using TIK.Integration.Identity;
using TIK.WebPortal.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TIK.WebPortal.Controllers
{
    [Route("IdentityToken")]
    public class IdentityTokenController : Controller
    {

        public IIdentityTokenPublisher TokenPublisher { get; }

        public IdentityTokenController(IIdentityTokenPublisher tokenPublisher)
        {
            TokenPublisher = tokenPublisher;   
        }

        //password=password&grant_type=password&username=user1
        [Route("Authen")]
        [HttpPost]public IActionResult Post(string username, string password, string grant_type)
        {
            var taskResult = TokenPublisher.Authen(username, password);

            return Ok(taskResult.Result);

        }
    }


}
