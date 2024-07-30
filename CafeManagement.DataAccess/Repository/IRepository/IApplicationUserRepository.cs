


using CafeManagement.Models.Entities;

namespace CafeManagement.DataAccess.Repository.IRepository
{
    public interface IApplicationUserRepository  : IRepository<ApplicationUser>
    {
        void Update(ApplicationUser obj);

        Task<String> GetRoleByIdAsync(String userId);
    }
}
