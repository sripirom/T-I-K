using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TIK.Domain.UserAccounts;

namespace TIK.Applications.Identity.Authentication
{
    public class UserAccountQuery : IUserAccountQuery
    {
        private readonly IUserAccountRepository _userAccountRepository;

        public UserAccountQuery(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        public UserAccount GetUser(string username, string password)
        {
            if(String.IsNullOrEmpty(username)){
                throw new ArgumentException("username cannot be null.");   
            }
            if(String.IsNullOrEmpty(password)){
                throw new ArgumentException("password cannot be null.");  
            }

            IList<Tuple<Expression<Func<UserAccount, object>>, object>> paramValue =
                new List<Tuple<Expression<Func<UserAccount, object>>, object>>();

            paramValue.Add(new Tuple<Expression<Func<UserAccount, object>>, object>(q => q.UserName, username));
            paramValue.Add(new Tuple<Expression<Func<UserAccount, object>>, object>(q => q.Password, password));


            var user = _userAccountRepository.Search(paramValue).FirstOrDefault();

            return user;
        }
    }
}
