using System.Configuration;
using DatabaseSchemaReader.Website.Configurations.Interfaces;

namespace DatabaseSchemaReader.Website.Configurations
{
    public class WebsiteConfigurations : ConfigurationSection, IWebsiteConfigurations
    {
        [ConfigurationProperty("isTestingEnviroment", IsRequired = true)]
        public bool IsTestingEnviroment
        {
            get { return (bool)this["isTestingEnviroment"]; }
            set { this["isTestingEnviroment"] = value; }
        }

        [ConfigurationProperty("connectionstringName", IsRequired = true)]
        public string ConnectionstringName
        {
            get { return (string)this["connectionstringName"]; }
            set { this["connectionstringName"] = value; }
        }
    }
}