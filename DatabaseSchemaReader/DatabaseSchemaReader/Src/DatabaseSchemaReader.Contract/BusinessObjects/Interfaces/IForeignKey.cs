using System.Collections.Generic;

namespace DatabaseSchemaReader.Contract.BusinessObjects.Interfaces
{
    public interface IForeignKey
    {
        string Name                               { get; set; }
        string OriginalName                       { get; set; }
        
        string ForeignKeyTableName                { get; set; }
        List<ForeignKeyColumn> ForeignKeyColumns  { get; set; }
        string ForeignKeyTableSchema              { get; set; }
        
        string PrimaryKeyTableName                { get; set; }
        List<PrimaryKeyColumn> PrimaryKeysColumns { get; set; }
        string PrimaryKeyTableSchema              { get; set; }
    }
}