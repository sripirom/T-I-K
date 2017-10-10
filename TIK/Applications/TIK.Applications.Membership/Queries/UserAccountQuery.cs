using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TIK.Domain.Member;

namespace TIK.Applications.Membership.Queries
{
    public class UserAccountQuery : IUserAccountQuery
    {
        private readonly IUserAccountRepository _userAccountReporitory;

        public UserAccountQuery(IUserAccountRepository userAccountReporitory)
        {
            _userAccountReporitory = userAccountReporitory;
        }

        public UserAccount GetUser(string username, string password)
        {

             IList<Tuple<Expression<Func<UserAccount, object>>, object>> paramValue =
                new List<Tuple<Expression<Func<UserAccount, object>>, object>>()
            {
                    new Tuple<Expression<Func<UserAccount, object>>, object>(q=>q.UserName, username),
                    new Tuple<Expression<Func<UserAccount, object>>, object>(q=>q.Password, password)
            };

            var user = _userAccountReporitory.Search(paramValue).FirstOrDefault();

            return user;
        }
    }
}
