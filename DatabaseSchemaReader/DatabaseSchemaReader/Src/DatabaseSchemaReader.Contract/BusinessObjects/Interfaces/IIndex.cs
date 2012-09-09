using System.Collections.Generic;
using DatabaseSchemaReader.Contract.Enums;

namespace DatabaseSchemaReader.Contract.BusinessObjects.Interfaces
{
    public interface IIndex
    {
        string Name                 { get; set; }
        string OriginalName         { get; set; }
        bool Unique                 { get; set; }
        IndexType IndexType         { get; set; }
        List<IndexColumn> Columns   { get; set; }
    }
}