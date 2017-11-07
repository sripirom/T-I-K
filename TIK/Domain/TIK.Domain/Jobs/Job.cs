using System;

namespace TIK.Domain.Jobs 
{
    public class Job 
    {
        public string Id { get; set; }
        public string Application { get; set; }
        public string Procedure { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        //public int PricePerUnit { get; set; }
        public int InQueue { get; set; }
    }
}

