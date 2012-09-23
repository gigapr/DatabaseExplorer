using DatabaseSchemaReader.Website.Controllers;
using DatabaseSchemaReader.Website.Mappers.Interfaces;
using DatabaseSchemaReader.Website.Model;
using DatabaseSchemaReader.Website.ViewModels;
using NUnit.Framework;
using Raven.Client;
using Rhino.Mocks;

namespace DatabaseSchemaReader.Website.Test.Controllers
{
    [TestFixture, Category("Unit")]
    public class AccountControllerTest
    {
        private AccountController _accountController;
        private IDocumentSession _documentSession;
        private IUserMapper _userMapper;

        [SetUp]
        public void SetUp()
        {
            _documentSession = MockRepository.GenerateMock<IDocumentSession>();

            var documentStore = MockRepository.GenerateStub<IDocumentStore>();
            documentStore.Expect(store => store.OpenSession()).Return(_documentSession);

            _userMapper = MockRepository.GenerateMock<IUserMapper>();
            
            _accountController = new AccountController(documentStore, _userMapper);
        }

        [Test]
        public void Should_use_documentsession_to_register_a_user_into_ravendb_database()
        {
            var user = MakeUser();

            _userMapper.Expect(mapper => mapper.Map(Arg<Register>.Is.Anything)).Return(user);
            _documentSession.Expect(session => session.Store(user));
            _documentSession.Expect(session => session.SaveChanges());

            _accountController.Register(new Register());

            _documentSession.VerifyAllExpectations();
        }

        [Test]
        public void Should_use_documentsession_to_validate_login()
        {
            SignIn signIn = MakeSignIn();
            var user = MakeUser();

            _userMapper.Expect(mapper => mapper.Map(Arg<SignIn>.Is.Anything)).Return(user);
            _documentSession.Expect(session => session.Load<User>(user.Email, user.Password)).Return(Arg<User[]>.Is.Anything);

            _accountController.SignIn(signIn);

            _documentSession.VerifyAllExpectations();
        }

        [Test]
        public void Should_be_able_to_register_a_user()
        {
            var register = MakeRegister();

            _accountController.Register(register);
        }

        [Test]
        public void Should_use_usermapper_to_map_signin_to_user()
        {
            var register = MakeRegister();

            _accountController.Register(register);

            _userMapper.AssertWasCalled(mapper => mapper.Map(register));
        }

        [Test]
        public void Should_use_usermapper_to_map_register_to_user()
        {
            var signIn = MakeSignIn();

            _userMapper.Expect(mapper => mapper.Map(signIn)).Return(MakeUser());
            _accountController.SignIn(signIn);

            _userMapper.VerifyAllExpectations();
        }

        private static Register MakeRegister()
        {
            var register = new Register
            {
                Email           = "gigapr@hotmail.com",
                Password        = "Password",
                ConfirmPassword = "Password"
            };
         
            return register;
        }

        private static User MakeUser()
        {
            var user = new User
            {
                Email    = "gigapr@hotmail.com",
                Password = "Password"
            };
            
            return user;
        }

        private static SignIn MakeSignIn()
        {
            return new SignIn
            {
                Email    = "gigapr@hotmail.com",
                Password = "Password"
            };
        }
    }
}