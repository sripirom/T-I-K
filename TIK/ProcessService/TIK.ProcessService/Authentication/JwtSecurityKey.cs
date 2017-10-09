using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TIK.ProcessService.Authentication
{
    public static class JwtSecurityKey
    {
        public static SymmetricSecurityKey Create(string secret)
        {
            try
            {
                return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
