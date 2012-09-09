using System.Runtime.Serialization;

namespace DatabaseSchemaReader.Contract.Enums
{
    [DataContract]
    public enum IndexType
    {
        [EnumMember]
        PrimaryKey,

        [EnumMember]
        Index
    }
}