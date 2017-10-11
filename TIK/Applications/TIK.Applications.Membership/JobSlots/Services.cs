using Microsoft.Extensions.DependencyInjection;

namespace TIK.Applications.Membership.JobSlots
{
    public static class Services
    {
        public static void AddJobSlotServices(this IServiceCollection services)
        {
            services.AddSingleton<JobSlotsActorProvider>();

            services.AddSingleton<Routes.GetJobSlot>();
            services.AddSingleton<Routes.AddItemToJobSlot>();
            services.AddSingleton<Routes.RemoveItemFromJobSlot>();
        }
    }
}
