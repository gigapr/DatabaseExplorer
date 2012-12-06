using DatabaseSchemaReader.ConnectionstringBuilder.Validators;
using DatabaseSchemaReader.ConnectionstringBuilder.Validators.Interfaces;
using DatabaseSchemaReader.Contract.BusinessObjects;
using DatabaseSchemaReader.Contract.Enums;
using DatabaseSchemaReader.Contract.Exceptions;
using NUnit.Framework;

namespace DatabaseSchemaReader.ConnectionstringBuilderTest.Validators
{
    [TestFixture]
    public class AccessConnectionstringArgumentsValidatorTests
    {
        protected IConnectionstringArgumentsValidator _accessConnectionstringArgumentsValidator;

        [SetUp]
        public void SetUp()
        {
            _accessConnectionstringArgumentsValidator = new AccessConnectionstringArgumentsValidator();
        }

        [Test, ExpectedException(typeof(ConnectionstringArgumentsException))]
        public void Should_throw_an_exception_when_datasource_is_invalid()
        {
            var accessConnectionstringArguments = new AccessConnectionstringArguments
            {
                DataSource    = "",
                DatabaseName  = "DatabaseName",
                DatabaseType  = DatabaseType.Access,
                Password      = "password",
                Provider      = "Provider",
                Username      = "Username"
            };

            _accessConnectionstringArgumentsValidator.Validate(accessConnectionstringArguments);
        }

        //TODO do we need to check anything else??????
        //[Test, ExpectedException(typeof(ConnectionstringArgumentsException))]
        //public void Should_throw_an_exception_when_databasename_is_invalid()
        //{
        //    var accessConnectionstringArguments = new AccessConnectionstringArguments
        //    {
        //        DataSource   = "DataSource",
        //        DatabaseName = null,
        //        DatabaseType = DatabaseType.Access,
        //        Password     = "password",
        //        Provider     = "Provider",
        //        Username     = "Username"
        //    };

        //    _accessConnectionstringArgumentsValidator.Validate(accessConnectionstringArguments);
        //}

        [Test, ExpectedException(typeof(ConnectionstringArgumentsException))]
        public void Should_throw_an_exception_when_password_is_invalid()
        {
            var accessConnectionstringArguments = new AccessConnectionstringArguments
            {
                DataSource   = "DataSource",
                DatabaseType = DatabaseType.Access,
                DatabaseName = "DatabaseName",                
                Password     = "",
                Provider     = "Provider",
                Username     = "Username"
            };

            _accessConnectionstringArgumentsValidator.Validate(accessConnectionstringArguments);
        }

        [Test, ExpectedException(typeof(ConnectionstringArgumentsException))]
        public void Should_throw_an_exception_when_provider_is_invalid()
        {
            var accessConnectionstringArguments = new AccessConnectionstringArguments
            {
                DataSource   = "DataSource",
                DatabaseType = DatabaseType.Access,
                DatabaseName = "DatabaseName",                
                Password     = "Password",
                Provider     = null,
                Username     = "Username"
            };

            _accessConnectionstringArgumentsValidator.Validate(accessConnectionstringArguments);
        }

        [Test, ExpectedException(typeof(ConnectionstringArgumentsException))]
        public void Should_throw_an_exception_when_username_is_invalid()
        {
            var accessConnectionstringArguments = new AccessConnectionstringArguments
            {
                DataSource   = "DataSource",
                DatabaseType = DatabaseType.Access,
                DatabaseName = "DatabaseName",                
                Password     = "Password",
                Provider     = "Provider",
                Username     = ""
            };

            _accessConnectionstringArgumentsValidator.Validate(accessConnectionstringArguments);
        }

        [Test]
        public void Should_return_tru_if_all_paramaters_are_valid()
        {
            var accessConnectionstringArguments = new AccessConnectionstringArguments
            {
                DataSource   = "DataSource",
                DatabaseType = DatabaseType.Access,
                DatabaseName = "DatabaseName",                
                Password     = "Password",
                Provider     = "Provider",
                Username     = "Username"
            };

            var result = _accessConnectionstringArgumentsValidator.Validate(accessConnectionstringArguments);
            
            Assert.IsTrue(result);
        }
    }
}