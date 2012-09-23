using System.Collections.Generic;
using DatabaseSchemaReader.Contract.BusinessObjects;
using DatabaseSchemaReader.Website.Model;

namespace DatabaseSchemaReader.Website.ViewModels
{
    public class DatabaseExplorer
    {
        public List<Table> Tables                    { get; set; }
        public DatabaseConnection DatabaseConnection { get; set; } 
    }
}