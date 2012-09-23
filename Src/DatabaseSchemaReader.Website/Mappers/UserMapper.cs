using DatabaseSchemaReader.Website.Mappers.Interfaces;
using DatabaseSchemaReader.Website.ViewModels;
using User = DatabaseSchemaReader.Website.Model.User;

namespace DatabaseSchemaReader.Website.Mappers
{
    public class UserMapper : IUserMapper
    {
        public User Map(Register register)
        {
            return new User
            {
                Email    = register.Email,
                Password = register.Password
            };
        }

        public User Map(SignIn signIn)
        {
            return new User
            {
                Email    = signIn.Email,
                Password = signIn.Password
            };
        }
    }
}