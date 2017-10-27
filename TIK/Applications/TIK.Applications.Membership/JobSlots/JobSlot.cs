using System.Collections.Generic;
using TIK.Domain.Membership;

namespace TIK.Applications.Membership.JobSlots 
{
    public class JobSlot {

        public List<JobItem> Items { get; set; } = new List<JobItem>();
    }
}
