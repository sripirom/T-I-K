namespace TIK.Domain.Jobs 
{
    public class Job 
    {
        public int Id { get; set; }
        public string Application { get; set; }
        public string Procedure { get; set; }
        public int PricePerUnit { get; set; }
        public int InQueue { get; set; }
    }
}

