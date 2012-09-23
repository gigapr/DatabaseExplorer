using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DatabaseSchemaReader.Website.Model.Interfaces;

namespace DatabaseSchemaReader.Website.Model
{
    public class DatabaseConnection : IDatabaseConnection
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

        [Required(ErrorMessage = "Databse Type is required")]
        public SelectList DatabaseType { get; set; }

        public string Provider    { get; set; }
    }
}