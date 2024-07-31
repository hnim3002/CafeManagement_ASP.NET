using CafeManagement.DataAccess.Repository;
using CafeManagement.DataAccess.Repository.IRepository;
using CafeManagement.Models.Entities;
using CafeManagement.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CafeManagement.Web.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Authorize(Roles = WebRoles.Web_Staff)]
    public class CategoryController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var categories = await _unitOfWork.Category.GetAllAsync();

            return Json(new { data = categories });
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            await _unitOfWork.Category.AddAsync(category);
            await _unitOfWork.SaveAsync();
            TempData["Success"] = "Cafe created successfully";
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var Categories = await _unitOfWork.Category.GetAsync(u => u.Id == id);
            return View(Categories);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Category category)
        {

            _unitOfWork.Category.Delete(category);
            await _unitOfWork.SaveAsync();
            TempData["Success"] = "Cafe deleted successfully";

            return RedirectToAction("List");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var Categories = await _unitOfWork.Category.GetAsync(u => u.Id == id);
            return View(Categories);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            _unitOfWork.Category.Update(category);
            await _unitOfWork.SaveAsync();
            TempData["Success"] = "Category updated successfully";
            return RedirectToAction("List");
        }
    }
}
