using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using CafeManagement.DataAccess.Data;
using CafeManagement.Models.Entities;
using Microsoft.Identity.Client;

namespace CafeManagement.Utilities
{
    public class DbInitializer : IDbInitializer
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _db;

        public DbInitializer(UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager
            , ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0) 
                { 
                    _db.Database.Migrate(); 
                }
            } 
            catch (Exception ex)
            {
                throw;
            }

            if(!_roleManager.RoleExistsAsync(WebRoles.Web_Admin).GetAwaiter().GetResult()) 
            { 
                _roleManager.CreateAsync(new IdentityRole(WebRoles.Web_Admin)).GetAwaiter().GetResult(); 
                _roleManager.CreateAsync(new IdentityRole(WebRoles.Web_Manager)).GetAwaiter().GetResult(); 
                _roleManager.CreateAsync(new IdentityRole(WebRoles.Web_Staff)).GetAwaiter().GetResult();


                var cafeId = _db.Cafes.FirstOrDefault().Id;
               

                var user = new ApplicationUser
                {
                    UserName = "Mink",
                    Email = "Mink@gmail.com",
                    CafeId = cafeId,

                    // Initialize other properties if needed
                };

                var result = _userManager.CreateAsync(user, "Mink@123").GetAwaiter().GetResult();

                if (result.Succeeded)
                {
                    var appUser = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "Mink@gmail.com");
                    if (appUser != null)
                    {
                        _userManager.AddToRoleAsync(appUser, WebRoles.Web_Admin).GetAwaiter().GetResult();
                    }
                }
            }
        }

    
    }
}
