using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using DatabaseSchemaReader.Contract.BusinessObjects;
using DatabaseSchemaReader.Contract.Exceptions;
using GigaWebSolution.DatabaseSchemaReader.Interfaces;
using GigaWebSolution.DatabaseSchemaReader.Mappers.Interfaces;

namespace GigaWebSolution.DatabaseSchemaReader
{
    public class SchemaReader : ISchemaReader
    {
        private readonly IIndexMapper _indexMapper;
        private readonly IColumnMapper _columnMapper;
        private readonly IForeignKeyMapper _foreignKeyMapper;

        public SchemaReader(IForeignKeyMapper foreignKeyMapper, IColumnMapper columnMapper, IIndexMapper indexMapper)
        {
            _foreignKeyMapper = foreignKeyMapper;
            _columnMapper = columnMapper;
            _indexMapper = indexMapper;            
        }

        public Table GetTable(string connectionstring, string tableName)
        {
            var foreignKeys = GetForeignKeys(connectionstring);

            var table = GetTableInfo(connectionstring, tableName, foreignKeys);

            return table;
        }       

        public List<string> GetViewsName(string connectionstring)
        {
            var viewNames = new List<string>();

            using (var conn = new OleDbConnection(connectionstring))
            {
                conn.Open();

                var schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new Object[] { null, null, null, "VIEW" });

                for (var i = 0; i < schemaTable.Rows.Count; i++)
                {
                    viewNames.Add(schemaTable.Rows[i].ItemArray[2].ToString());
                }

                conn.Close();
            }

            return viewNames;
        }

        public List<Table> GetTables(string connectionstring)
        {
            var tablesName = GetTablesName(connectionstring);

            var foreignKeys = GetForeignKeys(connectionstring);

            return tablesName.Select(table => GetTableInfo(connectionstring, table, foreignKeys)).AsParallel().ToList();
        }

        public List<string> GetTablesName(string connectionstring)
        {
            var tableNames = new List<string>();

            using (var connection = new OleDbConnection(connectionstring))
            {
                connection.Open();

                var schemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new Object[] { null, null, null, "TABLE" });

                for (var i = 0; i < schemaTable.Rows.Count; i++)
                {
                    tableNames.Add(schemaTable.Rows[i].ItemArray[2].ToString());
                }

                connection.Close();
            }

            return tableNames;
        }
        
        public IEnumerable<ForeignKey> GetForeignKeys(string connectionString)
        {
            List<ForeignKey> foreignKeys;

            var restrictions = new string[] { null };

            using (var schema = GetOleDbSchemaTableForeigKeys(connectionString, restrictions))
            {
                foreignKeys = _foreignKeyMapper.MapForeignKeys(schema);
            }

            return foreignKeys;
        }

        private ICollection<Column> GetColumns(string connectionString, string tableName)
        {
            var columns = new Collection<Column>();

            var restrictions = new[] { null, null, tableName };

            var primaryKeys = GetPrimaryKey(connectionString, tableName);

            var columnSchema = GetColumnSchema(connectionString, restrictions);

            foreach (var column in from DataRow row in columnSchema.Rows select _columnMapper.MapColumn(primaryKeys, row))
            {
                columns.Add(column);
            }

            return columns;
        }

        private static List<string> GetPrimaryKey(string connectionString, string tableName)
        {
            object[] restrictions = new[] { null, null, tableName };

            var primaryKeys = new List<string>();

            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                var dataTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Primary_Keys, restrictions);

                var collection = dataTable.Rows.Cast<DataRow>().Select(row => row["COLUMN_NAME"].ToString());

                primaryKeys.AddRange(collection);

                connection.Close();
            }

            return primaryKeys;
        }

        private static DataTable GetColumnSchema(string connectionString, string[] restrictions)
        {
            DataTable columnSchema;

            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                columnSchema = connection.GetSchema(OleDbMetaDataCollectionNames.Columns, restrictions);

                connection.Close();
            }

            return columnSchema;
        }

        private static DataTable GetOleDbSchemaTableForeigKeys(string connectionString, string[] restrictions)
        {
            DataTable oleDbSchemaTableFk;

            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                oleDbSchemaTableFk = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, restrictions);

                connection.Close();
            }

            return oleDbSchemaTableFk;
        }

        private IList<Index> GetIndexes(string connectionString, string tableName)
        {
            var restrictions = new[] { null, null, null, null, tableName };

            var schema = GetOleDbSchemaTableIndexes(connectionString, restrictions);

            return (from DataRow row in schema.Rows select _indexMapper.MapToIndex(row)).ToList();
        }

        private static DataTable GetOleDbSchemaTableIndexes(string connectionString, string[] restrictions)
        {
            DataTable oleDbSchemaTableIn;

            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                oleDbSchemaTableIn = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Indexes, restrictions);

                connection.Close();
            }

            return oleDbSchemaTableIn;
        }

        private static List<ForeignKey> GetForeignKeysForTable(string tableName, IEnumerable<ForeignKey> foreignKeys)
        {
            return foreignKeys.Where(fk => fk.ForeignKeyTableName == tableName).ToList();
        }

        private Table GetTableInfo(string connectionstring, string tableName, IEnumerable<ForeignKey> foreignKeys)
        {
            var columns = Task<ICollection<Column>>.Factory.StartNew(() => GetColumns(connectionstring, tableName));
            var indexes = Task<IList<Index>>.Factory.StartNew(() => GetIndexes(connectionstring, tableName));

            var table = new Table(tableName)
            {
                Columns = columns.Result,
                Indexes = indexes.Result,
                ForeignKeys = GetForeignKeysForTable(tableName, foreignKeys)
            };

            if(!table.Columns.Any())
            {
                throw new TableNotFoundException(string.Format("Table '{0}' not found.", tableName)); 
            }

            return table;
        }
    }    
}
