using System.Web.Mvc;
using DatabaseSchemaReader.Website.Services.Interfaces;
using DatabaseSchemaReader.Website.ViewModels;

namespace DatabaseSchemaReader.Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public ActionResult SignIn(SignIn signIn)
        {
            _accountService.SignIn(signIn);

            return View("DatabaseExplorer");
        }

        [HttpPost]
        public ActionResult Register(Register register)
        {
            var succeeded = _accountService.RegisterUser(register);

            var message = succeeded ? "User successfully registered" : string.Format("User with email '{0}' already exist.", register.Email);

            return  Json(new { succeeded = succeeded, message = message});
        }        
    }
}