using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;

namespace DatabaseSchemaReader.WebHost.Mappers.Interfaces
{
    public interface IConnectionstringArgumentsMapper
    {
        IConnectionstringArguments Map(string databaseType, string provider, string dataSource, string databaseName, string username = "", string password = "");
    }
}