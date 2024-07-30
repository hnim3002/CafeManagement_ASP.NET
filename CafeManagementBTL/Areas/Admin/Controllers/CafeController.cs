using CafeManagement.DataAccess.Repository.IRepository;
using CafeManagement.Models.Entities;
using CafeManagement.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CafeManagement.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = WebRoles.Web_Admin)]
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

        [HttpGet]
        public IActionResult Create() 
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cafe cafe)
        {
            await _unitOfWork.Cafe.AddAsync(cafe);
            await _unitOfWork.SaveAsync();
            TempData["Success"] = "Cafe created successfully";
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var cafe = await _unitOfWork.Cafe.GetAsync(u => u.Id == id);
            return View(cafe);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Cafe cafe)
        {
             _unitOfWork.Cafe.Update(cafe);
             await _unitOfWork.SaveAsync();
             TempData["Success"] = "Cafe updated successfully";
             return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var category = await _unitOfWork.Cafe.GetAsync(u => u.Id == id);

            if (category is not null)
            {
                _unitOfWork.Cafe.Delete(category);
                await _unitOfWork.SaveAsync();
                TempData["Success"] = "Cafe deleted successfully";
            }
            return RedirectToAction("List");
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
