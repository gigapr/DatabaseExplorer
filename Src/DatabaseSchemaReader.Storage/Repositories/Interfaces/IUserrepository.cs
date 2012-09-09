using DatabaseSchemaReader.Storage.EntityModel;
using MongoDB.Driver;

namespace DatabaseSchemaReader.Storage.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User Get(int id);
        SafeModeResult Register(User user);
    }
}