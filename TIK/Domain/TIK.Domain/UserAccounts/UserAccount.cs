using System;
using TIK.Core.Domain;

namespace TIK.Domain.UserAccounts
{
    public class UserAccount : BaseModel<Int32>
    {
        public UserAccount()
        {
        }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string TokenId { get; set; }

        public TimeSpan Expire { get; set; }

    }
}
