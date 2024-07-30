using CafeManagement.Models;
using CafeManagement.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CafeManagementBTL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Cafe()
        {
            if (User.IsInRole(WebRoles.Web_Admin))
            {
                return RedirectToAction("List", "Cafe", new { area = "Admin" });
            }
            if (User.IsInRole(WebRoles.Web_Manager))
            {
                return RedirectToAction("List", "Cafe", new { area = "Manager" });
            }
            if (User.IsInRole(WebRoles.Web_Staff))
            {
                return RedirectToAction("List", "Cafe", new { area = "Staff" });
            }
            return Forbid();
        }


        public IActionResult UserApp()
        {
            if (User.IsInRole(WebRoles.Web_Admin))
            {
                return RedirectToAction("List", "User", new { area = "Admin" });
            }
            if (User.IsInRole(WebRoles.Web_Manager))
            {
                return RedirectToAction("List", "User", new { area = "Manager" });
            }
            if (User.IsInRole(WebRoles.Web_Staff))
            {
                return RedirectToAction("List", "Cafe", new { area = "Staff" });
            }
            return Forbid();
        }

        public IActionResult Customer()
        {
            if (User.IsInRole(WebRoles.Web_Admin))
            {
                return RedirectToAction("ListCustomer", "Customer", new { area = "Admin" });
            }
            if (User.IsInRole(WebRoles.Web_Manager))
            {
                return RedirectToAction("ListCustomer", "Customer", new { area = "Manager" });
            }
            if (User.IsInRole(WebRoles.Web_Staff))
            {
                return RedirectToAction("ListCustomer", "Customer", new { area = "Staff" });
            }
            return Forbid();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
