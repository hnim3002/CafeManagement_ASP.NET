using CafeManagement.DataAccess.Repository.IRepository;
using CafeManagement.Models.Entities;
using CafeManagement.Models.ViewModel;
using CafeManagement.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace CafeManagement.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = WebRoles.Web_Admin)]
    public class ReceiptController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReceiptController(IUnitOfWork unitOfWork)
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
            var receiptVM = new ReceiptVM
            {
                CafeList = _unitOfWork.Cafe.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            return View(receiptVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReceiptVM receiptVM)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var receipt = new Receipt
            {
                EmployeeId = userId,        
                CafeId = receiptVM.CafeId,
                Date = DateTime.Now,
                Discount = receiptVM.Discount,
                FinalTotal = receiptVM.FinalTotal,
                Id = Guid.NewGuid(),
                Tax = receiptVM.Tax,
                Total = receiptVM.Total
              
            };

            await _unitOfWork.Receipt.AddAsync(receipt);
            await _unitOfWork.SaveAsync();
            TempData["Success"] = "Receipt created successfully";
            return RedirectToAction("CreateDetail", new { id = receipt.Id });
            //return RedirectToAction("List");
        }


        [HttpGet]
        public async Task<IActionResult> CreateDetail(Guid id)
        {
            

            var receipt = await _unitOfWork.Receipt.GetAsync(u => u.Id == id);
            var receiptDetailVM = new ReceiptDetailVM
            {
                ReceiptId = receipt.Id,
                Receipt = receipt,
                InventoryList = _unitOfWork.Product.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            return View();
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
        public async Task<IActionResult> GetAllReceipts()
        {
            var receipts = await _unitOfWork.Receipt.GetAllAsync();

            var receiptVMs = receipts.Select(receipt => new ReceiptVM
            {
                EmployeeId = receipt.EmployeeId,
                CafeId = receipt.CafeId,
                Date = receipt.Date,
                Discount = receipt.Discount,
                FinalTotal = receipt.FinalTotal,
                Id = receipt.Id,
                Tax = receipt.Tax,
                Total = receipt.Total,
                CafeList = _unitOfWork.Cafe.GetAll().ToList().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            }).ToList();

            return Json(new { data = receipts });
        }
        #endregion
    }
}
