using System;
using System.Net;

namespace TIK.ProcessService.Online
{
    public class EnvSettings
    {
        public static EnvSettings Instance()
        {
            return new EnvSettings();
        }
        public IPAddress IP
        {
            get { return IPAddress.Parse(Environment.GetEnvironmentVariable("TIK_ONLINE_IP")) ; }
        }

        public Int32 Port
        {
            get { return Int32.Parse(Environment.GetEnvironmentVariable("TIK_ONLINE_PORT")); }
        }

        public string HuconFileName 
        { 
            get { return Environment.GetEnvironmentVariable("TIK_ONLINE_HUCON"); }
        }

        public string AkkaAddress
        {
            get { return Environment.GetEnvironmentVariable("TIK_AKKASEED_ADDR"); }
        }

        public string BatchUrl
        {
            get { return Environment.GetEnvironmentVariable("TIK_BATCH_URL"); }
        }

        public string ActorSystem
        {
            get { return Environment.GetEnvironmentVariable("TIK_ACTORSYSTEM"); }
        }
    } 
}
