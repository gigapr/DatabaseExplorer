using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;
using DatabaseSchemaReader.Contract.Enums;

namespace DatabaseSchemaReader.Contract.BusinessObjects
{
    [DataContract]
    public class Index : IIndex
    {
        public Index()
        {
            Columns = new List<IndexColumn>();
        }

        [DataMember]
        public string Name                  { get; set; }

        [DataMember]
        public string OriginalName { get; set; }

        [DataMember]
        public bool Unique { get; set; }

        [DataMember]
        public IndexType IndexType { get; set; }

        [DataMember]
        public List<IndexColumn> Columns { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(string.Format("Index Name          {0}", Name));
            stringBuilder.AppendLine(string.Format("Index OriginalName  {0}", OriginalName));
            stringBuilder.AppendLine(string.Format("Index Unique        {0}", Unique));
            stringBuilder.AppendLine(string.Format("Index IndexType     {0}", IndexType));
            stringBuilder.AppendLine();

            foreach (var index in Columns)
            {
                stringBuilder.AppendLine(index.ToString());
            }

            return stringBuilder.ToString();
        }
    }
}
