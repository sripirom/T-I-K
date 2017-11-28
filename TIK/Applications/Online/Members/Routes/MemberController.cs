using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TIK.Domain.Membership;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using TIK.Applications.Security;

namespace TIK.Applications.Online.Members.Routes
{ 
    [Authorize]
    [Route("/api/member")]
    public class MemberController : Controller
    {
        private ActiveMember ActiveMember { get; }


        public MemberController(ActiveMember activeMember)
        {
            ActiveMember = activeMember;
        }

       
        [HttpGet]
        public async Task<Member> Active()
        {
            var user = HttpContext.GetUserInfo();


           var result = await this.ActiveMember.Execute(user.Id); 
    

            return result;
        }

    }
}
