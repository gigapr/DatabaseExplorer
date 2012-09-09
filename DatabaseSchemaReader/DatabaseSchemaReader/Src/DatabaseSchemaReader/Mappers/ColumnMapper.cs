using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using DatabaseSchemaReader.Contract.BusinessObjects;
using GigaWebSolution.DatabaseSchemaReader.Mappers.Interfaces;

namespace GigaWebSolution.DatabaseSchemaReader.Mappers
{
    public class ColumnMapper : IColumnMapper
    {
        public Column MapColumn(ICollection<string> primaryKeys, DataRow row)
        {
            
            var column = new Column
            {
                Name         = row["COLUMN_NAME"].ToString(),
                OriginalName = row["COLUMN_NAME"].ToString(),
                AllowNulls   = (bool)row["IS_NULLABLE"],
                TypeInfo     = (OleDbType)Int32.Parse(row["DATA_TYPE"].ToString()),
                DefaultValue = GetDefaultValue(row),
                Precision    = GetPrecision(row),
                Scale        = GetScale(row),
                InPrimaryKey = IsPrimaryKey(row, primaryKeys),
                IsIdentity   = false,
                Length       = GetLength(row)
            };

            return column;
        }

        private static int GetLength(DataRow row)
        {
            return ((OleDbType)Int32.Parse(row["DATA_TYPE"].ToString()) != OleDbType.WChar) ? -1 : Int32.Parse(row["CHARACTER_MAXIMUM_LENGTH"].ToString());
        }

        private static bool IsPrimaryKey(DataRow row, ICollection<string> primaryKeys)
        {
            return primaryKeys.Contains(row["COLUMN_NAME"].ToString());
        }

        private static int GetScale(DataRow row)
        {
            return ((row["NUMERIC_SCALE"] as DBNull) != null) ? 0 : Int32.Parse(row["NUMERIC_SCALE"].ToString());
        }

        private static int GetPrecision(DataRow row)
        {
            return ((row["NUMERIC_PRECISION"] as DBNull) != null) ? 0 : Int32.Parse(row["NUMERIC_PRECISION"].ToString());
        }

        private static string GetDefaultValue(DataRow row)
        {
            return ((row["COLUMN_DEFAULT"] as DBNull) != null) ? "Null" : row["COLUMN_DEFAULT"].ToString();
        }
    }
}