using DatabaseSchemaReader.Contract.BusinessObjects;
using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;
using DatabaseSchemaReader.Contract.Enums;
using DatabaseSchemaReader.Website.Mappers.Interfaces;
using DatabaseSchemaReader.Website.Model;

namespace DatabaseSchemaReader.Website.Mappers
{
    public class AccessConnectionstringArgumentsMapper : IConnectionstringArgumentsMapper
    {
        public IConnectionstringArguments Map(DatabaseConnection databaseConnection)
        {
            return new AccessConnectionstringArguments
            {
                DatabaseType = DatabaseType.Access,
                DataSource   = databaseConnection.DataSource,
                Password     = databaseConnection.Password,
                Provider     = databaseConnection.Provider,
                Username     = databaseConnection.Username
            };
        }
    }
}