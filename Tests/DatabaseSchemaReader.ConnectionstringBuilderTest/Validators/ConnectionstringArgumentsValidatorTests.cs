using DatabaseSchemaReader.ConnectionstringBuilder.Validators;
using DatabaseSchemaReader.ConnectionstringBuilder.Validators.Interfaces;
using DatabaseSchemaReader.Contract.BusinessObjects;
using DatabaseSchemaReader.Contract.Exceptions;
using NUnit.Framework;

namespace DatabaseSchemaReader.ConnectionstringBuilderTest.Validators
{
    [TestFixture, Category("Unit")]
    public class ConnectionstringArgumentsValidatorTests
    {
        private IConnectionstringArgumentsValidator _connectionstringArgumentsValidator;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _connectionstringArgumentsValidator = new ConnectionstringArgumentsValidator();   
        }

        [Test, ExpectedException(typeof(ConnectionstringArgumentsException), ExpectedMessage = "DataSource can't be null or Empty")]
        public void Should_throw_an_exception_if_datasource_is_null_or_empty()
        {
            var connectionstringArguments = new ConnectionstringArguments
            {
                DataSource   = "",
                DatabaseName = "DatabaseName",
                Password     = "Password",
                Provider     = "Provider",
                Username     = "Username"
            };

            _connectionstringArgumentsValidator.Validate(connectionstringArguments);
        }

        [Test, ExpectedException(typeof(ConnectionstringArgumentsException), ExpectedMessage = "DatabaseName can't be null or Empty")]
        public void Should_throw_an_exception_if_databasename_is_null_or_empty()
        {
            var connectionstringArguments = new ConnectionstringArguments
            {
                DataSource   = "DataSource",
                DatabaseName = "",
                Password     = "Password",
                Provider     = "Provider",
                Username     = "Username"
            };

            _connectionstringArgumentsValidator.Validate(connectionstringArguments);
        }

        [Test, ExpectedException(typeof(ConnectionstringArgumentsException), ExpectedMessage = "Provider can't be null or Empty")]
        public void Should_throw_an_exception_if_provider_is_null_or_empty()
        {
            var connectionstringArguments = new ConnectionstringArguments
            {
                DataSource   = "DataSource",
                DatabaseName = "DatabaseName",
                Password     = "Password",
                Provider     = null,
                Username     = "Username"
            };

            _connectionstringArgumentsValidator.Validate(connectionstringArguments);
        }

        [Test, ExpectedException(typeof(ConnectionstringArgumentsException), ExpectedMessage = "Username can't be null or Empty")]
        public void Should_throw_an_exception_if_username_is_null_or_empty_and_password_is_not_null_or_empty()
        {
            var connectionstringArguments = new ConnectionstringArguments
            {
                DataSource   = "DataSource",
                DatabaseName = "DatabaseName",
                Password     = "Password",
                Provider     = "Provider",
                Username     = ""
            };

            _connectionstringArgumentsValidator.Validate(connectionstringArguments);
        }

        [Test, ExpectedException(typeof(ConnectionstringArgumentsException), ExpectedMessage = "Password can't be null or Empty")]
        public void Should_throw_an_exception_if_password_is_null_or_empty_and_username_is_not_null_or_empty()
        {
            var connectionstringArguments = new ConnectionstringArguments
            {
                DataSource   = "DataSource",
                DatabaseName = "DatabaseName",
                Password     = "",
                Provider     = "Provider",
                Username     = "Username"
            };

            _connectionstringArgumentsValidator.Validate(connectionstringArguments);
        }

        [Test]
        public void Should_return_true_if_username_and_password_are_null_or_empty()
        {
            var connectionstringArguments = new ConnectionstringArguments
            {
                DataSource   = "DataSource",
                DatabaseName = "DatabaseName",
                Provider     = "Provider",
            };

            var isValid = _connectionstringArgumentsValidator.Validate(connectionstringArguments);

            Assert.IsTrue(isValid);
        }
    }    
}