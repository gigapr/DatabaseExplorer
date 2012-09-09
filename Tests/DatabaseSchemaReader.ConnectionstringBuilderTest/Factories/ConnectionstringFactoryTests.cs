using DatabaseSchemaReader.ConnectionstringBuilder.Factories;
using DatabaseSchemaReader.ConnectionstringBuilder.Factories.Interfaces;
using DatabaseSchemaReader.ConnectionstringBuilder.Strategies;
using DatabaseSchemaReader.ConnectionstringBuilder.Validators;
using DatabaseSchemaReader.Contract.BusinessObjects;
using NUnit.Framework;

namespace DatabaseSchemaReader.ConnectionstringBuilderTest.Factories
{
    [TestFixture, Category("Unit")]
    public class ConnectionstringFactoryTests
    {
        private IConnectionstringFactory _connectionstringFactory;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var connectionstringParametersValidator = new ConnectionstringArgumentsValidator();

            var sqlServerConnectionstringBuilderStrategy = new SqlServerConnectionstringBuilderStrategy(connectionstringParametersValidator);

            _connectionstringFactory = new ConnectionstringFactory(sqlServerConnectionstringBuilderStrategy);
        }

        [Test]
        public void Should_be_able_to_build_a_connection_string_for_sqlserver_databases()
        {
            var connectionstringArguments = new ConnectionstringArguments
            {
                Provider     = "SQLOLEDB",
                DataSource   = "localhost",
                DatabaseName = "TestDatabase",
                Password     = "password",
                Username     = "username"
            };

            var connectionstring = _connectionstringFactory.BuildConnection(connectionstringArguments);

            Assert.AreEqual("Provider=SQLOLEDB;Data Source=localhost;Initial Catalog=TestDatabase;User ID=username;Password=password;OLE DB Services=-4;", connectionstring);
        }
    }    
}
