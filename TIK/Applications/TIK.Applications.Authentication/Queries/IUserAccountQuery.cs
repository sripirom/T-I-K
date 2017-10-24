using System;
using TIK.Domain.UserAccounts;

namespace TIK.Applications.Authentication.Queries
{
    public interface IUserAccountQuery
    {
        UserAccount GetUser(string username, string password);
    }
}
