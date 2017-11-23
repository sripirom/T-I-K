using System;
using System.Threading.Tasks;
using TIK.Domain.UserAccounts;

namespace TIK.Applications.Identity.Authentication
{
    public interface IUserAccountQuery
    {
        UserAccount GetUser(string username, string password);
    }
}
