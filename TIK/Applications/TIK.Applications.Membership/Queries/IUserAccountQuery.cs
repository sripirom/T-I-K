using System;
using TIK.Domain.Member;

namespace TIK.Applications.Membership.Queries
{
    public interface IUserAccountQuery
    {
        UserAccount GetUser(string username, string password);
    }
}
