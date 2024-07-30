using CafeManagement.DataAccess.Data;
using CafeManagement.DataAccess.Repository.IRepository;
using CafeManagement.Models.Entities;
using CafeManagement.Models.ViewModel;
using CafeManagement.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;

namespace CafeManagement.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AddUserController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<AddUserController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _db;

        public AddUserController(UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AddUserController> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _db = db;
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var roles = _roleManager.Roles.ToList();

            RegisterVM registerVM = new()
            {
                Roles = roles.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Name
                })
            };

            return View(registerVM);
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {

            if (ModelState.IsValid)
            {
                var cafeId = _db.Cafes.FirstOrDefault().Id;
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, CafeId = cafeId };
                var result = await _userManager.CreateAsync(user, model.Password);
          
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                     
                    var appUser = _db.ApplicationUsers.FirstOrDefault(u => u.Email == model.Email);
                    if (appUser != null)
                    {
                        _userManager.AddToRoleAsync(appUser, model.Role).GetAwaiter().GetResult();

                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            var roles = _roleManager.Roles.ToList();

            RegisterVM registerVM = new()
            {
                Roles = roles.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            return View(registerVM);
        }
    }
}
