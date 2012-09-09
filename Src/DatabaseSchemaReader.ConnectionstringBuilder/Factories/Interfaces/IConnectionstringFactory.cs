using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;

namespace DatabaseSchemaReader.ConnectionstringBuilder.Factories.Interfaces
{
    public interface IConnectionstringFactory
    {
        string BuildConnection(IConnectionstringArguments connectionstringArguments);
    }
}