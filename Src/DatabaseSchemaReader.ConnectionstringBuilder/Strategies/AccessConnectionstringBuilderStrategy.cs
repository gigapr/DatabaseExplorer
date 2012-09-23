using System;
using DatabaseSchemaReader.ConnectionstringBuilder.Extensions;
using DatabaseSchemaReader.ConnectionstringBuilder.Strategies.Interfaces;
using DatabaseSchemaReader.ConnectionstringBuilder.Validators.Interfaces;
using DatabaseSchemaReader.Contract.BusinessObjects;
using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;

namespace DatabaseSchemaReader.ConnectionstringBuilder.Strategies
{
    public class AccessConnectionstringBuilderStrategy : IConnectionstringBuilderStrategy
    {
        private readonly IConnectionstringArgumentsValidator _connectionstringArgumentsValidator;

        public AccessConnectionstringBuilderStrategy(IConnectionstringArgumentsValidator connectionstringArgumentsValidator)
        {
            _connectionstringArgumentsValidator = connectionstringArgumentsValidator;
        }

        public string BuildConnectionstring(IConnectionstringArguments connectionstringArguments)
        {
            var connectionstring = string.Empty;

            if (connectionstringArguments.IsA<AccessConnectionstringArguments>())
            {
                var arguments = (AccessConnectionstringArguments)connectionstringArguments;

                var areValid = _connectionstringArgumentsValidator.Validate(arguments);

                if (areValid)
                {
                    connectionstring = BuildConnectionString(arguments);
                }
            }
            else
            {
                var message = string.Format("AccessConnectionstringBuilderStrategy.BuildConnectionstring accept only type of AccessConnectionstringArguments, not type of {0}.", connectionstringArguments.GetType());

                throw new ArgumentException(message);
            }
            
            return connectionstring;
        }

        private static string BuildConnectionString(AccessConnectionstringArguments arguments)
        {
            string connectionString;

            if (string.IsNullOrEmpty(arguments.Username) && string.IsNullOrEmpty(arguments.Password))
            {
                connectionString = string.Format("Provider={0};Data Source={1};", arguments.Provider, arguments.DataSource);
            }
            else
            {
                connectionString = string.Format("Provider={0};Data Source={1};User ID={2};Password={3};", arguments.Provider, arguments.DataSource, arguments.Username, arguments.Password);
            }
            return connectionString;
        }
    }
}