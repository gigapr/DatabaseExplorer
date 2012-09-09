using System.Data;
using DatabaseSchemaReader.Contract.BusinessObjects;

namespace GigaWebSolution.DatabaseSchemaReader.Mappers.Interfaces
{
    public interface IIndexMapper
    {
        Index MapToIndex(DataRow row);
    }
}