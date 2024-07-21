using CafeManagement.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Web.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult List()
        {
            return View();
        }

        //#region API CALLS
        //[HttpGet]
        //public async Task<IActionResult> GetAllCafe()
        //{
        //    var categorys = await _unitOfWork.Category.GetAllAsync();

        //    return Json(new { data = categorys });
        //}
        //#endregion
    }
}
