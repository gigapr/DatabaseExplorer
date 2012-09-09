using System.Linq;
using DatabaseSchemaReader.ConnectionstringBuilder.Interfaces;
using DatabaseSchemaReader.Contract.BusinessObjects;
using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;
using DatabaseSchemaReader.Service.Interfaces;
using GigaWebSolution.DatabaseSchemaReader.Interfaces;

namespace DatabaseSchemaReader.Service
{
    public class DatabaseSchemaExplorer : IDatabaseSchemaExplorer
    {
        private readonly IConnectionstringBuilder _connectionstringBuilder;
        private readonly ISchemaReader _schemaReader;

        public DatabaseSchemaExplorer(IConnectionstringBuilder connectionstringBuilder, ISchemaReader schemaReader)
        {
            _connectionstringBuilder = connectionstringBuilder;
            _schemaReader = schemaReader;
        }

        public Table GetTable(IConnectionstringArguments connectionstringArguments, string tableName)
        {
            var connectionstring = _connectionstringBuilder.BuildConnectionString(connectionstringArguments);

            return _schemaReader.GetTable(connectionstring, tableName);
        }

        public IQueryable<string> GetTablesName(IConnectionstringArguments connectionstringArguments)
        {
            var connectionstring = _connectionstringBuilder.BuildConnectionString(connectionstringArguments);

            return _schemaReader.GetTablesName(connectionstring).AsQueryable();
        }

        public IQueryable<string> GetViewsName(IConnectionstringArguments connectionstringArguments)
        {
            var connectionstring = _connectionstringBuilder.BuildConnectionString(connectionstringArguments);

            return _schemaReader.GetViewsName(connectionstring).AsQueryable();
        }

        public IQueryable<Table> GetTables(IConnectionstringArguments connectionstringArguments)
        {
            var connectionstring = _connectionstringBuilder.BuildConnectionString(connectionstringArguments);

            return _schemaReader.GetTables(connectionstring).AsQueryable();
        }

        public IQueryable<ForeignKey> GetForeignKeys(IConnectionstringArguments connectionstringArguments)
        {
            var connectionstring = _connectionstringBuilder.BuildConnectionString(connectionstringArguments);

            return _schemaReader.GetForeignKeys(connectionstring).AsQueryable();
        }
    }
}