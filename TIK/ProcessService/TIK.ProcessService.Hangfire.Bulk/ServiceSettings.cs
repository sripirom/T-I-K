using System;
using Microsoft.Extensions.Configuration;

namespace TIK.ProcessService.Hangfire.Bulk
{
    public class ServiceSettings
    {

        private static readonly Lazy<ServiceSettings> _instance = new Lazy<ServiceSettings>(() => new ServiceSettings());

        public static ServiceSettings Instance => _instance.Value;

        public IConfigurationRoot Configuration { get; }

        public ServiceSettings()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        /// <summary>
        /// Windows ServiceName
        /// </summary>
        public string ServiceName => Configuration["processService.server.serviceName"];
        /// <summary>
        /// Windows ServiceDisplayName
        /// </summary>
        public string ServiceDisplayName => Configuration["processService.server.serviceDisplayName"];
        /// <summary>
        /// Windows ServiceDescription
        /// </summary>
        public string ServiceDescription => Configuration["processService.server.serviceDescription"];
        /// <summary>
        /// Windows ServiceAddress
        /// </summary>
        public string ServiceAddress => Configuration["processService.server.serviceAddress"];

        /// <summary>
        /// App WebSite
        /// </summary>
        public string AppWebSite => Configuration["processService.server.website"];

        /// <summary>
        /// App WebSite
        /// </summary>
        public string AuthenValidIssuer => Configuration["processService.authen.validIssuer"];

        /// <summary>
        /// App WebSite
        /// </summary>
        public string AuthenValidAudience => Configuration["processService.authen.validAudience"];

        /// <summary>
        /// App WebSite
        /// </summary>
        public string AuthenIssuerSigningKey => Configuration["processService.authen.issuerSigningKey"];

        /// <summary>
        /// App WebSite
        /// </summary>
        public string AuthenClaimMembershipId => Configuration["processService.authen.claimMembershipId"];

        /// <summary>
        /// Hangfire sql server connectionstring
        /// </summary>
        public string HangfireSqlserverConnectionString => Configuration.GetConnectionString("hangfire.sqlserver");

        /// <summary>
        ///  Hangfire redis server connectionstring
        /// </summary>
        public string HangfireRedisConnectionString => Configuration.GetConnectionString("hangfire.redis");



    }
}
