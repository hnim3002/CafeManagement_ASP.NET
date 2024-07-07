using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Web.Areas.Staff.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
