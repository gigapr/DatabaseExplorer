using DatabaseSchemaReader.ConnectionstringBuilder.Factories.Interfaces;
using DatabaseSchemaReader.ConnectionstringBuilder.Strategies.Interfaces;
using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;

namespace DatabaseSchemaReader.ConnectionstringBuilder.Factories
{
    public class ConnectionstringFactory : IConnectionstringFactory
    {
        private readonly IConnectionstringBuilderStrategy _connectionstringBuilderStrategy;

        public ConnectionstringFactory(IConnectionstringBuilderStrategy connectionstringBuilderStrategy)
        {
            _connectionstringBuilderStrategy = connectionstringBuilderStrategy;
        }

        public string BuildConnection(IConnectionstringArguments connectionstringArguments)
        {
            return _connectionstringBuilderStrategy.BuildConnectionstring(connectionstringArguments);
        }
    }
}
