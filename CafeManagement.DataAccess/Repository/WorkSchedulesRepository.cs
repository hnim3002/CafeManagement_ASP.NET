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
    public class WorkSchedulesRepository : Repository<WorkSchedules>, IWorkSchedulesRepository
    {
        private ApplicationDbContext _db;

        public WorkSchedulesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(WorkSchedules obj)
        {
            _db.WorkSchedules.Update(obj);
        }
    }
}
