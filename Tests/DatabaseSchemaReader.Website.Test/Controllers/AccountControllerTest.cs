using System.Web.Mvc;
using DatabaseSchemaReader.Tests.Helper;
using DatabaseSchemaReader.Website.Controllers;
using DatabaseSchemaReader.Website.Services.Interfaces;
using DatabaseSchemaReader.Website.ViewModels;
using NUnit.Framework;
using Raven.Client.Embedded;
using Rhino.Mocks;

namespace DatabaseSchemaReader.Website.Test.Controllers
{
    [TestFixture, Category("Unit")]
    public class AccountControllerTest
    {
        private EmbeddableDocumentStore _documentStore;
        private AccountController _accountController;
        private static string _email;
        private static string _password;
        private IAccountService _accountService;

        [SetUp]
        public void SetUp()
        {
            _email = "gigapr@hotmail.com";
            _password = "Password";

            _documentStore = EmbeddableDocumentStoreHelper.GetInMemorySession();

            _accountService = MockRepository.GenerateStub<IAccountService>();

            _accountController = new AccountController(_accountService);
        }

        [Test]
        public void Should_be_able_to_register_a_user()
        {
            _accountService.Expect(service => service.RegisterUser(Arg<Register>.Is.Anything)).Return(true);

            var register = UserHelper.MakeRegister(_email, _password);

            var result = _accountController.Register(register);

            Assert.IsNotNull(result);
            Assert.AreEqual("{ succeeded = True, message = User successfully registered }", ((JsonResult)(result)).Data.ToString());
        }

        [Test]
        public void Should_be_able_to_sign_in_a_user()
        {
            var signIn = UserHelper.MakeSignIn(_email, _password);

            using (var session = _documentStore.OpenSession())
            {
                var makeUser = UserHelper.MakeUser(_email, _password);

                session.Store(makeUser);
            }

            var actionResult = (RedirectToRouteResult)_accountController.SignIn(signIn);

            Assert.AreEqual("Index", actionResult.RouteValues["action"]);
            Assert.AreEqual("DatabaseExplorer", actionResult.RouteValues["controller"]);
        }        
    }
}