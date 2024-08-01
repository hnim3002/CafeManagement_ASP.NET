using CafeManagement.DataAccess.Repository;
using CafeManagement.DataAccess.Repository.IRepository;
using CafeManagement.Models.Entities;
using CafeManagement.Models.ViewModel;
using CafeManagement.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace CafeManagement.Web.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Authorize(Roles = WebRoles.Web_Staff)]
    public class InventoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<InventoryController> _logger;
        public InventoryController(IUnitOfWork unitOfWork, ILogger<InventoryController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpGet]
        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInventory()
        {
            var products = await _unitOfWork.Inventory.GetAllInventories();
            return Json(new { data = products });
        }

        //[HttpGet]
        //public async Task<IActionResult> Create()
        //{
        //    // Lấy danh sách tất cả sản phẩm và quán cà phê
        //    IEnumerable<Product> products = await _unitOfWork.Product.GetAllProduct();
        //    IEnumerable<Cafe> cafes = await _unitOfWork.Cafe.GetAllAsync();
        //    IEnumerable<Category> categories = await _unitOfWork.Category.GetAllAsync();
        //    // Tạo đối tượng view model
        //    InventoryVM inventoryVM = new InventoryVM()
        //    {
        //        products = products,
        //        cafes = cafes,
        //        categories = categories,
        //        inventory = new Inventory(),

        //    };

        //    // Trả về view với view model
        //    return View(inventoryVM);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(Inventory inventory)
        //{
        //    //var category = await _unitOfWork.Category.GetAsync(p => p.Id == inventoryVM.inventory.Product.CategoryId);
        //    //if (category == null)
        //    //{
        //    //    ModelState.AddModelError("inventory.Product.CategoryId", "Selected category does not exist.");
        //    //    inventoryVM.products = await _unitOfWork.Product.GetAllProduct();
        //    //    inventoryVM.cafes = await _unitOfWork.Cafe.GetAllAsync();
        //    //    inventoryVM.categories = await _unitOfWork.Category.GetAllAsync();
        //    //    return View(inventoryVM);
        //    //}

        //    // Thực hiện thêm Inventory
        //    await _unitOfWork.Inventory.AddAsync(inventory);
        //    await _unitOfWork.SaveAsync();
        //    TempData["Success"] = "Inventory created successfully";
        //    return RedirectToAction("List");
        //}


        //[HttpPost]
        //public async Task<IActionResult> Delete(int productId, Guid cafeId)
        //{

        //    //_unitOfWork.Inventory.Delete(inventory);
        //    //await _unitOfWork.SaveAsync();
        //    //TempData["Success"] = "Cafe deleted successfully";

        //    //return RedirectToAction("List");
        //    var inventory = await _unitOfWork.Inventory.GetAsync(i => i.ProductId == productId && i.CafeId == cafeId);

        //    if (inventory == null)
        //    {
        //        return NotFound();
        //    }

        //    _unitOfWork.Inventory.Delete(inventory);
        //    await _unitOfWork.SaveAsync();
        //    TempData["Success"] = "Inventory deleted successfully";
        //    return RedirectToAction("List");
        //}


        //[HttpGet]
        //public async Task<IActionResult> Edit(int productId, Guid cafeId)
        //{
        //    var inventory = await _unitOfWork.Inventory.GetAsync(i => i.ProductId == productId && i.CafeId == cafeId);

        //    if (inventory == null)
        //    {
        //        return NotFound();
        //    }

        //    // Trả về view với model Inventory để chỉnh sửa
        //    return View(inventory);
        //}


        //[HttpPost]
        //public async Task<IActionResult> Edit(int productId, Guid cafeId, Inventory model)
        //{
        //    var inventory = await _unitOfWork.Inventory.GetAsync(i => i.ProductId == productId && i.CafeId == cafeId);

        //    if (inventory == null)
        //    {
        //        return NotFound();
        //    }

        //    // Cập nhật các thuộc tính của inventory với các giá trị từ model
        //    inventory.Quantity = model.Quantity;
        //    // Cập nhật các thuộc tính khác nếu cần

        //    _unitOfWork.Inventory.Update(inventory);
        //    await _unitOfWork.SaveAsync();

        //    return RedirectToAction("List"); // Hoặc bất kỳ trang nào bạn muốn điều hướng sau khi chỉnh sửa
        //}

    }
}

    