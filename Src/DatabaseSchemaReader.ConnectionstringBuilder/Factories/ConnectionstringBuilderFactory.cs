using DatabaseSchemaReader.ConnectionstringBuilder.Factories.Interfaces;
using DatabaseSchemaReader.ConnectionstringBuilder.Strategies;
using DatabaseSchemaReader.ConnectionstringBuilder.Strategies.Interfaces;
using DatabaseSchemaReader.ConnectionstringBuilder.Validators;
using DatabaseSchemaReader.Contract.Enums;

namespace DatabaseSchemaReader.ConnectionstringBuilder.Factories
{
    public class ConnectionstringBuilderFactory : IConnectionstringBuilderFactory
    {
        public IConnectionstringBuilderStrategy Make(DatabaseType databaseType)
        {
            IConnectionstringBuilderStrategy connectionstringBuilderStrategy = null;

            switch (databaseType)
            {
                case DatabaseType.Access:
                    connectionstringBuilderStrategy = new AccessConnectionstringBuilderStrategy(new AccessConnectionstringArgumentsValidator());
                    break;
                case DatabaseType.SqlServer:
                    connectionstringBuilderStrategy = new SqlServerConnectionstringBuilderStrategy(new SqlServerConnectionstringArgumentsValidator());
                    break;
                case DatabaseType.Oracle:
                    connectionstringBuilderStrategy = new OracleConnectionstringBuilderStrategy(null);
                    break;
            }

            return connectionstringBuilderStrategy;
        }
    }
}