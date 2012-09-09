using System;
using DatabaseSchemaReader.Contract.BusinessObjects;
using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;
using DatabaseSchemaReader.Contract.Enums;
using DatabaseSchemaReader.WebHost.Exceptions;
using DatabaseSchemaReader.WebHost.Mappers.Interfaces;

namespace DatabaseSchemaReader.WebHost.Mappers
{
    public class ConnectionstringArgumentsMapper : IConnectionstringArgumentsMapper
    {
        public IConnectionstringArguments Map(string databaseType, string provider, string dataSource, string databaseName, string username = "", string password = "")
        {
            return new ConnectionstringArguments
                       {
                           DatabaseType = MapToDatabaseType(databaseType),
                           Provider     = provider, 
                           DataSource   = dataSource,
                           DatabaseName = databaseName,
                           Username     = username,
                           Password     = password
                       };
        }

        private static DatabaseType MapToDatabaseType(string databaseType)
        {
            try
            {
                return (DatabaseType)Enum.Parse(typeof(DatabaseType), databaseType, true);
            }
            catch (Exception)
            {
                var message = string.Format("Database Type '{0}' is not supported.", databaseType);

                var databaseTypeException = new DatabaseTypeException(message);

                throw databaseTypeException;
            }
        }
    }
}