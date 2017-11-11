using System;
using System.Threading.Tasks;

namespace TIK.Integration.Identity
{
    public interface IIdentityTokenPublisher
    {
        Task<string> Authen(string username, string password);
    }
}
