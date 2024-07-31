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
    public class ReceiptRepository : Repository<Receipt>, IReceiptRepository
    {
        private ApplicationDbContext _db;
        public ReceiptRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Receipt> GetReceiptWithDetailsAsync(Guid id)
        {
            return await _db.Receipts
               .Include(r => r.ReceiptDetails)
               .FirstOrDefaultAsync(r => r.Id == id);
        }


        public void Update(Receipt obj)
        {
            _db.Receipts.Update(obj);
        }
    }
}
