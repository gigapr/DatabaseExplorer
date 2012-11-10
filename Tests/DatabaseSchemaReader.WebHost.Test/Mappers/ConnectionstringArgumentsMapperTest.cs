using DatabaseSchemaReader.Contract.Enums;
using DatabaseSchemaReader.WebHost.Exceptions;
using DatabaseSchemaReader.WebHost.Mappers;
using DatabaseSchemaReader.WebHost.Mappers.Interfaces;
using NUnit.Framework;

namespace DatabaseSchemaReader.WebHost.Test.Mappers
{
    [TestFixture, Category("Unit")]
    public class ConnectionstringArgumentsMapperTest
    {
        private IConnectionstringArgumentsMapper _connectionstringArgumentsMapper;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _connectionstringArgumentsMapper = new ConnectionstringArgumentsMapper();
        }

        [Test]
        public void Should_be_able_to_map_to_connectionstringargument()
        {
            const string databaseType = "SqlServer";
            const string dataSource   = "DataSource";
            const string databaseName = "DatabaseName";
            const string username     = "Username";
            const string password     = "Password";
            const string provider     = "SQLOLEDB";

            var connectionstringArguments = _connectionstringArgumentsMapper.Map(databaseType, provider, dataSource, databaseName, username, password);

            Assert.IsNotNull(connectionstringArguments);
            Assert.AreEqual(DatabaseType.SqlServer, connectionstringArguments.DatabaseType);
            Assert.AreEqual("SQLOLEDB", connectionstringArguments.Provider);
            Assert.AreEqual("DataSource", connectionstringArguments.DataSource);
            Assert.AreEqual("DatabaseName", connectionstringArguments.DatabaseName);
            Assert.AreEqual("Username", connectionstringArguments.Username);
            Assert.AreEqual("Password", connectionstringArguments.Password);
        }

        [Test]
        public void Should_be_able_to_map_to_connectionstringargument_when_username_and_password_are_missing()
        {
            const string databaseType = "SqlServer";
            const string dataSource   = "DataSource";
            const string databaseName = "DatabaseName";
            const string provider     = "Provider";

            var connectionstringArguments = _connectionstringArgumentsMapper.Map(databaseType, provider, dataSource, databaseName, null, null);

            Assert.IsNotNull(connectionstringArguments);
            Assert.AreEqual(DatabaseType.SqlServer, connectionstringArguments.DatabaseType);
            Assert.AreEqual("DataSource", connectionstringArguments.DataSource);
            Assert.AreEqual("DatabaseName", connectionstringArguments.DatabaseName);
        }

        [TestCase("SqlServer", Result = DatabaseType.SqlServer)]
        [TestCase("sqlServer", Result = DatabaseType.SqlServer)]
        [TestCase("sqlserver", Result = DatabaseType.SqlServer)]
        public DatabaseType Should_be_able_to_map_databasetype_indipendently_by_the_case(string databaseType)
        {
            const string dataSource = "DataSource";
            const string databaseName = "DatabaseName";
            const string username = "Username";
            const string password = "Password";

            var connectionstringArguments = _connectionstringArgumentsMapper.Map(databaseType, dataSource, databaseName, username, password);

            return connectionstringArguments.DatabaseType;
        }

        [Test, ExpectedException(typeof(DatabaseTypeException), ExpectedMessage = "Database Type 'Invalid' is not supported.")]
        public void Should_throw_an_exception_for_not_supported_database_type()
        {
            const string databaseType = "Invalid";
            const string dataSource   = "DataSource";
            const string databaseName = "DatabaseName";
            const string username     = "Username";
            const string password     = "Password";

            var connectionstringArguments = _connectionstringArgumentsMapper.Map(databaseType, dataSource, databaseName, username, password);

            Assert.IsNotNull(connectionstringArguments);
        }
    }
}