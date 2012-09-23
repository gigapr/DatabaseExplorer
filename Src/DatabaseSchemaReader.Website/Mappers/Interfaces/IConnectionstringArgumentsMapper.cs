using DatabaseSchemaReader.Contract.BusinessObjects;
using DatabaseSchemaReader.Website.Model;

namespace DatabaseSchemaReader.Website.Mappers.Interfaces
{
    public interface IConnectionstringArgumentsMapper
    {
        SqlServerConnectionstringArguments Map(DatabaseConnection databaseConnection);
    }
}