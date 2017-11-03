using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TIK.Domain.Membership;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using TIK.ProcessService.Authentication;
using TIK.Applications.Membership.JobSlots;
using static TIK.Applications.Membership.Members.MemberActor;
using static TIK.Applications.Membership.JobSlots.JobSlotActor;
using TIK.Applications.Membership.Routes;

namespace TIK.ProcessService.Membership.Controllers
{ 
    [Authorize]
    [Route("/api/member")]
    public class MemberController : Controller
    {
        private Applications.Membership.Routes.ActiveMember ActiveMember { get; }
        private Applications.Membership.Routes.GetJobSlot GetJobSlot { get; }
        private Applications.Membership.Routes.AddItemToJobSlot AddItemToJobSlot { get; }
        private Applications.Membership.Routes.RemoveItemFromJobSlot RemoveItemFromJobSlot { get; }
        private IMemberRepository MemberRepository { get; }

        public MemberController(Applications.Membership.Routes.ActiveMember activeMember, 
                                Applications.Membership.Routes.GetJobSlot getJobSlot, 
                                Applications.Membership.Routes.AddItemToJobSlot addItemToJobSlot,
                                Applications.Membership.Routes.RemoveItemFromJobSlot removeItemFromJobSlot, 
                                IMemberRepository memberRepository)
        {
            ActiveMember = activeMember;
            GetJobSlot = getJobSlot;
            AddItemToJobSlot = addItemToJobSlot;
            RemoveItemFromJobSlot = removeItemFromJobSlot;
            MemberRepository = memberRepository;
        }

       
        [HttpGet]
        public async Task<Member> Active()
        {
            var user = HttpContext.GetUserInfo();

            Member result=null;
            var member = MemberRepository.Get(user.Id);
            if(member!=null)
            {
                result = await this.ActiveMember.Execute(member, user.Id); 
            }else
            {
                
            }

            return result;
        }

        [HttpGet("jobs")]
        public async Task<JobSlot> Get()
        {
            var memberId = HttpContext.GetMemberId();
            var result = await this.GetJobSlot.Execute(memberId);
            return result;
        }

        [HttpPut("jobs/add")]
        public async Task<IActionResult> PutItem([FromBody] ApiDsl.AddItem item)
        {
            var memberId = HttpContext.GetMemberId();
            var result = await this.AddItemToJobSlot.Execute(memberId, item.JobId, item.Application);
            return result;
        }

        [HttpDelete("jobs/delete/{jobSlotItemId}")]
        public async Task<IActionResult> DeleteItem(Guid jobSlotItemId)
        {
            var memberId = HttpContext.GetMemberId();
            var result = await this.RemoveItemFromJobSlot.Execute(memberId, jobSlotItemId);
            return result;
        }


    }
}
