using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using CafeManagement.DataAccess.Data;

namespace CafeManagement.Utilities
{
    public class DbInitializer : IDbInitializer
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _db;

        public DbInitializer(UserManager<IdentityUser> userManager
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

                _userManager.CreateAsync(new IdentityUser
                {
                    UserName = "Mink",
                    Email = "Mink@gmail.com"
                },"Mink@123").GetAwaiter().GetResult();

                var Appuser = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "Mink@gmail.com");
                if(Appuser != null)
                {
                    _userManager.AddToRoleAsync(Appuser, WebRoles.Web_Admin).GetAwaiter().GetResult();
                }
            }
        }
    
    }
}
