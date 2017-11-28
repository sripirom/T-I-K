using System;
using System.Net;

namespace TIK.ProcessService.Identity
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

    } 
}
