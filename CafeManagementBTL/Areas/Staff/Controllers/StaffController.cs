using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Web.Areas.Staff.Controllers
{
    public class StaffController : Controller
    {
        [Area("Staff")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
