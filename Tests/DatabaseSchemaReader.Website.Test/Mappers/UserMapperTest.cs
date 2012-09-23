using DatabaseSchemaReader.Website.Mappers;
using DatabaseSchemaReader.Website.Model;
using DatabaseSchemaReader.Website.ViewModels;
using DatabaseSchemaReader.Website.Mappers.Interfaces;
using NUnit.Framework;
using User = DatabaseSchemaReader.Website.Model.User;

namespace DatabaseSchemaReader.Website.Test.Mappers
{
    [TestFixture]
    public class UserMapperTest
    {
        private IUserMapper _userMapper;

        [SetUp]
        public void SetUp()
        {
            _userMapper = new UserMapper();
        }

        [Test]
        public void Should_be_able_to_map_a_register_to_user()
        {
            var register = new Register
            {
                Email = "Email",
                Password = "Password"
            };

            var user = _userMapper.Map(register);

            Assert.IsNotNull(user);
            Assert.AreEqual(register.Email, user.Email);
            Assert.AreEqual(register.Password, user.Password);
        }

        [Test]
        public void Should_be_able_to_map_asignin_to_user()
        {
            var signIn = new SignIn
            {
                Email = "Email",
                Password = "Password"
            };

            var user = _userMapper.Map(signIn);

            Assert.IsNotNull(user);
            Assert.AreEqual(user.Email, user.Email);
            Assert.AreEqual(user.Password, user.Password);
        }
    }
}