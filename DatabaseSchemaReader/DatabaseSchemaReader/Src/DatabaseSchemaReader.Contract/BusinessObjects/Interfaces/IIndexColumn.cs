namespace DatabaseSchemaReader.Contract.BusinessObjects.Interfaces
{
    public interface IIndexColumn
    {
        string Name     { get; set; }
        bool Descending { get; set; }
    }
}