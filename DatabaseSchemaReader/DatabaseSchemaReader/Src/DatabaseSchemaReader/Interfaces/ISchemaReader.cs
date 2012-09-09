using System.Collections.Generic;
using DatabaseSchemaReader.Contract.BusinessObjects;

namespace GigaWebSolution.DatabaseSchemaReader.Interfaces
{
    public interface ISchemaReader
    {
        Table GetTable(string connectionstring, string tableName);
        List<string> GetTablesName(string connectionstring);
        List<string> GetViewsName(string connectionstring);
        List<Table> GetTables(string connectionstring);
        IEnumerable<ForeignKey> GetForeignKeys(string connectionString);
    }
}