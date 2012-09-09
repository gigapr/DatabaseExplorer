using System.Collections.Generic;
using System.Data;
using System.Linq;
using DatabaseSchemaReader.Contract.BusinessObjects;
using GigaWebSolution.DatabaseSchemaReader.Mappers.Interfaces;

namespace GigaWebSolution.DatabaseSchemaReader.Mappers
{
    public class ForeignKeyMapper : IForeignKeyMapper
    {
        public ForeignKey MapToForeignKey(DataRow row, DataTable schema)
        {
            var dbForeignKey = new ForeignKey
            {
                Name                  = row["FK_NAME"].ToString(),
                OriginalName          = row["FK_NAME"].ToString(),
                ForeignKeyTableName   = row["FK_TABLE_NAME"].ToString(),
                PrimaryKeyTableName   = row["PK_TABLE_NAME"].ToString(),
                ForeignKeyTableSchema = schema.ToString()
            };

            var fkc = new ForeignKeyColumn { Name = row["FK_COLUMN_NAME"].ToString() };
            dbForeignKey.ForeignKeyColumns.Add(fkc);

            var pkc = new PrimaryKeyColumn { Name = row["PK_COLUMN_NAME"].ToString() };
            dbForeignKey.PrimaryKeysColumns.Add(pkc);

            return dbForeignKey;
        }

        public List<ForeignKey> MapForeignKeys(DataTable dataTable)
        {
            var foreignKeysList = new List<ForeignKey>();

            foreach (DataRow row in dataTable.Rows)
            {
                var mappedForeignKey = MapToForeignKey(row, dataTable);

                foreignKeysList.Add(mappedForeignKey);

                foreach (var temp in foreignKeysList.ToList())
                {
                    if(IsCompositeKey(mappedForeignKey, temp))
                    {                        
                        var compositeKey = MapCompositeKey(temp, mappedForeignKey);

                        foreignKeysList = foreignKeysList.Where(x => !(x.PrimaryKeyTableName == mappedForeignKey.PrimaryKeyTableName && x.ForeignKeyTableName == mappedForeignKey.ForeignKeyTableName)).ToList();

                        foreignKeysList.Add(compositeKey);
                        break;
                    }
                }
            }

            return foreignKeysList;
        }

        private static ForeignKey MapCompositeKey(ForeignKey temp, ForeignKey mappedForeignKey)
        {
            var primaryKeys = temp.PrimaryKeysColumns.Union(mappedForeignKey.PrimaryKeysColumns).ToList();
            var foreignKeys = temp.ForeignKeyColumns.Union(mappedForeignKey.ForeignKeyColumns).ToList();

            var compositeKey = temp;
            compositeKey.ForeignKeyColumns = foreignKeys;
            compositeKey.PrimaryKeysColumns = primaryKeys;
            return compositeKey;
        }

        private static bool IsCompositeKey(ForeignKey mappedForeignKey, ForeignKey t)
        {
            return t.PrimaryKeyTableName == mappedForeignKey.PrimaryKeyTableName && t.ForeignKeyTableName == mappedForeignKey.ForeignKeyTableName;
        }
    }
}