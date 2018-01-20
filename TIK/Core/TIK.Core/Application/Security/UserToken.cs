using System;
namespace TIK.Core.Application.Security
{
    public class UserToken
    {
        public Int32 Id { get; set; }

        public string UserName { get; set; }

        public string TokenId { get; set; }

        public TimeSpan Expire { get; set; }
    }
}
