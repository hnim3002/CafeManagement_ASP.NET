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
    public class WorkSchedulesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkSchedulesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult ListWorkSchedules()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateWorkSchedules()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkSchedules(WorkSchedules workSchedules)
        {
            await _unitOfWork.WorkSchedules.AddAsync(workSchedules);
            await _unitOfWork.SaveAsync();
            TempData["Success"] = "WorkSchedules created successfully";
            return RedirectToAction("ListWorkSchedules");
        }

        [HttpGet]
        public async Task<IActionResult> EditWorkSchedules(Guid id)
        {
            var WorkSchedules = await _unitOfWork.WorkSchedules.GetAsync(u => u.Id == id);
            return View(WorkSchedules);
        }


        [HttpPost]
        public async Task<IActionResult> EditCustomer(WorkSchedules workSchedules)
        {

            _unitOfWork.WorkSchedules.Update(workSchedules);
            await _unitOfWork.SaveAsync();
            TempData["Success"] = "WorkSchedules updated successfully";
            return RedirectToAction("ListWorkSchedules");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteWorkSchedules(Guid id)
        {
            var workSchedules = await _unitOfWork.WorkSchedules.GetAsync(u => u.Id == id);

            if (workSchedules is not null)
            {
                _unitOfWork.WorkSchedules.Delete(workSchedules);
                await _unitOfWork.SaveAsync();
                TempData["Success"] = "Customer deleted successfully";
            }
            return RedirectToAction("ListCustomer");
        }


        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAllWorkSchedules()
        {
            var workSchedules = await _unitOfWork.WorkSchedules.GetAllAsync();

            return Json(new { data = workSchedules });
        }
        #endregion

vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv
    }
}
