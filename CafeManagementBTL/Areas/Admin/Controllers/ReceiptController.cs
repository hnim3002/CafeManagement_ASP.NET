﻿using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Web.Areas.Admin.Controllers
{
    public class ReceiptController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
