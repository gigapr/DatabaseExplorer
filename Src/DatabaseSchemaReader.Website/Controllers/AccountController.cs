using System;
using System.Web.Mvc;
using DatabaseSchemaReader.Website.Models;
using Raven.Client;

namespace DatabaseSchemaReader.Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly IDocumentStore _documentStore;

        public AccountController(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        [HttpPost]
        public JsonResult SignIn(SignIn sign)
        {
            using (var session = _documentStore.OpenSession())
            {
                session.Store(sign);

                session.SaveChanges();
            }

            return Json(new { Message = "Success" }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Register(SignIn sign)
        {
            //var user = new User
            //{
            //    Password = "Password",
            //    Username = "Username"
            //}; 
            using (var session = _documentStore.OpenSession())
            {
                session.Store(sign);

                session.SaveChanges();
            }    

            return View();
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}