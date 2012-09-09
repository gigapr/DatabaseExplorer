using System.Collections.Generic;
using System.Data;
using DatabaseSchemaReader.Contract.BusinessObjects;

namespace GigaWebSolution.DatabaseSchemaReader.Mappers.Interfaces
{
    public interface IColumnMapper
    {
        Column MapColumn(ICollection<string> primaryKeys, DataRow row);
    }
}