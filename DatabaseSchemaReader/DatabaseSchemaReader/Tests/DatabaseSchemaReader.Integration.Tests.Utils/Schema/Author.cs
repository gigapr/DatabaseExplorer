using System.ComponentModel.DataAnnotations;

namespace DatabaseSchemaReader.Integration.Tests.Utils.Schema
{
    public class Author
    {
        [Key, Column(Order = 0)]
        public string Name       { get; set; }

        [Key, Column(Order = 1)]
        public string Surname    { get; set; }
    }
}
