using DatabaseSchemaReader.ConnectionstringBuilder.Strategies.Interfaces;
using DatabaseSchemaReader.Contract.Enums;

namespace DatabaseSchemaReader.ConnectionstringBuilder.Factories.Interfaces
{
    public interface IConnectionstringBuilderFactory
    {
        IConnectionstringBuilderStrategy Make(DatabaseType databaseType);
    }
}