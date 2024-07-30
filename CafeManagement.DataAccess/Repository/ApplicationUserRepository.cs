using CafeManagement.DataAccess.Data;
using CafeManagement.DataAccess.Repository.IRepository;
using CafeManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<string> GetRoleByIdAsync(String userId)
        {
            var roleName = await(from user in _db.ApplicationUsers
                                 join userRole in _db.UserRoles on user.Id equals userRole.UserId
                                 join role in _db.Roles on userRole.RoleId equals role.Id
                                 where user.Id == userId
                                 select role.Name).FirstOrDefaultAsync();

            return roleName;
        }

        public void Update(ApplicationUser obj)
        {
            _db.ApplicationUsers.Update(obj);
        }
    }
}
