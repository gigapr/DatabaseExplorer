using System;
using DatabaseSchemaReader.ConnectionstringBuilder.Strategies;
using DatabaseSchemaReader.ConnectionstringBuilder.Strategies.Interfaces;
using DatabaseSchemaReader.ConnectionstringBuilder.Validators.Interfaces;
using DatabaseSchemaReader.Contract.BusinessObjects;
using DatabaseSchemaReader.Contract.Enums;
using NUnit.Framework;
using Rhino.Mocks;

namespace DatabaseSchemaReader.ConnectionstringBuilderTest.Strategies
{
    [TestFixture, Category("Unit")]
    public class AccessConnectionstringBuilderStrategyTest
    {
        private IConnectionstringBuilderStrategy _connectionstringBuilderStrategy;
        private IConnectionstringArgumentsValidator _connectionstringArgumentsValidator;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _connectionstringArgumentsValidator = MockRepository.GenerateStrictMock<IConnectionstringArgumentsValidator>();

            _connectionstringBuilderStrategy = new AccessConnectionstringBuilderStrategy(_connectionstringArgumentsValidator);
        }

        [Test]
        public void Should_use_accessconnectionstringargumentsvalidator_to_validate_accessconnectionstringarguments()
        {
            var accessConnectionstringArguments = new AccessConnectionstringArguments();

            _connectionstringArgumentsValidator.Expect(validator => validator.Validate(accessConnectionstringArguments)).Return(true);

            _connectionstringBuilderStrategy.BuildConnectionstring(accessConnectionstringArguments);
        }

        [Test]
        public void Should_be_able_to_create_a_access_database_connectionstring()
        {
            _connectionstringArgumentsValidator.Expect(validator => validator.Validate(Arg<AccessConnectionstringArguments>.Is.Anything)).Return(true);

            var connectionstringArguments = new AccessConnectionstringArguments
            {
                DataSource   = "Northwind.accdb",
                DatabaseType = DatabaseType.Access,
                Provider     = "Microsoft.Jet.OLEDB.4.0"
            };

            var connectionstring = _connectionstringBuilderStrategy.BuildConnectionstring(connectionstringArguments);

            Assert.AreEqual("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Northwind.accdb;", connectionstring);
        }

        [Test]
        public void Should_be_able_to_create_a_access_database_connectionstring_with_credentials()
        {
            _connectionstringArgumentsValidator.Expect(validator => validator.Validate(Arg<AccessConnectionstringArguments>.Is.Anything)).Return(true);

            var connectionstringArguments = new AccessConnectionstringArguments
            {
                DataSource   = "Northwind.accdb",
                DatabaseType = DatabaseType.Access,
                Username     = "TestUsername",
                Password     = "TestPassword",
                Provider     = "Microsoft.Jet.OLEDB.4.0" 
            };

            var connectionstring = _connectionstringBuilderStrategy.BuildConnectionstring(connectionstringArguments);

            Assert.AreEqual("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Northwind.accdb;User ID=TestUsername;Password=TestPassword;", connectionstring);
        }

        [Test, ExpectedException(typeof(ArgumentException), ExpectedMessage = "AccessConnectionstringBuilderStrategy.BuildConnectionstring accept only type of AccessConnectionstringArguments, not type of DatabaseSchemaReader.Contract.BusinessObjects.SqlServerConnectionstringArguments.")]
        public void Validate_should_accept_accessconnectionstringarguments_as_argument()
        {
            _connectionstringBuilderStrategy.BuildConnectionstring(new SqlServerConnectionstringArguments());
        }
    }
}