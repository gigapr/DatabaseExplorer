using System;
using System.Data;
using DatabaseSchemaReader.Contract.BusinessObjects;
using DatabaseSchemaReader.Contract.Enums;
using GigaWebSolution.DatabaseSchemaReader.Mappers.Interfaces;

namespace GigaWebSolution.DatabaseSchemaReader.Mappers
{
    public class IndexMapper : IIndexMapper
    {
        public Index MapToIndex(DataRow row)
        {
            var dbIndex = new Index
            {
                Name         = row["INDEX_NAME"].ToString(),
                OriginalName = row["INDEX_NAME"].ToString(),
                Unique       = IsUnique(row),
                IndexType    = GetIndexType(row)    
            };

            var column = new IndexColumn
            {
                Name       = row["COLUMN_NAME"].ToString(),
                Descending = Int32.Parse(row["COLLATION"].ToString()) == 2
            };

            dbIndex.Columns.Add(column);

            return dbIndex;
        }

        private static bool IsUnique(DataRow row)
        {
            return (bool) row["UNIQUE"];
        }

        private static IndexType GetIndexType(DataRow row)
        {
            return (bool) row["PRIMARY_KEY"] ? IndexType.PrimaryKey : IndexType.Index;
        }
    }
}
