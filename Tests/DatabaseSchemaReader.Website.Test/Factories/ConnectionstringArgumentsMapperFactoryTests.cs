using System;
using DatabaseSchemaReader.Website.Factories;
using DatabaseSchemaReader.Website.Factories.Interfaces;
using DatabaseSchemaReader.Website.Mappers;
using NUnit.Framework;

namespace DatabaseSchemaReader.Website.Test.Factories
{
    [TestFixture]
    public class ConnectionstringArgumentsMapperFactoryTests
    {
        private IConnectionstringArgumentsMapperFactory _connectionstringArgumentsMapperFactory;

        [SetUp]
        public void SetUp()
        {
            _connectionstringArgumentsMapperFactory = new ConnectionstringArgumentsMapperFactory();
        }

        [Test]
        public void Make_should_return_a_sqlconnectionstringargumentsmapper_when_databasetype_is_sqlserver()
        {
            const string databaseType = "SqlServer";

            var connectionstringArgumentsMapper = _connectionstringArgumentsMapperFactory.Make(databaseType);

            Assert.IsInstanceOf<SqlServerConnectionstringArgumentsMapper>(connectionstringArgumentsMapper);
        }

        [Test]
        public void Make_should_return_a_accessconnectionstringargumentsmapper_when_databasetype_is_access()
        {
            const string databaseType = "Access";

            var connectionstringArgumentsMapper = _connectionstringArgumentsMapperFactory.Make(databaseType);

            Assert.IsInstanceOf<AccessConnectionstringArgumentsMapper>(connectionstringArgumentsMapper);
        }

        [Test]
        public void Make_should_return_a_connectionstringargumentsmapper_indipendently_from_the_case()
        {
            const string databaseType = "SQLSERVER";

            var connectionstringArgumentsMapper = _connectionstringArgumentsMapperFactory.Make(databaseType);

            Assert.IsInstanceOf<SqlServerConnectionstringArgumentsMapper>(connectionstringArgumentsMapper);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void Make_should_throw_an_exception_if_database_type_is_not_supported()
        {
            const string databaseType = "Invalid";

            _connectionstringArgumentsMapperFactory.Make(databaseType);
        }
    }
}