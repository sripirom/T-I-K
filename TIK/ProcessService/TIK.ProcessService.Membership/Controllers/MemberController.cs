using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TIK.Domain.Member;
using TIK.Applications.Membership.Members.Routes;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using TIK.ProcessService.Authentication;

namespace TIK.ProcessService.Membership.Controllers
{
    [Authorize]
    [Route("/api/member")]
    public class MemberController : Controller
    {
        private ActiveMember ActiveMember { get; }
        private IMemberRepository MemberRepository { get; }

        public MemberController(ActiveMember activeMember, IMemberRepository memberRepository)
        {
            ActiveMember = activeMember;
            MemberRepository = memberRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<Member> Active()
        {
            var user = HttpContext.GetUserInfo();

            Member result=null;
            var member = MemberRepository.Get(user.Id);
            if(member!=null)
            {
                result = await this.ActiveMember.Execute(member); 
            }else
            {
                
            }

            return result;
        }


    }
}
