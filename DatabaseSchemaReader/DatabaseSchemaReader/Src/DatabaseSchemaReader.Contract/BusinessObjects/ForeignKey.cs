using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;

namespace DatabaseSchemaReader.Contract.BusinessObjects
{
    [DataContract]
    public class ForeignKey : IForeignKey
    {
        public ForeignKey()
        {
            ForeignKeyColumns  = new List<ForeignKeyColumn>();
            PrimaryKeysColumns = new List<PrimaryKeyColumn>();
        }

        [DataMember]
        public string Name                                  { get; set; }

        [DataMember]
        public string OriginalName                          { get; set; }

        [DataMember]
        public string ForeignKeyTableName                   { get; set; }

        [DataMember]
        public string ForeignKeyTableSchema { get; set; }

        [DataMember]
        public List<ForeignKeyColumn> ForeignKeyColumns     { get; set; }

        [DataMember]
        public string PrimaryKeyTableName { get; set; }

        [DataMember]
        public string PrimaryKeyTableSchema { get; set; }

        [DataMember]
        public List<PrimaryKeyColumn> PrimaryKeysColumns { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(string.Format("ForeignKey Name                  {0}", Name));
            stringBuilder.AppendLine(string.Format("ForeignKey OriginalName          {0}", OriginalName));
            stringBuilder.AppendLine();

            stringBuilder.AppendLine(string.Format("ForeignKeyTableName   {0}", Name));
            stringBuilder.AppendLine(string.Format("ForeignKeyTableSchema {0}", ForeignKeyTableSchema));

            foreach (var foreignKey in ForeignKeyColumns)
            {
                stringBuilder.AppendLine(foreignKey.ToString());
            }
            stringBuilder.AppendLine();

            stringBuilder.AppendLine(string.Format("PrimaryKeyTableName   {0}", PrimaryKeyTableName));
            stringBuilder.AppendLine(string.Format("PrimaryKeyTableSchema {0}", PrimaryKeyTableSchema));

            foreach (var primaryKey in PrimaryKeysColumns)
            {
                stringBuilder.AppendLine(primaryKey.ToString());
            }
            stringBuilder.AppendLine();

            return stringBuilder.ToString();
        }
    }
}