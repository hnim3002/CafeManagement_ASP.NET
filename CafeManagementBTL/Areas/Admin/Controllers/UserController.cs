using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Web.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        public IActionResult List()
        {
            return View();
        }



    }
}
