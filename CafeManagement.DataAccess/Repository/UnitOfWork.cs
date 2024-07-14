using CafeManagement.DataAccess.Data;
using CafeManagement.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public ICafeRepository Cafe { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Cafe = new CafeRepository(_db);
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
