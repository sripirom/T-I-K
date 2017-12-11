using System;
using Microsoft.Extensions.Configuration;

namespace TIK.Core.Configure
{
    public class BaseSettings<TSettings>
        where TSettings : BaseSettings<TSettings>
    {
        private static readonly Lazy<TSettings> _instance = 
            new Lazy<TSettings>(() => (TSettings)System.Activator.CreateInstance(typeof(TSettings)));

        public static TSettings Instance => _instance.Value;


        public BaseSettings(string configFile = "appsettings.json")
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile(configFile, optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
    }
}
