using System;
namespace TIK.WebPortal.Models
{
    public class IdentityTokenModel
    {
        public class LogIn
        {
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
}
