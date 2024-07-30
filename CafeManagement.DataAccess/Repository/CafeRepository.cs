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
    public class CafeRepository : Repository<Cafe>, ICafeRepository
    {
        private ApplicationDbContext _db;

        public CafeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Cafe obj)
        {
            _db.Cafes.Update(obj);
        }
    }
}
