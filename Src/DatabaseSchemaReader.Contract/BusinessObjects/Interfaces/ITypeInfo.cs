namespace DatabaseSchemaReader.Contract.BusinessObjects.Interfaces
{
    public interface ITypeInfo
    {
        object Name             { get; set; }
        bool AllowIdentity      { get; set; }
        bool AllowLength        { get; set; }
        bool AllowNulls         { get; set; }
        bool IsBinary           { get; set; }
        bool IsVariableLength   { get; set; }
        int MaximumPrecision    { get; set; }
        int DefaultPrecision    { get; set; }
        bool HasPrecision       { get; set; }
    }
}