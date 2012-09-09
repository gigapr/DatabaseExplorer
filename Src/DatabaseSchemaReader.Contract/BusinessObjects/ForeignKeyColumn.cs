using System.Runtime.Serialization;
using System.Text;
using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;

namespace DatabaseSchemaReader.Contract.BusinessObjects
{
    [DataContract]
    public class ForeignKeyColumn : IForeignKeyColumn
    {
        [DataMember]
        public string Name { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("ForeignKeyColumn Name {0}", Name);
            stringBuilder.AppendLine();

            return stringBuilder.ToString();
        }
    }
}