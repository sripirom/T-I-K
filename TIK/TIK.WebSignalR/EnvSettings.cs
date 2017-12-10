using System;
using System.Net;

namespace TIK.WebSignalR
{
    public class EnvSettings
    {
        public static EnvSettings Instance()
        {
            return new EnvSettings();
        }
        public IPAddress IP
        {
            get { return IPAddress.Parse(Environment.GetEnvironmentVariable("TIK_WEBSIGNALR_IP")) ; }
        }

        public Int32 Port
        {
            get { return Int32.Parse(Environment.GetEnvironmentVariable("TIK_WEBSIGNALR_PORT")); }
        }

        public string WebPortalUrl
        {
            get { return Environment.GetEnvironmentVariable("TIK_WEBPORTAL_URL"); }
        }


    } 
}
