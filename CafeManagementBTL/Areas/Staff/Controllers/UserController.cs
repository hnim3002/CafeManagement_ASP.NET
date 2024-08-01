using CafeManagement.DataAccess.Repository.IRepository;
using CafeManagement.Models;
using CafeManagement.Models.Entities;
using CafeManagement.Models.ViewModel;
using CafeManagement.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CafeManagement.Web.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Authorize(Roles = "Staff")]
    public class UserController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;


        public UserController(
            IUnitOfWork unitOfWork

            )
        {
            _unitOfWork = unitOfWork;

        }

        public IActionResult List()
        {
            return View();
        }



        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAllUser(Guid? cafeId = null)
        {
            if (cafeId != null)
            {
                return Json(new { data = "succes" });
            }
            else
            {
                var users = await _unitOfWork.ApplicationUser.GetAllAsync(includeProperties: "Cafe");

                List<UserVM> userVMs = new List<UserVM>();

                foreach (var user in users)
                {
                    userVMs.Add(new UserVM
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        Address = user.Address,
                        DateOfBirth = user.DateOfBirth,
                        PhoneNumber = user.PhoneNumber,
                        Role = await _unitOfWork.ApplicationUser.GetRoleByIdAsync(user.Id),
                        Cafe = user.Cafe
                    });
                }
                return Json(new { data = userVMs });
            }
        }
        #endregion

    }
}
