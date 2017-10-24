using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace TIK.ProcessService.Authentication.Helpers
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
        public static SymmetricSecurityKey Create(IConfiguration configuration)
        {
            try
            {
                return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["processService.authen.issuerSigningKey"]));

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
