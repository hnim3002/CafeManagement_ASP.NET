using CafeManagement.DataAccess.Data;
using CafeManagement.DataAccess.Repository.IRepository;
using CafeManagement.Models.Entities;
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

        public void Update(Receipt obj)
        {
            _db.Receipts.Update(obj);
        }
    }
}
