using System.Configuration;

namespace Maplink.Webservices.Places.Client.Wrappers
{
    public class ConfigurationWrapper : IConfigurationWrapper
    {
        public string ValueFor(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}