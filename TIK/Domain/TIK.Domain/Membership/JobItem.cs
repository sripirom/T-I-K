using System;

namespace TIK.Domain.Membership 
{
    public class JobItem 
    {
        public Guid Id { get; set; }

        public string JobId { get ; set; }

        public string Application { get; set; }

        public string Procedure { get; set; }

        public DateTime Created { get; set; }

    }
}
