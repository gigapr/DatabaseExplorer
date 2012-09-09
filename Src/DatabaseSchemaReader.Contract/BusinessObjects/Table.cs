using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;

namespace DatabaseSchemaReader.Contract.BusinessObjects
{
    [DataContract]
    public class Table : ITable
    {
        public Table(string tableName)
        {
            Name = tableName;
        }

        [DataMember]
        public string Name                   { get; set; }

        [DataMember]
        public ICollection<Column> Columns   { get; set; }

        [DataMember]
        public IList<ForeignKey> ForeignKeys { get; set; }
        
        [DataMember]
        public IList<Index> Indexes          { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("Table Name         {0}", Name);
            stringBuilder.AppendLine();

            stringBuilder.AppendLine("Table Columns");
            foreach (var column in Columns)
            {
                stringBuilder.AppendLine(column.ToString());
            }
            stringBuilder.AppendLine();

            stringBuilder.AppendLine("Table ForeignKeys");
            foreach (var foreignKey in ForeignKeys)
            {
                stringBuilder.AppendLine(foreignKey.ToString());
            }
            stringBuilder.AppendLine();

            stringBuilder.AppendLine("Table Indexes");
            foreach (var index in Indexes)
            {
                stringBuilder.AppendLine(index.ToString());
            }
            stringBuilder.AppendLine();

            return stringBuilder.ToString();
        }
    }
}