using DatabaseSchemaReader.Website.ViewModels;

namespace DatabaseSchemaReader.Website.Services.Interfaces
{
    public interface IAccountService
    {
        bool RegisterUser(Register register);
        bool SignIn(SignIn signIn);
    }
}