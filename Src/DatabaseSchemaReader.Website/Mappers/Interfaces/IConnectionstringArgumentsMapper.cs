using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;
using DatabaseSchemaReader.Website.Model;

namespace DatabaseSchemaReader.Website.Mappers.Interfaces
{
    public interface IConnectionstringArgumentsMapper
    {
        IConnectionstringArguments Map(DatabaseConnection databaseConnection);
    }
}