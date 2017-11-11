using System;
namespace TIK.Applications.Identity.Authentication.Routes
{
    public class ApiDsl
    {
        public class LoginInputModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
