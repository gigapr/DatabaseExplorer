using System;
using DatabaseSchemaReader.Contract.BusinessObjects;
using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;
using DatabaseSchemaReader.Contract.Enums;
using DatabaseSchemaReader.Website.Model;
using DatabaseSchemaReader.Website.Mappers.Interfaces;

namespace DatabaseSchemaReader.Website.Mappers
{
    public class SqlServerConnectionstringArgumentsMapper : IConnectionstringArgumentsMapper
    {
        public IConnectionstringArguments Map(DatabaseConnection databaseConnection)
        {
            return new SqlServerConnectionstringArguments
            {
                DataSource   = databaseConnection.DataSource,
                DatabaseName = databaseConnection.DatabaseName,
                DatabaseType = MapDatabaseType(databaseConnection.DatabaseType),
                Username     = databaseConnection.Username,
                Password     = databaseConnection.Password,
                Provider     = databaseConnection.Provider
            };
        }

        private static DatabaseType MapDatabaseType(string databaseType)
        {
            return (DatabaseType)Enum.Parse(typeof(DatabaseType), databaseType);
        }
    }
}