using DatabaseSchemaReader.Website.ViewModels;
using User = DatabaseSchemaReader.Website.Model.User;

namespace DatabaseSchemaReader.Website.Mappers.Interfaces
{
    public interface IUserMapper
    {
        User Map(Register register);
        User Map(SignIn signIn);
    }
}