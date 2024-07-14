using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
