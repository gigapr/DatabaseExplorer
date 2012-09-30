using System;
using DatabaseSchemaReader.Website.Factories.Interfaces;
using DatabaseSchemaReader.Website.Mappers;
using DatabaseSchemaReader.Website.Mappers.Interfaces;

namespace DatabaseSchemaReader.Website.Factories
{
    public class ConnectionstringArgumentsMapperFactory : IConnectionstringArgumentsMapperFactory
    {
        public IConnectionstringArgumentsMapper Make(string databaseType)
        {
            IConnectionstringArgumentsMapper connectionstringArgumentsMapper;

            switch (databaseType.ToUpper())
            {
                case "SQLSERVER":
                    connectionstringArgumentsMapper = new SqlServerConnectionstringArgumentsMapper();
                    break;
                case "ACCESS":
                    connectionstringArgumentsMapper = new AccessConnectionstringArgumentsMapper();
                    break;
                default:
                    throw new ArgumentException();
            }

            return connectionstringArgumentsMapper;
        }
    }
}