using System.Data.OleDb;
using System.Runtime.Serialization;
using System.Text;
using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;

namespace DatabaseSchemaReader.Contract.BusinessObjects
{
    [DataContract]
    public class Column : IColumn
    {
        [DataMember]
        public string Name          { get; set; }

        [DataMember]
        public string OriginalName  { get; set; }

        [DataMember]
        public bool AllowNulls      { get; set; }

        [DataMember]
        public OleDbType   TypeInfo { get; set; }

        [DataMember]
        public string DefaultValue  { get; set; }

        [DataMember]
        public int Precision        { get; set; }

        [DataMember]
        public int Scale            { get; set; }

        [DataMember]
        public bool InPrimaryKey    { get; set; }

        [DataMember]
        public bool IsIdentity      { get; set; }

        [DataMember]
        public int Length { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(string.Format("Column Name         {0}", Name));
            stringBuilder.AppendLine(string.Format("Column OriginalName {0}", OriginalName));
            stringBuilder.AppendLine(string.Format("Column AllowNulls   {0}", AllowNulls));
            stringBuilder.AppendLine(string.Format("Column TypeInfo     {0}", TypeInfo));
            stringBuilder.AppendLine(string.Format("Column DefaultValue {0}", DefaultValue));
            stringBuilder.AppendLine(string.Format("Column Precision    {0}", Precision));
            stringBuilder.AppendLine(string.Format("Column Scale        {0}", Scale));
            stringBuilder.AppendLine(string.Format("Column InPrimaryKey {0}", InPrimaryKey));
            stringBuilder.AppendLine(string.Format("Column IsIdentity   {0}", IsIdentity));
            stringBuilder.AppendLine(string.Format("Column Length       {0}", Length));
            stringBuilder.AppendLine();

            return stringBuilder.ToString();
        }
    }
}