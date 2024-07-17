using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Web.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
