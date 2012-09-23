using DatabaseSchemaReader.ConnectionstringBuilder.Strategies;
using DatabaseSchemaReader.ConnectionstringBuilder.Strategies.Interfaces;
using DatabaseSchemaReader.ConnectionstringBuilder.Validators;
using NUnit.Framework;

namespace DatabaseSchemaReader.ConnectionstringBuilderTest.Strategies
{
    [TestFixture, Category("Unit")]
    public class OracleConnectionstringBuilderStrategyTest
    {
        private IConnectionstringBuilderStrategy _connectionstringBuilderStrategy;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var connectionstringArgumentsValidator = new SqlServerConnectionstringArgumentsValidator();

            _connectionstringBuilderStrategy = new OracleConnectionstringBuilderStrategy(connectionstringArgumentsValidator);
        }
    }
}