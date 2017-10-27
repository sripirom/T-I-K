using System;

namespace TIK.Domain.Membership
{
    public class Job {
        public string Id { get; set; }
        public string Application { get; set; }
        public string Procedure { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
    }
}

 