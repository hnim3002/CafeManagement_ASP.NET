using CafeManagement.DataAccess.Repository.IRepository;
using CafeManagement.Models.Entities;
using CafeManagement.Models.ViewModel;
using CafeManagement.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Web.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Authorize(Roles = WebRoles.Web_Staff)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IUnitOfWork unitOfWork, ILogger<ProductController> logger)
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
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await _unitOfWork.Product.GetAllProduct();
            if (products == null)
            {
                _logger.LogError($"Product with ID not found.");
                TempData["Error"] = "Product not found.";
                return RedirectToAction("List");
            }
            return Json(new { data = products });
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var product = await _unitOfWork.Product.GetByProductIDAysnc(id);
            IEnumerable<Product> products = await _unitOfWork.Product.GetAllProduct();
            IEnumerable<Category> categories = await _unitOfWork.Category.GetAllAsync();
            ProductVM productVM = new ProductVM()
            {
                Product = product,
                products = products,
                category = categories
            };
            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            await _unitOfWork.Product.AddAsync(product);
            await _unitOfWork.SaveAsync();
            TempData["Success"] = "Cafe created successfully";
            return RedirectToAction("List");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _unitOfWork.Product.GetByProductIDAysnc(id);
            
            if (product == null)
            {
                _logger.LogError($"Product with ID {id} not found.");
                TempData["Error"] = "Product not found.";
                return RedirectToAction("List");
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            _unitOfWork.Product.Update(product);
            await _unitOfWork.SaveAsync();
            TempData["Success"] = "product updated successfully";
            return RedirectToAction("List");
            //if (product == null || product.Id <= 0)
            //{
            //    _logger.LogError("Edit failed: Product is null or ID is invalid.");
            //    TempData["Error"] = "Product not found or invalid.";
            //    return RedirectToAction("List");
            //}

            //if (!ModelState.IsValid)
            //{
            //    // ModelState không hợp lệ
            //    _logger.LogError("Edit failed: ModelState is invalid.");
            //    TempData["Error"] = "Invalid product data.";
            //    return View(product); // Trả về lại trang Edit với thông báo lỗi
            //}

            //_logger.LogInformation($"Attempting to Edit product with ID {product.Id}");

            //var existingProduct = await _unitOfWork.Product.GetByProductIDAysnc(product.Id);
            //if (existingProduct == null)
            //{
            //    _logger.LogError($"Product with ID {product.Id} not found.");
            //    TempData["Error"] = "Product not found.";
            //    return RedirectToAction("List");
            //}

            //// Cập nhật các thuộc tính của sản phẩm
            //existingProduct.Name = product.Name;
            //existingProduct.Price = product.Price;
            //// Thêm các thuộc tính khác nếu có

            //_unitOfWork.Product.Update(existingProduct);
            //await _unitOfWork.SaveAsync();
            //_logger.LogInformation("Product Edited successfully");

            //TempData["Success"] = "Product Edited successfully";
            //return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.Product.GetByProductIDAysnc(id);
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Product product)
        {

            _unitOfWork.Product.Delete(product);
            await _unitOfWork.SaveAsync();
            TempData["Success"] = "Cafe deleted successfully";

            return RedirectToAction("List");
        }

    }
}
