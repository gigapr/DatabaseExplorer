using System.Collections.Generic;

namespace DatabaseSchemaReader.Contract.BusinessObjects.Interfaces
{
    public interface ITable
    {
        string Name                     { get; set; }
        ICollection<Column> Columns     { get; set; }
        IList<ForeignKey> ForeignKeys   { get; set; }
        IList<Index> Indexes            { get; set; }
    }
}