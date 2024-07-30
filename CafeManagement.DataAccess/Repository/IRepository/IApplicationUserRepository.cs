


using CafeManagement.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.DataAccess.Repository.IRepository
{
    public interface IApplicationUserRepository  : IRepository<ApplicationUser>
    {
        void Update(ApplicationUser obj);

        Task<String> GetRoleByIdAsync(String userId);
    }
}
