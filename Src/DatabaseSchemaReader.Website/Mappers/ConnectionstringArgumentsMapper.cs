using DatabaseSchemaReader.Contract.BusinessObjects;
using DatabaseSchemaReader.Contract.Enums;
using DatabaseSchemaReader.Website.Model;
using DatabaseSchemaReader.Website.Mappers.Interfaces;

namespace DatabaseSchemaReader.Website.Mappers
{
    public class ConnectionstringArgumentsMapper : IConnectionstringArgumentsMapper
    {
        public SqlServerConnectionstringArguments Map(DatabaseConnection databaseConnection)
        {
            return new SqlServerConnectionstringArguments
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