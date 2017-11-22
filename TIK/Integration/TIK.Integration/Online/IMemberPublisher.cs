using System;
using System.Threading.Tasks;
using TIK.Domain.Membership;

namespace TIK.Integration.Online
{
    public interface IMemberPublisher
    {
        Task<Member> Active(string token);
    } 
}
