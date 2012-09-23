using System;
using DatabaseSchemaReader.ConnectionstringBuilder.Strategies;
using DatabaseSchemaReader.ConnectionstringBuilder.Strategies.Interfaces;
using DatabaseSchemaReader.ConnectionstringBuilder.Validators;
using DatabaseSchemaReader.Contract.BusinessObjects;
using NUnit.Framework;

namespace DatabaseSchemaReader.ConnectionstringBuilderTest.Strategies
{
    [TestFixture, Category("Unit")]
    public class SqlServerConnectionstringBuilderStrategyTests
    {
        private IConnectionstringBuilderStrategy _connectionstringBuilderStrategy;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var connectionstringArgumentsValidator = new SqlServerConnectionstringArgumentsValidator();  

            _connectionstringBuilderStrategy = new SqlServerConnectionstringBuilderStrategy(connectionstringArgumentsValidator);
        }

        [Test, ExpectedException(typeof(ArgumentException), ExpectedMessage = "SqlServerConnectionstringBuilderStrategy.BuildConnectionstring accept only type of SqlServerConnectionstringArguments, not type of DatabaseSchemaReader.Contract.BusinessObjects.AccessConnectionstringArguments.")]
        public void Validate_should_accept_accessconnectionstringarguments_as_argument()
        {
            _connectionstringBuilderStrategy.BuildConnectionstring(new AccessConnectionstringArguments());
        }

        [Test]
        public void Should_be_able_to_build_a_sqlserver_connectionstring_whith_integrated_security()
        {
            var connectionstringArguments = new SqlServerConnectionstringArguments
            {
                DataSource   = "TestDatabase",
                DatabaseName = "Test",
                Provider     = "SQLOLEDB"
            };

            var connectionstring = _connectionstringBuilderStrategy.BuildConnectionstring(connectionstringArguments);

            Assert.AreEqual("Provider=SQLOLEDB;Data Source=TestDatabase;Initial Catalog=Test;Integrated Security=SSPI;OLE DB Services=-4;", connectionstring);
        }

        [Test]
        public void Should_be_able_to_build_a_sqlserver_connectionstring_when_username_and_password_are_specified()
        {
            var connectionstringArguments = new SqlServerConnectionstringArguments
            {
                DataSource = "TestDatabase",
                DatabaseName = "Test",
                Provider = "SQLOLEDB",
                Username = "Username",
                Password = "Password"
            };

            var connectionstring = _connectionstringBuilderStrategy.BuildConnectionstring(connectionstringArguments);

            Assert.AreEqual("Provider=SQLOLEDB;Data Source=TestDatabase;Initial Catalog=Test;User ID=Username;Password=Password;OLE DB Services=-4;", connectionstring);
        }
    }
}