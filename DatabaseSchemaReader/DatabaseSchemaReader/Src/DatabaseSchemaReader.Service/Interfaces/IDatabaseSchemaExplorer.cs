using System.Linq;
using DatabaseSchemaReader.Contract.BusinessObjects;
using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;

namespace DatabaseSchemaReader.Service.Interfaces
{
    public interface IDatabaseSchemaExplorer
    {
        Table GetTable(IConnectionstringArguments connectionstringArguments, string tableName);
        IQueryable<string> GetTablesName(IConnectionstringArguments connectionstringArguments);
        IQueryable<string> GetViewsName(IConnectionstringArguments connectionstringArguments);
        IQueryable<Table> GetTables(IConnectionstringArguments connectionstringArguments);
        IQueryable<ForeignKey> GetForeignKeys(IConnectionstringArguments connectionstringArguments);
    }
}
