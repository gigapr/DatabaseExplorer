using System;
using DatabaseSchemaReader.ConnectionstringBuilder.Extensions;
using DatabaseSchemaReader.ConnectionstringBuilder.Strategies.Interfaces;
using DatabaseSchemaReader.ConnectionstringBuilder.Validators.Interfaces;
using DatabaseSchemaReader.Contract.BusinessObjects;
using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;

namespace DatabaseSchemaReader.ConnectionstringBuilder.Strategies
{
    public class SqlServerConnectionstringBuilderStrategy : IConnectionstringBuilderStrategy
    { 
        private readonly IConnectionstringArgumentsValidator _connectionstringArgumentsValidator;
        
        public SqlServerConnectionstringBuilderStrategy(IConnectionstringArgumentsValidator connectionstringArgumentsValidator)
        {
            _connectionstringArgumentsValidator = connectionstringArgumentsValidator;
        }

        public string BuildConnectionstring(IConnectionstringArguments connectionstringArguments)
        {
            var connectionString = string.Empty;

            if(connectionstringArguments.IsA<SqlServerConnectionstringArguments>())
            {
                var arguments = (SqlServerConnectionstringArguments)connectionstringArguments;

                var areValid = _connectionstringArgumentsValidator.Validate(arguments);

                if (areValid)
                {
                    connectionString = BuildConnectionString(arguments);
                }
            }
            else
            {
                var message = string.Format("SqlServerConnectionstringBuilderStrategy.BuildConnectionstring accept only type of SqlServerConnectionstringArguments, not type of {0}.", connectionstringArguments.GetType());

                throw new ArgumentException(message);
            }

            return connectionString;
        }

        private static string BuildConnectionString(SqlServerConnectionstringArguments arguments)
        {
            string connectionString;

            if (string.IsNullOrEmpty(arguments.Username) && string.IsNullOrEmpty(arguments.Password))
            {
                connectionString = string.Format("Provider={0};Data Source={1};Initial Catalog={2};Integrated Security=SSPI;OLE DB Services=-4;", arguments.Provider, arguments.DataSource, arguments.DatabaseName);
            }
            else
            {
                connectionString = string.Format("Provider={0};Data Source={1};Initial Catalog={2};User ID={3};Password={4};OLE DB Services=-4;", arguments.Provider, arguments.DataSource, arguments.DatabaseName, arguments.Username, arguments.Password);
            }
            return connectionString;
        }
    }
}