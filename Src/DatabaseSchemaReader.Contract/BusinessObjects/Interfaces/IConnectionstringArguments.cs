using DatabaseSchemaReader.Contract.Enums;

namespace DatabaseSchemaReader.Contract.BusinessObjects.Interfaces
{
    public interface IConnectionstringArguments
    {
        string Provider           { get; set; }
        string DataSource         { get; set; } 
        string DatabaseName       { get; set; } 
        string Username           { get; set; } 
        string Password           { get; set; }
        DatabaseType DatabaseType { get; set; }
    }
}