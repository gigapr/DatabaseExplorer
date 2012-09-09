using System.Collections.Generic;
using System.Data;
using DatabaseSchemaReader.Contract.BusinessObjects;

namespace GigaWebSolution.DatabaseSchemaReader.Mappers.Interfaces
{
    public interface IForeignKeyMapper
    {
        ForeignKey MapToForeignKey(DataRow row, DataTable schema);

        List<ForeignKey> MapForeignKeys(DataTable dataTable);
    }
}