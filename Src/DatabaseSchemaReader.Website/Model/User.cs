using DatabaseSchemaReader.Website.Model.Interfaces;

namespace DatabaseSchemaReader.Website.Model
{
    public class User : IUser
    {
        public string Email    { get; set; }
        public string Password { get; set; }
    }
}