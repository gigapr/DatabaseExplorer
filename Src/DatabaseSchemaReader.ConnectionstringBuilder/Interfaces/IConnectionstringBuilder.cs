using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;

namespace DatabaseSchemaReader.ConnectionstringBuilder.Interfaces
{
    public interface IConnectionstringBuilder
    {
        string BuildConnectionString(IConnectionstringArguments connectionstringArguments);
    }
}