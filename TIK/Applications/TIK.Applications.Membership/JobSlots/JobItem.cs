using System;

namespace TIK.Applications.Membership.JobSlots 
{
    public class JobItem 
    {
        public Guid Id { get; set; }

        public int JobId { get ; set; }

        public string Title { get; set; }

        public string Brand { get; set; }

        public int PricePerUnit { get; set; }

        public int Amount { get; set; }
    }
}
