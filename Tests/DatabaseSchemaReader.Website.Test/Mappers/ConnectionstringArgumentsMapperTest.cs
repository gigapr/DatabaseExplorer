using DatabaseSchemaReader.Website.Model;
using DatabaseSchemaReader.Website.Mappers;
using DatabaseSchemaReader.Website.Mappers.Interfaces;
using NUnit.Framework;

namespace DatabaseSchemaReader.Website.Test.Mappers
{
    [TestFixture, Category("Unit")]
    public class ConnectionstringArgumentsMapperTest
    {
        private IConnectionstringArgumentsMapper _connectionstringArgumentsMapper;
 
        [SetUp]
        public void SetUp()
        {
            _connectionstringArgumentsMapper = new ConnectionstringArgumentsMapper();    
        }

        [Test]
        public void Should_be_able_to_map_databaseconnection_to_connectionstringargument()
        {
            var databaseConnection = new DatabaseConnection
            {
                DataSource   = "DataSource",
                DatabaseName = "DatabaseName",
                Password     = "Password",
                Username     = "Username",
                Provider     = "Provider" 
            };

            var connectionstringArguments = _connectionstringArgumentsMapper.Map(databaseConnection);

            Assert.IsNotNull(connectionstringArguments);
            Assert.AreEqual("DataSource", connectionstringArguments.DataSource);
            Assert.AreEqual("DatabaseName", connectionstringArguments.DatabaseName);
            Assert.AreEqual("Provider", connectionstringArguments.Provider);
            Assert.AreEqual("Username", connectionstringArguments.Username);
            Assert.AreEqual("Password", connectionstringArguments.Password);
        }
    }
}