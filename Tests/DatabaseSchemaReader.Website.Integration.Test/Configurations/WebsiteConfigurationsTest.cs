using System.Configuration;
using DatabaseSchemaReader.Website.Configurations.Interfaces;
using NUnit.Framework;

namespace DatabaseSchemaReader.Website.Integration.Test.Configurations
{
    [TestFixture]
    public class WebsiteConfigurationsTest
    {
        private IWebsiteConfigurations _websiteConfigurations;

        [SetUp]
        public void SetUp()
        {
            _websiteConfigurations = (IWebsiteConfigurations)ConfigurationManager.GetSection("websiteConfiguration");
        }

        [Test]
        public void Should_be_able_to_map_istestingenviroment()
        {
            Assert.IsNotNull(_websiteConfigurations.IsTestingEnviroment);
            Assert.IsTrue(_websiteConfigurations.IsTestingEnviroment);
        }
    }
}