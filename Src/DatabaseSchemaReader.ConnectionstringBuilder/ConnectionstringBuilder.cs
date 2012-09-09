using DatabaseSchemaReader.ConnectionstringBuilder.Factories;
using DatabaseSchemaReader.ConnectionstringBuilder.Factories.Interfaces;
using DatabaseSchemaReader.ConnectionstringBuilder.Interfaces;
using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;

namespace DatabaseSchemaReader.ConnectionstringBuilder
{
    public class ConnectionstringBuilder : IConnectionstringBuilder
    {
        private readonly IConnectionstringBuilderFactory _connectionstringBuilderFactory;

        public ConnectionstringBuilder(IConnectionstringBuilderFactory connectionstringBuilderFactory)
        {
            _connectionstringBuilderFactory = connectionstringBuilderFactory;
        }

        public string BuildConnectionString(IConnectionstringArguments connectionstringArguments)
        {
            var connectionstringBuilderStrategy = _connectionstringBuilderFactory.Make(connectionstringArguments.DatabaseType);

            var connectionstringFactory = new ConnectionstringFactory(connectionstringBuilderStrategy);

            var connectionString = connectionstringFactory.BuildConnection(connectionstringArguments);

            return connectionString;
        }
    }
}