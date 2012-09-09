using System.Runtime.Serialization;
using System.Text;
using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;

namespace DatabaseSchemaReader.Contract.BusinessObjects
{
    [DataContract]
    public class IndexColumn : IIndexColumn
    {
        [DataMember]
        public string Name      { get; set; }

        [DataMember]
        public bool Descending { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(string.Format("IndexColumn Name        {0}", Name));
            stringBuilder.AppendLine(string.Format("IndexColumn Descending  {0}", Descending));

            return stringBuilder.ToString();
        }
    }
}