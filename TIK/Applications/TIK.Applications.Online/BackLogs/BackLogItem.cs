using System;

namespace TIK.Applications.Online.BackLogs {
    public class BackLogItem 
    {
        public Guid Id { get; set; }

        public int JobId { get ; set; }

        public string Title { get; set; }

        public string Brand { get; set; }

        public int PricePerUnit { get; set; }

        public string Command { get; set; }
    }
}
