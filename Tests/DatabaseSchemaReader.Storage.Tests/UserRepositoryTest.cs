using System;
using System.Security.Cryptography;
using DatabaseSchemaReader.Storage.EntityModel;
using DatabaseSchemaReader.Storage.Repositories;
using DatabaseSchemaReader.Storage.Repositories.Interfaces;
using MongoDB.Driver;
using NUnit.Framework;

namespace DatabaseSchemaReader.Storage.Tests
{
    [TestFixture]
    public class UserRepositoryTest
    {
        private IUserRepository _userRepository;
        private MongoServer _server;
        private string _connectionString;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _connectionString = "mongodb://localhost/TestDatabase?safe=true";

            _server = MongoServer.Create(_connectionString);

            _userRepository = new UserRepository(_connectionString);
        }

        [Test]
        public void Should_be_able_to_register_a_user()
        {
            var user = new User
            {
                Id          = new Oid(),
                Email       = "gigpr@hotmail.com",
                Password    = "Password",
                DateCreated = DateTime.Now
            };

            var databaseName = MongoUrl.Create(_connectionString).DatabaseName;

            var server = MongoServer.Create(_connectionString);

            var database = server.GetDatabase(databaseName);

            var collection = database.GetCollection<User>("users");

            var safeModeResult = collection.Insert(user);

            server.Disconnect();

            Assert.IsNotNull(safeModeResult);
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            var databaseName = MongoUrl.Create(_connectionString).DatabaseName;

            _server.DropDatabase(databaseName);
        }
    }
}
