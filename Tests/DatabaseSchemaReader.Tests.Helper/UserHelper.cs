using DatabaseSchemaReader.Website.Model;
using DatabaseSchemaReader.Website.ViewModels;

namespace DatabaseSchemaReader.Tests.Helper
{
    public class UserHelper
    {
        public static User MakeUser(string email, string password)
        {
            var user = new User
            {
                Email = email,
                Password = password
            };

            return user;
        }

        public static Register MakeRegister(string email, string password)
        {
            var register = new Register
            {
                Email = email,
                Password = password,
                ConfirmPassword = password
            };

            return register;
        }

        public static SignIn MakeSignIn(string email, string password)
        {
            return new SignIn
            {
                Email = email,
                Password = password
            };
        }
    }
}
