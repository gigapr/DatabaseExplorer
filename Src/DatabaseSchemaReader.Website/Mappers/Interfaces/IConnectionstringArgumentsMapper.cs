using DatabaseSchemaReader.Contract.BusinessObjects;
using DatabaseSchemaReader.Website.Models;

namespace DatabaseSchemaReader.Website.Mappers.Interfaces
{
    public interface IConnectionstringArgumentsMapper
    {
        ConnectionstringArguments Map(DatabaseConnection databaseConnection);
    }
}