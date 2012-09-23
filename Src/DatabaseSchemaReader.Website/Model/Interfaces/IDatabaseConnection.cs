using System.ComponentModel.DataAnnotations;

namespace DatabaseSchemaReader.Website.Model.Interfaces
{
    public interface IDatabaseConnection
    {
        [Required(ErrorMessage = "Datasource is required")]
        string DataSource { get; set; }

        [Required(ErrorMessage = "Database Name is required")]
        string DatabaseName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        string Password { get; set; }

        string Provider { get; set; }
    }
}