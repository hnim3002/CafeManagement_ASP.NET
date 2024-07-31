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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context) {
             _context = context;
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Product> GetByProductIDAysnc(int id)
        {
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(c=>c.Id==id);
        }
    }
}
