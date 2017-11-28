using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace TIK.Applications.Security
{
    
    public static class JwtBearerAuthenticationExtension
    {
        
        public static void JwtBearerAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
  
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["processService.authen.validIssuer"],
                    ValidAudience = configuration["processService.authen.validAudience"],
                    IssuerSigningKey = JwtSecurityKey.Create(configuration["processService.authen.issuerSigningKey"])
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Member",
                                  policy => policy.RequireClaim(configuration["processService.authen.claimMembershipId"]));
            });

        }
    }

}
