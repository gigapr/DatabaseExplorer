using System;
using DatabaseSchemaReader.ConnectionstringBuilder.Strategies.Interfaces;
using DatabaseSchemaReader.ConnectionstringBuilder.Validators.Interfaces;
using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;

namespace DatabaseSchemaReader.ConnectionstringBuilder.Strategies
{
    public class OracleConnectionstringBuilderStrategy : IConnectionstringBuilderStrategy
    {
        private readonly IConnectionstringArgumentsValidator _connectionstringArgumentsValidator;

        public OracleConnectionstringBuilderStrategy(IConnectionstringArgumentsValidator connectionstringArgumentsValidator)
        {
            _connectionstringArgumentsValidator = connectionstringArgumentsValidator;
        }

        public string BuildConnectionstring(IConnectionstringArguments connectionstringArguments)
        {
            throw new NotImplementedException();
        }
    }
}