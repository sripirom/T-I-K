using System;
using Microsoft.Extensions.DependencyInjection;
using TIK.Domain.Member;
using TIK.Persistance.ElasticSearch.Repositories;
using TIK.ProcessService.Membership.Mock;

namespace TIK.ProcessService.Membership
{
    public static class Services
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IMemberRepository, MockMemberRepository>();


        }
    }
}
