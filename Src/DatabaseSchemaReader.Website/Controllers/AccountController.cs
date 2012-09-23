using System.Web.Mvc;
using DatabaseSchemaReader.Website.Mappers.Interfaces;
using DatabaseSchemaReader.Website.Model;
using DatabaseSchemaReader.Website.ViewModels;
using Raven.Client;

namespace DatabaseSchemaReader.Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly IDocumentStore _documentStore;
        private readonly IUserMapper _userMapper;

        public AccountController(IDocumentStore documentStore, IUserMapper userMapper)
        {
            _documentStore = documentStore;
            _userMapper = userMapper;
        }

        [HttpPost]
        public ActionResult SignIn(SignIn signIn)
        {
            var user = _userMapper.Map(signIn);

            using (var session = _documentStore.OpenSession())
            {
                session.Load<User>(user.Email, user.Password);

                session.SaveChanges();
            }

            return View();
        }

        [HttpPost]
        public ActionResult Register(Register register)
        {
            var user = _userMapper.Map(register);

            using (var session = _documentStore.OpenSession())
            {
                session.Store(user);

                session.SaveChanges();
            }    

            return View();
        }
    }
}