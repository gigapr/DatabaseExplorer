using System.Linq;
using DatabaseSchemaReader.Tests.Helper;
using DatabaseSchemaReader.Website.Mappers;
using DatabaseSchemaReader.Website.Model;
using DatabaseSchemaReader.Website.Services;
using DatabaseSchemaReader.Website.Services.Interfaces;
using NUnit.Framework;
using Raven.Client.Embedded;

namespace DatabaseSchemaReader.Website.Test.Services
{
    [TestFixture]
    public class AccountServiceTests
    {
        private EmbeddableDocumentStore _documentStore;
        private static string _email;
        private static string _password;
        private IAccountService _accountService;

        [SetUp]
        public void SetUp()
        {
            _email = "gigapr@hotmail.com";
            _password = "Password";

            _documentStore = EmbeddableDocumentStoreHelper.GetInMemorySession();

            var userMapper = new UserMapper();

           _accountService = new AccountService(_documentStore, userMapper);
        }

        [Test]
        public void Should_be_able_to_register_a_user()
        {
            var register = UserHelper.MakeRegister(_email, _password);

            _accountService.RegisterUser(register);

            using (var session = _documentStore.OpenSession())
            {
                var user = session.Query<User>()
                                  .Customize(x => x.WaitForNonStaleResultsAsOfLastWrite())
                                  .First(u => u.Email == register.Email);

                Assert.IsNotNull(user);
                Assert.AreEqual(register.Email, user.Email);
                Assert.AreEqual(register.Password, user.Password);
            }
        }

        [Test]
        public void Should_not_register_duplicate_users()
        {
            var register = UserHelper.MakeRegister(_email, _password);

            _accountService.RegisterUser(register);
            _accountService.RegisterUser(register);

            using (var session = _documentStore.OpenSession())
            {
                var count = session.Query<User>()
                                  .Customize(x => x.WaitForNonStaleResultsAsOfLastWrite())
                                  .Count(u => u.Email == register.Email);

                Assert.AreEqual(1, count);
            }
        }

        [Test]
        public void Should_return_true_when_successfully_registering_a_user()
        {
            var register = UserHelper.MakeRegister(_email, _password);

            var result = _accountService.RegisterUser(register);

            Assert.IsTrue(result);
        }

        [Test]
        public void Should_return_be_able_to_sign_in_a_user()
        {
            var signIn = UserHelper.MakeSignIn(_email, _password);

            using (var session = _documentStore.OpenSession())
            {
                var makeUser = UserHelper.MakeUser(_email, _password);

                session.Store(makeUser);
            }

            var result = _accountService.SignIn(signIn);

            Assert.IsTrue(result);
        }         
    }
}