using System;
namespace TIK.Domain.Member
{
    public class UserAccount
    {
        public UserAccount()
        {
        }
        public Guid Id
        {
            get;
            set;
        }
        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
    }
}
