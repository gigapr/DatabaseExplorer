using System.Web.Mvc;

namespace DatabaseExplorer.Controllers
{
    public class DatabaseExplorerController : Controller
    {
        public ViewResult Index()
        {
            return View("Index");
        }
    }
}
