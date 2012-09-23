using System.Web.Mvc;

namespace DatabaseSchemaReader.Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}
