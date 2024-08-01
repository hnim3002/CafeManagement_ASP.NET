using CafeManagement.DataAccess.Repository.IRepository;
using CafeManagement.Models.Entities;
using CafeManagement.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CafeManagement.Web.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Authorize(Roles = "Staff")]
    public class CafeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CafeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult List()
        {
            return View();
        }

       
        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAllCafe()
        {
            var cafes = await _unitOfWork.Cafe.GetAllAsync();
            return Json(new { data = cafes });
        }
        #endregion
    }
}
