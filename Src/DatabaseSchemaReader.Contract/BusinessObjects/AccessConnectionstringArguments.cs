using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;
using DatabaseSchemaReader.Contract.Enums;

namespace DatabaseSchemaReader.Contract.BusinessObjects
{
    public class AccessConnectionstringArguments : IConnectionstringArguments
    {
        public string Provider           { get; set; }
        public string DataSource         { get; set; }
        public string Username           { get; set; }
        public string Password           { get; set; }
        public DatabaseType DatabaseType { get; set; }
    }
}
