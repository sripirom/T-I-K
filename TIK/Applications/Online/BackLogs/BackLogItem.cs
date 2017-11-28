using System;

namespace TIK.Applications.Online.BackLogs {
    public class BackLogItem 
    {
        public Guid Id { get; set; }

        public string JobId { get ; set; }

        public string Title { get; set; }

        public string Application { get; set; }

        public int PricePerUnit { get; set; }

        public string Procedure { get; set; }
    }
}
