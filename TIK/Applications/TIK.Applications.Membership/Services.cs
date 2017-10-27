using System;
using Microsoft.Extensions.DependencyInjection;
using TIK.Applications.Integration;
using TIK.Applications.Membership.Jobs;
using TIK.Applications.Membership.JobSlots;
using TIK.Applications.Membership.Members;
using TIK.Applications.Membership.Mocks;

namespace TIK.Applications.Membership
{
    public static class Services
    {
        public static void AddMembershipServices(this IServiceCollection services)
        {
            services.AddSingleton<MemberActorProvider>();

            services.AddSingleton<Routes.ActiveMember>();

            services.AddSingleton<JobSlotsActorProvider>();

            services.AddSingleton<Routes.GetJobSlot>();
            services.AddSingleton<Routes.AddItemToJobSlot>();
            services.AddSingleton<Routes.RemoveItemFromJobSlot>();
       
        
            services.AddTransient<IBatchPublisher, MockBatchPublisher>();
            services.AddSingleton<JobsActorProvider>();
            services.AddSingleton<Routes.GetAllJobs>();
        }
    }
}
