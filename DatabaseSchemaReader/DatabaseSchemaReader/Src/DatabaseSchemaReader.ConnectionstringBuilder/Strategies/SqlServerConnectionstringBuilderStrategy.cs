using DatabaseSchemaReader.ConnectionstringBuilder.Strategies.Interfaces;
using DatabaseSchemaReader.ConnectionstringBuilder.Validators.Interfaces;
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

            var areValid = _connectionstringArgumentsValidator.Validate(connectionstringArguments);

            if(areValid)
            {
                if(string.IsNullOrEmpty(connectionstringArguments.Username) && string.IsNullOrEmpty(connectionstringArguments.Password))
                {
                    connectionString = string.Format("Provider={0};Data Source={1};Initial Catalog={2};Integrated Security=SSPI;OLE DB Services=-4;", connectionstringArguments.Provider, connectionstringArguments.DataSource, connectionstringArguments.DatabaseName);
                }
                else
                {
                    connectionString = string.Format("Provider={0};Data Source={1};Initial Catalog={2};User ID={3};Password={4};OLE DB Services=-4;", connectionstringArguments.Provider, connectionstringArguments.DataSource, connectionstringArguments.DatabaseName, connectionstringArguments.Username, connectionstringArguments.Password);
                }

            }

            return connectionString;
        }
    }
}