using System.Data.OleDb;

namespace DatabaseSchemaReader.Contract.BusinessObjects.Interfaces
{    
    public interface IColumn
    {
        string Name         { get; set; }
        string OriginalName { get; set; }
        bool AllowNulls     { get; set; }
        OleDbType TypeInfo  { get; set; }
        string DefaultValue { get; set; }
        int Precision       { get; set; }
        int Scale           { get; set; }
        bool InPrimaryKey   { get; set; }
        bool IsIdentity     { get; set; }
        int Length          { get; set; }
    }
}