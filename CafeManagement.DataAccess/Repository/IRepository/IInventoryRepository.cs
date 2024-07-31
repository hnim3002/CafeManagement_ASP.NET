using CafeManagement.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.DataAccess.Repository.IRepository
{
    public interface IInventoryRepository : IRepository<Inventory>
    {
        void Update(Inventory inventory);
        Task<IEnumerable<Inventory>> GetAllInventories();
        Task<Inventory> GetInventoriesById(int id);

    }
}
