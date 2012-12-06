using System.ComponentModel.DataAnnotations;

namespace DatabaseSchemaReader.Website.Model
{
    //TODO Conditional validation
    public class DatabaseConnection 
    {
        [Required(ErrorMessage = "Datasource is required")]
        public string DataSource       { get; set; }

        [Required(ErrorMessage = "Provider is required")]
        public string Provider         { get; set; }

        [Required(ErrorMessage = "Databse Type is required")]
        public string DatabaseType     { get; set; }

        //[Required(ErrorMessage = "Database Name is required")]
        public string DatabaseName     { get; set; }

        public string Username         { get; set; }

        [DataType(DataType.Password)]
        public string Password         { get; set; }

        public bool IntegratedSecurity { get; set; }
    }
}