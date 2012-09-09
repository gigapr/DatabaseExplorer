using System.Linq;
using System.Web.Http;
using DatabaseSchemaReader.Contract.BusinessObjects;
using DatabaseSchemaReader.Service.Interfaces;
using DatabaseSchemaReader.WebHost.Attributes;
using DatabaseSchemaReader.WebHost.Mappers.Interfaces;

namespace DatabaseSchemaReader.WebHost.Controllers
{
    [SchemaExplorerExceptionHandler]
    public class DatabaseSchemaExplorerController : ApiController
    {
        private readonly IConnectionstringArgumentsMapper _connectionstringArgumentsMapper;
        private readonly IDatabaseSchemaExplorer _schemaExplorer;

        public DatabaseSchemaExplorerController() : this(IoC.Resolve<IConnectionstringArgumentsMapper>(), IoC.Resolve<IDatabaseSchemaExplorer>())
        {
        }

        public DatabaseSchemaExplorerController(IConnectionstringArgumentsMapper connectionstringArgumentsMapper, IDatabaseSchemaExplorer schemaExplorer)
        {
            _connectionstringArgumentsMapper = connectionstringArgumentsMapper;
            _schemaExplorer = schemaExplorer;
        }

        /// <summary>
        /// Gets an list of Tables.
        /// </summary>
        [HttpGet]
        public IQueryable<Table> Tables(string databaseType, string provider, string dataSource, string databaseName, string username, string password)
        {
            var connectionstringArguments = _connectionstringArgumentsMapper.Map(databaseType, provider, dataSource, databaseName, username, password);

            return _schemaExplorer.GetTables(connectionstringArguments);
        }

        /// <summary>
        /// Gets info about a table.
        /// </summary>
        [HttpGet]
        public Table Tables(string databaseType, string provider, string dataSource, string databaseName, string tableName, string username, string password)
        {
            var connectionstringArguments = _connectionstringArgumentsMapper.Map(databaseType, provider, dataSource, databaseName, username, password);

            return _schemaExplorer.GetTable(connectionstringArguments, tableName);
        }

        /// <summary>
        /// Gets a list of views name.
        /// </summary>
        [HttpGet]
        public IQueryable<string> ViewsName(string databaseType, string provider, string dataSource, string databaseName, string username, string password)
        {
            var connectionstringArguments = _connectionstringArgumentsMapper.Map(databaseType, provider, dataSource, databaseName, username, password);

            return _schemaExplorer.GetViewsName(connectionstringArguments);
        }

        /// <summary>
        /// Gets a list of tables name.
        /// </summary>
        [HttpGet]
        public IQueryable<string> TablesName(string databaseType, string provider, string dataSource, string databaseName, string username, string password)
        {
            var connectionstringArguments = _connectionstringArgumentsMapper.Map(databaseType, provider, dataSource, databaseName, username, password);

            return _schemaExplorer.GetTablesName(connectionstringArguments);
        }

        /// <summary>
        /// Gets a list of tables name.
        /// </summary>
        [HttpGet]
        public IQueryable<ForeignKey> ForeignKeys(string databaseType, string provider, string dataSource, string databaseName, string username, string password)
        {
            var connectionstringArguments = _connectionstringArgumentsMapper.Map(databaseType, provider, dataSource, databaseName, username, password);

            return _schemaExplorer.GetForeignKeys(connectionstringArguments);
        }
    }
}