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
    public class ReceiptDetailRepository : Repository<ReceiptDetail>, IReceiptDetailRepository
    {

        private ApplicationDbContext _db;

        public ReceiptDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(ReceiptDetail obj)
        {
            _db.ReceiptDetails.Update(obj);
        }
    }
}
