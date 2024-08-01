using CafeManagement.DataAccess.Repository.IRepository;
using CafeManagement.Models.Entities;
using CafeManagement.Models.ViewModel;
using CafeManagement.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace CafeManagement.Web.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Authorize(Roles = "Staff")]
    [Route("staff/[controller]/[action]")]
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
        }



        [HttpGet]

        public async Task<IActionResult> CreateDetail(Guid id)
        {
            var a = await _unitOfWork.Receipt.GetAsync(u => u.Id == id, includeProperties: "Cafe");


            var receipt = await _unitOfWork.Receipt.GetReceiptWithDetailsAsync(id);

            receipt.Cafe = a.Cafe;
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

            return View(receiptDetailVM);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDetail(ReceiptDetailVM receiptDetailVM)
        {
           
                var receiptDetail = new ReceiptDetail
                {
                    ReceiptId = receiptDetailVM.Receipt.Id,
                    ProductId = receiptDetailVM.ProductId,
                    CafeId = receiptDetailVM.Receipt.Cafe.Id,
                    Quantity = receiptDetailVM.Quantity,                  
                };

                await _unitOfWork.ReceiptDetail.AddAsync(receiptDetail);

            var inventory = await _unitOfWork.Inventory.GetAsync(u => u.ProductId == receiptDetail.ProductId && u.CafeId == receiptDetail.CafeId);

            var receipt = await _unitOfWork.Receipt.GetAsync(u => u.Id == receiptDetailVM.Receipt.Id);
            receipt.Total += receiptDetail.Quantity * inventory.Price;
            receipt.FinalTotal = receipt.Total - (receipt.Discount + receipt.Tax)* receipt.Total;
            _unitOfWork.Receipt.Update(receipt);
  

                await _unitOfWork.SaveAsync();
                TempData["Success"] = "Receipt detail added successfully";
                return RedirectToAction("CreateDetail", new { id = receiptDetailVM.Receipt.Id });
        }


        [HttpGet("/admin/Receipt/Detail/{id}")]
        public async Task<IActionResult> Detail(Guid id)
        {

            var a = await _unitOfWork.Receipt.GetAsync(u => u.Id == id, includeProperties: "Cafe");


            var receipt = await _unitOfWork.Receipt.GetReceiptWithDetailsAsync(id);

            receipt.Cafe = a.Cafe;
            var receiptDetailVM = new ReceiptDetailVM
            {
                ReceiptId = receipt.Id,
                Receipt = receipt,
            };

            return View(receiptDetailVM);


        }


        [HttpDelete("/admin/receipt/delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var receipt = await _unitOfWork.Receipt.GetReceiptWithDetailsAsync(id);

            if (receipt is not null)
            {
                // Delete associated receipt details
                foreach (var detail in receipt.ReceiptDetails)
                {
                    _unitOfWork.ReceiptDetail.Delete(detail);
                }

                // Delete the receipt
                _unitOfWork.Receipt.Delete(receipt);
                await _unitOfWork.SaveAsync();
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Error deleting user." });
        }


        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAllReceipts()
        {
            var receipts = await _unitOfWork.Receipt.GetAllAsync(includeProperties: "Cafe");

            var receiptVMs = receipts.Select(receipt => new ViewReceipt
            {
                EmployeeId = receipt.EmployeeId,
                Cafe = receipt.Cafe,
                Date = receipt.Date,
                Discount = receipt.Discount,
                FinalTotal = receipt.FinalTotal,
                Id = receipt.Id,
                Tax = receipt.Tax,
                Total = receipt.Total,
               
            }).ToList();

            return Json(new { data = receiptVMs });
        }
        #endregion
    }
}
