using CafeManagement.DataAccess.Data;
using CafeManagement.DataAccess.Repository.IRepository;
using CafeManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.DataAccess.Repository
{
    public class InventoryRepository : Repository<Inventory>, IInventoryRepository
    {

        private readonly ApplicationDbContext _context;
        public InventoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Inventory>> GetAllInventories()
        {
            return await _context.Inventories.Include(c => c.Product).Include(d=>d.Cafe).ToListAsync();
        }

        public async Task<Inventory> GetInventoriesById(int id)
        {
            return await _context.Inventories.Include(c => c.Product).Include(d => d.Cafe).FirstOrDefaultAsync(c => c.ProductId == id);
        }

        public void Update(Inventory inventory)
        {
            _context.Inventories.Update(inventory);
        }
    }
}
