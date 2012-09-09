using DatabaseSchemaReader.Storage.EntityModel;
using DatabaseSchemaReader.Storage.Repositories.Interfaces;
using MongoDB.Driver;

namespace DatabaseSchemaReader.Storage.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionstring)
        {
            _connectionString = connectionstring;
        }

        public User Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public SafeModeResult Register(User user)
        {
            var databaseName = MongoUrl.Create(_connectionString).DatabaseName;
            
            var server = MongoServer.Create(_connectionString);

            var database = server.GetDatabase(databaseName);

            var collection = database.GetCollection<User>("users");

            var safeModeResult = collection.Insert(user);

            server.Disconnect();

            return safeModeResult;
        }
    }
}