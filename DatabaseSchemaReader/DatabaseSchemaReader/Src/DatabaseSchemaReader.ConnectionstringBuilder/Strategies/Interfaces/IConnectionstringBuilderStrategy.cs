using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;

namespace DatabaseSchemaReader.ConnectionstringBuilder.Strategies.Interfaces
{
    public interface IConnectionstringBuilderStrategy
    {
        string BuildConnectionstring(IConnectionstringArguments connectionstringArguments);
    }
}