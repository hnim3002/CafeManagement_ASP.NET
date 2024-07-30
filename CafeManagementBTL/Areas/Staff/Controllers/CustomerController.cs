using CafeManagement.DataAccess.Repository.IRepository;
using CafeManagement.Models.Entities;
using CafeManagement.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CafeManagement.Web.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Authorize(Roles = WebRoles.Web_Staff)]
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult ListCustomer()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            await _unitOfWork.Customer.AddAsync(customer);
            await _unitOfWork.SaveAsync();
            TempData["Success"] = "Customer created successfully";
            return RedirectToAction("ListCustomer");
        }

        [HttpGet]
        public async Task<IActionResult> EditCustomer(Guid id)
        {
            var customer = await _unitOfWork.Customer.GetAsync(u => u.Id == id);
            return View(customer);
        }


        [HttpPost]
        public async Task<IActionResult> EditCustomer(Customer customer)
        {

            _unitOfWork.Customer.Update(customer);
            await _unitOfWork.SaveAsync();
            TempData["Success"] = "Customer updated successfully";
            return RedirectToAction("ListCustomer");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var receipt = await _unitOfWork.Customer.GetAsync(u => u.Id == id);

            if (receipt is not null)
            {
                _unitOfWork.Customer.Delete(receipt);
                await _unitOfWork.SaveAsync();
                TempData["Success"] = "Customer deleted successfully";
            }
            return RedirectToAction("ListCustomer");
        }


        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAllCustomer()
        {
            var customers = await _unitOfWork.Customer.GetAllAsync();

            return Json(new { data = customers });
        }
        #endregion
    }
}
