using System;
using Microsoft.Extensions.DependencyInjection;
using TIK.Domain.Membership;
using TIK.Integration.Batch;
using TIK.Persistance.ElasticSearch.Repositories;
using TIK.ProcessService.Membership.Mock;
using TIK.Integration.WebApi.Batch;
namespace TIK.ProcessService.Membership
{
    public static class Services
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IMemberRepository, MockMemberRepository>();

            services.AddSingleton<IBatchPublisher>(_=> new BatchPublisher(new Uri("http://localhost:5102")));
        }
    }
}
