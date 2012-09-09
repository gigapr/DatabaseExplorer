using DatabaseSchemaReader.Contract.BusinessObjects;
using DatabaseSchemaReader.Contract.Enums;
using DatabaseSchemaReader.Website.Mappers.Interfaces;
using DatabaseSchemaReader.Website.Models;

namespace DatabaseSchemaReader.Website.Mappers
{
    public class ConnectionstringArgumentsMapper : IConnectionstringArgumentsMapper
    {
        public ConnectionstringArguments Map(DatabaseConnection databaseConnection)
        {
            return new ConnectionstringArguments
            {
                DataSource   = databaseConnection.DataSource,
                DatabaseName = databaseConnection.DatabaseName,
                DatabaseType = DatabaseType.SqlServer,
                Username     = databaseConnection.Username,
                Password     = databaseConnection.Password,
                Provider     = databaseConnection.Provider
            };
        }
    }
}