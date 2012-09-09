using System.ComponentModel.DataAnnotations;

namespace DatabaseSchemaReader.Website.Models
{
    public class DatabaseConnection
    {
        [Required(ErrorMessage = "Datasource is required")]
        public string DataSource   { get; set; }

        [Required(ErrorMessage = "Database Name is required")]
        public string DatabaseName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username     { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Password     { get; set; }

        public string Provider    { get; set; }
    }
}