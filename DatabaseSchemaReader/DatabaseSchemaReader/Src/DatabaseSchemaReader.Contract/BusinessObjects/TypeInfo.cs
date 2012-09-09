using System.Runtime.Serialization;
using System.Text;
using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;

namespace DatabaseSchemaReader.Contract.BusinessObjects
{
    public class TypeInfo : ITypeInfo
    {
        [DataMember]
        public object Name { get; set; }

        [DataMember]
        public bool AllowIdentity       { get; set; }

        [DataMember]
        public bool AllowLength         { get; set; }

        [DataMember]
        public bool AllowNulls          { get; set; }

        [DataMember]
        public bool IsBinary            { get; set; }

        [DataMember]
        public bool IsVariableLength    { get; set; }

        [DataMember]
        public int MaximumPrecision     { get; set; }

        [DataMember]
        public int DefaultPrecision     { get; set; }

        [DataMember]
        public bool HasPrecision        { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("TypeInfo Name               {0}", Name);
            stringBuilder.AppendFormat("TypeInfo AllowIdentity      {0}", AllowIdentity);
            stringBuilder.AppendFormat("TypeInfo AllowLength        {0}", AllowLength);
            stringBuilder.AppendFormat("TypeInfo AllowNulls         {0}", AllowNulls);
            stringBuilder.AppendFormat("TypeInfo IsBinary           {0}", IsBinary);
            stringBuilder.AppendFormat("TypeInfo IsVariableLength   {0}", IsVariableLength);
            stringBuilder.AppendFormat("TypeInfo MaximumPrecision   {0}", MaximumPrecision);
            stringBuilder.AppendFormat("TypeInfo DefaultPrecision   {0}", DefaultPrecision);
            stringBuilder.AppendFormat("TypeInfo HasPrecision       {0}", HasPrecision);

            return stringBuilder.ToString();
        }
    }
}