using System.Linq;
using DatabaseSchemaReader.Website.Controllers;
using DatabaseSchemaReader.Website.Mappers;
using DatabaseSchemaReader.Website.Mappers.Interfaces;
using DatabaseSchemaReader.Website.Model;
using DatabaseSchemaReader.Website.ViewModels;
using NUnit.Framework;
using Raven.Client.Embedded;

namespace DatabaseSchemaReader.Website.Test.Controllers
{
    [TestFixture, Category("Unit")]
    public class AccountControllerTest
    {
        private EmbeddableDocumentStore _documentStore;
        private AccountController _accountController;
        private IUserMapper _userMapper;
        private static string _email;
        private static string _password;

        [SetUp]
        public void SetUp()
        {
            _email = "gigapr@hotmail.com";
            _password = "Password";

            _documentStore = GetInMemorySession();

            _userMapper = new UserMapper();

            _accountController = new AccountController(_documentStore, _userMapper);
        }

        [Test]
        public void Should_be_able_to_register_a_user()
        {
            var register = MakeRegister();

            _accountController.Register(register);

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
        public void Should_be_able_to_sign_in_a_user()
        {
            var signIn = MakeSignIn();

            using (var session = _documentStore.OpenSession())
            {
                var makeUser = MakeUser();

                session.Store(makeUser);
            }

            var viewResult = _accountController.SignIn(signIn);

            Assert.AreEqual("DatabaseExplorer", viewResult.ViewName);
        }

        private static User MakeUser()
        {
            var user = new User
            {
                Email = _email,
                Password = _password
            };

            return user;
        }

        private static Register MakeRegister()
        {
            var register = new Register
            {
                Email = _email,
                Password = _password,
                ConfirmPassword = _password
            };

            return register;
        }

        private static SignIn MakeSignIn()
        {
            return new SignIn
            {
                Email = _email,
                Password = _password
            };
        }

        private static EmbeddableDocumentStore GetInMemorySession()
        {
            var documentStore = new EmbeddableDocumentStore
            {
                RunInMemory = true,
                UseEmbeddedHttpServer = true
            };

            documentStore.Initialize();

            return documentStore;
        }
    }
}