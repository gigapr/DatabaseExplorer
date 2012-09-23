using DatabaseSchemaReader.ConnectionstringBuilder.Factories;
using DatabaseSchemaReader.ConnectionstringBuilder.Factories.Interfaces;
using DatabaseSchemaReader.ConnectionstringBuilder.Strategies;
using DatabaseSchemaReader.Contract.Enums;
using NUnit.Framework;

namespace DatabaseSchemaReader.ConnectionstringBuilderTest.Factories
{
    [TestFixture, Category("Unit")]
    public class ConnectionstringBuilderFactoryTest
    {
        private IConnectionstringBuilderFactory _connectionstringBuilderFactory;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _connectionstringBuilderFactory = new ConnectionstringBuilderFactory();
        }

        [Test]
        public void Should_create_an_instance_of_accessconnectionstringbuilderstrategy_when_databasetype_is_access()
        {
            var strategy = _connectionstringBuilderFactory.Make(DatabaseType.Access);

            Assert.IsInstanceOf(typeof(AccessConnectionstringBuilderStrategy), strategy);
        }

        [Test]
        public void Should_create_an_instance_of_oracleconnectionstringbuilderstrategy_when_databasetype_is_oracle()
        {
            var strategy = _connectionstringBuilderFactory.Make(DatabaseType.Oracle);

            Assert.IsInstanceOf(typeof(OracleConnectionstringBuilderStrategy), strategy);
        }

        [Test]
        public void Should_create_an_instance_of_sqlserverconnectionstringbuilderstrategy_when_databasetype_is_sqlserver()
        {
            var strategy = _connectionstringBuilderFactory.Make(DatabaseType.SqlServer);

            Assert.IsInstanceOf(typeof(SqlServerConnectionstringBuilderStrategy), strategy);
        }
    }
}