using DatabaseSchemaReader.ConnectionstringBuilder.Factories.Interfaces;
using DatabaseSchemaReader.ConnectionstringBuilder.Strategies;
using DatabaseSchemaReader.ConnectionstringBuilder.Strategies.Interfaces;
using DatabaseSchemaReader.ConnectionstringBuilder.Validators.Interfaces;
using DatabaseSchemaReader.Contract.Enums;

namespace DatabaseSchemaReader.ConnectionstringBuilder.Factories
{
    public class ConnectionstringBuilderFactory : IConnectionstringBuilderFactory
    {
        private readonly IConnectionstringArgumentsValidator _connectionstringArgumentsValidator;

        public ConnectionstringBuilderFactory(IConnectionstringArgumentsValidator connectionstringArgumentsValidator)
        {
            _connectionstringArgumentsValidator = connectionstringArgumentsValidator;
        }

        public IConnectionstringBuilderStrategy Make(DatabaseType databaseType)
        {
            IConnectionstringBuilderStrategy connectionstringBuilderStrategy = null;

            switch (databaseType)
            {
                //case DatabaseType.Access:
                //    connectionstringBuilderStrategy = new AccessConnectionstringBuilderStrategy(_connectionstringArgumentsValidator);
                //    break;
                case DatabaseType.SqlServer:
                    connectionstringBuilderStrategy = new SqlServerConnectionstringBuilderStrategy(_connectionstringArgumentsValidator);
                    break;
                //case DatabaseType.Oracle:
                //    connectionstringBuilderStrategy = new OracleConnectionstringBuilderStrategy(_connectionstringArgumentsValidator);
                //    break;
            }

            return connectionstringBuilderStrategy;
        }
    }
}