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

namespace CafeManagement.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = WebRoles.Web_Admin)]
    public class UserController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(
            IUnitOfWork unitOfWork, 
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult List()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {
            AddUserVM vm = new AddUserVM
            {
                CafeList = _unitOfWork.Cafe.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                Genders = Enum.GetValues(typeof(Gender))
                          .Cast<Gender>()
                          .Select(g => new SelectListItem
                          {
                              Text = g.ToString(),
                              Value = g.ToString()
                          }),

               Roles = _roleManager.Roles.ToList().Select(i => new SelectListItem
               {
                   Text = i.Name,
                   Value = i.Name
               })
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddUserVM userVM)
        {
            var user = new ApplicationUser
            {
                FullName = userVM.FullName,
                Address = userVM.Address,
                PhoneNumber = userVM.PhoneNumber,
                UserName = userVM.UserName,
                Email = userVM.Email,
                DateOfBirth = userVM.DateOfBirth,
                Gender = userVM.Gender,
                CafeId = userVM.CafeId,
            };

            var result = _userManager.CreateAsync(user, "Cafe@123").GetAwaiter().GetResult();

            if (result.Succeeded)
            {
                var appUser = _unitOfWork.ApplicationUser.GetAsync(u => u.Email == userVM.Email).GetAwaiter().GetResult();
                if (appUser != null)
                {
                    _userManager.AddToRoleAsync(appUser, userVM.Role).GetAwaiter().GetResult();
                }
            }
            TempData["Success"] = "Cafe created successfully";
            return RedirectToAction("List");
        }

  
        public async Task<IActionResult> Edit(String id)
        {
            var user = await _unitOfWork.ApplicationUser.GetAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            AddUserVM vm = new AddUserVM
            {
                Address = user.Address,
                FullName = user.FullName,
                DateOfBirth = user.DateOfBirth,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                CafeId = user.CafeId,
                Role = await _unitOfWork.ApplicationUser.GetRoleByIdAsync(user.Id),

                CafeList = _unitOfWork.Cafe.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                Genders = Enum.GetValues(typeof(Gender))
                              .Cast<Gender>()
                              .Select(g => new SelectListItem
                              {
                                  Text = g.ToString(),
                                  Value = g.ToString()
                              }),
                Roles = _roleManager.Roles.ToList().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Name
                })
            };

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(AddUserVM vm)
        {
            var user = await _userManager.FindByIdAsync(vm.Id);

            user.FullName = vm.FullName;
            user.Address = vm.Address;
            user.PhoneNumber = vm.PhoneNumber;
            user.UserName = vm.UserName;
            user.Email = vm.Email;
            user.DateOfBirth = vm.DateOfBirth;
            user.Gender = vm.Gender;
            user.CafeId = vm.CafeId;

            // ... update other properties as needed

            var result = await _userManager.UpdateAsync(user);
            _userManager.AddToRoleAsync(user, vm.Role).GetAwaiter().GetResult();



            if (result.Succeeded)
            {
                return RedirectToAction(nameof(List));
            }


            AddUserVM newVM = new AddUserVM
            {
               
                CafeList = _unitOfWork.Cafe.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                Genders = Enum.GetValues(typeof(Gender))
                              .Cast<Gender>()
                              .Select(g => new SelectListItem
                              {
                                  Text = g.ToString(),
                                  Value = g.ToString()
                              }),
                Roles = _roleManager.Roles.ToList().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Name
                })

            };

            return View();

        }

        [HttpGet]
        public IActionResult PassChange()
        {
            return View();
        }


        public async Task<IActionResult> PassChange(PassChangeVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                TempData["Success"] = "Your password has been changed.";
                return RedirectToAction("List");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpDelete("/admin/user/delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found." });
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["Success"] = "User deleted successfully";
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Error deleting user." });
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
